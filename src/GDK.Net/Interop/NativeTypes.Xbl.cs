// Blittable mirrors of the XSAPI (Xbox Live Services) core types -- xsapi-c\types_c.h and
// xbox_live_global_c.h, GDK edition 260404. Each Xbox Live service keeps its own type mirrors in a
// NativeTypes.Xbl<Service>.cs partner (NativeTypes.XblProfile.cs, NativeTypes.XblAchievements.cs,
// ...).
//
// Every struct here is laid out exactly as the C++ header declares it. Two platform details fix
// the layouts and must not be second-guessed:
//
//   * A GDK title compiles with HC_PLATFORM == HC_PLATFORM_GDK (httpClient\config.h picks it from
//     _GAMING_DESKTOP / _GAMING_XBOX). XblInitArgs therefore has *only* `queue` and `scid`:
//     `localStoragePath` is HC_PLATFORM_WIN32, and the Android/iOS/external fields are excluded
//     too. Adding them would silently corrupt the SCID pointer.
//   * C++ `bool` is one byte and `time_t` is a signed 64-bit value on both x64 and arm64, so they
//     map to `byte` and `long`. Default sequential layout then reproduces the native padding.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>
/// Mirrors <c>XblInitArgs</c> as it is declared for <c>HC_PLATFORM_GDK</c>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XblInitArgs
{
    /// <summary>Optional queue for XSAPI's internal work. Null means "use a thread-pool queue".</summary>
    internal IntPtr Queue;

    /// <summary>Required, case-sensitive, null-terminated UTF-8 Service Configuration ID.</summary>
    internal IntPtr Scid;
}

/// <summary>Mirrors <c>XblConfigSetting</c> from xbox_live_global_c.h.</summary>
internal enum XblConfigSetting : uint
{
    ThisCodeNeedsToBeRemoved = 0,
}

/// <summary>Mirrors <c>XblServiceCallRoutedArgs</c>' fixed prefix. Only the fields the projection reads.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblServiceCallRoutedArgs
{
    internal IntPtr Call;
    internal byte* FullResponseFormatted;
}
