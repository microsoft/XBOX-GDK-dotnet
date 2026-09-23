# <a id="GDK_Net_XboxLive_AchievementsManagerEvent"></a> Class AchievementsManagerEvent

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One event returned by <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref>.

```csharp
public abstract record AchievementsManagerEvent : IEquatable<AchievementsManagerEvent>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementsManagerEvent](GDK.Net.XboxLive.AchievementsManagerEvent.md)

#### Derived

[AchievementsManagerAchievementProgressUpdatedEvent](GDK.Net.XboxLive.AchievementsManagerAchievementProgressUpdatedEvent.md), 
[AchievementsManagerAchievementUnlockedEvent](GDK.Net.XboxLive.AchievementsManagerAchievementUnlockedEvent.md), 
[AchievementsManagerLocalUserInitialStateSyncedEvent](GDK.Net.XboxLive.AchievementsManagerLocalUserInitialStateSyncedEvent.md)

#### Implements

[IEquatable<AchievementsManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Derived records model the native <code>XblAchievementsManagerEventType</code> variants. The list
returned from <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> keeps completions and notifications
interleaved in the exact order XSAPI returned them.

## Properties

### <a id="GDK_Net_XboxLive_AchievementsManagerEvent_Type"></a> Type

The native event variant.

```csharp
public AchievementsManagerEventType Type { get; }
```

#### Property Value

 [AchievementsManagerEventType](GDK.Net.XboxLive.AchievementsManagerEventType.md)

### <a id="GDK_Net_XboxLive_AchievementsManagerEvent_XboxUserId"></a> XboxUserId

The Xbox user id the event belongs to.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

