# <a id="GDK_Net_XboxLive_Achievement"></a> Class Achievement

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One Xbox Live achievement. Managed snapshot of <code>XblAchievement</code>.

```csharp
public sealed class Achievement
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Achievement](GDK.Net.XboxLive.Achievement.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The native struct is a graph of pointers into memory owned by the
<code>XblAchievementsResultHandle</code> it came from, and that memory dies with the handle. Every
achievement is therefore copied out in full while the handle is alive, so an instance stays
usable after the result it came from is disposed.

## Properties

### <a id="GDK_Net_XboxLive_Achievement_Available"></a> Available

The window the achievement is available in.

```csharp
public AchievementTimeWindow Available { get; }
```

#### Property Value

 [AchievementTimeWindow](GDK.Net.XboxLive.AchievementTimeWindow.md)

### <a id="GDK_Net_XboxLive_Achievement_DeepLink"></a> DeepLink

A deep link into the title that starts the relevant activity.

```csharp
public string DeepLink { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_Achievement_EstimatedUnlockTime"></a> EstimatedUnlockTime

The service's estimate of how long the achievement takes to unlock.

```csharp
public TimeSpan EstimatedUnlockTime { get; }
```

#### Property Value

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

### <a id="GDK_Net_XboxLive_Achievement_Id"></a> Id

The achievement's id.

```csharp
public string Id { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_Achievement_IsRevoked"></a> IsRevoked

Whether the achievement has been revoked, for example after cheat detection.

```csharp
public bool IsRevoked { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_Achievement_IsSecret"></a> IsSecret

Whether the achievement's details are hidden until it is unlocked.

```csharp
public bool IsSecret { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_Achievement_IsUnlocked"></a> IsUnlocked

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when <xref href="GDK.Net.XboxLive.Achievement.ProgressState" data-throw-if-not-resolved="false"></xref> is
<xref href="GDK.Net.XboxLive.AchievementProgressState.Achieved" data-throw-if-not-resolved="false"></xref> and the achievement has not been revoked.

```csharp
public bool IsUnlocked { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_Achievement_LockedDescription"></a> LockedDescription

Description shown while the achievement is locked.

```csharp
public string LockedDescription { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_Achievement_MediaAssets"></a> MediaAssets

Icons and artwork for the achievement.

```csharp
public IReadOnlyList<AchievementMediaAsset> MediaAssets { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[AchievementMediaAsset](GDK.Net.XboxLive.AchievementMediaAsset.md)\>

### <a id="GDK_Net_XboxLive_Achievement_Name"></a> Name

The achievement's localized name.

```csharp
public string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_Achievement_ParticipationType"></a> ParticipationType

Whether the achievement is earned individually or as a group.

```csharp
public AchievementParticipationType ParticipationType { get; }
```

#### Property Value

 [AchievementParticipationType](GDK.Net.XboxLive.AchievementParticipationType.md)

### <a id="GDK_Net_XboxLive_Achievement_PlatformsAvailableOn"></a> PlatformsAvailableOn

The platforms the achievement can be unlocked on.

```csharp
public IReadOnlyList<string> PlatformsAvailableOn { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_XboxLive_Achievement_ProductId"></a> ProductId

The product the achievement belongs to.

```csharp
public string ProductId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_Achievement_ProgressState"></a> ProgressState

The player's progress state.

```csharp
public AchievementProgressState ProgressState { get; }
```

#### Property Value

 [AchievementProgressState](GDK.Net.XboxLive.AchievementProgressState.md)

### <a id="GDK_Net_XboxLive_Achievement_Progression"></a> Progression

The player's progress detail.

```csharp
public AchievementProgression Progression { get; }
```

#### Property Value

 [AchievementProgression](GDK.Net.XboxLive.AchievementProgression.md)

### <a id="GDK_Net_XboxLive_Achievement_Rewards"></a> Rewards

What unlocking the achievement grants.

```csharp
public IReadOnlyList<AchievementReward> Rewards { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[AchievementReward](GDK.Net.XboxLive.AchievementReward.md)\>

### <a id="GDK_Net_XboxLive_Achievement_ServiceConfigurationId"></a> ServiceConfigurationId

The service configuration the achievement belongs to.

```csharp
public string ServiceConfigurationId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_Achievement_TitleAssociations"></a> TitleAssociations

The titles this achievement is associated with.

```csharp
public IReadOnlyList<AchievementTitleAssociation> TitleAssociations { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[AchievementTitleAssociation](GDK.Net.XboxLive.AchievementTitleAssociation.md)\>

### <a id="GDK_Net_XboxLive_Achievement_Type"></a> Type

Whether the achievement is persistent or a challenge.

```csharp
public AchievementType Type { get; }
```

#### Property Value

 [AchievementType](GDK.Net.XboxLive.AchievementType.md)

### <a id="GDK_Net_XboxLive_Achievement_UnlockedDescription"></a> UnlockedDescription

Description shown once the achievement is unlocked.

```csharp
public string UnlockedDescription { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="GDK_Net_XboxLive_Achievement_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

