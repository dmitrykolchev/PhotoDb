// <copyright file="LLamaTokenAttribute.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using static Managed.LLama.Cpp.Native.llama_token_attr;

namespace Managed.LLama.Cpp;

public enum LLamaTokenAttribute
{
    Undefined = LLAMA_TOKEN_ATTR_UNDEFINED,
    Unknown = LLAMA_TOKEN_ATTR_UNKNOWN,
    Unused = LLAMA_TOKEN_ATTR_UNUSED,
    Normal = LLAMA_TOKEN_ATTR_NORMAL,
    Control = LLAMA_TOKEN_ATTR_CONTROL,
    UserDefined = LLAMA_TOKEN_ATTR_USER_DEFINED,
    Byte = LLAMA_TOKEN_ATTR_BYTE,
    Normalized = LLAMA_TOKEN_ATTR_NORMALIZED,
    LStrip = LLAMA_TOKEN_ATTR_LSTRIP,
    RStrip = LLAMA_TOKEN_ATTR_RSTRIP,
    SingleWord = LLAMA_TOKEN_ATTR_SINGLE_WORD,
}
