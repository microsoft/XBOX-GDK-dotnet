# <a id="GDK_Net_XboxLive_AchievementsService"></a> Class AchievementsService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Achievement queries and progress updates. Reached through
<xref href="GDK.Net.XboxLive.XboxLiveContext.Achievements" data-throw-if-not-resolved="false"></xref>. Mirrors <code>achievements_c.h</code>.

```csharp
public sealed class AchievementsService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementsService](GDK.Net.XboxLive.AchievementsService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
A title normally only writes progress for the signed-in user, and the service enforces that. The
query APIs take an explicit Xbox user id because reading another player's achievements is a
legitimate scenario for a leaderboard or profile card.
</p>
<p>
<code>XblAchievementUnlockAddNotificationHandler</code> is declared only for non-GDK platforms and is
not exported by the thunks DLL, so unlock notifications are unavailable here.
<xref href="GDK.Net.XboxLive.AchievementsService.ProgressChanged" data-throw-if-not-resolved="false"></xref>, which is RTA-backed, is exported and works.
</p>

## Methods

### <a id="GDK_Net_XboxLive_AchievementsService_GetAsync_System_UInt64_System_String_System_String_System_Threading_CancellationToken_"></a> GetAsync\(ulong, string, string, CancellationToken\)

Gets a single achievement by id (<code>XblAchievementsGetAchievementAsync</code>).

```csharp
public Task<AchievementsPage> GetAsync(ulong xboxUserId, string serviceConfigurationId, string achievementId, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The user whose progress to read.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The SCID the achievement is defined in.

`achievementId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The achievement's id.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AchievementsPage](GDK.Net.XboxLive.AchievementsPage.md)\>

A single-entry page. The caller owns it and should dispose it, though the
<xref href="GDK.Net.XboxLive.Achievement" data-throw-if-not-resolved="false"></xref> it yields outlives disposal.

### <a id="GDK_Net_XboxLive_AchievementsService_GetForTitleAsync_System_UInt64_System_UInt32_GDK_Net_XboxLive_AchievementType_System_Boolean_GDK_Net_XboxLive_AchievementOrderBy_System_UInt32_System_UInt32_System_Threading_CancellationToken_"></a> GetForTitleAsync\(ulong, uint, AchievementType, bool, AchievementOrderBy, uint, uint, CancellationToken\)

Gets a page of achievements for a title
(<code>XblAchievementsGetAchievementsForTitleIdAsync</code>).

```csharp
public Task<AchievementsPage> GetForTitleAsync(ulong xboxUserId, uint titleId, AchievementType type = AchievementType.All, bool unlockedOnly = false, AchievementOrderBy orderBy = AchievementOrderBy.Default, uint skipItems = 0, uint maxItems = 0, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The user whose progress to read.

`titleId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The title to read achievements for.

`type` [AchievementType](GDK.Net.XboxLive.AchievementType.md)

Which kinds of achievement to include.

`unlockedOnly` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, only achievements the user has earned.

`orderBy` [AchievementOrderBy](GDK.Net.XboxLive.AchievementOrderBy.md)

Sort order.

`skipItems` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

How many achievements to skip: the paging offset.

`maxItems` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Maximum achievements per page; 0 lets the service choose.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AchievementsPage](GDK.Net.XboxLive.AchievementsPage.md)\>

### <a id="GDK_Net_XboxLive_AchievementsService_UpdateAsync_System_UInt64_System_String_System_UInt32_System_Threading_CancellationToken_"></a> UpdateAsync\(ulong, string, uint, CancellationToken\)

Reports progress towards an achievement (<code>XblAchievementsUpdateAchievementAsync</code>).

```csharp
public Task UpdateAsync(ulong xboxUserId, string achievementId, uint percentComplete, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The user to record progress for.

`achievementId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The achievement's id.

`percentComplete` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Progress from 0 to 100. 100 unlocks the achievement.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

The service treats a repeated or lower value as a no-op, so a title may call this freely
without tracking what it last sent.

### <a id="GDK_Net_XboxLive_AchievementsService_UpdateForTitleAsync_System_UInt64_System_UInt32_System_String_System_String_System_UInt32_System_Threading_CancellationToken_"></a> UpdateForTitleAsync\(ulong, uint, string, string, uint, CancellationToken\)

Reports progress towards an achievement defined by another title
(<code>XblAchievementsUpdateAchievementForTitleIdAsync</code>).

```csharp
public Task UpdateForTitleAsync(ulong xboxUserId, uint titleId, string serviceConfigurationId, string achievementId, uint percentComplete, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

`titleId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`achievementId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`percentComplete` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

#### Remarks

Only needed when the achievement lives in a different title or service configuration than
the running one; otherwise use
<xref href="GDK.Net.XboxLive.AchievementsService.UpdateAsync(System.UInt64%2cSystem.String%2cSystem.UInt32%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_XboxLive_AchievementsService_ProgressChanged"></a> ProgressChanged

Raised when the service reports progress on an achievement
(<code>XblAchievementsAddAchievementProgressChangeHandler</code>).

```csharp
public event EventHandler<AchievementProgressChangedEventArgs>? ProgressChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[AchievementProgressChangedEventArgs](GDK.Net.XboxLive.AchievementProgressChangedEventArgs.md)\>?

#### Remarks

Backed by real-time activity, so it only fires while an RTA connection is live. The native
registration is created on the first subscription and released on the last, so a title that
never subscribes pays nothing.

