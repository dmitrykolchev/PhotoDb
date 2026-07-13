// <copyright file="LLama.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.CompilerServices;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public static class LLama
{
    /// <summary>
    /// Get the maximum number of devices supported by llama.cpp
    /// </summary>
    /// <returns></returns>
    public static long MaxDevices
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (long)llama_max_devices();
    }

    public static long TimeUs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_time_us();
    }

    public static long MaxParallelSequences
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (long)llama_max_parallel_sequences();
    }

    public static long MaxTensorBuftOverrides
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (long)llama_max_tensor_buft_overrides();
    }

    public static bool SupportsMemoryMap
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_supports_mmap();
    }

    public static bool SupportsMemoryLock
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_supports_mlock();
    }

    public static bool SupportsGpuOffload
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_supports_gpu_offload();
    }

    public static bool SupportsRpc
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_supports_rpc();
    }
}
