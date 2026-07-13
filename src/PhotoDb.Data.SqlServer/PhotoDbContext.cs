// <copyright file="PhotoDbContext.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using PhotoDb.Data.Models;

namespace PhotoDb.Data.SqlServer;

public partial class PhotoDbContext : PhotoDbContextBase
{
    public PhotoDbContext(DbContextOptions<PhotoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("pk_album")
                .IsClustered(false);

            entity.ToTable("album");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[album_sequence])")
                .HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Modified).HasColumnName("modified");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<AlbumImage>(entity =>
        {
            entity.HasKey(e => new { e.AlbumId, e.ImageId })
                .HasName("pk_album_image")
                .IsClustered(false);

            entity.ToTable("album_image");

            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Placeholder).HasColumnName("placeholder");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumImage)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_image_album");

            entity.HasOne(d => d.Image).WithMany(p => p.AlbumImage)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_album_image_image");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_image");

            entity.ToTable("image");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[image_sequence])")
                .HasColumnName("id");
            entity.Property(e => e.Aperture)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("aperture");
            entity.Property(e => e.CameraModel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("camera_model");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Embedding)
                .HasMaxLength(256)
                .HasColumnName("embedding");
            entity.Property(e => e.ExposureTime)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("exposure_time");
            entity.Property(e => e.FilePath)
                .HasMaxLength(1024)
                .HasColumnName("file_path");
            entity.Property(e => e.Flag).HasColumnName("flag");
            entity.Property(e => e.FocalLength)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("focal_length");
            entity.Property(e => e.Hash)
                .HasMaxLength(128)
                .HasColumnName("hash");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.IndexedDate).HasColumnName("indexed_date");
            entity.Property(e => e.IsoSpeed)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("iso_speed");
            entity.Property(e => e.JsonData)
                .HasColumnType("json")
                .HasColumnName("json_data");
            entity.Property(e => e.LensModel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lens_model");
            entity.Property(e => e.LibraryId).HasColumnName("library_id");
            entity.Property(e => e.Modified).HasColumnName("modified");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ShootingDate).HasColumnName("shooting_date");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.Library).WithMany(p => p.Image)
                .HasForeignKey(d => d.LibraryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_image_library");
        });

        modelBuilder.Entity<ImageTag>(entity =>
        {
            entity.HasKey(e => new { e.ImageId, e.TagId })
                .HasName("pk_image_tag")
                .IsClustered(false);

            entity.ToTable("image_tag");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Placeholder).HasColumnName("placeholder");

            entity.HasOne(d => d.Image).WithMany(p => p.ImageTag)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_image_tag_image");

            entity.HasOne(d => d.Tag).WithMany(p => p.ImageTag)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_image_tag_tag");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_library");

            entity.ToTable("library");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[library_sequence])")
                .HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.LastScanDate).HasColumnName("last_scan_date");
            entity.Property(e => e.Modified).HasColumnName("modified");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.Property(e => e.Path)
                .HasMaxLength(1024)
                .HasColumnName("path");
            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<TagItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_tag_item");

            entity.ToTable("tag_item");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[tag_sequence])")
                .HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
            entity.Property(e => e.State).HasColumnName("state");
        });
        modelBuilder.HasSequence<int>("album_sequence");
        modelBuilder.HasSequence("image_sequence");
        modelBuilder.HasSequence<int>("library_sequence");
        modelBuilder.HasSequence<int>("tag_sequence");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
