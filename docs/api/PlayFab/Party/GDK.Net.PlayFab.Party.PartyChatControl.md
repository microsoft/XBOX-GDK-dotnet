# <a id="GDK_Net_PlayFab_Party_PartyChatControl"></a> Class PartyChatControl

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_CHAT_CONTROL_HANDLE</code>: the voice and text chat surface for one user on one
device.

```csharp
public sealed class PartyChatControl : PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md) ← 
[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

#### Inherited Members

[PartyObject.IsValid](GDK.Net.PlayFab.Party.PartyObject.md\#GDK\_Net\_PlayFab\_Party\_PartyObject\_IsValid), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The members that configure the control are only valid on a local chat control; calling them on
a remote one fails with a <xref href="GDK.Net.PlayFab.Party.PartyException" data-throw-if-not-resolved="false"></xref> from Party itself.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_AudioEncoderBitrate"></a> AudioEncoderBitrate

The encoder bitrate in bits per second.

```csharp
public uint AudioEncoderBitrate { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_AudioInput"></a> AudioInput

The audio capture device in use.

```csharp
public PartyAudioDeviceSelection AudioInput { get; }
```

#### Property Value

 [PartyAudioDeviceSelection](GDK.Net.PlayFab.Party.PartyAudioDeviceSelection.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_AudioInputMuted"></a> AudioInputMuted

Whether the local microphone is muted.

```csharp
public bool AudioInputMuted { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_AudioManipulationCaptureStream"></a> AudioManipulationCaptureStream

The capture stream, when one has been configured.

```csharp
public PartyAudioManipulationSinkStream? AudioManipulationCaptureStream { get; }
```

#### Property Value

 [PartyAudioManipulationSinkStream](GDK.Net.PlayFab.Party.PartyAudioManipulationSinkStream.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_AudioManipulationRenderStream"></a> AudioManipulationRenderStream

The render stream, when one has been configured.

```csharp
public PartyAudioManipulationSinkStream? AudioManipulationRenderStream { get; }
```

#### Property Value

 [PartyAudioManipulationSinkStream](GDK.Net.PlayFab.Party.PartyAudioManipulationSinkStream.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_AudioManipulationVoiceStream"></a> AudioManipulationVoiceStream

The pre-encode voice stream, when one has been configured.

```csharp
public PartyAudioManipulationSourceStream? AudioManipulationVoiceStream { get; }
```

#### Property Value

 [PartyAudioManipulationSourceStream](GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStream.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_AudioOutput"></a> AudioOutput

The audio render device in use.

```csharp
public PartyAudioDeviceSelection AudioOutput { get; }
```

#### Property Value

 [PartyAudioDeviceSelection](GDK.Net.PlayFab.Party.PartyAudioDeviceSelection.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_AvailableTextToSpeechProfiles"></a> AvailableTextToSpeechProfiles

The text-to-speech voices Party has discovered, once
<xref href="GDK.Net.PlayFab.Party.PartyChatControl.PopulateAvailableTextToSpeechProfiles" data-throw-if-not-resolved="false"></xref> has completed.

```csharp
public IReadOnlyList<PartyTextToSpeechProfile> AvailableTextToSpeechProfiles { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyTextToSpeechProfile](GDK.Net.PlayFab.Party.PartyTextToSpeechProfile.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_Device"></a> Device

The device hosting the chat control.

```csharp
public PartyDevice Device { get; }
```

#### Property Value

 [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_EntityId"></a> EntityId

The PlayFab entity id behind the chat control.

```csharp
public string? EntityId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_EntityType"></a> EntityType

The PlayFab entity type behind the chat control.

```csharp
public string? EntityType { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_IsLocal"></a> IsLocal

Whether the chat control belongs to the local device.

```csharp
public bool IsLocal { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_Language"></a> Language

The BCP-47 language code used for transcription and synthesis.

```csharp
public string? Language { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_LocalChatIndicator"></a> LocalChatIndicator

What the local user is currently doing on the voice channel.

```csharp
public PartyLocalChatControlChatIndicator LocalChatIndicator { get; }
```

#### Property Value

 [PartyLocalChatControlChatIndicator](GDK.Net.PlayFab.Party.PartyLocalChatControlChatIndicator.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_LocalUser"></a> LocalUser

The local user the chat control speaks for, when it is local.

```csharp
public PartyLocalUser? LocalUser { get; }
```

#### Property Value

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_Networks"></a> Networks

The networks the chat control is connected to.

```csharp
public IReadOnlyList<PartyNetwork> Networks { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_TextChatOptions"></a> TextChatOptions

Which text chat translations Party generates.

```csharp
public PartyTextChatOptions TextChatOptions { get; }
```

#### Property Value

 [PartyTextChatOptions](GDK.Net.PlayFab.Party.PartyTextChatOptions.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_TranscriptionOptions"></a> TranscriptionOptions

Which voice chat transcriptions Party generates.

```csharp
public PartyVoiceChatTranscriptionOptions TranscriptionOptions { get; }
```

#### Property Value

 [PartyVoiceChatTranscriptionOptions](GDK.Net.PlayFab.Party.PartyVoiceChatTranscriptionOptions.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_VoiceAudioOptions"></a> VoiceAudioOptions

How Party mixes this chat control's captured audio.

```csharp
public PartyVoiceAudioOptions VoiceAudioOptions { get; set; }
```

#### Property Value

 [PartyVoiceAudioOptions](GDK.Net.PlayFab.Party.PartyVoiceAudioOptions.md)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_ConfigureAudioManipulationCaptureStream_GDK_Net_PlayFab_Party_PartyAudioManipulationSinkStreamConfiguration_"></a> ConfigureAudioManipulationCaptureStream\(PartyAudioManipulationSinkStreamConfiguration?\)

Starts configuring the capture manipulation stream.

```csharp
public PartyOperationId ConfigureAudioManipulationCaptureStream(PartyAudioManipulationSinkStreamConfiguration? configuration)
```

#### Parameters

`configuration` [PartyAudioManipulationSinkStreamConfiguration](GDK.Net.PlayFab.Party.PartyAudioManipulationSinkStreamConfiguration.md)?

The stream configuration, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to tear the stream down.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_ConfigureAudioManipulationRenderStream_GDK_Net_PlayFab_Party_PartyAudioManipulationSinkStreamConfiguration_"></a> ConfigureAudioManipulationRenderStream\(PartyAudioManipulationSinkStreamConfiguration?\)

Starts configuring the render manipulation stream.

```csharp
public PartyOperationId ConfigureAudioManipulationRenderStream(PartyAudioManipulationSinkStreamConfiguration? configuration)
```

#### Parameters

`configuration` [PartyAudioManipulationSinkStreamConfiguration](GDK.Net.PlayFab.Party.PartyAudioManipulationSinkStreamConfiguration.md)?

The stream configuration, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to tear the stream down.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_ConfigureAudioManipulationVoiceStream_GDK_Net_PlayFab_Party_PartyAudioManipulationSourceStreamConfiguration_"></a> ConfigureAudioManipulationVoiceStream\(PartyAudioManipulationSourceStreamConfiguration?\)

Starts configuring the pre-encode voice manipulation stream.

```csharp
public PartyOperationId ConfigureAudioManipulationVoiceStream(PartyAudioManipulationSourceStreamConfiguration? configuration)
```

#### Parameters

`configuration` [PartyAudioManipulationSourceStreamConfiguration](GDK.Net.PlayFab.Party.PartyAudioManipulationSourceStreamConfiguration.md)?

The stream configuration, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to tear the stream down.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_GetAudioRenderVolume_GDK_Net_PlayFab_Party_PartyChatControl_"></a> GetAudioRenderVolume\(PartyChatControl\)

The render volume applied to a target chat control, from 0 to 1.

```csharp
public float GetAudioRenderVolume(PartyChatControl target)
```

#### Parameters

`target` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The target chat control.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_GetChatIndicator_GDK_Net_PlayFab_Party_PartyChatControl_"></a> GetChatIndicator\(PartyChatControl\)

What a target chat control is currently doing on the voice channel.

```csharp
public PartyChatControlChatIndicator GetChatIndicator(PartyChatControl target)
```

#### Parameters

`target` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The target chat control.

#### Returns

 [PartyChatControlChatIndicator](GDK.Net.PlayFab.Party.PartyChatControlChatIndicator.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_GetIncomingAudioMuted_GDK_Net_PlayFab_Party_PartyChatControl_"></a> GetIncomingAudioMuted\(PartyChatControl\)

Whether incoming audio from a target chat control is muted.

```csharp
public bool GetIncomingAudioMuted(PartyChatControl target)
```

#### Parameters

`target` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The target chat control.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_GetIncomingTextMuted_GDK_Net_PlayFab_Party_PartyChatControl_"></a> GetIncomingTextMuted\(PartyChatControl\)

Whether incoming text from a target chat control is muted.

```csharp
public bool GetIncomingTextMuted(PartyChatControl target)
```

#### Parameters

`target` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The target chat control.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_GetPermissions_GDK_Net_PlayFab_Party_PartyChatControl_"></a> GetPermissions\(PartyChatControl\)

Reads what this local control is allowed to send to and receive from a target.

```csharp
public PartyChatPermissionOptions GetPermissions(PartyChatControl target)
```

#### Parameters

`target` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The target chat control.

#### Returns

 [PartyChatPermissionOptions](GDK.Net.PlayFab.Party.PartyChatPermissionOptions.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_GetSharedProperty_System_String_"></a> GetSharedProperty\(string\)

Reads a shared property, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it is not set.

```csharp
public byte[]? GetSharedProperty(string key)
```

#### Parameters

`key` [string](https://learn.microsoft.com/dotnet/api/system.string)

The property key.

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_GetSharedPropertyKeys"></a> GetSharedPropertyKeys\(\)

The keys of every property shared on the chat control.

```csharp
public IReadOnlyList<string> GetSharedPropertyKeys()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_GetTextToSpeechProfile_GDK_Net_PlayFab_Party_PartySynthesizeTextToSpeechType_"></a> GetTextToSpeechProfile\(PartySynthesizeTextToSpeechType\)

The text-to-speech profile selected for a synthesis type.

```csharp
public PartyTextToSpeechProfile? GetTextToSpeechProfile(PartySynthesizeTextToSpeechType type)
```

#### Parameters

`type` [PartySynthesizeTextToSpeechType](GDK.Net.PlayFab.Party.PartySynthesizeTextToSpeechType.md)

Which synthesis pipeline the profile applies to.

#### Returns

 [PartyTextToSpeechProfile](GDK.Net.PlayFab.Party.PartyTextToSpeechProfile.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_PopulateAvailableTextToSpeechProfiles"></a> PopulateAvailableTextToSpeechProfiles\(\)

Starts discovering the text-to-speech voices available on this device.

```csharp
public PartyOperationId PopulateAvailableTextToSpeechProfiles()
```

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SendText_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyChatControl__System_String_System_Byte___"></a> SendText\(IReadOnlyList<PartyChatControl\>, string, byte\[\]?\)

Sends a chat message to the given chat controls.

```csharp
public void SendText(IReadOnlyList<PartyChatControl> targets, string text, byte[]? data = null)
```

#### Parameters

`targets` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)\>

The receiving chat controls, or an empty list to send to every connected control.

`text` [string](https://learn.microsoft.com/dotnet/api/system.string)

The message text.

`data` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional opaque data delivered alongside the text.

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetAudioEncoderBitrate_System_UInt32_"></a> SetAudioEncoderBitrate\(uint\)

Starts changing the encoder bitrate.

```csharp
public PartyOperationId SetAudioEncoderBitrate(uint bitrate)
```

#### Parameters

`bitrate` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The bitrate in bits per second.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetAudioInput_GDK_Net_PlayFab_Party_PartyAudioDeviceSelectionType_System_String_"></a> SetAudioInput\(PartyAudioDeviceSelectionType, string?\)

Starts selecting the audio capture device.

```csharp
public PartyOperationId SetAudioInput(PartyAudioDeviceSelectionType selectionType, string? selectionContext = null)
```

#### Parameters

`selectionType` [PartyAudioDeviceSelectionType](GDK.Net.PlayFab.Party.PartyAudioDeviceSelectionType.md)

How the device is chosen.

`selectionContext` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The platform-specific selection context.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetAudioOutput_GDK_Net_PlayFab_Party_PartyAudioDeviceSelectionType_System_String_"></a> SetAudioOutput\(PartyAudioDeviceSelectionType, string?\)

Starts selecting the audio render device.

```csharp
public PartyOperationId SetAudioOutput(PartyAudioDeviceSelectionType selectionType, string? selectionContext = null)
```

#### Parameters

`selectionType` [PartyAudioDeviceSelectionType](GDK.Net.PlayFab.Party.PartyAudioDeviceSelectionType.md)

How the device is chosen.

`selectionContext` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The platform-specific selection context.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetAudioRenderVolume_GDK_Net_PlayFab_Party_PartyChatControl_System_Single_"></a> SetAudioRenderVolume\(PartyChatControl, float\)

Sets the render volume applied to a target chat control.

```csharp
public void SetAudioRenderVolume(PartyChatControl target, float volume)
```

#### Parameters

`target` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The target chat control.

`volume` [float](https://learn.microsoft.com/dotnet/api/system.single)

The volume, from 0 to 1.

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetIncomingAudioMuted_GDK_Net_PlayFab_Party_PartyChatControl_System_Boolean_"></a> SetIncomingAudioMuted\(PartyChatControl, bool\)

Mutes or unmutes incoming audio from a target chat control.

```csharp
public void SetIncomingAudioMuted(PartyChatControl target, bool muted)
```

#### Parameters

`target` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The target chat control.

`muted` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the target is muted.

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetIncomingTextMuted_GDK_Net_PlayFab_Party_PartyChatControl_System_Boolean_"></a> SetIncomingTextMuted\(PartyChatControl, bool\)

Mutes or unmutes incoming text from a target chat control.

```csharp
public void SetIncomingTextMuted(PartyChatControl target, bool muted)
```

#### Parameters

`target` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The target chat control.

`muted` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the target is muted.

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetLanguage_System_String_"></a> SetLanguage\(string?\)

Starts changing the language used for transcription and synthesis.

```csharp
public PartyOperationId SetLanguage(string? languageCode)
```

#### Parameters

`languageCode` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The BCP-47 language code, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the default.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetPermissions_GDK_Net_PlayFab_Party_PartyChatControl_GDK_Net_PlayFab_Party_PartyChatPermissionOptions_"></a> SetPermissions\(PartyChatControl, PartyChatPermissionOptions\)

Sets what this local control may send to and receive from a target.

```csharp
public void SetPermissions(PartyChatControl target, PartyChatPermissionOptions permissions)
```

#### Parameters

`target` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The target chat control.

`permissions` [PartyChatPermissionOptions](GDK.Net.PlayFab.Party.PartyChatPermissionOptions.md)

The permissions to apply.

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetSharedProperties_System_Collections_Generic_IReadOnlyDictionary_System_String_System_Byte____"></a> SetSharedProperties\(IReadOnlyDictionary<string, byte\[\]?\>\)

Sets or, for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> value, removes shared properties.

```csharp
public void SetSharedProperties(IReadOnlyDictionary<string, byte[]?> properties)
```

#### Parameters

`properties` [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?\>

The properties to write.

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetTextChatOptions_GDK_Net_PlayFab_Party_PartyTextChatOptions_"></a> SetTextChatOptions\(PartyTextChatOptions\)

Starts changing which text chat translations Party generates.

```csharp
public PartyOperationId SetTextChatOptions(PartyTextChatOptions options)
```

#### Parameters

`options` [PartyTextChatOptions](GDK.Net.PlayFab.Party.PartyTextChatOptions.md)

The text chat options.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetTextToSpeechProfile_GDK_Net_PlayFab_Party_PartySynthesizeTextToSpeechType_System_String_"></a> SetTextToSpeechProfile\(PartySynthesizeTextToSpeechType, string?\)

Starts selecting the voice used for a synthesis type.

```csharp
public PartyOperationId SetTextToSpeechProfile(PartySynthesizeTextToSpeechType type, string? profileIdentifier)
```

#### Parameters

`type` [PartySynthesizeTextToSpeechType](GDK.Net.PlayFab.Party.PartySynthesizeTextToSpeechType.md)

Which synthesis pipeline the profile applies to.

`profileIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The profile identifier, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear the selection.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SetTranscriptionOptions_GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionOptions_"></a> SetTranscriptionOptions\(PartyVoiceChatTranscriptionOptions\)

Starts changing which voice chat transcriptions Party generates.

```csharp
public PartyOperationId SetTranscriptionOptions(PartyVoiceChatTranscriptionOptions options)
```

#### Parameters

`options` [PartyVoiceChatTranscriptionOptions](GDK.Net.PlayFab.Party.PartyVoiceChatTranscriptionOptions.md)

The transcription options.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyChatControl_SynthesizeTextToSpeech_GDK_Net_PlayFab_Party_PartySynthesizeTextToSpeechType_System_String_"></a> SynthesizeTextToSpeech\(PartySynthesizeTextToSpeechType, string\)

Starts synthesizing speech from text.

```csharp
public PartyOperationId SynthesizeTextToSpeech(PartySynthesizeTextToSpeechType type, string text)
```

#### Parameters

`type` [PartySynthesizeTextToSpeechType](GDK.Net.PlayFab.Party.PartySynthesizeTextToSpeechType.md)

Which synthesis pipeline to use.

`text` [string](https://learn.microsoft.com/dotnet/api/system.string)

The text to speak.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

