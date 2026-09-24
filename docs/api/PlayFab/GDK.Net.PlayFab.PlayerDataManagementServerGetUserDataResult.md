# <a id="GDK_Net_PlayFab_PlayerDataManagementServerGetUserDataResult"></a> Class PlayerDataManagementServerGetUserDataResult

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFPlayerDataManagementServerGetUserDataResult</code>.

```csharp
public sealed class PlayerDataManagementServerGetUserDataResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayerDataManagementServerGetUserDataResult](GDK.Net.PlayFab.PlayerDataManagementServerGetUserDataResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_PlayerDataManagementServerGetUserDataResult_Data"></a> Data

<code>Data</code>.

```csharp
public IReadOnlyDictionary<string, UserDataRecord>? Data { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [UserDataRecord](GDK.Net.PlayFab.UserDataRecord.md)\>?

### <a id="GDK_Net_PlayFab_PlayerDataManagementServerGetUserDataResult_DataVersion"></a> DataVersion

<code>DataVersion</code>.

```csharp
public uint DataVersion { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_PlayerDataManagementServerGetUserDataResult_PlayFabId"></a> PlayFabId

<code>PlayFabId</code>.

```csharp
public string? PlayFabId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

