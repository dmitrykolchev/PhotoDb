// <copyright file="ImageViewItem.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>


using System.Text.Json.Serialization;
using Xobex.CryptoId;

namespace PhotoDb.Data.Models.Images;

public class ImageViewItem
{
    public Int64CryptoId Id { get; set; }
    public string Name { get; set; } = null!;
    public Int32CryptoId LibraryId { get; set; }
    public string FilePath { get; set; } = null!;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ImageFlag Flag { get; set; }
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
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public IEnumerable<string> Tags { get; set; } = [];
    public IEnumerable<Album>? Albums { get; set; } = [];
}
