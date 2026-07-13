// <copyright file="LibraryDataService.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using PhotoDb.Data.Models;
using PhotoDb.Data.Models.Libraries;
using PhotoDb.Imaging;
using Xobex.Cryptography.Abstractions;

namespace PhotoDb.Data.Services;

public sealed class LibraryDataService : DataServiceBase
{
    private readonly ICryptoIdEncoder<int> _int32encoder;

    public LibraryDataService(PhotoDbContextBase context, ICryptoIdEncoder<int> int32encoder) : base(context)
    {
        ArgumentNullException.ThrowIfNull(int32encoder);
        _int32encoder = int32encoder;
    }

    public async Task<List<LibraryViewEntry>> GetItemListAsync(CancellationToken cancellation = default)
    {
        var query = from item in Context.Library.AsNoTracking()
                    select new LibraryViewEntry()
                    {
                        Id = _int32encoder.Encode(item.Id),
                        State = item.State,
                        Name = item.Name,
                        Path = item.Path,
                        LastScanDate = item.LastScanDate,
                        Created = item.Created,
                        Modified = item.Modified,
                        ImageCount = item.Image.Count
                    };

        return await query.ToListAsync(cancellation).ConfigureAwait(false);
    }

    public async Task<LibraryViewEntry> CreateLibraryAsync(CreateLibraryRequest request, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(request.Name);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(request.Path);
        if (!Directory.Exists(request.Path))
        {
            throw new ArgumentException("invalid path specified", nameof(request));
        }

        var entry = new Library()
        {
            State = 1,
            Name = request.Name,
            Path = Path.GetFullPath(request.Path),
            Description = request.Description,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
        };

        Context.Library.Add(entry);
        await Context.SaveChangesAsync(cancellation).ConfigureAwait(false);

        return await GetItemAsync(entry.Id, default, cancellation).ConfigureAwait(false);
    }

    public async Task<LibraryViewEntry> UpdateLibraryAsync(int id, UpdateLibraryRequest request, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(request.Name);
        var found = await Context.Library.SingleAsync(t => t.Id == id, cancellation).ConfigureAwait(false);
        found.Name = request.Name;
        found.Description = request.Description;
        await Context.SaveChangesAsync(cancellation).ConfigureAwait(false);

        return await GetItemAsync(id, default, cancellation).ConfigureAwait(false);
    }

    public async Task<LibraryViewEntry> GetItemAsync(int id, LibraryViewEntry? _, CancellationToken cancellation = default)
    {
        var query = from item in Context.Library.AsNoTracking()
                    where item.Id == id
                    select new LibraryViewEntry()
                    {
                        Id = _int32encoder.Encode(item.Id),
                        State = item.State,
                        Name = item.Name,
                        Path = item.Path,
                        LastScanDate = item.LastScanDate,
                        Description = item.Description,
                        Created = item.Created,
                        Modified = item.Modified,
                        ImageCount = item.Image.Count
                    };
        return await query.SingleAsync(cancellation);
    }

