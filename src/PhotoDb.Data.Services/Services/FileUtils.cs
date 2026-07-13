// <copyright file="FileUtils.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.Data.Services;

internal class FileUtils
{
    private static readonly HashSet<string> _ignoreDirs = new(StringComparer.OrdinalIgnoreCase)
    {
        "node_modules", ".git", "dist", "proc", "sys", "dev", "run", "var", "etc",
        "boot", "lib", "lib64", "bin", "sbin", "root", "tmp", "lost+found"
    };

    public static void FindImagesRecursivelySafe(string dir, List<FileInfo> fileList, HashSet<string> allowedExtensions)
    {
        if (!Directory.Exists(dir))
        {
            return;
        }
        try
        {
            foreach (var file in Directory.EnumerateFiles(dir, "*.*"))
            {
                try
                {
                    var ext = Path.GetExtension(file).ToLowerInvariant();
                    if (allowedExtensions.Contains(ext))
                    {
                        fileList.Add(new FileInfo(file));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not inspect file {file}: {ex.Message}");
                }
            }

            foreach (var subDir in Directory.EnumerateDirectories(dir))
            {
                try
                {
                    var dirName = Path.GetFileName(subDir);
                    if (_ignoreDirs.Contains(dirName))
                    {
                        continue;
                    }
                    FindImagesRecursivelySafe(subDir, fileList, allowedExtensions);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not traverse subdirectory {subDir}: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not enumerate elements in directory {dir}: {ex.Message}");
        }
    }
}
