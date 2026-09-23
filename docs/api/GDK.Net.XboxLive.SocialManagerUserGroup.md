# <a id="GDK_Net_XboxLive_SocialManagerUserGroup"></a> Class SocialManagerUserGroup

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

A long-lived social-manager user group backed by an XSAPI native handle.

```csharp
public sealed class SocialManagerUserGroup : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManagerUserGroup](GDK.Net.XboxLive.SocialManagerUserGroup.md)

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

Instances are resolved through a handle-to-wrapper identity map, so a group carried by a
<xref href="GDK.Net.XboxLive.SocialUserGroupLoadedSocialManagerEvent" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.XboxLive.SocialUserGroupUpdatedSocialManagerEvent" data-throw-if-not-resolved="false"></xref> is reference-equal to the group returned
by the create call. Disposing a group destroys the native handle, removes it from that map and
makes further use throw <xref href="System.ObjectDisposedException" data-throw-if-not-resolved="false"></xref>.

## Properties

### <a id="GDK_Net_XboxLive_SocialManagerUserGroup_Filters"></a> Filters

The filters for a filter-backed group, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for a list-backed group.

```csharp
public SocialManagerUserGroupFilters? Filters { get; }
```

#### Property Value

 [SocialManagerUserGroupFilters](GDK.Net.XboxLive.SocialManagerUserGroupFilters.md)?

### <a id="GDK_Net_XboxLive_SocialManagerUserGroup_LocalUser"></a> LocalUser

The local user associated with this group.

```csharp
public User LocalUser { get; }
```

#### Property Value

 [User](GDK.Net.Users.User.md)

### <a id="GDK_Net_XboxLive_SocialManagerUserGroup_Type"></a> Type

How the group was created.

```csharp
public SocialUserGroupType Type { get; }
```

#### Property Value

 [SocialUserGroupType](GDK.Net.XboxLive.SocialUserGroupType.md)

## Methods

### <a id="GDK_Net_XboxLive_SocialManagerUserGroup_Dispose"></a> Dispose\(\)

Destroys the native social user group.

```csharp
public void Dispose()
```

### <a id="GDK_Net_XboxLive_SocialManagerUserGroup_GetUsers"></a> GetUsers\(\)

Snapshots users currently in the group.

```csharp
public IReadOnlyList<SocialManagerUser> GetUsers()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SocialManagerUser](GDK.Net.XboxLive.SocialManagerUser.md)\>

### <a id="GDK_Net_XboxLive_SocialManagerUserGroup_GetUsersTrackedByGroup"></a> GetUsersTrackedByGroup\(\)

Snapshots Xbox user ids currently tracked by the group.

```csharp
public IReadOnlyList<ulong> GetUsersTrackedByGroup()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

### <a id="GDK_Net_XboxLive_SocialManagerUserGroup_UpdateUsers_System_Collections_Generic_IEnumerable_System_UInt64__"></a> UpdateUsers\(IEnumerable<ulong\>\)

Replaces the tracked Xbox user ids for a list-backed group.

```csharp
public void UpdateUsers(IEnumerable<ulong> xboxUserIds)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

The replacement set. It cannot exceed 100 users.

