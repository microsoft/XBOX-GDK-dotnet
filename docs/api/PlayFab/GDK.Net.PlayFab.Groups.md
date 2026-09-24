# <a id="GDK_Net_PlayFab_Groups"></a> Class Groups

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Groups service (<code>PFGroups.h</code>).

```csharp
public static class Groups
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Groups](GDK.Net.PlayFab.Groups.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Groups_AcceptGroupApplicationAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsAcceptGroupApplicationRequest_System_Threading_CancellationToken_"></a> AcceptGroupApplicationAsync\(PlayFabEntity, GroupsAcceptGroupApplicationRequest, CancellationToken\)

Calls <code>PFGroupsAcceptGroupApplicationAsync</code>.

```csharp
public static Task AcceptGroupApplicationAsync(PlayFabEntity entity, GroupsAcceptGroupApplicationRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsAcceptGroupApplicationRequest](GDK.Net.PlayFab.GroupsAcceptGroupApplicationRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_AcceptGroupInvitationAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsAcceptGroupInvitationRequest_System_Threading_CancellationToken_"></a> AcceptGroupInvitationAsync\(PlayFabEntity, GroupsAcceptGroupInvitationRequest, CancellationToken\)

Calls <code>PFGroupsAcceptGroupInvitationAsync</code>.

```csharp
public static Task AcceptGroupInvitationAsync(PlayFabEntity entity, GroupsAcceptGroupInvitationRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsAcceptGroupInvitationRequest](GDK.Net.PlayFab.GroupsAcceptGroupInvitationRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_AddMembersAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsAddMembersRequest_System_Threading_CancellationToken_"></a> AddMembersAsync\(PlayFabEntity, GroupsAddMembersRequest, CancellationToken\)

Calls <code>PFGroupsAddMembersAsync</code>.

```csharp
public static Task AddMembersAsync(PlayFabEntity entity, GroupsAddMembersRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsAddMembersRequest](GDK.Net.PlayFab.GroupsAddMembersRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_ApplyToGroupAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsApplyToGroupRequest_System_Threading_CancellationToken_"></a> ApplyToGroupAsync\(PlayFabEntity, GroupsApplyToGroupRequest, CancellationToken\)

Calls <code>PFGroupsApplyToGroupAsync</code>.

```csharp
public static Task<GroupsApplyToGroupResponse> ApplyToGroupAsync(PlayFabEntity entity, GroupsApplyToGroupRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsApplyToGroupRequest](GDK.Net.PlayFab.GroupsApplyToGroupRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsApplyToGroupResponse](GDK.Net.PlayFab.GroupsApplyToGroupResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_BlockEntityAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsBlockEntityRequest_System_Threading_CancellationToken_"></a> BlockEntityAsync\(PlayFabEntity, GroupsBlockEntityRequest, CancellationToken\)

Calls <code>PFGroupsBlockEntityAsync</code>.

```csharp
public static Task BlockEntityAsync(PlayFabEntity entity, GroupsBlockEntityRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsBlockEntityRequest](GDK.Net.PlayFab.GroupsBlockEntityRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_ChangeMemberRoleAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsChangeMemberRoleRequest_System_Threading_CancellationToken_"></a> ChangeMemberRoleAsync\(PlayFabEntity, GroupsChangeMemberRoleRequest, CancellationToken\)

Calls <code>PFGroupsChangeMemberRoleAsync</code>.

```csharp
public static Task ChangeMemberRoleAsync(PlayFabEntity entity, GroupsChangeMemberRoleRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsChangeMemberRoleRequest](GDK.Net.PlayFab.GroupsChangeMemberRoleRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_CreateGroupAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsCreateGroupRequest_System_Threading_CancellationToken_"></a> CreateGroupAsync\(PlayFabEntity, GroupsCreateGroupRequest, CancellationToken\)

Calls <code>PFGroupsCreateGroupAsync</code>.

```csharp
public static Task<GroupsCreateGroupResponse> CreateGroupAsync(PlayFabEntity entity, GroupsCreateGroupRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsCreateGroupRequest](GDK.Net.PlayFab.GroupsCreateGroupRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsCreateGroupResponse](GDK.Net.PlayFab.GroupsCreateGroupResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_CreateRoleAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsCreateGroupRoleRequest_System_Threading_CancellationToken_"></a> CreateRoleAsync\(PlayFabEntity, GroupsCreateGroupRoleRequest, CancellationToken\)

Calls <code>PFGroupsCreateRoleAsync</code>.

```csharp
public static Task<GroupsCreateGroupRoleResponse> CreateRoleAsync(PlayFabEntity entity, GroupsCreateGroupRoleRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsCreateGroupRoleRequest](GDK.Net.PlayFab.GroupsCreateGroupRoleRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsCreateGroupRoleResponse](GDK.Net.PlayFab.GroupsCreateGroupRoleResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_DeleteGroupAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsDeleteGroupRequest_System_Threading_CancellationToken_"></a> DeleteGroupAsync\(PlayFabEntity, GroupsDeleteGroupRequest, CancellationToken\)

Calls <code>PFGroupsDeleteGroupAsync</code>.

```csharp
public static Task DeleteGroupAsync(PlayFabEntity entity, GroupsDeleteGroupRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsDeleteGroupRequest](GDK.Net.PlayFab.GroupsDeleteGroupRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_DeleteRoleAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsDeleteRoleRequest_System_Threading_CancellationToken_"></a> DeleteRoleAsync\(PlayFabEntity, GroupsDeleteRoleRequest, CancellationToken\)

Calls <code>PFGroupsDeleteRoleAsync</code>.

```csharp
public static Task DeleteRoleAsync(PlayFabEntity entity, GroupsDeleteRoleRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsDeleteRoleRequest](GDK.Net.PlayFab.GroupsDeleteRoleRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_GetGroupAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsGetGroupRequest_System_Threading_CancellationToken_"></a> GetGroupAsync\(PlayFabEntity, GroupsGetGroupRequest, CancellationToken\)

Calls <code>PFGroupsGetGroupAsync</code>.

```csharp
public static Task<GroupsGetGroupResponse> GetGroupAsync(PlayFabEntity entity, GroupsGetGroupRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsGetGroupRequest](GDK.Net.PlayFab.GroupsGetGroupRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsGetGroupResponse](GDK.Net.PlayFab.GroupsGetGroupResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_InviteToGroupAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsInviteToGroupRequest_System_Threading_CancellationToken_"></a> InviteToGroupAsync\(PlayFabEntity, GroupsInviteToGroupRequest, CancellationToken\)

Calls <code>PFGroupsInviteToGroupAsync</code>.

```csharp
public static Task<GroupsInviteToGroupResponse> InviteToGroupAsync(PlayFabEntity entity, GroupsInviteToGroupRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsInviteToGroupRequest](GDK.Net.PlayFab.GroupsInviteToGroupRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsInviteToGroupResponse](GDK.Net.PlayFab.GroupsInviteToGroupResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_IsMemberAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsIsMemberRequest_System_Threading_CancellationToken_"></a> IsMemberAsync\(PlayFabEntity, GroupsIsMemberRequest, CancellationToken\)

Calls <code>PFGroupsIsMemberAsync</code>.

```csharp
public static Task<GroupsIsMemberResponse> IsMemberAsync(PlayFabEntity entity, GroupsIsMemberRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsIsMemberRequest](GDK.Net.PlayFab.GroupsIsMemberRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsIsMemberResponse](GDK.Net.PlayFab.GroupsIsMemberResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_ListGroupApplicationsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsListGroupApplicationsRequest_System_Threading_CancellationToken_"></a> ListGroupApplicationsAsync\(PlayFabEntity, GroupsListGroupApplicationsRequest, CancellationToken\)

Calls <code>PFGroupsListGroupApplicationsAsync</code>.

```csharp
public static Task<GroupsListGroupApplicationsResponse> ListGroupApplicationsAsync(PlayFabEntity entity, GroupsListGroupApplicationsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsListGroupApplicationsRequest](GDK.Net.PlayFab.GroupsListGroupApplicationsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsListGroupApplicationsResponse](GDK.Net.PlayFab.GroupsListGroupApplicationsResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_ListGroupBlocksAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsListGroupBlocksRequest_System_Threading_CancellationToken_"></a> ListGroupBlocksAsync\(PlayFabEntity, GroupsListGroupBlocksRequest, CancellationToken\)

Calls <code>PFGroupsListGroupBlocksAsync</code>.

```csharp
public static Task<GroupsListGroupBlocksResponse> ListGroupBlocksAsync(PlayFabEntity entity, GroupsListGroupBlocksRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsListGroupBlocksRequest](GDK.Net.PlayFab.GroupsListGroupBlocksRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsListGroupBlocksResponse](GDK.Net.PlayFab.GroupsListGroupBlocksResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_ListGroupInvitationsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsListGroupInvitationsRequest_System_Threading_CancellationToken_"></a> ListGroupInvitationsAsync\(PlayFabEntity, GroupsListGroupInvitationsRequest, CancellationToken\)

Calls <code>PFGroupsListGroupInvitationsAsync</code>.

```csharp
public static Task<GroupsListGroupInvitationsResponse> ListGroupInvitationsAsync(PlayFabEntity entity, GroupsListGroupInvitationsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsListGroupInvitationsRequest](GDK.Net.PlayFab.GroupsListGroupInvitationsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsListGroupInvitationsResponse](GDK.Net.PlayFab.GroupsListGroupInvitationsResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_ListGroupMembersAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsListGroupMembersRequest_System_Threading_CancellationToken_"></a> ListGroupMembersAsync\(PlayFabEntity, GroupsListGroupMembersRequest, CancellationToken\)

Calls <code>PFGroupsListGroupMembersAsync</code>.

```csharp
public static Task<GroupsListGroupMembersResponse> ListGroupMembersAsync(PlayFabEntity entity, GroupsListGroupMembersRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsListGroupMembersRequest](GDK.Net.PlayFab.GroupsListGroupMembersRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsListGroupMembersResponse](GDK.Net.PlayFab.GroupsListGroupMembersResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_ListMembershipAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsListMembershipRequest_System_Threading_CancellationToken_"></a> ListMembershipAsync\(PlayFabEntity, GroupsListMembershipRequest, CancellationToken\)

Calls <code>PFGroupsListMembershipAsync</code>.

```csharp
public static Task<GroupsListMembershipResponse> ListMembershipAsync(PlayFabEntity entity, GroupsListMembershipRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsListMembershipRequest](GDK.Net.PlayFab.GroupsListMembershipRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsListMembershipResponse](GDK.Net.PlayFab.GroupsListMembershipResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_ListMembershipOpportunitiesAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsListMembershipOpportunitiesRequest_System_Threading_CancellationToken_"></a> ListMembershipOpportunitiesAsync\(PlayFabEntity, GroupsListMembershipOpportunitiesRequest, CancellationToken\)

Calls <code>PFGroupsListMembershipOpportunitiesAsync</code>.

```csharp
public static Task<GroupsListMembershipOpportunitiesResponse> ListMembershipOpportunitiesAsync(PlayFabEntity entity, GroupsListMembershipOpportunitiesRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsListMembershipOpportunitiesRequest](GDK.Net.PlayFab.GroupsListMembershipOpportunitiesRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsListMembershipOpportunitiesResponse](GDK.Net.PlayFab.GroupsListMembershipOpportunitiesResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_RemoveGroupApplicationAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsRemoveGroupApplicationRequest_System_Threading_CancellationToken_"></a> RemoveGroupApplicationAsync\(PlayFabEntity, GroupsRemoveGroupApplicationRequest, CancellationToken\)

Calls <code>PFGroupsRemoveGroupApplicationAsync</code>.

```csharp
public static Task RemoveGroupApplicationAsync(PlayFabEntity entity, GroupsRemoveGroupApplicationRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsRemoveGroupApplicationRequest](GDK.Net.PlayFab.GroupsRemoveGroupApplicationRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_RemoveGroupInvitationAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsRemoveGroupInvitationRequest_System_Threading_CancellationToken_"></a> RemoveGroupInvitationAsync\(PlayFabEntity, GroupsRemoveGroupInvitationRequest, CancellationToken\)

Calls <code>PFGroupsRemoveGroupInvitationAsync</code>.

```csharp
public static Task RemoveGroupInvitationAsync(PlayFabEntity entity, GroupsRemoveGroupInvitationRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsRemoveGroupInvitationRequest](GDK.Net.PlayFab.GroupsRemoveGroupInvitationRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_RemoveMembersAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsRemoveMembersRequest_System_Threading_CancellationToken_"></a> RemoveMembersAsync\(PlayFabEntity, GroupsRemoveMembersRequest, CancellationToken\)

Calls <code>PFGroupsRemoveMembersAsync</code>.

```csharp
public static Task RemoveMembersAsync(PlayFabEntity entity, GroupsRemoveMembersRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsRemoveMembersRequest](GDK.Net.PlayFab.GroupsRemoveMembersRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_UnblockEntityAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsUnblockEntityRequest_System_Threading_CancellationToken_"></a> UnblockEntityAsync\(PlayFabEntity, GroupsUnblockEntityRequest, CancellationToken\)

Calls <code>PFGroupsUnblockEntityAsync</code>.

```csharp
public static Task UnblockEntityAsync(PlayFabEntity entity, GroupsUnblockEntityRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsUnblockEntityRequest](GDK.Net.PlayFab.GroupsUnblockEntityRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Groups_UpdateGroupAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsUpdateGroupRequest_System_Threading_CancellationToken_"></a> UpdateGroupAsync\(PlayFabEntity, GroupsUpdateGroupRequest, CancellationToken\)

Calls <code>PFGroupsUpdateGroupAsync</code>.

```csharp
public static Task<GroupsUpdateGroupResponse> UpdateGroupAsync(PlayFabEntity entity, GroupsUpdateGroupRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsUpdateGroupRequest](GDK.Net.PlayFab.GroupsUpdateGroupRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsUpdateGroupResponse](GDK.Net.PlayFab.GroupsUpdateGroupResponse.md)\>

### <a id="GDK_Net_PlayFab_Groups_UpdateRoleAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_GroupsUpdateGroupRoleRequest_System_Threading_CancellationToken_"></a> UpdateRoleAsync\(PlayFabEntity, GroupsUpdateGroupRoleRequest, CancellationToken\)

Calls <code>PFGroupsUpdateRoleAsync</code>.

```csharp
public static Task<GroupsUpdateGroupRoleResponse> UpdateRoleAsync(PlayFabEntity entity, GroupsUpdateGroupRoleRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [GroupsUpdateGroupRoleRequest](GDK.Net.PlayFab.GroupsUpdateGroupRoleRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[GroupsUpdateGroupRoleResponse](GDK.Net.PlayFab.GroupsUpdateGroupRoleResponse.md)\>

