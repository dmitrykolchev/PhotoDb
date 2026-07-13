// <copyright file="KnownPerson.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.AI.FaceDetection;

public class KnownPerson
{
    public int Id { get; }

    public string Name { get; }

    public float AdaptiveThreshold { get; } = 0.6f;

    public List<float[]> ReferenceEmbeddings { get; }

    public KnownPerson(int id, string name, float threshold)
    {
        Id = id;
        Name = name;
        AdaptiveThreshold = threshold;
        ReferenceEmbeddings = [];
    }
}
