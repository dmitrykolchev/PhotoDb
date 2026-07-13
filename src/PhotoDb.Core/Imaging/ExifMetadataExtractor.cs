// <copyright file="SimpleMetadataExtractor.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Globalization;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Exif.Makernotes;
using SkiaSharp;

namespace PhotoDb.Imaging;

public class ExifMetadataExtractor
{
    public static ImageMetadataCore ReadMetadataCore(string imagePath)
    {
        var width = 0;
        var height = 0;
        try
        {
            using var codec = SKCodec.Create(imagePath);
            if (codec != null)
            {
                width = codec.Info.Width;
                height = codec.Info.Height;
            }
        }
        catch { }

        try
        {
            // 1. Читаем все метаданные из файла
            var directories = ImageMetadataReader.ReadMetadata(imagePath);

            // 2. Получаем нужные директории EXIF
            var subIfdDir = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            var ifd0Dir = directories.OfType<ExifIfd0Directory>().FirstOrDefault();

            // 3. Извлекаем параметры с безопасной проверкой на null
            var cameraMake = ifd0Dir?.GetDescription(ExifIfd0Directory.TagMake);

            // Camera Model (Модель камеры)
            var cameraModel = ifd0Dir?.GetDescription(ExifIfd0Directory.TagModel);

            // Shooting Date/Time (Дата/время съемки)
            var shootingDate = subIfdDir?.GetDescription(ExifSubIfdDirectory.TagDateTimeOriginal);

            // Exposure Time (Выдержка)
            var exposureTime = subIfdDir?.GetDescription(ExifSubIfdDirectory.TagExposureTime);

            // Aperture Value (Диафрагма)
            var aperture = subIfdDir?.GetDescription(ExifSubIfdDirectory.TagAperture);

            // ISO Speed (Чувствительность ISO)
            var isoSpeed = subIfdDir?.GetDescription(ExifSubIfdDirectory.TagIsoEquivalent)
                ?? subIfdDir?.GetDescription(ExifSubIfdDirectory.TagIsoSpeed);

            // Focal Length (Фокусное расстояние)
            var focalLength = subIfdDir?.GetDescription(ExifSubIfdDirectory.TagFocalLength);

            // Lens Model (Модель объектива)
            var lensModel = ExtractLensModel(directories);

            return new ImageMetadataCore
            {
                Width = width,
                Height = height,
                CameraMake = cameraMake,
                CameraModel = cameraModel,
                LensModel = lensModel,
                FocalLength = focalLength,
                Aperture = aperture,
                IsoSpeed = isoSpeed,
                ExposureTime = exposureTime,
                ShootingDate = ParseDateTime(shootingDate)
            };
        }
        catch
        {
            return new ImageMetadataCore { Width = width, Height = height };
        }
    }

    private static DateTime? ParseDateTime(string? shootingDate)
    {
        if (DateTime.TryParseExact(shootingDate, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result))
        {
            return result;
        }
        return null;
    }

    public static string? ExtractLensModel(IReadOnlyList<MetadataExtractor.Directory> directories)
    {
        // 1. Проверяем стандартный EXIF (актуально для 90% современных камер)
        var subIfd = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
        var lens = subIfd?.GetDescription(ExifSubIfdDirectory.TagLensModel);
        if (!string.IsNullOrEmpty(lens))
        {
            return lens;
        }

        // 2. Если пусто и это Canon, смотрим в его Makernote
        var canon = directories.OfType<CanonMakernoteDirectory>().FirstOrDefault();
        // У Canon описание типа объектива часто находится в настройках камеры
        lens = canon?.GetDescription(CanonMakernoteDirectory.TagLensModel);
        if (!string.IsNullOrEmpty(lens))
        {
            return lens;
        }

        // 3. Если это Nikon, смотрим в его Makernote
        var nikon = directories.OfType<NikonType2MakernoteDirectory>().FirstOrDefault();
        lens = nikon?.GetDescription(NikonType2MakernoteDirectory.TagLens);
        if (!string.IsNullOrEmpty(lens))
        {
            return lens;
        }

        // 3. Если это Sony, смотрим в его Makernote
        var sony = directories.OfType<SonyType1MakernoteDirectory>().FirstOrDefault();
        lens = sony?.GetDescription(SonyType1MakernoteDirectory.TagLensSpec);
        if (!string.IsNullOrEmpty(lens))
        {
            return lens;
        }

        return null;
    }
}
