// <copyright file="AdaFaceEmbedder.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Buffers;
using System.Numerics.Tensors;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SkiaSharp;

namespace PhotoDb.AI.FaceDetection;

public sealed class AdaFaceEmbedder : IDisposable
{
    private readonly InferenceSession _session;
    private readonly string _inputName;
    private readonly string _outputName;

    public AdaFaceEmbedder(string onnxModelPath, bool useGpu = false)
    {
        SessionOptions options = new()
        {
            GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL
        };

        if (useGpu)
        {
            try { options.AppendExecutionProvider_CUDA(); }
            catch { /* Fallback to CPU */ }
        }

        _session = new InferenceSession(onnxModelPath, options);
        _inputName = _session.InputMetadata.Keys.First();
        _outputName = _session.OutputMetadata.Keys.First();
    }

    public FaceEmbedding Extract(string imagePath)
    {
        using var image = SKBitmap.Decode(imagePath);
        return Extract(image);
    }

    /// <summary>
    /// Извлекает 512D вектор и его норму (качество) из выровненного лица 112x112.
    /// </summary>
    public FaceEmbedding Extract(SKBitmap alignedFace)
    {
        if (alignedFace.Width != 112 || alignedFace.Height != 112)
        {
            throw new ArgumentException("Входное изображение должно быть строго 112x112.");
        }

        // 1. Препроцессинг в тензор [1, 3, 112, 112]
        var inputData = ArrayPool<float>.Shared.Rent(3 * 112 * 112);
        try
        {
            BuildInputTensor(alignedFace, inputData);

            Memory<float> inputMemory = new(inputData, 0, 3 * 112 * 112);
            DenseTensor<float> inputTensor = new(inputMemory, new[] { 1, 3, 112, 112 });

            var inputs = new NamedOnnxValue[] { NamedOnnxValue.CreateFromTensor(_inputName, inputTensor) };

            // 2. Инференс
            using var results = _session.Run(inputs);

            if (results.First(r => r.Name == _outputName).Value is not DenseTensor<float> outputTensor)
            {
                throw new InvalidOperationException("Output must be DenseTensor.");
            }

            // Копируем сырой вектор из неуправляемой памяти ONNX Runtime
            var rawVector = outputTensor.Buffer.ToArray();

            // 3. Вычисление Quality Norm (L2-норма) и L2-нормализация
            return NormalizeAndExtractQuality(rawVector);
        }
        finally
        {
            ArrayPool<float>.Shared.Return(inputData);
        }
    }

    private static unsafe void BuildInputTensor(SKBitmap bitmap, float[] tensorData)
    {
        var plane = 112 * 112;

        // Для AdaFace стандартная нормализация: (pixel - 127.5) / 127.5 
        // Порядок каналов обычно RGB, так как датасеты (WebFace) парсятся через PIL/torchvision.
        // Важно: если при экспорте модели был жестко задан BGR, нужно поменять каналы. 
        // Здесь реализован RGB (0=R, 1=G, 2=B).

        var p = (byte*)bitmap.GetPixels().ToPointer();
        for (var y = 0; y < 112; y++)
        {
            for (var x = 0; x < 112; x++)
            {
                var pixelIdx = ((y * 112) + x) * 4; // SKBitmap BGRA
                var planeIdx = (y * 112) + x;

                // SkiaSharp хранит пиксели как BGRA
                var b = p[pixelIdx + 0];
                var g = p[pixelIdx + 1];
                var r = p[pixelIdx + 2];

                tensorData[planeIdx] = (r - 127.5f) / 127.5f; // R
                tensorData[plane + planeIdx] = (g - 127.5f) / 127.5f; // G
                tensorData[(2 * plane) + planeIdx] = (b - 127.5f) / 127.5f; // B
            }
        }
    }

    private static FaceEmbedding NormalizeAndExtractQuality(float[] rawVector)
    {
        Span<float> span = rawVector;

        // 1. L2 норма (длина исходного вектора) — это Quality Score в AdaFace
        var qualityNorm = TensorPrimitives.Norm(rawVector);

        // 2. Предотвращение деления на 0 для шумовых инференсов
        var invNorm = 1.0f / MathF.Max(qualityNorm, 1e-10f);

        // 3. L2 Нормализация исходного вектора на месте (In-place)
        // Метод умножает каждый элемент span на инвертированную норму и пишет обратно в span
        TensorPrimitives.Divide(span, invNorm, span);

        return new FaceEmbedding(rawVector, qualityNorm);
    }

    public void Dispose()
    {
        _session.Dispose();
    }
}
