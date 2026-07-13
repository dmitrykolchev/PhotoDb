// <copyright file="BitmapProcessor.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using SkiaSharp;

namespace PhotoDb.Imaging;

public static class BitmapProcessor
{
    private static readonly SKSamplingOptions DefaultSampler = new(SKCubicResampler.Mitchell);
    /// <summary>
    /// Creates a thumbnail from the source image, scaling so that the longest side
    /// does not exceed <paramref name="maxSize"/> pixels. EXIF orientation is always
    /// corrected, regardless of whether the image is actually scaled.
    /// </summary>
    public static SKBitmap NormalizeOrientation(string inputPath)
    {
        if (string.IsNullOrWhiteSpace(inputPath))
        {
            throw new ArgumentException("Input path must not be empty.", nameof(inputPath));
        }

        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException("Source file not found.", inputPath);
        }

        using var stream = File.OpenRead(inputPath);
        return NormalizeOrientation(stream);
    }

    public static SKBitmap NormalizeOrientation(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        using var codec = SKCodec.Create(stream);

        var origin = codec.EncodedOrigin;

        using var originalBitmap = SKBitmap.Decode(codec);

        var orientedBitmap = NormalizeOrientation(originalBitmap, origin);

        return orientedBitmap;
    }

    /// <summary>
    /// Returns a new <see cref="SKBitmap"/> with the EXIF orientation applied.
    /// The caller is always responsible for disposing the returned instance.
    /// The input <paramref name="bitmap"/> is never disposed here.
    /// </summary>
    public static SKBitmap NormalizeOrientation(SKBitmap bitmap, SKEncodedOrigin origin)
    {
        // Always return a new object so the caller has a clear, uniform ownership contract.
        if (origin is SKEncodedOrigin.TopLeft or SKEncodedOrigin.Default)
        {
            // No transformation needed — but we still return a copy to keep ownership semantics uniform.
            return bitmap.Copy();
        }

        // Map EXIF origin to (rotation-degrees, horizontal-mirror).
        // Mirror is applied AFTER rotation (i.e., in the rotated coordinate space).
        //
        // EXIF origin → physical transformation table:
        //   TopRight    →   flip X
        //   BottomRight →   rotate 180
        //   BottomLeft  →   rotate 180 + flip X  (= flip Y)
        //   LeftTop     →   rotate 90 CW + flip X  (transpose)
        //   RightTop    →   rotate 90 CW
        //   RightBottom →   rotate 270 CW + flip X  (transverse)
        //   LeftBottom  →   rotate 270 CW
        (var rotation, var mirrorX) = origin switch
        {
            SKEncodedOrigin.TopRight => (0, true),
            SKEncodedOrigin.BottomRight => (180, false),
            SKEncodedOrigin.BottomLeft => (180, true),
            SKEncodedOrigin.LeftTop => (90, true),
            SKEncodedOrigin.RightTop => (90, false),
            SKEncodedOrigin.RightBottom => (270, true),
            SKEncodedOrigin.LeftBottom => (270, false),
            _ => (0, false)
        };

        var isTransposed = rotation is 90 or 270;
        var newWidth = isTransposed ? bitmap.Height : bitmap.Width;
        var newHeight = isTransposed ? bitmap.Width : bitmap.Height;

        var result = new SKBitmap(newWidth, newHeight, bitmap.ColorType, bitmap.AlphaType);

        using var canvas = new SKCanvas(result);
        canvas.Clear(SKColors.Transparent);

        // Build the transform around the centre of the output canvas.
        canvas.Translate(newWidth / 2f, newHeight / 2f);
        canvas.RotateDegrees(rotation);
        if (mirrorX)
        {
            canvas.Scale(-1f, 1f);
        }

        // Draw the source centred on the (transformed) origin.
        canvas.DrawBitmap(bitmap, -bitmap.Width / 2f, -bitmap.Height / 2f, DefaultSampler);

        return result;
    }
}
