using System;
using System.Collections.Generic;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// One entry from a <see cref="PartyManager.ProcessStateChanges"/> batch.
/// </summary>
/// <remarks>
/// Party reports everything — operation completions, membership changes, chat and messaging —
/// through a single poll-drain queue, so the loop is the public surface. Every record snapshots
/// its values, so it stays valid after the batch is returned to Party; the Party objects it
/// references are invalidated when their teardown change is processed.
/// </remarks>
/// <param name="Kind">The discriminator matching the native <c>PartyStateChangeType</c>.</param>
public abstract record PartyStateChange(PartyStateChangeType Kind);

/// <summary>A state change that completes an operation the title started.</summary>
/// <param name="Kind">The discriminator matching the native <c>PartyStateChangeType</c>.</param>
/// <param name="Operation">The id returned by the matching start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the operation failed.</param>
public abstract record PartyOperationCompleted(
    PartyStateChangeType Kind,
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail)
    : PartyStateChange(Kind)
{
    /// <summary>Whether the operation succeeded.</summary>
    public bool Succeeded => Result == PartyStateChangeResult.Succeeded;

    /// <summary>The failure, or <see langword="null"/> when the operation succeeded.</summary>
    public Exception? Error => Succeeded
        ? null
        : new PartyException(ErrorDetail, PartyInterop.DescribeParty(ErrorDetail));
}

/// <summary>Party finished measuring latency to the Azure regions.</summary>
/// <param name="Result">Whether the measurement succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Regions">The regions and their measured latencies.</param>
public sealed record PartyRegionsChanged(
    PartyStateChangeResult Result, uint ErrorDetail, IReadOnlyList<PartyRegion> Regions)
    : PartyOperationCompleted(
        PartyStateChangeType.RegionsChanged, PartyOperationId.None, Result, ErrorDetail);

/// <summary>A <c>PartyManager.CreateNewNetwork</c> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="LocalUser">The user that created the network.</param>
/// <param name="Configuration">The network's configuration.</param>
/// <param name="Regions">The regions the network was allowed to use.</param>
/// <param name="Descriptor">The descriptor other devices need in order to connect.</param>
/// <param name="AppliedInitialInvitationIdentifier">The identifier Party gave the initial invitation.</param>
public sealed record PartyCreateNewNetworkCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyLocalUser? LocalUser,
    PartyNetworkConfiguration Configuration,
    IReadOnlyList<PartyRegion> Regions,
    PartyNetworkDescriptor Descriptor,
    string? AppliedInitialInvitationIdentifier)
    : PartyOperationCompleted(
        PartyStateChangeType.CreateNewNetworkCompleted, Operation, Result, ErrorDetail);

/// <summary>A <c>PartyManager.ConnectToNetwork</c> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Descriptor">The descriptor that was connected to.</param>
/// <param name="Network">The connected network, when the call succeeded.</param>
public sealed record PartyConnectToNetworkCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetworkDescriptor Descriptor,
    PartyNetwork? Network)
    : PartyOperationCompleted(
        PartyStateChangeType.ConnectToNetworkCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyNetwork.AuthenticateLocalUser"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network the user was authenticating into.</param>
/// <param name="LocalUser">The user that was authenticating.</param>
/// <param name="InvitationIdentifier">The invitation that admitted the user.</param>
public sealed record PartyAuthenticateLocalUserCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    PartyLocalUser? LocalUser,
    string? InvitationIdentifier)
    : PartyOperationCompleted(
        PartyStateChangeType.AuthenticateLocalUserCompleted, Operation, Result, ErrorDetail);

/// <summary>The network's configuration became readable.</summary>
/// <param name="Network">The network.</param>
/// <param name="Configuration">The configuration.</param>
public sealed record PartyNetworkConfigurationMadeAvailable(
    PartyNetwork? Network, PartyNetworkConfiguration Configuration)
    : PartyStateChange(PartyStateChangeType.NetworkConfigurationMadeAvailable);

/// <summary>The network's descriptor changed and must be redistributed to joiners.</summary>
/// <param name="Network">The network.</param>
public sealed record PartyNetworkDescriptorChanged(PartyNetwork? Network)
    : PartyStateChange(PartyStateChangeType.NetworkDescriptorChanged);

