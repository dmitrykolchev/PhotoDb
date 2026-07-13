// <copyright file="TagListItem.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.Data.Models.Tags;

public class TagListItem
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int ImageCount { get; set; }
}
