// <copyright file="LLamaBatch.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Managed.LLama.Cpp;

public readonly ref struct LLamaBatch
{
    private readonly int[] _tokens = null!;
    private readonly float[] _embeddings = null!;
    private readonly int _sequenceId;

    public LLamaBatch(int[] tokens, int sequenceId = 0)
    {
        ArgumentNullException.ThrowIfNull(tokens);
        _tokens = tokens;
        _sequenceId = sequenceId;
    }

    public LLamaBatch(float[] embeddings)
    {
        ArgumentNullException.ThrowIfNull(embeddings);
        _embeddings = embeddings;
    }

    public LLamaBatch(int token, int sequenceId = 0)
    {
        _tokens = [token];
        _sequenceId = sequenceId;
    }

    public int SequenceId => _sequenceId;

    public ReadOnlySpan<int> Tokens => _tokens;

    public ReadOnlySpan<float> Embeddings => _embeddings;

    public int Length => _tokens is not null ? _tokens.Length : _embeddings.Length;
}
