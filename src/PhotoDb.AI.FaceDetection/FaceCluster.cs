// <copyright file="FaceCluster.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Numerics.Tensors;

namespace PhotoDb.AI.FaceDetection;

public class FaceCluster
{
    public int Id { get; }
    public float[] Centroid { get; private set; }
    public float TotalQualityMass { get; private set; }
    public int FaceCount { get; private set; }

    public FaceCluster(int id, float[] initialEmbedding, float initialQuality)
    {
        Id = id;
        Centroid = (float[])initialEmbedding.Clone();
        TotalQualityMass = initialQuality;
        FaceCount = 1;
    }

    /// <summary>
    /// Инкрементальное обновление центроида на месте (Zero-Allocation Fused Multiply-Add).
    /// </summary>
    public void AddFaceAndUpdateCentroid(ReadOnlySpan<float> embedding, float qualityNorm)
    {
        var currentCentroid = Centroid.AsSpan();

        // 1. Текущий центроид взвешивается по накопленной массе: C = C * M
        TensorPrimitives.Multiply(currentCentroid, TotalQualityMass, currentCentroid);

        // 2. Fused Multiply-Add за один векторизованный проход: C_new = (e_new * Q_new) + C
        TensorPrimitives.MultiplyAdd(embedding, qualityNorm, currentCentroid, currentCentroid);

        // 3. Проекция обратно на гиперсферу S^511
        MetricSpace.L2NormalizeInPlace(currentCentroid);

        TotalQualityMass += qualityNorm;
        FaceCount++;
    }
}
