# <a id="GDK_Net_PlayFab_Party_PartyInvitationCreated"></a> Class PartyInvitationCreated

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

An invitation to the network became known to the local device.

```csharp
public sealed record PartyInvitationCreated : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyInvitationCreated>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyInvitationCreated](GDK.Net.PlayFab.Party.PartyInvitationCreated.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyInvitationCreated\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyInvitationCreated__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyInvitation_"></a> PartyInvitationCreated\(PartyNetwork?, PartyInvitation?\)

An invitation to the network became known to the local device.

```csharp
public PartyInvitationCreated(PartyNetwork? Network, PartyInvitation? Invitation)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`Invitation` [PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md)?

The invitation.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyInvitationCreated_Invitation"></a> Invitation

The invitation.

```csharp
public PartyInvitation? Invitation { get; init; }
```

#### Property Value

 [PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md)?

### <a id="GDK_Net_PlayFab_Party_PartyInvitationCreated_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

