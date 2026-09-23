using GDK.Net.Interop;

namespace GDK.Net.SystemInfo;

/// <summary>
/// Thread time-sensitivity markers. APIs that perform substantial work call
/// <see cref="AssertNotTimeSensitive"/> to detect accidental invocations from audio or
/// render threads.
/// </summary>
/// <remarks>
/// Requires the Gaming Runtime to be initialized (<see cref="GameRuntime.Initialize()"/>).
/// </remarks>
public static class GameThread
{
    /// <summary>
    /// Gets or sets whether the calling thread is marked as time-sensitive
    /// (<c>XThreadIsTimeSensitive</c> / <c>XThreadSetTimeSensitive</c>).
    /// </summary>
    /// <remarks>
    /// Setting this to <see langword="true"/> causes any GDK API that performs a potentially
    /// long operation to assert in debug builds via <see cref="AssertNotTimeSensitive"/>.
    /// </remarks>
    public static bool IsTimeSensitive
    {
        get => Native.XThreadIsTimeSensitive() != 0;
        set => Hr.ThrowIfFailed(Native.XThreadSetTimeSensitive(value ? (byte)1 : (byte)0));
    }

    /// <summary>
    /// Asserts in debug builds that the calling thread is not marked as time-sensitive
    /// (<c>XThreadAssertNotTimeSensitive</c>).
    /// </summary>
    /// <remarks>
    /// APIs that should not be called from audio/render loops call this automatically.
    /// Call it from your own code to document the same requirement.
    /// </remarks>
    public static void AssertNotTimeSensitive() => Native.XThreadAssertNotTimeSensitive();
}
