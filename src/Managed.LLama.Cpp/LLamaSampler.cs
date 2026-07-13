// <copyright file="LLamaSampler.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public unsafe class LLamaSampler : NativeObjectWrapper<llama_sampler>
{
    public LLamaSampler(llama_sampler* nativePointer) : base(nativePointer)
    {
    }

    public static LLamaSampler ChainInit(LLamaSamplerChainParams parameters)
    {
        var nativePointer = llama_sampler_chain_init(parameters._params);
        if (nativePointer is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler chain");
        }
        return new LLamaSampler(nativePointer);
    }

    public LLamaSampler AddGreedy()
    {
        var temp = llama_sampler_init_greedy();
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init greedy");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddSeed(uint seed = LLAMA_DEFAULT_SEED)
    {
        var temp = llama_sampler_init_dist(seed);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init dist");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddTopK(int k)
    {
        var temp = llama_sampler_init_top_k(k);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init TopK");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddTopP(float p, long minKeep)
    {
        var temp = llama_sampler_init_top_p(p, (nuint)minKeep);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init TopP");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddMinP(float p, long minKeep)
    {
        var temp = llama_sampler_init_min_p(p, (nuint)minKeep);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init MinP");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddTypical(float p, long minKeep)
    {
        var temp = llama_sampler_init_typical(p, (nuint)minKeep);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init typical");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddTemp(float t)
    {
        var temp = llama_sampler_init_temp(t);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init temp");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddTempExt(float t, float delta, float exponent)
    {
        var temp = llama_sampler_init_temp_ext(t, delta, exponent);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init temp ext");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddXtc(float probability, float threshold, long minKeep, uint seed)
    {
        var temp = llama_sampler_init_xtc(probability, threshold, (nuint)minKeep, seed);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init xtc");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddMirostatV2(uint seed, float tau, float eta)
    {
        var temp = llama_sampler_init_mirostat_v2(seed, tau, eta);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler mirostat v2");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddTopNSigma(float n)
    {
        var temp = llama_sampler_init_top_n_sigma(n);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init top n sigma");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddPenalties(int penaltyLastN, float penaltyRepeat, float penaltyFreq, float penaltyPresent)
    {
        var temp = llama_sampler_init_penalties(penaltyLastN, penaltyRepeat, penaltyFreq, penaltyPresent);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama sampler init penalties");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }

    public LLamaSampler AddLogitBias(int count, int token, float bias)
    {
        var lb = new llama_logit_bias()
        {
            token = token,
            bias = bias
        };
        var temp = llama_sampler_init_logit_bias(count, 1, &lb);
        if (temp is null)
        {
            throw new InvalidOperationException("Failed to initialize llama init logit bias");
        }
        llama_sampler_chain_add(SafeNative, temp);
        return this;
    }
    /// <summary>
    /// Sample and accept a token from the idx-th output of the last evaluation
    /// Shorthand for:
    ///    const auto * logits = llama_get_logits_ith(ctx, idx);
    ///    llama_token_data_array cur_p = { ... init from logits ... };
    ///    llama_sampler_apply(smpl, &cur_p);
    ///    auto token = cur_p.data[cur_p.selected].id;
    ///    llama_sampler_accept(smpl, token);
    ///    return token;
    /// Returns the sampled token
    /// </summary>
    /// <param name="context">LLama Context</param>
    /// <param name="index"></param>
    /// <returns></returns>
    public int Sample(LLamaContext context, int index)
    {
        return llama_sampler_sample(SafeNative, context.SafeNative, index);
    }

    protected override unsafe void FreeNativeResource(llama_sampler* pointer)
    {
        llama_sampler_free(pointer);
    }

}
