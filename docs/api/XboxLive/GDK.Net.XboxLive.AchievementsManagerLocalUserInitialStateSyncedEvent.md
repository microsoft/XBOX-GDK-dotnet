# <a id="GDK_Net_XboxLive_AchievementsManagerLocalUserInitialStateSyncedEvent"></a> Class AchievementsManagerLocalUserInitialStateSyncedEvent

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

A local user's initial achievement cache has finished syncing.

```csharp
public sealed record AchievementsManagerLocalUserInitialStateSyncedEvent : AchievementsManagerEvent, IEquatable<AchievementsManagerEvent>, IEquatable<AchievementsManagerLocalUserInitialStateSyncedEvent>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementsManagerEvent](GDK.Net.XboxLive.AchievementsManagerEvent.md) ← 
[AchievementsManagerLocalUserInitialStateSyncedEvent](GDK.Net.XboxLive.AchievementsManagerLocalUserInitialStateSyncedEvent.md)

#### Implements

[IEquatable<AchievementsManagerEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<AchievementsManagerLocalUserInitialStateSyncedEvent\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[AchievementsManagerEvent.Type](GDK.Net.XboxLive.AchievementsManagerEvent.md\#GDK\_Net\_XboxLive\_AchievementsManagerEvent\_Type), 
[AchievementsManagerEvent.XboxUserId](GDK.Net.XboxLive.AchievementsManagerEvent.md\#GDK\_Net\_XboxLive\_AchievementsManagerEvent\_XboxUserId), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Cached queries for this user are safe after this event has been returned by
<xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref> and <xref href="GDK.Net.XboxLive.AchievementsManager.IsUserInitialized(System.UInt64)" data-throw-if-not-resolved="false"></xref>
returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

