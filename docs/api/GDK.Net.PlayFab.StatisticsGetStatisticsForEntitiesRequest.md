# <a id="GDK_Net_PlayFab_StatisticsGetStatisticsForEntitiesRequest"></a> Class StatisticsGetStatisticsForEntitiesRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFStatisticsGetStatisticsForEntitiesRequest</code>.

```csharp
public sealed class StatisticsGetStatisticsForEntitiesRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StatisticsGetStatisticsForEntitiesRequest](GDK.Net.PlayFab.StatisticsGetStatisticsForEntitiesRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_StatisticsGetStatisticsForEntitiesRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_StatisticsGetStatisticsForEntitiesRequest_Entities"></a> Entities

<code>Entities</code>.

```csharp
public IReadOnlyList<EntityKey>? Entities { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[EntityKey](GDK.Net.PlayFab.EntityKey.md)\>?

### <a id="GDK_Net_PlayFab_StatisticsGetStatisticsForEntitiesRequest_StatisticNames"></a> StatisticNames

<code>StatisticNames</code>.

```csharp
public IReadOnlyList<string>? StatisticNames { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

