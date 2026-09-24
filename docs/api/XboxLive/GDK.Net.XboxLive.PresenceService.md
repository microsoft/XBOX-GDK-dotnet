# <a id="GDK_Net_XboxLive_PresenceService"></a> Class PresenceService

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Xbox Live presence queries, rich presence updates and real-time presence notifications.
Mirrors <code>presence_c.h</code>.

```csharp
public sealed class PresenceService
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PresenceService](GDK.Net.XboxLive.PresenceService.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Real-time presence subscriptions and change events are backed by XSAPI Real-Time Activity. The
title must activate RTA for the owning <xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref> before expecting device or
title presence notifications; activation is provided by the RTA service projection and is not a
compile-time dependency of this type.

## Methods

### <a id="GDK_Net_XboxLive_PresenceService_GetAsync_System_UInt64_System_Threading_CancellationToken_"></a> GetAsync\(ulong, CancellationToken\)

Gets one user's presence (<code>XblPresenceGetPresenceAsync</code>).

```csharp
public Task<PresenceRecord> GetAsync(ulong xboxUserId, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

The user whose presence should be read.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PresenceRecord](GDK.Net.XboxLive.PresenceRecord.md)\>

### <a id="GDK_Net_XboxLive_PresenceService_GetAsync_System_Collections_Generic_IEnumerable_System_UInt64__GDK_Net_XboxLive_PresenceQueryFilters_System_Threading_CancellationToken_"></a> GetAsync\(IEnumerable<ulong\>, PresenceQueryFilters?, CancellationToken\)

Gets presence for several users in one request.

```csharp
public Task<IReadOnlyList<PresenceRecord>> GetAsync(IEnumerable<ulong> xboxUserIds, PresenceQueryFilters? filters = null, CancellationToken cancellationToken = default)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

The users whose presence should be read.

`filters` [PresenceQueryFilters](GDK.Net.XboxLive.PresenceQueryFilters.md)?

Optional result filters.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PresenceRecord](GDK.Net.XboxLive.PresenceRecord.md)\>\>

### <a id="GDK_Net_XboxLive_PresenceService_GetAsync_System_Collections_Generic_IEnumerable_System_UInt64__System_Threading_CancellationToken_"></a> GetAsync\(IEnumerable<ulong\>, CancellationToken\)

Gets presence for several users in one request with no filters.

```csharp
public Task<IReadOnlyList<PresenceRecord>> GetAsync(IEnumerable<ulong> xboxUserIds, CancellationToken cancellationToken)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

The users whose presence should be read.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PresenceRecord](GDK.Net.XboxLive.PresenceRecord.md)\>\>

### <a id="GDK_Net_XboxLive_PresenceService_GetForSocialGroupAsync_GDK_Net_XboxLive_PresenceSocialGroup_System_Nullable_System_UInt64__GDK_Net_XboxLive_PresenceQueryFilters_System_Threading_CancellationToken_"></a> GetForSocialGroupAsync\(PresenceSocialGroup, ulong?, PresenceQueryFilters?, CancellationToken\)

Gets presence for a social group (<code>XblPresenceGetPresenceForSocialGroupAsync</code>).

```csharp
public Task<IReadOnlyList<PresenceRecord>> GetForSocialGroupAsync(PresenceSocialGroup socialGroup, ulong? socialGroupOwnerXuid = null, PresenceQueryFilters? filters = null, CancellationToken cancellationToken = default)
```

#### Parameters

`socialGroup` [PresenceSocialGroup](GDK.Net.XboxLive.PresenceSocialGroup.md)

The social group to query.

`socialGroupOwnerXuid` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)?

The owner of the group, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the context user.

`filters` [PresenceQueryFilters](GDK.Net.XboxLive.PresenceQueryFilters.md)?

Optional result filters.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PresenceRecord](GDK.Net.XboxLive.PresenceRecord.md)\>\>

### <a id="GDK_Net_XboxLive_PresenceService_SetPresenceAsync_System_Boolean_GDK_Net_XboxLive_PresenceRichPresenceIds_System_Threading_CancellationToken_"></a> SetPresenceAsync\(bool, PresenceRichPresenceIds?, CancellationToken\)

Sets rich presence for the context's user (<code>XblPresenceSetPresenceAsync</code>).

```csharp
public Task SetPresenceAsync(bool isUserActiveInTitle, PresenceRichPresenceIds? richPresenceIds = null, CancellationToken cancellationToken = default)
```

