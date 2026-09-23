# <a id="GDK_Net_PlayFab_Party_PartyCreateEndpointCompleted"></a> Class PartyCreateEndpointCompleted

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A <xref href="GDK.Net.PlayFab.Party.PartyNetwork.CreateEndpoint(GDK.Net.PlayFab.Party.PartyLocalUser%2cSystem.Collections.Generic.IReadOnlyDictionary%7bSystem.String%2cSystem.Byte%5b%5d%7d)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public sealed record PartyCreateEndpointCompleted : PartyOperationCompleted, IEquatable<PartyStateChange>, IEquatable<PartyOperationCompleted>, IEquatable<PartyCreateEndpointCompleted>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyOperationCompleted](GDK.Net.PlayFab.Party.PartyOperationCompleted.md) ← 
[PartyCreateEndpointCompleted](GDK.Net.PlayFab.Party.PartyCreateEndpointCompleted.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyOperationCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyCreateEndpointCompleted\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyOperationCompleted.Operation](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Operation), 
[PartyOperationCompleted.Result](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Result), 
[PartyOperationCompleted.ErrorDetail](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_ErrorDetail), 
[PartyOperationCompleted.Succeeded](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Succeeded), 
[PartyOperationCompleted.Error](GDK.Net.PlayFab.Party.PartyOperationCompleted.md\#GDK\_Net\_PlayFab\_Party\_PartyOperationCompleted\_Error), 
[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyCreateEndpointCompleted__ctor_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyStateChangeResult_System_UInt32_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyLocalUser_GDK_Net_PlayFab_Party_PartyEndpoint_"></a> PartyCreateEndpointCompleted\(PartyOperationId, PartyStateChangeResult, uint, PartyNetwork?, PartyLocalUser?, PartyEndpoint?\)

A <xref href="GDK.Net.PlayFab.Party.PartyNetwork.CreateEndpoint(GDK.Net.PlayFab.Party.PartyLocalUser%2cSystem.Collections.Generic.IReadOnlyDictionary%7bSystem.String%2cSystem.Byte%5b%5d%7d)" data-throw-if-not-resolved="false"></xref> call completed.

```csharp
public PartyCreateEndpointCompleted(PartyOperationId Operation, PartyStateChangeResult Result, uint ErrorDetail, PartyNetwork? Network, PartyLocalUser? LocalUser, PartyEndpoint? Endpoint)
```

#### Parameters

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by the start call.

`Result` [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

Whether the operation succeeded.

`ErrorDetail` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The <code>PartyError</code> detail when it failed.

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network the endpoint was created in.

`LocalUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

The user that owns the endpoint, if any.

`Endpoint` [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

The created endpoint, when the call succeeded.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyCreateEndpointCompleted_Endpoint"></a> Endpoint

The created endpoint, when the call succeeded.

```csharp
public PartyEndpoint? Endpoint { get; init; }
```

#### Property Value

 [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

### <a id="GDK_Net_PlayFab_Party_PartyCreateEndpointCompleted_LocalUser"></a> LocalUser

The user that owns the endpoint, if any.

```csharp
public PartyLocalUser? LocalUser { get; init; }
```

#### Property Value

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyCreateEndpointCompleted_Network"></a> Network

The network the endpoint was created in.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

