// <copyright file="GgmlLogLevel.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using static Managed.LLama.Cpp.Native.ggml_log_level;

namespace Managed.LLama.Cpp;

/// <summary>
/// Logging levels
/// </summary>
public enum GgmlLogLevel
{
    None = GGML_LOG_LEVEL_NONE,
    Debug = GGML_LOG_LEVEL_DEBUG,
    Info = GGML_LOG_LEVEL_INFO,
    Warn = GGML_LOG_LEVEL_WARN,
    Error = GGML_LOG_LEVEL_ERROR,
    /// <summary>
    /// continue previous log
    /// </summary>
    Cont = GGML_LOG_LEVEL_CONT, 
}
