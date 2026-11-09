using System;
using System.Diagnostics.Tracing;
using Microsoft.Diagnostics.Telemetry;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace GDK.Net.Telemetry;

internal static class InitializationTelemetryWriter
{
    internal const string ProviderName = "Microsoft.Gaming.Middleware";
    internal const string ProviderGuid = "AB2F0BE6-763C-4048-818B-33999458B21C";
    internal const string EventName = "SdkComponentInitialized";

    private static readonly Lazy<MiddlewareEventSource> Provider = new(() =>
    {
        var source = new MiddlewareEventSource();
        if (source.ConstructionException is not null)
        {
            throw new InvalidOperationException("The initialization telemetry provider could not be constructed.",
                source.ConstructionException);
        }

        return source;
    });

    internal static bool IsEnabled() =>
        Provider.Value.IsEnabled(EventLevel.Informational, TelemetryEventSource.CriticalDataKeyword);

#if NET5_0_OR_GREATER
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(InitializationTelemetryPayload))]
    [UnconditionalSuppressMessage("Trimming", "IL2026",
        Justification = "The closed payload contains only primitive and string properties. "
            + "DynamicDependency preserves all its public property metadata and getters.")]
#endif
    internal static void Write(InitializationTelemetryPayload payload)
    {
        if (!payload.IsValid())
        {
            throw new ArgumentException("Invalid initialization telemetry payload.", nameof(payload));
        }

        Provider.Value.Write(EventName, new EventSourceOptions
        {
            Level = EventLevel.Informational,
            Keywords = TelemetryEventSource.CriticalDataKeyword,
            Tags = TelemetryEventSource.CoreData,
        }, payload);
    }

    [EventSource(Name = ProviderName, Guid = ProviderGuid)]
    private sealed class MiddlewareEventSource : TelemetryEventSource
    {
    }
}
