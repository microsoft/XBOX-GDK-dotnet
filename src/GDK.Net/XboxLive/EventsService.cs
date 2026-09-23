using System;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Xbox Live telemetry event writing. Mirrors <c>events_c.h</c>.</summary>
/// <remarks>
/// In-game events contain JSON dimensions for finite fields, such as map id or difficulty, and
/// JSON measurements for scalar metrics, such as score, time or counters. Event and field names
/// must match the title's Xbox Live service configuration; if they do not, the service drops the
/// event without notification. On GDK PC, the Gaming Runtime Services runtime must include the
/// XGameEvent feature or the native call returns <c>E_NOTIMPL</c>.
/// </remarks>
public sealed unsafe class EventsService
{
    private readonly XboxLiveContext _context;

    internal EventsService(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Writes one configured in-game telemetry event
    /// (<c>XblEventsWriteInGameEvent</c>).
    /// </summary>
    /// <param name="eventName">The service-configured event name. It must contain only ASCII letters and digits.</param>
    /// <param name="dimensionsJson">
    /// Optional JSON object containing dimension fields with finite string, Boolean or numeric values.
    /// </param>
    /// <param name="measurementsJson">Optional JSON object containing scalar numeric measurement fields.</param>
    public void WriteInGameEvent(
        string eventName,
        string? dimensionsJson = null,
        string? measurementsJson = null)
    {
        if (eventName is null)
        {
            throw new ArgumentNullException(nameof(eventName));
        }

        if (!IsValidEventName(eventName))
        {
            throw new ArgumentException(
                "Event names must be non-empty and contain only ASCII letters and digits.",
                nameof(eventName));
        }

        IntPtr name = Utf8.Allocate(eventName);
        IntPtr dimensions = IntPtr.Zero;
        IntPtr measurements = IntPtr.Zero;

        try
        {
            dimensions = Utf8.Allocate(dimensionsJson);
            measurements = Utf8.Allocate(measurementsJson);

            Hr.ThrowIfFailed(NativeXbl.XblEventsWriteInGameEvent(
                _context.Handle,
                (byte*)name,
                (byte*)dimensions,
                (byte*)measurements));
        }
        finally
        {
            Utf8.Free(name);
            Utf8.Free(dimensions);
            Utf8.Free(measurements);
        }
    }

    internal static bool IsValidEventName(string eventName)
    {
        if (eventName.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < eventName.Length; i++)
        {
            char c = eventName[i];
            bool digit = c >= '0' && c <= '9';
            bool upper = c >= 'A' && c <= 'Z';
            bool lower = c >= 'a' && c <= 'z';
            if (!digit && !upper && !lower)
            {
                return false;
            }
        }

        return true;
    }
}
