// <copyright file="ImageDataService.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using PhotoDb.Data.Models;
using PhotoDb.Data.Models.Images;
using Xobex.Cryptography.Abstractions;
using Xobex.CryptoId;

namespace PhotoDb.Data.Services;

public sealed class ImageDataService : DataServiceBase
{
    public ImageDataService(PhotoDbContextBase context) : base(context)
    {
    }

    public async Task<GetImagesResponse> GetImagesAsync(GetImagesRequest request)
    {
        var query = Context.Image.AsNoTracking();

        if (!request.LibraryId.IsEmpty)
        {
            query = from item in query where item.LibraryId == (int)request.LibraryId select item;
        }

        if (!request.AlbumId.IsEmpty)
        {
            var imageIdsInAlbum = from item in Context.AlbumImage
                                  where item.AlbumId == (int)request.AlbumId
                                  select item.ImageId;
            query = from item in query
                    where imageIdsInAlbum.Contains(item.Id)
                    select item;
        }

        if (request.MinRating.HasValue && request.MinRating > 0)
        {
            query = from img in query where img.Rating >= request.MinRating.Value select img;
        }

        if (request.Flags != null && request.Flags.Length > 0)
        {
            var parsedFlags = request.Flags
                .SelectMany(f => f.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                .Select(t => (int)Enum.Parse<ImageFlag>(t))
                .ToList();
            if (parsedFlags.Any())
            {
                query = from item in query where parsedFlags.Contains(item.Flag) select item;
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Tags))
        {
            var searchTags = request.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(t => t.ToLower())
                .ToList();

            if (searchTags.Any())
            {
                var matchedImageIds = await Context.ImageTag
                    .Join(Context.TagItem, it => it.TagId, t => t.Id, (it, t) => new { it.ImageId, t.Name })
                    .Where(x => searchTags.Contains(x.Name.ToLower()))
                    .GroupBy(x => x.ImageId)
                    .Where(g => g.Count() == searchTags.Count)
                    .Select(g => g.Key)
                    .ToListAsync();

                query = query.Where(img => matchedImageIds.Contains(img.Id));
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.ToLower().Trim();
            query = from img in query
                    where img.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                          img.FilePath.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                          (img.Description != null && img.Description.Contains(term, StringComparison.OrdinalIgnoreCase))
                    select img;
        }

        var sortOrder = request.SortOrder.ToLower();
        // High Performance Memory Sort
        query = request.SortBy.ToLower() switch
        {
            "filename" => sortOrder == "asc" ? query.OrderBy(i => i.Name) : query.OrderByDescending(i => i.Name),
            "rating" => sortOrder == "asc" ? query.OrderBy(i => i.Rating) : query.OrderByDescending(i => i.Rating),
            "size" => sortOrder == "asc" ? query.OrderBy(i => i.Size) : query.OrderByDescending(i => i.Size),
            _ => sortOrder == "asc" ? query.OrderBy(i => i.Created) : query.OrderByDescending(i => i.Created)
        };

        var totalCount = await query.CountAsync();
        var imagesList = await query.Skip((request.Page - 1) * request.Limit).Take(request.Limit).ToListAsync();

        // Populate joined list tags
        var imageIds = imagesList.Select(img => img.Id).ToList();
        var imgTagsMapping = await Context.ImageTag
            .Where(it => imageIds.Contains(it.ImageId))
            .Join(Context.TagItem, it => it.TagId, t => t.Id, (it, t) => new { it.ImageId, TagName = t.Name })
            .GroupBy(x => x.ImageId)
            .ToDictionaryAsync(g => g.Key, g => g.Select(x => x.TagName).ToArray());

        var finalImages = imagesList.Select(img => new ImageListItem
        {
            Id = img.Id,
            LibraryId = img.LibraryId,
            Name = img.Name,
            FilePath = img.FilePath,
            Created = img.Created,
            IndexedDate = img.IndexedDate,
            Rating = img.Rating,
            Flag = (ImageFlag)img.Flag,
            Hash = img.Hash,
            Size = img.Size,
            Width = img.Width,
            Height = img.Height,
            CameraModel = img.CameraModel,
            LensModel = img.LensModel,
            FocalLength = img.FocalLength,
            Aperture = img.Aperture,
            IsoSpeed = img.IsoSpeed,
            ExposureTime = img.ExposureTime,
            ShootingDate = img.ShootingDate,
            Description = img.Description,
            Tags = imgTagsMapping.TryGetValue(img.Id, out var value) ? value : Array.Empty<string>()
        });

        return new GetImagesResponse()
        {
            Images = finalImages,
            Count = totalCount,
            Page = request.Page,
            Limit = request.Limit,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.Limit)
        };
    }

    public async Task<ImageViewItem> UpdateImageAsync(long imageId, UpdateImageRequest req)
    {
        var img = await Context.Image.SingleAsync(t => t.Id == imageId);
        if (req.Rating.HasValue)
        {
            img.Rating = (short)Math.Clamp(req.Rating.Value, 0, 5);
        }

        if (req.Flag != null)
        {
            img.Flag = (int)req.Flag.Value;
        }

        if (!string.IsNullOrEmpty(req.Description))
        {
            img.Description = req.Description;
        }

        await Context.SaveChangesAsync();

        // Resolve modern state tags mapping
        var tags = await Context.ImageTag
            .Where(it => it.ImageId == imageId)
            .Join(Context.TagItem, it => it.TagId, t => t.Id, (it, t) => t.Name)
            .ToListAsync();

        var imgAlbumIds = await Context.AlbumImage
            .Where(ai => ai.ImageId == imageId)
            .Select(ai => ai.AlbumId)
            .ToListAsync();

        var imgAlbums = await Context.Album
            .Where(a => imgAlbumIds.Contains(a.Id))
            .ToListAsync();

        return new ImageViewItem()
        {
            Id = (Int64CryptoId)img.Id,
            LibraryId = (Int32CryptoId)img.LibraryId,
            Name = img.Name,
            FilePath = img.FilePath,
            Created = img.Created,
            IndexedDate = img.IndexedDate,
            Rating = img.Rating,
            Flag = (ImageFlag)img.Flag,
            Hash = img.Hash,
            Size = img.Size,
            Width = img.Width,
            Height = img.Height,
            CameraModel = img.CameraModel,
            LensModel = img.LensModel,
            FocalLength = img.FocalLength,
            Aperture = img.Aperture,
            IsoSpeed = img.IsoSpeed,
            ExposureTime = img.ExposureTime,
            ShootingDate = img.ShootingDate,
            Description = img.Description,
            Tags = tags,
            Albums = imgAlbums
        };
    }

    public async Task<AddTagResponse> AddTagToImageAsync(long id, AddTagRequest req)
    {
        ArgumentNullException.ThrowIfNull(req);
        ArgumentNullException.ThrowIfNullOrEmpty(req.TagName);

        var img = await Context.Image.SingleAsync(t => t.Id == id);
        var sanitizedTagName = req.TagName.Trim();

        // Check if tag entity already exists
        var tag = await Context.TagItem.FirstOrDefaultAsync(t => t.Name == sanitizedTagName);
        if (tag == null)
        {
            var matchingTags = await Context.TagItem
                .Where(t => t.Name.Length == sanitizedTagName.Length)
                .ToListAsync();
            tag = matchingTags.FirstOrDefault(t => string.Equals(t.Name, sanitizedTagName, StringComparison.OrdinalIgnoreCase));
        }

        if (tag == null)
        {
            tag = new TagItem
            {
                Name = sanitizedTagName
            };
            Context.TagItem.Add(tag);
            await Context.SaveChangesAsync();
        }

        // Check relation mapping existence
        var relation = await Context.ImageTag.FirstOrDefaultAsync(t => t.ImageId == id && t.TagId == tag.Id);
        if (relation == null)
        {
            var newRelation = new ImageTag
            {
                ImageId = id,
                TagId = tag.Id
            };
            Context.ImageTag.Add(newRelation);
            await Context.SaveChangesAsync();
        }

        // Return list of tags for image
        var tags = await Context.ImageTag
            .Where(it => it.ImageId == id)
            .Join(Context.TagItem, it => it.TagId, t => t.Id, (it, t) => t.Name)
            .ToListAsync();

        return new AddTagResponse() { Tags = tags };
    }

    public async Task<RemoveTagResponse> RemoveTagFromImageAsync(long id, string tagName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(tagName);
        var img = await Context.Image.SingleAsync(t => t.Id == id);

        var decodedTag = Uri.UnescapeDataString(tagName).Trim();
        var tag = await Context.TagItem.FirstOrDefaultAsync(t => t.Name.ToLower() == decodedTag.ToLower());
        if (tag != null)
        {
            var relation = await Context.ImageTag.SingleOrDefaultAsync(t => t.ImageId == id && t.TagId == tag.Id);
            if (relation != null)
            {
                Context.ImageTag.Remove(relation);
                await Context.SaveChangesAsync();
            }

            // Clean orphaned tags (tags with zero files referenced)
            var referenceCount = await Context.ImageTag.CountAsync(it => it.TagId == tag.Id);
            if (referenceCount == 0)
            {
                Context.TagItem.Remove(tag);
                await Context.SaveChangesAsync();
            }
        }

        // Return remaining assigned tags list
        var tags = await Context.ImageTag
            .Where(it => it.ImageId == id)
            .Join(Context.TagItem, it => it.TagId, t => t.Id, (it, t) => t.Name)
            .ToListAsync();

        return new RemoveTagResponse() { Tags = tags };
    }

    public async Task<ImageViewItem> GetImageAsync(long id)
    {
        var img = await Context.Image.SingleAsync(t => t.Id == id);
        var tags = await Context.ImageTag
            .Where(it => it.ImageId == id)
            .Join(Context.TagItem, it => it.TagId, t => t.Id, (it, t) => t.Name)
            .ToListAsync();

        var imgAlbumIds = await Context.AlbumImage
            .Where(ai => ai.ImageId == id)
            .Select(ai => ai.AlbumId)
            .ToListAsync();
        var imgAlbums = await Context.Album
            .Where(a => imgAlbumIds.Contains(a.Id))
            .ToListAsync();

        return new ImageViewItem
        {
            Id = (Int64CryptoId)img.Id,
            LibraryId = (Int32CryptoId)img.LibraryId,
            Name = img.Name,
            FilePath = img.FilePath,
            Created = img.Created,
            IndexedDate = img.IndexedDate,
            Rating = img.Rating,
            Flag = (ImageFlag)img.Flag,
            Hash = img.Hash,
            Size = img.Size,
            Width = img.Width,
            Height = img.Height,
            CameraModel = img.CameraModel,
            LensModel = img.LensModel,
            FocalLength = img.FocalLength,
            Aperture = img.Aperture,
            IsoSpeed = img.IsoSpeed,
            ExposureTime = img.ExposureTime,
            ShootingDate = img.ShootingDate,
            Description = img.Description,
            Tags = tags,
            Albums = imgAlbums
        };
    }
}
