// <copyright file="ImageMetadataCore.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.Imaging;

public class ImageMetadataCore
{
    public int Width { get; init; }
    public int Height { get; init; }
    public string? CameraMake { get; init; }
    public string? CameraModel { get; init; }
    public string? LensModel { get; init; }
    public string? FocalLength { get; init; }
    public string? Aperture { get; init; }
    public string? IsoSpeed { get; init; }
    public string? ExposureTime { get; init; }
    public DateTime? ShootingDate { get; init; }
}

