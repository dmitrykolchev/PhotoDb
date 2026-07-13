// <copyright file="MtmdBitmap.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public sealed unsafe class MtmdBitmap : NativeObjectWrapper<mtmd_bitmap>
{
    internal MtmdBitmap(mtmd_bitmap* nativePointer) : base(nativePointer)
    {
    }

    public bool IsAudio => mtmd_bitmap_is_audio(SafeNative);

    public int Width => (int)mtmd_bitmap_get_nx(SafeNative);

    public int Height => (int)mtmd_bitmap_get_ny(SafeNative);

    public long LongLength => (long)mtmd_bitmap_get_n_bytes(SafeNative);

    public int Length => (int)mtmd_bitmap_get_n_bytes(SafeNative);

    public ReadOnlySpan<byte> ToSpan()
    {
        var length = mtmd_bitmap_get_n_bytes(SafeNative);
        if(length > int.MaxValue)
        {
            throw new InvalidOperationException("Data is too large");
        }
        var data = mtmd_bitmap_get_data(SafeNative);
        return new ReadOnlySpan<byte>(data, (int)length);
    }

    public MtmdBitmap FromImage(int width, int height, ReadOnlySpan<byte> data)
    {
        if(width <= 0 || height <= 0)
        {
            throw new ArgumentOutOfRangeException("Width or height less that or equals to 0", default(Exception));
        }
        mtmd_bitmap* nativePtr;
        fixed (byte* ptr = data)
        {
            nativePtr = mtmd_bitmap_init((uint)width, (uint)height, ptr);
        }
        if (nativePtr is null)
        {
            throw new InvalidOperationException("Failed to initialize mtmd bitmap");
        }
        return new MtmdBitmap(nativePtr);
    }

    public MtmdBitmap FromAudio(ReadOnlySpan<float> data)
    {
        mtmd_bitmap* nativePtr;
        fixed (float* ptr = data)
        {
            nativePtr = mtmd_bitmap_init_from_audio((nuint)data.Length, ptr);
        }
        if (nativePtr is null)
        {
            throw new InvalidOperationException("Failed to initialize mtmd bitmap");
        }
        return new MtmdBitmap(nativePtr);
    }

    public MtmdBitmap FromAudio(float[] data)
    {
        mtmd_bitmap* nativePtr;
        fixed (float* ptr = data)
        {
            nativePtr = mtmd_bitmap_init_from_audio((nuint)data.LongLength, ptr);
        }
        if (nativePtr is null)
        {
            throw new InvalidOperationException("Failed to initialize mtmd bitmap");
        }
        return new MtmdBitmap(nativePtr);
    }

    protected override void FreeNativeResource(mtmd_bitmap* pointer)
    {
        mtmd_bitmap_free(pointer);
    }
}
