// <copyright file="GetImagesResponse.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Text.Json.Serialization;

namespace PhotoDb.Data.Models.Images;

public class GetImagesResponse
{
    [JsonPropertyName("images")]
    public IEnumerable<ImageListItem> Images { get; set; } = null!;

    [JsonPropertyName("totalCount")]
    public int Count { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}
