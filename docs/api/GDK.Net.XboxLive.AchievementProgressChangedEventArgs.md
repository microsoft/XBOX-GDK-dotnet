# <a id="GDK_Net_XboxLive_AchievementProgressChangedEventArgs"></a> Class AchievementProgressChangedEventArgs

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.XboxLive.AchievementsService.ProgressChanged" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>XblAchievementProgressChangeEventArgs</code>.

```csharp
public sealed class AchievementProgressChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[AchievementProgressChangedEventArgs](GDK.Net.XboxLive.AchievementProgressChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_AchievementProgressChangedEventArgs_Changes"></a> Changes

The achievements whose progress changed.

```csharp
public IReadOnlyList<AchievementProgressChange> Changes { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[AchievementProgressChange](GDK.Net.XboxLive.AchievementProgressChange.md)\>

