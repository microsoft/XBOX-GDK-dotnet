// Raw interop types for XTaskQueue extension APIs and XGameRuntimeInit.h.
// Sources: XTaskQueue.h, XGameRuntimeInit.h (GDK edition 260404).

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>
/// Where <c>XGameRuntimeInitializeWithOptions</c> should load the game configuration from.
/// Mirrors <c>XGameRuntimeGameConfigSource</c> from XGameRuntimeInit.h.
/// </summary>
internal enum XGameRuntimeGameConfigSource : uint
{
    Default = 0,
    Inline  = 1,
    File    = 2,
}

/// <summary>
/// Options passed to <c>XGameRuntimeInitializeWithOptions</c>.
/// Mirrors <c>struct XGameRuntimeOptions</c> from XGameRuntimeInit.h.
/// </summary>
/// <remarks>
/// Layout on x64: 4-byte enum at offset 0, 4 bytes implicit padding, 8-byte pointer at offset 8.
/// Total: 16 bytes.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct XGameRuntimeOptions
{
    /// <summary>How to locate the game configuration (Default / Inline / File).</summary>
    public XGameRuntimeGameConfigSource GameConfigSource;

    /// <summary>
    /// Null-terminated UTF-8 string. Interpretation depends on <see cref="GameConfigSource"/>:
    /// for <see cref="XGameRuntimeGameConfigSource.Inline"/> the XML content itself; for
    /// <see cref="XGameRuntimeGameConfigSource.File"/> the path to the config file. Ignored (may be
    /// null) when <see cref="XGameRuntimeGameConfigSource.Default"/>.
    /// </summary>
    public IntPtr GameConfig; // const char*: caller-managed UTF-8 unmanaged memory
}
