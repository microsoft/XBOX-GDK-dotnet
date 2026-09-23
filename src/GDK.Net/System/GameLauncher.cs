using System;
using System.Text;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.SystemInfo;

/// <summary>
/// Title identity and cross-game launch APIs.
/// </summary>
/// <remarks>
/// Requires the Gaming Runtime to be initialized (<see cref="GameRuntime.Initialize()"/>).
/// All methods require a packaged title with a valid <c>MicrosoftGame.config</c> unless noted.
/// </remarks>
public static unsafe class GameLauncher
{
    // ─── XGame.h ─────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the Xbox title id declared in <c>MicrosoftGame.config</c>
    /// (<c>XGameGetXboxTitleId</c>).
    /// </summary>
    /// <exception cref="GameRuntimeException">
    /// Thrown when the title id cannot be found (HRESULT_FROM_WIN32(ERROR_NOT_FOUND)).
    /// </exception>
    public static uint GetXboxTitleId()
    {
        uint titleId;
        Hr.ThrowIfFailed(Native.XGameGetXboxTitleId(&titleId));
        return titleId;
    }

    // ─── XGame.h: launch ─────────────────────────────────────────────────────────

    /// <summary>
    /// Launches a new game process in place of the current one and terminates this process
    /// (<c>XLaunchNewGame</c>). This method never returns.
    /// </summary>
    /// <param name="exePath">
    /// Absolute path to the executable to launch. Must be inside the package layout.
    /// </param>
    /// <param name="args">Optional command-line arguments to pass to the new process.</param>
    /// <remarks>
    /// The process is terminated after the native call. Resources allocated on the heap
    /// (including managed objects) are not cleaned up — call <see cref="GameRuntime.Dispose"/>
    /// and flush any pending I/O before calling this method.
    /// </remarks>
    public static void LaunchNewGame(string exePath, string? args = null)
    {
        byte[] exePathBytes = ToUtf8Z(exePath);
        byte[]? argsBytes   = args != null ? ToUtf8Z(args) : null;

        fixed (byte* pExePath = exePathBytes)
        fixed (byte* pArgs    = argsBytes)
        {
            Native.XLaunchNewGame(pExePath, pArgs, IntPtr.Zero);
        }
    }

    /// <summary>
    /// Requests that the current game process be restarted automatically when it crashes
    /// (<c>XLaunchRestartOnCrash</c>). Only active during development; the call is a no-op in
    /// retail packages.
    /// </summary>
    /// <param name="args">Optional arguments forwarded to the restarted process.</param>
    public static void RestartOnCrash(string? args = null)
    {
        byte[]? argsBytes = args != null ? ToUtf8Z(args) : null;

        fixed (byte* pArgs = argsBytes)
        {
            Hr.ThrowIfFailed(Native.XLaunchRestartOnCrash(pArgs, reserved: 0));
        }
    }

    // ─── XLauncher.h ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Opens a URI using the appropriate handler on the current platform
    /// (<c>XLaunchUri</c>).
    /// </summary>
    /// <param name="uri">The URI to open (e.g. <c>ms-xbl-multiplayer://...</c>).</param>
    /// <param name="requestingUser">
    /// The user the launch is attributed to, or <see langword="null"/> to let the system choose.
    /// </param>
    /// <remarks>
    /// <paramref name="uri"/> must be a URI registered in the platform URI scheme registry.
    /// </remarks>
    public static void LaunchUri(string uri, User? requestingUser = null)
    {
        byte[] uriBytes = ToUtf8Z(uri);

        fixed (byte* pUri = uriBytes)
        {
            Hr.ThrowIfFailed(Native.XLaunchUri(requestingUser?.Handle ?? IntPtr.Zero, pUri));
        }
    }

    // ─── Helpers ─────────────────────────────────────────────────────────────────

    private static byte[] ToUtf8Z(string value)
    {
        int byteCount = Encoding.UTF8.GetByteCount(value);
        byte[] buffer = new byte[byteCount + 1]; // +1 for null terminator
        Encoding.UTF8.GetBytes(value, 0, value.Length, buffer, 0);
        // buffer[byteCount] is already 0
        return buffer;
    }
}
