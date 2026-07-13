// <copyright file="PhotosController.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PhotoDb.Data.Models.Albums;
using PhotoDb.Data.Models.Images;
using PhotoDb.Data.Models.Libraries;
using PhotoDb.Data.Services;
using PhotoLab.Hubs;
using Xobex.CryptoId;

namespace PhotoLab.Controllers;

[ApiController]
[Route("api")]
public class PhotosController : ControllerBase
{
    private readonly IHubContext<CommunicationHub> _hubContext;

    public PhotosController(IHubContext<CommunicationHub> hubContext)
    {
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
    }

    // ==========================================
    // LIBRARIES API ENDPOINTS
    // ==========================================

    [HttpGet("libraries")]
    public async Task<IActionResult> GetLibraries([FromServices] LibraryDataService lds)
    {
        return Ok(await lds.GetItemListAsync());
    }

    [HttpPost("libraries")]
    public async Task<IActionResult> CreateLibrary([FromBody] CreateLibraryRequest req, [FromServices] LibraryDataService lds)
    {
        if (string.IsNullOrWhiteSpace(req.Name) || string.IsNullOrWhiteSpace(req.Path))
        {
            return BadRequest(new { error = "Catalog Name and physical Disk Path are required parameters" });
        }
        if (!Directory.Exists(req.Path))
        {
            return BadRequest(new { error = $"Directory path doesn't exist on host disk: {req.Path}" });
        }
        return Ok(await lds.CreateLibraryAsync(req));
    }

    [HttpPut("libraries/{id}")]
    public async Task<IActionResult> UpdateLibrary([FromRoute] Int32CryptoId id, [FromBody] UpdateLibraryRequest req, [FromServices] LibraryDataService lds)
    {
        return Ok(await lds.UpdateLibraryAsync(id.Value, req));
    }

    [HttpDelete("libraries/{id}")]
    public async Task<IActionResult> DeleteLibrary([FromRoute] Int32CryptoId id, [FromServices] LibraryDataService lds)
    {
        await lds.DeleteLibraryAsync(id.Value);
        return Ok(new { success = true });
    }

    [HttpPost("libraries/{id}/scan")]
    public async Task<IActionResult> ScanLibrary([FromRoute] Int32CryptoId id, [FromServices] LibraryDataService lds)
    {
        return Ok(await lds.ScanLibraryAsync(id.Value));
    }

    [HttpGet("images")]
    public async Task<IActionResult> GetImages(
        [FromServices] ImageDataService ids,
        [FromQuery] Int32CryptoId libraryId,
        [FromQuery] Int32CryptoId albumId,
        [FromQuery] string? search,
        [FromQuery] int? minRating,
        [FromQuery] string[] flags,
        [FromQuery] string? tags,
        [FromQuery] string sortBy = "dateCreated",
        [FromQuery] string sortOrder = "desc",
        [FromQuery] int page = 1,
        [FromQuery] int limit = 24)
    {

        var result = await ids.GetImagesAsync(new GetImagesRequest()
        {
            LibraryId = libraryId,
            AlbumId = albumId,
            Search = search,
            MinRating = minRating,
            Flags = flags,
            Tags = tags,
            SortBy = sortBy,
            SortOrder = sortOrder,
            Page = page,
            Limit = limit
        });
        return Ok(result);
    }

    [HttpPut("images/{id}")]
    public async Task<IActionResult> UpdateImage([FromRoute] Int64CryptoId id, [FromBody] UpdateImageRequest req, [FromServices] ImageDataService ids)
    {
        var result = await ids.UpdateImageAsync(id.Value, req);
        return Ok(result);
    }

    [HttpGet("tags")]
    public async Task<IActionResult> GetTags([FromServices] TagDataService tds)
    {
        return Ok(await tds.GetTagsAsync());
    }

    [HttpPost("images/{id}/tags")]
    public async Task<IActionResult> AddTagToImage([FromRoute] Int64CryptoId id, [FromBody] AddTagRequest req, [FromServices] ImageDataService ids)
    {
        await _hubContext.Clients.All.SendAsync("messageReceived", "AddTagToImage: ", req.TagName);
        var result = await ids.AddTagToImageAsync(id.Value, req);
        return Ok(result);
    }

    [HttpDelete("images/{id}/tags/{tagName}")]
    public async Task<IActionResult> RemoveTagFromImage([FromRoute] Int64CryptoId id, string tagName, [FromServices] ImageDataService ids)
    {
        var result = await ids.RemoveTagFromImageAsync(id.Value, tagName);
        return Ok(result);
    }

    // ==========================================
    // ALBUMS & SINGLE IMAGE / EDIT ENDPOINTS
    // ==========================================

    [HttpGet("images/{id}")]
    public async Task<IActionResult> GetImage([FromRoute] Int64CryptoId id, [FromServices] ImageDataService ids)
    {
        var result = await ids.GetImageAsync(id.Value);
        return Ok(result);
    }

