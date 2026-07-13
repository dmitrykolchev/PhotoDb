// <copyright file="GgmlBackendDeviceProps.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.InteropServices.Marshalling;
using Managed.LLama.Cpp.Native;

namespace Managed.LLama.Cpp;

public unsafe class GgmlBackendDeviceProps
{
    internal ggml_backend_dev_props _properties;

    public string? Name =>
        _properties.name != null ? Utf8StringMarshaller.ConvertToManaged((byte*)_properties.name) : string.Empty;

    public string? Description =>
        _properties.description != null ? Utf8StringMarshaller.ConvertToManaged((byte*)_properties.description) : string.Empty;

    public string? DeviceId =>
        _properties.device_id != null ? Utf8StringMarshaller.ConvertToManaged((byte*)_properties.device_id) : string.Empty;

    public GgmlBackendDeviceType DeviceType => (GgmlBackendDeviceType)_properties.type;

    public long FreeMemory => (long)_properties.memory_free;

    public long TotalMemory => (long)_properties.memory_total;
    /// <summary>
    /// asynchronous operations
    /// </summary>
    public bool HasAsync => _properties.caps.async;
    /// <summary>
    /// pinned host buffer
    /// </summary>
    public bool HasHostBuffer => _properties.caps.host_buffer;
    /// <summary>
    /// creating buffers from host ptr
    /// </summary>
    public bool HasBufferFromHostPtr => _properties.caps.buffer_from_host_ptr;
    /// <summary>
    /// event synchronization
    /// </summary>
    public bool HasEvents => _properties.caps.events;
}
