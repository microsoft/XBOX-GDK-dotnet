using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>Projects <c>PARTY_REGION</c>: an Azure region and its measured latency.</summary>
public sealed class PartyRegion
{
    internal PartyRegion(string regionName, uint roundTripLatencyInMilliseconds)
    {
        RegionName = regionName;
        RoundTripLatency = TimeSpan.FromMilliseconds(roundTripLatencyInMilliseconds);
    }

    /// <summary>The Azure region name, for example <c>eastus</c>.</summary>
    public string RegionName { get; }

    /// <summary>The measured round-trip latency to the region.</summary>
    public TimeSpan RoundTripLatency { get; }

    internal static unsafe PartyRegion FromNative(PARTY_REGION* source) =>
        new(PartyInterop.ReadFixed(source->RegionName, 19), source->RoundTripLatencyInMilliseconds);

    internal unsafe void WriteTo(PARTY_REGION* target)
    {
        *target = default;
        Utf8.CopyFixed(target->RegionName, 20, RegionName);
        target->RoundTripLatencyInMilliseconds =
            (uint)Math.Max(0, Math.Min(uint.MaxValue, RoundTripLatency.TotalMilliseconds));
    }
}

/// <summary>
/// Projects <c>PARTY_NETWORK_DESCRIPTOR</c>: the opaque token that identifies a network and
/// carries enough information for another device to connect to it.
/// </summary>
/// <remarks>
/// The payload is deliberately opaque. Use <see cref="Serialize"/> to obtain the string form the
/// title distributes through its own invite channel, and <see cref="Deserialize"/> to recover it.
/// </remarks>
public sealed class PartyNetworkDescriptor
{
    private readonly byte[] _raw;

    internal unsafe PartyNetworkDescriptor(PARTY_NETWORK_DESCRIPTOR* source)
    {
        _raw = new byte[sizeof(PARTY_NETWORK_DESCRIPTOR)];
        fixed (byte* target = _raw)
        {
            Buffer.MemoryCopy(source, target, _raw.Length, _raw.Length);
        }

        NetworkIdentifier = PartyInterop.ReadFixed(source->NetworkIdentifier, 36);
        RegionName = PartyInterop.ReadFixed(source->RegionName, 19);
    }

    /// <summary>The network's unique identifier.</summary>
    public string NetworkIdentifier { get; }

    /// <summary>The Azure region hosting the network.</summary>
    public string RegionName { get; }

    /// <summary>
    /// Serializes the descriptor to the string form a title distributes to joiners.
    /// </summary>
    public unsafe string Serialize()
    {
        // PARTY_MAX_SERIALIZED_NETWORK_DESCRIPTOR_STRING_LENGTH + 1.
        byte* buffer = stackalloc byte[449];
        fixed (byte* raw = _raw)
        {
            PartyInterop.Check(
                NativePlayFab.PartySerializeNetworkDescriptor((PARTY_NETWORK_DESCRIPTOR*)raw, buffer));
        }

        return Utf8.ToString(buffer) ?? string.Empty;
    }

    /// <summary>Recovers a descriptor from the string produced by <see cref="Serialize"/>.</summary>
    /// <param name="serialized">The serialized descriptor.</param>
    public static unsafe PartyNetworkDescriptor Deserialize(string serialized)
    {
        if (serialized is null)
        {
            throw new ArgumentNullException(nameof(serialized));
        }

        PARTY_NETWORK_DESCRIPTOR descriptor;
        IntPtr text = Utf8.Allocate(serialized);
        try
        {
            PartyInterop.Check(
                NativePlayFab.PartyDeserializeNetworkDescriptor((byte*)text, &descriptor));
        }
        finally
        {
            Utf8.Free(text);
        }

        return new PartyNetworkDescriptor(&descriptor);
    }

    internal unsafe void WriteTo(PARTY_NETWORK_DESCRIPTOR* target)
    {
        fixed (byte* raw = _raw)
        {
            Buffer.MemoryCopy(raw, target, _raw.Length, _raw.Length);
        }
    }
}

