// <copyright file="LLamaModel.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public unsafe class LLamaModel : NativeObjectWrapper<llama_model>
{
    private LLamaModel(llama_model* native) : base(native)
    {
    }

    public bool HasEncoder
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_has_encoder(SafeNative);
    }

    public bool HasDecoder
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_has_decoder(SafeNative);
    }

    public bool IsRecurrent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_is_recurrent(SafeNative);
    }

    public int DecoderStartToken
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_decoder_start_token(SafeNative);
    }

    public bool IsHybrid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_is_hybrid(SafeNative);
    }

    public bool IsDiffusion
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_is_diffusion(SafeNative);
    }

    /// <summary>
    /// Возвращает размер контекстного окна (максимальное количество токенов), на
    /// котором модель изначально обучалась (training context size)
    /// </summary>
    public int TrainContextSize
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_n_ctx_train(SafeNative);
    }

    /// <summary>
    /// Возвращает общую размерность эмбеддингов (скрытых состояний) модели (embedding dimension)
    /// </summary>
    public int EmbeddingDimension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_n_embd(SafeNative);
    }
    /// <summary>
    /// Возвращает размерность входных эмбеддингов модели. Используется для поддержки расширенных
    /// или раздельных архитектур эмбеддингов.
    /// </summary>
    public int InputEmbeddingDimension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_n_embd_inp(SafeNative);
    }

    /// <summary>
    /// Возвращает размерность выходных эмбеддингов модели перед классификационным слоем.
    /// </summary>
    public int OutputEmbeddingDimension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_n_embd_out(SafeNative);
    }

    /// <summary>
    /// Возвращает общее количество слоев (блоков трансформера) в архитектуре модели (number of layers).
    /// </summary>
    public int LayersCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_n_layer(SafeNative);
    }
    /// <summary>
    /// Возвращает количество голов внимания (attention heads) для механизма Query (number of attention heads)
    /// </summary>
    public int AttentionHeadCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_n_head(SafeNative);
    }

    /// <summary>
    /// Возвращает количество голов Key и Value (KV-голов) на один слой.
    /// Различается с n_head при использовании механизмов Grouped-Query Attention (GQA)
    /// или Multi-Query Attention (MQA).
    /// </summary>
    public int AttentionHeadKVCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_n_head_kv(SafeNative);
    }
    /// <summary>
    /// Возвращает размер окна скользящего внимания (Sliding Window Attention size).
    /// Для моделей без поддержки SWA (например, Qwen 3.5) или при отсутствии окна возвращает
    /// значение, сигнализирующее о его отсутствии или базовой конфигурации.
    /// </summary>
    public int SlidingWindowSize
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_model_n_swa(SafeNative);
    }


    public static LLamaModel LoadFromFile(string modelPath, LLamaModelParams parameters)
    {
        using var utf8modelPath = new PinnedUtf8String(modelPath);
        var native = llama_model_load_from_file((sbyte*)utf8modelPath.Pointer, parameters._params);
        if (native is null)
        {
            throw new InvalidOperationException("Failed to load llama model from file");
        }
        return new LLamaModel(native);
    }

    public LLamaVocabulary Vocabulary
    {
        get
        {
            if (field is null)
            {
                var native = llama_model_get_vocab(SafeNative);
                if (native is null)
                {
                    throw new InvalidOperationException("Failed to get llama vocabulary");
                }
                field = new LLamaVocabulary(native);
            }
            return field;
        }
    }

    public LLamaTemplate GetChatTemplate()
    {
        var template = llama_model_chat_template(SafeNative, null);
        return new LLamaTemplate(template);
    }

    public string? GetChatTemplate(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        using var utf8name = new PinnedUtf8String(name);
        var template = llama_model_chat_template(SafeNative, (sbyte*)utf8name.Pointer);
        return Utf8StringMarshaller.ConvertToManaged((byte*)template);
    }

    protected override void FreeNativeResource(llama_model* pointer)
    {
        llama_model_free(pointer);
    }
}
