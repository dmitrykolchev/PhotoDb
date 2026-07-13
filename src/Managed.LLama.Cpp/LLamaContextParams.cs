// <copyright file="LLamaContextParams.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.CompilerServices;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public class LLamaContextParams
{
    internal llama_context_params _params;

    public LLamaContextParams()
    {
    }

    public static LLamaContextParams Default()
    {
        return new LLamaContextParams
        {
            _params = llama_context_default_params()
        };
    }
    /// <summary>
    /// context size used during inference
    /// </summary>
    public uint ContextSize
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.n_ctx;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.n_ctx = value;
    }
    /// <summary>
    /// logical maximum batch size that can be submitted to llama_decode
    /// </summary>
    public uint BatchSize
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.n_batch;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.n_batch = value;
    }
    /// <summary>
    /// physical maximum batch size
    /// </summary>
    public uint UBatchSize
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.n_ubatch;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.n_ubatch = value;
    }

    /// <summary>
    /// max number of sequences (i.e. distinct states for recurrent models)
    /// </summary>
    public uint SeqMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.n_seq_max;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.n_seq_max = value;
    }

    /// <summary>
    /// number of recurrent-state snapshots per seq for rollback (0 = no rollback) [EXPERIMENTAL]
    /// </summary>
    public uint RecurrentRollbackSnapshots
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.n_rs_seq;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.n_rs_seq = value;
    }

    /// <summary>
    ///  max outputs in a ubatch (0 = n_batch)
    /// </summary>
    public uint OutputsMaxCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.n_outputs_max;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.n_outputs_max = value;
    }

    /// <summary>
    /// number of threads to use for generation
    /// </summary>
    public int Threads
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.n_threads;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value > 0)
            {
                _params.n_threads = value;
            }
            else
            {
                _params.n_threads = Math.Max(Environment.ProcessorCount / 2, 1);
            }
        }
    }

    /// <summary>
    /// number of threads to use for batch processing
    /// </summary>
    public int BatchThreads
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.n_threads_batch;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value > 0)
            {
                _params.n_threads_batch = value;
            }
            else
            {
                _params.n_threads_batch = Math.Max(Environment.ProcessorCount / 2, 1);
            }
        }
    }

    public LLamaContextType ContextType
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (LLamaContextType)_params.ctx_type;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.ctx_type = (llama_context_type)value;
    }

    public LLamaRopeScalingType RopeScalingType
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (LLamaRopeScalingType)_params.rope_scaling_type;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.rope_scaling_type = (llama_rope_scaling_type)value;
    }

    public LLamaAttentionType AttentionType
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (LLamaAttentionType)_params.attention_type;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.attention_type = (llama_attention_type)value;
    }

    public LLamaFlashAttentionType FlashAttentionType
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (LLamaFlashAttentionType)_params.flash_attn_type;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.flash_attn_type = (llama_flash_attn_type)value;
    }

    public LLamaPoolingType PoolingType
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (LLamaPoolingType)_params.pooling_type;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.pooling_type = (llama_pooling_type)value;
    }

    public bool Embeddings
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.embeddings;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.embeddings = value;
    }

    public bool KqvOffload
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.offload_kqv;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.offload_kqv = value;
    }

    public bool NoPerf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.no_perf;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.no_perf = value;
    }

    public bool OpOffload
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.op_offload;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.op_offload = value;
    }

    public bool SwaFull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.swa_full;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.swa_full = value;
    }

    public bool KvUnified
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _params.kv_unified;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.kv_unified = value;
    }

    public GgmlType TypeK
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (GgmlType)_params.type_k;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.type_k = (ggml_type)value;
    }

    public GgmlType TypeV
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (GgmlType)_params.type_v;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _params.type_v = (ggml_type)value;
    }

}
