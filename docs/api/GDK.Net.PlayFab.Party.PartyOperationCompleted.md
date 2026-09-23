# <a id="GDK_Net_PlayFab_Party_PartyOperationCompleted"></a> Class PartyOperationCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A state change that completes an operation the title started.

```csharp
public abstract record PartyOperationCompleted : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyOperationCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md)

#### Derived

[PartyAuthenticateLocalUserCompleted](GDK.Net.PlayFab.Party.PartyAuthenticateLocalUserCompleted.md), 
[PartyConfigureAudioManipulationCaptureStreamCompleted](GDK.Net.PlayFab.Party.PartyConfigureAudioManipulationCaptureStreamCompleted.md), 
[PartyConfigureAudioManipulationRenderStreamCompleted](GDK.Net.PlayFab.Party.PartyConfigureAudioManipulationRenderStreamCompleted.md), 
[PartyConfigureAudioManipulationVoiceStreamCompleted](GDK.Net.PlayFab.Party.PartyConfigureAudioManipulationVoiceStreamCompleted.md), 
[PartyConnectChatControlCompleted](GDK.Net.PlayFab.Party.PartyConnectChatControlCompleted.md), 
[PartyConnectToNetworkCompleted](GDK.Net.PlayFab.Party.PartyConnectToNetworkCompleted.md), 
[PartyCreateChatControlCompleted](GDK.Net.PlayFab.Party.PartyCreateChatControlCompleted.md), 
[PartyCreateEndpointCompleted](GDK.Net.PlayFab.Party.PartyCreateEndpointCompleted.md), 
[PartyCreateInvitationCompleted](GDK.Net.PlayFab.Party.PartyCreateInvitationCompleted.md), 
[PartyCreateNewNetworkCompleted](GDK.Net.PlayFab.Party.PartyCreateNewNetworkCompleted.md), 
[PartyDestroyChatControlCompleted](GDK.Net.PlayFab.Party.PartyDestroyChatControlCompleted.md), 
[PartyDestroyEndpointCompleted](GDK.Net.PlayFab.Party.PartyDestroyEndpointCompleted.md), 
[PartyDestroyLocalUserCompleted](GDK.Net.PlayFab.Party.PartyDestroyLocalUserCompleted.md), 
[PartyDisconnectChatControlCompleted](GDK.Net.PlayFab.Party.PartyDisconnectChatControlCompleted.md), 
[PartyKickDeviceCompleted](GDK.Net.PlayFab.Party.PartyKickDeviceCompleted.md), 
[PartyKickUserCompleted](GDK.Net.PlayFab.Party.PartyKickUserCompleted.md), 
[PartyLeaveNetworkCompleted](GDK.Net.PlayFab.Party.PartyLeaveNetworkCompleted.md), 
[PartyPopulateAvailableTextToSpeechProfilesCompleted](GDK.Net.PlayFab.Party.PartyPopulateAvailableTextToSpeechProfilesCompleted.md), 
[PartyRegionsChanged](GDK.Net.PlayFab.Party.PartyRegionsChanged.md), 
[PartyRemoveLocalUserCompleted](GDK.Net.PlayFab.Party.PartyRemoveLocalUserCompleted.md), 
[PartyRevokeInvitationCompleted](GDK.Net.PlayFab.Party.PartyRevokeInvitationCompleted.md), 
[PartySetChatAudioEncoderBitrateCompleted](GDK.Net.PlayFab.Party.PartySetChatAudioEncoderBitrateCompleted.md), 
[PartySetChatAudioInputCompleted](GDK.Net.PlayFab.Party.PartySetChatAudioInputCompleted.md), 
[PartySetChatAudioOutputCompleted](GDK.Net.PlayFab.Party.PartySetChatAudioOutputCompleted.md), 
[PartySetLanguageCompleted](GDK.Net.PlayFab.Party.PartySetLanguageCompleted.md), 
[PartySetTextChatOptionsCompleted](GDK.Net.PlayFab.Party.PartySetTextChatOptionsCompleted.md), 
[PartySetTextToSpeechProfileCompleted](GDK.Net.PlayFab.Party.PartySetTextToSpeechProfileCompleted.md), 
[PartySetTranscriptionOptionsCompleted](GDK.Net.PlayFab.Party.PartySetTranscriptionOptionsCompleted.md), 
[PartySynthesizeTextToSpeechCompleted](GDK.Net.PlayFab.Party.PartySynthesizeTextToSpeechCompleted.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyOperationCompleted__ctor_GDK_Net_PlayFab_Party_PartyStateChangeType_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_"></a> PartyOperationCompleted\(PartyStateChangeType, PartyOperationId, PartyStateChangeResult, uint\)

A state change that completes an operation the title started.

```csharp
protected PartyOperationCompleted(PartyStateChangeType Kind, PartyOperationId Operation, PartyStateChangeResult Result, uint ErrorDetail)
```

#### Parameters

`Kind` [PartyStateChangeType](GDK.Net.PlayFab.Party.PartyStateChangeType.md)

The discriminator matching the native <code>PartyStateChangeType</code>.

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the matching start call.

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when the operation failed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyOperationCompleted_Error"></a> Error

The failure, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the operation succeeded.

```csharp
public Exception? Error { get; }
```

#### Property Value

 [Exception](https://learn.microsoft.com/dotnet/api/system.exception)?

### <a id="GDK_Net_PlayFab_Party_PartyOperationCompleted_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when the operation failed.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyOperationCompleted_Operation"></a> Operation

The id returned by the matching start call.

```csharp
public PartyOperationId Operation { get; init; }
```

#### Property Value

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyOperationCompleted_Result"></a> Result

Whether the operation succeeded.

```csharp
public PartyStateChangeResult Result { get; init; }
```

#### Property Value

 [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

### <a id="GDK_Net_PlayFab_Party_PartyOperationCompleted_Succeeded"></a> Succeeded

Whether the operation succeeded.

```csharp
public bool Succeeded { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

