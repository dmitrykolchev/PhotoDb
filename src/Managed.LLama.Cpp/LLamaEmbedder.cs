// <copyright file="LLamaEmbedder.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.ComponentModel;
using System.Numerics.Tensors;

namespace Managed.LLama.Cpp;

public class LLamaEmbedder : IDisposable
{
    private readonly LLamaModelParams _modelParams;
    private readonly LLamaModel _model;
    private readonly LLamaContextParams _contextParams;
    private readonly LLamaContext _context;

    private LLamaEmbedder(string modelPath, int gpuLayerCount)
    {
        _modelParams = LLamaModelParams.Default();
        _modelParams.UseMemoryMap = true;
        _modelParams.MainGpu = 0;
        _modelParams.GpuLayerCount = gpuLayerCount;
        _model = LLamaModel.LoadFromFile(modelPath, _modelParams);

        _contextParams = LLamaContextParams.Default();
        _contextParams.ContextSize = 4096;
        _contextParams.Embeddings = true;
        _contextParams.AttentionType = LLamaAttentionType.Unspecified;
        _contextParams.FlashAttentionType = LLamaFlashAttentionType.Enabled;
        _contextParams.AttentionType = LLamaAttentionType.NonCausal;
        _contextParams.KvUnified = true;
        _contextParams.TypeK = GgmlType.Q4_0;
        _contextParams.TypeV = GgmlType.Q4_0;
        if (_contextParams.BatchSize < _contextParams.ContextSize)
        {
            _contextParams.BatchSize = _contextParams.ContextSize;
        }
        if (_contextParams.AttentionType != LLamaAttentionType.Causal)
        {
            _contextParams.UBatchSize = _contextParams.BatchSize;
        }

        _context = LLamaContext.InitFromModel(_model, _contextParams);
    }

    /// <summary>
    /// targetDim = null → полный вектор (4096 для Qwen3-4B)
    /// targetDim = 256/512/1024 → MRL truncation
    /// </summary>
    public int? TargetDim { get; set; }

    public float[] GetEmbedding(string text)
    {
        var trainContextLength = _model.TrainContextSize;
        var contextLength = _context.ContextLength;
        if (contextLength > trainContextLength)
        {
            throw new WarningException($"warning: model was trained on only {trainContextLength} context tokens ({contextLength} specified)");
        }

        if (_model.HasEncoder && _model.HasDecoder)
        {
            throw new InvalidOperationException("Computing embeddings in encoder-decoder models is not supported");
        }

        var addedSepToken = _model.Vocabulary.GetAddSep() ? _model.Vocabulary.GetText(_model.Vocabulary.Sep) : "";
        var addedEosToken = _model.Vocabulary.GetAddEos() ? _model.Vocabulary.GetText(_model.Vocabulary.Eos) : "";

        var tokens = _context.Tokenize(text, true, true);
        if (tokens.Length > _context.ContextLength)
        {
            throw new InvalidOperationException("Embedding prompt is longer than the context window");
        }
        var batch = new LLamaBatch(tokens);

        var result = _context.Decode(batch, 0, 0);

        var poolingType = _context.PoolingType;
        var resultsCount = poolingType == LLamaPoolingType.None ? tokens.Length : 1;

        var outputEmbeddingDimension = _model.OutputEmbeddingDimension;

        Span<float> data;
        if (poolingType == LLamaPoolingType.None)
        {
            data = _context.GetEmbeddingsIth(batch.Length - 1);
        }
        else if (poolingType == LLamaPoolingType.Last)
        {
            data = _context.GetEmbeddingsSeq(0);
        }
        else
        {
            // TODO: implement
            throw new NotSupportedException();
        }
        var targetDimension = Math.Min(data.Length, TargetDim ?? outputEmbeddingDimension);
        var embedding = new float[targetDimension];
        data[..targetDimension].CopyTo(embedding);
        Normalize(embedding.AsSpan());
        return embedding;
    }

    public float CosineSimilarity(string a, string b)
    {
        var ea = GetEmbedding(a);
        var eb = GetEmbedding(b);
        // Векторы уже нормализованы → cosine = dot product
        return TensorPrimitives.Dot(ea, eb);
    }

    public static LLamaEmbedder Create(string modelPath, int gpuLayerCount = 99)
    {
        return new LLamaEmbedder(modelPath, gpuLayerCount);
    }

    private static Span<float> Normalize(Span<float> vector)
    {
        TensorPrimitives.Divide(vector, TensorPrimitives.Norm(vector), vector);
        return vector;
    }

    public void Dispose()
    {
        _context.Dispose();
        _model.Dispose();
    }
}
