# <a id="GDK_Net_PlayFab_Party_PartySynchronizeMessagesBetweenEndpointsCompleted"></a> Class PartySynchronizeMessagesBetweenEndpointsCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Party.PartyManager.SynchronizeMessagesBetweenEndpoints(System.Collections.Generic.IReadOnlyList%7bGDK.Net.PlayFab.Party.PartyEndpoint%7d%2cGDK.Net.PlayFab.Party.PartySynchronizeMessagesBetweenEndpointsOptions)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public sealed record PartySynchronizeMessagesBetweenEndpointsCompleted : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartySynchronizeMessagesBetweenEndpointsCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartySynchronizeMessagesBetweenEndpointsCompleted](GDK.Net.PlayFab.Party.PartySynchronizeMessagesBetweenEndpointsCompleted.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartySynchronizeMessagesBetweenEndpointsCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartySynchronizeMessagesBetweenEndpointsCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyEndpoint__GDK_Net_PlayFab_Party_PartySynchronizeMessagesBetweenEndpointsOptions_"></a> PartySynchronizeMessagesBetweenEndpointsCompleted\(PartyOperationId, IReadOnlyList<PartyEndpoint\>, PartySynchronizeMessagesBetweenEndpointsOptions\)

A <xref href="GDK.Net.PlayFab.Party.PartyManager.SynchronizeMessagesBetweenEndpoints(System.Collections.Generic.IReadOnlyList%7bGDK.Net.PlayFab.Party.PartyEndpoint%7d%2cGDK.Net.PlayFab.Party.PartySynchronizeMessagesBetweenEndpointsOptions)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public PartySynchronizeMessagesBetweenEndpointsCompleted(PartyOperationId Operation, IReadOnlyList<PartyEndpoint> Endpoints, PartySynchronizeMessagesBetweenEndpointsOptions Options)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Endpoints` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

The endpoints that were synchronized.

`Options` [PartySynchronizeMessagesBetweenEndpointsOptions](GDK.Net.PlayFab.Party.PartySynchronizeMessagesBetweenEndpointsOptions.md)

The synchronization options that were requested.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartySynchronizeMessagesBetweenEndpointsCompleted_Endpoints"></a> Endpoints

The endpoints that were synchronized.

```csharp
public IReadOnlyList<PartyEndpoint> Endpoints { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

### <a id="GDK_Net_PlayFab_Party_PartySynchronizeMessagesBetweenEndpointsCompleted_Operation"></a> Operation

The id returned by the start call.

```csharp
public PartyOperationId Operation { get; init; }
```

#### Property Value

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartySynchronizeMessagesBetweenEndpointsCompleted_Options"></a> Options

The synchronization options that were requested.

```csharp
public PartySynchronizeMessagesBetweenEndpointsOptions Options { get; init; }
```

#### Property Value

 [PartySynchronizeMessagesBetweenEndpointsOptions](GDK.Net.PlayFab.Party.PartySynchronizeMessagesBetweenEndpointsOptions.md)

