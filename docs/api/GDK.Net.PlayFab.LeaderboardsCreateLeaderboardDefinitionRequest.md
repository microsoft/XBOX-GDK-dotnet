# <a id="GDK_Net_PlayFab_LeaderboardsCreateLeaderboardDefinitionRequest"></a> Class LeaderboardsCreateLeaderboardDefinitionRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFLeaderboardsCreateLeaderboardDefinitionRequest</code>.

```csharp
public sealed class LeaderboardsCreateLeaderboardDefinitionRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LeaderboardsCreateLeaderboardDefinitionRequest](GDK.Net.PlayFab.LeaderboardsCreateLeaderboardDefinitionRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_LeaderboardsCreateLeaderboardDefinitionRequest_Columns"></a> Columns

<code>Columns</code>.

```csharp
public IReadOnlyList<LeaderboardsLeaderboardColumn>? Columns { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LeaderboardsLeaderboardColumn](GDK.Net.PlayFab.LeaderboardsLeaderboardColumn.md)\>?

### <a id="GDK_Net_PlayFab_LeaderboardsCreateLeaderboardDefinitionRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_LeaderboardsCreateLeaderboardDefinitionRequest_EntityType"></a> EntityType

<code>EntityType</code>.

```csharp
public string? EntityType { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_LeaderboardsCreateLeaderboardDefinitionRequest_EventEmissionConfig"></a> EventEmissionConfig

<code>EventEmissionConfig</code>.

```csharp
public LeaderboardsLeaderboardEventEmissionConfig? EventEmissionConfig { get; set; }
```

#### Property Value

 [LeaderboardsLeaderboardEventEmissionConfig](GDK.Net.PlayFab.LeaderboardsLeaderboardEventEmissionConfig.md)?

### <a id="GDK_Net_PlayFab_LeaderboardsCreateLeaderboardDefinitionRequest_Name"></a> Name

<code>Name</code>.

```csharp
public string? Name { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_LeaderboardsCreateLeaderboardDefinitionRequest_SizeLimit"></a> SizeLimit

<code>SizeLimit</code>.

```csharp
public int SizeLimit { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_LeaderboardsCreateLeaderboardDefinitionRequest_VersionConfiguration"></a> VersionConfiguration

<code>VersionConfiguration</code>.

```csharp
public VersionConfiguration? VersionConfiguration { get; set; }
```

#### Property Value

 [VersionConfiguration](GDK.Net.PlayFab.VersionConfiguration.md)?

