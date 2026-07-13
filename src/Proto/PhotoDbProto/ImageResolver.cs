// <copyright file="ImageResolver.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace PhotoDbProto;

internal class ImageResolver
{
    private PatternMatchingResult? _results;

    public ImageResolver(string searchDirectory, string includePatterns, string? excludePatterns = null)
    {
        IncludePatterns = includePatterns;
        ExcludePatterns = excludePatterns;
        SearchDirectory = searchDirectory;
    }

    public string SearchDirectory { get; }

    public string IncludePatterns { get; }

    public string? ExcludePatterns { get; }

    public bool HasMatches { get; private set; }

    public IEnumerable<FilePatternMatch> Files => HasMatches ? _results!.Files : [];

    public void Execute()
    {
        Matcher matcher = new();
        matcher.AddIncludePatterns(IncludePatterns.Split(';', StringSplitOptions.RemoveEmptyEntries));
        if (ExcludePatterns != null)
        {
            matcher.AddExcludePatterns(ExcludePatterns.Split(';', StringSplitOptions.RemoveEmptyEntries));
        }
        _results = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(SearchDirectory)));
        HasMatches = _results.HasMatches;
    }
}
