using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Security;
using System.Threading;
using GDK.Net.Interop;

namespace GDK.Net.Telemetry;

internal static class InitializationTelemetry
{
    private static long _sequence;
    private static int _reportedFailure;

    internal static void RecordSuccess(bool withOptions, GameRuntimeGameConfigSource configSource, string? config)
    {
        ulong sequence = unchecked((ulong)Interlocked.Increment(ref _sequence));
        try
        {
            if (sequence == 0)
            {
                ReportFailure();
                return;
            }

            if (!withOptions || Native.XPackageIsPackagedProcess() != 0 || !InitializationTelemetryWriter.IsEnabled())
            {
                return;
            }

            InitializationTelemetryPayload payload = InitializationTelemetryMetadata.Collect(configSource, config, sequence);
            if (!payload.IsValid())
            {
                ReportFailure();
                return;
            }

            InitializationTelemetryWriter.Write(payload);
        }
        catch (Exception exception) when (exception is SecurityException
            or DllNotFoundException or EntryPointNotFoundException
            or InvalidOperationException or ArgumentException or NotSupportedException or EventSourceException)
        {
            ReportFailure();
        }
    }

    private static void ReportFailure()
    {
        if (Interlocked.Exchange(ref _reportedFailure, 1) == 0)
        {
            Trace.TraceWarning("GDK.Net initialization telemetry was skipped because a local telemetry operation failed.");
        }
    }
}
