// <copyright file="FaceEmbedding.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.AI.FaceDetection;

public readonly struct FaceEmbedding
{
    private readonly float[] _vector;

    /// <summary>
    /// L2-нормализованный вектор (512-dim). Лежит на единичной гиперсфере.
    /// Готов для косинусного матчинга (Dot Product).
    /// </summary>
    public ReadOnlySpan<float> Vector => _vector;

    /// <summary>
    /// Длина сырого вектора до нормализации.
    /// Для AdaFace служит абсолютной метрикой качества лица (Image Quality).
    /// </summary>
    public float QualityNorm { get; }

    public FaceEmbedding(float[] vector, float qualityNorm)
    {
        if (vector.Length != 512)
        {
            throw new ArgumentException("Эмбеддинг AdaFace должен быть размерности 512.");
        }

        _vector = vector;
        QualityNorm = qualityNorm;
    }
}
