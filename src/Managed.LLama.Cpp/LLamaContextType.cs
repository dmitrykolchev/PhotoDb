// <copyright file="LLamaContextType.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using static Managed.LLama.Cpp.Native.llama_context_type;

namespace Managed.LLama.Cpp;

public enum LLamaContextType
{
    /// <summary>
    /// Next-Token Prediction. Стандартный (один токен за шаг)
    /// </summary>
    Default = LLAMA_CONTEXT_TYPE_DEFAULT,
    /// <summary>
    /// Multi-Token Prediction. Спекулятивный (несколько токенов за шаг)
    /// </summary>
    Mtp = LLAMA_CONTEXT_TYPE_MTP,
}