    public async Task DeleteLibraryAsync(int id, CancellationToken cancellation = default)
    {
        var found = await Context.Library.AnyAsync(t => t.Id == id, cancellation).ConfigureAwait(false);
        if (!found)
        {
            throw new ArgumentException("library with specified id does not exist", nameof(id));
        }
        using var transaction = await Context.Database.BeginTransactionAsync(cancellation).ConfigureAwait(false);

        try
        {
            await Context.ImageTag.Where(t => t.Image.LibraryId == id).ExecuteDeleteAsync(cancellation).ConfigureAwait(false);

            await Context.AlbumImage.Where(t => t.Image.LibraryId == id).ExecuteDeleteAsync(cancellation).ConfigureAwait(false);

            await Context.Image.Where(t => t.LibraryId == id).ExecuteDeleteAsync(cancellation).ConfigureAwait(false);

            await Context.Library.Where(t => t.Id == id).ExecuteDeleteAsync(cancellation).ConfigureAwait(false);

            await (from item in Context.TagItem
                   where !Context.ImageTag.Any(it => it.TagId == item.Id)
                   select item).ExecuteDeleteAsync(cancellation).ConfigureAwait(false);

            await transaction.CommitAsync(cancellation).ConfigureAwait(false);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellation).ConfigureAwait(false);
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="libraryId">library id</param>
    /// <returns></returns>
    public async Task<ScanLibraryResponse> ScanLibraryAsync(int libraryId, int batchSize = 50, CancellationToken cancellation = default)
    {
        var library = await Context.Library.SingleAsync(t => t.Id == libraryId, cancellation).ConfigureAwait(false);

        if (!Directory.Exists(library.Path))
        {
            throw new InvalidOperationException($"Physical directory path doesn't exist on server: {library.Path}");
        }

        var startTime = DateTime.UtcNow;
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp", ".svg" };

        // Scan files recursively with safe directory enumeration
        var physicalFiles = new List<FileInfo>();
        var allowedExtsSet = new HashSet<string>(allowedExtensions, StringComparer.OrdinalIgnoreCase);
        FileUtils.FindImagesRecursivelySafe(library.Path, physicalFiles, allowedExtsSet);

        var scannedFilePaths = physicalFiles.Select(f => f.FullName).ToHashSet(StringComparer.OrdinalIgnoreCase);

        // Cascade drop missing image records from SQLite
        var dbImages = await Context.Image.Where(i => i.LibraryId == libraryId).ToListAsync(cancellation).ConfigureAwait(false);
        var removedCount = 0;

        foreach (var image in dbImages)
        {
            if (!scannedFilePaths.Contains(image.FilePath))
            {
                Context.Image.Remove(image);
                var relationTags = Context.ImageTag.Where(it => it.ImageId == image.Id);
                Context.ImageTag.RemoveRange(relationTags);
                removedCount++;
            }
        }

        // Insert newly detected files
        var addedCount = 0;
        var updatedCount = 0;
        var currentPathsInDB = dbImages.Select(img => img.FilePath).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var existingImages = dbImages.ToDictionary(img => img.FilePath, img => img, StringComparer.OrdinalIgnoreCase);

        var buffer = new byte[1024 * 1024];
        var count = 0;
        foreach (var file in physicalFiles)
        {
            var metadata = ExifMetadataExtractor.ReadMetadataCore(file.FullName);
            using var stream = file.OpenRead();
            // Lightweight hashing of first 1MB directory files (optimal indexing speed)
            var bytesRead = await stream.ReadAsync(buffer, cancellation).ConfigureAwait(false);
            var fileHash = SHA256.HashData(buffer.AsSpan(0, bytesRead));

            if (!currentPathsInDB.Contains(file.FullName))
            {
                var newImage = new Image
                {
                    State = 1,
                    LibraryId = libraryId,
                    Name = file.Name,
                    FilePath = file.FullName,
                    Created = file.CreationTimeUtc,
                    IndexedDate = DateTime.UtcNow,
                    Rating = 0,
                    Flag = 0,
                    Hash = fileHash,
                    Size = file.Length,
                    Width = metadata.Width,
                    Height = metadata.Height,
                    CameraModel = metadata.CameraModel,
                    LensModel = metadata.LensModel,
                    FocalLength = metadata.FocalLength,
                    Aperture = metadata.Aperture,
                    IsoSpeed = metadata.IsoSpeed,
                    ExposureTime = metadata.ExposureTime,
                    ShootingDate = metadata.ShootingDate,
                    Modified = DateTime.UtcNow
                };
                Context.Image.Add(newImage);
                addedCount++;
            }
            else
            {
                var foundImage = existingImages[file.FullName];
                if (!Enumerable.SequenceEqual(foundImage.Hash, fileHash))
                {
                    foundImage.Created = file.CreationTimeUtc;
                    foundImage.IndexedDate = DateTime.UtcNow;
                    foundImage.Rating = 0;
                    foundImage.Flag = 0;
                    foundImage.Hash = fileHash;
                    foundImage.Size = file.Length;
                    foundImage.Width = metadata.Width;
                    foundImage.Height = metadata.Height;
                    foundImage.CameraModel = metadata.CameraModel;
                    foundImage.LensModel = metadata.LensModel;
                    foundImage.FocalLength = metadata.FocalLength;
                    foundImage.Aperture = metadata.Aperture;
                    foundImage.IsoSpeed = metadata.IsoSpeed;
                    foundImage.ExposureTime = metadata.ExposureTime;
                    foundImage.ShootingDate = metadata.ShootingDate;
                    foundImage.Modified = DateTime.UtcNow;
                    updatedCount++;
                }
                existingImages.Remove(file.FullName);
            }
            count++;
            if (count % batchSize == batchSize)
            {
                await Context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            }
        }

        library.LastScanDate = DateTime.UtcNow;
        await Context.SaveChangesAsync(cancellation).ConfigureAwait(false);

        var elapsed = DateTime.UtcNow - startTime;

        return new ScanLibraryResponse
        {
            AddedCount = addedCount,
            UpdatedCount = updatedCount,
            RemovedCount = removedCount,
            TotalCount = await Context.Image.CountAsync(i => i.LibraryId == libraryId, cancellationToken: cancellation).ConfigureAwait(false),
            ElapsedMilliseconds = elapsed.TotalMilliseconds
        };
    }
}
