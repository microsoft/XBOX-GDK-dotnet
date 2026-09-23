# <a id="GDK_Net_PlayFab_PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest"></a> Class PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFPlayerDataManagementClientUpdatePlayerCustomPropertiesRequest</code>.

```csharp
public sealed class PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest](GDK.Net.PlayFab.PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest_ExpectedPropertiesVersion"></a> ExpectedPropertiesVersion

<code>ExpectedPropertiesVersion</code>.

```csharp
public int? ExpectedPropertiesVersion { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="GDK_Net_PlayFab_PlayerDataManagementClientUpdatePlayerCustomPropertiesRequest_Properties"></a> Properties

<code>Properties</code>.

```csharp
public IReadOnlyList<PlayerDataManagementUpdateProperty>? Properties { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PlayerDataManagementUpdateProperty](GDK.Net.PlayFab.PlayerDataManagementUpdateProperty.md)\>?

