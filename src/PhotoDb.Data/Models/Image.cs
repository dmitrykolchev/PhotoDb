using System;
using System.Collections.Generic;
using Microsoft.Data.SqlTypes;

namespace PhotoDb.Data.Models;

public partial class Image
{
    public long Id { get; set; }

    public short State { get; set; }

    public string Name { get; set; } = null!;

    public int LibraryId { get; set; }

    public string FilePath { get; set; } = null!;

    public string? JsonData { get; set; }

    public int Flag { get; set; }

    public short Rating { get; set; }

    public long Size { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public string? CameraModel { get; set; }

    public string? LensModel { get; set; }

    public string? FocalLength { get; set; }

    public string? Aperture { get; set; }

    public string? IsoSpeed { get; set; }

    public string? ExposureTime { get; set; }

    public DateTime? ShootingDate { get; set; }

    public DateTime? IndexedDate { get; set; }

    public byte[] Hash { get; set; } = null!;

    public float[]? Embedding { get; set; }

    public string? Description { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public virtual ICollection<AlbumImage> AlbumImage { get; set; } = new List<AlbumImage>();

    public virtual ICollection<ImageTag> ImageTag { get; set; } = new List<ImageTag>();

    public virtual Library Library { get; set; } = null!;
}
