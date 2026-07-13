// <copyright file="" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using static Managed.LLama.Cpp.Native.llama_split_mode;

namespace Managed.LLama.Cpp;

public enum LLamaSplitMode
{
    None = LLAMA_SPLIT_MODE_NONE,
    Layer = LLAMA_SPLIT_MODE_LAYER,
    Row = LLAMA_SPLIT_MODE_ROW,
    Tensor = LLAMA_SPLIT_MODE_TENSOR
}
