# <a id="GDK_Net_PlayFab_Party_PartyAuthenticateLocalUserCompleted"></a> Class PartyAuthenticateLocalUserCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Party.PartyNetwork.AuthenticateLocalUser(GDK.Net.PlayFab.Party.PartyLocalUser%2cSystem.String)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public sealed record PartyAuthenticateLocalUserCompleted : PartyOperationCompleted, IEquatable<PartyStateChange>, IEquatable<PartyOperationCompleted>, IEquatable<PartyAuthenticateLocalUserCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md) ← 
[PartyAuthenticateLocalUserCompleted](GDK.Net.PlayFab.Party.PartyAuthenticateLocalUserCompleted.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyAuthenticateLocalUserCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

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

### <a id="GDK_Net_PlayFab_Party_PartyAuthenticateLocalUserCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyLocalUser_System_String_"></a> PartyAuthenticateLocalUserCompleted\(PartyOperationId, PartyStateChangeResult, uint, PartyNetwork?, PartyLocalUser?, string?\)

A <xref href="GDK.Net.PlayFab.Party.PartyNetwork.AuthenticateLocalUser(GDK.Net.PlayFab.Party.PartyLocalUser%2cSystem.String)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public PartyAuthenticateLocalUserCompleted(PartyOperationId Operation, PartyStateChangeResult Result, uint ErrorDetail, PartyNetwork? Network, PartyLocalUser? LocalUser, string? InvitationIdentifier)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network the user was authenticating into.

`LocalUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

The user that was authenticating.

`InvitationIdentifier` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The invitation that admitted the user.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyAuthenticateLocalUserCompleted_InvitationIdentifier"></a> InvitationIdentifier

The invitation that admitted the user.

```csharp
public string? InvitationIdentifier { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyAuthenticateLocalUserCompleted_LocalUser"></a> LocalUser

The user that was authenticating.

```csharp
public PartyLocalUser? LocalUser { get; init; }
```

#### Property Value

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyAuthenticateLocalUserCompleted_Network"></a> Network

The network the user was authenticating into.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

