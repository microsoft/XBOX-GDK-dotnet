# <a id="GDK_Net_PlayFab_LobbyServerDataUpdate"></a> Class LobbyServerDataUpdate

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFLobbyServerDataUpdate</code>.

```csharp
public sealed class LobbyServerDataUpdate
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LobbyServerDataUpdate](GDK.Net.PlayFab.LobbyServerDataUpdate.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_LobbyServerDataUpdate_NewServer"></a> NewServer

<code>NewServer</code>.

```csharp
public EntityKey? NewServer { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_LobbyServerDataUpdate_ServerProperties"></a> ServerProperties

The <code>ServerPropertyKeys</code> / <code>ServerPropertyValues</code> pairs.

```csharp
public IReadOnlyDictionary<string, string>? ServerProperties { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

