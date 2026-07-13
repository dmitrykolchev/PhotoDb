// <copyright file="LLamaTemplate.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Buffers;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public sealed unsafe class LLamaTemplate
{
    private readonly ConcurrentDictionary<string, ReadOnlyMemory<byte>> _roleCache = new();
    private readonly List<LLamaChatMessage> _messages = [];
    private readonly byte[] _template;
    private bool _dirty;
    private llama_chat_message[]? _nativeMessages;
    private byte[] _result = [];
    private int _resultLength;

    internal LLamaTemplate(sbyte* template)
    {
        if (template == null)
        {
            _template = [];
            return;
        }
        var unmanagedSpan = MemoryMarshal.CreateReadOnlySpanFromNullTerminated((byte*)template);
        if (unmanagedSpan.IsEmpty)
        {
            _template = [];
            return;
        }
        _template = unmanagedSpan.ToArray();
    }

    public LLamaTemplate(string template)
    {
        if (string.IsNullOrEmpty(template))
        {
            _template = [];
            return;
        }
        _template = Encoding.GetBytes(template);
    }

    private static Encoding Encoding => Encoding.UTF8;

    public int Count => _messages.Count;

    /// <summary>
    /// Whether to end the prompt with the token(s) that indicate the start of an assistant message.
    /// </summary>
    public bool AddAssistant
    {
        get => field;
        set
        {
            if (value != field)
            {
                _dirty = true;
                field = value;
            }
        }
    }

    public LLamaTemplate Add(string role, string content)
    {
        var message = new LLamaChatMessage(role, content, _roleCache);
        return Add(message);
    }

    public LLamaTemplate Add(LLamaChatMessage message)
    {
        _messages.Add(message);
        _dirty = true;
        return this;
    }

    public LLamaTemplate RemoveAt(int index)
    {
        _messages.RemoveAt(index);
        _dirty = true;
        return this;
    }

    public LLamaTemplate Clear()
    {
        _messages.Clear();
        _resultLength = 0;
        _result = [];
        _dirty = true;
        return this;
    }

    public ReadOnlySpan<byte> Apply()
    {
        if (_dirty)
        {
            _dirty = false;
            using var batch = new BatchDisposable();
            var totalInputBytes = CreateNativeMessages(batch);
            // Get an array that's twice as large as the amount of input, hopefully that's large enough!
            var output = ArrayPool<byte>.Shared.Rent(Math.Max(32, totalInputBytes * 2));
            try
            {
                // Run templater and discover true length
                var outputLength = ApplyInternal(_nativeMessages.AsSpan(0, Count), output);

                // if we have a return code of -1, the template was not found.
                if (outputLength == -1)
                {
                    if (_template != null)
                    {
                        throw new MissingTemplateException(Encoding.GetString(_template));
                    }
                    else
                    {
                        throw new MissingTemplateException();
                    }
                }

                // If length was too big for output buffer run it again
                if (outputLength > output.Length)
                {
                    // Array was too small, rent another one that's exactly the size needed
                    ArrayPool<byte>.Shared.Return(output, true);
                    output = ArrayPool<byte>.Shared.Rent(outputLength);

                    // Run again, but this time with an output that is definitely large enough
                    ApplyInternal(_nativeMessages.AsSpan(0, Count), output);
                }

                // Grow result buffer if necessary
                if (_result.Length < outputLength)
                {
                    Array.Resize(ref _result, Math.Max(_result.Length * 2, outputLength));
                }

                // Copy to result buffer
                output.AsSpan(0, outputLength).CopyTo(_result);
                _resultLength = outputLength;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(output, true);
            }
        }
        return _result.AsSpan(0, _resultLength);

        unsafe int ApplyInternal(ReadOnlySpan<llama_chat_message> messages, byte[] output)
        {
            fixed (void* templatePtr = _template)
            fixed (void* outputPtr = output)
            fixed (llama_chat_message* messagesPtr = messages)
            {
                return llama_chat_apply_template((sbyte*)templatePtr,
                    messagesPtr, (nuint)messages.Length,
                    AddAssistant,
                    (sbyte*)outputPtr, output.Length);
            }
        }
    }

    private int CreateNativeMessages(BatchDisposable batch)
    {
        EnsureCapacity();
        var messages = CollectionsMarshal.AsSpan(_messages);
        var totalInputBytes = 0;
        for (var index = 0; index < Count; index++)
        {
            ref var message = ref messages[index];
            totalInputBytes += message.RoleData.Length + message.ContentData.Length;

            // Pin byte arrays in place
            var r = message.RoleData.Pin();
            batch.Add(r);
            var c = message.ContentData.Pin();
            batch.Add(c);

            _nativeMessages[index] = new llama_chat_message
            {
                role = (sbyte*)r.Pointer,
                content = (sbyte*)c.Pointer
            };
        }
        return totalInputBytes;
    }

    [MemberNotNull(nameof(_nativeMessages))]
    private void EnsureCapacity()
    {
        if (_nativeMessages == null)
        {
            _nativeMessages = new llama_chat_message[_messages.Count];
        }
        else if (_nativeMessages.Length < _messages.Count)
        {
            Array.Resize(ref _nativeMessages, _messages.Count);
        }
    }

    public override string ToString()
    {
        return Encoding.GetString(_template);
    }
}
