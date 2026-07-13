// <copyright file="ImageLoader.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using SkiaSharp;

namespace PhotoDbProto;

internal class ImageLoader
{
    public static byte[] ScaleImage(MemoryStream inputStream, float maxDimension = 1176f, int quality = 75)
    {
        // 1. Декодируем исходный массив байт в структуру SkiaSharp
        using var codec = SKCodec.Create(inputStream);
        if (codec == null)
        {
            return inputStream.ToArray(); // Если файл поврежден или формат не поддерживается
        }

        // Получаем оригинальные размеры изображения
        var originalWidth = codec.Info.Width;
        var originalHeight = codec.Info.Height;

        var targetWidth = originalWidth;
        var targetHeight = originalHeight;

        // 2. Рассчитываем новые пропорциональные размеры
        if (originalWidth < maxDimension && originalHeight < maxDimension)
        {
            // Если изображение и так меньше лимита, возвращаем оригинал
            return inputStream.ToArray();
        }

        if (originalWidth > originalHeight)
        {
            targetHeight = (int)Math.Round(maxDimension / originalWidth * originalHeight);
            targetWidth = (int)maxDimension;
        }
        else
        {
            targetWidth = (int)Math.Round(maxDimension / originalHeight * originalWidth);
            targetHeight = (int)maxDimension;
        }

        // 3. Создаем информацию о новом изображении с сохранением цветового пространства
        var targetInfo = new SKImageInfo(targetWidth, targetHeight, codec.Info.ColorType, codec.Info.AlphaType, codec.Info.ColorSpace);

        // 4. Декодируем и масштабируем изображение с высоким качеством фильтрации (High/Lanczos)
        using var bitmap = new SKBitmap(targetInfo);
        using var originalBitmap = SKBitmap.Decode(codec);

        var samplingOptions = new SKSamplingOptions(SKCubicResampler.Mitchell);
        // Выполняем масштабирование методом ScalePixels
        originalBitmap.ScalePixels(bitmap, samplingOptions);

        //return bitmap.GetPixelSpan().ToArray();

        // 5. Кодируем результат обратно в JPEG-поток с заданным качеством
        using var image = SKImage.FromBitmap(bitmap);
        using var outputStream = new SKMemoryStream();

        // 6. Кодируем в JPEG и получаем SKData
        using var data = image.Encode(SKEncodedImageFormat.Jpeg, quality);
        if (data == null)
        {
            return inputStream.ToArray();
        }

        // 7. Преобразуем нативные данные SKData в обычный управляемый byte[]
        return data.ToArray();
    }
}