/// <summary>Projects <c>PARTY_NETWORK_CONFIGURATION</c>.</summary>
public sealed class PartyNetworkConfiguration
{
    /// <summary>The maximum number of users allowed in the network.</summary>
    public uint MaxUserCount { get; set; }

    /// <summary>The maximum number of devices allowed in the network.</summary>
    public uint MaxDeviceCount { get; set; }

    /// <summary>The maximum number of users a single device may authenticate.</summary>
    public uint MaxUsersPerDeviceCount { get; set; }

    /// <summary>The maximum number of devices a single user may authenticate from.</summary>
    public uint MaxDevicesPerUserCount { get; set; }

    /// <summary>The maximum number of endpoints a single device may create.</summary>
    public uint MaxEndpointsPerDeviceCount { get; set; }

    /// <summary>Which direct peer-to-peer connections the network may attempt.</summary>
    public PartyDirectPeerConnectivityOptions DirectPeerConnectivityOptions { get; set; }

    internal static unsafe PartyNetworkConfiguration FromNative(
        PARTY_NETWORK_CONFIGURATION* source) =>
        new()
        {
            MaxUserCount = source->MaxUserCount,
            MaxDeviceCount = source->MaxDeviceCount,
            MaxUsersPerDeviceCount = source->MaxUsersPerDeviceCount,
            MaxDevicesPerUserCount = source->MaxDevicesPerUserCount,
            MaxEndpointsPerDeviceCount = source->MaxEndpointsPerDeviceCount,
            DirectPeerConnectivityOptions =
                (PartyDirectPeerConnectivityOptions)source->DirectPeerConnectivityOptions,
        };

    internal unsafe void WriteTo(PARTY_NETWORK_CONFIGURATION* target)
    {
        target->MaxUserCount = MaxUserCount;
        target->MaxDeviceCount = MaxDeviceCount;
        target->MaxUsersPerDeviceCount = MaxUsersPerDeviceCount;
        target->MaxDevicesPerUserCount = MaxDevicesPerUserCount;
        target->MaxEndpointsPerDeviceCount = MaxEndpointsPerDeviceCount;
        target->DirectPeerConnectivityOptions =
            (PARTY_DIRECT_PEER_CONNECTIVITY_OPTIONS)DirectPeerConnectivityOptions;
    }
}

/// <summary>Projects <c>PARTY_INVITATION_CONFIGURATION</c>.</summary>
public sealed class PartyInvitationConfiguration
{
    /// <summary>
    /// The invitation identifier. Leave <see langword="null"/> to have Party generate one.
    /// </summary>
    public string? Identifier { get; set; }

    /// <summary>Who is allowed to revoke the invitation.</summary>
    public PartyInvitationRevocability Revocability { get; set; }

    /// <summary>The PlayFab entity ids the invitation admits; empty means anyone.</summary>
    public IReadOnlyList<string> EntityIds { get; set; } = Array.Empty<string>();

    internal static unsafe PartyInvitationConfiguration FromNative(
        PARTY_INVITATION_CONFIGURATION* source) =>
        new()
        {
            Identifier = Utf8.ToString(source->Identifier),
            Revocability = (PartyInvitationRevocability)source->Revocability,
            EntityIds = PartyInterop.ReadStrings(source->EntityIds, source->EntityIdCount),
        };

    internal unsafe void WriteTo(PARTY_INVITATION_CONFIGURATION* target, PlayFabArena arena)
    {
        *target = default;
        target->Identifier = arena.String(Identifier);
        target->Revocability = (PARTY_INVITATION_REVOCABILITY)Revocability;
        IReadOnlyList<string> ids = EntityIds ?? Array.Empty<string>();
        target->EntityIdCount = (uint)ids.Count;
        if (ids.Count > 0)
        {
            var boxed = new string?[ids.Count];
            for (int i = 0; i < ids.Count; i++)
            {
                boxed[i] = ids[i];
            }

            target->EntityIds = arena.StringArray(boxed);
        }
    }
}

