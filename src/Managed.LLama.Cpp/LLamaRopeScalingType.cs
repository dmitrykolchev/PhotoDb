// <copyright file="" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>
using static Managed.LLama.Cpp.Native.llama_rope_scaling_type;

namespace Managed.LLama.Cpp;

public enum LLamaRopeScalingType
{
    Unspecified = LLAMA_ROPE_SCALING_TYPE_UNSPECIFIED,
    None = LLAMA_ROPE_SCALING_TYPE_NONE,
    Linear = LLAMA_ROPE_SCALING_TYPE_LINEAR,
    Yarn = LLAMA_ROPE_SCALING_TYPE_YARN,
    LongRope = LLAMA_ROPE_SCALING_TYPE_LONGROPE,
}
