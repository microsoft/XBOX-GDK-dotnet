# <a id="GDK_Net_XboxLive_AchievementsManager"></a> Class AchievementsManager

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Process-global Xbox Live achievements manager. Mirrors <code>achievements_manager_c.h</code>.

```csharp
public sealed class AchievementsManager
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementsManager](GDK.Net.XboxLive.AchievementsManager.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
This is the cached, pumped achievements layer. Unlike <xref href="GDK.Net.XboxLive.AchievementsService" data-throw-if-not-resolved="false"></xref>, which
performs explicit asynchronous service queries through an <xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref>, this
manager warms a local cache when <xref href="GDK.Net.XboxLive.AchievementsManager.AddLocalUser(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref> is called and then answers
<xref href="GDK.Net.XboxLive.AchievementsManager.GetAchievement(System.UInt64%2cSystem.String)" data-throw-if-not-resolved="false"></xref>, <xref href="GDK.Net.XboxLive.AchievementsManager.GetAchievements(System.UInt64%2cGDK.Net.XboxLive.AchievementOrderBy%2cGDK.Net.XboxLive.AchievementsManagerSortOrder)" data-throw-if-not-resolved="false"></xref> and
<xref href="GDK.Net.XboxLive.AchievementsManager.GetAchievementsByState(System.UInt64%2cGDK.Net.XboxLive.AchievementProgressState%2cGDK.Net.XboxLive.AchievementOrderBy%2cGDK.Net.XboxLive.AchievementsManagerSortOrder)" data-throw-if-not-resolved="false"></xref> synchronously from that cache.
</p>
<p>
The cache and unlock notifications advance only when the title calls <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref>,
ideally once per frame. After adding a local user, keep pumping until
<xref href="GDK.Net.XboxLive.AchievementsManagerLocalUserInitialStateSyncedEvent" data-throw-if-not-resolved="false"></xref> appears and
<xref href="GDK.Net.XboxLive.AchievementsManager.IsUserInitialized(System.UInt64)" data-throw-if-not-resolved="false"></xref> returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>; cached queries before then fail.
</p>
<p>
This manager is process-global, not per user and not per <xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref>. Drive
<xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> and the other achievements-manager calls from one thread; XSAPI does not
make <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> thread-safe against concurrent achievements-manager calls.
</p>
<p>
XSAPI differs from the PFMP and Party state-change pattern: <code>XblAchievementsManagerDoWork</code>
has no matching <code>Finish</code>. Its returned array is valid only until the next
<xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> call, so a borrowed C# iterator would dangle silently. This projection
deliberately snapshots the batch into managed <xref href="GDK.Net.XboxLive.AchievementsManagerEvent" data-throw-if-not-resolved="false"></xref> records
before returning it, while keeping the native event order intact.
</p>

## Methods

### <a id="GDK_Net_XboxLive_AchievementsManager_AddLocalUser_GDK_Net_Users_User_"></a> AddLocalUser\(User\)

Adds a local user and starts warming that user's cached achievements
(<code>XblAchievementsManagerAddLocalUser</code>).

```csharp
public void AddLocalUser(User user)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The local user whose achievements should be cached.

#### Remarks

This call starts the cache warm-up but does not complete it synchronously. Call
<xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> regularly and wait for
<xref href="GDK.Net.XboxLive.AchievementsManagerLocalUserInitialStateSyncedEvent" data-throw-if-not-resolved="false"></xref> before using cached query
methods for this user.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">user</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live Services have not been initialized.

### <a id="GDK_Net_XboxLive_AchievementsManager_DoWork"></a> DoWork\(\)

Pumps the achievements manager and returns any events produced since the previous pump
(<code>XblAchievementsManagerDoWork</code>).

```csharp
public IReadOnlyList<AchievementsManagerEvent> DoWork()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[AchievementsManagerEvent](GDK.Net.XboxLive.AchievementsManagerEvent.md)\>

A managed snapshot of the native event batch. Completions and notifications stay interleaved
in native order.

#### Remarks

Call this regularly, ideally once per frame. Nothing happens in the achievements manager —
cache warming, progress updates or unlock notifications — unless this pump runs. Unlike
PFMP and Party, XSAPI has no <code>Finish</code> call for this batch; the native array is valid
only until the next pump, so this method copies every event before returning.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live Services have not been initialized.

### <a id="GDK_Net_XboxLive_AchievementsManager_GetAchievement_System_UInt64_System_String_"></a> GetAchievement\(ulong, string\)

Gets the cached state of one achievement for a local user
(<code>XblAchievementsManagerGetAchievement</code>).

```csharp
public AchievementsManagerResult GetAchievement(ulong xboxUserId, string achievementId)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id whose cache should be queried.

`achievementId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The achievement id from Partner Center.

#### Returns

 [AchievementsManagerResult](GDK.Net.XboxLive.AchievementsManagerResult.md)

A disposable result whose <xref href="GDK.Net.XboxLive.AchievementsManagerResult.Achievements" data-throw-if-not-resolved="false"></xref> list is snapshotted.

#### Remarks

This is a synchronous cache lookup, not a service call. If the user is not initialized yet,
keep pumping <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> and wait for
<xref href="GDK.Net.XboxLive.AchievementsManagerLocalUserInitialStateSyncedEvent" data-throw-if-not-resolved="false"></xref>, or use
<xref href="GDK.Net.XboxLive.AchievementsService.GetAsync(System.UInt64%2cSystem.String%2cSystem.String%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> when an explicit asynchronous service query is
what you need.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">achievementId</code> is empty.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live Services have not been initialized.

### <a id="GDK_Net_XboxLive_AchievementsManager_GetAchievements_System_UInt64_GDK_Net_XboxLive_AchievementOrderBy_GDK_Net_XboxLive_AchievementsManagerSortOrder_"></a> GetAchievements\(ulong, AchievementOrderBy, AchievementsManagerSortOrder\)

Gets all cached achievements for a local user (<code>XblAchievementsManagerGetAchievements</code>).

```csharp
public AchievementsManagerResult GetAchievements(ulong xboxUserId, AchievementOrderBy sortField = AchievementOrderBy.Default, AchievementsManagerSortOrder sortOrder = AchievementsManagerSortOrder.Unsorted)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id whose cache should be queried.

