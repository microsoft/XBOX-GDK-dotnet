# <a id="GDK_Net_PlayFab_PlayerDataManagementClientUpdateUserDataRequest"></a> Class PlayerDataManagementClientUpdateUserDataRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFPlayerDataManagementClientUpdateUserDataRequest</code>.

```csharp
public sealed class PlayerDataManagementClientUpdateUserDataRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayerDataManagementClientUpdateUserDataRequest](GDK.Net.PlayFab.PlayerDataManagementClientUpdateUserDataRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_PlayerDataManagementClientUpdateUserDataRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_PlayerDataManagementClientUpdateUserDataRequest_Data"></a> Data

<code>Data</code>.

```csharp
public IReadOnlyDictionary<string, string>? Data { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_PlayerDataManagementClientUpdateUserDataRequest_KeysToRemove"></a> KeysToRemove

<code>KeysToRemove</code>.

```csharp
public IReadOnlyList<string>? KeysToRemove { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_PlayerDataManagementClientUpdateUserDataRequest_Permission"></a> Permission

<code>Permission</code>.

```csharp
public UserDataPermission? Permission { get; set; }
```

#### Property Value

 [UserDataPermission](GDK.Net.PlayFab.UserDataPermission.md)?

