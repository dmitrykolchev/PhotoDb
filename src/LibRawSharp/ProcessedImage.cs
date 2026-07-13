using Managed.LibRaw.Native;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static Managed.LibRaw.Native.Methods;

namespace Managed.LibRaw;

public sealed unsafe class ProcessedImage : IDisposable
{
    private libraw_processed_image_t* _data;

    internal ProcessedImage(libraw_processed_image_t* data)
    {
        Debug.Assert(data != null, "data should not be null");
        _data = data;
        GC.AddMemoryPressure(_data->data_size);
    }

    public ImageType ImageType => (ImageType)_data->type;

    public int Height => _data->height;

    public int Width => _data->width;

    public int Channels => _data->colors;

    public int BitsPerChannel => _data->bits;

    public int DataSize => (int)_data->data_size;

    public Span<T> AsSpan<T>()
    {
        fixed (void* pfirstData = &_data->data[0])
        {
            return new Span<T>(pfirstData, DataSize / Marshal.SizeOf<T>());
        }
    }

    ~ProcessedImage()
    {
        Dispose();
    }

    public void Dispose()
    {
        uint size = _data->data_size;
        libraw_dcraw_clear_mem(_data);
        GC.RemoveMemoryPressure(size);
        _data = null;
        GC.SuppressFinalize(this);
    }

    internal static unsafe ProcessedImage MakeDcrawMemoryThumbnail(RawFile context)
    {
        int errorCode;

        libraw_processed_image_t* image = libraw_dcraw_make_mem_thumb(context.Native, &errorCode);
        LibRawException.ThrowIfFailed(errorCode);
        if (image->width == 0)
        {
            image->width = context.Native->thumbnail.twidth;
        }
        if (image->height == 0)
        {
            image->height = context.Native->thumbnail.theight;
        }
        if (image->colors == 0)
        {
            image->colors = (ushort)context.Native->thumbnail.tcolors;
        }
        return new ProcessedImage(image); // need to dispose by user
    }
}
