using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GDK.Net.Telemetry;
using Microsoft.Diagnostics.Telemetry;
using Xunit;

namespace GDK.Net.Tests;

[Collection(RuntimeCollection.Name)]
public sealed class InitializationTelemetryTests
{
    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void MissingDevelopmentEvidenceAndPackagedProcessesNeverCollect(bool withOptions, bool packaged)
    {
        int providerQueries = 0;
        var telemetry = new InitializationTelemetry(() => packaged,
            () => { providerQueries++; return true; },
            (_, _, sequence) => Payload(sequence),
            _ => Assert.Fail("No event should be written."));

        telemetry.RecordSuccess(withOptions, GameRuntimeGameConfigSource.Default, null);

        Assert.Equal(1UL, telemetry.Sequence);
        Assert.Equal(0, providerQueries);
    }

    [Fact]
    public void DisabledProviderAvoidsOptionalMetadata()
    {
        int collected = 0;
        var telemetry = new InitializationTelemetry(() => false, () => false,
            (_, _, sequence) => { collected++; return Payload(sequence); }, _ => Assert.Fail("Unexpected event."));

        telemetry.RecordSuccess(true, GameRuntimeGameConfigSource.Default, null);

        Assert.Equal(0, collected);
    }

    [Fact]
    public void SequenceIncludesIneligibleAndUnobservedCyclesAndRetainsSelectedConfig()
    {
        var written = new List<InitializationTelemetryPayload>();
        bool enabled = false;
        string? selected = null;
        GameRuntimeGameConfigSource selectedSource = GameRuntimeGameConfigSource.Default;
        var telemetry = new InitializationTelemetry(() => false, () => enabled,
            (source, config, sequence) =>
            {
                selectedSource = source;
                selected = config;
                return Payload(sequence);
            }, written.Add);
        telemetry.RecordSuccess(true, GameRuntimeGameConfigSource.Default, null);
        enabled = true;
        telemetry.RecordSuccess(false, GameRuntimeGameConfigSource.Default, null);
        telemetry.RecordSuccess(true, GameRuntimeGameConfigSource.Inline, "<Game />");
        telemetry.RecordSuccess(true, GameRuntimeGameConfigSource.Inline, "<Game />");

        Assert.Equal(new ulong[] { 3, 4 }, written.Select(value => value.InitializationSequence));
        Assert.Equal(GameRuntimeGameConfigSource.Inline, selectedSource);
        Assert.Equal("<Game />", selected);
    }

    [Fact]
    public void UnknownPackageEvidenceSkipsTheEventWithoutThrowing()
    {
        var packageFailure = new InitializationTelemetry(
            () => throw new EntryPointNotFoundException(), () => true,
            (_, _, _) => throw new InvalidOperationException(), _ => Assert.Fail("Unexpected event."));

        packageFailure.RecordSuccess(true, GameRuntimeGameConfigSource.Default, null);
    }

    [Fact]
    public void WriterFailureDoesNotChangeInitializationSuccessOrResetSequence()
    {
        var telemetry = new InitializationTelemetry(() => false, () => true,
            (_, _, sequence) => Payload(sequence), _ => throw new InvalidOperationException());

        telemetry.RecordSuccess(true, GameRuntimeGameConfigSource.Default, null);
        telemetry.RecordSuccess(true, GameRuntimeGameConfigSource.Default, null);

        Assert.Equal(2UL, telemetry.Sequence);
    }

    [Fact]
    public void FailedNativeInitializationNeverAdvancesSuccessfulSequence()
    {
        ulong before = InitializationTelemetry.Runtime.Sequence;
        Assert.Throws<GameRuntimeException>(() => GameRuntime.Initialize(new GameRuntimeOptions
        {
            GameConfigSource = GameRuntimeGameConfigSource.Inline,
            GameConfig = "<not-valid-game-config",
        }));
        Assert.Equal(before, InitializationTelemetry.Runtime.Sequence);
    }

    [Fact]
    public void ConcurrentSuccessesHaveDistinctSequences()
    {
        var sequences = new ConcurrentBag<ulong>();
        var telemetry = new InitializationTelemetry(() => false, () => true,
            (_, _, sequence) => Payload(sequence), payload => sequences.Add(payload.InitializationSequence));

        Parallel.For(0, 100, _ => telemetry.RecordSuccess(true, GameRuntimeGameConfigSource.Default, null));

        Assert.Equal(100UL, telemetry.Sequence);
        Assert.Equal(Enumerable.Range(1, 100).Select(value => (ulong)value), sequences.OrderBy(value => value));
    }

    [Fact]
    public void InvalidRequiredPayloadIsNotWritten()
    {
        var telemetry = new InitializationTelemetry(() => false, () => true,
            (_, _, sequence) => { var value = Payload(sequence); value.ComponentVersion = "bad\0version"; return value; },
            _ => Assert.Fail("Invalid payload was written."));

        telemetry.RecordSuccess(true, GameRuntimeGameConfigSource.Default, null);
    }

    [Theory]
    [InlineData("ok", true)]
    [InlineData("a\0b", false)]
    [InlineData("a\nb", false)]
    [InlineData("\ud83d\ude00", true)]
    public void StringsMustBeBoundedControlFreeAndWellFormed(string value, bool valid)
    {
        Assert.Equal(valid, InitializationTelemetryPayload.ValidText(value, 128));
        Assert.False(InitializationTelemetryPayload.ValidText(new string('a', 129), 128));
        Assert.Equal("", InitializationTelemetryPayload.OptionalText("bad\0value", 128));
    }

    [Fact]
    public void UnpairedSurrogatesAreRejected()
    {
        Assert.False(InitializationTelemetryPayload.ValidText(new string((char)0xd800, 1), 128));
        Assert.False(InitializationTelemetryPayload.ValidText(new string((char)0xdc00, 1), 128));
    }

