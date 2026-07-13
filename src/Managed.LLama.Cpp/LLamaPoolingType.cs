// <copyright file="" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using static Managed.LLama.Cpp.Native.llama_pooling_type;

namespace Managed.LLama.Cpp;

public enum LLamaPoolingType
{
    Unspecified = LLAMA_POOLING_TYPE_UNSPECIFIED,
    None = LLAMA_POOLING_TYPE_NONE,
    Mean = LLAMA_POOLING_TYPE_MEAN,
    Cls = LLAMA_POOLING_TYPE_CLS,
    Last = LLAMA_POOLING_TYPE_LAST,
    Rank =  LLAMA_POOLING_TYPE_RANK,
}
