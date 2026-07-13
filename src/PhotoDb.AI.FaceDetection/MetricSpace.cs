// <copyright file="MetricSpace.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Diagnostics;
using System.Numerics.Tensors;
using System.Runtime.CompilerServices;

namespace PhotoDb.AI.FaceDetection;

public static class MetricSpace
{
    /// <summary>
    /// Вычисляет косинусное сходство для СТРОГО L2-нормализованных векторов.
    /// Явное именование предотвращает ошибочное использование ненормализованных данных.
    /// В Debug-сборке выполняется проверка контракта (длина векторов должна быть равна 1.0).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float CosineSimilarityNormalized(ReadOnlySpan<float> v1, ReadOnlySpan<float> v2)
    {
        // Проверка контракта на этапе разработки. В Release-сборке этот код полностью вырезается JIT.
        // Допустимая погрешность округления float — 1e-4f.
        Debug.Assert(MathF.Abs(TensorPrimitives.SumOfSquares(v1) - 1.0f) < 1e-4f,
            "Биометрическая ошибка: Первый вектор нарушает контракт нормализации (длина != 1.0).");

        Debug.Assert(MathF.Abs(TensorPrimitives.SumOfSquares(v2) - 1.0f) < 1e-4f,
            "Биометрическая ошибка: Второй вектор нарушает контракт нормализации (длина != 1.0).");

        return TensorPrimitives.Dot(v1, v2);
    }

    /// <summary>
    /// Проецирует вектор на единичную гиперсферу S^511 (сжимает его длину до 1.0).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void L2NormalizeInPlace(Span<float> vector)
    {
        var norm = TensorPrimitives.Norm(vector);
        var invNorm = 1.0f / MathF.Max(norm, 1e-10f);
        TensorPrimitives.Multiply(vector, invNorm, vector);
    }
}