/// <summary>A local user left the network.</summary>
/// <param name="Network">The network.</param>
/// <param name="LocalUser">The user that left.</param>
/// <param name="Reason">Why the user left.</param>
public sealed record PartyLocalUserRemoved(
    PartyNetwork? Network, PartyLocalUser? LocalUser, PartyLocalUserRemovedReason Reason)
    : PartyStateChange(PartyStateChangeType.LocalUserRemoved);

/// <summary>A <see cref="PartyNetwork.RemoveLocalUser"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network the user was removed from.</param>
/// <param name="LocalUser">The user that was removed.</param>
public sealed record PartyRemoveLocalUserCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    PartyLocalUser? LocalUser)
    : PartyOperationCompleted(
        PartyStateChangeType.RemoveLocalUserCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyManager.DestroyLocalUser"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="LocalUser">The user that was destroyed.</param>
public sealed record PartyDestroyLocalUserCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyLocalUser? LocalUser)
    : PartyOperationCompleted(
        PartyStateChangeType.DestroyLocalUserCompleted, Operation, Result, ErrorDetail);

/// <summary>A local user was kicked from the network by its owner.</summary>
/// <param name="Network">The network.</param>
/// <param name="LocalUser">The user that was kicked.</param>
public sealed record PartyLocalUserKicked(PartyNetwork? Network, PartyLocalUser? LocalUser)
    : PartyStateChange(PartyStateChangeType.LocalUserKicked);

/// <summary>A <see cref="PartyNetwork.CreateEndpoint"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network the endpoint was created in.</param>
/// <param name="LocalUser">The user that owns the endpoint, if any.</param>
/// <param name="Endpoint">The created endpoint, when the call succeeded.</param>
public sealed record PartyCreateEndpointCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    PartyLocalUser? LocalUser,
    PartyEndpoint? Endpoint)
    : PartyOperationCompleted(
        PartyStateChangeType.CreateEndpointCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyNetwork.DestroyEndpoint"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network the endpoint belonged to.</param>
/// <param name="Endpoint">The endpoint that was destroyed.</param>
public sealed record PartyDestroyEndpointCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    PartyEndpoint? Endpoint)
    : PartyOperationCompleted(
        PartyStateChangeType.DestroyEndpointCompleted, Operation, Result, ErrorDetail);

/// <summary>An endpoint appeared in the network.</summary>
/// <param name="Network">The network.</param>
/// <param name="Endpoint">The new endpoint.</param>
public sealed record PartyEndpointCreated(PartyNetwork? Network, PartyEndpoint? Endpoint)
    : PartyStateChange(PartyStateChangeType.EndpointCreated);

/// <summary>An endpoint left the network.</summary>
/// <param name="Network">The network.</param>
/// <param name="Endpoint">The endpoint that went away.</param>
/// <param name="Reason">Why it went away.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the teardown was involuntary.</param>
public sealed record PartyEndpointDestroyed(
    PartyNetwork? Network, PartyEndpoint? Endpoint, PartyDestroyedReason Reason, uint ErrorDetail)
    : PartyStateChange(PartyStateChangeType.EndpointDestroyed);

/// <summary>A remote device became known to the library.</summary>
/// <param name="Device">The device.</param>
public sealed record PartyRemoteDeviceCreated(PartyDevice? Device)
    : PartyStateChange(PartyStateChangeType.RemoteDeviceCreated);

/// <summary>A remote device is no longer known to the library.</summary>
/// <param name="Device">The device.</param>
public sealed record PartyRemoteDeviceDestroyed(PartyDevice? Device)
    : PartyStateChange(PartyStateChangeType.RemoteDeviceDestroyed);

/// <summary>A remote device joined a network.</summary>
/// <param name="Device">The device.</param>
/// <param name="Network">The network it joined.</param>
public sealed record PartyRemoteDeviceJoinedNetwork(PartyDevice? Device, PartyNetwork? Network)
    : PartyStateChange(PartyStateChangeType.RemoteDeviceJoinedNetwork);

/// <summary>A remote device left a network.</summary>
/// <param name="Device">The device.</param>
/// <param name="Network">The network it left.</param>
/// <param name="Reason">Why it left.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the departure was involuntary.</param>
public sealed record PartyRemoteDeviceLeftNetwork(
    PartyDevice? Device, PartyNetwork? Network, PartyDestroyedReason Reason, uint ErrorDetail)
    : PartyStateChange(PartyStateChangeType.RemoteDeviceLeftNetwork);

