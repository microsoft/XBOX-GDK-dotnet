# <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody"></a> Class ProfilesEntityProfileBody

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFProfilesEntityProfileBody</code>.

```csharp
public sealed class ProfilesEntityProfileBody
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ProfilesEntityProfileBody](GDK.Net.PlayFab.ProfilesEntityProfileBody.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_AvatarUrl"></a> AvatarUrl

<code>AvatarUrl</code>.

```csharp
public string? AvatarUrl { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_Created"></a> Created

<code>Created</code>.

```csharp
public DateTimeOffset Created { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_DisplayName"></a> DisplayName

<code>DisplayName</code>.

```csharp
public string? DisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_Entity"></a> Entity

<code>Entity</code>.

```csharp
public EntityKey? Entity { get; set; }
```

#### Property Value

 [EntityKey](GDK.Net.PlayFab.EntityKey.md)?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_EntityChain"></a> EntityChain

<code>EntityChain</code>.

```csharp
public string? EntityChain { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_ExperimentVariants"></a> ExperimentVariants

<code>ExperimentVariants</code>.

```csharp
public IReadOnlyList<string>? ExperimentVariants { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_Files"></a> Files

<code>Files</code>.

```csharp
public IReadOnlyDictionary<string, ProfilesEntityProfileFileMetadata>? Files { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [ProfilesEntityProfileFileMetadata](GDK.Net.PlayFab.ProfilesEntityProfileFileMetadata.md)\>?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_Language"></a> Language

<code>Language</code>.

```csharp
public string? Language { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_Lineage"></a> Lineage

<code>Lineage</code>.

```csharp
public EntityLineage? Lineage { get; set; }
```

#### Property Value

 [EntityLineage](GDK.Net.PlayFab.EntityLineage.md)?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_Objects"></a> Objects

<code>Objects</code>.

```csharp
public IReadOnlyDictionary<string, ProfilesEntityDataObject>? Objects { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [ProfilesEntityDataObject](GDK.Net.PlayFab.ProfilesEntityDataObject.md)\>?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_Permissions"></a> Permissions

<code>Permissions</code>.

```csharp
public IReadOnlyList<ProfilesEntityPermissionStatement>? Permissions { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ProfilesEntityPermissionStatement](GDK.Net.PlayFab.ProfilesEntityPermissionStatement.md)\>?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_Statistics"></a> Statistics

<code>Statistics</code>.

```csharp
public IReadOnlyDictionary<string, EntityStatisticValue>? Statistics { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [EntityStatisticValue](GDK.Net.PlayFab.EntityStatisticValue.md)\>?

### <a id="GDK_Net_PlayFab_ProfilesEntityProfileBody_VersionNumber"></a> VersionNumber

<code>VersionNumber</code>.

```csharp
public int VersionNumber { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

