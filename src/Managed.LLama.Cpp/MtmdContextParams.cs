// <copyright file="MtmdContextParams.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public class MtmdContextParams
{
    internal mtmd_context_params _params;

    public bool UseGpu
    {
        get => _params.use_gpu;
        set => _params.use_gpu = value;
    }

    public bool PrintTimings
    {
        get => _params.print_timings;
        set => _params.print_timings = value;
    }

    public int Threads
    {
        get => _params.n_threads;
        set => _params.n_threads = value;
    }

    public LLamaFlashAttentionType FlashAttentionType
    {
        get => (LLamaFlashAttentionType)_params.flash_attn_type;
        set => _params.flash_attn_type = (llama_flash_attn_type)value;
    }

    public bool Warmup
    {
        get => _params.warmup;
        set => _params.warmup = value;
    }

    public int ImageMinTokens
    {
        get => _params.image_min_tokens;
        set => _params.image_min_tokens = value;
    }

    public int ImageMaxTokens
    {
        get => _params.image_max_tokens;
        set => _params.image_max_tokens = value;
    }

    public static MtmdContextParams Default()
    {
        return new MtmdContextParams()
        {
            _params = mtmd_context_params_default()
        };
    }
}
