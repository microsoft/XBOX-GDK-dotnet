# <a id="GDK_Net_PlayFab_Party_PartyChatControlDestroyed"></a> Class PartyChatControlDestroyed

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A chat control is no longer known to the local device.

```csharp
public sealed record PartyChatControlDestroyed : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyChatControlDestroyed>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyChatControlDestroyed](GDK.Net.PlayFab.Party.PartyChatControlDestroyed.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyChatControlDestroyed\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyChatControlDestroyed__ctor_GDK_Net_PlayFab_Party_PartyChatControl_GDK_Net_PlayFab_Party_PartyDestroyedReason_System_UInt32_"></a> PartyChatControlDestroyed\(PartyChatControl?, PartyDestroyedReason, uint\)

A chat control is no longer known to the local device.

```csharp
public PartyChatControlDestroyed(PartyChatControl? ChatControl, PartyDestroyedReason Reason, uint ErrorDetail)
```

#### Parameters

`ChatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

The chat control.

`Reason` [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

Why it went away.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when the teardown was involuntary.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyChatControlDestroyed_ChatControl"></a> ChatControl

The chat control.

```csharp
public PartyChatControl? ChatControl { get; init; }
```

#### Property Value

 [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)?

### <a id="GDK_Net_PlayFab_Party_PartyChatControlDestroyed_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when the teardown was involuntary.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyChatControlDestroyed_Reason"></a> Reason

Why it went away.

```csharp
public PartyDestroyedReason Reason { get; init; }
```

#### Property Value

 [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

