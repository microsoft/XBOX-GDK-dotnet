# <a id="GDK_Net_PlayFab_LeaderboardsUpdateLeaderboardEntriesRequest"></a> Class LeaderboardsUpdateLeaderboardEntriesRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFLeaderboardsUpdateLeaderboardEntriesRequest</code>.

```csharp
public sealed class LeaderboardsUpdateLeaderboardEntriesRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LeaderboardsUpdateLeaderboardEntriesRequest](GDK.Net.PlayFab.LeaderboardsUpdateLeaderboardEntriesRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_LeaderboardsUpdateLeaderboardEntriesRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_LeaderboardsUpdateLeaderboardEntriesRequest_Entries"></a> Entries

<code>Entries</code>.

```csharp
public IReadOnlyList<LeaderboardsLeaderboardEntryUpdate>? Entries { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LeaderboardsLeaderboardEntryUpdate](GDK.Net.PlayFab.LeaderboardsLeaderboardEntryUpdate.md)\>?

### <a id="GDK_Net_PlayFab_LeaderboardsUpdateLeaderboardEntriesRequest_LeaderboardName"></a> LeaderboardName

<code>LeaderboardName</code>.

```csharp
public string? LeaderboardName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

