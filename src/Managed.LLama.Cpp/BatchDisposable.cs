// <copyright file="BatchDisposable.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace Managed.LLama.Cpp;

public sealed class BatchDisposable : IDisposable
{
    private readonly ConcurrentStack<IDisposable> _disposables = new();
    private readonly ConcurrentStack<GCHandle> _handles = new();
    private int _disposed;

    // Добавить обычный IDisposable объект
    public void Add<T>(T disposable) where T : IDisposable
    {
        ObjectDisposedException.ThrowIf(_disposed == 1, this);
        if (disposable != null)
        {
            _disposables.Push(disposable);
        }
    }

    // Добавить дескриптор закрепленной памяти
    public void Add(GCHandle handle)
    {
        ObjectDisposedException.ThrowIf(_disposed == 1, this);
        if (handle.IsAllocated)
        {
            _handles.Push(handle);
        }
    }

    public void Dispose()
    {
        // Потокобезопасная проверка на повторный вызов
        if (Interlocked.Exchange(ref _disposed, 1) == 1)
        {
            return;
        }

        // 1. Освобождаем закрепленную память (Unpin)
        while (_handles.TryPop(out var handle))
        {
            if (handle.IsAllocated)
            {
                handle.Free();
            }
        }

        // 2. Вызываем Dispose у управляемых объектов
        while (_disposables.TryPop(out var disposable))
        {
            try
            {
                disposable.Dispose();
            }
            catch (Exception ex)
            {
                // Логируем или подавляем исключения, чтобы один плохой Dispose 
                // не прервал освобождение остальных ресурсов
                System.Diagnostics.Debug.WriteLine($"Ошибка при Dispose: {ex.Message}");
            }
        }
    }
}
