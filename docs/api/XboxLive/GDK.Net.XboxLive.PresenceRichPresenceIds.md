# <a id="GDK_Net_XboxLive_PresenceRichPresenceIds"></a> Class PresenceRichPresenceIds

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Rich presence string identifiers supplied to <xref href="GDK.Net.XboxLive.PresenceService.SetPresenceAsync(System.Boolean%2cGDK.Net.XboxLive.PresenceRichPresenceIds%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class PresenceRichPresenceIds
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PresenceRichPresenceIds](GDK.Net.XboxLive.PresenceRichPresenceIds.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_XboxLive_PresenceRichPresenceIds__ctor_System_String_System_String_System_Collections_Generic_IEnumerable_System_String__"></a> PresenceRichPresenceIds\(string, string, IEnumerable<string\>?\)

Creates rich presence string identifiers for <code>XblPresenceSetPresenceAsync</code>.

```csharp
public PresenceRichPresenceIds(string serviceConfigurationId, string presenceId, IEnumerable<string>? presenceTokenIds = null)
```

#### Parameters

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The SCID containing the presence strings.

`presenceId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The presence string id defined in the service configuration.

`presenceTokenIds` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

Optional replacement token ids used by the presence string.

## Properties

### <a id="GDK_Net_XboxLive_PresenceRichPresenceIds_PresenceId"></a> PresenceId

The presence string id defined in the service configuration.

```csharp
public string PresenceId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_PresenceRichPresenceIds_PresenceTokenIds"></a> PresenceTokenIds

The optional replacement token ids used by the presence string.

```csharp
public IReadOnlyList<string> PresenceTokenIds { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_XboxLive_PresenceRichPresenceIds_ServiceConfigurationId"></a> ServiceConfigurationId

The SCID containing the presence strings.

```csharp
public string ServiceConfigurationId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

