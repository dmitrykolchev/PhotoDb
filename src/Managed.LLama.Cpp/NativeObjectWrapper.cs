// <copyright file="NativeObjectWrapper.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.CompilerServices;

namespace Managed.LLama.Cpp;

public abstract class NativeObjectWrapperBase : IDisposable
{
    private nint _rawNativePointer;
    // Состояния объекта: >= 0 (активные операции), -1 (уничтожен)
    private int _referenceCount;
    private bool _disposeRequested;

    // Храним как void*, чтобы базовый класс не зависел от конкретного типа структуры
    protected nint RawNativePointer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _rawNativePointer;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private set => _rawNativePointer = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected NativeObjectWrapperBase(nint nativePointer)
    {
        if (nativePointer == nint.Zero)
        {
            throw new ArgumentNullException(nameof(nativePointer), "Native pointer cannot be null.");
        }
        RawNativePointer = nativePointer;
    }

    // Финализатор на случай, если пользователь вообще забыл про Dispose.
    // Наследуется от обычной очереди, но так как мы используем GcShield, 
    // риск Race Condition с нативным кодом полностью исключен.
    ~NativeObjectWrapperBase()
    {
        CleanUpInternal();
    }

    // Абстрактный метод, который должен реализовать каждый наследник для вызова своей free-функции
    protected abstract void FreeNativeResource(nint pointer);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool IncrementReference()
    {
        while (true)
        {
            var current = Volatile.Read(ref _referenceCount);
            if (current < 0 || _disposeRequested)
            {
                return false;
            }

            if (Interlocked.CompareExchange(ref _referenceCount, current + 1, current) == current)
            {
                return true;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void DecrementReference()
    {
        var remaining = Interlocked.Decrement(ref _referenceCount);

        if (remaining == 0 && _disposeRequested)
        {
            if (Interlocked.CompareExchange(ref _referenceCount, -1, 0) == 0)
            {
                CleanUpInternal();
            }
        }
    }

    public void Dispose()
    {
        _disposeRequested = true;

        if (Interlocked.CompareExchange(ref _referenceCount, -1, 0) == 0)
        {
            CleanUpInternal();
        }

        GC.SuppressFinalize(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CleanUpInternal()
    {
        // Метод потокобезопасен, выполнится строго один раз
        var ptr = Interlocked.Exchange(ref _rawNativePointer, nint.Zero);
        if (ptr != nint.Zero)
        {
            FreeNativeResource(ptr);
        }
    }
}

// Удобный строго типизированный промежуточный класс для наследников
public abstract unsafe class NativeObjectWrapper<TNative> : NativeObjectWrapperBase
    where TNative : unmanaged
{
    protected NativeObjectWrapper(TNative* nativePointer) : base((nint)nativePointer) { }

    // Публичное или internal свойство для быстрого доступа к типизированному указателю
    internal TNative* Native
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (TNative*)RawNativePointer;
    }

    internal TNative* SafeNative
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var ptr = RawNativePointer;
            if (ptr == nint.Zero)
            {
                throw new ObjectDisposedException(GetType().FullName, "The native resource has already been freed.");
            }
            return (TNative*)ptr;
        }
    }

    protected sealed override void FreeNativeResource(nint pointer)
    {
        FreeNativeResource((TNative*)pointer);
    }

    protected abstract void FreeNativeResource(TNative* pointer);
}
