// <copyright file="MtmdInputChunks.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public sealed unsafe class MtmdInputChunks : NativeObjectWrapper<mtmd_input_chunks>
{
    private MtmdInputChunks(mtmd_input_chunks* nativePointer) : base(nativePointer)
    {
    }

    public static MtmdInputChunks Create()
    {
        var nativePtr = mtmd_input_chunks_init();
        if (nativePtr is null)
        {
            throw new InvalidOperationException("Failed to init mtmd input chunks");
        }
        return new MtmdInputChunks(nativePtr);
    }

    protected override unsafe void FreeNativeResource(mtmd_input_chunks* pointer)
    {
        mtmd_input_chunks_free(pointer);
    }
}
