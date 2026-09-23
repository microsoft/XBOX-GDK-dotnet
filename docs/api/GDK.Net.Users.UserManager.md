# <a id="GDK_Net_Users_UserManager"></a> Class UserManager

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

User sign-in and change notifications. Reached through <xref href="GDK.Net.GameRuntime.Users" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class UserManager : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[UserManager](GDK.Net.Users.UserManager.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The native change-event registration is created lazily on the first
<xref href="GDK.Net.Users.UserManager.UserChanged" data-throw-if-not-resolved="false"></xref> subscription and released on <xref href="GDK.Net.Users.UserManager.Dispose" data-throw-if-not-resolved="false"></xref> with
<code>XUserUnregisterForChangeEvent(token, wait: true)</code>, so no callback can be in flight once
the manager is gone.

## Methods

### <a id="GDK_Net_Users_UserManager_AddAsync_GDK_Net_Users_UserAddOptions_System_Threading_CancellationToken_"></a> AddAsync\(UserAddOptions, CancellationToken\)

Adds a user (<code>XUserAddAsync</code> / <code>XUserAddResult</code>).

```csharp
public Task<User> AddAsync(UserAddOptions options = UserAddOptions.AddDefaultUserSilently, CancellationToken cancellationToken = default)
```

#### Parameters

`options` [UserAddOptions](GDK.Net.Users.UserAddOptions.md)

Use <xref href="GDK.Net.Users.UserAddOptions.AddDefaultUserSilently" data-throw-if-not-resolved="false"></xref> at startup and fall back to
<xref href="GDK.Net.Users.UserAddOptions.AddDefaultUserAllowingUI" data-throw-if-not-resolved="false"></xref> when that fails.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[User](GDK.Net.Users.User.md)\>

### <a id="GDK_Net_Users_UserManager_AddByIdWithUiAsync_System_UInt64_System_Threading_CancellationToken_"></a> AddByIdWithUiAsync\(ulong, CancellationToken\)

Adds a user by Xbox user id, showing system UI if needed
(<code>XUserAddByIdWithUiAsync</code> / <code>XUserAddByIdWithUiResult</code>).

```csharp
public Task<User> AddByIdWithUiAsync(ulong userId, CancellationToken cancellationToken = default)
```

#### Parameters

`userId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id of the user to add.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[User](GDK.Net.Users.User.md)\>

### <a id="GDK_Net_Users_UserManager_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Users_UserManager_FindForDevice_GDK_Net_Users_AppLocalDeviceId_"></a> FindForDevice\(AppLocalDeviceId\)

Finds the user associated with a device (<code>XUserFindForDevice</code>).

```csharp
public User FindForDevice(AppLocalDeviceId deviceId)
```

#### Parameters

`deviceId` [AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)

The device identifier.

#### Returns

 [User](GDK.Net.Users.User.md)

A new <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> for the user associated with the device.

### <a id="GDK_Net_Users_UserManager_FindUserById_System_UInt64_"></a> FindUserById\(ulong\)

Finds an already-signed-in user by Xbox user id (<code>XUserFindUserById</code>).

```csharp
public User FindUserById(ulong userId)
```

#### Parameters

`userId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id to look up.

#### Returns

 [User](GDK.Net.Users.User.md)

A new <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> for the located user.

#### Exceptions

 [UserException](GDK.Net.UserException.md)

Thrown when no user with <code class="paramref">userId</code> is signed in
(<xref href="GDK.Net.HResult.EGameUserUserNotFound" data-throw-if-not-resolved="false"></xref>).

### <a id="GDK_Net_Users_UserManager_FindUserByLocalId_GDK_Net_Users_UserLocalId_"></a> FindUserByLocalId\(UserLocalId\)

Finds an already-signed-in user by local id (<code>XUserFindUserByLocalId</code>).

```csharp
public User FindUserByLocalId(UserLocalId localId)
```

#### Parameters

`localId` [UserLocalId](GDK.Net.Users.UserLocalId.md)

The local id to look up.

#### Returns

 [User](GDK.Net.Users.User.md)

A new <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> for the located user.

### <a id="GDK_Net_Users_UserManager_GetMaxUsers"></a> GetMaxUsers\(\)

Returns the maximum number of users that can be signed in simultaneously
(<code>XUserGetMaxUsers</code>).

```csharp
public uint GetMaxUsers()
```

#### Returns

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Users_UserManager_GetSignOutDeferral"></a> GetSignOutDeferral\(\)

Acquires a sign-out deferral that prevents the Gaming Runtime from completing the current
sign-out until the returned object is disposed (<code>XUserGetSignOutDeferral</code>).

```csharp
public SignOutDeferral GetSignOutDeferral()
```

#### Returns

 [SignOutDeferral](GDK.Net.Users.SignOutDeferral.md)

#### Remarks

Call this inside a <xref href="GDK.Net.Users.UserManager.UserChanged" data-throw-if-not-resolved="false"></xref> handler when
<code>UserChangeEvent.SigningOut</code> fires to delay sign-out while the title completes any
work that must finish before the user is removed (for example, saving game state).

### <a id="GDK_Net_Users_UserManager_DefaultAudioEndpointChanged"></a> DefaultAudioEndpointChanged

Raised when a user's default audio endpoint changes
(<code>XUserRegisterForDefaultAudioEndpointUtf16Changed</code>).

```csharp
public event EventHandler<UserDefaultAudioEndpointChangedEventArgs>? DefaultAudioEndpointChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[UserDefaultAudioEndpointChangedEventArgs](GDK.Net.Users.UserDefaultAudioEndpointChangedEventArgs.md)\>?

### <a id="GDK_Net_Users_UserManager_DeviceAssociationChanged"></a> DeviceAssociationChanged

Raised when a device's user association changes
(<code>XUserRegisterForDeviceAssociationChanged</code>).

```csharp
public event EventHandler<UserDeviceAssociationChangedEventArgs>? DeviceAssociationChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[UserDeviceAssociationChangedEventArgs](GDK.Net.Users.UserDeviceAssociationChangedEventArgs.md)\>?

#### Remarks

The runtime replays all current associations on the first subscription. Delivered on the
manager's task queue.

### <a id="GDK_Net_Users_UserManager_UserChanged"></a> UserChanged

Raised when a user's sign-in state, gamertag, gamer picture or privileges change
(<code>XUserRegisterForChangeEvent</code>).

```csharp
public event EventHandler<UserChangedEventArgs>? UserChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[UserChangedEventArgs](GDK.Net.Users.UserChangedEventArgs.md)\>?

#### Remarks

Delivered on the manager's task queue. When the manager names no queue the Gaming Runtime
resolves the process default, so handlers arrive on the thread pool; a manager constructed
over a manual queue delivers them on whichever thread pumps that queue.

