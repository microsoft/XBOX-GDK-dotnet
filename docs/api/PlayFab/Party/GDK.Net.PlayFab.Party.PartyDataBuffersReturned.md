# <a id="GDK_Net_PlayFab_Party_PartyDataBuffersReturned"></a> Class PartyDataBuffersReturned

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Party finished with the buffers submitted by a send.

```csharp
public sealed record PartyDataBuffersReturned : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyDataBuffersReturned>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyDataBuffersReturned](GDK.Net.PlayFab.Party.PartyDataBuffersReturned.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyDataBuffersReturned\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyDataBuffersReturned__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyEndpoint_GDK_Net_PlayFab_Party_PartyOperationId_"></a> PartyDataBuffersReturned\(PartyNetwork?, PartyEndpoint?, PartyOperationId\)

Party finished with the buffers submitted by a send.

```csharp
public PartyDataBuffersReturned(PartyNetwork? Network, PartyEndpoint? LocalSenderEndpoint, PartyOperationId Operation)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network.

`LocalSenderEndpoint` [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

The endpoint that sent the message.

`Operation` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The id returned by <xref href="GDK.Net.PlayFab.Party.PartyEndpoint.SendMessage(System.Collections.Generic.IReadOnlyList%7bGDK.Net.PlayFab.Party.PartyEndpoint%7d%2cSystem.Byte%5b%5d%2cGDK.Net.PlayFab.Party.PartySendMessageOptions%2cGDK.Net.PlayFab.Party.PartySendMessageQueuingConfiguration)" data-throw-if-not-resolved="false"></xref>.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyDataBuffersReturned_LocalSenderEndpoint"></a> LocalSenderEndpoint

The endpoint that sent the message.

```csharp
public PartyEndpoint? LocalSenderEndpoint { get; init; }
```

#### Property Value

 [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

### <a id="GDK_Net_PlayFab_Party_PartyDataBuffersReturned_Network"></a> Network

The network.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

### <a id="GDK_Net_PlayFab_Party_PartyDataBuffersReturned_Operation"></a> Operation

The id returned by <xref href="GDK.Net.PlayFab.Party.PartyEndpoint.SendMessage(System.Collections.Generic.IReadOnlyList%7bGDK.Net.PlayFab.Party.PartyEndpoint%7d%2cSystem.Byte%5b%5d%2cGDK.Net.PlayFab.Party.PartySendMessageOptions%2cGDK.Net.PlayFab.Party.PartySendMessageQueuingConfiguration)" data-throw-if-not-resolved="false"></xref>.

```csharp
public PartyOperationId Operation { get; init; }
```

#### Property Value

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

