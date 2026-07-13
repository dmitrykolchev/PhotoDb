// <copyright file="UpdateLibraryRequest.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.Data.Models.Libraries;

public sealed class UpdateLibraryRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
