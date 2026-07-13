// <copyright file="GgmlBackend.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Diagnostics;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public unsafe class GgmlBackend : NativeObjectWrapper<ggml_backend>
{
    internal GgmlBackend(ggml_backend* native) : base(native)
    {
    }

    public GgmlBackendDevice GetDevice()
    {
        var deviceNative = ggml_backend_get_device(SafeNative);
        if (deviceNative is null)
        {
            throw new InvalidOperationException("Failed to get device from ggml backend");
        }
        return new GgmlBackendDevice(deviceNative);
    }

    protected override void FreeNativeResource(ggml_backend* pointer)
    {
        Debug.Assert(pointer != null);
        ggml_backend_free(pointer);
    }
}