/// <summary>Properties shared on a device changed.</summary>
/// <param name="Device">The device.</param>
/// <param name="Keys">The keys whose values changed.</param>
public sealed record PartyDevicePropertiesChanged(
    PartyDevice? Device, IReadOnlyList<string> Keys)
    : PartyStateChange(PartyStateChangeType.DevicePropertiesChanged);

/// <summary>A <see cref="PartyNetwork.Leave"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network that was left.</param>
public sealed record PartyLeaveNetworkCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network)
    : PartyOperationCompleted(
        PartyStateChangeType.LeaveNetworkCompleted, Operation, Result, ErrorDetail);

/// <summary>A network the local device belonged to was torn down.</summary>
/// <param name="Network">The network.</param>
/// <param name="Reason">Why it was torn down.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the teardown was involuntary.</param>
public sealed record PartyNetworkDestroyed(
    PartyNetwork? Network, PartyDestroyedReason Reason, uint ErrorDetail)
    : PartyStateChange(PartyStateChangeType.NetworkDestroyed);

/// <summary>A message arrived from a remote endpoint.</summary>
/// <param name="Network">The network the message arrived on.</param>
/// <param name="SenderEndpoint">The endpoint that sent it.</param>
/// <param name="ReceiverEndpoints">The local endpoints the message was addressed to.</param>
/// <param name="Options">Delivery details for the message.</param>
/// <param name="Message">The payload, copied into managed memory.</param>
public sealed record PartyEndpointMessageReceived(
    PartyNetwork? Network,
    PartyEndpoint? SenderEndpoint,
    IReadOnlyList<PartyEndpoint> ReceiverEndpoints,
    PartyMessageReceivedOptions Options,
    byte[] Message)
    : PartyStateChange(PartyStateChangeType.EndpointMessageReceived);

/// <summary>Party finished with the buffers submitted by a send.</summary>
/// <param name="Network">The network.</param>
/// <param name="LocalSenderEndpoint">The endpoint that sent the message.</param>
/// <param name="Operation">The id returned by <see cref="PartyEndpoint.SendMessage"/>.</param>
public sealed record PartyDataBuffersReturned(
    PartyNetwork? Network, PartyEndpoint? LocalSenderEndpoint, PartyOperationId Operation)
    : PartyStateChange(PartyStateChangeType.DataBuffersReturned);

/// <summary>Properties shared on an endpoint changed.</summary>
/// <param name="Endpoint">The endpoint.</param>
/// <param name="Keys">The keys whose values changed.</param>
public sealed record PartyEndpointPropertiesChanged(
    PartyEndpoint? Endpoint, IReadOnlyList<string> Keys)
    : PartyStateChange(PartyStateChangeType.EndpointPropertiesChanged);

/// <summary>A <see cref="PartyManager.SynchronizeMessagesBetweenEndpoints"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Endpoints">The endpoints that were synchronized.</param>
/// <param name="Options">The synchronization options that were requested.</param>
public sealed record PartySynchronizeMessagesBetweenEndpointsCompleted(
    PartyOperationId Operation,
    IReadOnlyList<PartyEndpoint> Endpoints,
    PartySynchronizeMessagesBetweenEndpointsOptions Options)
    : PartyStateChange(PartyStateChangeType.SynchronizeMessagesBetweenEndpointsCompleted);

/// <summary>A <see cref="PartyNetwork.CreateInvitation"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network the invitation belongs to.</param>
/// <param name="LocalUser">The user that created it.</param>
/// <param name="Invitation">The created invitation, when the call succeeded.</param>
public sealed record PartyCreateInvitationCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    PartyLocalUser? LocalUser,
    PartyInvitation? Invitation)
    : PartyOperationCompleted(
        PartyStateChangeType.CreateInvitationCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyNetwork.RevokeInvitation"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network the invitation belonged to.</param>
/// <param name="LocalUser">The user that revoked it.</param>
/// <param name="Invitation">The revoked invitation.</param>
public sealed record PartyRevokeInvitationCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    PartyLocalUser? LocalUser,
    PartyInvitation? Invitation)
    : PartyOperationCompleted(
        PartyStateChangeType.RevokeInvitationCompleted, Operation, Result, ErrorDetail);

