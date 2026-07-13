using static Managed.LibRaw.Native.LibRaw_image_formats;

namespace Managed.LibRaw;


/// <summary>
/// Represents the image formats in LibRaw.
/// </summary>
/// <remarks>
/// Original C API enum: LibRaw_image_formats
/// </remarks>
public enum ImageType
{
    Jpeg = LIBRAW_IMAGE_JPEG,
    Bitmap = LIBRAW_IMAGE_BITMAP,
    JpegXl = LIBRAW_IMAGE_JPEGXL,
    H265 = LIBRAW_IMAGE_H265
}
