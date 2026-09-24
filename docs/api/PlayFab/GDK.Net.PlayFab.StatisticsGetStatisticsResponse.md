# <a id="GDK_Net_PlayFab_StatisticsGetStatisticsResponse"></a> Class StatisticsGetStatisticsResponse

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFStatisticsGetStatisticsResponse</code>.

```csharp
public sealed class StatisticsGetStatisticsResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StatisticsGetStatisticsResponse](GDK.Net.PlayFab.StatisticsGetStatisticsResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_StatisticsGetStatisticsResponse_ColumnDetails"></a> ColumnDetails

<code>ColumnDetails</code>.

```csharp
public IReadOnlyDictionary<string, StatisticsStatisticColumnCollection>? ColumnDetails { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [StatisticsStatisticColumnCollection](GDK.Net.PlayFab.StatisticsStatisticColumnCollection.md)\>?

### <a id="GDK_Net_PlayFab_StatisticsGetStatisticsResponse_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_StatisticsGetStatisticsResponse_Statistics"></a> Statistics

<code>Statistics</code>.

```csharp
public IReadOnlyDictionary<string, StatisticsEntityStatisticValue>? Statistics { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [StatisticsEntityStatisticValue](GDK.Net.PlayFab.StatisticsEntityStatisticValue.md)\>?

