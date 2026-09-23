using System;
using Microsoft.Win32.SafeHandles;
using GDK.Net.Interop;

namespace GDK.Net.SystemInfo;

/// <summary>
/// HDR mode control and display-timeout deferral APIs.
/// </summary>
/// <remarks>
/// Requires the Gaming Runtime to be initialized (<see cref="GameRuntime.Initialize()"/>).
/// </remarks>
public static unsafe class GameDisplay
{
    /// <summary>
    /// Attempts to enable HDR output on the primary display (<c>XDisplayTryEnableHdrMode</c>).
    /// </summary>
    /// <param name="preference">Whether to prefer HDR or a higher refresh rate.</param>
    /// <param name="info">
    /// When HDR was successfully enabled, receives the display's luminance capabilities;
    /// otherwise <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see cref="HdrModeResult.Enabled"/> when HDR is active after this call;
    /// <see cref="HdrModeResult.Disabled"/> when HDR is unavailable;
    /// <see cref="HdrModeResult.Unknown"/> when the display state cannot be determined.
    /// </returns>
    public static HdrModeResult TryEnableHdrMode(
        HdrModePreference preference,
        out HdrModeInfo? info)
    {
        XDisplayHdrModeInfo nativeInfo;
        XDisplayHdrModeResult result = Native.XDisplayTryEnableHdrMode(
            (XDisplayHdrModePreference)preference,
            &nativeInfo);

        if (result == XDisplayHdrModeResult.Enabled)
        {
            info = new HdrModeInfo(
                nativeInfo.MinToneMapLuminance,
                nativeInfo.MaxToneMapLuminance,
                nativeInfo.MaxFullFrameToneMapLuminance);
        }
        else
        {
            info = null;
        }

        return (HdrModeResult)result;
    }

    /// <summary>
    /// Acquires a display-timeout deferral that prevents the screen saver from activating while
    /// the deferral is held (<c>XDisplayAcquireTimeoutDeferral</c>).
    /// </summary>
    /// <returns>
    /// An <see cref="IDisposable"/> that closes the native deferral handle when disposed.
    /// </returns>
    /// <remarks>
    /// Dispose the returned object as soon as the deferral is no longer needed.
    /// Forgetting to dispose it will keep the screen saver suppressed indefinitely.
    /// </remarks>
    public static DisplayTimeoutDeferral AcquireTimeoutDeferral()
    {
        IntPtr rawHandle;
        Hr.ThrowIfFailed(Native.XDisplayAcquireTimeoutDeferral(&rawHandle));
        return new DisplayTimeoutDeferral(new DisplayTimeoutDeferralHandle(rawHandle));
    }
}

/// <summary>
/// Wraps an <c>XDisplayTimeoutDeferralHandle</c>; released with
/// <c>XDisplayCloseTimeoutDeferralHandle</c> on disposal.
/// </summary>
public sealed class DisplayTimeoutDeferral : IDisposable
{
    private readonly DisplayTimeoutDeferralHandle _handle;

    internal DisplayTimeoutDeferral(DisplayTimeoutDeferralHandle handle)
    {
        _handle = handle;
    }

    /// <summary>Releases the deferral, allowing the screen saver to activate normally.</summary>
    public void Dispose() => _handle.Dispose();
}

/// <summary>Owns a native <c>XDisplayTimeoutDeferralHandle</c>.</summary>
internal sealed class DisplayTimeoutDeferralHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal DisplayTimeoutDeferralHandle(IntPtr existingHandle)
        : base(ownsHandle: true)
    {
        SetHandle(existingHandle);
    }

    protected override bool ReleaseHandle()
    {
        Native.XDisplayCloseTimeoutDeferralHandle(handle);
        return true;
    }
}
