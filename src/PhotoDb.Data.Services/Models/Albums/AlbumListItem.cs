// <copyright file="AlbumListItem.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.CryptoId;

namespace PhotoDb.Data.Models.Albums;

public class AlbumListItem
{
    public Int32CryptoId Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Created { get; set; }
    public int ImageCount { get; set; }
}
