// <copyright file="LLamaModelParams.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public class LLamaModelParams
{
    internal llama_model_params _params;

    public LLamaModelParams()
    {
    }

    public static LLamaModelParams Default()
    {
        return new LLamaModelParams()
        {
            _params = llama_model_default_params()
        };
    }

    /// <summary>
    /// number of layers to store in VRAM, a negative value means all layers
    /// </summary>
    public int GpuLayerCount
    {
        get => _params.n_gpu_layers;
        set => _params.n_gpu_layers = value;
    }

    /// <summary>
    /// how to split the model across multiple GPUs
    /// </summary>
    public LLamaSplitMode SplitMode
    {
        get => (LLamaSplitMode)_params.split_mode;
        set => _params.split_mode = (llama_split_mode)value;
    }

    /// <summary>
    /// the GPU that is used for the entire model when split_mode is <see cref="LLamaSplitMode.None"/>
    /// </summary>
    public int MainGpu
    {
        get => _params.main_gpu;
        set => _params.main_gpu = value;
    }

    /// <summary>
    /// only load the vocabulary, no weights
    /// </summary>
    public bool VocabOnly
    {
        get => _params.vocab_only;
        set => _params.vocab_only = value;
    }

    /// <summary>
    /// use mmap if possible
    /// </summary>
    public bool UseMemoryMap
    {
        get => _params.use_mmap;
        set => _params.use_mmap = value;
    }

    /// <summary>
    /// use direct io, takes precedence over use_mmap when supported
    /// </summary>
    public bool UseDirectIO
    {
        get => _params.use_direct_io;
        set => _params.use_direct_io = value;
    }

    /// <summary>
    /// force system to keep model in RAM
    /// </summary>
    public bool UseMemoryLock
    {
        get => _params.use_mlock;
        set => _params.use_mlock = value;
    }

    /// <summary>
    /// validate model tensor data
    /// </summary>
    public bool CheckTensors
    {
        get => _params.check_tensors;
        set => _params.check_tensors = value;
    }

    /// <summary>
    /// use extra buffer types (used for weight repacking)
    /// </summary>
    public bool UseExtraBufts
    {
        get => _params.use_extra_bufts;
        set => _params.use_extra_bufts = value;
    }

    /// <summary>
    /// bypass host buffer allowing extra buffers to be used
    /// </summary>
    public bool NoHost
    {
        get => _params.no_host;
        set => _params.no_host = value;
    }
    /// <summary>
    /// only load metadata and simulate memory allocations
    /// </summary>
    public bool NoAlloc
    {
        get => _params.no_alloc;
        set => _params.no_alloc = value;
    }
}
