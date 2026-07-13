// <copyright file="ScanLibraryResponse.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Text.Json.Serialization;

namespace PhotoDb.Data.Models.Libraries;

public class ScanLibraryResponse
{
    [JsonPropertyName("added")]
    public int AddedCount { get; set; }

    [JsonPropertyName("updated")]
    public int UpdatedCount { get; set; }

    [JsonPropertyName("removed")]
    public int RemovedCount { get; set; }

    [JsonPropertyName("total")]
    public int TotalCount { get; set; }

    [JsonPropertyName("elapsedMs")]
    public double ElapsedMilliseconds { get; set; }
}