`sortField` [AchievementOrderBy](GDK.Net.XboxLive.AchievementOrderBy.md)

The achievement field to sort by.

`sortOrder` [AchievementsManagerSortOrder](GDK.Net.XboxLive.AchievementsManagerSortOrder.md)

The sort direction.

#### Returns

 [AchievementsManagerResult](GDK.Net.XboxLive.AchievementsManagerResult.md)

A disposable result whose <xref href="GDK.Net.XboxLive.AchievementsManagerResult.Achievements" data-throw-if-not-resolved="false"></xref> list is snapshotted.

#### Remarks

This reads the achievements manager's local cache. Use <xref href="GDK.Net.XboxLive.AchievementsService" data-throw-if-not-resolved="false"></xref> for
explicit asynchronous service queries or when the local cache has not been warmed.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live Services have not been initialized.

### <a id="GDK_Net_XboxLive_AchievementsManager_GetAchievementsByState_System_UInt64_GDK_Net_XboxLive_AchievementProgressState_GDK_Net_XboxLive_AchievementOrderBy_GDK_Net_XboxLive_AchievementsManagerSortOrder_"></a> GetAchievementsByState\(ulong, AchievementProgressState, AchievementOrderBy, AchievementsManagerSortOrder\)

Gets cached achievements in a specific progress state
(<code>XblAchievementsManagerGetAchievementsByState</code>).

```csharp
public AchievementsManagerResult GetAchievementsByState(ulong xboxUserId, AchievementProgressState achievementState, AchievementOrderBy sortField = AchievementOrderBy.Default, AchievementsManagerSortOrder sortOrder = AchievementsManagerSortOrder.Unsorted)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id whose cache should be queried.

`achievementState` [AchievementProgressState](GDK.Net.XboxLive.AchievementProgressState.md)

The progress state to include.

`sortField` [AchievementOrderBy](GDK.Net.XboxLive.AchievementOrderBy.md)

The achievement field to sort by.

`sortOrder` [AchievementsManagerSortOrder](GDK.Net.XboxLive.AchievementsManagerSortOrder.md)

The sort direction.

#### Returns

 [AchievementsManagerResult](GDK.Net.XboxLive.AchievementsManagerResult.md)

A disposable result whose <xref href="GDK.Net.XboxLive.AchievementsManagerResult.Achievements" data-throw-if-not-resolved="false"></xref> list is snapshotted.

#### Remarks

This reads the achievements manager's local cache and fails until the user is initialized.
Pump <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> and wait for
<xref href="GDK.Net.XboxLive.AchievementsManagerLocalUserInitialStateSyncedEvent" data-throw-if-not-resolved="false"></xref> first.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live Services have not been initialized.

### <a id="GDK_Net_XboxLive_AchievementsManager_IsUserInitialized_System_UInt64_"></a> IsUserInitialized\(ulong\)

Returns whether a local user's initial cached state has finished syncing
(<code>XblAchievementsManagerIsUserInitialized</code>).

```csharp
public bool IsUserInitialized(ulong xboxUserId)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id to check.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when cached queries can be used for this user; otherwise keep calling
<xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> until the local-user-initial-state event arrives.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live Services have not been initialized.

### <a id="GDK_Net_XboxLive_AchievementsManager_RemoveLocalUser_GDK_Net_Users_User_"></a> RemoveLocalUser\(User\)

Removes a local user and immediately discards that user's cached achievements
(<code>XblAchievementsManagerRemoveLocalUser</code>).

```csharp
public void RemoveLocalUser(User user)
```

#### Parameters

`user` [User](GDK.Net.Users.User.md)

The local user to remove from the achievements manager.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">user</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live Services have not been initialized.

### <a id="GDK_Net_XboxLive_AchievementsManager_UpdateAchievement_System_UInt64_System_String_System_UInt32_"></a> UpdateAchievement\(ulong, string, uint\)

Updates cached achievement progress and unlocks at 100 percent
(<code>XblAchievementsManagerUpdateAchievement</code>).

```csharp
public void UpdateAchievement(ulong xboxUserId, string achievementId, uint currentProgress)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The Xbox user id to update.

`achievementId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The achievement id from Partner Center.

`currentProgress` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Progress from 1 through 100. 100 unlocks the achievement.

#### Remarks

The update is not reflected locally immediately. Keep pumping <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref>; a later
batch may contain <xref href="GDK.Net.XboxLive.AchievementsManagerAchievementProgressUpdatedEvent" data-throw-if-not-resolved="false"></xref> and, when
the update unlocks the achievement, <xref href="GDK.Net.XboxLive.AchievementsManagerAchievementUnlockedEvent" data-throw-if-not-resolved="false"></xref>.
This API can work offline after the user has previously been added while online. If that
prerequisite is not met, use <xref href="GDK.Net.XboxLive.AchievementsService.UpdateAsync(System.UInt64%2cSystem.String%2cSystem.UInt32%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> as a fallback.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">achievementId</code> is empty.

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">currentProgress</code> is outside 1 through 100.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Xbox Live Services have not been initialized.