/// <summary>An invitation to the network became known to the local device.</summary>
/// <param name="Network">The network.</param>
/// <param name="Invitation">The invitation.</param>
public sealed record PartyInvitationCreated(PartyNetwork? Network, PartyInvitation? Invitation)
    : PartyStateChange(PartyStateChangeType.InvitationCreated);

/// <summary>An invitation to the network is no longer valid.</summary>
/// <param name="Network">The network.</param>
/// <param name="Invitation">The invitation.</param>
/// <param name="Reason">Why it went away.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the teardown was involuntary.</param>
public sealed record PartyInvitationDestroyed(
    PartyNetwork? Network,
    PartyInvitation? Invitation,
    PartyDestroyedReason Reason,
    uint ErrorDetail)
    : PartyStateChange(PartyStateChangeType.InvitationDestroyed);

/// <summary>Properties shared on a network changed.</summary>
/// <param name="Network">The network.</param>
/// <param name="Keys">The keys whose values changed.</param>
public sealed record PartyNetworkPropertiesChanged(
    PartyNetwork? Network, IReadOnlyList<string> Keys)
    : PartyStateChange(PartyStateChangeType.NetworkPropertiesChanged);

/// <summary>A <see cref="PartyNetwork.KickDevice"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network.</param>
/// <param name="KickedDevice">The device that was kicked.</param>
public sealed record PartyKickDeviceCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    PartyDevice? KickedDevice)
    : PartyOperationCompleted(
        PartyStateChangeType.KickDeviceCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyNetwork.KickUser"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network.</param>
/// <param name="KickedEntityId">The entity id that was kicked.</param>
public sealed record PartyKickUserCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    string? KickedEntityId)
    : PartyOperationCompleted(
        PartyStateChangeType.KickUserCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyDevice.CreateChatControl"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="LocalDevice">The device hosting the chat control.</param>
/// <param name="LocalUser">The user the chat control speaks for.</param>
/// <param name="LanguageCode">The language the chat control was created with.</param>
/// <param name="ChatControl">The created chat control, when the call succeeded.</param>
public sealed record PartyCreateChatControlCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyDevice? LocalDevice,
    PartyLocalUser? LocalUser,
    string? LanguageCode,
    PartyChatControl? ChatControl)
    : PartyOperationCompleted(
        PartyStateChangeType.CreateChatControlCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyDevice.DestroyChatControl"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="LocalDevice">The device that hosted the chat control.</param>
/// <param name="ChatControl">The chat control that was destroyed.</param>
public sealed record PartyDestroyChatControlCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyDevice? LocalDevice,
    PartyChatControl? ChatControl)
    : PartyOperationCompleted(
        PartyStateChangeType.DestroyChatControlCompleted, Operation, Result, ErrorDetail);

/// <summary>A chat control became known to the local device.</summary>
/// <param name="ChatControl">The chat control.</param>
public sealed record PartyChatControlCreated(PartyChatControl? ChatControl)
    : PartyStateChange(PartyStateChangeType.ChatControlCreated);

/// <summary>A chat control is no longer known to the local device.</summary>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Reason">Why it went away.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the teardown was involuntary.</param>
public sealed record PartyChatControlDestroyed(
    PartyChatControl? ChatControl, PartyDestroyedReason Reason, uint ErrorDetail)
    : PartyStateChange(PartyStateChangeType.ChatControlDestroyed);

/// <summary>A <see cref="PartyChatControl.SetAudioEncoderBitrate"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Bitrate">The bitrate that was requested.</param>
public sealed record PartySetChatAudioEncoderBitrateCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    uint Bitrate)
    : PartyOperationCompleted(
        PartyStateChangeType.SetChatAudioEncoderBitrateCompleted, Operation, Result, ErrorDetail);

/// <summary>A chat message arrived.</summary>
/// <param name="SenderChatControl">The chat control that sent it.</param>
/// <param name="ReceiverChatControls">The local chat controls it was addressed to.</param>
/// <param name="LanguageCode">The language the sender wrote in.</param>
/// <param name="ChatText">The message text, after any filtering.</param>
/// <param name="OriginalChatText">The unfiltered text, when filtering changed it.</param>
/// <param name="Data">Opaque data the sender attached.</param>
/// <param name="Translations">Machine translations Party generated.</param>
/// <param name="Options">Details about how the message was processed.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when processing partly failed.</param>
public sealed record PartyChatTextReceived(
    PartyChatControl? SenderChatControl,
    IReadOnlyList<PartyChatControl> ReceiverChatControls,
    string? LanguageCode,
    string? ChatText,
    string? OriginalChatText,
    byte[] Data,
    IReadOnlyList<PartyTranslation> Translations,
    PartyChatTextReceivedOptions Options,
    uint ErrorDetail)
    : PartyStateChange(PartyStateChangeType.ChatTextReceived);

