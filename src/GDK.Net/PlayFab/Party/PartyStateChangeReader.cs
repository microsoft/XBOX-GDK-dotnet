using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// Turns the native tagged union in <c>Party_c.h</c> into the public record hierarchy,
/// snapshotting every value while the batch is still open.
/// </summary>
internal static unsafe class PartyStateChangeReader
{
    internal static PartyStateChange Read(
        PartyManager owner, PARTY_STATE_CHANGE* change, List<PartyObject> retired)
    {
        switch ((PARTY_STATE_CHANGE_TYPE)change->StateChangeType)
        {
            case PARTY_STATE_CHANGE_TYPE.RegionsChanged:
            {
                var typed = (PARTY_REGIONS_CHANGED_STATE_CHANGE*)change;
                return new PartyRegionsChanged(
                    (PartyStateChangeResult)typed->Result, typed->ErrorDetail, owner.GetRegions());
            }

            case PARTY_STATE_CHANGE_TYPE.CreateNewNetworkCompleted:
            {
                var typed = (PARTY_CREATE_NEW_NETWORK_COMPLETED_STATE_CHANGE*)change;
                return new PartyCreateNewNetworkCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.Find<PartyLocalUser>(typed->LocalUser),
                    PartyNetworkConfiguration.FromNative(&typed->NetworkConfiguration),
                    ReadRegions(typed->Regions, typed->RegionCount),
                    new PartyNetworkDescriptor(&typed->NetworkDescriptor),
                    Utf8.ToString(typed->AppliedInitialInvitationIdentifier));
            }

            case PARTY_STATE_CHANGE_TYPE.ConnectToNetworkCompleted:
            {
                var typed = (PARTY_CONNECT_TO_NETWORK_COMPLETED_STATE_CHANGE*)change;
                PartyNetwork? network = owner.WrapOrNull<PartyNetwork>(typed->Network);
                RetireOnFailure(retired, network, typed->Result);
                return new PartyConnectToNetworkCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    new PartyNetworkDescriptor(&typed->NetworkDescriptor),
                    network);
            }

