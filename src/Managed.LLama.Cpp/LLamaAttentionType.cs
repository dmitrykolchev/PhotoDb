// <copyright file="LLamaAttentionType.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using static Managed.LLama.Cpp.Native.llama_attention_type;

namespace Managed.LLama.Cpp;

public enum LLamaAttentionType
{
    Unspecified = LLAMA_ATTENTION_TYPE_UNSPECIFIED,
    Causal = LLAMA_ATTENTION_TYPE_CAUSAL,
    NonCausal = LLAMA_ATTENTION_TYPE_NON_CAUSAL,
}

