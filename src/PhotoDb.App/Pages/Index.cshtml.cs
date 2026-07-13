// <copyright file="Index.cshtml.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoDb.Data.Models.Images;
using PhotoDb.Data.Services;
using Xobex.CryptoId;

namespace PhotoLab.Pages;

public class IndexModel : PageModel
{
    public IndexModel()
    {
    }

    public void OnGet()
    {
        // Initial layout deliver
    }

    public async Task<IActionResult> OnGetImagesAsync(
        [FromServices] ImageDataService ids,
        Int32CryptoId libraryId,
        string? search,
        int? minRating,
        [FromQuery] string[] flags,
        string? tags,
        string sortBy = "dateCreated",
        string sortOrder = "desc",
        int page = 1,
        int limit = 24)
    {
        var result = ids.GetImagesAsync(new GetImagesRequest
        {
            LibraryId = libraryId,
            Search = search,
            MinRating = minRating,
            Flags = flags,
            Tags = tags,
            SortBy = sortBy,
            SortOrder = sortOrder,
            Page = page,
            Limit = limit
        });
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnPostScanLibraryAsync(Int32CryptoId libraryId, [FromServices] LibraryDataService lds)
    {
        var result = await lds.ScanLibraryAsync(libraryId.Value);
        return new JsonResult(result);
    }
}
