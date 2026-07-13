// <copyright file="LLamaContext.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Buffers;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public sealed unsafe class LLamaContext : NativeObjectWrapper<llama_context>
{
    private LLamaContext(LLamaModel model, llama_context* native) : base(native)
    {
        Model = model;
    }

    public static LLamaContext InitFromModel(LLamaModel model, LLamaContextParams parameters)
    {
        ArgumentNullException.ThrowIfNull(model);
        var native = llama_init_from_model(model.SafeNative, parameters._params);
        if (native is null)
        {
            throw new InvalidOperationException("Failed to initialize llama context");
        }
        return new LLamaContext(model, native);
    }

    public LLamaModel Model { get; }

    public int ContextLength => (int)llama_n_ctx(SafeNative);

    public int BatchSize => (int)llama_n_batch(SafeNative);

    public LLamaPoolingType PoolingType => (LLamaPoolingType)llama_pooling_type(SafeNative);

    public LLamaMemory Memory
    {
        get
        {
            if (field is null)
            {
                var pointer = llama_get_memory(SafeNative);
                if (pointer is null)
                {
                    throw new InvalidOperationException("Failed to get llama memory");
                }
                field = new LLamaMemory(pointer);
            }
            return field;
        }
    }

    /// <summary>
    /// Convert the provided text into tokens.
    /// </summary>
    /// <param name="text">text</param>
    /// <param name="addSpecial">Allow to add BOS and EOS tokens if model is configured to do so.</param>
    /// <param name="parseSpecial">Allow tokenizing special and/or control tokens which otherwise are not exposed and treated as plaintext.</param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public int[] Tokenize(string text, bool addSpecial, bool parseSpecial)
    {
        return Tokenize(Model.Vocabulary, text, addSpecial, parseSpecial);
    }

    /// <summary>
    /// Convert the provided text into tokens.
    /// </summary>
    /// <param name="vocabulary"></param>
    /// <param name="text">text</param>
    /// <param name="addSpecial">Allow to add BOS and EOS tokens if model is configured to do so.</param>
    /// <param name="parseSpecial">Allow tokenizing special and/or control tokens which otherwise are not exposed and treated as plaintext.</param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public int[] Tokenize(LLamaVocabulary vocabulary, string text, bool addSpecial, bool parseSpecial)
    {
        // Early exit if there's no work to do
        if (string.IsNullOrEmpty(text) && !addSpecial)
        {
            return [];
        }

        using var utf8text = new PinnedUtf8String(text);
        // Tokenize once with no output, to get the token count. Output will be negative (indicating that there was insufficient space)
        var count = -llama_tokenize(vocabulary.SafeNative, (sbyte*)utf8text.Pointer, utf8text.Length, null, 0, addSpecial, parseSpecial);
        // Tokenize again, this time outputting into an array of exactly the right size
        var tokens = new int[count];
        fixed (void* tokensPtr = tokens)
        {
            if (llama_tokenize(vocabulary.Native, (sbyte*)utf8text.Pointer, utf8text.Length, (int*)tokensPtr, count, addSpecial, parseSpecial) < 0)
            {
                throw new InvalidOperationException("Tokenize failed");
            }
            return tokens;
        }
    }
    /// <summary>
    /// Get the embeddings for the ith token. For positive indices, Equivalent to:
    /// llama_get_embeddings(ctx) + ctx->output_ids[i]*n_embd
    /// Negative indices can be used to access embeddings in reverse order, -1 is the last embedding.
    /// shape: [n_embd] (1-dimensional)
    /// </summary>
    /// <param name="position"></param>
    /// <returns>returns NULL for invalid ids.</returns>
    public Span<float> GetEmbeddingsIth(int position)
    {
        var empbedingsPtr = llama_get_embeddings_ith(SafeNative, position);
        if (empbedingsPtr == null)
        {
            throw new InvalidOperationException("invalid position");
        }
        // REVIEW: could be error if position != 0
        return new Span<float>(empbedingsPtr, Model.EmbeddingDimension);
    }

    /// <summary>
    /// Get the embeddings for a sequence id
    /// Returns NULL if pooling_type is LLAMA_POOLING_TYPE_NONE
    /// when pooling_type == LLAMA_POOLING_TYPE_RANK, returns float[n_cls_out] with the rank(s) of the sequence
    /// otherwise: float[n_embd] (1-dimensional)
    /// </summary>
    /// <param name="sequenceId"></param>
    /// <returns></returns>
    public Span<float> GetEmbeddingsSeq(int sequenceId)
    {
        var embeddingsPtr = llama_get_embeddings_seq(SafeNative, sequenceId);
        if (embeddingsPtr == null)
        {
            throw new InvalidOperationException("invalid pooling type (LLAMA_POOLING_TYPE_NONE)");
        }
        return new Span<float>(embeddingsPtr, Model.EmbeddingDimension);
    }

    public int DecodeSingle(LLamaBatch batch, int position, int sequenceId)
    {
        if (batch.Length > BatchSize)
        {
            throw new ArgumentException("Input contains more tokens than configured batch size");
        }
        
        var positions = ArrayPool<int>.Shared.Rent(batch.Length);
        var logits = ArrayPool<sbyte>.Shared.Rent(batch.Length);
        try
        {
            fixed (int* tokensPtr = batch.Tokens)
            fixed (int* positionsPtr = positions)
            fixed (sbyte* logitsPtr = logits)
            {
                var currentPosition = position;
                var length = batch.Length;
                for (var index = 0; index < length; index++)
                {
                    positions[index] = currentPosition++;
                    logits[index] = (index == batch.Length - 1) ? (sbyte)1 : (sbyte)0;
                }
                llama_batch b = new()
                {
                    n_tokens = length,
                    token = tokensPtr,
                    pos = positionsPtr,
                    logits = logitsPtr
                };
                var result = llama_decode(SafeNative, b);
                if (result != 0)
                {
                    throw new InvalidOperationException($"failed to decode, result = {result}");
                }
            }
        }
        finally
        {
            ArrayPool<int>.Shared.Return(positions);
            ArrayPool<sbyte>.Shared.Return(logits);
        }
        return position + batch.Length;
    }

    public int Decode(LLamaBatch batch, int position, int sequenceId)
    {
        var batchSize = BatchSize;
        if (batch.Length < batchSize)
        {
            return DecodeSingle(batch, position, sequenceId);
        }
        else
        {
            var positions = ArrayPool<int>.Shared.Rent(batchSize);
            var logits = ArrayPool<sbyte>.Shared.Rent(batchSize);
            try
            {
                fixed (int* tokensPtr = batch.Tokens)
                fixed (int* positionsPtr = positions)
                fixed (sbyte* logitsPtr = logits)
                {
                    var currentPtr = tokensPtr;
                    var rest = batch.Length;
                    var currentPosition = position;
                    while (rest > 0)
                    {
                        var length = rest < batchSize ? rest : batchSize;
                        for (var index = 0; index < length; index++)
                        {
                            positions[index] = currentPosition++;
                        }
                        if (rest <= length)
                        {
                            logits[rest - 1] = 1;
                        }
                        llama_batch b = new()
                        {
                            n_tokens = length,
                            token = currentPtr,
                            pos = positionsPtr,
                            logits = logitsPtr
                        };
                        currentPtr += length;
                        var result = llama_decode(SafeNative, b);
                        if (result != 0)
                        {
                            throw new InvalidOperationException($"failed to decode, result = {result}");
                        }
                        rest -= length;
                    }
                }
            }
            finally
            {
                ArrayPool<int>.Shared.Return(positions);
                ArrayPool<sbyte>.Shared.Return(logits);
            }
            return position + batch.Length;
        }
    }

    protected override unsafe void FreeNativeResource(llama_context* pointer)
    {
        llama_free(pointer);
    }
}
