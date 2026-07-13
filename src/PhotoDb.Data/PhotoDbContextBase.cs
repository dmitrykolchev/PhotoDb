// <copyright file="PhotoDbContextBase.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using PhotoDb.Data.Models;

namespace PhotoDb.Data;

public abstract class PhotoDbContextBase : DbContext, IPhotoDbContext
{
    protected PhotoDbContextBase(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Album> Album { get; set; }

    public virtual DbSet<AlbumImage> AlbumImage { get; set; }

    public virtual DbSet<Image> Image { get; set; }

    public virtual DbSet<ImageTag> ImageTag { get; set; }

    public virtual DbSet<Library> Library { get; set; }

    public virtual DbSet<TagItem> TagItem { get; set; }
}
