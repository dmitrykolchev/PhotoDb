// <copyright file="RetinaFaceDetector.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Buffers;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SkiaSharp;

namespace PhotoDb.AI.FaceDetection;

/// <summary>
/// RetinaFace через ONNX Runtime.
///
/// ONNX-контракт:
///   input  "input"  [1, 3, H, W]  float32  BGR, mean-subtracted (B-104, G-117, R-123)
///   output "loc"    [1, N, 4]     anchor-relative deltas (dx, dy, dw, dh)
///          "conf"   [1, N, 2]     softmax (background, face)
///          "landms" [1, N, 10]    5 landmarks x2 (опционально)
///
/// Декодирование anchor-боксов (variance=[0.1, 0.2]):
///   cx = prior_cx + dx * 0.1 * prior_w
///   cy = prior_cy + dy * 0.1 * prior_h
///   w  = prior_w  * exp(dw * 0.2)
///   h  = prior_h  * exp(dh * 0.2)
/// </summary>
public sealed class RetinaFaceDetector : IDisposable
{
    // Параметры модели — должны совпадать с тем, как обучалась сеть
    public const int DefaultSize = 1280; // 640 теряет мелкие лица на больших фото

    private static readonly int[] Steps = [8, 16, 32];
    private static readonly int[][] MinSizes = [[16, 32], [64, 128], [256, 512]];
    private const float Var0 = 0.1f; // variance для cx/cy
    private const float Var1 = 0.2f; // variance для w/h

    private readonly InferenceSession _session;

    // Кэш prior boxes по размеру (обычно нужен только один — 1280)
    private readonly System.Collections.Concurrent.ConcurrentDictionary<(int, int), float[]> _priorCache = [];

    public RetinaFaceDetector(string modelPath, bool useGpu = false)
    {
        var opts = BuildSessionOptions(useGpu);
        _session = new InferenceSession(modelPath, opts);
    }

    private static SessionOptions BuildSessionOptions(bool useGpu)
    {
        SessionOptions opts = new()
        {
            GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL
        };

        if (useGpu)
        {
            try
            {
                opts.AppendExecutionProvider_CUDA();
            }
            catch
            {
                // Молча падаем на CPU если CUDA недоступна
            }
        }

        return opts;
    }

    public List<DetectedFace> Detect(SKBitmap bitmap, float scoreThreshold = 0.5f, float iouThreshold = 0.4f, int maxDim = 1280)
    {
        var (targetW, targetH, _) = CalculateTargetSize(bitmap.Width, bitmap.Height, maxDim);

        var tensorData = BuildInputTensor(bitmap, targetW, targetH, out var scaleX, out var scaleY);

        try
        {
            // Создаем тензор, оборачивая арендованный массив (без копирования)
            var inputMemory = new Memory<float>(tensorData, 0, 3 * targetW * targetH);
            var inputTensor = new DenseTensor<float>(inputMemory, new[] { 1, 3, targetH, targetW });

            // ИСПРАВЛЕНИЕ 1: Убрано using и изменен тип массива (входные параметры не диспозятся)
            var inputs = new NamedOnnxValue[]
            {
                NamedOnnxValue.CreateFromTensor("input", inputTensor)
            };

            using var results = _session.Run(inputs);

            // ИСПРАВЛЕНИЕ 2: Явное приведение к DenseTensor<float> через свойство Value
            var locTensor = (DenseTensor<float>)results.First(r => r.Name == "loc").Value;
            var confTensor = (DenseTensor<float>)results.First(r => r.Name == "conf").Value;
            var landmsTensor = (DenseTensor<float>)results.First(r => r.Name == "landms").Value;

            if (locTensor == null || confTensor == null)
            {
                throw new InvalidOperationException("Критическая ошибка: ONNX Runtime вернул не DenseTensor.");
            }

            // ОДНОМЕРНЫЕ SPAN'ы ДЛЯ МАКСИМАЛЬНОЙ СКОРОСТИ
            ReadOnlySpan<float> loc = locTensor.Buffer.Span;
            ReadOnlySpan<float> conf = confTensor.Buffer.Span;
            ReadOnlySpan<float> landms = landmsTensor.Buffer.Span;

            // Генерация priors требует передачи Width и Height отдельно
            var priors = GetOrBuildPriors(targetW, targetH);
            var numAnchors = priors.Length / 4;

            var candidates = new List<(FaceBox Box, Landmark[] Landmarks)>(128);

            for (var i = 0; i < numAnchors; i++)
            {
                // conf имеет layout [1, N, 2]. Индекс i-го анкора: i * 2 + 1
                var score = conf[(i * 2) + 1];
                if (score < scoreThreshold)
                {
                    continue;
                }

                var pIdx = i * 4;
                var pCx = priors[pIdx + 0];
                var pCy = priors[pIdx + 1];
                var pW = priors[pIdx + 2];
                var pH = priors[pIdx + 3];

                // loc имеет layout [1, N, 4]. Индекс i-го анкора: i * 4 + offset
                var dx = loc[(i * 4) + 0];
                var dy = loc[(i * 4) + 1];
                var dw = loc[(i * 4) + 2];
                var dh = loc[(i * 4) + 3];

                var cx = pCx + (dx * Var0 * pW);
                var cy = pCy + (dy * Var0 * pH);
                var w = pW * MathF.Exp(dw * Var1);
                var h = pH * MathF.Exp(dh * Var1);

                var x1 = (cx - (w / 2f)) * targetW * scaleX;
                var y1 = (cy - (h / 2f)) * targetH * scaleY;
                var x2 = (cx + (w / 2f)) * targetW * scaleX;
                var y2 = (cy + (h / 2f)) * targetH * scaleY;

                var landmarks = DecodeLandmarksFast(landms, i, pCx, pCy, pW, pH, targetW, targetH, scaleX, scaleY);

                candidates.Add((new FaceBox(x1, y1, x2, y2, score), landmarks));
            }

            return NMS(candidates, iouThreshold);
        }
        finally
        {
            // Обязательный возврат памяти в пул
            ArrayPool<float>.Shared.Return(tensorData);
        }
    }

