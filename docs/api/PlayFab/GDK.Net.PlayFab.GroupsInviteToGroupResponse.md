# <a id="GDK_Net_PlayFab_GroupsInviteToGroupResponse"></a> Class GroupsInviteToGroupResponse

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFGroupsInviteToGroupResponse</code>.

```csharp
public sealed class GroupsInviteToGroupResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GroupsInviteToGroupResponse](GDK.Net.PlayFab.GroupsInviteToGroupResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_GroupsInviteToGroupResponse_Expires"></a> Expires

<code>Expires</code>.

```csharp
public DateTimeOffset Expires { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_PlayFab_GroupsInviteToGroupResponse_Group"></a> Group

<code>Group</code>.

```csharp
public EntityKey? Group { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_GroupsInviteToGroupResponse_InvitedByEntity"></a> InvitedByEntity

<code>InvitedByEntity</code>.

```csharp
public GroupsEntityWithLineage? InvitedByEntity { get; set; }
```

#### Property Value

 [GroupsEntityWithLineage](GDK.Net.PlayFab.GroupsEntityWithLineage.md)?

### <a id="GDK_Net_PlayFab_GroupsInviteToGroupResponse_InvitedEntity"></a> InvitedEntity

<code>InvitedEntity</code>.

```csharp
public GroupsEntityWithLineage? InvitedEntity { get; set; }
```

#### Property Value

 [GroupsEntityWithLineage](GDK.Net.PlayFab.GroupsEntityWithLineage.md)?

### <a id="GDK_Net_PlayFab_GroupsInviteToGroupResponse_RoleId"></a> RoleId

<code>RoleId</code>.

```csharp
public string? RoleId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

