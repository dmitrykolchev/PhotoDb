// <copyright file="LocalFileProviderMiddleware.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Security.Cryptography;
using System.Text;
using PhotoDb.Data.Services;
using PhotoDb.Imaging;

namespace PhotoLab.Middleware;

public class LocalFileProviderMiddleware
{
    private readonly RequestDelegate _next;
    private static List<string>? _cachedLibraryPaths;
    private static DateTime _cacheExpiration = DateTime.MinValue;
    private static readonly Lock _lock = new();

    public LocalFileProviderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, LibraryDataService libraryDataService)
    {
        // Intercept requests matching /api/local-file endpoint
        if (context.Request.Path.Equals("/api/local-file", StringComparison.OrdinalIgnoreCase))
        {
            string? filePath = context.Request.Query["filePath"];
            if (string.IsNullOrWhiteSpace(filePath))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Parameter 'filePath' is required");
                return;
            }

            // Resolve path to standard full absolute paths to sanitize input directories
            var resolvedPath = Path.GetFullPath(filePath);

            // Security checks: Query active tracked libraries inside SQLite with lightweight 5-second caching
            List<string> activeLibraryPaths;
            lock (_lock)
            {
                if (_cachedLibraryPaths != null && DateTime.UtcNow < _cacheExpiration)
                {
                    activeLibraryPaths = _cachedLibraryPaths;
                }
                else
                {
                    activeLibraryPaths = null!;
                }
            }

            if (activeLibraryPaths == null)
            {
                var libs = await libraryDataService.GetItemListAsync();
                activeLibraryPaths = libs.Select(lib => Path.GetFullPath(lib.Path)).ToList();
                lock (_lock)
                {
                    _cachedLibraryPaths = activeLibraryPaths;
                    _cacheExpiration = DateTime.UtcNow.AddSeconds(5);
                }
            }

            var isAuthorized = activeLibraryPaths.Any(allowedBase =>
            {
                // Match path starts with allowed catalog directories base path boundary check
                return resolvedPath.StartsWith(allowedBase, StringComparison.OrdinalIgnoreCase);
            });

            if (!isAuthorized)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Requested file is not located inside any cataloged library folder");
                return;
            }

            if (!File.Exists(resolvedPath))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("File not found");
                return;
            }

            // Check if cached thumbnail is requested
            var isThumbnailRequested = !string.IsNullOrEmpty(context.Request.Query["thumb"]);
            if (isThumbnailRequested)
            {
                try
                {
                    var cacheKey = GetCacheKey(resolvedPath);
                    var cacheDir = Path.Combine(Directory.GetCurrentDirectory(), "thumbs_cache");
                    if (!Directory.Exists(cacheDir))
                    {
                        Directory.CreateDirectory(cacheDir);
                    }

                    var cachePath = Path.Combine(cacheDir, cacheKey + ".jpg");

                    if (!File.Exists(cachePath))
                    {
                        ImageResizer.CreateThumbnail(resolvedPath, cachePath, 318);
                    }

                    if (File.Exists(cachePath))
                    {
                        context.Response.ContentType = "image/jpeg";
                        context.Response.Headers.CacheControl = "public, max-age=31536000"; // Cache forever (1 year)
                        await context.Response.SendFileAsync(cachePath);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // Fallback gracefully to serving the full original image if any error occurs
                    System.Diagnostics.Debug.WriteLine($"Thumbnail generation failed: {ex.Message}");
                }
            }

            // Match appropriate browser MIME content types
            var ext = Path.GetExtension(resolvedPath).ToLowerInvariant();
            var contentType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream"
            };

            context.Response.ContentType = contentType;
            context.Response.Headers.CacheControl = "public, max-age=86400"; // 24-hours caching optimization

            // Direct performance stream pipe
            await context.Response.SendFileAsync(resolvedPath);
            return;
        }

        // Move forward to next requests
        await _next(context);
    }

    private static string GetCacheKey(string filePath)
    {
        var fi = new FileInfo(filePath);
        var length = fi.Exists ? fi.Length : 0;
        var lastWrite = fi.Exists ? fi.LastWriteTimeUtc.Ticks : 0;

        using var sha256 = SHA256.Create();
        var rawKey = $"{filePath}_{length}_{lastWrite}";
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawKey));
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}
