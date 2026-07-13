// <copyright file="PinnedUtf8String.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Managed.LLama.Cpp;

/// <summary>
/// Creates pinned UTF-8 string from UTF-16 string
/// </summary>
public readonly ref struct PinnedUtf8String
{
    private readonly ArrayPool<byte> _pool;
    private readonly byte[] _buffer;
    private readonly GCHandle _handle;

    public PinnedUtf8String(string text, ArrayPool<byte>? pool = default)
    {
        ArgumentNullException.ThrowIfNull(text);

        _pool = pool ?? ArrayPool<byte>.Shared;

        var maxByteCount = Encoding.UTF8.GetMaxByteCount(text.Length) + 1;
        _buffer = _pool.Rent(maxByteCount);

        var written = Encoding.UTF8.GetBytes(text, _buffer);
        _buffer[written] = 0; // Нативный null-терминатор
        Length = written;

        // Используем только Pinned, так как адрес нужен в 99% случаев.
        _handle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
    }

    /// <summary>
    /// byte buffer length without terminating \0
    /// </summary>
    public int Length { get; }

    public nint Pointer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _handle.AddrOfPinnedObject();
    }

    public void Dispose()
    {
        // Сначала освобождаем дескриптор, чтобы распинить память для GC
        if (_handle.IsAllocated)
        {
            _handle.Free();
        }

        // Возвращаем массив в пул
        if (_buffer != null)
        {
            _pool.Return(_buffer);
        }
    }
}
