// <copyright file="GCShield.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Managed.LLama.Cpp;

public readonly ref struct GCShield
{
    private readonly NativeObjectWrapperBase _wrappedObject;

    public GCShield(NativeObjectWrapperBase wrappedObject)
    {
        _wrappedObject = wrappedObject;

        // Атомарно инкрементируем счетчик ссылок перед входом в нативный код
        if (!_wrappedObject.IncrementReference())
        {
            throw new ObjectDisposedException(_wrappedObject.GetType().Name, "Cannot execute operation: object is disposed or disposing.");
        }
    }

    public void Dispose()
    {
        // Декрементируем счетчик и запускаем очистку, если был вызван явный Dispose
        _wrappedObject.DecrementReference();

        // Магический якорь для JIT-компилятора
        GC.KeepAlive(_wrappedObject);
    }
}
