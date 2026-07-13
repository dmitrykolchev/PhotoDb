using static Managed.LibRaw.Native.LibRaw_errors;

namespace Managed.LibRaw;

public enum Errors
{
    Success = LIBRAW_SUCCESS,
    UnspecifiedError = LIBRAW_UNSPECIFIED_ERROR,
    FileUnsupported = LIBRAW_FILE_UNSUPPORTED,
    RequestForNonexistentImage = LIBRAW_REQUEST_FOR_NONEXISTENT_IMAGE,
    OutOfOrderCall = LIBRAW_OUT_OF_ORDER_CALL,
    NoThumbnail = LIBRAW_NO_THUMBNAIL,
    UnsupportedThumbnail = LIBRAW_UNSUPPORTED_THUMBNAIL,
    InputClosed = LIBRAW_INPUT_CLOSED,
    NotImplemented = LIBRAW_NOT_IMPLEMENTED,
    RequestForNonexistentThumbnail = LIBRAW_REQUEST_FOR_NONEXISTENT_THUMBNAIL,
    UnsufficientMemory = LIBRAW_UNSUFFICIENT_MEMORY,
    DataError = LIBRAW_DATA_ERROR,
    IoError = LIBRAW_IO_ERROR,
    CancelledByCallback = LIBRAW_CANCELLED_BY_CALLBACK,
    BadCrop = LIBRAW_BAD_CROP,
    TooBig = LIBRAW_TOO_BIG,
    MempoolOverflow = LIBRAW_MEMPOOL_OVERFLOW,
}