/// <summary>Projects <c>PARTY_SEND_MESSAGE_QUEUING_CONFIGURATION</c>.</summary>
public sealed class PartySendMessageQueuingConfiguration
{
    /// <summary>Relative send priority; higher values are sent first.</summary>
    public sbyte Priority { get; set; }

    /// <summary>An arbitrary tag matched by the cancel filters.</summary>
    public uint IdentityForCancelFilters { get; set; }

    /// <summary>How long the message may sit queued before it is dropped.</summary>
    public TimeSpan Timeout { get; set; }

    internal unsafe void WriteTo(PARTY_SEND_MESSAGE_QUEUING_CONFIGURATION* target)
    {
        target->Priority = Priority;
        target->IdentityForCancelFilters = IdentityForCancelFilters;
        target->TimeoutInMilliseconds =
            (uint)Math.Max(0, Math.Min(uint.MaxValue, Timeout.TotalMilliseconds));
    }
}

/// <summary>Projects <c>PARTY_LOCAL_UDP_SOCKET_BIND_ADDRESS_CONFIGURATION</c>.</summary>
public sealed class PartyLocalUdpSocketBindAddressConfiguration
{
    /// <summary>How the bind address is chosen.</summary>
    public PartyLocalUdpSocketBindAddressOptions Options { get; set; }

    /// <summary>The UDP port to bind, or 0 to let the platform choose.</summary>
    public ushort Port { get; set; }

    internal unsafe void WriteTo(PARTY_LOCAL_UDP_SOCKET_BIND_ADDRESS_CONFIGURATION* target)
    {
        target->Options = (PARTY_LOCAL_UDP_SOCKET_BIND_ADDRESS_OPTIONS)Options;
        target->Port = Port;
    }

    internal static unsafe PartyLocalUdpSocketBindAddressConfiguration FromNative(
        PARTY_LOCAL_UDP_SOCKET_BIND_ADDRESS_CONFIGURATION* source) =>
        new()
        {
            Options = (PartyLocalUdpSocketBindAddressOptions)source->Options,
            Port = source->Port,
        };
}

/// <summary>Projects <c>PARTY_REGION_UPDATE_CONFIGURATION</c>.</summary>
public sealed class PartyRegionUpdateConfiguration
{
    /// <summary>When Party refreshes its region latency measurements.</summary>
    public PartyRegionUpdateMode Mode { get; set; }

    /// <summary>How often the measurements are refreshed in deferred mode.</summary>
    public TimeSpan RefreshInterval { get; set; }

    internal unsafe void WriteTo(PARTY_REGION_UPDATE_CONFIGURATION* target)
    {
        target->Mode = (PARTY_REGION_UPDATE_MODE)Mode;
        target->RefreshIntervalInSeconds =
            (uint)Math.Max(0, Math.Min(uint.MaxValue, RefreshInterval.TotalSeconds));
    }

    internal static unsafe PartyRegionUpdateConfiguration FromNative(
        PARTY_REGION_UPDATE_CONFIGURATION* source) =>
        new()
        {
            Mode = (PartyRegionUpdateMode)source->Mode,
            RefreshInterval = TimeSpan.FromSeconds(source->RefreshIntervalInSeconds),
        };
}

/// <summary>Projects <c>PARTY_REGION_QUALITY_MEASUREMENT_CONFIGURATION</c>.</summary>
public sealed class PartyRegionQualityMeasurementConfiguration
{
    /// <summary>The overall budget for a full measurement pass.</summary>
    public TimeSpan TotalMeasurementTimeout { get; set; }

    /// <summary>The latency above which a region is treated as high-latency.</summary>
    public ushort HighLatencyHintInMilliseconds { get; set; }

