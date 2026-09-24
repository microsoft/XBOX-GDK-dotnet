# <a id="GDK_Net_PlayFab_MultiplayerServerBuildSummary"></a> Class MultiplayerServerBuildSummary

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFMultiplayerServerBuildSummary</code>.

```csharp
public sealed class MultiplayerServerBuildSummary
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MultiplayerServerBuildSummary](GDK.Net.PlayFab.MultiplayerServerBuildSummary.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_MultiplayerServerBuildSummary_BuildId"></a> BuildId

<code>BuildId</code>.

```csharp
public string? BuildId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_MultiplayerServerBuildSummary_BuildName"></a> BuildName

<code>BuildName</code>.

```csharp
public string? BuildName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_MultiplayerServerBuildSummary_CreationTime"></a> CreationTime

<code>CreationTime</code>.

```csharp
public DateTimeOffset? CreationTime { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_PlayFab_MultiplayerServerBuildSummary_Metadata"></a> Metadata

<code>Metadata</code>.

```csharp
public IReadOnlyDictionary<string, string>? Metadata { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_MultiplayerServerBuildSummary_RegionConfigurations"></a> RegionConfigurations

<code>RegionConfigurations</code>.

```csharp
public IReadOnlyList<MultiplayerServerBuildRegion>? RegionConfigurations { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[MultiplayerServerBuildRegion](GDK.Net.PlayFab.MultiplayerServerBuildRegion.md)\>?

