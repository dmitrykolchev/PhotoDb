// <copyright file="Cuda.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.InteropServices.Marshalling;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public static unsafe class Cuda
{
    public static int GetDeviceCount()
    {
        return ggml_backend_cuda_get_device_count();
    }

    public static string? GetDeviceDescription(int deviceIndex)
    {
        var buffer = stackalloc byte[256];
        ggml_backend_cuda_get_device_description(deviceIndex, (sbyte*)buffer, 256);
        return Utf8StringMarshaller.ConvertToManaged(buffer);
    }

    public static void GetDeviceMemory(int deviceIndex, out long freeMemory, out long totalMemory)
    {
        nuint free;
        nuint total;
        ggml_backend_cuda_get_device_memory(deviceIndex, &free, &total);
        freeMemory = (long)free;
        totalMemory = (long)total;
    }

    public static (long free, long total) GetDeviceMemory(int deviceIndex)
    {
        nuint free;
        nuint total;
        ggml_backend_cuda_get_device_memory(deviceIndex, &free, &total);
        return ((long)free, (long)total);
    }

    public static GgmlBackend GetBackend(int deviceIndex)
    {
        var native = ggml_backend_cuda_init(deviceIndex);
        if (native is null)
        {
            throw new InvalidOperationException($"Failed to get CUDA backend for device index {deviceIndex}");
        }
        return new GgmlBackend(native);
    }

    public static bool IsCuda(GgmlBackend backend)
    {
        return ggml_backend_is_cuda(backend.SafeNative);
    }
}
