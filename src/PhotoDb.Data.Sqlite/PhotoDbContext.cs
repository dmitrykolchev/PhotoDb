// <copyright file="PhotoDbContext.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using PhotoDb.Data.Models;

namespace PhotoDb.Data.Sqlite;

public class PhotoDbContext : PhotoDbContextBase
{
    public PhotoDbContext(DbContextOptions<PhotoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("album");
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AlbumImage>(entity =>
        {
            entity.HasKey(e => new { e.AlbumId, e.ImageId });
            entity.ToTable("album_image");
            entity.HasOne(d => d.Album).WithMany(p => p.AlbumImage)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_image_album");

            entity.HasOne(d => d.Image).WithMany(p => p.AlbumImage)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_image_image");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("library");
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("image");
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Hash)
                .HasColumnType("byte[128]");
            entity.Property(e => e.Embedding)
                .HasColumnType("float[256]");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.HasIndex(e => e.Rating);
        });

        modelBuilder.Entity<TagItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("tag_item");
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ImageTag>(entity =>
        {
            entity.HasKey(e => new { e.ImageId, e.TagId });

            entity.ToTable("image_tag");

            entity.HasOne(d => d.Image).WithMany(p => p.ImageTag)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_image_tag_image");

            entity.HasOne(d => d.Tag).WithMany(p => p.ImageTag)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_image_tag_tag");
        });
    }
}