/// <summary>A voice transcription arrived.</summary>
/// <param name="Result">Whether transcription succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="SenderChatControl">The chat control that was speaking.</param>
/// <param name="ReceiverChatControls">The local chat controls the transcription was delivered to.</param>
/// <param name="SourceType">Whether the audio came from a microphone or from synthesis.</param>
/// <param name="LanguageCode">The language that was transcribed.</param>
/// <param name="Transcription">The transcribed text.</param>
/// <param name="PhraseType">Whether the phrase is a hypothesis or final.</param>
/// <param name="Translations">Machine translations Party generated.</param>
public sealed record PartyVoiceChatTranscriptionReceived(
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? SenderChatControl,
    IReadOnlyList<PartyChatControl> ReceiverChatControls,
    PartyAudioSourceType SourceType,
    string? LanguageCode,
    string? Transcription,
    PartyVoiceChatTranscriptionPhraseType PhraseType,
    IReadOnlyList<PartyTranslation> Translations)
    : PartyStateChange(PartyStateChangeType.VoiceChatTranscriptionReceived);

/// <summary>A <see cref="PartyChatControl.SetAudioInput"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Selection">The device selection that was requested.</param>
public sealed record PartySetChatAudioInputCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    PartyAudioDeviceSelection Selection)
    : PartyOperationCompleted(
        PartyStateChangeType.SetChatAudioInputCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyChatControl.SetAudioOutput"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Selection">The device selection that was requested.</param>
public sealed record PartySetChatAudioOutputCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    PartyAudioDeviceSelection Selection)
    : PartyOperationCompleted(
        PartyStateChangeType.SetChatAudioOutputCompleted, Operation, Result, ErrorDetail);

/// <summary>The local capture device's state changed.</summary>
/// <param name="ChatControl">The chat control.</param>
/// <param name="State">The new state.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the device failed.</param>
public sealed record PartyLocalChatAudioInputChanged(
    PartyChatControl? ChatControl, PartyAudioInputState State, uint ErrorDetail)
    : PartyStateChange(PartyStateChangeType.LocalChatAudioInputChanged);

/// <summary>The local render device's state changed.</summary>
/// <param name="ChatControl">The chat control.</param>
/// <param name="State">The new state.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the device failed.</param>
public sealed record PartyLocalChatAudioOutputChanged(
    PartyChatControl? ChatControl, PartyAudioOutputState State, uint ErrorDetail)
    : PartyStateChange(PartyStateChangeType.LocalChatAudioOutputChanged);

/// <summary>A <see cref="PartyChatControl.SetTextToSpeechProfile"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Type">Which synthesis pipeline the profile applies to.</param>
/// <param name="ProfileIdentifier">The profile that was requested.</param>
public sealed record PartySetTextToSpeechProfileCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    PartySynthesizeTextToSpeechType Type,
    string? ProfileIdentifier)
    : PartyOperationCompleted(
        PartyStateChangeType.SetTextToSpeechProfileCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyChatControl.SynthesizeTextToSpeech"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Type">Which synthesis pipeline was used.</param>
/// <param name="TextToSynthesize">The text that was spoken.</param>
public sealed record PartySynthesizeTextToSpeechCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    PartySynthesizeTextToSpeechType Type,
    string? TextToSynthesize)
    : PartyOperationCompleted(
        PartyStateChangeType.SynthesizeTextToSpeechCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyChatControl.SetLanguage"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="LanguageCode">The language that was requested.</param>
public sealed record PartySetLanguageCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    string? LanguageCode)
    : PartyOperationCompleted(
        PartyStateChangeType.SetLanguageCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyChatControl.SetTranscriptionOptions"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Options">The options that were requested.</param>
public sealed record PartySetTranscriptionOptionsCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    PartyVoiceChatTranscriptionOptions Options)
    : PartyOperationCompleted(
        PartyStateChangeType.SetTranscriptionOptionsCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyChatControl.SetTextChatOptions"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Options">The options that were requested.</param>
public sealed record PartySetTextChatOptionsCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    PartyTextChatOptions Options)
    : PartyOperationCompleted(
        PartyStateChangeType.SetTextChatOptionsCompleted, Operation, Result, ErrorDetail);

