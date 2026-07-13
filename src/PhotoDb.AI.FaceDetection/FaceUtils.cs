// <copyright file="FaceUtils.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using SkiaSharp;

namespace PhotoDb.AI.FaceDetection;

public static class FaceUtils
{
    private static readonly SKSamplingOptions DefaultSampler = new(SKCubicResampler.Mitchell);

    private static readonly float[,] ArcFaceDst = new float[,]
    {
        { 38.2946f, 51.6963f },
        { 73.5318f, 51.5014f },
        { 56.0252f, 71.7366f },
        { 41.5493f, 92.3655f },
        { 70.7299f, 92.2041f }
    };

    /// <summary>
    /// Estimates the affine transformation matrix M that maps landmarks to the ArcFace destination.
    /// </summary>
    public static SKMatrix EstimateNorm(float[,] landmark, int imageSize = 112, string mode = "arcface")
    {
        if (landmark.GetLength(0) != 5 || landmark.GetLength(1) != 2)
        {
            throw new ArgumentException("Landmarks must be 5x2");
        }

        var ratio = (imageSize % 112 == 0) ? imageSize / 112.0f : imageSize / 128.0f;
        var diffX = (imageSize % 112 == 0) ? 0f : 8.0f * ratio;

        var dst = new float[5, 2];
        for (var i = 0; i < 5; i++)
        {
            dst[i, 0] = (ArcFaceDst[i, 0] * ratio) + diffX;
            dst[i, 1] = ArcFaceDst[i, 1] * ratio;
        }

        return EstimateAffineMatrix(landmark, dst);
    }

    /// <summary>
    /// Performs a normalized crop using the estimated transformation.
    /// </summary>
    public static SKBitmap NormCrop(SKBitmap img, float[,] landmark, int imageSize = 112, string mode = "arcface")
    {
        var M = EstimateNorm(landmark, imageSize, mode);
        return WarpImage(img, M, imageSize, imageSize);
    }

    /// <summary>
    /// Resizes image to fit within SxS and pads to a square.
    /// </summary>
    public static (SKBitmap bitmap, float scale) SquareCrop(SKBitmap img, int S)
    {
        float imgW = img.Width;
        float imgH = img.Height;
        float scale;
        float targetW, targetH;

        if (imgH > imgW)
        {
            targetH = S;
            targetW = imgW / imgH * S;
            scale = S / imgH;
        }
        else
        {
            targetW = S;
            targetH = imgH / imgW * S;
            scale = S / imgW;
        }

        using var resized = ResizeBitmap(img, (int)targetW, (int)targetH);
        SKBitmap result = new(S, S);
        using (SKCanvas canvas = new(result))
        {
            canvas.Clear(SKColors.Black);
            canvas.DrawBitmap(resized, 0, 0, DefaultSampler);
        }

        return (result, scale);
    }

    /// <summary>
    /// Applies a complex similarity transform (Scale -> Translate -> Rotate -> Translate).
    /// </summary>
    public static (SKBitmap bitmap, SKMatrix) Transform(SKBitmap data, float[] center, int outputSize, float scale, float rotation)
    {
        var rotRad = (float)(rotation * Math.PI / 180.0);

        // t1: Scale
        SKMatrix m1 = new() { ScaleX = scale, ScaleY = scale };
        // t2: Translate to origin (relative to center)
        SKMatrix m2 = new() { TransX = -center[0] * scale, TransY = -center[1] * scale };
        // t3: Rotate
        var m3 = SKMatrix.CreateRotation(rotRad);
        // t4: Translate to target center
        SKMatrix m4 = new() { TransX = outputSize / 2f, TransY = outputSize / 2f };

        // Composition: M = m4 * m3 * m2 * m1
        var finalMatrix = SKMatrix.Concat(SKMatrix.Concat(SKMatrix.Concat(m4, m3), m2), m1);

        var warped = WarpImage(data, finalMatrix, outputSize, outputSize);
        return (warped, finalMatrix);
    }

