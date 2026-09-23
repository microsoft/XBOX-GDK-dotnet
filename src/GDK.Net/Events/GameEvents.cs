using System;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.Events;

/// <summary>
/// Writes in-game telemetry events (<c>XGameEventWrite</c>).
/// </summary>
/// <remarks>
/// Events are declared in Partner Center. The dimension and measurement payloads are JSON objects
/// whose property names are the event's declared fields; the runtime validates them against the
/// title's event manifest at write time.
/// </remarks>
public static unsafe class GameEvents
{
    /// <summary>
    /// Writes a single in-game event for <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user the event is attributed to.</param>
    /// <param name="serviceConfigId">The title's service configuration id (SCID).</param>
    /// <param name="playSessionId">
    /// An identifier that groups events from one play session. Any stable string chosen by the title.
    /// </param>
    /// <param name="eventName">The event name as declared in Partner Center.</param>
    /// <param name="dimensionsJson">The event's dimension fields as a JSON object, or <see langword="null"/>.</param>
    /// <param name="measurementsJson">The event's measurement fields as a JSON object, or <see langword="null"/>.</param>
    public static void Write(
        User user,
        string serviceConfigId,
        string playSessionId,
        string eventName,
        string? dimensionsJson = null,
        string? measurementsJson = null)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (string.IsNullOrEmpty(serviceConfigId))
        {
            throw new ArgumentException("A service configuration id is required.", nameof(serviceConfigId));
        }

        if (string.IsNullOrEmpty(playSessionId))
        {
            throw new ArgumentException("A play session id is required.", nameof(playSessionId));
        }

        if (string.IsNullOrEmpty(eventName))
        {
            throw new ArgumentException("An event name is required.", nameof(eventName));
        }

        IntPtr scid = IntPtr.Zero;
        IntPtr session = IntPtr.Zero;
        IntPtr name = IntPtr.Zero;
        IntPtr dimensions = IntPtr.Zero;
        IntPtr measurements = IntPtr.Zero;

        try
        {
            scid = Utf8.Allocate(serviceConfigId);
            session = Utf8.Allocate(playSessionId);
            name = Utf8.Allocate(eventName);
            dimensions = Utf8.Allocate(dimensionsJson);
            measurements = Utf8.Allocate(measurementsJson);

            Hr.ThrowIfFailed(Native.XGameEventWrite(
                user.Handle,
                (byte*)scid,
                (byte*)session,
                (byte*)name,
                (byte*)dimensions,
                (byte*)measurements));
        }
        finally
        {
            Utf8.Free(scid);
            Utf8.Free(session);
            Utf8.Free(name);
            Utf8.Free(dimensions);
            Utf8.Free(measurements);
        }
    }
}
