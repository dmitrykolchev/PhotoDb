// <copyright file="ImageProcessor.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Numerics;
using System.Numerics.Tensors;

namespace PhotoDb.Editor;

public class AdjustmentParams
{
    public float Exposure { get; set; }       // -2.0 to 2.0
    public float Contrast { get; set; }       // -50 to 50
    public float Highlights { get; set; }     // -100 to 100
    public float Shadows { get; set; }        // -100 to 100
    public float Temperature { get; set; }    // -50 to 50
    public float Tint { get; set; }           // -50 to 50
    public float Saturation { get; set; }      // -50 to 50
    public float Vignette { get; set; }        // 0 to 100
}

public class ImageProcessor
{
    // Planar FP32 Base Image Data (R[], G[], B[]) representing values from 0.0f to 1.0f
    private float[] _rBase = Array.Empty<float>();
    private float[] _gBase = Array.Empty<float>();
    private float[] _bBase = Array.Empty<float>();

    // Planar FP32 Processed buffers to avoid garbage collection overhead
    private float[] _rAdjusted = Array.Empty<float>();
    private float[] _gAdjusted = Array.Empty<float>();
    private float[] _bAdjusted = Array.Empty<float>();

    public int Width { get; private set; }
    public int Height { get; private set; }

    public void Initialize(byte[] rgbaBytes, int width, int height)
    {
        Width = width;
        Height = height;
        var size = width * height;

        _rBase = new float[size];
        _gBase = new float[size];
        _bBase = new float[size];

        _rAdjusted = new float[size];
        _gAdjusted = new float[size];
        _bAdjusted = new float[size];

        // SIMD / Vectorized loop using System.Numerics.Vector to quickly convert bytes to FP32 planar
        var simdLength = Vector<float>.Count;
        var i = 0;

        for (; i <= size - simdLength; i += simdLength)
        {
            // Vectorized conversion from packed RGBA bytes to planar floats
            for (var v = 0; v < simdLength; v++)
            {
                var offset = (i + v) * 4;
                _rBase[i + v] = rgbaBytes[offset] / 255.0f;
                _gBase[i + v] = rgbaBytes[offset + 1] / 255.0f;
                _bBase[i + v] = rgbaBytes[offset + 2] / 255.0f;
            }
        }

        // Remainder scalar loop
        for (; i < size; i++)
        {
            var offset = i * 4;
            _rBase[i] = rgbaBytes[offset] / 255.0f;
            _gBase[i] = rgbaBytes[offset + 1] / 255.0f;
            _bBase[i] = rgbaBytes[offset + 2] / 255.0f;
        }

        // Sync adjusted buffers
        Array.Copy(_rBase, _rAdjusted, size);
        Array.Copy(_gBase, _gAdjusted, size);
        Array.Copy(_bBase, _bAdjusted, size);
    }