    public static float[,] TransPoints(float[,] pts, float[,] M)
    {
        var rows = pts.GetLength(0);
        var cols = pts.GetLength(1);
        var result = new float[rows, cols];

        if (cols == 2)
        {
            for (var i = 0; i < rows; i++)
            {
                var x = pts[i, 0];
                var y = pts[i, 1];
                result[i, 0] = (M[0, 0] * x) + (M[0, 1] * y) + M[0, 2];
                result[i, 1] = (M[1, 0] * x) + (M[1, 1] * y) + M[1, 2];
            }
        }
        else if (cols == 3)
        {
            var scale = (float)Math.Sqrt((M[0, 0] * M[0, 0]) + (M[0, 1] * M[0, 1]));
            for (var i = 0; i < rows; i++)
            {
                var x = pts[i, 0];
                var y = pts[i, 1];
                var z = pts[i, 2];
                result[i, 0] = (M[0, 0] * x) + (M[0, 1] * y) + M[0, 2];
                result[i, 1] = (M[1, 0] * x) + (M[1, 1] * y) + M[1, 2];
                result[i, 2] = z * scale;
            }
        }
        return result;
    }

    #region Helpers

    private static SKMatrix EstimateAffineMatrix(float[,] src, float[,] dst)
    {
        // Solve Ax = B using Least Squares
        // A is 10x6, B is 10x1
        var A = new double[10, 6];
        var B = new double[10];

        for (var i = 0; i < 5; i++)
        {
            A[i * 2, 0] = src[i, 0];
            A[i * 2, 1] = src[i, 1];
            A[i * 2, 2] = 1.0;
            A[i * 2, 3] = 0;
            A[i * 2, 4] = 0;
            A[i * 2, 5] = 0;

            A[(i * 2) + 1, 0] = src[i, 0];
            A[(i * 2) + 1, 1] = src[i, 1];
            A[(i * 2) + 1, 2] = 0;
            A[(i * 2) + 1, 3] = 0;
            A[(i * 2) + 1, 4] = 0;
            A[(i * 2) + 1, 5] = 1.0;

            B[i * 2] = dst[i, 0];
            B[(i * 2) + 1] = dst[i, 1];
        }

        // Normal Equation: (A^T * A) * x = A^T * B
        var AtA = new double[6, 6];
        var AtB = new double[6];

        for (var i = 0; i < 6; i++)
        {
            for (var j = 0; j < 6; j++)
            {
                for (var k = 0; k < 10; k++)
                {
                    AtA[i, j] += A[k, i] * A[k, j];
                }
            }
            for (var k = 0; k < 10; k++)
            {
                AtB[i] += A[k, i] * B[k];
            }
        }

        // Simple Gaussian Elimination to solve AtA * x = AtB
        for (var i = 0; i < 6; i++)
        {
            for (var k = i + 1; k < 6; k++)
            {
                var factor = AtA[k, i] / AtA[i, i];
                for (var j = i; j < 6; j++)
                {
                    AtA[k, j] -= factor * AtA[i, j];
                }
                AtB[k] -= factor * AtB[i];
            }
        }

        var x = new float[9];
        for (var i = 5; i >= 0; i--)
        {
            double sum = 0;
            for (var j = i + 1; j < 6; j++)
            {
                sum += AtA[i, j] * x[j];
            }
            x[i] = (float)((AtB[i] - sum) / AtA[i, i]);
        }
        return new SKMatrix(x);
    }

    private static SKBitmap WarpImage(SKBitmap source, SKMatrix matrix, int width, int height)
    {
        // 1. Create a destination bitmap to hold the warped result
        // Note: You may need larger dimensions depending on how far your warp stretches
        SKBitmap outputBitmap = new(width, height);

        // 2. Wrap a canvas around the output bitmap
        using (SKCanvas canvas = new(outputBitmap))
        {
            // Clear background with transparency or a solid color
            canvas.Clear(SKColors.Transparent);

            // Enable high-quality anti-aliasing to smooth out warped edges
            using SKPaint paint = new()
            {
                IsAntialias = true
            };
            // 3. Apply the warping transformation matrix to the canvas
            canvas.SetMatrix(matrix);

            // 4. Draw the source bitmap starting at the origin (0,0)
            // The canvas uses the set matrix to skew/project the image dynamically
            canvas.DrawBitmap(source, 0, 0, DefaultSampler, paint);
        }

        return outputBitmap;
    }

    private static SKBitmap ResizeBitmap(SKBitmap source, int width, int height)
    {
        SKMatrix matrix = new()
        {
            ScaleX = (float)width / source.Width,
            ScaleY = (float)height / source.Height
        };
        return WarpImage(source, matrix, width, height);
    }

    #endregion
}
