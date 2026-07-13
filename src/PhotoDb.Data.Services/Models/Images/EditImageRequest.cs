// <copyright file="EditImageRequest.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.Data.Models.Images;

public class EditImageRequest
{
    public string ImageBase64 { get; set; } = string.Empty;
}
