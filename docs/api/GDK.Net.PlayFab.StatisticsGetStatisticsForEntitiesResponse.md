# <a id="GDK_Net_PlayFab_StatisticsGetStatisticsForEntitiesResponse"></a> Class StatisticsGetStatisticsForEntitiesResponse

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFStatisticsGetStatisticsForEntitiesResponse</code>.

```csharp
public sealed class StatisticsGetStatisticsForEntitiesResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StatisticsGetStatisticsForEntitiesResponse](GDK.Net.PlayFab.StatisticsGetStatisticsForEntitiesResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_StatisticsGetStatisticsForEntitiesResponse_ColumnDetails"></a> ColumnDetails

<code>ColumnDetails</code>.

```csharp
public IReadOnlyDictionary<string, StatisticsStatisticColumnCollection>? ColumnDetails { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [StatisticsStatisticColumnCollection](GDK.Net.PlayFab.StatisticsStatisticColumnCollection.md)\>?

### <a id="GDK_Net_PlayFab_StatisticsGetStatisticsForEntitiesResponse_EntitiesStatistics"></a> EntitiesStatistics

<code>EntitiesStatistics</code>.

```csharp
public IReadOnlyList<StatisticsEntityStatistics>? EntitiesStatistics { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StatisticsEntityStatistics](GDK.Net.PlayFab.StatisticsEntityStatistics.md)\>?

