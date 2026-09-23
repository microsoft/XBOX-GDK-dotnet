# <a id="GDK_Net_XboxLive_SocialManager"></a> Class SocialManager

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Process-global Xbox Live social manager. Mirrors <code>social_manager_c.h</code>.

```csharp
public sealed class SocialManager
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManager](GDK.Net.XboxLive.SocialManager.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Social manager is pumped: nothing happens — no local-user completions, group updates, rich
presence polling or notifications — unless the title calls <xref href="GDK.Net.XboxLive.SocialManager.DoWork" data-throw-if-not-resolved="false"></xref> regularly,
ideally once per frame.
</p>
<p>
This is XSAPI's first DoWork-pumped manager in this projection, and it differs from the
Start/Finish state-change systems: <code>XblSocialManagerDoWork</code> has no <code>Finish</code>. Native
events remain valid only until the next DoWork call, so this method deliberately snapshots the
batch into managed <xref href="GDK.Net.XboxLive.SocialManagerEvent" data-throw-if-not-resolved="false"></xref> records before returning. Long-lived social
user groups are not snapshotted; they are identity-mapped wrappers around native handles.
</p>
<p>
<xref href="GDK.Net.XboxLive.SocialManager.DoWork" data-throw-if-not-resolved="false"></xref> is not thread-safe against other social-manager calls. Drive the manager
from one thread and keep all social-manager calls on that thread.
</p>

## Properties

### <a id="GDK_Net_XboxLive_SocialManager_LocalUserCount"></a> LocalUserCount

The number of local users currently tracked by social manager.

```csharp
public ulong LocalUserCount { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_SocialManager_AddLocalUser_GDK_Net_Users_User_GDK_Net_XboxLive_SocialManagerExtraDetailLevel_"></a> AddLocalUser\(User, SocialManagerExtraDetailLevel\)

Adds a local user to social manager (<code>XblSocialManagerAddLocalUser</code>).

```csharp
public void AddLocalUser(User user, SocialManagerExtraDetailLevel extraDetailLevel = SocialManagerExtraDetailLevel.NoExtraDetail)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The local user to add.

`extraDetailLevel` [SocialManagerExtraDetailLevel](GDK.Net.XboxLive.SocialManagerExtraDetailLevel.md)

Extra profile fields to populate for graph users.

#### Remarks

The user must remain alive while it is added to social manager; call
<xref href="GDK.Net.XboxLive.SocialManager.RemoveLocalUser(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref> before disposing the <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref>. Completion is
reported later as a <xref href="GDK.Net.XboxLive.LocalUserAddedSocialManagerEvent" data-throw-if-not-resolved="false"></xref> from <xref href="GDK.Net.XboxLive.SocialManager.DoWork" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_XboxLive_SocialManager_CreateSocialUserGroupFromFilters_GDK_Net_Users_User_GDK_Net_XboxLive_PresenceFilter_GDK_Net_XboxLive_RelationshipFilter_"></a> CreateSocialUserGroupFromFilters\(User, PresenceFilter, RelationshipFilter\)

Creates a filter-backed social user group
(<code>XblSocialManagerCreateSocialUserGroupFromFilters</code>).

```csharp
public SocialManagerUserGroup CreateSocialUserGroupFromFilters(User user, PresenceFilter presenceFilter, RelationshipFilter relationshipFilter)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

`presenceFilter` [PresenceFilter](GDK.Net.XboxLive.PresenceFilter.md)

`relationshipFilter` [RelationshipFilter](GDK.Net.XboxLive.RelationshipFilter.md)

#### Returns

 [SocialManagerUserGroup](GDK.Net.XboxLive.SocialManagerUserGroup.md)

#### Remarks

The returned group exists immediately but is empty until a
<xref href="GDK.Net.XboxLive.SocialUserGroupLoadedSocialManagerEvent" data-throw-if-not-resolved="false"></xref> is returned from <xref href="GDK.Net.XboxLive.SocialManager.DoWork" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_XboxLive_SocialManager_CreateSocialUserGroupFromList_GDK_Net_Users_User_System_Collections_Generic_IEnumerable_System_UInt64__"></a> CreateSocialUserGroupFromList\(User, IEnumerable<ulong\>\)

Creates a list-backed social user group
(<code>XblSocialManagerCreateSocialUserGroupFromList</code>).

```csharp
public SocialManagerUserGroup CreateSocialUserGroupFromList(User user, IEnumerable<ulong> xboxUserIds)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

#### Returns

 [SocialManagerUserGroup](GDK.Net.XboxLive.SocialManagerUserGroup.md)

#### Remarks

The list cannot exceed 100 Xbox user ids. The returned group exists immediately but is
empty until a <xref href="GDK.Net.XboxLive.SocialUserGroupLoadedSocialManagerEvent" data-throw-if-not-resolved="false"></xref> is returned from
<xref href="GDK.Net.XboxLive.SocialManager.DoWork" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_XboxLive_SocialManager_DestroySocialUserGroup_GDK_Net_XboxLive_SocialManagerUserGroup_"></a> DestroySocialUserGroup\(SocialManagerUserGroup\)

Destroys a social user group (<code>XblSocialManagerDestroySocialUserGroup</code>).

```csharp
public void DestroySocialUserGroup(SocialManagerUserGroup group)
```

#### Parameters

`group` [SocialManagerUserGroup](GDK.Net.XboxLive.SocialManagerUserGroup.md)

The group to destroy.

### <a id="GDK_Net_XboxLive_SocialManager_DoWork"></a> DoWork\(\)

Pumps social manager and returns a managed snapshot of the events in native order.

```csharp
public IReadOnlyList<SocialManagerEvent> DoWork()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SocialManagerEvent](GDK.Net.XboxLive.SocialManagerEvent.md)\>

