using System.Diagnostics;

namespace Managed.LibRaw;

public class OutputPropertyCollection
{
    private readonly RawFile _context;

    internal OutputPropertyCollection(RawFile context)
    {
        Debug.Assert(context != null);
        _context = context;
    }

    public unsafe int GetInt32(OutputProperty property)
    {
        return property switch
        {
            OutputProperty.BitsPerSample => _context.Native->@params.output_bps,
            _ => throw new ArgumentException("Invalid property", nameof(property)),
        };
    }
    public unsafe bool GetBoolean(OutputProperty property)
    {
        return property switch
        {
            OutputProperty.Tiff => _context.Native->@params.output_tiff != 0,
            _ => throw new ArgumentException("Invalid property", nameof(property)),
        };
    }

    public unsafe void SetValue(OutputProperty property, int value)
    {
        switch (property)
        {
            case OutputProperty.BitsPerSample:
                _context.Native->@params.output_bps = value;
                break;
            default:
                throw new ArgumentException("Invalid property", nameof(property));
        }
    }
    public unsafe void SetValue(OutputProperty property, bool value)
    {
        switch (property)
        {
            case OutputProperty.Tiff:
                _context.Native->@params.output_tiff = value ? 1 : 0;
                break;
            default:
                throw new ArgumentException("Invalid property", nameof(property));
        }
    }
}