    /// <summary>The minimum number of successful responses required per region.</summary>
    public ushort MinRequiredSuccessfulResponses { get; set; }

    /// <summary>The number of successful responses Party aims for per region.</summary>
    public ushort IdealNumberOfSuccessfulResponses { get; set; }

    /// <summary>How many times a silent region is retried.</summary>
    public ushort MaxRetriesWithNoResponse { get; set; }

    /// <summary>How many timeouts are tolerated after the first response.</summary>
    public ushort MaxTimeoutsAfterResponse { get; set; }

    internal unsafe void WriteTo(PARTY_REGION_QUALITY_MEASUREMENT_CONFIGURATION* target)
    {
        target->TotalMeasurementTimeoutInMilliseconds =
            (uint)Math.Max(0, Math.Min(uint.MaxValue, TotalMeasurementTimeout.TotalMilliseconds));
        target->HighLatencyHintInMilliseconds = HighLatencyHintInMilliseconds;
        target->MinRequiredSuccessfulResponses = MinRequiredSuccessfulResponses;
        target->IdealNumberOfSuccessfulResponses = IdealNumberOfSuccessfulResponses;
        target->MaxRetriesWithNoResponse = MaxRetriesWithNoResponse;
        target->MaxTimeoutsAfterResponse = MaxTimeoutsAfterResponse;
    }
}

/// <summary>Projects <c>PARTY_AUDIO_FORMAT</c>.</summary>
public sealed class PartyAudioFormat
{
    /// <summary>The sample rate in hertz.</summary>
    public uint SamplesPerSecond { get; set; }

    /// <summary>The speaker-position mask describing the channel layout.</summary>
    public uint ChannelMask { get; set; }

    /// <summary>The number of channels.</summary>
    public ushort ChannelCount { get; set; }

    /// <summary>The number of bits in a single sample.</summary>
    public ushort BitsPerSample { get; set; }

    /// <summary>Whether samples are integer or floating point.</summary>
    public PartyAudioSampleType SampleType { get; set; }

    /// <summary>Whether channels are interleaved within a buffer.</summary>
    public bool Interleaved { get; set; }

    internal static unsafe PartyAudioFormat FromNative(PARTY_AUDIO_FORMAT* source) =>
        new()
        {
            SamplesPerSecond = source->SamplesPerSecond,
            ChannelMask = source->ChannelMask,
            ChannelCount = source->ChannelCount,
            BitsPerSample = source->BitsPerSample,
            SampleType = (PartyAudioSampleType)source->SampleType,
            Interleaved = PartyInterop.ToBool(source->Interleaved),
        };

    internal unsafe void WriteTo(PARTY_AUDIO_FORMAT* target)
    {
        target->SamplesPerSecond = SamplesPerSecond;
        target->ChannelMask = ChannelMask;
        target->ChannelCount = ChannelCount;
        target->BitsPerSample = BitsPerSample;
        target->SampleType = (PARTY_AUDIO_SAMPLE_TYPE)SampleType;
        target->Interleaved = PartyInterop.FromBool(Interleaved);
    }
}

/// <summary>Projects <c>PARTY_AUDIO_MANIPULATION_SOURCE_STREAM_CONFIGURATION</c>.</summary>
public sealed class PartyAudioManipulationSourceStreamConfiguration
{
    /// <summary>The requested format, or <see langword="null"/> for Party's default.</summary>
    public PartyAudioFormat? Format { get; set; }

    /// <summary>How much audio the stream may buffer before it drops data.</summary>
    public TimeSpan MaxTotalAudioBufferSize { get; set; }

    internal static unsafe PartyAudioManipulationSourceStreamConfiguration FromNative(
        PARTY_AUDIO_MANIPULATION_SOURCE_STREAM_CONFIGURATION* source) =>
        new()
        {
            Format = source->Format is null ? null : PartyAudioFormat.FromNative(source->Format),
            MaxTotalAudioBufferSize =
                TimeSpan.FromMilliseconds(source->MaxTotalAudioBufferSizeInMilliseconds),
        };

