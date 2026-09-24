# <a id="GDK_Net_XboxLive_ProfileService"></a> Class ProfileService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live profile lookups. Reached through <xref href="GDK.Net.XboxLive.XboxLiveContext.Profiles" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>profile_c.h</code>.

```csharp
public sealed class ProfileService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ProfileService](GDK.Net.XboxLive.ProfileService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_XboxLive_ProfileService_GetAsync_System_UInt64_System_Threading_CancellationToken_"></a> GetAsync\(ulong, CancellationToken\)

Gets one user's profile (<code>XblProfileGetUserProfileAsync</code>).

```csharp
public Task<UserProfile> GetAsync(ulong xboxUserId, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[UserProfile](GDK.Net.XboxLive.UserProfile.md)\>

#### Remarks

Use <xref href="GDK.Net.XboxLive.ProfileService.GetAsync(System.Collections.Generic.IEnumerable%7bSystem.UInt64%7d%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> when more than one profile
is needed: the service batches them into a single request.

### <a id="GDK_Net_XboxLive_ProfileService_GetAsync_System_Collections_Generic_IEnumerable_System_UInt64__System_Threading_CancellationToken_"></a> GetAsync\(IEnumerable<ulong\>, CancellationToken\)

Gets several users' profiles in one request (<code>XblProfileGetUserProfilesAsync</code>).

```csharp
public Task<IReadOnlyList<UserProfile>> GetAsync(IEnumerable<ulong> xboxUserIds, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

The users to look up. An empty sequence returns an empty result.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[UserProfile](GDK.Net.XboxLive.UserProfile.md)\>\>

### <a id="GDK_Net_XboxLive_ProfileService_GetForSocialGroupAsync_GDK_Net_XboxLive_SocialGroup_System_Threading_CancellationToken_"></a> GetForSocialGroupAsync\(SocialGroup, CancellationToken\)

Gets the profiles of everyone in a social group
(<code>XblProfileGetUserProfilesForSocialGroupAsync</code>).

```csharp
public Task<IReadOnlyList<UserProfile>> GetForSocialGroupAsync(SocialGroup socialGroup, CancellationToken cancellationToken = default)
```

#### Parameters

`socialGroup` [SocialGroup](GDK.Net.XboxLive.SocialGroup.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[UserProfile](GDK.Net.XboxLive.UserProfile.md)\>\>

### <a id="GDK_Net_XboxLive_ProfileService_GetOwnAsync_System_Threading_CancellationToken_"></a> GetOwnAsync\(CancellationToken\)

Gets the profile of the user this context belongs to.

```csharp
public Task<UserProfile> GetOwnAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[UserProfile](GDK.Net.XboxLive.UserProfile.md)\>

