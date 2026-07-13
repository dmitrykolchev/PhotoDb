// <copyright file="FaceBox.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using SkiaSharp;

namespace PhotoDb.AI.FaceDetection;

public readonly record struct FaceBox(float X1, float Y1, float X2, float Y2, float Score)
{
    public float Width => X2 - X1;
    public float Height => Y2 - Y1;
    public SKRect ToSkRect()
    {
        return new(X1, Y1, X2, Y2);
    }
}

public readonly record struct Landmark(float X, float Y);

public readonly record struct DetectedFace(FaceBox Box, Landmark[] Landmarks);

