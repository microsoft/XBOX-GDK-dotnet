# <a id="GDK_Net_PlayFab_Party_PartyXblLoginToPlayFabCompleted"></a> Class PartyXblLoginToPlayFabCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Party.PartyXblChatUser.LoginToPlayFab" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public sealed record PartyXblLoginToPlayFabCompleted : PartyXblOperationCompleted, IEquatable<PartyXblStateChange>, IEquatable<PartyXblOperationCompleted>, IEquatable<PartyXblLoginToPlayFabCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblStateChange](GDK.Net.PlayFab.Party.PartyXblStateChange.md) ← 
[PartyXblOperationCompleted](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md) ← 
[PartyXblLoginToPlayFabCompleted](GDK.Net.PlayFab.Party.PartyXblLoginToPlayFabCompleted.md)

#### Implements

[IEquatable<PartyXblStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyXblLoginToPlayFabCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyXblOperationCompleted.Operation](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_Operation), 
[PartyXblOperationCompleted.Result](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_Result), 
[PartyXblOperationCompleted.ErrorDetail](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_ErrorDetail), 
[PartyXblOperationCompleted.Succeeded](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_Succeeded), 
[PartyXblOperationCompleted.Error](GDK.Net.PlayFab.Party.PartyXblOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyXblOperationCompleted\_Error), 
[PartyXblStateChange.Kind](GDK.Net.PlayFab.Party.PartyXblStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyXblStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyXblLoginToPlayFabCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyXblStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyXblChatUser_System_String_System_String_System_DateTimeOffset_"></a> PartyXblLoginToPlayFabCompleted\(PartyOperationId, PartyXblStateChangeResult, uint, PartyXblChatUser?, string?, string?, DateTimeOffset\)

A <xref href="GDK.Net.PlayFab.Party.PartyXblChatUser.LoginToPlayFab" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public PartyXblLoginToPlayFabCompleted(PartyOperationId Operation, PartyXblStateChangeResult Result, uint ErrorDetail, PartyXblChatUser? LocalChatUser, string? EntityId, string? TitlePlayerEntityToken, DateTimeOffset ExpirationTime)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyXblStateChangeResult](GDK.Net.PlayFab.Party.PartyXblStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`LocalChatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

The chat user that logged in.

`EntityId` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The PlayFab entity id the Xbox Live user maps to.

`TitlePlayerEntityToken` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The entity token to pass to PlayFab services.

`ExpirationTime` [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

When the token expires.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblLoginToPlayFabCompleted_EntityId"></a> EntityId

The PlayFab entity id the Xbox Live user maps to.

```csharp
public string? EntityId { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyXblLoginToPlayFabCompleted_ExpirationTime"></a> ExpirationTime

When the token expires.

```csharp
public DateTimeOffset ExpirationTime { get; init; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_PlayFab_Party_PartyXblLoginToPlayFabCompleted_LocalChatUser"></a> LocalChatUser

The chat user that logged in.

```csharp
public PartyXblChatUser? LocalChatUser { get; init; }
```

#### Property Value

 [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyXblLoginToPlayFabCompleted_TitlePlayerEntityToken"></a> TitlePlayerEntityToken

The entity token to pass to PlayFab services.

```csharp
public string? TitlePlayerEntityToken { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

