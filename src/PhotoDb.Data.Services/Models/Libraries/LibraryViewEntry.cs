// <copyright file="LibraryViewEntry.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.Data.Models.Libraries;

public sealed class LibraryViewEntry
{
    public string Id { get; set; } = null!;

    public short State { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public DateTime LastScanDate { get; set; }

    public string? Description { get; set; }

    public int ImageCount { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
}
