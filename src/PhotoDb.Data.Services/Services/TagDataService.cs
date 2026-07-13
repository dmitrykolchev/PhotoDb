// <copyright file="TagDataService.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using PhotoDb.Data.Models.Tags;
using Xobex.Cryptography.Abstractions;

namespace PhotoDb.Data.Services;

public sealed class TagDataService : DataServiceBase
{
    private readonly ICryptoIdEncoder<int> _int32encoder;

    public TagDataService(PhotoDbContextBase context, ICryptoIdEncoder<int> int32encoder) : base(context)
    {
        ArgumentNullException.ThrowIfNull(int32encoder);
        _int32encoder = int32encoder;
    }

    public async Task<List<TagListItem>> GetTagsAsync()
    {
        var tagCounts = await Context.ImageTag
            .GroupBy(it => it.TagId)
            .Select(g => new { TagId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.TagId, x => x.Count);

        var tagsList = await Context.TagItem.AsNoTracking().ToListAsync();

        var resultList = tagsList.Select(t => new TagListItem()
        {
            Id = _int32encoder.Encode(t.Id),
            Name = t.Name,
            ImageCount = tagCounts.TryGetValue(t.Id, out var count) ? count : 0
        })
        .OrderByDescending(r => r.ImageCount)
        .ToList();

        return resultList;
    }
}
