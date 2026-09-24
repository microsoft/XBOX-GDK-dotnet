# <a id="GDK_Net_Users_User"></a> Class User

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

A signed-in user. Wraps <code>XUserHandle</code>.

```csharp
public sealed class User : IDisposable, IEquatable<User>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[User](GDK.Net.Users.User.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable), 
[IEquatable<User\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
The native handle is owned by a <xref href="System.Runtime.InteropServices.SafeHandle" data-throw-if-not-resolved="false"></xref>, so a
missed <xref href="GDK.Net.Users.User.Dispose" data-throw-if-not-resolved="false"></xref> still releases it at finalization. <xref href="GDK.Net.Users.User.Duplicate" data-throw-if-not-resolved="false"></xref> produces
an independent instance with its own handle.
</p>
<p>
Equality follows <code>XUserCompare</code>. Because a comparison function cannot yield a hash,
<xref href="GDK.Net.Users.User.GetHashCode" data-throw-if-not-resolved="false"></xref> hashes the stable <xref href="GDK.Net.Users.User.LocalId" data-throw-if-not-resolved="false"></xref>, which every handle for the
same user shares.
</p>

## Properties

### <a id="GDK_Net_Users_User_AgeGroup"></a> AgeGroup

The user's age group (<code>XUserGetAgeGroup</code>).

```csharp
public UserAgeGroup AgeGroup { get; }
```

#### Property Value

 [UserAgeGroup](GDK.Net.Users.UserAgeGroup.md)

### <a id="GDK_Net_Users_User_Id"></a> Id

The user's Xbox user id (<code>XUserGetId</code>). Cached; stable for the handle's lifetime.

```csharp
public ulong Id { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Users_User_IsGuest"></a> IsGuest

Whether the user is a guest (<code>XUserGetIsGuest</code>).

```csharp
public bool IsGuest { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Users_User_IsStoreUser"></a> IsStoreUser

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this user is the store user
(<code>XUserIsStoreUser</code>).

```csharp
public bool IsStoreUser { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Users_User_LocalId"></a> LocalId

The machine-stable local id (<code>XUserGetLocalId</code>). Cached; also backs <xref href="GDK.Net.Users.User.GetHashCode" data-throw-if-not-resolved="false"></xref>.

```csharp
public UserLocalId LocalId { get; }
```

#### Property Value

 [UserLocalId](GDK.Net.Users.UserLocalId.md)

### <a id="GDK_Net_Users_User_State"></a> State

The user's current sign-in state (<code>XUserGetState</code>).

```csharp
public UserState State { get; }
```

#### Property Value

 [UserState](GDK.Net.Users.UserState.md)

## Methods

### <a id="GDK_Net_Users_User_CheckPrivilege_GDK_Net_Users_UserPrivilege_GDK_Net_Users_UserPrivilegeDenyReason__GDK_Net_Users_UserPrivilegeOptions_"></a> CheckPrivilege\(UserPrivilege, out UserPrivilegeDenyReason, UserPrivilegeOptions\)

Checks a privilege for this user (<code>XUserCheckPrivilege</code>).

```csharp
public bool CheckPrivilege(UserPrivilege privilege, out UserPrivilegeDenyReason denyReason, UserPrivilegeOptions options = UserPrivilegeOptions.None)
```

#### Parameters

`privilege` [UserPrivilege](GDK.Net.Users.UserPrivilege.md)

The privilege to test.

`denyReason` [UserPrivilegeDenyReason](GDK.Net.Users.UserPrivilegeDenyReason.md)

Why the privilege was denied; <xref href="GDK.Net.Users.UserPrivilegeDenyReason.None" data-throw-if-not-resolved="false"></xref> when granted.

`options` [UserPrivilegeOptions](GDK.Net.Users.UserPrivilegeOptions.md)

Scope of the check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the user holds the privilege.

### <a id="GDK_Net_Users_User_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Users_User_Duplicate"></a> Duplicate\(\)

Returns an independent instance backed by its own native handle
(<code>XUserDuplicateHandle</code>).

```csharp
public User Duplicate()
```

#### Returns

 [User](GDK.Net.Users.User.md)

### <a id="GDK_Net_Users_User_Equals_GDK_Net_Users_User_"></a> Equals\(User?\)

Compares two users with <code>XUserCompare</code>. Falls back to <xref href="GDK.Net.Users.User.LocalId" data-throw-if-not-resolved="false"></xref> when either
instance has already been disposed, so equality never throws.

```csharp
public bool Equals(User? other)
```

#### Parameters

`other` [User](GDK.Net.Users.User.md)?

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Users_User_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_Users_User_FindControllerWithUiAsync_System_Threading_CancellationToken_"></a> FindControllerWithUiAsync\(CancellationToken\)

Presents system UI to help the user pair a controller
(<code>XUserFindControllerForUserWithUiAsync</code>).

```csharp
public Task<AppLocalDeviceId> FindControllerWithUiAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)\>

The <xref href="GDK.Net.Users.AppLocalDeviceId" data-throw-if-not-resolved="false"></xref> of the controller the user paired, or
<xref href="GDK.Net.Users.AppLocalDeviceId.Null" data-throw-if-not-resolved="false"></xref> when no controller was paired.

### <a id="GDK_Net_Users_User_GetDefaultAudioEndpointUtf16_GDK_Net_Users_UserDefaultAudioEndpointKind_"></a> GetDefaultAudioEndpointUtf16\(UserDefaultAudioEndpointKind\)

Returns the default audio endpoint id for the given role
(<code>XUserGetDefaultAudioEndpointUtf16</code>).

```csharp
public string? GetDefaultAudioEndpointUtf16(UserDefaultAudioEndpointKind kind)
```

#### Parameters

`kind` [UserDefaultAudioEndpointKind](GDK.Net.Users.UserDefaultAudioEndpointKind.md)

Which endpoint role to query.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The endpoint id string, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the user has no default endpoint
for this role.

### <a id="GDK_Net_Users_User_GetGamerPictureAsync_GDK_Net_Users_GamerPictureSize_System_Threading_CancellationToken_"></a> GetGamerPictureAsync\(GamerPictureSize, CancellationToken\)

Downloads the user's gamer picture at <code class="paramref">size</code>
(<code>XUserGetGamerPictureAsync</code>).

```csharp
public Task<byte[]> GetGamerPictureAsync(GamerPictureSize size = GamerPictureSize.Medium, CancellationToken cancellationToken = default)
```

#### Parameters

`size` [GamerPictureSize](GDK.Net.Users.GamerPictureSize.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]\>

### <a id="GDK_Net_Users_User_GetGamertag_GDK_Net_Users_GamertagComponent_"></a> GetGamertag\(GamertagComponent\)

Reads a component of the user's gamertag (<code>XUserGetGamertag</code>).

```csharp
public string GetGamertag(GamertagComponent component = GamertagComponent.UniqueModern)
```

#### Parameters

`component` [GamertagComponent](GDK.Net.Users.GamertagComponent.md)

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

The native call is a sized two-call pattern. The GDK publishes a maximum byte count per
component, so the first attempt uses that; the loop only re-runs if a future edition needs
more room.

### <a id="GDK_Net_Users_User_GetHashCode"></a> GetHashCode\(\)

Serves as the default hash function.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A hash code for the current object.

### <a id="GDK_Net_Users_User_GetTokenAndSignatureAsync_GDK_Net_Users_TokenAndSignatureOptions_System_String_System_String_System_Collections_Generic_IReadOnlyList_GDK_Net_Users_TokenAndSignatureHttpHeader__System_Byte___System_Threading_CancellationToken_"></a> GetTokenAndSignatureAsync\(TokenAndSignatureOptions, string, string, IReadOnlyList<TokenAndSignatureHttpHeader\>?, byte\[\]?, CancellationToken\)

Requests an Xbox Live token and optional body signature for the specified HTTP request
(<code>XUserGetTokenAndSignatureAsync</code>). Method and URL are marshalled as UTF-8.

```csharp
public Task<TokenAndSignature> GetTokenAndSignatureAsync(TokenAndSignatureOptions options, string method, string url, IReadOnlyList<TokenAndSignatureHttpHeader>? headers = null, byte[]? body = null, CancellationToken cancellationToken = default)
```

#### Parameters

`options` [TokenAndSignatureOptions](GDK.Net.Users.TokenAndSignatureOptions.md)

Request options (e.g. force-refresh).

`method` [string](https://learn.microsoft.com/dotnet/api/system.string)

The HTTP method string, e.g. <code>"GET"</code>.

`url` [string](https://learn.microsoft.com/dotnet/api/system.string)

The request URL.

`headers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[TokenAndSignatureHttpHeader](GDK.Net.Users.TokenAndSignatureHttpHeader.md)\>?

Optional HTTP headers to include in the signature computation.

`body` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional request body bytes to sign.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TokenAndSignature](GDK.Net.Users.TokenAndSignature.md)\>

### <a id="GDK_Net_Users_User_GetTokenAndSignatureUtf16Async_GDK_Net_Users_TokenAndSignatureOptions_System_String_System_String_System_Collections_Generic_IReadOnlyList_GDK_Net_Users_TokenAndSignatureHttpHeader__System_Byte___System_Threading_CancellationToken_"></a> GetTokenAndSignatureUtf16Async\(TokenAndSignatureOptions, string, string, IReadOnlyList<TokenAndSignatureHttpHeader\>?, byte\[\]?, CancellationToken\)

Requests an Xbox Live token and optional body signature for the specified HTTP request
(<code>XUserGetTokenAndSignatureUtf16Async</code>). Method and URL are marshalled as UTF-16.

```csharp
public Task<TokenAndSignature> GetTokenAndSignatureUtf16Async(TokenAndSignatureOptions options, string method, string url, IReadOnlyList<TokenAndSignatureHttpHeader>? headers = null, byte[]? body = null, CancellationToken cancellationToken = default)
```

#### Parameters

`options` [TokenAndSignatureOptions](GDK.Net.Users.TokenAndSignatureOptions.md)

Request options (e.g. force-refresh).

`method` [string](https://learn.microsoft.com/dotnet/api/system.string)

The HTTP method string, e.g. <code>"GET"</code>.

`url` [string](https://learn.microsoft.com/dotnet/api/system.string)

The request URL.

`headers` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[TokenAndSignatureHttpHeader](GDK.Net.Users.TokenAndSignatureHttpHeader.md)\>?

Optional HTTP headers to include in the signature computation.

`body` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional request body bytes to sign.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[TokenAndSignature](GDK.Net.Users.TokenAndSignature.md)\>

### <a id="GDK_Net_Users_User_IsSignOutPresent"></a> IsSignOutPresent\(\)

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when a sign-out is currently in progress
(<code>XUserIsSignOutPresent</code>).

```csharp
public static bool IsSignOutPresent()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

See <xref href="GDK.Net.Users.User.SignOutAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_Users_User_ResolveIssueWithUiAsync_System_String_System_Threading_CancellationToken_"></a> ResolveIssueWithUiAsync\(string?, CancellationToken\)

Displays a system UI that lets the user resolve a pending account issue
(<code>XUserResolveIssueWithUiAsync</code>). The URL and method are marshalled as UTF-8.

```csharp
public Task ResolveIssueWithUiAsync(string? url = null, CancellationToken cancellationToken = default)
```

#### Parameters

`url` [string](https://learn.microsoft.com/dotnet/api/system.string)?

An optional URL to open in the resolution UI, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_Users_User_ResolveIssueWithUiUtf16Async_System_String_System_Threading_CancellationToken_"></a> ResolveIssueWithUiUtf16Async\(string?, CancellationToken\)

Displays a system UI that lets the user resolve a pending account issue
(<code>XUserResolveIssueWithUiUtf16Async</code>). The URL is marshalled as UTF-16.

```csharp
public Task ResolveIssueWithUiUtf16Async(string? url = null, CancellationToken cancellationToken = default)
```

#### Parameters

`url` [string](https://learn.microsoft.com/dotnet/api/system.string)?

An optional URL to open in the resolution UI, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_Users_User_ResolvePrivilegeWithUiAsync_GDK_Net_Users_UserPrivilege_GDK_Net_Users_UserPrivilegeOptions_System_Threading_CancellationToken_"></a> ResolvePrivilegeWithUiAsync\(UserPrivilege, UserPrivilegeOptions, CancellationToken\)

Displays a system UI that lets the user resolve a missing or denied privilege
(<code>XUserResolvePrivilegeWithUiAsync</code>).

```csharp
public Task ResolvePrivilegeWithUiAsync(UserPrivilege privilege, UserPrivilegeOptions options = UserPrivilegeOptions.None, CancellationToken cancellationToken = default)
```

#### Parameters

`privilege` [UserPrivilege](GDK.Net.Users.UserPrivilege.md)

The privilege the title requires.

`options` [UserPrivilegeOptions](GDK.Net.Users.UserPrivilegeOptions.md)

Scope options.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_Users_User_SignOutAsync_System_Threading_CancellationToken_"></a> SignOutAsync\(CancellationToken\)

Signs this user out (<code>XUserSignOutAsync</code>).

```csharp
public Task SignOutAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Sign-out is normally driven by the user through the system UI. Prefer observing
<xref href="GDK.Net.Users.UserManager.UserChanged" data-throw-if-not-resolved="false"></xref> and <xref href="GDK.Net.Users.User.State" data-throw-if-not-resolved="false"></xref> over calling this.

### <a id="GDK_Net_Users_User_ToString"></a> ToString\(\)

Returns the Xbox user id and local id for diagnostics.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Operators

### <a id="GDK_Net_Users_User_op_Equality_GDK_Net_Users_User_GDK_Net_Users_User_"></a> operator ==\(User?, User?\)

Equality operator.

```csharp
public static bool operator ==(User? left, User? right)
```

#### Parameters

`left` [User](GDK.Net.Users.User.md)?

`right` [User](GDK.Net.Users.User.md)?

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Users_User_op_Inequality_GDK_Net_Users_User_GDK_Net_Users_User_"></a> operator \!=\(User?, User?\)

Inequality operator.

```csharp
public static bool operator !=(User? left, User? right)
```

#### Parameters

`left` [User](GDK.Net.Users.User.md)?

`right` [User](GDK.Net.Users.User.md)?

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