    [Fact]
    public void ConfiguredIdentityUsesWindowsPublisherHash()
    {
        var identity = InitializationTelemetryMetadata.DeriveIdentity("Microsoft.XboxApp",
            "CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US");

        Assert.Equal("8wekyb3d8bbwe", identity.PublisherId);
        Assert.Equal("Microsoft.XboxApp_8wekyb3d8bbwe", identity.Family);
        Assert.Equal(("", ""), InitializationTelemetryMetadata.DeriveIdentity("invalid_name", "CN=Example"));
        Assert.Equal(("", ""), InitializationTelemetryMetadata.DeriveIdentity("Example", "bad\0publisher"));
    }

    [Fact]
    public void InlineAndFileConfigsProduceTheSameIdentity()
    {
        const string config = "<Game><Identity Name=\"Example.Game\" Publisher=\"CN=Example\" /></Game>";
        string file = Path.GetTempFileName();
        try
        {
            File.WriteAllText(file, config);
            var inline = InitializationTelemetryMetadata.ReadConfiguredIdentity(GameRuntimeGameConfigSource.Inline, config);
            var fromFile = InitializationTelemetryMetadata.ReadConfiguredIdentity(GameRuntimeGameConfigSource.File, file);
            Assert.NotEmpty(inline.Family);
            Assert.Equal(inline, fromFile);
        }
        finally
        {
            File.Delete(file);
        }
    }

    [Theory]
    [InlineData("<Game />")]
    [InlineData("<broken")]
    [InlineData("<!DOCTYPE Game [<!ENTITY x SYSTEM 'file:///does-not-exist'>]><Game>&x;</Game>")]
    public void MissingInvalidAndExternalEntityConfigsProduceNoIdentity(string config)
    {
        Assert.Equal(("", ""), InitializationTelemetryMetadata.ReadConfiguredIdentity(
            GameRuntimeGameConfigSource.Inline, config));
    }

    [Fact]
    public void OversizedConfigProducesNoIdentity()
    {
        Assert.Equal(("", ""), InitializationTelemetryMetadata.ReadConfiguredIdentity(
            GameRuntimeGameConfigSource.Inline,
            new string(' ', InitializationTelemetryMetadata.MaximumConfigBytes + 1)));
    }

    [Fact]
    public void OversizedFileConfigProducesNoIdentity()
    {
        string file = Path.GetTempFileName();
        try
        {
            File.WriteAllText(file, "<Game><Identity Name=\"Example.Game\" Publisher=\"CN=Example\" /></Game>"
                + new string(' ', InitializationTelemetryMetadata.MaximumConfigBytes));
            Assert.Equal(("", ""), InitializationTelemetryMetadata.ReadConfiguredIdentity(
                GameRuntimeGameConfigSource.File, file));
        }
        finally
        {
            File.Delete(file);
        }
    }

    [Fact]
    public void PublicStubNeverSetsProviderGroupTraits()
    {
        foreach (TelemetryGroup group in Enum.GetValues<TelemetryGroup>())
        {
            using var source = new TelemetryEventSource("GdkNetStubTest." + group, group);
            Assert.Null(source.ConstructionException);
            Assert.Null(source.GetTrait("ETW_GROUP"));
            Assert.True(source.Settings.HasFlag(EventSourceSettings.EtwSelfDescribingEventFormat));
        }

        Assert.Null(InitializationTelemetryWriter.Source.GetTrait("ETW_GROUP"));
        Assert.Equal(new Guid(InitializationTelemetryWriter.ProviderGuid), InitializationTelemetryWriter.Source.Guid);
    }

    [Fact]
    public void EventHasExactPublicSchemaClassificationAndNativeTypes()
    {
        using var listener = new Capture();
        InitializationTelemetryPayload payload = Payload(ulong.MaxValue);
        payload.DeviceType = uint.MaxValue;

        InitializationTelemetryWriter.Write(payload);

        Assert.Empty(listener.Errors);
        var value = Assert.Single(listener.Events);
        Assert.Equal(InitializationTelemetryWriter.EventName, value.EventName);
        Assert.Equal(EventLevel.Informational, value.Level);
        Assert.Equal(TelemetryEventSource.CriticalDataKeyword, value.Keywords);
        Assert.Equal(TelemetryEventSource.CoreData, value.Tags);
        Assert.Equal(new[]
        {
            "PartA_PrivTags", "SchemaVersion", "ComponentId", "ComponentVersion", "ComponentKind",
            "InitializationSequence", "EngineName", "EngineVersion", "ExecutionRuntimeName",
            "ExecutionRuntimeVersion", "GamingRuntimeVersion", "GdkBuildSdkVersion", "DevelopmentContext",
            "ConfiguredTitlePackageFamilyName", "PackagePublisherId", "DeviceType", "XboxTitleId", "PlayFabTitleId",
        }, value.PayloadNames);
        Assert.Equal(18, value.Payload!.Count);
        Assert.Equal((ulong)PrivTags.ProductAndServiceUsage, Assert.IsType<ulong>(value.Payload[0]));
        Assert.Equal(1u, Assert.IsType<uint>(value.Payload[1]));
        Assert.Equal(ulong.MaxValue, Assert.IsType<ulong>(value.Payload[5]));
        Assert.Equal(uint.MaxValue, Assert.IsType<uint>(value.Payload[15]));
        Assert.All(value.Payload.Where((_, index) => index is 2 or 3 or >= 6 and <= 11 or 13 or 14 or 16 or 17),
            item => Assert.IsType<string>(item));
    }

    private static InitializationTelemetryPayload Payload(ulong sequence) =>
        new() { ComponentVersion = "1.0.0", InitializationSequence = sequence };

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
