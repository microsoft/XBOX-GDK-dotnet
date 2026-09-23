# <a id="GDK_Net_XboxLive_PrivacyService"></a> Class PrivacyService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live privacy permission checks and privacy lists. Mirrors <code>privacy_c.h</code>.

```csharp
public sealed class PrivacyService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PrivacyService](GDK.Net.XboxLive.PrivacyService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Privacy checks are certification-sensitive gates for communication, multiplayer and
user-generated content. This projection fails closed by construction: permission-check methods
return <xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult" data-throw-if-not-resolved="false"></xref> objects whose <xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult.IsAllowed" data-throw-if-not-resolved="false"></xref>
is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> whenever Xbox Live could not complete the check, including native
failures and cancellation.
</p>
<p>
The mute-list and block-list change handler functions are present in the header but are not
exported by Microsoft.Xbox.Services.C.Thunks.dll in GDK edition 260404, so events are not
exposed for this family.
</p>

## Methods

### <a id="GDK_Net_XboxLive_PrivacyService_BatchCheckPermissionAsync_System_Collections_Generic_IEnumerable_GDK_Net_XboxLive_Permission__System_Collections_Generic_IEnumerable_System_UInt64__System_Collections_Generic_IEnumerable_GDK_Net_XboxLive_AnonymousUserType__System_Threading_CancellationToken_"></a> BatchCheckPermissionAsync\(IEnumerable<Permission\>, IEnumerable<ulong\>, IEnumerable<AnonymousUserType\>, CancellationToken\)

Checks several permissions against several Xbox Live and non-Xbox Live target classes
(<code>XblPrivacyBatchCheckPermissionAsync</code>).

```csharp
public Task<IReadOnlyList<PrivacyPermissionCheckResult>> BatchCheckPermissionAsync(IEnumerable<Permission> permissions, IEnumerable<ulong> targetXboxUserIds, IEnumerable<AnonymousUserType> targetAnonymousUserTypes, CancellationToken cancellationToken = default)
```

#### Parameters

`permissions` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[Permission](GDK.Net.XboxLive.Permission.md)\>

The permissions to check. An empty sequence returns an empty result.

`targetXboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Xbox Live target users. An empty sequence checks only anonymous classes.

`targetAnonymousUserTypes` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[AnonymousUserType](GDK.Net.XboxLive.AnonymousUserType.md)\>

Non-Xbox Live target classes. An empty sequence checks only Xbox Live target users.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the native call; returned results are denied.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PrivacyPermissionCheckResult](GDK.Net.XboxLive.PrivacyPermissionCheckResult.md)\>\>

One fail-closed result for each permission-and-target combination. If the batch cannot be
completed, every requested combination is returned as denied with
<xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult.WasChecked" data-throw-if-not-resolved="false"></xref> <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_XboxLive_PrivacyService_CheckPermissionAsync_GDK_Net_XboxLive_Permission_System_UInt64_System_Threading_CancellationToken_"></a> CheckPermissionAsync\(Permission, ulong, CancellationToken\)

Checks whether the signed-in user can perform an action with a target Xbox Live user
(<code>XblPrivacyCheckPermissionAsync</code>).

```csharp
public Task<PrivacyPermissionCheckResult> CheckPermissionAsync(Permission permission, ulong targetXboxUserId, CancellationToken cancellationToken = default)
```

#### Parameters

`permission` [Permission](GDK.Net.XboxLive.Permission.md)

The permission to check.

`targetXboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The target user's Xbox user id.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the native call; the returned result is denied.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PrivacyPermissionCheckResult](GDK.Net.XboxLive.PrivacyPermissionCheckResult.md)\>

A fail-closed permission result. <xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult.IsAllowed" data-throw-if-not-resolved="false"></xref> is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only when Xbox Live completed the check and explicitly granted the
permission.

### <a id="GDK_Net_XboxLive_PrivacyService_CheckPermissionForAnonymousUserAsync_GDK_Net_XboxLive_Permission_GDK_Net_XboxLive_AnonymousUserType_System_Threading_CancellationToken_"></a> CheckPermissionForAnonymousUserAsync\(Permission, AnonymousUserType, CancellationToken\)

Checks whether the signed-in user can perform an action with a class of non-Xbox Live users
(<code>XblPrivacyCheckPermissionForAnonymousUserAsync</code>).

```csharp
public Task<PrivacyPermissionCheckResult> CheckPermissionForAnonymousUserAsync(Permission permission, AnonymousUserType targetUserType, CancellationToken cancellationToken = default)
```

#### Parameters

`permission` [Permission](GDK.Net.XboxLive.Permission.md)

The permission to check.

`targetUserType` [AnonymousUserType](GDK.Net.XboxLive.AnonymousUserType.md)

The non-Xbox Live user class to check.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the native call; the returned result is denied.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PrivacyPermissionCheckResult](GDK.Net.XboxLive.PrivacyPermissionCheckResult.md)\>

A fail-closed permission result. <xref href="GDK.Net.XboxLive.PrivacyPermissionCheckResult.IsAllowed" data-throw-if-not-resolved="false"></xref> is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only when Xbox Live completed the check and explicitly granted the
permission.

### <a id="GDK_Net_XboxLive_PrivacyService_GetAvoidListAsync_System_Threading_CancellationToken_"></a> GetAvoidListAsync\(CancellationToken\)

Gets the Xbox user ids the signed-in user should avoid during multiplayer matchmaking
(<code>XblPrivacyGetAvoidListAsync</code>).

```csharp
public Task<IReadOnlyList<ulong>> GetAvoidListAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>\>

### <a id="GDK_Net_XboxLive_PrivacyService_GetMuteListAsync_System_Threading_CancellationToken_"></a> GetMuteListAsync\(CancellationToken\)

Gets the Xbox user ids the signed-in user should not hear during multiplayer matchmaking
(<code>XblPrivacyGetMuteListAsync</code>).

```csharp
public Task<IReadOnlyList<ulong>> GetMuteListAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>\>

