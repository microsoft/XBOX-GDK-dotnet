using System;
using System.Collections.Generic;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Party;

/// <summary>
/// The audio device a chat control captures from or renders to.
/// </summary>
/// <param name="SelectionType">How the device was chosen.</param>
/// <param name="SelectionContext">The platform-specific selection context, when one applies.</param>
/// <param name="DeviceId">The resolved platform device id, when one is in use.</param>
public readonly record struct PartyAudioDeviceSelection(
    PartyAudioDeviceSelectionType SelectionType,
    string? SelectionContext,
    string? DeviceId);

/// <summary>
/// Projects <c>PARTY_CHAT_CONTROL_HANDLE</c>: the voice and text chat surface for one user on one
/// device.
/// </summary>
/// <remarks>
/// The members that configure the control are only valid on a local chat control; calling them on
/// a remote one fails with a <see cref="PartyException"/> from Party itself.
/// </remarks>
public sealed class PartyChatControl : PartyObject
{
    internal PartyChatControl(PartyManager owner, IntPtr handle)
        : base(owner, handle)
    {
    }

    /// <summary>Whether the chat control belongs to the local device.</summary>
    public unsafe bool IsLocal
    {
        get
        {
            byte value;
            PartyInterop.Check(NativePlayFab.PartyChatControlIsLocal(Checked, &value));
            return PartyInterop.ToBool(value);
        }
    }

    /// <summary>The PlayFab entity id behind the chat control.</summary>
    public unsafe string? EntityId
    {
        get
        {
            byte* value;
            PartyInterop.Check(NativePlayFab.PartyChatControlGetEntityId(Checked, &value));
            return Utf8.ToString(value);
        }
    }

    /// <summary>The PlayFab entity type behind the chat control.</summary>
    public unsafe string? EntityType
    {
        get
        {
            byte* value;
            PartyInterop.Check(NativePlayFab.PartyChatControlGetEntityType(Checked, &value));
            return Utf8.ToString(value);
        }
    }

    /// <summary>The local user the chat control speaks for, when it is local.</summary>
    public unsafe PartyLocalUser? LocalUser
    {
        get
        {
            IntPtr handle;
            PartyInterop.Check(NativePlayFab.PartyChatControlGetLocalUser(Checked, &handle));
            return handle == IntPtr.Zero ? null : Owner.WrapLocalUser(handle);
        }
    }

    /// <summary>The device hosting the chat control.</summary>
    public unsafe PartyDevice Device
    {
        get
        {
            IntPtr handle;
            PartyInterop.Check(NativePlayFab.PartyChatControlGetDevice(Checked, &handle));
            return Owner.WrapDevice(handle);
        }
    }

