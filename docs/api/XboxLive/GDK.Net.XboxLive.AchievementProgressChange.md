# <a id="GDK_Net_XboxLive_AchievementProgressChange"></a> Class AchievementProgressChange

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One achievement whose progress changed. Mirrors <code>XblAchievementProgressChangeEntry</code>.

```csharp
public sealed class AchievementProgressChange
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementProgressChange](GDK.Net.XboxLive.AchievementProgressChange.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_AchievementProgressChange_AchievementId"></a> AchievementId

The achievement's id.

```csharp
public string AchievementId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_AchievementProgressChange_ProgressState"></a> ProgressState

The new progress state.

```csharp
public AchievementProgressState ProgressState { get; }
```

#### Property Value

 [AchievementProgressState](GDK.Net.XboxLive.AchievementProgressState.md)

### <a id="GDK_Net_XboxLive_AchievementProgressChange_Progression"></a> Progression

The new progress detail.

```csharp
public AchievementProgression Progression { get; }
```

#### Property Value

 [AchievementProgression](GDK.Net.XboxLive.AchievementProgression.md)

