// <copyright file="MtmdContext.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using Managed.LLama.Cpp.Native;
using static Managed.LLama.Cpp.Native.Methods;

namespace Managed.LLama.Cpp;

public sealed unsafe class MtmdContext : NativeObjectWrapper<mtmd_context>
{
    public MtmdContext(mtmd_context* nativePointer) : base(nativePointer)
    {
    }

    public static string? DefaultMarker => field ??= GetDefaultMarker();

    /// <summary>
    /// Whether the current model supports vision input
    /// </summary>
    public bool SupportsVision => mtmd_support_vision(SafeNative);

    /// <summary>
    /// whether the current model supports audio input
    /// </summary>
    public bool SupportsAudio => mtmd_support_audio(SafeNative);

    /// <summary>
    /// get audio sample rate in Hz, for example 16000 for Whisper
    /// returns -1 if audio is not supported
    /// </summary>
    public int AudioSampleRate => mtmd_get_audio_sample_rate(SafeNative);

    /// <summary>
    /// The current marker string
    /// </summary>
    public string? Marker => field ??= GetMarker();

    /// <summary>
    /// whether the current model use M-RoPE for llama_decode
    /// </summary>
    public bool UsesMropeForDecode => mtmd_decode_use_mrope(SafeNative);

    public static string? GetDefaultMarker()
    {
        var markerPtr = mtmd_default_marker();
        return Utf8StringMarshaller.ConvertToManaged((byte*)markerPtr);
    }

    /// <summary>
    /// Returns the current marker string
    /// </summary>
    /// <returns></returns>
    public string? GetMarker()
    {
        var markerPtr = mtmd_get_marker(SafeNative);
        return Utf8StringMarshaller.ConvertToManaged((byte*)markerPtr);
    }

    public static MtmdContext InitFromFile(string path, LLamaModel model, MtmdContextParams parameters)
    {
        using var utf8Path = new PinnedUtf8String(path);
        var nativePtr = mtmd_init_from_file((sbyte*)utf8Path.Pointer, model.SafeNative, parameters._params);
        if (nativePtr is null)
        {
            throw new InvalidOperationException("Failed to initialize mtmd context");
        }
        return new MtmdContext(nativePtr);
    }

    /// <summary>
    /// helper function to construct a mtmd_bitmap from a file
    /// it calls mtmd_helper_bitmap_init_from_buf() internally
    /// this function is thread-safe
    /// </summary>
    /// <param name="path"></param>
    /// <param name="placeholder"></param>
    /// <returns>returns nullptr on failure</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public MtmdBitmap LoadBitmap(string path, bool placeholder = false)
    {
        using var utf8Path = new PinnedUtf8String(path);
        var result = mtmd_helper_bitmap_init_from_file(SafeNative, (sbyte*)utf8Path.Pointer, placeholder);
        if (result.bitmap is null)
        {
            throw new InvalidOperationException("Failed to load mtmd bitmap");
        }
        Debug.Assert(result.video_ctx == null);
        return new MtmdBitmap(result.bitmap);
    }

    /// <summary>
    /// // helper function to construct a mtmd_bitmap from a buffer containing a file
    /// supported formats:
    ///     image: formats supported by stb_image: jpg, png, bmp, gif, etc.
    ///     audio: formats supported by miniaudio: wav, mp3, flac
    /// note:
    ///   - for now, video input is only supported via C++ helper functions
    ///   - audio files will be auto-detected based on magic bytes
    ///   - output bitmap will have FNV hash as the ID
    /// this function is thread-safe
    /// </summary>
    /// <param name="data"></param>
    /// <param name="placeholder"></param>
    /// <returns>returns nullptr on failure</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public MtmdBitmap LoadBitmap(ReadOnlySpan<byte> data, bool placeholder = false)
    {
        fixed (byte* dataPtr = data)
        {
            var result = mtmd_helper_bitmap_init_from_buf(SafeNative, dataPtr, (nuint)data.Length, placeholder);
            if (result.bitmap is null)
            {
                throw new InvalidOperationException("Failed to load mtmd bitmap");
            }
            Debug.Assert(result.video_ctx == null);
            return new MtmdBitmap(result.bitmap);
        }
    }

    public MtmdInputChunks Tokenize(string text, bool addSpecial, bool parseSpecial, IEnumerable<MtmdBitmap> bitmaps, out int length)
    {
        var inputChunks = MtmdInputChunks.Create();
        using var utf8text = new PinnedUtf8String(text);
        var inputText = new mtmd_input_text
        {
            add_special = addSpecial,
            parse_special = parseSpecial,
            text = (sbyte*)utf8text.Pointer
        };
        var bitmapPtrs = stackalloc mtmd_bitmap*[bitmaps.Count()];
        var count = 0;
        foreach (var bitmap in bitmaps)
        {
            bitmapPtrs[count++] = bitmap.Native;
        }
        length = mtmd_tokenize(SafeNative, inputChunks.Native, &inputText, bitmapPtrs, (nuint)count);
        return inputChunks;
    }

    public int EvalChunks(LLamaContext context, MtmdInputChunks chunks, int position, int sequenceId, int batch, bool logits)
    {
        var newPosition = 0;
        var status = mtmd_helper_eval_chunks(SafeNative, context.SafeNative, chunks.SafeNative, position, sequenceId, batch, logits, &newPosition);
        if (status != 0)
        {
            throw new InvalidOperationException("Failed to eval chunks");
        }
        return newPosition;
    }

    protected override unsafe void FreeNativeResource(mtmd_context* pointer)
    {
        mtmd_free(pointer);
    }
}
