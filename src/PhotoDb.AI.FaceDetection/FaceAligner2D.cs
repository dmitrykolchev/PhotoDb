// <copyright file="FaceAligner2D.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using SkiaSharp;

namespace PhotoDb.AI.FaceDetection;

public static class FaceAligner2D
{
    private static readonly SKSamplingOptions DefaultSampler = new(SKCubicResampler.Mitchell);

    // Канонические точки InsightFace/AdaFace для тензора 112x112
    private static readonly Landmark[] CanonicalPoints =
    [
        new Landmark(38.2946f, 51.6963f), // Левый глаз
        new Landmark(73.5318f, 51.5014f), // Правый глаз
        new Landmark(56.0252f, 71.7366f), // Нос
        new Landmark(41.5493f, 92.3655f), // Левый рот
        new Landmark(70.7299f, 92.2041f)  // Правый рот
    ];

    /// <summary>
    /// Вырезает и выравнивает лицо к каноническому размеру 112x112 для эмбеддинга.
    /// Вызывающий код ОБЯЗАН вызвать .Dispose() у возвращаемого SKBitmap.
    /// </summary>
    public static SKBitmap AlignFace(SKBitmap originalImage, Landmark[] detectedLandmarks)
    {
        if (detectedLandmarks.Length != 5)
        {
            throw new ArgumentException("Требуется ровно 5 landmarks.");
        }

        // 1. Вычисляем матрицу преобразования подобия
        var transformMatrix = CalculateSimilarityTransform(detectedLandmarks, CanonicalPoints);

        // 2. Создаем пустой холст 112x112
        var alignedFace = new SKBitmap(112, 112, originalImage.ColorType, originalImage.AlphaType);

        using var canvas = new SKCanvas(alignedFace);
        // Очищаем черным на случай, если лицо выходит за границы кадра и будут пустые зоны
        canvas.Clear(SKColors.Black);

        // 3. Применяем трансформацию.
        // SKCanvas.SetMatrix задает маппинг из локальных координат (исходного фото) 
        // в координаты холста (112x112).
        canvas.SetMatrix(transformMatrix);

        // 4. Отрисовка исходного изображения (аппаратно ускорено через Skia)
        canvas.DrawBitmap(originalImage, 0, 0, DefaultSampler);

        return alignedFace;
    }

    /// <summary>
    /// Аналитическое вычисление 2D Procrustes (Similarity Transform) без OpenCV.
    /// </summary>
    private static SKMatrix CalculateSimilarityTransform(Landmark[] src, Landmark[] dst)
    {
        var n = src.Length;

        // 1. Центроиды
        float meanSrcX = 0, meanSrcY = 0;
        float meanDstX = 0, meanDstY = 0;

        for (var i = 0; i < n; i++)
        {
            meanSrcX += src[i].X;
            meanSrcY += src[i].Y;
            meanDstX += dst[i].X;
            meanDstY += dst[i].Y;
        }

        meanSrcX /= n; meanSrcY /= n;
        meanDstX /= n; meanDstY /= n;

        // 2. Вычисление ковариации и дисперсии
        float srcVar = 0;
        float c1 = 0;
        float c2 = 0;

        for (var i = 0; i < n; i++)
        {
            var sx = src[i].X - meanSrcX;
            var sy = src[i].Y - meanSrcY;
            var dx = dst[i].X - meanDstX;
            var dy = dst[i].Y - meanDstY;

            srcVar += (sx * sx) + (sy * sy);
            c1 += (sx * dx) + (sy * dy);
            c2 += (sy * dx) - (sx * dy);
        }

        // Предотвращение деления на ноль при вырожденных landmarks
        if (srcVar < 1e-6f)
        {
            return SKMatrix.Identity;
        }

        // 3. Компоненты масштабированной матрицы вращения (s * R)
        var a = c1 / srcVar;
        var b = c2 / srcVar;

        // 4. Вектор трансляции t = meanDst - (s * R) * meanSrc
        var tx = meanDstX - ((a * meanSrcX) + (b * meanSrcY));
        var ty = meanDstY - ((-b * meanSrcX) + (a * meanSrcY));

        // 5. Конструирование SKMatrix.
        // Матрица аффинного преобразования SkiaSharp имеет вид:
        // [ ScaleX, SkewX,  TransX ]
        // [ SkewY,  ScaleY, TransY ]
        // [ Persp0, Persp1, Persp2 ]
        return new SKMatrix(
            scaleX: a, skewX: b, transX: tx,
            skewY: -b, scaleY: a, transY: ty,
            persp0: 0, persp1: 0, persp2: 1
        );
    }
}
