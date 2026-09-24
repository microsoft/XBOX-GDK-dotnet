# <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioInputChanged"></a> Class PartyLocalChatAudioInputChanged

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The local capture device's state changed.

```csharp
public sealed record PartyLocalChatAudioInputChanged : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyLocalChatAudioInputChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyLocalChatAudioInputChanged](GDK.Net.PlayFab.Party.PartyLocalChatAudioInputChanged.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyLocalChatAudioInputChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioInputChanged__ctor_GDK_Net_PlayFab_Party_PartyChatControl_GDK_Net_PlayFab_Party_PartyAudioInputState_System_UInt32_"></a> PartyLocalChatAudioInputChanged\(PartyChatControl?, PartyAudioInputState, uint\)

The local capture device's state changed.

```csharp
public PartyLocalChatAudioInputChanged(PartyChatControl? ChatControl, PartyAudioInputState State, uint ErrorDetail)
```

#### Parameters

`ChatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

The chat control.

`State` [PartyAudioInputState](GDK.Net.PlayFab.Party.PartyAudioInputState.md)

The new state.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when the device failed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioInputChanged_ChatControl"></a> ChatControl

The chat control.

```csharp
public PartyChatControl? ChatControl { get; init; }
```

#### Property Value

 [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

### <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioInputChanged_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when the device failed.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioInputChanged_State"></a> State

The new state.

```csharp
public PartyAudioInputState State { get; init; }
```

#### Property Value

 [PartyAudioInputState](GDK.Net.PlayFab.Party.PartyAudioInputState.md)

