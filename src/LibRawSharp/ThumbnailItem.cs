using Managed.LibRaw.Native;

namespace Managed.LibRaw;

public readonly struct ThumbnailItem
{
    internal ThumbnailItem(ref libraw_thumbnail_item_t item)
    {
        Format = (InternalThumnailFormat)item.tformat;
        Width = item.twidth;
        Height = item.theight;
        Flip = item.tflip;
        Length = item.tlength;
        Misc = item.tmisc;
        Offset = item.toffset;
    }

    public InternalThumnailFormat Format { get; }

    public int Width { get; }

    public int Height { get; }

    public int Flip { get; }

    public long Length { get; }

    public uint Misc { get; }

    public long Offset { get; }
}
