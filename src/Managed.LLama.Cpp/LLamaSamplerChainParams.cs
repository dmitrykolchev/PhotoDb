// <copyright file="" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public class LLamaSamplerChainParams
{
    internal llama_sampler_chain_params _params;

    public static LLamaSamplerChainParams Default()
    {
        return new LLamaSamplerChainParams()
        {
            _params = llama_sampler_chain_default_params()
        };
    }
}
