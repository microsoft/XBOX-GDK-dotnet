using System;
using GDK.Net.Interop;

namespace GDK.Net.SystemInfo;

/// <summary>
/// A GDK version number (<c>XVersion</c> from XGameRuntimeTypes.h). The four 16-bit components
/// pack into a single uint64 for ordered comparisons.
/// </summary>
public readonly struct SystemVersion : IEquatable<SystemVersion>
{
    /// <param name="major">Major version component.</param>
    /// <param name="minor">Minor version component.</param>
    /// <param name="build">Build version component.</param>
    /// <param name="revision">Revision version component.</param>
    public SystemVersion(ushort major, ushort minor, ushort build, ushort revision)
    {
        Major    = major;
        Minor    = minor;
        Build    = build;
        Revision = revision;
    }

    internal SystemVersion(XVersion native)
        : this(native.Major, native.Minor, native.Build, native.Revision)
    {
    }

    /// <summary>Major version component.</summary>
    public ushort Major { get; }

    /// <summary>Minor version component.</summary>
    public ushort Minor { get; }

    /// <summary>Build number.</summary>
    public ushort Build { get; }

    /// <summary>Revision number.</summary>
    public ushort Revision { get; }

    /// <summary>The bit-packed uint64 representation used by the GDK for version comparisons.</summary>
    public ulong PackedValue =>
        (ulong)Major | ((ulong)Minor << 16) | ((ulong)Build << 32) | ((ulong)Revision << 48);

    /// <inheritdoc/>
    public bool Equals(SystemVersion other) => PackedValue == other.PackedValue;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is SystemVersion other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <summary>Returns the version formatted as <c>Major.Minor.Build.Revision</c>.</summary>
    public override string ToString() => $"{Major}.{Minor}.{Build}.{Revision}";

    /// <summary>Equality operator.</summary>
    public static bool operator ==(SystemVersion left, SystemVersion right) => left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(SystemVersion left, SystemVersion right) => !left.Equals(right);
}

/// <summary>
/// Analytics information about the current device (<c>XSystemAnalyticsInfo</c> from XSystem.h).
/// </summary>
public sealed class SystemAnalyticsInfo
{
    internal SystemAnalyticsInfo(
        SystemVersion osVersion,
        SystemVersion hostingOsVersion,
        string family,
        string form)
    {
        OsVersion        = osVersion;
        HostingOsVersion = hostingOsVersion;
        Family           = family;
        Form             = form;
    }

    /// <summary>The OS version running on the device.</summary>
    public SystemVersion OsVersion { get; }

    /// <summary>The hosting OS version (relevant on streamed titles).</summary>
    public SystemVersion HostingOsVersion { get; }

    /// <summary>Device family string (e.g. <c>"XboxOne"</c>).</summary>
    public string Family { get; }

    /// <summary>Device form factor string (e.g. <c>"Xbox One S"</c>).</summary>
    public string Form { get; }

    /// <summary>Returns the family, form factor and OS version for diagnostics.</summary>
    public override string ToString() => $"{Family} / {Form} OS={OsVersion}";
}

/// <summary>
/// Runtime and available GDK version information (<c>XSystemRuntimeInfo</c> from XSystem.h).
/// </summary>
public sealed class SystemRuntimeInfo
{
    internal SystemRuntimeInfo(SystemVersion runtimeVersion, SystemVersion availableVersion)
    {
        RuntimeVersion   = runtimeVersion;
        AvailableVersion = availableVersion;
    }

    /// <summary>The version of the Gaming Runtime currently running.</summary>
    public SystemVersion RuntimeVersion { get; }

    /// <summary>The highest version available on this device.</summary>
    public SystemVersion AvailableVersion { get; }

    /// <summary>Returns the runtime and available versions for diagnostics.</summary>
    public override string ToString() => $"Runtime={RuntimeVersion} Available={AvailableVersion}";
}

/// <summary>
/// The device type of the current machine. Mirrors <c>enum class XSystemDeviceType</c> from
/// XSystem.h.
/// </summary>
public enum SystemDeviceType : uint
{
    /// <summary>Unknown device type.</summary>
    Unknown              = 0x00,

    /// <summary>A Windows PC.</summary>
    Pc                   = 0x01,

    /// <summary>Xbox One.</summary>
    XboxOne              = 0x02,

    /// <summary>Xbox One S.</summary>
    XboxOneS             = 0x03,

    /// <summary>Xbox One X.</summary>
    XboxOneX             = 0x04,

    /// <summary>Xbox One X devkit.</summary>
    XboxOneXDevkit       = 0x05,

    /// <summary>Xbox Series S (Lockhart).</summary>
    XboxScarlettLockhart = 0x06,

    /// <summary>Xbox Series X (Anaconda).</summary>
    XboxScarlettAnaconda = 0x07,

    /// <summary>Xbox Series devkit.</summary>
    XboxScarlettDevkit   = 0x08,
}

/// <summary>
/// The type of a GDK handle passed to <c>XSystemHandleCallback</c>. Mirrors
/// <c>enum class XSystemHandleType</c> from XSystem.h.
/// </summary>
public enum SystemHandleType : uint
{
    /// <summary>An app-capture screenshot stream handle.</summary>
    AppCaptureScreenshotStream = 0x00,

