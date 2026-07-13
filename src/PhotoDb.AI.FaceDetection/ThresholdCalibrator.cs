// <copyright file="ThresholdCalibrator.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Numerics.Tensors;

namespace PhotoDb.AI.FaceDetection;

public static class ThresholdCalibrator
{
    // Глобальные байесовские априорные параметры для AdaFace IR100.
    // Отражают среднее внутриклассовое сходство по репрезентативной выборке.
    private const float PriorMean = 0.62f;
    private const float PriorStd = 0.08f;
    private const float PriorWeight = 5.0f; // Эквивалент 5 "виртуальных" фотографий

    /// <summary>
    /// Рассчитывает оптимальный адаптивный порог для набора референсных эмбеддингов одного человека.
    /// </summary>
    public static float Calibrate(List<float[]> embeddings, int tier)
    {
        var k = embeddings.Count;

        // Крайний случай 1: Если фото всего одно, мы физически не можем посчитать дисперсию.
        // Возвращаем жесткий безопасный порог по умолчанию в зависимости от важности (Tier)
        if (k < 2)
        {
            return tier == 0 ? 0.52f : 0.48f;
        }

        // 1. Вычисляем все попарные косинусные сходства внутри галереи человека
        var intraSimilarities = new List<float>(k * (k - 1) / 2);
        for (var i = 0; i < k; i++)
        {
            for (var j = i + 1; j < k; j++)
            {
                var sim = TensorPrimitives.Dot(embeddings[i], embeddings[j]);
                intraSimilarities.Add(sim);
            }
        }

        var n = intraSimilarities.Count;
        var sampleMean = intraSimilarities.Average();

        // Вычисление дисперсии выборки
        var sumOfSquares = intraSimilarities.Select(val => (val - sampleMean) * (val - sampleMean)).Sum();
        var sampleVariance = sumOfSquares / n;

        // 2. Байесовское сглаживание (MAP) для защиты от малых выборок.
        // Если n мало (например, 3 фото дают 3 попарных сравнения), 
        // вес PriorWeight (5.0) будет доминировать, не давая порогу упасть или завыситься.
        var mapMean = ((PriorWeight * PriorMean) + (n * sampleMean)) / (PriorWeight + n);
        var mapVariance = ((PriorWeight * (PriorStd * PriorStd)) + (n * sampleVariance)) / (PriorWeight + n);
        var mapStd = MathF.Sqrt(mapVariance);

        // Множитель строгости. Для ключевых персон (Tier 0) мы ставим более жесткий порог (3 сигмы),
        // чтобы минимизировать ложные срабатывания. Для Tier 1 - более мягкий (2 сигмы).
        var sigmaMultiplier = tier == 0 ? 3.0f : 2.0f;

        var calculatedThreshold = mapMean - (sigmaMultiplier * mapStd);

        // 3. Ограничиваем порог жесткими санитарными границами.
        // Порог ниже 0.38 для AdaFace приведет к лавине ложноположительных связей.
        // Порог выше 0.65 сделает распознавание практически невозможным при малейшей смене света.
        return Math.Clamp(calculatedThreshold, 0.40f, 0.60f);
    }
}
