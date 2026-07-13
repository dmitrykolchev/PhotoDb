// <copyright file="IPhotoDbContext.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using PhotoDb.Data.Models;

namespace PhotoDb.Data;

public interface IPhotoDbContext
{
    DbSet<Album> Album { get; set; }

    DbSet<AlbumImage> AlbumImage { get; set; }

    DbSet<Image> Image { get; set; }

    DbSet<ImageTag> ImageTag { get; set; }

    DbSet<Library> Library { get; set; }

    DbSet<TagItem> TagItem { get; set; }
}
