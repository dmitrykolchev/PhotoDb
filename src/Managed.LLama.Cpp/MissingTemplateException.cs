// <copyright file="MissingTemplateException.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Managed.LLama.Cpp;

public abstract class RuntimeError : Exception
{
    public RuntimeError(string message) : base(message)
    {
    }
}

/// <summary>
/// `llama_decode` return a non-zero status code
/// </summary>
public sealed class MissingTemplateException : RuntimeError
{
    /// <inheritdoc />
    public MissingTemplateException()
        : base("llama_chat_apply_template failed: template not found")
    {
    }

    /// <inheritdoc />
    public MissingTemplateException(string message)
        : base($"llama_chat_apply_template failed: template not found for '{message}'")
    {
    }
}
