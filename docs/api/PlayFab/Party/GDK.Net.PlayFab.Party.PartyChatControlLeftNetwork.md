# <a id="GDK_Net_PlayFab_Party_PartyChatControlLeftNetwork"></a> Class PartyChatControlLeftNetwork

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A chat control left a network.

```csharp
public sealed record PartyChatControlLeftNetwork : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyChatControlLeftNetwork>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyChatControlLeftNetwork](GDK.Net.PlayFab.Party.PartyChatControlLeftNetwork.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyChatControlLeftNetwork\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyChatControlLeftNetwork__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyChatControl_GDK_Net_PlayFab_Party_PartyDestroyedReason_System_UInt32_"></a> PartyChatControlLeftNetwork\(PartyNetwork?, PartyChatControl?, PartyDestroyedReason, uint\)

A chat control left a network.

```csharp
public PartyChatControlLeftNetwork(PartyNetwork? Network, PartyChatControl? ChatControl, PartyDestroyedReason Reason, uint ErrorDetail)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`ChatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

The chat control.

`Reason` [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

Why it left.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when the departure was involuntary.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyChatControlLeftNetwork_ChatControl"></a> ChatControl

The chat control.

```csharp
public PartyChatControl? ChatControl { get; init; }
```

#### Property Value

 [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControlLeftNetwork_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when the departure was involuntary.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyChatControlLeftNetwork_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControlLeftNetwork_Reason"></a> Reason

Why it left.

```csharp
public PartyDestroyedReason Reason { get; init; }
```

#### Property Value

 [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

