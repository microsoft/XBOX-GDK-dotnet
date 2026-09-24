# <a id="GDK_Net_PlayFab_PlayFabLocalUser"></a> Class PlayFabLocalUser

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Binds a platform user to a PlayFab title so the PlayFab libraries can log in and silently
re-authenticate on the title's behalf. Wraps <code>PFLocalUserHandle</code>.

```csharp
public sealed class PlayFabLocalUser : IDisposable, IEquatable<PlayFabLocalUser>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable), 
[IEquatable<PlayFabLocalUser\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

A local user is the GDK-recommended entry point: it keeps the association between an
<xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> and the PlayFab entity, so an expired entity token is refreshed without the
title re-running the login flow.

## Properties

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_LocalId"></a> LocalId

The machine-stable local id for this user (<code>PFLocalUserGetLocalId</code>).

```csharp
public string LocalId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_ServiceConfig"></a> ServiceConfig

The service configuration this local user was created with
(<code>PFLocalUserGetServiceConfigHandle</code>).

```csharp
public PlayFabServiceConfig ServiceConfig { get; }
```

#### Property Value

 [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

## Methods

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_CreateForSteamUser_GDK_Net_PlayFab_PlayFabServiceConfig_"></a> CreateForSteamUser\(PlayFabServiceConfig\)

Creates a local user for the signed-in Steam user
(<code>PFLocalUserCreateHandleWithSteamUser</code>).

```csharp
public static PlayFabLocalUser CreateForSteamUser(PlayFabServiceConfig serviceConfig)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

#### Returns

 [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_CreateForXboxUser_GDK_Net_PlayFab_PlayFabServiceConfig_GDK_Net_Users_User_"></a> CreateForXboxUser\(PlayFabServiceConfig, User\)

Creates a local user for a signed-in Xbox user
(<code>PFLocalUserCreateHandleWithXboxUser</code>).

```csharp
public static PlayFabLocalUser CreateForXboxUser(PlayFabServiceConfig serviceConfig, User user)
```

#### Parameters

`serviceConfig` [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

`user` [User](../Users/GDK.Net.Users.User.md)

#### Returns

 [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_Dispose"></a> Dispose\(\)

Releases the native handle.

```csharp
public void Dispose()
```

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_Duplicate"></a> Duplicate\(\)

Returns an independent instance backed by its own native handle
(<code>PFLocalUserDuplicateHandle</code>).

```csharp
public PlayFabLocalUser Duplicate()
```

#### Returns

 [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_Equals_GDK_Net_PlayFab_PlayFabLocalUser_"></a> Equals\(PlayFabLocalUser?\)

Compares two local users by identity (<code>PFLocalUserHandleCompare</code>).

```csharp
public bool Equals(PlayFabLocalUser? other)
```

#### Parameters

`other` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)?

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_Equals_System_Object_"></a> Equals\(object?\)

Determines whether the specified object is equal to the current object.

```csharp
public override bool Equals(object? obj)
```

#### Parameters

`obj` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The object to compare with the current object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the specified object  is equal to the current object; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_GetHashCode"></a> GetHashCode\(\)

Serves as the default hash function.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A hash code for the current object.

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_LoginAsync_System_Boolean_System_Threading_CancellationToken_"></a> LoginAsync\(bool, CancellationToken\)

Logs the local user in to PlayFab (<code>PFLocalUserLoginAsync</code>).

```csharp
public Task<PlayFabLoginResult> LoginAsync(bool createAccount = true, CancellationToken cancellationToken = default)
```

#### Parameters

`createAccount` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether PlayFab may create an account when the platform user has never played the title.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PlayFabLoginResult](GDK.Net.PlayFab.PlayFabLoginResult.md)\>

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_TryGetEntity"></a> TryGetEntity\(\)

Returns the entity this local user is already logged in as, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it
has not logged in yet (<code>PFLocalUserTryGetEntityHandle</code>).

```csharp
public PlayFabEntity? TryGetEntity()
```

#### Returns

 [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)?

### <a id="GDK_Net_PlayFab_PlayFabLocalUser_TryGetXboxUser"></a> TryGetXboxUser\(\)

Returns the Xbox user this local user was created from, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it was
not created from one (<code>PFLocalUserTryGetXUser</code>).

```csharp
public User? TryGetXboxUser()
```

#### Returns

 [User](../Users/GDK.Net.Users.User.md)?

