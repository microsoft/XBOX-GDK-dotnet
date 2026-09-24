# <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioOutputChanged"></a> Class PartyLocalChatAudioOutputChanged

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The local render device's state changed.

```csharp
public sealed record PartyLocalChatAudioOutputChanged : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyLocalChatAudioOutputChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyLocalChatAudioOutputChanged](GDK.Net.PlayFab.Party.PartyLocalChatAudioOutputChanged.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyLocalChatAudioOutputChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioOutputChanged__ctor_GDK_Net_PlayFab_Party_PartyChatControl_GDK_Net_PlayFab_Party_PartyAudioOutputState_System_UInt32_"></a> PartyLocalChatAudioOutputChanged\(PartyChatControl?, PartyAudioOutputState, uint\)

The local render device's state changed.

```csharp
public PartyLocalChatAudioOutputChanged(PartyChatControl? ChatControl, PartyAudioOutputState State, uint ErrorDetail)
```

#### Parameters

`ChatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

The chat control.

`State` [PartyAudioOutputState](GDK.Net.PlayFab.Party.PartyAudioOutputState.md)

The new state.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when the device failed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioOutputChanged_ChatControl"></a> ChatControl

The chat control.

```csharp
public PartyChatControl? ChatControl { get; init; }
```

#### Property Value

 [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

### <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioOutputChanged_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when the device failed.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyLocalChatAudioOutputChanged_State"></a> State

The new state.

```csharp
public PartyAudioOutputState State { get; init; }
```

#### Property Value

 [PartyAudioOutputState](GDK.Net.PlayFab.Party.PartyAudioOutputState.md)

