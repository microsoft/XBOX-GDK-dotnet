# <a id="GDK_Net_XboxLive_AchievementRequirement"></a> Class AchievementRequirement

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One requirement that makes up an achievement's progression. Mirrors
<code>XblAchievementRequirement</code>.

```csharp
public sealed class AchievementRequirement
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementRequirement](GDK.Net.XboxLive.AchievementRequirement.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The service reports progress values as strings because a requirement can be counted in any unit
the title chose. They are surfaced unparsed rather than guessed at.

## Properties

### <a id="GDK_Net_XboxLive_AchievementRequirement_CurrentProgressValue"></a> CurrentProgressValue

Progress so far, as the service formats it. Empty when the achievement is locked.

```csharp
public string CurrentProgressValue { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_AchievementRequirement_Id"></a> Id

The requirement's id.

```csharp
public string Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_AchievementRequirement_TargetProgressValue"></a> TargetProgressValue

The value that satisfies the requirement, as the service formats it.

```csharp
public string TargetProgressValue { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