    internal unsafe void WriteTo(
        PARTY_AUDIO_MANIPULATION_SOURCE_STREAM_CONFIGURATION* target, PlayFabArena arena)
    {
        *target = default;
        if (Format is not null)
        {
            target->Format = arena.Alloc<PARTY_AUDIO_FORMAT>(1);
            Format.WriteTo(target->Format);
        }

        target->MaxTotalAudioBufferSizeInMilliseconds =
            (uint)Math.Max(0, Math.Min(uint.MaxValue, MaxTotalAudioBufferSize.TotalMilliseconds));
    }
}

/// <summary>Projects <c>PARTY_AUDIO_MANIPULATION_SINK_STREAM_CONFIGURATION</c>.</summary>
public sealed class PartyAudioManipulationSinkStreamConfiguration
{
    /// <summary>The requested format, or <see langword="null"/> for Party's default.</summary>
    public PartyAudioFormat? Format { get; set; }

    /// <summary>How much audio the stream may buffer before it drops data.</summary>
    public TimeSpan MaxTotalAudioBufferSize { get; set; }

    internal static unsafe PartyAudioManipulationSinkStreamConfiguration FromNative(
        PARTY_AUDIO_MANIPULATION_SINK_STREAM_CONFIGURATION* source) =>
        new()
        {
            Format = source->Format is null ? null : PartyAudioFormat.FromNative(source->Format),
            MaxTotalAudioBufferSize =
                TimeSpan.FromMilliseconds(source->MaxTotalAudioBufferSizeInMilliseconds),
        };

    internal unsafe void WriteTo(
        PARTY_AUDIO_MANIPULATION_SINK_STREAM_CONFIGURATION* target, PlayFabArena arena)
    {
        *target = default;
        if (Format is not null)
        {
            target->Format = arena.Alloc<PARTY_AUDIO_FORMAT>(1);
            Format.WriteTo(target->Format);
        }

        target->MaxTotalAudioBufferSizeInMilliseconds =
            (uint)Math.Max(0, Math.Min(uint.MaxValue, MaxTotalAudioBufferSize.TotalMilliseconds));
    }
}

/// <summary>Projects <c>PARTY_TRANSLATION</c>: one machine translation of a chat message.</summary>
public sealed class PartyTranslation
{
    private PartyTranslation(
        PartyStateChangeResult result,
        uint errorDetail,
        string languageCode,
        PartyTranslationReceivedOptions options,
        string? translation)
    {
        Result = result;
        ErrorDetail = errorDetail;
        LanguageCode = languageCode;
        Options = options;
        Translation = translation;
    }

    /// <summary>Whether the translation succeeded.</summary>
    public PartyStateChangeResult Result { get; }

    /// <summary>The <c>PartyError</c> detail when <see cref="Result"/> is not success.</summary>
    public uint ErrorDetail { get; }

    /// <summary>The language the text was translated into.</summary>
    public string LanguageCode { get; }

    /// <summary>Flags describing the translation, such as profanity masking.</summary>
    public PartyTranslationReceivedOptions Options { get; }

    /// <summary>The translated text, or <see langword="null"/> when translation failed.</summary>
    public string? Translation { get; }

    internal static unsafe PartyTranslation FromNative(PARTY_TRANSLATION* source) =>
        new(
            (PartyStateChangeResult)source->Result,
            source->ErrorDetail,
            Utf8.ToString(source->LanguageCode) ?? string.Empty,
            (PartyTranslationReceivedOptions)source->Options,
            Utf8.ToString(source->Translation));

    internal static unsafe IReadOnlyList<PartyTranslation> ReadArray(
        PARTY_TRANSLATION* source, uint count)
    {
        if (source is null || count == 0)
        {
            return Array.Empty<PartyTranslation>();
        }

        var result = new PartyTranslation[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = FromNative(&source[i]);
        }

        return result;
    }
}
