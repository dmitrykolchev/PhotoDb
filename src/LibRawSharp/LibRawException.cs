using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using static Managed.LibRaw.Native.Methods;

namespace Managed.LibRaw;

public class LibRawException : Exception
{
    public LibRawException(Errors error) : base($"LibRaw returned an error: {error}")
    {
        Error = error;
    }

    public LibRawException(Errors error, Exception innerException) : base($"LibRaw returned an error: {error}", innerException)
    {
        Error = error;
    }

    public LibRawException(string message, Errors error) : base(message)
    {
        Error = error;
    }

    public LibRawException(string message) : base(message)
    {
        Error = Errors.UnspecifiedError;
    }

    public Errors Error { get; }

    public unsafe string ErrorText => Marshal.PtrToStringAnsi((nint)libraw_strerror((int)Error))!;

    [DoesNotReturn]
    public static LibRawException ThrowOnError(Errors error)
    {
        throw new LibRawException(error);
    }

    [DoesNotReturn]
    public static void ThrowOnError(Errors error, string message)
    {
        throw new LibRawException(message, error);
    }

    internal static void ThrowIfFailed(int error)
    {
        if (error == (int)Errors.Success)
        {
            return;
        }
        _ = ThrowOnError((Errors)error);
    }
}
