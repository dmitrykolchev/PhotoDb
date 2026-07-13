// <copyright file="LLamaVocabulary.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public unsafe class LLamaVocabulary : NativeObjectWrapper<llama_vocab>
{
    internal LLamaVocabulary(llama_vocab* native) : base(native)
    {
    }

    protected override void FreeNativeResource(llama_vocab* pointer)
    {
    }

    public int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_vocab_n_tokens(SafeNative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetAddBos()
    {
        return llama_vocab_get_add_bos(SafeNative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetAddEos()
    {
        return llama_vocab_get_add_eos(SafeNative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetAddSep()
    {
        return llama_vocab_get_add_sep(SafeNative);
    }

    public int Bos
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_vocab_bos(SafeNative);
    }

    public int Eos
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_vocab_eos(SafeNative);
    }

    public int Eot
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_vocab_eot(SafeNative);
    }

    public int Sep
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_vocab_sep(SafeNative);
    }

    public int Nl
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_vocab_nl(SafeNative);
    }

    public int Pad
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_vocab_pad(SafeNative);
    }

    public int Mask
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => llama_vocab_mask(SafeNative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEOG(int token)
    {
        return llama_vocab_is_eog(SafeNative, token);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsControl(int token)
    {
        return llama_vocab_is_control(SafeNative, token);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LLamaTokenAttribute GetAttribute(int token)
    {
        return (LLamaTokenAttribute)llama_vocab_get_attr(SafeNative, token);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string? GetText(int token)
    {
        return Utf8StringMarshaller.ConvertToManaged((byte*)llama_vocab_get_text(SafeNative, token));
    }

    public int GetTokenId(string text, bool parseSpecial = true)
    {
        using var utf8text = new PinnedUtf8String(text);
        var tokens = stackalloc int[2];
        var tokensCount = llama_tokenize(SafeNative, (sbyte*)utf8text.Pointer, utf8text.Length, tokens, 2, false, parseSpecial);
        if (tokensCount == 1)
        {
            return tokens[0];
        }
        return -1;
    }

    public string? TokenToPiece(int token, int lstrip, bool special)
    {
        var buffer = stackalloc byte[256];
        var result = llama_token_to_piece(SafeNative, token, (sbyte*)buffer, 256, lstrip, special);
        if (result < 0)
        {
            throw new InvalidOperationException("failed to convert token to piece");
        }
        return Utf8StringMarshaller.ConvertToManaged(buffer);
    }
}
