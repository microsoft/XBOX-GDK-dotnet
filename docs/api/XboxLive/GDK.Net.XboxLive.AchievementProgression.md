# <a id="GDK_Net_XboxLive_AchievementProgression"></a> Class AchievementProgression

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Progress towards an achievement. Mirrors <code>XblAchievementProgression</code>.

```csharp
public sealed class AchievementProgression
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementProgression](GDK.Net.XboxLive.AchievementProgression.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_AchievementProgression_Requirements"></a> Requirements

The requirements that make up this achievement.

```csharp
public IReadOnlyList<AchievementRequirement> Requirements { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[AchievementRequirement](GDK.Net.XboxLive.AchievementRequirement.md)\>

### <a id="GDK_Net_XboxLive_AchievementProgression_TimeUnlocked"></a> TimeUnlocked

When the achievement was unlocked, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it has not been. The
native field is a <code>time_t</code> of 0 in that case, which is a real instant rather than an
absent one, so it is projected as null.

```csharp
public DateTimeOffset? TimeUnlocked { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