/// <summary>Properties shared on a chat control changed.</summary>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Keys">The keys whose values changed.</param>
public sealed record PartyChatControlPropertiesChanged(
    PartyChatControl? ChatControl, IReadOnlyList<string> Keys)
    : PartyStateChange(PartyStateChangeType.ChatControlPropertiesChanged);

/// <summary>A chat control joined a network.</summary>
/// <param name="Network">The network.</param>
/// <param name="ChatControl">The chat control.</param>
public sealed record PartyChatControlJoinedNetwork(
    PartyNetwork? Network, PartyChatControl? ChatControl)
    : PartyStateChange(PartyStateChangeType.ChatControlJoinedNetwork);

/// <summary>A chat control left a network.</summary>
/// <param name="Network">The network.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Reason">Why it left.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when the departure was involuntary.</param>
public sealed record PartyChatControlLeftNetwork(
    PartyNetwork? Network,
    PartyChatControl? ChatControl,
    PartyDestroyedReason Reason,
    uint ErrorDetail)
    : PartyStateChange(PartyStateChangeType.ChatControlLeftNetwork);

/// <summary>A <see cref="PartyNetwork.ConnectChatControl"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network.</param>
/// <param name="ChatControl">The chat control.</param>
public sealed record PartyConnectChatControlCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    PartyChatControl? ChatControl)
    : PartyOperationCompleted(
        PartyStateChangeType.ConnectChatControlCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyNetwork.DisconnectChatControl"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="Network">The network.</param>
/// <param name="ChatControl">The chat control.</param>
public sealed record PartyDisconnectChatControlCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyNetwork? Network,
    PartyChatControl? ChatControl)
    : PartyOperationCompleted(
        PartyStateChangeType.DisconnectChatControlCompleted, Operation, Result, ErrorDetail);

/// <summary>A <see cref="PartyChatControl.PopulateAvailableTextToSpeechProfiles"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
public sealed record PartyPopulateAvailableTextToSpeechProfilesCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl)
    : PartyOperationCompleted(
        PartyStateChangeType.PopulateAvailableTextToSpeechProfilesCompleted,
        Operation,
        Result,
        ErrorDetail);

/// <summary>A <see cref="PartyChatControl.ConfigureAudioManipulationVoiceStream"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Configuration">The configuration that was requested, if any.</param>
public sealed record PartyConfigureAudioManipulationVoiceStreamCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    PartyAudioManipulationSourceStreamConfiguration? Configuration)
    : PartyOperationCompleted(
        PartyStateChangeType.ConfigureAudioManipulationVoiceStreamCompleted,
        Operation,
        Result,
        ErrorDetail);

/// <summary>A <see cref="PartyChatControl.ConfigureAudioManipulationCaptureStream"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Configuration">The configuration that was requested, if any.</param>
public sealed record PartyConfigureAudioManipulationCaptureStreamCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    PartyAudioManipulationSinkStreamConfiguration? Configuration)
    : PartyOperationCompleted(
        PartyStateChangeType.ConfigureAudioManipulationCaptureStreamCompleted,
        Operation,
        Result,
        ErrorDetail);

/// <summary>A <see cref="PartyChatControl.ConfigureAudioManipulationRenderStream"/> call completed.</summary>
/// <param name="Operation">The id returned by the start call.</param>
/// <param name="Result">Whether the operation succeeded.</param>
/// <param name="ErrorDetail">The <c>PartyError</c> detail when it failed.</param>
/// <param name="ChatControl">The chat control.</param>
/// <param name="Configuration">The configuration that was requested, if any.</param>
public sealed record PartyConfigureAudioManipulationRenderStreamCompleted(
    PartyOperationId Operation,
    PartyStateChangeResult Result,
    uint ErrorDetail,
    PartyChatControl? ChatControl,
    PartyAudioManipulationSinkStreamConfiguration? Configuration)
    : PartyOperationCompleted(
        PartyStateChangeType.ConfigureAudioManipulationRenderStreamCompleted,
        Operation,
        Result,
        ErrorDetail);
