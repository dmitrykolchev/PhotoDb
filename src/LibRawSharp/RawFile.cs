using Managed.LibRaw.Native;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using static Managed.LibRaw.Native.Methods;

namespace Managed.LibRaw;

public sealed unsafe class RawFile : IDisposable
{
    private libraw_data_t* _data;
    private ThumbnailItemCollection? _thumbnails;

    internal RawFile(libraw_data_t* data)
    {
        Debug.Assert(data != null, "init_data should not be null");
        _data = data;
    }

    ~RawFile()
    {
        Dispose();
    }

    public int RawWidth => libraw_get_raw_width(_data);

    public int RawHeight => libraw_get_raw_height(_data);

    public int Width => libraw_get_iwidth(_data);

    public int Height => libraw_get_iheight(_data);

    public string UnpackFunction => Marshal.PtrToStringAnsi((nint)libraw_unpack_function_name(_data))!;

    public ProgressFlags Progress => (ProgressFlags)Native->progress_flags;

    internal libraw_data_t* Native => _data;

    public ThumbnailItemCollection Thumbnails
    {
        get
        {
            return _thumbnails ??= new ThumbnailItemCollection(this);
        }
    }

    public void Unpack()
    {
        LibRawException.ThrowIfFailed(libraw_unpack(_data));
    }

    public void DcrawProcess()
    {
        LibRawException.ThrowIfFailed(libraw_dcraw_process(_data));
    }

    public void WriteDcrawPpmTiff(string fileName, bool saveAsTiff = false, int output_bps = 8)
    {
        int count = Encoding.UTF8.GetByteCount(fileName);
        sbyte* buffer = stackalloc sbyte[count];
        _ = Encoding.UTF8.GetBytes(fileName.AsSpan(), new Span<byte>(buffer, count));
        Native->@params.output_tiff = saveAsTiff ? 1 : 0;
        Native->@params.output_bps = output_bps;
        LibRawException.ThrowIfFailed(libraw_dcraw_ppm_tiff_writer(_data, buffer));
    }

    /// <summary>
    /// Retrieves a thumbnail image from the Dcraw memory.
    /// </summary>
    /// <returns>The thumbnail image retrieved from the Dcraw memory.</returns>
    /// <remarks>Corresponds to the C API function: libraw_dcraw_make_mem_thumb</remarks>
    public void Dispose()
    {
        libraw_recycle(_data);
        libraw_close(_data);
        _data = null;
        GC.SuppressFinalize(this);
    }
}
