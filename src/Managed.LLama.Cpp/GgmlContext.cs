// <copyright file="GgmlContext.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public unsafe class GgmlContext : NativeObjectWrapper<ggml_context>
{
    private GgmlContext(ggml_context* native) : base(native)
    {
    }

    public static GgmlContext Init(GgmlInitParams parameters)
    {
        var native = ggml_init(parameters._params);
        if (native is null)
        {
            throw new InvalidOperationException("Failed to initialize ggml context");
        }
        return new GgmlContext(native);
    }

    public void Reset()
    {
        ggml_reset(SafeNative);
    }

    protected override unsafe void FreeNativeResource(ggml_context* pointer)
    {
        ggml_free(pointer);
    }
}
