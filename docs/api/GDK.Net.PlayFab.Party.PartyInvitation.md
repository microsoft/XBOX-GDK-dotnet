# <a id="GDK_Net_PlayFab_Party_PartyInvitation"></a> Class PartyInvitation

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_INVITATION_HANDLE</code>: an outstanding invitation to a network.

```csharp
public sealed class PartyInvitation : PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md) ← 
[PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md)

#### Inherited Members

[PartyObject.IsValid](GDK.Net.PlayFab.Party.PartyObject.md\#GDK\_Net\_PlayFab\_Party\_PartyObject\_IsValid), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyInvitation_Configuration"></a> Configuration

The invitation's identifier, revocability and admitted entity ids.

```csharp
public PartyInvitationConfiguration Configuration { get; }
```

#### Property Value

 [PartyInvitationConfiguration](GDK.Net.PlayFab.Party.PartyInvitationConfiguration.md)

### <a id="GDK_Net_PlayFab_Party_PartyInvitation_CreatorEntityId"></a> CreatorEntityId

The entity id of the user that created the invitation.

```csharp
public string? CreatorEntityId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

