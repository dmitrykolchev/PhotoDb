// <copyright file="LLamaLogger.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public delegate void GgmlLogCallback(GgmlLogLevel logLevel, string text, object? userData);

public unsafe class GgmlLogger
{
    private static readonly Stream _stdout = Console.OpenStandardOutput();

    private static readonly delegate* unmanaged[Cdecl]<ggml_log_level, sbyte*, void*, void> _logHandlerPtr;
    private static GCHandle _userDataHandle;
    private static GgmlLogCallback? _currentCallback;

    static GgmlLogger()
    {
        _logHandlerPtr = &NativeLogHandler;
    }

    public static void InitializeLogger()
    {
        ggml_log_set(_logHandlerPtr, null);
    }

    public static void SetCustomLogger(GgmlLogCallback logCallback, object? userData = null)
    {
        ArgumentNullException.ThrowIfNull(logCallback);
        ReleaseCustomLogger();
        _currentCallback = logCallback;
        void* nativeUserData = null;
        if (userData is not null)
        {
            _userDataHandle = GCHandle.Alloc(userData, GCHandleType.Normal);
            nativeUserData = (void*)GCHandle.ToIntPtr(_userDataHandle);
        }
        ggml_log_set(_logHandlerPtr, nativeUserData);
    }

    public static void ReleaseCustomLogger()
    {
        ggml_log_set(_logHandlerPtr, null);
        _currentCallback = null;
        if (_userDataHandle.IsAllocated)
        {
            _userDataHandle.Free();
            _userDataHandle = default;
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static void NativeLogHandler(ggml_log_level level, sbyte* textPtr, void* userData)
    {
        if (textPtr is null)
        {
            return;
        }

        try
        {
            var message = MemoryMarshal.CreateReadOnlySpanFromNullTerminated((byte*)textPtr);
            if (_currentCallback != null)
            {
                var text = Marshal.PtrToStringUTF8((nint)textPtr, message.Length);
                object? managedUserData = null;
                if (userData is not null)
                {
                    // Восстанавливаем дескриптор из указателя и получаем сам объект
                    var handle = GCHandle.FromIntPtr((IntPtr)userData);
                    managedUserData = handle.Target;
                }
                _currentCallback((GgmlLogLevel)level, text, managedUserData);
            }
            else
            {
                _stdout.Write(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в логгере: {ex.Message}");
        }
    }
}
