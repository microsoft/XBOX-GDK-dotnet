using System;

namespace GDK.Net.PlayFab;

/// <summary>
/// Converts between PlayFab's <c>time_t</c> fields and <see cref="DateTimeOffset"/>.
/// </summary>
/// <remarks>
/// The GDK builds the PlayFab libraries with a 64-bit <c>time_t</c> holding whole seconds since the
/// Unix epoch, which is exactly what <see cref="DateTimeOffset.FromUnixTimeSeconds"/> consumes.
/// </remarks>
internal static class PlayFabTime
{
    /// <summary>Projects a native <c>time_t</c> as a UTC <see cref="DateTimeOffset"/>.</summary>
    internal static DateTimeOffset ToDateTimeOffset(long value) =>
        DateTimeOffset.FromUnixTimeSeconds(value);

    /// <summary>Converts a <see cref="DateTimeOffset"/> back to a native <c>time_t</c>.</summary>
    internal static long ToUnixTime(DateTimeOffset value) => value.ToUnixTimeSeconds();
}