#### Remarks

Call this regularly, ideally once per frame. Unlike the Start/Finish state-change pattern
used by PFMP and Party, XSAPI social manager has no <code>Finish</code> call; native events are
valid only until the next <code>XblSocialManagerDoWork</code>. This method therefore snapshots the
batch before returning instead of exposing borrowed views.

### <a id="GDK_Net_XboxLive_SocialManager_GetLocalUsers"></a> GetLocalUsers\(\)

Returns the local users currently tracked by social manager.

```csharp
public IReadOnlyList<User> GetLocalUsers()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[User](GDK.Net.Users.User.md)\>

#### Remarks

XSAPI returns borrowed handles that must not be closed. This method resolves those handles
back to the <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> instances passed to <xref href="GDK.Net.XboxLive.SocialManager.AddLocalUser(GDK.Net.Users.User%2cGDK.Net.XboxLive.SocialManagerExtraDetailLevel)" data-throw-if-not-resolved="false"></xref> when
possible; if an unknown handle is returned, it is duplicated before being wrapped.

### <a id="GDK_Net_XboxLive_SocialManager_RemoveLocalUser_GDK_Net_Users_User_"></a> RemoveLocalUser\(User\)

Removes a local user from social manager (<code>XblSocialManagerRemoveLocalUser</code>).

```csharp
public void RemoveLocalUser(User user)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The user to remove. It must not already be disposed.

#### Remarks

Removing a local user also destroys its social user groups. The corresponding managed group
wrappers are invalidated and any further use throws <xref href="System.ObjectDisposedException" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_XboxLive_SocialManager_SetRichPresencePollingStatus_GDK_Net_Users_User_System_Boolean_"></a> SetRichPresencePollingStatus\(User, bool\)

Enables or disables XSAPI rich-presence polling for a local user
(<code>XblSocialManagerSetRichPresencePollingStatus</code>).

```csharp
public void SetRichPresencePollingStatus(User user, bool shouldEnablePolling)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

`shouldEnablePolling` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