#### Parameters

`isUserActiveInTitle` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the user is active in the current title.

`richPresenceIds` [PresenceRichPresenceIds](GDK.Net.XboxLive.PresenceRichPresenceIds.md)?

Optional rich presence string identifiers.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the call; surfaces as <xref href="System.OperationCanceledException" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_XboxLive_PresenceService_StopTrackingAdditionalTitles_System_Collections_Generic_IEnumerable_System_UInt32__"></a> StopTrackingAdditionalTitles\(IEnumerable<uint\>\)

Stops tracking additional title ids for real-time title presence changes.

```csharp
public void StopTrackingAdditionalTitles(IEnumerable<uint> titleIds)
```

#### Parameters

`titleIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[uint](https://learn.microsoft.com/dotnet/api/system.uint32)\>

Title ids to remove from the tracked set.

### <a id="GDK_Net_XboxLive_PresenceService_StopTrackingUsers_System_Collections_Generic_IEnumerable_System_UInt64__"></a> StopTrackingUsers\(IEnumerable<ulong\>\)

Stops tracking users for real-time presence changes.

```csharp
public void StopTrackingUsers(IEnumerable<ulong> xboxUserIds)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Users to remove from the tracked set.

### <a id="GDK_Net_XboxLive_PresenceService_TrackAdditionalTitles_System_Collections_Generic_IEnumerable_System_UInt32__"></a> TrackAdditionalTitles\(IEnumerable<uint\>\)

Tracks additional title ids for real-time title presence changes.

```csharp
public void TrackAdditionalTitles(IEnumerable<uint> titleIds)
```

#### Parameters

`titleIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[uint](https://learn.microsoft.com/dotnet/api/system.uint32)\>

Title ids to append to the tracked set.

#### Remarks

The current title is tracked by default.

### <a id="GDK_Net_XboxLive_PresenceService_TrackUsers_System_Collections_Generic_IEnumerable_System_UInt64__"></a> TrackUsers\(IEnumerable<ulong\>\)

Tracks users for real-time device and title presence changes.

```csharp
public void TrackUsers(IEnumerable<ulong> xboxUserIds)
```

#### Parameters

`xboxUserIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

Users to append to the tracked set.

#### Remarks

Changes arrive through <xref href="GDK.Net.XboxLive.PresenceService.DevicePresenceChanged" data-throw-if-not-resolved="false"></xref> and
    <xref href="GDK.Net.XboxLive.PresenceService.TitlePresenceChanged" data-throw-if-not-resolved="false"></xref>; XSAPI opens the real-time activity connection on demand.

### <a id="GDK_Net_XboxLive_PresenceService_DevicePresenceChanged"></a> DevicePresenceChanged

Raised when a tracked user's device presence changes.

```csharp
public event EventHandler<DevicePresenceChangedEventArgs>? DevicePresenceChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[DevicePresenceChangedEventArgs](GDK.Net.XboxLive.DevicePresenceChangedEventArgs.md)\>?

#### Remarks

Call <xref href="GDK.Net.XboxLive.PresenceService.TrackUsers(System.Collections.Generic.IEnumerable%7bSystem.UInt64%7d)" data-throw-if-not-resolved="false"></xref> first. XSAPI opens the real-time activity connection on demand,
and callbacks arrive on an XSAPI-internal thread rather than this context's task queue.

### <a id="GDK_Net_XboxLive_PresenceService_TitlePresenceChanged"></a> TitlePresenceChanged

Raised when a tracked user's title presence changes.

```csharp
public event EventHandler<TitlePresenceChangedEventArgs>? TitlePresenceChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[TitlePresenceChangedEventArgs](GDK.Net.XboxLive.TitlePresenceChangedEventArgs.md)\>?

#### Remarks

Call <xref href="GDK.Net.XboxLive.PresenceService.TrackUsers(System.Collections.Generic.IEnumerable%7bSystem.UInt64%7d)" data-throw-if-not-resolved="false"></xref> and, for non-current titles, <xref href="GDK.Net.XboxLive.PresenceService.TrackAdditionalTitles(System.Collections.Generic.IEnumerable%7bSystem.UInt32%7d)" data-throw-if-not-resolved="false"></xref>
first. XSAPI opens the real-time activity connection on demand, and callbacks arrive on an
XSAPI-internal thread rather than this context's task queue.

