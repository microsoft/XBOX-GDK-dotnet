# <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived"></a> Class PartyVoiceChatTranscriptionReceived

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A voice transcription arrived.

```csharp
public sealed record PartyVoiceChatTranscriptionReceived : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyVoiceChatTranscriptionReceived>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyVoiceChatTranscriptionReceived](GDK.Net.PlayFab.Party.PartyVoiceChatTranscriptionReceived.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyVoiceChatTranscriptionReceived\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived__ctor_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyChatControl_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyChatControl__GDK_Net_PlayFab_Party_PartyAudioSourceType_System_String_System_String_GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionPhraseType_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyTranslation__"></a> PartyVoiceChatTranscriptionReceived\(PartyStateChangeResult, uint, PartyChatControl?, IReadOnlyList<PartyChatControl\>, PartyAudioSourceType, string?, string?, PartyVoiceChatTranscriptionPhraseType, IReadOnlyList<PartyTranslation\>\)

A voice transcription arrived.

```csharp
public PartyVoiceChatTranscriptionReceived(PartyStateChangeResult Result, uint ErrorDetail, PartyChatControl? SenderChatControl, IReadOnlyList<PartyChatControl> ReceiverChatControls, PartyAudioSourceType SourceType, string? LanguageCode, string? Transcription, PartyVoiceChatTranscriptionPhraseType PhraseType, IReadOnlyList<PartyTranslation> Translations)
```

#### Parameters

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether transcription succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`SenderChatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

The chat control that was speaking.

`ReceiverChatControls` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)\>

The local chat controls the transcription was delivered to.

`SourceType` [PartyAudioSourceType](GDK.Net.PlayFab.Party.PartyAudioSourceType.md)

Whether the audio came from a microphone or from synthesis.

`LanguageCode` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The language that was transcribed.

`Transcription` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The transcribed text.

`PhraseType` [PartyVoiceChatTranscriptionPhraseType](GDK.Net.PlayFab.Party.PartyVoiceChatTranscriptionPhraseType.md)

Whether the phrase is a hypothesis or final.

`Translations` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyTranslation](GDK.Net.PlayFab.Party.PartyTranslation.md)\>

Machine translations Party generated.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when it failed.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived_LanguageCode"></a> LanguageCode

The language that was transcribed.

```csharp
public string? LanguageCode { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived_PhraseType"></a> PhraseType

Whether the phrase is a hypothesis or final.

```csharp
public PartyVoiceChatTranscriptionPhraseType PhraseType { get; init; }
```

#### Property Value

 [PartyVoiceChatTranscriptionPhraseType](GDK.Net.PlayFab.Party.PartyVoiceChatTranscriptionPhraseType.md)

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived_ReceiverChatControls"></a> ReceiverChatControls

The local chat controls the transcription was delivered to.

```csharp
public IReadOnlyList<PartyChatControl> ReceiverChatControls { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived_Result"></a> Result

Whether transcription succeeded.

```csharp
public PartyStateChangeResult Result { get; init; }
```

#### Property Value

 [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived_SenderChatControl"></a> SenderChatControl

The chat control that was speaking.

```csharp
public PartyChatControl? SenderChatControl { get; init; }
```

#### Property Value

 [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived_SourceType"></a> SourceType

Whether the audio came from a microphone or from synthesis.

```csharp
public PartyAudioSourceType SourceType { get; init; }
```

#### Property Value

 [PartyAudioSourceType](GDK.Net.PlayFab.Party.PartyAudioSourceType.md)

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived_Transcription"></a> Transcription

The transcribed text.

```csharp
public string? Transcription { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyVoiceChatTranscriptionReceived_Translations"></a> Translations

Machine translations Party generated.

```csharp
public IReadOnlyList<PartyTranslation> Translations { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyTranslation](GDK.Net.PlayFab.Party.PartyTranslation.md)\>

