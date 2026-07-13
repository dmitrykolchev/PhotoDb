// <copyright file="AlbumDataService.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using PhotoDb.Data.Models;
using PhotoDb.Data.Models.Albums;
using Xobex.Cryptography.Abstractions;
using Xobex.CryptoId;

namespace PhotoDb.Data.Services;

public class AlbumDataService : DataServiceBase
{
    public AlbumDataService(PhotoDbContextBase context) : base(context)
    {
    }

    public async Task<List<AlbumListItem>> GetAlbumsAsync()
    {
        var albums = await Context.Album.AsNoTracking().ToListAsync();
        var imageCounts = await Context.AlbumImage
            .GroupBy(ai => ai.AlbumId)
            .Select(g => new { AlbumId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.AlbumId, x => x.Count);

        var result = albums.Select(alb => new AlbumListItem()
        {
            Id = (Int32CryptoId)alb.Id,
            Name = alb.Name,
            Created = alb.Created,
            ImageCount = imageCounts.TryGetValue(alb.Id, out var count) ? count : 0
        }).ToList();

        return result;
    }

    public async Task<AlbumListItem> GetAlbumAsync(int id)
    {
        return await (from item in Context.Album.AsNoTracking()
                      where item.Id == id
                      select new AlbumListItem()
                      {
                          Id = (Int32CryptoId)item.Id,
                          Name = item.Name,
                          Created = item.Created,
                          ImageCount = item.AlbumImage.Count
                      }).SingleAsync();
    }

    public async Task<AlbumListItem> CreateAlbumAsync(CreateAlbumRequest req)
    {
        ArgumentNullException.ThrowIfNull(req);
        ArgumentNullException.ThrowIfNullOrEmpty(req.Name);

        var newAlbum = new Album
        {
            Name = req.Name.Trim(),
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
        };

        Context.Album.Add(newAlbum);
        await Context.SaveChangesAsync();
        return await GetAlbumAsync(newAlbum.Id);
    }

    public async Task<AlbumListItem> RenameAlbumAsync(int id, CreateAlbumRequest req)
    {
        ArgumentNullException.ThrowIfNull(req);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(req.Name);

        var album = await Context.Album.SingleAsync(t => t.Id == id);
        album.Name = req.Name.Trim();
        await Context.SaveChangesAsync();
        return await GetAlbumAsync(id);
    }

    public async Task DeleteAlbumAsync(int id)
    {
        var album = await Context.Album.SingleAsync(t => t.Id == id);

        using var tx = await Context.Database.BeginTransactionAsync();
        try
        {
            var relations = await Context.AlbumImage.Where(ai => ai.AlbumId == id).ToListAsync();
            Context.AlbumImage.RemoveRange(relations);
            Context.Album.Remove(album);
            await Context.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch (Exception)
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public async Task RemoveImageFromAlbumAsync(int id, long imageId)
    {
        await Context.AlbumImage.Where(t => t.AlbumId == id && t.ImageId == imageId).ExecuteDeleteAsync();
    }

    public async Task AddImageToAlbumAsync(int id, long imageId)
    {
        var albumExists = await Context.Album.AnyAsync(a => a.Id == id);
        if (!albumExists)
        {
            throw new InvalidOperationException("Album not found");
        }
        var exists = await Context.AlbumImage.AnyAsync(ai => ai.AlbumId == id && ai.ImageId == imageId);
        if (!exists)
        {
            var relation = new AlbumImage
            {
                AlbumId = id,
                ImageId = imageId
            };
            Context.AlbumImage.Add(relation);
            await Context.SaveChangesAsync();
        }
    }

    public async Task<int> AddImagesToAlbumAsync(int id, AddImagesToAlbumBatchRequest imageIds)
    {
        var albumExists = await Context.Album.AnyAsync(a => a.Id == id);
        if (!albumExists)
        {
            throw new InvalidOperationException("Album not found");
        }
        var addedCount = 0;
        foreach (var imageId in imageIds.ImageIds)
        {
            var exists = await Context.AlbumImage.AnyAsync(ai => ai.AlbumId == id && ai.ImageId == (long)imageId);
            if (!exists)
            {
                var relation = new AlbumImage
                {
                    AlbumId = id,
                    ImageId = (long)imageId
                };
                Context.AlbumImage.Add(relation);
                addedCount++;
            }
        }

        if (addedCount > 0)
        {
            await Context.SaveChangesAsync();
        }
        return addedCount;
    }
}
