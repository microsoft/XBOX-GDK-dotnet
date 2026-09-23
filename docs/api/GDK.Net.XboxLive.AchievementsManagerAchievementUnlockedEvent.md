# <a id="GDK_Net_XboxLive_AchievementsManagerAchievementUnlockedEvent"></a> Class AchievementsManagerAchievementUnlockedEvent

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

An achievement was unlocked for a local user.

```csharp
public sealed record AchievementsManagerAchievementUnlockedEvent : AchievementsManagerEvent, IEquatable<AchievementsManagerEvent>, IEquatable<AchievementsManagerAchievementUnlockedEvent>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementsManagerEvent](GDK.Net.XboxLive.AchievementsManagerEvent.md) ← 
[AchievementsManagerAchievementUnlockedEvent](GDK.Net.XboxLive.AchievementsManagerAchievementUnlockedEvent.md)

#### Implements

[IEquatable<AchievementsManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<AchievementsManagerAchievementUnlockedEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[AchievementsManagerEvent.Type](GDK.Net.XboxLive.AchievementsManagerEvent.md\#GDK\_Net\_XboxLive\_AchievementsManagerEvent\_Type), 
[AchievementsManagerEvent.XboxUserId](GDK.Net.XboxLive.AchievementsManagerEvent.md\#GDK\_Net\_XboxLive\_AchievementsManagerEvent\_XboxUserId), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_AchievementsManagerAchievementUnlockedEvent_Progress"></a> Progress

The unlocked achievement and its final progress state.

```csharp
public AchievementProgressChange Progress { get; }
```

#### Property Value

 [AchievementProgressChange](GDK.Net.XboxLive.AchievementProgressChange.md)