            case PARTY_STATE_CHANGE_TYPE.AuthenticateLocalUserCompleted:
            {
                var typed = (PARTY_AUTHENTICATE_LOCAL_USER_COMPLETED_STATE_CHANGE*)change;
                return new PartyAuthenticateLocalUserCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.Find<PartyLocalUser>(typed->LocalUser),
                    Utf8.ToString(typed->InvitationIdentifier));
            }

            case PARTY_STATE_CHANGE_TYPE.NetworkConfigurationMadeAvailable:
            {
                var typed = (PARTY_NETWORK_CONFIGURATION_MADE_AVAILABLE_STATE_CHANGE*)change;
                return new PartyNetworkConfigurationMadeAvailable(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    PartyNetworkConfiguration.FromNative(typed->NetworkConfiguration));
            }

            case PARTY_STATE_CHANGE_TYPE.NetworkDescriptorChanged:
            {
                var typed = (PARTY_NETWORK_DESCRIPTOR_CHANGED_STATE_CHANGE*)change;
                return new PartyNetworkDescriptorChanged(
                    owner.WrapOrNull<PartyNetwork>(typed->Network));
            }

            case PARTY_STATE_CHANGE_TYPE.LocalUserRemoved:
            {
                var typed = (PARTY_LOCAL_USER_REMOVED_STATE_CHANGE*)change;
                return new PartyLocalUserRemoved(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.Find<PartyLocalUser>(typed->LocalUser),
                    (PartyLocalUserRemovedReason)typed->RemovedReason);
            }

            case PARTY_STATE_CHANGE_TYPE.RemoveLocalUserCompleted:
            {
                var typed = (PARTY_REMOVE_LOCAL_USER_COMPLETED_STATE_CHANGE*)change;
                return new PartyRemoveLocalUserCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.Find<PartyLocalUser>(typed->LocalUser));
            }

            case PARTY_STATE_CHANGE_TYPE.DestroyLocalUserCompleted:
            {
                var typed = (PARTY_DESTROY_LOCAL_USER_COMPLETED_STATE_CHANGE*)change;
                PartyLocalUser? localUser = owner.Find<PartyLocalUser>(typed->LocalUser);
                Retire(retired, localUser);
                return new PartyDestroyLocalUserCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    localUser);
            }

            case PARTY_STATE_CHANGE_TYPE.LocalUserKicked:
            {
                var typed = (PARTY_LOCAL_USER_KICKED_STATE_CHANGE*)change;
                return new PartyLocalUserKicked(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.Find<PartyLocalUser>(typed->LocalUser));
            }

            case PARTY_STATE_CHANGE_TYPE.CreateEndpointCompleted:
            {
                var typed = (PARTY_CREATE_ENDPOINT_COMPLETED_STATE_CHANGE*)change;
                PartyEndpoint? endpoint = owner.WrapOrNull<PartyEndpoint>(typed->LocalEndpoint);
                RetireOnFailure(retired, endpoint, typed->Result);
                return new PartyCreateEndpointCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.Find<PartyLocalUser>(typed->LocalUser),
                    endpoint);
            }

            case PARTY_STATE_CHANGE_TYPE.DestroyEndpointCompleted:
            {
                var typed = (PARTY_DESTROY_ENDPOINT_COMPLETED_STATE_CHANGE*)change;
                return new PartyDestroyEndpointCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyEndpoint>(typed->LocalEndpoint));
            }

            case PARTY_STATE_CHANGE_TYPE.EndpointCreated:
            {
                var typed = (PARTY_ENDPOINT_CREATED_STATE_CHANGE*)change;
                return new PartyEndpointCreated(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyEndpoint>(typed->Endpoint));
            }

            case PARTY_STATE_CHANGE_TYPE.EndpointDestroyed:
            {
                var typed = (PARTY_ENDPOINT_DESTROYED_STATE_CHANGE*)change;
                PartyEndpoint? endpoint = owner.WrapOrNull<PartyEndpoint>(typed->Endpoint);
                Retire(retired, endpoint);
                return new PartyEndpointDestroyed(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    endpoint,
                    (PartyDestroyedReason)typed->Reason,
                    typed->ErrorDetail);
            }

            case PARTY_STATE_CHANGE_TYPE.RemoteDeviceCreated:
            {
                var typed = (PARTY_REMOTE_DEVICE_CREATED_STATE_CHANGE*)change;
                return new PartyRemoteDeviceCreated(owner.WrapOrNull<PartyDevice>(typed->Device));
            }

            case PARTY_STATE_CHANGE_TYPE.RemoteDeviceDestroyed:
            {
                var typed = (PARTY_REMOTE_DEVICE_DESTROYED_STATE_CHANGE*)change;
                PartyDevice? device = owner.WrapOrNull<PartyDevice>(typed->Device);
                Retire(retired, device);
                return new PartyRemoteDeviceDestroyed(device);
            }

            case PARTY_STATE_CHANGE_TYPE.RemoteDeviceJoinedNetwork:
            {
                var typed = (PARTY_REMOTE_DEVICE_JOINED_NETWORK_STATE_CHANGE*)change;
                return new PartyRemoteDeviceJoinedNetwork(
                    owner.WrapOrNull<PartyDevice>(typed->Device),
                    owner.WrapOrNull<PartyNetwork>(typed->Network));
            }

            case PARTY_STATE_CHANGE_TYPE.RemoteDeviceLeftNetwork:
            {
                var typed = (PARTY_REMOTE_DEVICE_LEFT_NETWORK_STATE_CHANGE*)change;
                return new PartyRemoteDeviceLeftNetwork(
                    owner.WrapOrNull<PartyDevice>(typed->Device),
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    (PartyDestroyedReason)typed->Reason,
                    typed->ErrorDetail);
            }

            case PARTY_STATE_CHANGE_TYPE.DevicePropertiesChanged:
            {
                var typed = (PARTY_DEVICE_PROPERTIES_CHANGED_STATE_CHANGE*)change;
                return new PartyDevicePropertiesChanged(
                    owner.WrapOrNull<PartyDevice>(typed->Device),
                    PartyInterop.ReadStrings(typed->Keys, typed->PropertyCount));
            }

            case PARTY_STATE_CHANGE_TYPE.LeaveNetworkCompleted:
            {
                var typed = (PARTY_LEAVE_NETWORK_COMPLETED_STATE_CHANGE*)change;
                return new PartyLeaveNetworkCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network));
            }

            case PARTY_STATE_CHANGE_TYPE.NetworkDestroyed:
            {
                var typed = (PARTY_NETWORK_DESTROYED_STATE_CHANGE*)change;
                PartyNetwork? network = owner.WrapOrNull<PartyNetwork>(typed->Network);
                Retire(retired, network);
                return new PartyNetworkDestroyed(
                    network, (PartyDestroyedReason)typed->Reason, typed->ErrorDetail);
            }

            case PARTY_STATE_CHANGE_TYPE.EndpointMessageReceived:
            {
                var typed = (PARTY_ENDPOINT_MESSAGE_RECEIVED_STATE_CHANGE*)change;
                return new PartyEndpointMessageReceived(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyEndpoint>(typed->SenderEndpoint),
                    owner.WrapEndpoints(typed->ReceiverEndpoints, typed->ReceiverEndpointCount),
                    (PartyMessageReceivedOptions)typed->Options,
                    PartyInterop.ReadBuffer(typed->MessageBuffer, typed->MessageSize));
            }

            case PARTY_STATE_CHANGE_TYPE.DataBuffersReturned:
            {
                var typed = (PARTY_DATA_BUFFERS_RETURNED_STATE_CHANGE*)change;
                return new PartyDataBuffersReturned(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyEndpoint>(typed->LocalSenderEndpoint),
                    PartyManager.FromContext(typed->MessageIdentifier));
            }

            case PARTY_STATE_CHANGE_TYPE.EndpointPropertiesChanged:
            {
                var typed = (PARTY_ENDPOINT_PROPERTIES_CHANGED_STATE_CHANGE*)change;
                return new PartyEndpointPropertiesChanged(
                    owner.WrapOrNull<PartyEndpoint>(typed->Endpoint),
                    PartyInterop.ReadStrings(typed->Keys, typed->PropertyCount));
            }

            case PARTY_STATE_CHANGE_TYPE.SynchronizeMessagesBetweenEndpointsCompleted:
            {
                var typed =
                    (PARTY_SYNCHRONIZE_MESSAGES_BETWEEN_ENDPOINTS_COMPLETED_STATE_CHANGE*)change;
                return new PartySynchronizeMessagesBetweenEndpointsCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    owner.WrapEndpoints(typed->Endpoints, typed->EndpointCount),
                    (PartySynchronizeMessagesBetweenEndpointsOptions)typed->Options);
            }

            case PARTY_STATE_CHANGE_TYPE.CreateInvitationCompleted:
            {
                var typed = (PARTY_CREATE_INVITATION_COMPLETED_STATE_CHANGE*)change;
                PartyInvitation? invitation =
                    owner.WrapOrNull<PartyInvitation>(typed->Invitation);
                RetireOnFailure(retired, invitation, typed->Result);
                return new PartyCreateInvitationCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.Find<PartyLocalUser>(typed->LocalUser),
                    invitation);
            }

            case PARTY_STATE_CHANGE_TYPE.RevokeInvitationCompleted:
            {
                var typed = (PARTY_REVOKE_INVITATION_COMPLETED_STATE_CHANGE*)change;
                return new PartyRevokeInvitationCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.Find<PartyLocalUser>(typed->LocalUser),
                    owner.WrapOrNull<PartyInvitation>(typed->Invitation));
            }

            case PARTY_STATE_CHANGE_TYPE.InvitationCreated:
            {
                var typed = (PARTY_INVITATION_CREATED_STATE_CHANGE*)change;
                return new PartyInvitationCreated(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyInvitation>(typed->Invitation));
            }

            case PARTY_STATE_CHANGE_TYPE.InvitationDestroyed:
            {
                var typed = (PARTY_INVITATION_DESTROYED_STATE_CHANGE*)change;
                PartyInvitation? invitation =
                    owner.WrapOrNull<PartyInvitation>(typed->Invitation);
                Retire(retired, invitation);
                return new PartyInvitationDestroyed(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    invitation,
                    (PartyDestroyedReason)typed->Reason,
                    typed->ErrorDetail);
            }

            case PARTY_STATE_CHANGE_TYPE.NetworkPropertiesChanged:
            {
                var typed = (PARTY_NETWORK_PROPERTIES_CHANGED_STATE_CHANGE*)change;
                return new PartyNetworkPropertiesChanged(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    PartyInterop.ReadStrings(typed->Keys, typed->PropertyCount));
            }

            case PARTY_STATE_CHANGE_TYPE.KickDeviceCompleted:
            {
                var typed = (PARTY_KICK_DEVICE_COMPLETED_STATE_CHANGE*)change;
                return new PartyKickDeviceCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyDevice>(typed->KickedDevice));
            }

            case PARTY_STATE_CHANGE_TYPE.KickUserCompleted:
            {
                var typed = (PARTY_KICK_USER_COMPLETED_STATE_CHANGE*)change;
                return new PartyKickUserCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    Utf8.ToString(typed->KickedEntityId));
            }

            case PARTY_STATE_CHANGE_TYPE.CreateChatControlCompleted:
            {
                var typed = (PARTY_CREATE_CHAT_CONTROL_COMPLETED_STATE_CHANGE*)change;
                PartyChatControl? chatControl =
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl);
                RetireOnFailure(retired, chatControl, typed->Result);
                return new PartyCreateChatControlCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyDevice>(typed->LocalDevice),
                    owner.Find<PartyLocalUser>(typed->LocalUser),
                    Utf8.ToString(typed->LanguageCode),
                    chatControl);
            }

            case PARTY_STATE_CHANGE_TYPE.DestroyChatControlCompleted:
            {
                var typed = (PARTY_DESTROY_CHAT_CONTROL_COMPLETED_STATE_CHANGE*)change;
                return new PartyDestroyChatControlCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyDevice>(typed->LocalDevice),
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl));
            }

            case PARTY_STATE_CHANGE_TYPE.ChatControlCreated:
            {
                var typed = (PARTY_CHAT_CONTROL_CREATED_STATE_CHANGE*)change;
                return new PartyChatControlCreated(
                    owner.WrapOrNull<PartyChatControl>(typed->ChatControl));
            }

            case PARTY_STATE_CHANGE_TYPE.ChatControlDestroyed:
            {
                var typed = (PARTY_CHAT_CONTROL_DESTROYED_STATE_CHANGE*)change;
                PartyChatControl? chatControl =
                    owner.WrapOrNull<PartyChatControl>(typed->ChatControl);
                Retire(retired, chatControl);
                return new PartyChatControlDestroyed(
                    chatControl, (PartyDestroyedReason)typed->Reason, typed->ErrorDetail);
            }

            case PARTY_STATE_CHANGE_TYPE.SetChatAudioEncoderBitrateCompleted:
            {
                var typed = (PARTY_SET_CHAT_AUDIO_ENCODER_BITRATE_COMPLETED_STATE_CHANGE*)change;
                return new PartySetChatAudioEncoderBitrateCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    typed->Bitrate);
            }

            case PARTY_STATE_CHANGE_TYPE.ChatTextReceived:
            {
                var typed = (PARTY_CHAT_TEXT_RECEIVED_STATE_CHANGE*)change;
                return new PartyChatTextReceived(
                    owner.WrapOrNull<PartyChatControl>(typed->SenderChatControl),
                    owner.WrapChatControls(
                        typed->ReceiverChatControls, typed->ReceiverChatControlCount),
                    Utf8.ToString(typed->LanguageCode),
                    Utf8.ToString(typed->ChatText),
                    Utf8.ToString(typed->OriginalChatText),
                    PartyInterop.ReadBuffer(typed->Data, typed->DataSize),
                    PartyTranslation.ReadArray(typed->Translations, typed->TranslationCount),
                    (PartyChatTextReceivedOptions)typed->Options,
                    typed->ErrorDetail);
            }

            case PARTY_STATE_CHANGE_TYPE.VoiceChatTranscriptionReceived:
            {
                var typed = (PARTY_VOICE_CHAT_TRANSCRIPTION_RECEIVED_STATE_CHANGE*)change;
                return new PartyVoiceChatTranscriptionReceived(
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->SenderChatControl),
                    owner.WrapChatControls(
                        typed->ReceiverChatControls, typed->ReceiverChatControlCount),
                    (PartyAudioSourceType)typed->SourceType,
                    Utf8.ToString(typed->LanguageCode),
                    Utf8.ToString(typed->Transcription),
                    (PartyVoiceChatTranscriptionPhraseType)typed->Type,
                    PartyTranslation.ReadArray(typed->Translations, typed->TranslationCount));
            }

            case PARTY_STATE_CHANGE_TYPE.SetChatAudioInputCompleted:
            {
                var typed = (PARTY_SET_CHAT_AUDIO_INPUT_COMPLETED_STATE_CHANGE*)change;
                return new PartySetChatAudioInputCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    new PartyAudioDeviceSelection(
                        (PartyAudioDeviceSelectionType)typed->AudioDeviceSelectionType,
                        Utf8.ToString(typed->AudioDeviceSelectionContext),
                        null));
            }

            case PARTY_STATE_CHANGE_TYPE.SetChatAudioOutputCompleted:
            {
                var typed = (PARTY_SET_CHAT_AUDIO_OUTPUT_COMPLETED_STATE_CHANGE*)change;
                return new PartySetChatAudioOutputCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    new PartyAudioDeviceSelection(
                        (PartyAudioDeviceSelectionType)typed->AudioDeviceSelectionType,
                        Utf8.ToString(typed->AudioDeviceSelectionContext),
                        null));
            }

            case PARTY_STATE_CHANGE_TYPE.LocalChatAudioInputChanged:
            {
                var typed = (PARTY_LOCAL_CHAT_AUDIO_INPUT_CHANGED_STATE_CHANGE*)change;
                return new PartyLocalChatAudioInputChanged(
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    (PartyAudioInputState)typed->State,
                    typed->ErrorDetail);
            }

            case PARTY_STATE_CHANGE_TYPE.LocalChatAudioOutputChanged:
            {
                var typed = (PARTY_LOCAL_CHAT_AUDIO_OUTPUT_CHANGED_STATE_CHANGE*)change;
                return new PartyLocalChatAudioOutputChanged(
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    (PartyAudioOutputState)typed->State,
                    typed->ErrorDetail);
            }

            case PARTY_STATE_CHANGE_TYPE.SetTextToSpeechProfileCompleted:
            {
                var typed = (PARTY_SET_TEXT_TO_SPEECH_PROFILE_COMPLETED_STATE_CHANGE*)change;
                return new PartySetTextToSpeechProfileCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    (PartySynthesizeTextToSpeechType)typed->Type,
                    Utf8.ToString(typed->ProfileIdentifier));
            }

            case PARTY_STATE_CHANGE_TYPE.SynthesizeTextToSpeechCompleted:
            {
                var typed = (PARTY_SYNTHESIZE_TEXT_TO_SPEECH_COMPLETED_STATE_CHANGE*)change;
                return new PartySynthesizeTextToSpeechCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    (PartySynthesizeTextToSpeechType)typed->Type,
                    Utf8.ToString(typed->TextToSynthesize));
            }

            case PARTY_STATE_CHANGE_TYPE.SetLanguageCompleted:
            {
                var typed = (PARTY_SET_LANGUAGE_COMPLETED_STATE_CHANGE*)change;
                return new PartySetLanguageCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    Utf8.ToString(typed->LanguageCode));
            }

            case PARTY_STATE_CHANGE_TYPE.SetTranscriptionOptionsCompleted:
            {
                var typed = (PARTY_SET_TRANSCRIPTION_OPTIONS_COMPLETED_STATE_CHANGE*)change;
                return new PartySetTranscriptionOptionsCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    (PartyVoiceChatTranscriptionOptions)typed->Options);
            }

            case PARTY_STATE_CHANGE_TYPE.SetTextChatOptionsCompleted:
            {
                var typed = (PARTY_SET_TEXT_CHAT_OPTIONS_COMPLETED_STATE_CHANGE*)change;
                return new PartySetTextChatOptionsCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    (PartyTextChatOptions)typed->Options);
            }

            case PARTY_STATE_CHANGE_TYPE.ChatControlPropertiesChanged:
            {
                var typed = (PARTY_CHAT_CONTROL_PROPERTIES_CHANGED_STATE_CHANGE*)change;
                return new PartyChatControlPropertiesChanged(
                    owner.WrapOrNull<PartyChatControl>(typed->ChatControl),
                    PartyInterop.ReadStrings(typed->Keys, typed->PropertyCount));
            }

            case PARTY_STATE_CHANGE_TYPE.ChatControlJoinedNetwork:
            {
                var typed = (PARTY_CHAT_CONTROL_JOINED_NETWORK_STATE_CHANGE*)change;
                return new PartyChatControlJoinedNetwork(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyChatControl>(typed->ChatControl));
            }

            case PARTY_STATE_CHANGE_TYPE.ChatControlLeftNetwork:
            {
                var typed = (PARTY_CHAT_CONTROL_LEFT_NETWORK_STATE_CHANGE*)change;
                return new PartyChatControlLeftNetwork(
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyChatControl>(typed->ChatControl),
                    (PartyDestroyedReason)typed->Reason,
                    typed->ErrorDetail);
            }

            case PARTY_STATE_CHANGE_TYPE.ConnectChatControlCompleted:
            {
                var typed = (PARTY_CONNECT_CHAT_CONTROL_COMPLETED_STATE_CHANGE*)change;
                return new PartyConnectChatControlCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl));
            }

            case PARTY_STATE_CHANGE_TYPE.DisconnectChatControlCompleted:
            {
                var typed = (PARTY_DISCONNECT_CHAT_CONTROL_COMPLETED_STATE_CHANGE*)change;
                return new PartyDisconnectChatControlCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyNetwork>(typed->Network),
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl));
            }

            case PARTY_STATE_CHANGE_TYPE.PopulateAvailableTextToSpeechProfilesCompleted:
            {
                var typed =
                    (PARTY_POPULATE_AVAILABLE_TEXT_TO_SPEECH_PROFILES_COMPLETED_STATE_CHANGE*)
                        change;
                return new PartyPopulateAvailableTextToSpeechProfilesCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl));
            }

            case PARTY_STATE_CHANGE_TYPE.ConfigureAudioManipulationVoiceStreamCompleted:
            {
                var typed =
                    (PARTY_CONFIGURE_AUDIO_MANIPULATION_VOICE_STREAM_COMPLETED_STATE_CHANGE*)
                        change;
                return new PartyConfigureAudioManipulationVoiceStreamCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->ChatControl),
                    typed->Configuration is null
                        ? null
                        : PartyAudioManipulationSourceStreamConfiguration.FromNative(
                            typed->Configuration));
            }

            case PARTY_STATE_CHANGE_TYPE.ConfigureAudioManipulationCaptureStreamCompleted:
            {
                var typed =
                    (PARTY_CONFIGURE_AUDIO_MANIPULATION_CAPTURE_STREAM_COMPLETED_STATE_CHANGE*)
                        change;
                return new PartyConfigureAudioManipulationCaptureStreamCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    typed->Configuration is null
                        ? null
                        : PartyAudioManipulationSinkStreamConfiguration.FromNative(
                            typed->Configuration));
            }

            case PARTY_STATE_CHANGE_TYPE.ConfigureAudioManipulationRenderStreamCompleted:
            {
                var typed =
                    (PARTY_CONFIGURE_AUDIO_MANIPULATION_RENDER_STREAM_COMPLETED_STATE_CHANGE*)
                        change;
                return new PartyConfigureAudioManipulationRenderStreamCompleted(
                    PartyManager.FromContext(typed->AsyncIdentifier),
                    (PartyStateChangeResult)typed->Result,
                    typed->ErrorDetail,
                    owner.WrapOrNull<PartyChatControl>(typed->LocalChatControl),
                    typed->Configuration is null
                        ? null
                        : PartyAudioManipulationSinkStreamConfiguration.FromNative(
                            typed->Configuration));
            }

            default:
                throw new NotSupportedException(
                    $"Unknown PartyStateChangeType value {(uint)change->StateChangeType}.");
        }
    }

    internal static IReadOnlyList<PartyRegion> ReadRegions(PARTY_REGION* regions, uint count)
    {
        if (regions is null || count == 0)
        {
            return Array.Empty<PartyRegion>();
        }

        var result = new PartyRegion[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = PartyRegion.FromNative(&regions[i]);
        }

        return result;
    }

    private static void Retire(List<PartyObject> retired, PartyObject? value)
    {
        if (value is not null)
        {
            retired.Add(value);
        }
    }

    private static void RetireOnFailure(
        List<PartyObject> retired, PartyObject? value, PARTY_STATE_CHANGE_RESULT result)
    {
        if (result != PARTY_STATE_CHANGE_RESULT.Succeeded)
        {
            Retire(retired, value);
        }
    }
}