    [HttpPost("images/{id}/edit")]
    public async Task<IActionResult> EditImage([FromRoute] Int64CryptoId id, [FromBody] EditImageRequest req)
    {
        throw new NotImplementedException();
        //if (string.IsNullOrWhiteSpace(req.ImageBase64))
        //{
        //    return BadRequest(new { error = "imageBase64 is required." });
        //}

        //var img = await _context.Images.FindAsync(id);
        //if (img == null)
        //{
        //    return NotFound(new { error = "Image record not found" });
        //}

        //try
        //{
        //    // Parse base64 string
        //    var base64Data = req.ImageBase64;
        //    if (base64Data.Contains(','))
        //    {
        //        base64Data = base64Data[(base64Data.IndexOf(",") + 1)..];
        //    }

        //    var imageBytes = Convert.FromBase64String(base64Data);
        //    await System.IO.File.WriteAllBytesAsync(img.FilePath, imageBytes);

        //    // Recheck stats and metadata
        //    var fileInfo = new FileInfo(img.FilePath);
        //    img.Size = fileInfo.Length;
        //    img.DateCreated = DateTime.UtcNow; // Mark as modified

        //    // Re-run metadata extractor to get width and height
        //    var metadata = SimpleMetadataExtractor.ReadMetadataCore(img.FilePath);
        //    if (metadata.Width.HasValue && metadata.Height.HasValue)
        //    {
        //        img.Width = metadata.Width;
        //        img.Height = metadata.Height;
        //    }

        //    // Compute hash
        //    using var md5 = MD5.Create();
        //    var hashBytes = md5.ComputeHash(imageBytes.Length > 1024 * 1024 ? imageBytes.Take(1024 * 1024).ToArray() : imageBytes);
        //    img.Hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

        //    await _context.SaveChangesAsync();

        //    // Get tags and albums for response
        //    var tags = await _context.ImageTags
        //        .Where(it => it.ImageId == id)
        //        .Join(_context.Tags, it => it.TagId, t => t.Id, (it, t) => t.Name)
        //        .ToListAsync();

        //    var imgAlbumIds = await _context.AlbumImages
        //        .Where(ai => ai.ImageId == id)
        //        .Select(ai => ai.AlbumId)
        //        .ToListAsync();
        //    var imgAlbums = await _context.Albums
        //        .Where(a => imgAlbumIds.Contains(a.Id))
        //        .ToListAsync();

        //    return Ok(new
        //    {
        //        img.Id,
        //        img.LibraryId,
        //        img.FileName,
        //        img.FilePath,
        //        img.DateCreated,
        //        img.DateIndexed,
        //        img.Rating,
        //        img.Flag,
        //        img.Hash,
        //        img.Size,
        //        img.Width,
        //        img.Height,
        //        img.CameraModel,
        //        img.LensModel,
        //        img.FocalLength,
        //        img.Aperture,
        //        img.IsoSpeed,
        //        img.ExposureTime,
        //        img.ShootingDate,
        //        img.Description,
        //        Tags = tags,
        //        Albums = imgAlbums
        //    });
        //}
        //catch (Exception ex)
        //{
        //    return StatusCode(500, new { error = "Failed to save edited image: " + ex.Message });
        //}
    }

    [HttpGet("albums")]
    public async Task<IActionResult> GetAlbums([FromServices] AlbumDataService ads)
    {
        return Ok(await ads.GetAlbumsAsync());
    }

    [HttpPost("albums")]
    public async Task<IActionResult> CreateAlbum([FromBody] CreateAlbumRequest req, [FromServices] AlbumDataService ads)
    {
        var result = await ads.CreateAlbumAsync(req);
        return Ok(result);
    }

    [HttpPut("albums/{id}")]
    public async Task<IActionResult> RenameAlbum([FromRoute] Int32CryptoId id, [FromBody] CreateAlbumRequest req, [FromServices] AlbumDataService ads)
    {
        var result = await ads.RenameAlbumAsync((int)id, req);
        return Ok(result);
    }

    [HttpDelete("albums/{id}")]
    public async Task<IActionResult> DeleteAlbum([FromRoute] Int32CryptoId id, [FromServices] AlbumDataService ads)
    {
        await ads.DeleteAlbumAsync((int)id);
        return Ok(new { success = true });
    }

    [HttpPost("albums/{id}/images")]
    public async Task<IActionResult> AddImageToAlbum([FromRoute] Int32CryptoId id, [FromBody] AddImageToAlbumRequest req, [FromServices] AlbumDataService ads)
    {
        await ads.AddImageToAlbumAsync((int)id, (long)req.ImageId);
        return Ok(new { success = true });
    }

    [HttpDelete("albums/{id}/images/{imageId}")]
    public async Task<IActionResult> RemoveImageFromAlbum([FromRoute] Int32CryptoId id, [FromRoute] Int64CryptoId imageId, [FromServices] AlbumDataService ads)
    {
        await ads.RemoveImageFromAlbumAsync((int)id, (long)imageId);
        return Ok(new { success = true });
    }

    [HttpPost("albums/{id}/images/batch")]
    public async Task<IActionResult> AddImagesToAlbumBatch([FromRoute] Int32CryptoId id, [FromBody] AddImagesToAlbumBatchRequest req, [FromServices] AlbumDataService ads)
    {
        if (req.ImageIds == null || req.ImageIds.Count == 0)
        {
            return BadRequest(new { error = "imageIds array is required." });
        }

        var addedCount = await ads.AddImagesToAlbumAsync((int)id, req);
        return Ok(new { success = true, addedCount });
    }
}

