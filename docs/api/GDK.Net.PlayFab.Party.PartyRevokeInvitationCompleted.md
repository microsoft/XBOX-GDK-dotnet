# <a id="GDK_Net_PlayFab_Party_PartyRevokeInvitationCompleted"></a> Class PartyRevokeInvitationCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Party.PartyNetwork.RevokeInvitation(GDK.Net.PlayFab.Party.PartyLocalUser%2cGDK.Net.PlayFab.Party.PartyInvitation)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public sealed record PartyRevokeInvitationCompleted : PartyOperationCompleted, IEquatable<PartyStateChange>, IEquatable<PartyOperationCompleted>, IEquatable<PartyRevokeInvitationCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md) ← 
[PartyRevokeInvitationCompleted](GDK.Net.PlayFab.Party.PartyRevokeInvitationCompleted.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyRevokeInvitationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyOperationCompleted.Operation](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Operation), 
[PartyOperationCompleted.Result](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Result), 
[PartyOperationCompleted.ErrorDetail](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_ErrorDetail), 
[PartyOperationCompleted.Succeeded](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Succeeded), 
[PartyOperationCompleted.Error](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Error), 
[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyRevokeInvitationCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyLocalUser_GDK_Net_PlayFab_Party_PartyInvitation_"></a> PartyRevokeInvitationCompleted\(PartyOperationId, PartyStateChangeResult, uint, PartyNetwork?, PartyLocalUser?, PartyInvitation?\)

A <xref href="GDK.Net.PlayFab.Party.PartyNetwork.RevokeInvitation(GDK.Net.PlayFab.Party.PartyLocalUser%2cGDK.Net.PlayFab.Party.PartyInvitation)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public PartyRevokeInvitationCompleted(PartyOperationId Operation, PartyStateChangeResult Result, uint ErrorDetail, PartyNetwork? Network, PartyLocalUser? LocalUser, PartyInvitation? Invitation)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network the invitation belonged to.

`LocalUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

The user that revoked it.

`Invitation` [PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md)?

The revoked invitation.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyRevokeInvitationCompleted_Invitation"></a> Invitation

The revoked invitation.

```csharp
public PartyInvitation? Invitation { get; init; }
```

#### Property Value

 [PartyInvitation](GDK.Net.PlayFab.Party.PartyInvitation.md)?

### <a id="GDK_Net_PlayFab_Party_PartyRevokeInvitationCompleted_LocalUser"></a> LocalUser

The user that revoked it.

```csharp
public PartyLocalUser? LocalUser { get; init; }
```

#### Property Value

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyRevokeInvitationCompleted_Network"></a> Network

The network the invitation belonged to.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

