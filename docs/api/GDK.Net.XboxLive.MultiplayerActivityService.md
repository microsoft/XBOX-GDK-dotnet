# <a id="GDK_Net_XboxLive_MultiplayerActivityService"></a> Class MultiplayerActivityService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live multiplayer activity, invites and recent-player reporting. Mirrors
<code>multiplayer_activity_c.h</code>.

```csharp
public sealed class MultiplayerActivityService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MultiplayerActivityService](GDK.Net.XboxLive.MultiplayerActivityService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
Multiplayer activity is certification-sensitive. The activity advertised through
<xref href="GDK.Net.XboxLive.MultiplayerActivityService.SetActivityAsync(GDK.Net.XboxLive.MultiplayerActivityInfo%2cSystem.Boolean%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> must always describe the real joinable session because it is what
lets friends join in progress. Update it when the join state changes and call
<xref href="GDK.Net.XboxLive.MultiplayerActivityService.DeleteActivityAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> when the session ends; leaving a stale or orphaned activity is
a certification failure.
</p>
<p>
Recent-player updates are privacy-sensitive. <xref href="GDK.Net.XboxLive.MultiplayerActivityService.UpdateRecentPlayers(System.Collections.Generic.IEnumerable%7bGDK.Net.XboxLive.MultiplayerActivityRecentPlayerUpdate%7d)" data-throw-if-not-resolved="false"></xref> only queues local
encounter data; XSAPI uploads the batch later, or immediately when
<xref href="GDK.Net.XboxLive.MultiplayerActivityService.FlushRecentPlayersAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> is called.
</p>
<p>
The invite notification handler functions are present in the header for non-GDK platforms but
are not exported by Microsoft.Xbox.Services.C.Thunks.dll in GDK edition 260404, so this service
exposes query-and-write APIs only.
</p>

## Methods

### <a id="GDK_Net_XboxLive_MultiplayerActivityService_DeleteActivityAsync_System_Threading_CancellationToken_"></a> DeleteActivityAsync\(CancellationToken\)

Clears the local user's multiplayer activity
(<code>XblMultiplayerActivityDeleteActivityAsync</code>).

```csharp
public Task DeleteActivityAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Calling this when the multiplayer session ends is not optional. A stale or orphaned activity
advertises a join path that no longer exists and is a certification failure.

### <a id="GDK_Net_XboxLive_MultiplayerActivityService_FlushRecentPlayersAsync_System_Threading_CancellationToken_"></a> FlushRecentPlayersAsync\(CancellationToken\)

Uploads pending recent-player updates immediately
(<code>XblMultiplayerActivityFlushRecentPlayersAsync</code>).

```csharp
public Task FlushRecentPlayersAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Use this after <xref href="GDK.Net.XboxLive.MultiplayerActivityService.UpdateRecentPlayers(System.Collections.Generic.IEnumerable%7bGDK.Net.XboxLive.MultiplayerActivityRecentPlayerUpdate%7d)" data-throw-if-not-resolved="false"></xref> when the title needs privacy-sensitive
encounter data to be visible before XSAPI's periodic background upload.

### <a id="GDK_Net_XboxLive_MultiplayerActivityService_GetActivitiesAsync_System_Collections_Generic_IEnumerable_System_UInt64__System_Threading_CancellationToken_"></a> GetActivitiesAsync\(IEnumerable<ulong\>, CancellationToken\)

Gets multiplayer activity for up to 30 users
(<code>XblMultiplayerActivityGetActivityAsync</code>).

```csharp
public Task<IReadOnlyList<MultiplayerActivityInfo>> GetActivitiesAsync(IEnumerable<ulong> xboxUserIds, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Xbox user ids to query. An empty sequence returns an empty result.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[MultiplayerActivityInfo](GDK.Net.XboxLive.MultiplayerActivityInfo.md)\>\>

Managed snapshots of the returned activities; no returned object points into native memory.

### <a id="GDK_Net_XboxLive_MultiplayerActivityService_SendInvitesAsync_System_Collections_Generic_IEnumerable_System_UInt64__System_Boolean_System_String_System_Threading_CancellationToken_"></a> SendInvitesAsync\(IEnumerable<ulong\>, bool, string?, CancellationToken\)

Sends invites for the caller's current activity
(<code>XblMultiplayerActivitySendInvitesAsync</code>).

```csharp
public Task SendInvitesAsync(IEnumerable<ulong> xboxUserIds, bool allowCrossPlatformJoin = false, string? connectionString = null, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Xbox user ids to invite. An empty sequence returns a completed task.

`allowCrossPlatformJoin` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to send cross-platform invites when the title is configured for them.

`connectionString` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional connection string to pass to invitees.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_XboxLive_MultiplayerActivityService_SetActivityAsync_GDK_Net_XboxLive_MultiplayerActivityInfo_System_Boolean_System_Threading_CancellationToken_"></a> SetActivityAsync\(MultiplayerActivityInfo, bool, CancellationToken\)

Sets or updates the local user's multiplayer activity
(<code>XblMultiplayerActivitySetActivityAsync</code>).

```csharp
public Task SetActivityAsync(MultiplayerActivityInfo activityInfo, bool allowCrossPlatformJoin = false, CancellationToken cancellationToken = default)
```

#### Parameters

`activityInfo` [MultiplayerActivityInfo](GDK.Net.XboxLive.MultiplayerActivityInfo.md)

Accurate activity information for the local user's current joinable session.

`allowCrossPlatformJoin` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the activity should be joinable on other title-supported platforms.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

This is a certification-sensitive advertisement. Keep it current while the player is in a
multiplayer session and always call <xref href="GDK.Net.XboxLive.MultiplayerActivityService.DeleteActivityAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> when that session ends;
<xref href="GDK.Net.XboxLive.MultiplayerActivityInfo.ConnectionString" data-throw-if-not-resolved="false"></xref> and
<xref href="GDK.Net.XboxLive.MultiplayerActivityInfo.GroupId" data-throw-if-not-resolved="false"></xref> must not be empty when setting an activity.

### <a id="GDK_Net_XboxLive_MultiplayerActivityService_UpdateRecentPlayers_System_Collections_Generic_IEnumerable_GDK_Net_XboxLive_MultiplayerActivityRecentPlayerUpdate__"></a> UpdateRecentPlayers\(IEnumerable<MultiplayerActivityRecentPlayerUpdate\>\)

Queues recent-player encounters locally
(<code>XblMultiplayerActivityUpdateRecentPlayers</code>).

```csharp
public void UpdateRecentPlayers(IEnumerable<MultiplayerActivityRecentPlayerUpdate> updates)
```

#### Parameters

`updates` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[MultiplayerActivityRecentPlayerUpdate](GDK.Net.XboxLive.MultiplayerActivityRecentPlayerUpdate.md)\>

Recent-player encounters to append or update. An empty sequence is a no-op.

#### Remarks

This synchronous call does not upload immediately; it only adds privacy-sensitive encounter
data to XSAPI's local batch. XSAPI periodically flushes the batch on its background queue, or
you can call <xref href="GDK.Net.XboxLive.MultiplayerActivityService.FlushRecentPlayersAsync(System.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> to send pending updates now.

