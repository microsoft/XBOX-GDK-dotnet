# <a id="GDK_Net_PlayFab_PlayFabServiceConfig"></a> Class PlayFabServiceConfig

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Identifies the PlayFab title and API endpoint a login is made against. Wraps
<code>PFServiceConfigHandle</code>.

```csharp
public sealed class PlayFabServiceConfig : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_PlayFabServiceConfig__ctor_System_String_System_String_"></a> PlayFabServiceConfig\(string, string\)

Creates a service configuration for a title (<code>PFServiceConfigCreateHandle</code>).

```csharp
public PlayFabServiceConfig(string apiEndpoint, string titleId)
```

#### Parameters

`apiEndpoint` [string](https://learn.microsoft.com/dotnet/api/system.string)

The title's PlayFab API endpoint, for example <code>https://ABCDE.playfabapi.com</code>.

`titleId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The title's PlayFab title id.

## Properties

### <a id="GDK_Net_PlayFab_PlayFabServiceConfig_ApiEndpoint"></a> ApiEndpoint

The configured PlayFab API endpoint (<code>PFServiceConfigGetAPIEndpoint</code>).

```csharp
public string ApiEndpoint { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_PlayFabServiceConfig_TitleId"></a> TitleId

The configured PlayFab title id (<code>PFServiceConfigGetTitleId</code>).

```csharp
public string TitleId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="GDK_Net_PlayFab_PlayFabServiceConfig_Dispose"></a> Dispose\(\)

Releases the native handle.

```csharp
public void Dispose()
```

### <a id="GDK_Net_PlayFab_PlayFabServiceConfig_Duplicate"></a> Duplicate\(\)

Returns an independent instance backed by its own native handle
(<code>PFServiceConfigDuplicateHandle</code>).

```csharp
public PlayFabServiceConfig Duplicate()
```

#### Returns

 [PlayFabServiceConfig](GDK.Net.PlayFab.PlayFabServiceConfig.md)

