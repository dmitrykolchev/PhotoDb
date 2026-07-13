using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Managed.LibRaw.Native;

public static unsafe partial class Methods
{
    [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int libraw_open_file(libraw_data_t* param0, [MarshalAs(UnmanagedType.LPStr)] string param1);

    [DllImport("libraw", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int libraw_open_wfile(libraw_data_t* param0, [MarshalAs(UnmanagedType.LPWStr)] string param1);
}