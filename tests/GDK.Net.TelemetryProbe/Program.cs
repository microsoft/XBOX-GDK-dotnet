using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using GDK.Net;
using GDK.Net.Telemetry;
using Microsoft.Diagnostics.Telemetry;

namespace GDK.Net.TelemetryProbe;

internal static class Program
{
    private static void Main(string[] args)
    {
        Require(args.Length == 1 && args[0] is "--jit" or "--aot", "Specify --jit or --aot.");
        Require(RuntimeFeature.IsDynamicCodeSupported == (args[0] == "--jit"), "Unexpected compilation mode.");
        using var listener = new Capture();
        EventSource source = InitializationTelemetryWriter.Source;
        Require(source.ConstructionException is null, "Provider construction failed.");
        Require(source.GetTrait("ETW_GROUP") is null, "Public provider must not join an ETW provider group.");
        Require(source.Guid == new Guid(InitializationTelemetryWriter.ProviderGuid), "Incorrect provider GUID.");
        Require(source.Settings.HasFlag(EventSourceSettings.EtwSelfDescribingEventFormat), "Incorrect ETW format.");

        var telemetry = new InitializationTelemetry(() => false,
            InitializationTelemetryWriter.IsEnabled,
            (_, _, sequence) => new InitializationTelemetryPayload
            {
                ComponentVersion = "1.2.3",
                InitializationSequence = sequence,
                ExecutionRuntimeName = ".NET",
                ExecutionRuntimeVersion = Environment.Version.ToString(),
                GamingRuntimeVersion = "1.2.3.4",
                DeviceType = uint.MaxValue,
                XboxTitleId = "0123ABCD",
            }, InitializationTelemetryWriter.Write);
        telemetry.RecordSuccess(true, GameRuntimeGameConfigSource.Default, null);

        Require(listener.Errors.Count == 0, "EventSource reported an emission error.");
        Require(listener.Events.Count == 1, "Expected exactly one event.");
        EventWrittenEventArgs value = listener.Events[0];
        Require(value.EventName == InitializationTelemetryWriter.EventName, "Incorrect event name.");
        Require(value.Level == EventLevel.Informational
            && value.Keywords == TelemetryEventSource.CriticalDataKeyword
            && value.Tags == TelemetryEventSource.CoreData, "Incorrect classification.");
        string[] names =
        {
            "PartA_PrivTags", "SchemaVersion", "ComponentId", "ComponentVersion", "ComponentKind",
            "InitializationSequence", "EngineName", "EngineVersion", "ExecutionRuntimeName",
            "ExecutionRuntimeVersion", "GamingRuntimeVersion", "GdkBuildSdkVersion", "DevelopmentContext",
            "ConfiguredTitlePackageFamilyName", "PackagePublisherId", "DeviceType", "XboxTitleId", "PlayFabTitleId",
        };
        Require(value.PayloadNames is not null && value.PayloadNames.SequenceEqual(names), "Incorrect schema.");
        object[] expected =
        {
            (ulong)PrivTags.ProductAndServiceUsage, 1u, "GDK.Net", "1.2.3", 2u, 1UL, "", "",
            ".NET", Environment.Version.ToString(), "1.2.3.4", "", 3u, "", "", uint.MaxValue, "0123ABCD", "",
        };
        Require(value.Payload is not null && value.Payload.Count == expected.Length, "Incorrect payload length.");
        for (int i = 0; i < expected.Length; i++)
        {
            object? actual = value.Payload![i];
            Require(actual is not null && actual.GetType() == expected[i].GetType()
                && actual.Equals(expected[i]), "Incorrect payload value or type at field " + names[i] + ".");
        }

        Console.WriteLine("PASS: " + args[0] + "; provider-group=none; 18 fields and types verified.");
        Console.WriteLine("RuntimeVersion: " + Environment.Version);
    }

    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }

    private sealed class Capture : EventListener
    {
        internal List<EventWrittenEventArgs> Events { get; } = new();
        internal List<EventWrittenEventArgs> Errors { get; } = new();

        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name == InitializationTelemetryWriter.ProviderName)
            {
                EnableEvents(eventSource, EventLevel.Verbose, EventKeywords.All);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData.EventId == 0) Errors.Add(eventData);
            else Events.Add(eventData);
        }
    }
}