    /// <summary>The networks the chat control is connected to.</summary>
    public unsafe IReadOnlyList<PartyNetwork> Networks
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(
                NativePlayFab.PartyChatControlGetNetworks(Checked, &count, &handles));
            return Owner.WrapNetworks(handles, count);
        }
    }

    /// <summary>What the local user is currently doing on the voice channel.</summary>
    public unsafe PartyLocalChatControlChatIndicator LocalChatIndicator
    {
        get
        {
            PARTY_LOCAL_CHAT_CONTROL_CHAT_INDICATOR value;
            PartyInterop.Check(
                NativePlayFab.PartyChatControlGetLocalChatIndicator(Checked, &value));
            return (PartyLocalChatControlChatIndicator)value;
        }
    }

    /// <summary>The BCP-47 language code used for transcription and synthesis.</summary>
    public unsafe string? Language
    {
        get
        {
            byte* value;
            PartyInterop.Check(NativePlayFab.PartyChatControlGetLanguage(Checked, &value));
            return Utf8.ToString(value);
        }
    }

    /// <summary>Which voice chat transcriptions Party generates.</summary>
    public unsafe PartyVoiceChatTranscriptionOptions TranscriptionOptions
    {
        get
        {
            PARTY_VOICE_CHAT_TRANSCRIPTION_OPTIONS value;
            PartyInterop.Check(
                NativePlayFab.PartyChatControlGetTranscriptionOptions(Checked, &value));
            return (PartyVoiceChatTranscriptionOptions)value;
        }
    }

    /// <summary>Which text chat translations Party generates.</summary>
    public unsafe PartyTextChatOptions TextChatOptions
    {
        get
        {
            PARTY_TEXT_CHAT_OPTIONS value;
            PartyInterop.Check(NativePlayFab.PartyChatControlGetTextChatOptions(Checked, &value));
            return (PartyTextChatOptions)value;
        }
    }

    /// <summary>How Party mixes this chat control's captured audio.</summary>
    public unsafe PartyVoiceAudioOptions VoiceAudioOptions
    {
        get
        {
            PARTY_VOICE_AUDIO_OPTIONS value;
            PartyInterop.Check(
                NativePlayFab.PartyChatControlGetVoiceAudioOptions(Checked, &value));
            return (PartyVoiceAudioOptions)value;
        }

        set => PartyInterop.Check(NativePlayFab.PartyChatControlSetVoiceAudioOptions(
            Checked, (PARTY_VOICE_AUDIO_OPTIONS)value));
    }

    /// <summary>The encoder bitrate in bits per second.</summary>
    public unsafe uint AudioEncoderBitrate
    {
        get
        {
            uint value;
            PartyInterop.Check(
                NativePlayFab.PartyChatControlGetAudioEncoderBitrate(Checked, &value));
            return value;
        }
    }

    /// <summary>Whether the local microphone is muted.</summary>
    public unsafe bool AudioInputMuted
    {
        get
        {
            byte value;
            PartyInterop.Check(NativePlayFab.PartyChatControlGetAudioInputMuted(Checked, &value));
            return PartyInterop.ToBool(value);
        }

        set => PartyInterop.Check(NativePlayFab.PartyChatControlSetAudioInputMuted(
            Checked, PartyInterop.FromBool(value)));
    }

    /// <summary>The audio capture device in use.</summary>
    public unsafe PartyAudioDeviceSelection AudioInput
    {
        get
        {
            PARTY_AUDIO_DEVICE_SELECTION_TYPE type;
            byte* context;
            byte* deviceId;
            PartyInterop.Check(NativePlayFab.PartyChatControlGetAudioInput(
                Checked, &type, &context, &deviceId));
            return new PartyAudioDeviceSelection(
                (PartyAudioDeviceSelectionType)type,
                Utf8.ToString(context),
                Utf8.ToString(deviceId));
        }
    }

    /// <summary>The audio render device in use.</summary>
    public unsafe PartyAudioDeviceSelection AudioOutput
    {
        get
        {
            PARTY_AUDIO_DEVICE_SELECTION_TYPE type;
            byte* context;
            byte* deviceId;
            PartyInterop.Check(NativePlayFab.PartyChatControlGetAudioOutput(
                Checked, &type, &context, &deviceId));
            return new PartyAudioDeviceSelection(
                (PartyAudioDeviceSelectionType)type,
                Utf8.ToString(context),
                Utf8.ToString(deviceId));
        }
    }

    /// <summary>
    /// The text-to-speech voices Party has discovered, once
    /// <see cref="PopulateAvailableTextToSpeechProfiles"/> has completed.
    /// </summary>
    public unsafe IReadOnlyList<PartyTextToSpeechProfile> AvailableTextToSpeechProfiles
    {
        get
        {
            uint count;
            IntPtr* handles;
            PartyInterop.Check(
                NativePlayFab.PartyChatControlGetAvailableTextToSpeechProfiles(
                    Checked, &count, &handles));
            return Owner.WrapTextToSpeechProfiles(handles, count);
        }
    }

    /// <summary>The pre-encode voice stream, when one has been configured.</summary>
    public unsafe PartyAudioManipulationSourceStream? AudioManipulationVoiceStream
    {
        get
        {
            IntPtr handle;
            PartyInterop.Check(
                NativePlayFab.PartyChatControlGetAudioManipulationVoiceStream(Checked, &handle));
            return handle == IntPtr.Zero ? null : Owner.WrapSourceStream(handle);
        }
    }

    /// <summary>The capture stream, when one has been configured.</summary>
    public unsafe PartyAudioManipulationSinkStream? AudioManipulationCaptureStream
    {
        get
        {
            IntPtr handle;
            PartyInterop.Check(
                NativePlayFab.PartyChatControlGetAudioManipulationCaptureStream(Checked, &handle));
            return handle == IntPtr.Zero ? null : Owner.WrapSinkStream(handle);
        }
    }

    /// <summary>The render stream, when one has been configured.</summary>
    public unsafe PartyAudioManipulationSinkStream? AudioManipulationRenderStream
    {
        get
        {
            IntPtr handle;
            PartyInterop.Check(
                NativePlayFab.PartyChatControlGetAudioManipulationRenderStream(Checked, &handle));
            return handle == IntPtr.Zero ? null : Owner.WrapSinkStream(handle);
        }
    }

    /// <summary>The keys of every property shared on the chat control.</summary>
    public IReadOnlyList<string> GetSharedPropertyKeys() =>
        PartyProperties.GetKeys(PartyPropertyOwner.ChatControl, Checked);

    /// <summary>Reads a shared property, or <see langword="null"/> when it is not set.</summary>
    /// <param name="key">The property key.</param>
    public byte[]? GetSharedProperty(string key) =>
        PartyProperties.Get(PartyPropertyOwner.ChatControl, Checked, key);

    /// <summary>Sets or, for a <see langword="null"/> value, removes shared properties.</summary>
    /// <param name="properties">The properties to write.</param>
    public void SetSharedProperties(IReadOnlyDictionary<string, byte[]?> properties) =>
        PartyProperties.Set(PartyPropertyOwner.ChatControl, Checked, properties);

    /// <summary>Reads what this local control is allowed to send to and receive from a target.</summary>
    /// <param name="target">The target chat control.</param>
    public unsafe PartyChatPermissionOptions GetPermissions(PartyChatControl target)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        PARTY_CHAT_PERMISSION_OPTIONS value;
        PartyInterop.Check(NativePlayFab.PartyChatControlGetPermissions(
            Checked, target.Handle, &value));
        return (PartyChatPermissionOptions)value;
    }

    /// <summary>Sets what this local control may send to and receive from a target.</summary>
    /// <param name="target">The target chat control.</param>
    /// <param name="permissions">The permissions to apply.</param>
    public void SetPermissions(PartyChatControl target, PartyChatPermissionOptions permissions)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        PartyInterop.Check(NativePlayFab.PartyChatControlSetPermissions(
            Checked, target.Handle, (PARTY_CHAT_PERMISSION_OPTIONS)permissions));
    }

    /// <summary>What a target chat control is currently doing on the voice channel.</summary>
    /// <param name="target">The target chat control.</param>
    public unsafe PartyChatControlChatIndicator GetChatIndicator(PartyChatControl target)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        PARTY_CHAT_CONTROL_CHAT_INDICATOR value;
        PartyInterop.Check(NativePlayFab.PartyChatControlGetChatIndicator(
            Checked, target.Handle, &value));
        return (PartyChatControlChatIndicator)value;
    }

    /// <summary>The render volume applied to a target chat control, from 0 to 1.</summary>
    /// <param name="target">The target chat control.</param>
    public unsafe float GetAudioRenderVolume(PartyChatControl target)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        float value;
        PartyInterop.Check(NativePlayFab.PartyChatControlGetAudioRenderVolume(
            Checked, target.Handle, &value));
        return value;
    }

    /// <summary>Sets the render volume applied to a target chat control.</summary>
    /// <param name="target">The target chat control.</param>
    /// <param name="volume">The volume, from 0 to 1.</param>
    public void SetAudioRenderVolume(PartyChatControl target, float volume)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        PartyInterop.Check(NativePlayFab.PartyChatControlSetAudioRenderVolume(
            Checked, target.Handle, volume));
    }

    /// <summary>Whether incoming audio from a target chat control is muted.</summary>
    /// <param name="target">The target chat control.</param>
    public unsafe bool GetIncomingAudioMuted(PartyChatControl target)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        byte value;
        PartyInterop.Check(NativePlayFab.PartyChatControlGetIncomingAudioMuted(
            Checked, target.Handle, &value));
        return PartyInterop.ToBool(value);
    }

    /// <summary>Mutes or unmutes incoming audio from a target chat control.</summary>
    /// <param name="target">The target chat control.</param>
    /// <param name="muted">Whether the target is muted.</param>
    public void SetIncomingAudioMuted(PartyChatControl target, bool muted)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        PartyInterop.Check(NativePlayFab.PartyChatControlSetIncomingAudioMuted(
            Checked, target.Handle, PartyInterop.FromBool(muted)));
    }

    /// <summary>Whether incoming text from a target chat control is muted.</summary>
    /// <param name="target">The target chat control.</param>
    public unsafe bool GetIncomingTextMuted(PartyChatControl target)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        byte value;
        PartyInterop.Check(NativePlayFab.PartyChatControlGetIncomingTextMuted(
            Checked, target.Handle, &value));
        return PartyInterop.ToBool(value);
    }

    /// <summary>Mutes or unmutes incoming text from a target chat control.</summary>
    /// <param name="target">The target chat control.</param>
    /// <param name="muted">Whether the target is muted.</param>
    public void SetIncomingTextMuted(PartyChatControl target, bool muted)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        PartyInterop.Check(NativePlayFab.PartyChatControlSetIncomingTextMuted(
            Checked, target.Handle, PartyInterop.FromBool(muted)));
    }

    /// <summary>The text-to-speech profile selected for a synthesis type.</summary>
    /// <param name="type">Which synthesis pipeline the profile applies to.</param>
    public unsafe PartyTextToSpeechProfile? GetTextToSpeechProfile(
        PartySynthesizeTextToSpeechType type)
    {
        IntPtr handle;
        PartyInterop.Check(NativePlayFab.PartyChatControlGetTextToSpeechProfile(
            Checked, (PARTY_SYNTHESIZE_TEXT_TO_SPEECH_TYPE)type, &handle));
        return handle == IntPtr.Zero ? null : Owner.WrapTextToSpeechProfile(handle);
    }

    /// <summary>Sends a chat message to the given chat controls.</summary>
    /// <param name="targets">
    /// The receiving chat controls, or an empty list to send to every connected control.
    /// </param>
    /// <param name="text">The message text.</param>
    /// <param name="data">Optional opaque data delivered alongside the text.</param>
    public unsafe void SendText(
        IReadOnlyList<PartyChatControl> targets, string text, byte[]? data = null)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        var arena = new PlayFabArena();
        try
        {
            uint targetCount = targets is null ? 0u : (uint)targets.Count;
            IntPtr* targetHandles = null;
            if (targetCount > 0)
            {
                targetHandles = arena.Alloc<IntPtr>((int)targetCount);
                for (int i = 0; i < targets!.Count; i++)
                {
                    targetHandles[i] = targets[i].Handle;
                }
            }

            var buffer = default(PARTY_DATA_BUFFER);
            uint bufferCount = 0;
            if (data is not null && data.Length > 0)
            {
                byte* payload = arena.Alloc<byte>(data.Length);
                fixed (byte* source = data)
                {
                    Buffer.MemoryCopy(source, payload, data.Length, data.Length);
                }

                buffer.Buffer = payload;
                buffer.BufferByteCount = (uint)data.Length;
                bufferCount = 1;
            }

            PartyInterop.Check(NativePlayFab.PartyChatControlSendText(
                Checked,
                targetCount,
                targetHandles,
                arena.String(text),
                bufferCount,
                bufferCount == 0 ? null : &buffer));
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>Starts changing the encoder bitrate.</summary>
    /// <param name="bitrate">The bitrate in bits per second.</param>
    public unsafe PartyOperationId SetAudioEncoderBitrate(uint bitrate)
    {
        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyChatControlSetAudioEncoderBitrate(
            Checked, bitrate, PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts selecting the audio capture device.</summary>
    /// <param name="selectionType">How the device is chosen.</param>
    /// <param name="selectionContext">The platform-specific selection context.</param>
    public unsafe PartyOperationId SetAudioInput(
        PartyAudioDeviceSelectionType selectionType, string? selectionContext = null)
    {
        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PartyInterop.Check(NativePlayFab.PartyChatControlSetAudioInput(
                Checked,
                (PARTY_AUDIO_DEVICE_SELECTION_TYPE)selectionType,
                arena.String(selectionContext),
                PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts selecting the audio render device.</summary>
    /// <param name="selectionType">How the device is chosen.</param>
    /// <param name="selectionContext">The platform-specific selection context.</param>
    public unsafe PartyOperationId SetAudioOutput(
        PartyAudioDeviceSelectionType selectionType, string? selectionContext = null)
    {
        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PartyInterop.Check(NativePlayFab.PartyChatControlSetAudioOutput(
                Checked,
                (PARTY_AUDIO_DEVICE_SELECTION_TYPE)selectionType,
                arena.String(selectionContext),
                PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts discovering the text-to-speech voices available on this device.</summary>
    public unsafe PartyOperationId PopulateAvailableTextToSpeechProfiles()
    {
        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(
            NativePlayFab.PartyChatControlPopulateAvailableTextToSpeechProfiles(
                Checked, PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts selecting the voice used for a synthesis type.</summary>
    /// <param name="type">Which synthesis pipeline the profile applies to.</param>
    /// <param name="profileIdentifier">
    /// The profile identifier, or <see langword="null"/> to clear the selection.
    /// </param>
    public unsafe PartyOperationId SetTextToSpeechProfile(
        PartySynthesizeTextToSpeechType type, string? profileIdentifier)
    {
        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PartyInterop.Check(NativePlayFab.PartyChatControlSetTextToSpeechProfile(
                Checked,
                (PARTY_SYNTHESIZE_TEXT_TO_SPEECH_TYPE)type,
                arena.String(profileIdentifier),
                PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts synthesizing speech from text.</summary>
    /// <param name="type">Which synthesis pipeline to use.</param>
    /// <param name="text">The text to speak.</param>
    public unsafe PartyOperationId SynthesizeTextToSpeech(
        PartySynthesizeTextToSpeechType type, string text)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PartyInterop.Check(NativePlayFab.PartyChatControlSynthesizeTextToSpeech(
                Checked,
                (PARTY_SYNTHESIZE_TEXT_TO_SPEECH_TYPE)type,
                arena.String(text),
                PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts changing the language used for transcription and synthesis.</summary>
    /// <param name="languageCode">The BCP-47 language code, or <see langword="null"/> for the default.</param>
    public unsafe PartyOperationId SetLanguage(string? languageCode)
    {
        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PartyInterop.Check(NativePlayFab.PartyChatControlSetLanguage(
                Checked, arena.String(languageCode), PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts changing which voice chat transcriptions Party generates.</summary>
    /// <param name="options">The transcription options.</param>
    public unsafe PartyOperationId SetTranscriptionOptions(
        PartyVoiceChatTranscriptionOptions options)
    {
        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyChatControlSetTranscriptionOptions(
            Checked,
            (PARTY_VOICE_CHAT_TRANSCRIPTION_OPTIONS)options,
            PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts changing which text chat translations Party generates.</summary>
    /// <param name="options">The text chat options.</param>
    public unsafe PartyOperationId SetTextChatOptions(PartyTextChatOptions options)
    {
        PartyOperationId operation = Owner.NextOperation();
        PartyInterop.Check(NativePlayFab.PartyChatControlSetTextChatOptions(
            Checked, (PARTY_TEXT_CHAT_OPTIONS)options, PartyManager.Context(operation)));
        return operation;
    }

    /// <summary>Starts configuring the pre-encode voice manipulation stream.</summary>
    /// <param name="configuration">
    /// The stream configuration, or <see langword="null"/> to tear the stream down.
    /// </param>
    public unsafe PartyOperationId ConfigureAudioManipulationVoiceStream(
        PartyAudioManipulationSourceStreamConfiguration? configuration)
    {
        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PARTY_AUDIO_MANIPULATION_SOURCE_STREAM_CONFIGURATION* native = null;
            if (configuration is not null)
            {
                native = arena.Alloc<PARTY_AUDIO_MANIPULATION_SOURCE_STREAM_CONFIGURATION>(1);
                configuration.WriteTo(native, arena);
            }

            PartyInterop.Check(
                NativePlayFab.PartyChatControlConfigureAudioManipulationVoiceStream(
                    Checked, native, PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts configuring the capture manipulation stream.</summary>
    /// <param name="configuration">
    /// The stream configuration, or <see langword="null"/> to tear the stream down.
    /// </param>
    public unsafe PartyOperationId ConfigureAudioManipulationCaptureStream(
        PartyAudioManipulationSinkStreamConfiguration? configuration)
    {
        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PARTY_AUDIO_MANIPULATION_SINK_STREAM_CONFIGURATION* native = null;
            if (configuration is not null)
            {
                native = arena.Alloc<PARTY_AUDIO_MANIPULATION_SINK_STREAM_CONFIGURATION>(1);
                configuration.WriteTo(native, arena);
            }

            PartyInterop.Check(
                NativePlayFab.PartyChatControlConfigureAudioManipulationCaptureStream(
                    Checked, native, PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }

    /// <summary>Starts configuring the render manipulation stream.</summary>
    /// <param name="configuration">
    /// The stream configuration, or <see langword="null"/> to tear the stream down.
    /// </param>
    public unsafe PartyOperationId ConfigureAudioManipulationRenderStream(
        PartyAudioManipulationSinkStreamConfiguration? configuration)
    {
        PartyOperationId operation = Owner.NextOperation();
        var arena = new PlayFabArena();
        try
        {
            PARTY_AUDIO_MANIPULATION_SINK_STREAM_CONFIGURATION* native = null;
            if (configuration is not null)
            {
                native = arena.Alloc<PARTY_AUDIO_MANIPULATION_SINK_STREAM_CONFIGURATION>(1);
                configuration.WriteTo(native, arena);
            }

            PartyInterop.Check(
                NativePlayFab.PartyChatControlConfigureAudioManipulationRenderStream(
                    Checked, native, PartyManager.Context(operation)));
        }
        finally
        {
            arena.Dispose();
        }

        return operation;
    }
}
