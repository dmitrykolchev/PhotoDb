using Managed.LibRaw.Native;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using static Managed.LibRaw.Native.Methods;

namespace Managed.LibRaw;

public class Factory
{
    public static unsafe ReadOnlyCollection<string> SupportedCameras
    {
        get
        {
            var count = libraw_cameraCount();
            sbyte** list = libraw_cameraList();
            string[] temp = new string[count];
            for (int index = 0; index < count; index++)
            {
                temp[index] = Marshal.PtrToStringAnsi((nint)list[index])!;
            }
            return new ReadOnlyCollection<string>(temp);
        }
    }

    public static unsafe string Version
    {
        get
        {
            return Marshal.PtrToStringAnsi((nint)libraw_version())!;
        }
    }

    public static Version VersionNumber
    {
        get
        {
            var version = libraw_versionNumber();
            int major = version >> 16;
            int minor = version >> 8;
            int patch = version & 0xFF;
            return new(major, minor, patch);
        }
    }

    public static unsafe RawFile Open(string filePath, InitFlags flags = InitFlags.None)
    {
        libraw_data_t* init_data = libraw_init((uint)flags);

        Errors error = Environment.OSVersion.Platform switch
        {
            PlatformID.Win32NT => (Errors)libraw_open_wfile(init_data, filePath),
            _ => (Errors)libraw_open_file(init_data, filePath),
        };
        if (error != Errors.Success)
        {
            libraw_recycle(init_data);
            libraw_close(init_data);
            LibRawException.ThrowOnError(error, $"Failed opening file: {filePath}");
        }
        return new RawFile(init_data);
    }

    public static unsafe RawFile Load(ReadOnlySpan<byte> data, InitFlags flags = InitFlags.None)
    {
        libraw_data_t* init_data = libraw_init((uint)flags);
        fixed (byte* p = data)
        {
            Errors error = (Errors)libraw_open_buffer(init_data, p, (nuint)data.Length);
            if (error != Errors.Success)
            {
                libraw_recycle(init_data);
                libraw_close(init_data);
                LibRawException.ThrowOnError(error, "Failed opening buffer");
            }
        }
        return new RawFile(init_data);
    }
}
