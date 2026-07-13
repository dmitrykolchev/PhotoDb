namespace Managed.LibRaw;

/// <summary>
/// Represents the constructor flags used in LibRaw.
/// </summary>
/// <remarks>
/// Original C API enum: LibRaw_constructor_flags
/// </remarks>
[Flags]
public enum InitFlags : uint
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates that there should be no data error callback.
    /// </summary>
    NoDataErrCallback = 1 << 1,

    /// <summary>
    /// Indicates that there should be no data error callback for options.
    /// </summary>
    OptionsNoDataErrCallback = NoDataErrCallback
}
