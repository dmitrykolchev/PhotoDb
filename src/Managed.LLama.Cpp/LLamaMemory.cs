// <copyright file="LLamaMemory.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public sealed unsafe class LLamaMemory : NativeObjectWrapper<llama_memory_i>
{
    internal LLamaMemory(llama_memory_i* nativePointer) : base(nativePointer)
    {
    }

    protected override unsafe void FreeNativeResource(llama_memory_i* pointer)
    {
    }

    /// <summary>
    /// Returns the largest position present in the memory for the specified sequence
    /// Note that all positions in the range [pos_min, pos_max] are guaranteed to be present in the memory
    /// Return -1 if the sequence is empty
    /// </summary>
    /// <param name="sequenceId">sequence id</param>
    /// <returns>largest position or -1 if the sequence is empty</returns>
    public int GetMaxPosition(int sequenceId)
    {
        return llama_memory_seq_pos_max(SafeNative, sequenceId);
    }

    /// <summary>
    /// Returns the smallest position present in the memory for the specified sequence
    /// This is typically non-zero only for SWA caches
    /// Note that all positions in the range [pos_min, pos_max] are guaranteed to be present in the memory
    /// Return -1 if the sequence is empty
    /// </summary>
    /// <param name="sequenceId">sequence id</param>
    /// <returns>smallest position or -1 if the sequence is empty</returns>
    public int GetMinPosition(int sequenceId)
    {
        return llama_memory_seq_pos_min(SafeNative, sequenceId);
    }

    /// <summary>
    /// Removes all tokens that belong to the specified sequence and have positions in [p0, p1)
    /// </summary>
    /// <param name="sequenceId">seq_id < 0 : match any sequence</param>
    /// <param name="start">p0 < 0     : [0,  p1]</param>
    /// <param name="end">p1 < 0     : [p0, inf)</param>
    /// <returns>Returns false if a partial sequence cannot be removed. Removing a whole sequence never fails</returns>
    public bool Remove(int sequenceId, int start = 0, int end = -1)
    {
        return llama_memory_seq_rm(SafeNative, sequenceId, start, end);
    }

    /// <summary>
    /// Removes all tokens that do not belong to the specified sequence
    /// </summary>
    /// <param name="sequenceId">sequence id</param>
    public void Keep(int sequenceId)
    {
        llama_memory_seq_keep(SafeNative, sequenceId);
    }

    /// <summary>
    /// Clear the memory contents 
    /// </summary>
    /// <param name="clearData">If data == true, the data buffers will also be cleared together with the metadata</param>
    public void Clear(bool clearData = false)
    {
        llama_memory_clear(SafeNative, clearData);
    }
}
