// Raw interop types for XSystem, XThread, XError, XGame, XLauncher, and XDisplay.
// Sources: XSystem.h, XDisplay.h, XError.h, XThread.h, XGame.h, XLauncher.h (GDK edition 260404).

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>
/// Mirrors <c>XVersion</c> from XGameRuntimeTypes.h.
/// The struct is a union of four 16-bit fields and a single 64-bit value.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 8)]
internal struct XVersion
{
    [FieldOffset(0)] public ushort Major;
    [FieldOffset(2)] public ushort Minor;
    [FieldOffset(4)] public ushort Build;
    [FieldOffset(6)] public ushort Revision;
    [FieldOffset(0)] public ulong Value;
}

/// <summary>
/// Mirrors <c>struct XSystemAnalyticsInfo</c> from XSystem.h.
/// Family (char[64]) is at offset 16; Form (char[64]) is at offset 80.
/// Access both via the unsafe helpers on <see cref="GDK.Net.SystemInfo.GameSystem"/>.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 144)]
internal struct XSystemAnalyticsInfo
{
    [FieldOffset(0)] public XVersion OsVersion;
    [FieldOffset(8)] public XVersion HostingOsVersion;
    // Offsets 16–79: char family[64]
    // Offsets 80–143: char form[64]
    // Accessed via pointer arithmetic in the idiomatic layer.
}

/// <summary>Mirrors <c>struct XSystemRuntimeInfo</c> from XSystem.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XSystemRuntimeInfo
{
    public XVersion RuntimeVersion;
    public XVersion AvailableVersion;
}

/// <summary>Mirrors <c>struct XDisplayHdrModeInfo</c> from XDisplay.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XDisplayHdrModeInfo
{
    public float MinToneMapLuminance;
    public float MaxToneMapLuminance;
    public float MaxFullFrameToneMapLuminance;
}

/// <summary>Mirrors <c>enum class XSystemDeviceType</c> from XSystem.h.</summary>
internal enum XSystemDeviceType : uint
{
    Unknown              = 0x00,
    Pc                   = 0x01,
    XboxOne              = 0x02,
    XboxOneS             = 0x03,
    XboxOneX             = 0x04,
    XboxOneXDevkit       = 0x05,
    XboxScarlettLockhart = 0x06,
    XboxScarlettAnaconda = 0x07,
    XboxScarlettDevkit   = 0x08,
}

/// <summary>Mirrors <c>enum class XSystemHandleType</c> from XSystem.h.</summary>
internal enum XSystemHandleType : uint
{
    AppCaptureScreenshotStream = 0x00,
    DisplayTimeoutDeferral     = 0x01,
    GameSaveContainer          = 0x02,
    GameSaveProvider           = 0x03,
    GameSaveUpdate             = 0x04,
    PackageInstallationMonitor = 0x05,
    PackageMount               = 0x06,
    SpeechSynthesizer          = 0x07,
    SpeechSynthesizerStream    = 0x08,
    StoreContext               = 0x09,
    StoreLicense               = 0x0a,
    StoreProductQuery          = 0x0b,
    TaskQueue                  = 0x0c,
    User                       = 0x0d,
    UserSignOutDeferral        = 0x0e,
    GameUiTextEntry            = 0x0f,
    PFXGameSaveConfig          = 0x10,
}

/// <summary>Mirrors <c>enum class XSystemHandleCallbackReason</c> from XSystem.h.</summary>
internal enum XSystemHandleCallbackReason : uint
{
    Created   = 0x00,
    Destroyed = 0x01,
}

/// <summary>Mirrors <c>enum class XDisplayHdrModePreference</c> from XDisplay.h.</summary>
internal enum XDisplayHdrModePreference : uint
{
    PreferHdr         = 0,
    PreferRefreshRate = 1,
}

/// <summary>Mirrors <c>enum class XDisplayHdrModeResult</c> from XDisplay.h.</summary>
internal enum XDisplayHdrModeResult : uint
{
    Unknown  = 0,
    Enabled  = 1,
    Disabled = 2,
}

/// <summary>Mirrors <c>enum class XErrorOptions</c> from XError.h.</summary>
[Flags]
internal enum XErrorOptions : uint
{
    None                     = 0x00,
    OutputDebugStringOnError = 0x01,
    DebugBreakOnError        = 0x02,
    FailFastOnError          = 0x04,
}

/// <summary>Fixed-size buffer constants from XSystem.h.</summary>
internal static class XSystemConstants
{
    /// <summary><c>XSystemConsoleIdBytes</c> from XSystem.h — includes null terminator.</summary>
    internal const nuint ConsoleIdBytes = 39;

    /// <summary><c>XSystemXboxLiveSandboxIdMaxBytes</c> from XSystem.h — includes null terminator.</summary>
    internal const nuint XboxLiveSandboxIdMaxBytes = 16;

    /// <summary><c>XSystemAppSpecificDeviceIdBytes</c> from XSystem.h — includes null terminator.</summary>
    internal const nuint AppSpecificDeviceIdBytes = 45;
}
