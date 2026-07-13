// <copyright file="ImageListItem.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Text.Json.Serialization;
using Xobex.CryptoId.Json.Serialization;

namespace PhotoDb.Data.Models.Images;

public class ImageListItem
{
    [JsonConverter(typeof(Int64JsonConverter))]
    public long Id { get; set; }
    [JsonConverter(typeof(Int32JsonConverter))]
    public int LibraryId { get; set; }
    public string Name { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime? IndexedDate { get; set; }
    public short Rating { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ImageFlag Flag { get; set; }
    public byte[]? Hash { get; set; }
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
    public string? Description { get; set; }
    public string[] Tags { get; set; } = null!;
}
