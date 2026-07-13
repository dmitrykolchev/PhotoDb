// <copyright file="MatchResult.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.AI.FaceDetection;

public abstract record MatchResult
{
    public record KnownMatch(string Name, float Similarity) : MatchResult;
    public record ClusterMatch(int ClusterId, float Similarity) : MatchResult;
    public record NewClusterCreated(int ClusterId) : MatchResult;
    public record NoiseIgnored() : MatchResult;
}
