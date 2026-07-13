// <copyright file="GgufContext.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Text;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public unsafe class GgufContext : NativeObjectWrapper<gguf_context>
{
    private GgufContext(gguf_context* native) : base(native)
    {
    }

    public GgufContext InitEmpty()
    {
        var native = gguf_init_empty();
        if (native is null)
        {
            throw new InvalidOperationException("Failed to initialize gguf context");
        }
        return new GgufContext(native);
    }

    public GgufContext InitFromFile(string path, GgufInitParams parameters)
    {
        var count = Encoding.UTF8.GetByteCount(path);
        var buffer = stackalloc byte[count + 1];
        Encoding.UTF8.GetBytes(path, new Span<byte>(buffer, count));
        var native = gguf_init_from_file((sbyte*)buffer, parameters._params);
        if (native is null)
        {
            throw new InvalidOperationException("Failed to initialize gguf context from file");
        }
        return new GgufContext(native);
    }

    protected override void FreeNativeResource(gguf_context* pointer)
    {
        gguf_free(pointer);
    }
}
