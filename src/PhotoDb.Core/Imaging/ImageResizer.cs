// <copyright file="ImageResizer.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using SkiaSharp;

namespace PhotoDb.Imaging;

public static class ImageResizer
{
    private static readonly SKSamplingOptions _defaultSampler = new(SKCubicResampler.Mitchell);
    /// <summary>
    /// Creates a thumbnail from the source image, scaling so that the longest side
    /// does not exceed <paramref name="maxSize"/> pixels. EXIF orientation is always
    /// corrected, regardless of whether the image is actually scaled.
    /// </summary>
    public static void CreateThumbnail(string inputPath, string outputPath, int maxSize)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(inputPath);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(outputPath);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxSize, 0);

        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException("Source file not found.", inputPath);
        }

        // 1. Open codec to read metadata (including EXIF orientation).
        using var stream = File.OpenRead(inputPath);
        using var codec = SKCodec.Create(stream);

        var origin = codec.EncodedOrigin;

        // 2. Decode the raw bitmap (SKBitmap.Decode does NOT auto-apply orientation).
        using var originalBitmap = SKBitmap.Decode(codec);

        // 3. Correct orientation unconditionally (fix: was skipped on the copy-path).
        //    CorrectOrientation always returns a NEW bitmap — caller owns it.
        using var orientedBitmap = BitmapProcessor.NormalizeOrientation(originalBitmap, origin);

        var srcWidth = orientedBitmap.Width;
        var srcHeight = orientedBitmap.Height;

        var ratio = Math.Min((double)maxSize / srcWidth, (double)maxSize / srcHeight);

        if (ratio >= 1.0)
        {
            // Image already fits within maxSize — save the orientation-corrected version
            // without re-encoding if source format matches output format; otherwise encode.
            SaveBitmap(orientedBitmap, outputPath);
            return;
        }

        var newWidth = (int)Math.Round((srcWidth * ratio) + 0.5);
        var newHeight = (int)Math.Round((srcHeight * ratio) + 0.5);

        // 4. Resize with a high-quality resampler.
        //    SKFilterQuality is obsolete in SkiaSharp >= 2.88; use SKSamplingOptions instead.
        //    Mitchell cubic is a good general-purpose downsampling filter.
        using var scaledBitmap = orientedBitmap.Resize(new SKImageInfo(newWidth, newHeight), _defaultSampler);

        SaveBitmap(scaledBitmap, outputPath);
    }

    /// <summary>
    /// Encodes <paramref name="bitmap"/> and writes it to <paramref name="outputPath"/>,
    /// always truncating any pre-existing file (fixes File.OpenWrite not truncating).
    /// </summary>
    private static void SaveBitmap(SKBitmap bitmap, string outputPath)
    {
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(GetEncodedFormat(outputPath), quality: 85);

        // FileMode.Create truncates an existing file, unlike File.OpenWrite.
        using var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);
        data.SaveTo(fs);
    }

    private static SKEncodedImageFormat GetEncodedFormat(string filePath)
    {
        return Path.GetExtension(filePath).ToLowerInvariant() switch
        {
            ".png" => SKEncodedImageFormat.Png,
            ".gif" => SKEncodedImageFormat.Gif,
            ".bmp" => SKEncodedImageFormat.Bmp,
            _ => SKEncodedImageFormat.Jpeg
        };
    }
}
