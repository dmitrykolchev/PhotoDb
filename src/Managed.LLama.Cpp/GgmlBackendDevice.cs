// <copyright file="GgmlBackendDevice.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.InteropServices.Marshalling;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public unsafe class GgmlBackendDevice : NativeObjectWrapper<ggml_backend_device>
{
    internal GgmlBackendDevice(ggml_backend_device* native) : base(native)
    {
    }

    public string? Name
    {
        get
        {
            var namePtr = ggml_backend_dev_name(SafeNative);
            return namePtr != null ? Utf8StringMarshaller.ConvertToManaged((byte*)namePtr) : null;
        }
    }

    public string? Description
    {
        get
        {
            var descPtr = ggml_backend_dev_description(SafeNative);
            return descPtr != null ? Utf8StringMarshaller.ConvertToManaged((byte*)descPtr) : null;
        }
    }

    public GgmlBackendDeviceType DeviceType => (GgmlBackendDeviceType)ggml_backend_dev_type(SafeNative);

    public (long free, long total) Memory
    {
        get
        {
            nuint free;
            nuint total;
            ggml_backend_dev_memory(SafeNative, &free, &total);
            return ((long)free, (long)total);
        }
    }

    public GgmlBackendDeviceProps GetProperties()
    {
        GgmlBackendDeviceProps props = new();
        fixed (ggml_backend_dev_props* propsPtr = &props._properties)
        {
            ggml_backend_dev_get_props(SafeNative, propsPtr);
        }
        return props;
    }

    protected override void FreeNativeResource(ggml_backend_device* pointer)
    {
        // The ggml_backend_device is owned by the ggml_backend and should not be freed separately
        // Therefore, we do not call any native method to free it here
    }
}
