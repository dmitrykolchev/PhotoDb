// <copyright file="GgmlBackendDeviceType.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using static Managed.LLama.Cpp.Native.ggml_backend_dev_type;

namespace Managed.LLama.Cpp;

public enum GgmlBackendDeviceType
{
    /// <summary>
    /// CPU device using system memory
    /// </summary>
    Cpu = GGML_BACKEND_DEVICE_TYPE_CPU,
    /// <summary>
    /// GPU device using dedicated video memory
    /// </summary>
    Gpu = GGML_BACKEND_DEVICE_TYPE_GPU,
    /// <summary>
    /// integrated GPU device using host memory
    /// </summary>
    Igpu = GGML_BACKEND_DEVICE_TYPE_IGPU,
    /// <summary>
    /// accelerator devices intended to be used together with the CPU backend (e.g. BLAS or AMX)
    /// </summary>
    Accel = GGML_BACKEND_DEVICE_TYPE_ACCEL,
    /// <summary>
    /// "meta" device wrapping multiple other devices for tensor parallelism
    /// </summary>
    Meta = GGML_BACKEND_DEVICE_TYPE_META,
}
