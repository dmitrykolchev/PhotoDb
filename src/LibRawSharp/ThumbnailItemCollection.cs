using Managed.LibRaw.Native;
using System.Collections;
using static Managed.LibRaw.Native.Methods;

namespace Managed.LibRaw;

public unsafe class ThumbnailItemCollection : IEnumerable<ThumbnailItem>
{
    private readonly RawFile _context;

    internal ThumbnailItemCollection(RawFile context)
    {
        _context = context;
    }

    public int Count => _context.Native->thumbs_list.thumbcount;

    public ThumbnailItem this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index should be between 0 and {Count - 1}");
            }
            libraw_thumbnail_item_t item = _context.Native->thumbs_list.thumblist[index];
            return new ThumbnailItem(ref item);
        }
    }

    public void UnpackThumbnail()
    {
        LibRawException.ThrowIfFailed(libraw_unpack_thumb(_context.Native));
    }

    public void UnpackThumbnail(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), $"Index should be between 0 and {Count - 1}");
        }
        LibRawException.ThrowIfFailed(libraw_unpack_thumb_ex(_context.Native, index));
    }

    public ProcessedImage Export()
    {
        UnpackThumbnail();
        return ProcessedImage.MakeDcrawMemoryThumbnail(_context);
    }

    public ProcessedImage Export(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), $"Index should be between 0 and {Count - 1}");
        }
        UnpackThumbnail(index);
        return ProcessedImage.MakeDcrawMemoryThumbnail(_context);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<ThumbnailItem> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return this[i];
        }
    }
}
