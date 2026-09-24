# <a id="GDK_Net_PlayFab_Party_PartyInvitationDestroyed"></a> Class PartyInvitationDestroyed

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

An invitation to the network is no longer valid.

```csharp
public sealed record PartyInvitationDestroyed : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyInvitationDestroyed>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyInvitationDestroyed](GDK.Net.PlayFab.Party.PartyInvitationDestroyed.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyInvitationDestroyed\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyInvitationDestroyed__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyInvitation_GDK_Net_PlayFab_Party_PartyDestroyedReason_System_UInt32_"></a> PartyInvitationDestroyed\(PartyNetwork?, PartyInvitation?, PartyDestroyedReason, uint\)

An invitation to the network is no longer valid.

```csharp
public PartyInvitationDestroyed(PartyNetwork? Network, PartyInvitation? Invitation, PartyDestroyedReason Reason, uint ErrorDetail)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`Invitation` [PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md)?

The invitation.

`Reason` [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

Why it went away.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when the teardown was involuntary.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyInvitationDestroyed_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when the teardown was involuntary.

```csharp
public uint ErrorDetail { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyInvitationDestroyed_Invitation"></a> Invitation

The invitation.

```csharp
public PartyInvitation? Invitation { get; init; }
```

#### Property Value

 [PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md)?

### <a id="GDK_Net_PlayFab_Party_PartyInvitationDestroyed_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

### <a id="GDK_Net_PlayFab_Party_PartyInvitationDestroyed_Reason"></a> Reason

Why it went away.

```csharp
public PartyDestroyedReason Reason { get; init; }
```

#### Property Value

 [PartyDestroyedReason](GDK.Net.PlayFab.Party.PartyDestroyedReason.md)

