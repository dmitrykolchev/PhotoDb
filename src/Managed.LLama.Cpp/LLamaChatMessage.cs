// <copyright file="LLamaChatMessage.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Diagnostics;
using System.Text;

namespace Managed.LLama.Cpp;

public sealed unsafe class LLamaChatMessage
{
    private static Encoding Encoding => Encoding.UTF8;

    internal LLamaChatMessage(string role, string content, IDictionary<string, ReadOnlyMemory<byte>> roleCache)
    {
        Role = role;
        Content = content;
        // Get bytes for role from cache
        if (!roleCache.TryGetValue(role, out var roleData))
        {
            // Convert role. Add one to length so there is a null byte at the end.
            var buffer = new byte[Encoding.GetByteCount(role) + 1];
            var encodedRoleLength = Encoding.GetBytes(role, buffer);
            Debug.Assert(buffer.Length == encodedRoleLength + 1);
            roleCache.Add(role, buffer);
            RoleData = buffer;
        }
        // Convert content. Add one to length so there is a null byte at the end.
        var contentData = new byte[Encoding.GetByteCount(content) + 1];
        var encodedContentLength = Encoding.GetBytes(content, contentData);
        Debug.Assert(contentData.Length == encodedContentLength + 1);
        ContentData = contentData;
    }

    public string Role { get; }
    public string Content { get; }

    internal ReadOnlyMemory<byte> RoleData { get; }
    internal ReadOnlyMemory<byte> ContentData { get; }
}
