// <copyright file="AddTagResponse.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Text.Json.Serialization;

namespace PhotoDb.Data.Models.Images;

public class RemoveTagResponse
{
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = null!;
}