    private static Landmark[] DecodeLandmarksFast(ReadOnlySpan<float> landms, int i, float pCx, float pCy, float pW, float pH, int targetW, int targetH, float scaleX, float scaleY)
    {
        if (landms.IsEmpty)
        {
            return Array.Empty<Landmark>();
        }

        var raw = new Landmark[5];
        var baseIdx = i * 10; // [1, N, 10] layout
        for (var p = 0; p < 5; p++)
        {
            var lx = (pCx + (landms[baseIdx + (p * 2)] * Var0 * pW)) * targetW * scaleX;
            var ly = (pCy + (landms[baseIdx + (p * 2) + 1] * Var0 * pH)) * targetH * scaleY;
            raw[p] = new Landmark(lx, ly);
        }
        return raw;
    }

    // Вычисление оптимального размера, кратного 32, с сохранением Aspect Ratio
    private static (int TargetW, int TargetH, float Scale) CalculateTargetSize(int originalW, int originalH, int maxDim = 1280)
    {
        var scale = 1.0f;
        if (originalW > maxDim || originalH > maxDim)
        {
            scale = maxDim / (float)Math.Max(originalW, originalH);
        }

        var targetW = (int)Math.Ceiling(originalW * scale);
        var targetH = (int)Math.Ceiling(originalH * scale);

        // Округление до ближайшего кратного 32 (требование FPN)
        targetW = Math.Max(32, (targetW + 31) / 32 * 32);
        targetH = Math.Max(32, (targetH + 31) / 32 * 32);

        return (targetW, targetH, scale);
    }

    private float[] BuildInputTensor(SKBitmap bitmap, int targetW, int targetH, out float scaleX, out float scaleY)
    {
        // Поскольку мы сохраняем пропорции при Resize, scaleX и scaleY будут почти идентичны,
        // разница возникает только из-за выравнивания по границе 32 пикселей.
        scaleX = (float)bitmap.Width / targetW;
        scaleY = (float)bitmap.Height / targetH;

        var plane = targetW * targetH;
        var tensorLength = 3 * plane;

        // Использование пула для предотвращения аллокаций в LOH
        var tensor = ArrayPool<float>.Shared.Rent(tensorLength);

        using var resized = bitmap.Resize(new SKImageInfo(targetW, targetH), SKSamplingOptions.Default);

        unsafe
        {
            var p = (byte*)resized.GetPixels().ToPointer();
            for (var y = 0; y < targetH; y++)
            {
                for (var x = 0; x < targetW; x++)
                {
                    var pixelIdx = ((y * targetW) + x) * 4;
                    var planeIdx = (y * targetW) + x;

                    tensor[planeIdx] = p[pixelIdx + 0] - 104f;             // B
                    tensor[plane + planeIdx] = p[pixelIdx + 1] - 117f;     // G
                    tensor[(2 * plane) + planeIdx] = p[pixelIdx + 2] - 123f; // R
                }
            }
        }
        return tensor;
    }

