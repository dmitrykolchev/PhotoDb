// <copyright file="AddImageToAlbumRequest.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.CryptoId;

namespace PhotoDb.Data.Models.Albums;

public class AddImageToAlbumRequest
{
    public Int64CryptoId ImageId { get; set; }
}

public class AddImagesToAlbumBatchRequest
{
    public List<Int64CryptoId> ImageIds { get; set; } = [];
}

