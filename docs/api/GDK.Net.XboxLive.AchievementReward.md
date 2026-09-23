# <a id="GDK_Net_XboxLive_AchievementReward"></a> Class AchievementReward

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

A reward granted for unlocking an achievement. Mirrors <code>XblAchievementReward</code>.

```csharp
public sealed class AchievementReward
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AchievementReward](GDK.Net.XboxLive.AchievementReward.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_AchievementReward_Description"></a> Description

The reward's description.

```csharp
public string Description { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_AchievementReward_MediaAsset"></a> MediaAsset

The reward's media asset, when it has one.

```csharp
public AchievementMediaAsset? MediaAsset { get; }
```

#### Property Value

 [AchievementMediaAsset](GDK.Net.XboxLive.AchievementMediaAsset.md)?

### <a id="GDK_Net_XboxLive_AchievementReward_Name"></a> Name

The reward's name.

```csharp
public string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_AchievementReward_RewardType"></a> RewardType

Whether the reward is gamerscore, in-app or art.

```csharp
public AchievementRewardType RewardType { get; }
```

#### Property Value

 [AchievementRewardType](GDK.Net.XboxLive.AchievementRewardType.md)

### <a id="GDK_Net_XboxLive_AchievementReward_Value"></a> Value

The reward's value, as the service formats it. Interpret with <xref href="GDK.Net.XboxLive.AchievementReward.ValueType" data-throw-if-not-resolved="false"></xref>.

```csharp
public string Value { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_AchievementReward_ValueType"></a> ValueType

The property type of <xref href="GDK.Net.XboxLive.AchievementReward.Value" data-throw-if-not-resolved="false"></xref>.

```csharp
public string ValueType { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

