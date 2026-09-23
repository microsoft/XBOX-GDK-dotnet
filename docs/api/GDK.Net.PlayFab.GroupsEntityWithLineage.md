# <a id="GDK_Net_PlayFab_GroupsEntityWithLineage"></a> Class GroupsEntityWithLineage

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFGroupsEntityWithLineage</code>.

```csharp
public sealed class GroupsEntityWithLineage
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GroupsEntityWithLineage](GDK.Net.PlayFab.GroupsEntityWithLineage.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_GroupsEntityWithLineage_Key"></a> Key

<code>Key</code>.

```csharp
public EntityKey? Key { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_GroupsEntityWithLineage_Lineage"></a> Lineage

<code>Lineage</code>.

```csharp
public IReadOnlyDictionary<string, EntityKey>? Lineage { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [EntityKey](GDK.Net.PlayFab.EntityKey.md)\>?