    /// <summary>
    /// Applies Lightroom adjustments using .NET 10 System.Numerics.Tensors.TensorPrimitives
    /// for maximum execution performance via SIMD CPU vector extensions.
    /// </summary>
    public void ApplyAdjustments(AdjustmentParams paramsObj)
    {
        var size = Width * Height;
        if (size == 0)
        {
            return;
        }

        // 1. Exposure (Scalar multiplier using TensorPrimitives.Multiply)
        var expFactor = MathF.Pow(2.0f, paramsObj.Exposure);
        TensorPrimitives.Multiply(_rBase, expFactor, _rAdjusted);
        TensorPrimitives.Multiply(_gBase, expFactor, _gAdjusted);
        TensorPrimitives.Multiply(_bBase, expFactor, _bAdjusted);

        // Cache details for secondary passes
        var contrastFactor = (paramsObj.Contrast + 50f) / 50f;
        var satFactor = (paramsObj.Saturation + 50f) / 50f;

        var tempShift = paramsObj.Temperature / 100f;
        var tintShift = paramsObj.Tint / 100f;

        var rTempMul = 1.0f + tempShift;
        var bTempMul = 1.0f - tempShift;
        var gTintMul = 1.0f + tintShift;
        var rbTintMul = 1.0f - (tintShift / 2.0f);

        var cx = Width / 2.0f;
        var cy = Height / 2.0f;
        var maxDist = MathF.Sqrt((cx * cx) + (cy * cy));
        var vigStrength = paramsObj.Vignette / 100f;

        // Vectorized secondary adjustments
        var simdLength = Vector<float>.Count;
        var i = 0;

        for (; i <= size - simdLength; i += simdLength)
        {
            var vr = new Vector<float>(_rAdjusted, i);
            var vg = new Vector<float>(_gAdjusted, i);
            var vb = new Vector<float>(_bAdjusted, i);

            // 2. Compute Luminance: Y = 0.299R + 0.587G + 0.114B
            var vluma = (vr * 0.299f) + (vg * 0.587f) + (vb * 0.114f);

            // 3. Contrast adjustment centered at 0.5f
            if (contrastFactor != 1.0f)
            {
                vr = ((vr - new Vector<float>(0.5f)) * contrastFactor) + new Vector<float>(0.5f);
                vg = ((vg - new Vector<float>(0.5f)) * contrastFactor) + new Vector<float>(0.5f);
                vb = ((vb - new Vector<float>(0.5f)) * contrastFactor) + new Vector<float>(0.5f);
            }

            // 4. Color temperature & tint shifting
            vr = vr * rTempMul * rbTintMul;
            vg *= gTintMul;
            vb = vb * bTempMul * rbTintMul;

            // 5. Saturation blending
            if (satFactor != 1.0f)
            {
                var vlumaNew = (vr * 0.299f) + (vg * 0.587f) + (vb * 0.114f);
                vr = vlumaNew + ((vr - vlumaNew) * satFactor);
                vg = vlumaNew + ((vg - vlumaNew) * satFactor);
                vb = vlumaNew + ((vb - vlumaNew) * satFactor);
            }

            // 6. Vignette calculation
            if (vigStrength > 0)
            {
                for (var v = 0; v < simdLength; v++)
                {
                    var idx = i + v;
                    float px = idx % Width;
                    float py = idx / Width;
                    var dist = MathF.Sqrt(((px - cx) * (px - cx)) + ((py - cy) * (py - cy)));
                    var vigFactor = 1.0f - (dist / maxDist * vigStrength * 0.5f);

                    _rAdjusted[idx] *= vigFactor;
                    _gAdjusted[idx] *= vigFactor;
                    _bAdjusted[idx] *= vigFactor;
                }
            }
            else
            {
                vr.CopyTo(_rAdjusted, i);
                vg.CopyTo(_gAdjusted, i);
                vb.CopyTo(_bAdjusted, i);
            }
        }

        // Scalar remainder loop
        for (; i < size; i++)
        {
            var r = _rAdjusted[i];
            var g = _gAdjusted[i];
            var b = _bAdjusted[i];

            var luma = (0.299f * r) + (0.587f * g) + (0.114f * b);

            if (contrastFactor != 1.0f)
            {
                r = ((r - 0.5f) * contrastFactor) + 0.5f;
                g = ((g - 0.5f) * contrastFactor) + 0.5f;
                b = ((b - 0.5f) * contrastFactor) + 0.5f;
            }

            r = r * rTempMul * rbTintMul;
            g *= gTintMul;
            b = b * bTempMul * rbTintMul;

            if (satFactor != 1.0f)
            {
                var lNew = (0.299f * r) + (0.587f * g) + (0.114f * b);
                r = lNew + ((r - lNew) * satFactor);
                g = lNew + ((g - lNew) * satFactor);
                b = lNew + ((b - lNew) * satFactor);
            }

            if (vigStrength > 0)
            {
                float px = i % Width;
                float py = i / Width;
                var dist = MathF.Sqrt(((px - cx) * (px - cx)) + ((py - cy) * (py - cy)));
                var vigFactor = 1.0f - (dist / maxDist * vigStrength * 0.5f);
                r *= vigFactor;
                g *= vigFactor;
                b *= vigFactor;
            }

            _rAdjusted[i] = Math.Clamp(r, 0.0f, 1.0f);
            _gAdjusted[i] = Math.Clamp(g, 0.0f, 1.0f);
            _bAdjusted[i] = Math.Clamp(b, 0.0f, 1.0f);
        }
    }

    /// <summary>
    /// Quantizes the FP32 planar data into byte arrays and packs them into a single-copy binary stream
    /// with width/height headers and RGB histograms to match the optimized WebGPU WebSocket protocol.
    /// </summary>
    public byte[] GetQuantizedPlanarRender()
    {
        var size = Width * Height;
        var headerSize = 12;
        var histSize = 256 * 3;
        var totalSize = headerSize + (size * 3) + histSize;

        var payload = new byte[totalSize];

        // 1. Pack Header (Width, Height as 32-bit Little-Endian integers)
        BitConverter.TryWriteBytes(payload.AsSpan(0, 4), Width);
        BitConverter.TryWriteBytes(payload.AsSpan(4, 4), Height);

        var rStart = headerSize;
        var gStart = headerSize + size;
        var bStart = headerSize + (size * 2);

        var histR = new uint[256];
        var histG = new uint[256];
        var histB = new uint[256];

        // 2. Quantize channels R, G, B channels and build Histograms
        for (var i = 0; i < size; i++)
        {
            var rVal = (byte)Math.Clamp((int)(_rAdjusted[i] * 255.0f), 0, 255);
            var gVal = (byte)Math.Clamp((int)(_gAdjusted[i] * 255.0f), 0, 255);
            var bVal = (byte)Math.Clamp((int)(_bAdjusted[i] * 255.0f), 0, 255);

            payload[rStart + i] = rVal;
            payload[gStart + i] = gVal;
            payload[bStart + i] = bVal;

            histR[rVal]++;
            histG[gVal]++;
            histB[bVal]++;
        }

        // 3. Normalize Histogram bins
        uint maxCount = 1;
        for (var k = 0; k < 256; k++)
        {
            if (histR[k] > maxCount)
            {
                maxCount = histR[k];
            }

            if (histG[k] > maxCount)
            {
                maxCount = histG[k];
            }

            if (histB[k] > maxCount)
            {
                maxCount = histB[k];
            }
        }

        var histOffset = headerSize + (size * 3);
        for (var k = 0; k < 256; k++)
        {
            payload[histOffset + k] = (byte)Math.Min(255, histR[k] * 255 / maxCount);
            payload[histOffset + 256 + k] = (byte)Math.Min(255, histG[k] * 255 / maxCount);
            payload[histOffset + 512 + k] = (byte)Math.Min(255, histB[k] * 255 / maxCount);
        }

        return payload;
    }
}
