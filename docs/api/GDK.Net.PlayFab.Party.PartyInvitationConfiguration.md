# <a id="GDK_Net_PlayFab_Party_PartyInvitationConfiguration"></a> Class PartyInvitationConfiguration

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_INVITATION_CONFIGURATION</code>.

```csharp
public sealed class PartyInvitationConfiguration
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyInvitationConfiguration](GDK.Net.PlayFab.Party.PartyInvitationConfiguration.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyInvitationConfiguration_EntityIds"></a> EntityIds

The PlayFab entity ids the invitation admits; empty means anyone.

```csharp
public IReadOnlyList<string> EntityIds { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Party_PartyInvitationConfiguration_Identifier"></a> Identifier

The invitation identifier. Leave <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to have Party generate one.

```csharp
public string? Identifier { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyInvitationConfiguration_Revocability"></a> Revocability

Who is allowed to revoke the invitation.

```csharp
public PartyInvitationRevocability Revocability { get; set; }
```

#### Property Value

 [PartyInvitationRevocability](GDK.Net.PlayFab.Party.PartyInvitationRevocability.md)

