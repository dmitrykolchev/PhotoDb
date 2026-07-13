// <copyright file="AddTagRequest.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.Data.Models.Images;

public class AddTagRequest
{
    public string TagName { get; set; } = null!;
}
