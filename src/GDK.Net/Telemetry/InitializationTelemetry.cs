using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Security;
using System.Threading;
using GDK.Net.Interop;

namespace GDK.Net.Telemetry;

internal sealed class InitializationTelemetry
{
    internal static readonly InitializationTelemetry Runtime = new(
        () => Native.XPackageIsPackagedProcess() != 0,
        InitializationTelemetryWriter.IsEnabled,
        InitializationTelemetryMetadata.Collect,
        InitializationTelemetryWriter.Write);

    private readonly Func<bool> _isPackaged;
    private readonly Func<bool> _isEnabled;
    private readonly Func<GameRuntimeGameConfigSource, string?, ulong, InitializationTelemetryPayload> _collect;
    private readonly Action<InitializationTelemetryPayload> _write;
    private long _sequence;
    private int _reportedFailure;

    internal InitializationTelemetry(
        Func<bool> isPackaged,
        Func<bool> isEnabled,
        Func<GameRuntimeGameConfigSource, string?, ulong, InitializationTelemetryPayload> collect,
        Action<InitializationTelemetryPayload> write)
    {
        _isPackaged = isPackaged;
        _isEnabled = isEnabled;
        _collect = collect;
        _write = write;
    }

    internal ulong Sequence => unchecked((ulong)Interlocked.Read(ref _sequence));

    internal void RecordSuccess(bool withOptions, GameRuntimeGameConfigSource configSource, string? config)
    {
        ulong sequence = unchecked((ulong)Interlocked.Increment(ref _sequence));
        try
        {
            if (sequence == 0)
            {
                ReportFailure();
                return;
            }

            if (!withOptions || _isPackaged() || !_isEnabled())
            {
                return;
            }

            InitializationTelemetryPayload payload = _collect(configSource, config, sequence);
            if (!payload.IsValid())
            {
                ReportFailure();
                return;
            }

            _write(payload);
        }
        catch (Exception exception) when (exception is SecurityException
            or DllNotFoundException or EntryPointNotFoundException
            or InvalidOperationException or ArgumentException or NotSupportedException or EventSourceException)
        {
            ReportFailure();
        }
    }

    private void ReportFailure()
    {
        if (Interlocked.Exchange(ref _reportedFailure, 1) == 0)
        {
            Trace.TraceWarning("GDK.Net initialization telemetry was skipped because a local telemetry operation failed.");
        }
    }
}
