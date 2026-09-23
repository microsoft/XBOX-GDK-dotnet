# <a id="GDK_Net_PlayFab_Party_PartyEndpointMessageReceived"></a> Class PartyEndpointMessageReceived

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

A message arrived from a remote endpoint.

```csharp
public sealed record PartyEndpointMessageReceived : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyEndpointMessageReceived>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyEndpointMessageReceived](GDK.Net.PlayFab.Party.PartyEndpointMessageReceived.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyEndpointMessageReceived\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyEndpointMessageReceived__ctor_GDK_Net_PlayFab_Party_PartyNetwork_GDK_Net_PlayFab_Party_PartyEndpoint_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyEndpoint__GDK_Net_PlayFab_Party_PartyMessageReceivedOptions_System_Byte___"></a> PartyEndpointMessageReceived\(PartyNetwork?, PartyEndpoint?, IReadOnlyList<PartyEndpoint\>, PartyMessageReceivedOptions, byte\[\]\)

A message arrived from a remote endpoint.

```csharp
public PartyEndpointMessageReceived(PartyNetwork? Network, PartyEndpoint? SenderEndpoint, IReadOnlyList<PartyEndpoint> ReceiverEndpoints, PartyMessageReceivedOptions Options, byte[] Message)
```

#### Parameters

`Network` [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

The network the message arrived on.

`SenderEndpoint` [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

The endpoint that sent it.

`ReceiverEndpoints` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

The local endpoints the message was addressed to.

`Options` [PartyMessageReceivedOptions](GDK.Net.PlayFab.Party.PartyMessageReceivedOptions.md)

Delivery details for the message.

`Message` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

The payload, copied into managed memory.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyEndpointMessageReceived_Message"></a> Message

The payload, copied into managed memory.

```csharp
public byte[] Message { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="GDK_Net_PlayFab_Party_PartyEndpointMessageReceived_Network"></a> Network

The network the message arrived on.

```csharp
public PartyNetwork? Network { get; init; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)?

### <a id="GDK_Net_PlayFab_Party_PartyEndpointMessageReceived_Options"></a> Options

Delivery details for the message.

```csharp
public PartyMessageReceivedOptions Options { get; init; }
```

#### Property Value

 [PartyMessageReceivedOptions](GDK.Net.PlayFab.Party.PartyMessageReceivedOptions.md)

### <a id="GDK_Net_PlayFab_Party_PartyEndpointMessageReceived_ReceiverEndpoints"></a> ReceiverEndpoints

The local endpoints the message was addressed to.

```csharp
public IReadOnlyList<PartyEndpoint> ReceiverEndpoints { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyEndpointMessageReceived_SenderEndpoint"></a> SenderEndpoint

The endpoint that sent it.

```csharp
public PartyEndpoint? SenderEndpoint { get; init; }
```

#### Property Value

 [PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)?

