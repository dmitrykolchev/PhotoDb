// <copyright file="UpdateImageRequest.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Text.Json.Serialization;

namespace PhotoDb.Data.Models.Images;

public class UpdateImageRequest
{
    public int? Rating { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ImageFlag? Flag { get; set; }
    public string? Description { get; set; }
}