    private float[] GetOrBuildPriors(int w, int h)
    {
        // Ключ кэша — комбинация ширины и высоты
        return _priorCache.GetOrAdd((w, h), size =>
        {
            var priors = new List<float>();

            for (var k = 0; k < Steps.Length; k++)
            {
                var step = Steps[k];
                var featureMapW = (int)MathF.Ceiling((float)size.Item1 / step);
                var featureMapH = (int)MathF.Ceiling((float)size.Item2 / step);

                for (var i = 0; i < featureMapH; i++)
                {
                    for (var j = 0; j < featureMapW; j++)
                    {
                        foreach (var minSize in MinSizes[k])
                        {
                            var sW = (float)minSize / size.Item1;
                            var sH = (float)minSize / size.Item2;
                            var cx = (j + 0.5f) * step / size.Item1;
                            var cy = (i + 0.5f) * step / size.Item2;

                            priors.Add(cx);
                            priors.Add(cy);
                            priors.Add(sW);
                            priors.Add(sH);
                        }
                    }
                }
            }
            return priors.ToArray();
        });
    }

    /// <summary>
    /// Декодирует 5 landmarks из тензора landms для i-го anchor.
    /// Возвращает пустой массив если landms == null.
    ///
    /// ONNX экспорт кодирует landmarks в порядке:
    ///   [0]=L_eye, [1]=R_eye, [2]=nose, [3]=L_mouth, [4]=R_mouth
    /// </summary>
    private static Landmark[] Decodelandmarks(
        Tensor<float>? landms, int i,
        float pCx, float pCy, float pW, float pH,
        int inferenceSize, float scaleX, float scaleY)
    {
        if (landms == null)
        {
            return [];
        }

        var raw = new Landmark[5];
        for (var p = 0; p < 5; p++)
        {
            var lx = (pCx + (landms[0, i, p * 2] * Var0 * pW)) * inferenceSize * scaleX;
            var ly = (pCy + (landms[0, i, (p * 2) + 1] * Var0 * pH)) * inferenceSize * scaleY;
            raw[p] = new Landmark(lx, ly);
        }
        return raw;
    }

    /// <summary>
    /// Non-Maximum Suppression. Greedy, O(N²) — достаточно для типичных N < 1000 кандидатов.
    /// </summary>
    private static List<DetectedFace> NMS(
        List<(FaceBox Box, Landmark[] Landmarks)> candidates,
        float iouThreshold)
    {
        // Сортировка по убыванию уверенности
        candidates.Sort((a, b) => b.Box.Score.CompareTo(a.Box.Score));

        List<DetectedFace> selected = new(candidates.Count);
        var suppressed = new bool[candidates.Count];

        for (var i = 0; i < candidates.Count; i++)
        {
            if (suppressed[i])
            {
                continue;
            }

            selected.Add(new DetectedFace(candidates[i].Box, candidates[i].Landmarks));

            for (var j = i + 1; j < candidates.Count; j++)
            {
                if (suppressed[j])
                {
                    continue;
                }

                if (IoU(candidates[i].Box, candidates[j].Box) > iouThreshold)
                {
                    suppressed[j] = true;
                }
            }
        }

        return selected;
    }

    private static float IoU(FaceBox a, FaceBox b)
    {
        var ix1 = MathF.Max(a.X1, b.X1);
        var iy1 = MathF.Max(a.Y1, b.Y1);
        var ix2 = MathF.Min(a.X2, b.X2);
        var iy2 = MathF.Min(a.Y2, b.Y2);

        var iw = MathF.Max(0f, ix2 - ix1);
        var ih = MathF.Max(0f, iy2 - iy1);
        var intersection = iw * ih;
        if (intersection == 0f)
        {
            return 0f;
        }

        var aArea = a.Width * a.Height;
        var bArea = b.Width * b.Height;
        return intersection / (aArea + bArea - intersection);
    }

    public void Dispose()
    {
        _session.Dispose();
    }
}
