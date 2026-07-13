// <copyright file="ImageFilter.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.CryptoId;

namespace PhotoDb.Data.Models.Images;

public class GetImagesRequest
{
    public Int32CryptoId LibraryId { get; set; }
    public Int32CryptoId AlbumId { get; set; }
    public string? Search { get; set; }
    public int? MinRating { get; set; }
    public string[]? Flags { get; set; }
    public string? Tags { get; set; }
    public string SortBy { get; set; } = "created";
    public string SortOrder { get; set; } = "desc";
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 24;
}
