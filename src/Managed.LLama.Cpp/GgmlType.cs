// <copyright file="GgmlType.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using static Managed.LLama.Cpp.Native.ggml_type;
namespace Managed.LLama.Cpp;

public enum GgmlType
{
    F32 = GGML_TYPE_F32,
    F16 = GGML_TYPE_F16,
    Q4_0 = GGML_TYPE_Q4_0,
    Q4_1 = GGML_TYPE_Q4_1,
    Q5_0 = GGML_TYPE_Q5_0,
    Q5_1 = GGML_TYPE_Q5_1,
    Q8_0 = GGML_TYPE_Q8_0,
    Q8_1 = GGML_TYPE_Q8_1,
    Q2_K = GGML_TYPE_Q2_K,
    Q3_K = GGML_TYPE_Q3_K,
    Q4_K = GGML_TYPE_Q4_K,
    Q5_K = GGML_TYPE_Q5_K,
    Q6_K = GGML_TYPE_Q6_K,
    Q8_K = GGML_TYPE_Q8_K,
    IQ2_XXS = GGML_TYPE_IQ2_XXS,
    IQ2_XS = GGML_TYPE_IQ2_XS,
    IQ3_XXS = GGML_TYPE_IQ3_XXS,
    IQ1_S = GGML_TYPE_IQ1_S,
    IQ4_NL = GGML_TYPE_IQ4_NL,
    IQ3_S = GGML_TYPE_IQ3_S,
    IQ2_S = GGML_TYPE_IQ2_S,
    IQ4_XS = GGML_TYPE_IQ4_XS,
    I8 = GGML_TYPE_I8,
    I16 = GGML_TYPE_I16,
    I32 = GGML_TYPE_I32,
    I64 = GGML_TYPE_I64,
    F64 = GGML_TYPE_F64,
    IQ1_M = GGML_TYPE_IQ1_M,
    BF16 = GGML_TYPE_BF16,
    TQ1_0 = GGML_TYPE_TQ1_0,
    TQ2_0 = GGML_TYPE_TQ2_0,
    MXFP4 = GGML_TYPE_MXFP4,
    NVFP4 = GGML_TYPE_NVFP4,
    Q1_0 = GGML_TYPE_Q1_0,
    COUNT = GGML_TYPE_COUNT,
}