    /// <summary>A display-timeout deferral handle.</summary>
    DisplayTimeoutDeferral     = 0x01,

    /// <summary>A game-save container handle.</summary>
    GameSaveContainer          = 0x02,

    /// <summary>A game-save provider handle.</summary>
    GameSaveProvider           = 0x03,

    /// <summary>A game-save update handle.</summary>
    GameSaveUpdate             = 0x04,

    /// <summary>A package-installation monitor handle.</summary>
    PackageInstallationMonitor = 0x05,

    /// <summary>A package mount handle.</summary>
    PackageMount               = 0x06,

    /// <summary>A speech synthesizer handle.</summary>
    SpeechSynthesizer          = 0x07,

    /// <summary>A speech synthesizer stream handle.</summary>
    SpeechSynthesizerStream    = 0x08,

    /// <summary>A Store context handle.</summary>
    StoreContext               = 0x09,

    /// <summary>A Store licence handle.</summary>
    StoreLicense               = 0x0a,

    /// <summary>A Store product-query handle.</summary>
    StoreProductQuery          = 0x0b,

    /// <summary>A task queue handle.</summary>
    TaskQueue                  = 0x0c,

    /// <summary>A user handle.</summary>
    User                       = 0x0d,

    /// <summary>A user sign-out deferral handle.</summary>
    UserSignOutDeferral        = 0x0e,

    /// <summary>A Game UI text-entry handle.</summary>
    GameUiTextEntry            = 0x0f,

    /// <summary>A PlayFab Game Save configuration handle.</summary>
    PFXGameSaveConfig          = 0x10,
}

/// <summary>
/// Whether a GDK handle was created or destroyed. Mirrors
/// <c>enum class XSystemHandleCallbackReason</c> from XSystem.h.
/// </summary>
public enum SystemHandleCallbackReason : uint
{
    /// <summary>The handle was just created.</summary>
    Created   = 0x00,

    /// <summary>The handle is being destroyed.</summary>
    Destroyed = 0x01,
}

/// <summary>
/// HDR display mode preference for <see cref="GameDisplay.TryEnableHdrMode"/>. Mirrors
/// <c>enum class XDisplayHdrModePreference</c> from XDisplay.h.
/// </summary>
public enum HdrModePreference : uint
{
    /// <summary>Prefer to enable HDR output if available.</summary>
    PreferHdr         = 0,

    /// <summary>Prefer a higher refresh rate over HDR.</summary>
    PreferRefreshRate = 1,
}

/// <summary>
/// Result of a call to <see cref="GameDisplay.TryEnableHdrMode"/>. Mirrors
/// <c>enum class XDisplayHdrModeResult</c> from XDisplay.h.
/// </summary>
public enum HdrModeResult : uint
{
    /// <summary>The HDR status is unknown.</summary>
    Unknown  = 0,

    /// <summary>HDR mode was successfully enabled.</summary>
    Enabled  = 1,

    /// <summary>HDR mode could not be enabled (display does not support it, or preference was <see cref="HdrModePreference.PreferRefreshRate"/>).</summary>
    Disabled = 2,
}

/// <summary>
/// Luminance parameters returned by <see cref="GameDisplay.TryEnableHdrMode"/> when HDR is
/// enabled. Mirrors <c>struct XDisplayHdrModeInfo</c> from XDisplay.h.
/// </summary>
public sealed class HdrModeInfo
{
    internal HdrModeInfo(float minToneMapLuminance, float maxToneMapLuminance, float maxFullFrameToneMapLuminance)
    {
        MinToneMapLuminance          = minToneMapLuminance;
        MaxToneMapLuminance          = maxToneMapLuminance;
        MaxFullFrameToneMapLuminance = maxFullFrameToneMapLuminance;
    }

    /// <summary>Minimum tone-map luminance in nits.</summary>
    public float MinToneMapLuminance { get; }

    /// <summary>Maximum tone-map luminance in nits.</summary>
    public float MaxToneMapLuminance { get; }

    /// <summary>Maximum full-frame tone-map luminance in nits.</summary>
    public float MaxFullFrameToneMapLuminance { get; }
}

/// <summary>
/// Error reporting options for <see cref="GameErrorHandling.SetOptions"/>. Mirrors
/// <c>enum class XErrorOptions</c> from XError.h.
/// </summary>
[Flags]
public enum ErrorOptions : uint
{
    /// <summary>No special error reporting.</summary>
    None                     = 0x00,

    /// <summary>Call <c>OutputDebugString</c> when an error is reported.</summary>
    OutputDebugStringOnError = 0x01,

    /// <summary>Break into the debugger when an error is reported.</summary>
    DebugBreakOnError        = 0x02,

    /// <summary>Call <c>RaiseFailFastException</c> when an error is reported.</summary>
    FailFastOnError          = 0x04,
}

/// <summary>
/// Callback invoked by the GDK when an error is reported via <c>XErrorReport</c>.
/// Return <see langword="true"/> to suppress the error (mark it as handled);
/// return <see langword="false"/> to let the GDK apply its default behaviour.
/// </summary>
/// <param name="hresult">The HRESULT that was reported.</param>
/// <param name="message">A human-readable description of the error.</param>
public delegate bool GdkErrorCallback(int hresult, string message);
