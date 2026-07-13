// <copyright file="LLamaFlashAttentionType.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using static Managed.LLama.Cpp.Native.llama_flash_attn_type;

namespace Managed.LLama.Cpp;

public enum LLamaFlashAttentionType
{
    Auto = LLAMA_FLASH_ATTN_TYPE_AUTO,
    Disabled = LLAMA_FLASH_ATTN_TYPE_DISABLED,
    Enabled = LLAMA_FLASH_ATTN_TYPE_ENABLED,
}
