# <a id="GDK_Net_XboxLive_XboxLiveContext"></a> Class XboxLiveContext

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

An Xbox Live context: the per-user, per-sign-in handle every Xbox Live service call is made
through. Wraps <code>XblContextHandle</code>.

```csharp
public sealed class XboxLiveContext : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[XboxLiveContext](GDK.Net.XboxLive.XboxLiveContext.md)

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

<p>
A context is built from a signed-in <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> and stops being valid the moment that
user signs out or is replaced, so it is <b>not</b> a process-lifetime object. The compliance
obligation is to dispose and rebuild it on user change; holding a stale context makes calls fail
against the previous identity.
</p>
<p>
The native handle is owned by a <xref href="System.Runtime.InteropServices.SafeHandle" data-throw-if-not-resolved="false"></xref>, so a
missed <xref href="GDK.Net.XboxLive.XboxLiveContext.Dispose" data-throw-if-not-resolved="false"></xref> still releases it at finalization. <xref href="GDK.Net.XboxLive.XboxLiveContext.Duplicate" data-throw-if-not-resolved="false"></xref> produces
an independent instance for a component with its own lifespan, which is preferable to creating a
second context for the same user.
</p>

## Properties

### <a id="GDK_Net_XboxLive_XboxLiveContext_Achievements"></a> Achievements

Achievement queries and progress updates for this user.

```csharp
public AchievementsService Achievements { get; }
```

#### Property Value

 [AchievementsService](GDK.Net.XboxLive.AchievementsService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_Events"></a> Events

In-game events written to the title's event stream.

```csharp
public EventsService Events { get; }
```

#### Property Value

 [EventsService](GDK.Net.XboxLive.EventsService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_Leaderboards"></a> Leaderboards

Leaderboard queries for this user's title.

```csharp
public LeaderboardService Leaderboards { get; }
```

#### Property Value

 [LeaderboardService](GDK.Net.XboxLive.LeaderboardService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_MultiplayerActivity"></a> MultiplayerActivity

The activity this user is advertising, the invites sent from it, and the recent-players
list. Keeping the advertised activity accurate is a certification obligation.

```csharp
public MultiplayerActivityService MultiplayerActivity { get; }
```

#### Property Value

 [MultiplayerActivityService](GDK.Net.XboxLive.MultiplayerActivityService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_Presence"></a> Presence

Presence reporting and lookups for this user and the people they know.

```csharp
public PresenceService Presence { get; }
```

#### Property Value

 [PresenceService](GDK.Net.XboxLive.PresenceService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_Privacy"></a> Privacy

Privacy permission checks, and the avoid and mute lists. Consult this before enabling
communications or user-generated content; the checks fail closed.

```csharp
public PrivacyService Privacy { get; }
```

#### Property Value

 [PrivacyService](GDK.Net.XboxLive.PrivacyService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_Profiles"></a> Profiles

Xbox Live profile lookups for this user.

```csharp
public ProfileService Profiles { get; }
```

#### Property Value

 [ProfileService](GDK.Net.XboxLive.ProfileService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_RealTimeActivity"></a> RealTimeActivity

The Real-Time Activity connection. Activate it before subscribing to presence, social or
statistic change notifications; those subscriptions are delivered over it.

```csharp
public RealTimeActivityService RealTimeActivity { get; }
```

#### Property Value

 [RealTimeActivityService](GDK.Net.XboxLive.RealTimeActivityService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_Settings"></a> Settings

HTTP and websocket tuning for calls made through this context.

```csharp
public XboxLiveContextSettings Settings { get; }
```

#### Property Value

 [XboxLiveContextSettings](GDK.Net.XboxLive.XboxLiveContextSettings.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_Social"></a> Social

Friends, followers and reputation feedback for this user.

```csharp
public SocialService Social { get; }
```

#### Property Value

 [SocialService](GDK.Net.XboxLive.SocialService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_StringVerification"></a> StringVerification

String verification. Run every player-authored string through this before displaying it; the
checks fail closed.

```csharp
public StringVerificationService StringVerification { get; }
```

#### Property Value

 [StringVerificationService](GDK.Net.XboxLive.StringVerificationService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_TitleManagedStatistics"></a> TitleManagedStatistics

Writes to title-managed statistics, the leaderboard-backing stats the service owns on the
title's behalf.

```csharp
public TitleManagedStatisticsService TitleManagedStatistics { get; }
```

#### Property Value

 [TitleManagedStatisticsService](GDK.Net.XboxLive.TitleManagedStatisticsService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_TitleStorage"></a> TitleStorage

Cloud-backed title storage: per-user, per-title and global blobs, transferred in chunks.

```csharp
public TitleStorageService TitleStorage { get; }
```

#### Property Value

 [TitleStorageService](GDK.Net.XboxLive.TitleStorageService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_UserStatistics"></a> UserStatistics

Statistic queries for this user, and the change subscriptions that keep them fresh over the
Real-Time Activity connection.

```csharp
public UserStatisticsService UserStatistics { get; }
```

#### Property Value

 [UserStatisticsService](GDK.Net.XboxLive.UserStatisticsService.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_XboxUserId"></a> XboxUserId

The Xbox user id of the user this context was created for
(<code>XblContextGetXboxUserId</code>). Cached; constant for the context's lifetime.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_XboxLiveContext_Dispose"></a> Dispose\(\)

Releases the native context handle (<code>XblContextCloseHandle</code>).

```csharp
public void Dispose()
```

#### Remarks

Any notification handler still registered against this context (presence, social,
real-time activity, achievement progress) is removed first, while the handle is still
valid. Leaving one attached would let XSAPI invoke it against a closed handle.

### <a id="GDK_Net_XboxLive_XboxLiveContext_Duplicate"></a> Duplicate\(\)

Returns an independent context backed by its own native handle
(<code>XblContextDuplicateHandle</code>).

```csharp
public XboxLiveContext Duplicate()
```

#### Returns

 [XboxLiveContext](GDK.Net.XboxLive.XboxLiveContext.md)

### <a id="GDK_Net_XboxLive_XboxLiveContext_GetUser"></a> GetUser\(\)

Returns the user this context was created for (<code>XblContextGetUser</code>).

```csharp
public User GetUser()
```

#### Returns

 [User](../Users/GDK.Net.Users.User.md)

#### Remarks

XSAPI duplicates the underlying user handle before returning it, so the returned
<xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> owns its own handle and must be disposed independently of this
context.

