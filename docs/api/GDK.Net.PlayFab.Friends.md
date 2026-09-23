# <a id="GDK_Net_PlayFab_Friends"></a> Class Friends

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Friends service (<code>PFFriends.h</code>).

```csharp
public static class Friends
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Friends](GDK.Net.PlayFab.Friends.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Friends_ClientAddFriendAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_FriendsClientAddFriendRequest_System_Threading_CancellationToken_"></a> ClientAddFriendAsync\(PlayFabEntity, FriendsClientAddFriendRequest, CancellationToken\)

Calls <code>PFFriendsClientAddFriendAsync</code>.

```csharp
public static Task<FriendsAddFriendResult> ClientAddFriendAsync(PlayFabEntity entity, FriendsClientAddFriendRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [FriendsClientAddFriendRequest](GDK.Net.PlayFab.FriendsClientAddFriendRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[FriendsAddFriendResult](GDK.Net.PlayFab.FriendsAddFriendResult.md)\>

### <a id="GDK_Net_PlayFab_Friends_ClientGetFriendsListAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_FriendsClientGetFriendsListRequest_System_Threading_CancellationToken_"></a> ClientGetFriendsListAsync\(PlayFabEntity, FriendsClientGetFriendsListRequest, CancellationToken\)

Calls <code>PFFriendsClientGetFriendsListAsync</code>.

```csharp
public static Task<FriendsGetFriendsListResult> ClientGetFriendsListAsync(PlayFabEntity entity, FriendsClientGetFriendsListRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [FriendsClientGetFriendsListRequest](GDK.Net.PlayFab.FriendsClientGetFriendsListRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[FriendsGetFriendsListResult](GDK.Net.PlayFab.FriendsGetFriendsListResult.md)\>

### <a id="GDK_Net_PlayFab_Friends_ClientRemoveFriendAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_FriendsClientRemoveFriendRequest_System_Threading_CancellationToken_"></a> ClientRemoveFriendAsync\(PlayFabEntity, FriendsClientRemoveFriendRequest, CancellationToken\)

Calls <code>PFFriendsClientRemoveFriendAsync</code>.

```csharp
public static Task ClientRemoveFriendAsync(PlayFabEntity entity, FriendsClientRemoveFriendRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [FriendsClientRemoveFriendRequest](GDK.Net.PlayFab.FriendsClientRemoveFriendRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Friends_ClientSetFriendTagsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_FriendsClientSetFriendTagsRequest_System_Threading_CancellationToken_"></a> ClientSetFriendTagsAsync\(PlayFabEntity, FriendsClientSetFriendTagsRequest, CancellationToken\)

Calls <code>PFFriendsClientSetFriendTagsAsync</code>.

```csharp
public static Task ClientSetFriendTagsAsync(PlayFabEntity entity, FriendsClientSetFriendTagsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [FriendsClientSetFriendTagsRequest](GDK.Net.PlayFab.FriendsClientSetFriendTagsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Friends_ServerAddFriendAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_FriendsServerAddFriendRequest_System_Threading_CancellationToken_"></a> ServerAddFriendAsync\(PlayFabEntity, FriendsServerAddFriendRequest, CancellationToken\)

Calls <code>PFFriendsServerAddFriendAsync</code>.

```csharp
public static Task ServerAddFriendAsync(PlayFabEntity entity, FriendsServerAddFriendRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [FriendsServerAddFriendRequest](GDK.Net.PlayFab.FriendsServerAddFriendRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Friends_ServerGetFriendsListAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_FriendsServerGetFriendsListRequest_System_Threading_CancellationToken_"></a> ServerGetFriendsListAsync\(PlayFabEntity, FriendsServerGetFriendsListRequest, CancellationToken\)

Calls <code>PFFriendsServerGetFriendsListAsync</code>.

```csharp
public static Task<FriendsGetFriendsListResult> ServerGetFriendsListAsync(PlayFabEntity entity, FriendsServerGetFriendsListRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [FriendsServerGetFriendsListRequest](GDK.Net.PlayFab.FriendsServerGetFriendsListRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[FriendsGetFriendsListResult](GDK.Net.PlayFab.FriendsGetFriendsListResult.md)\>

### <a id="GDK_Net_PlayFab_Friends_ServerRemoveFriendAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_FriendsServerRemoveFriendRequest_System_Threading_CancellationToken_"></a> ServerRemoveFriendAsync\(PlayFabEntity, FriendsServerRemoveFriendRequest, CancellationToken\)

Calls <code>PFFriendsServerRemoveFriendAsync</code>.

```csharp
public static Task ServerRemoveFriendAsync(PlayFabEntity entity, FriendsServerRemoveFriendRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [FriendsServerRemoveFriendRequest](GDK.Net.PlayFab.FriendsServerRemoveFriendRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_Friends_ServerSetFriendTagsAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_FriendsServerSetFriendTagsRequest_System_Threading_CancellationToken_"></a> ServerSetFriendTagsAsync\(PlayFabEntity, FriendsServerSetFriendTagsRequest, CancellationToken\)

Calls <code>PFFriendsServerSetFriendTagsAsync</code>.

```csharp
public static Task ServerSetFriendTagsAsync(PlayFabEntity entity, FriendsServerSetFriendTagsRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [FriendsServerSetFriendTagsRequest](GDK.Net.PlayFab.FriendsServerSetFriendTagsRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

