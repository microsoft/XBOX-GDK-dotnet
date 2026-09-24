# <a id="GDK_Net_PlayFab_Party_PartyEndpoint"></a> Class PartyEndpoint

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_ENDPOINT_HANDLE</code>: a messaging endpoint inside a network.

```csharp
public sealed class PartyEndpoint : PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md) ← 
[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)

#### Inherited Members

[PartyObject.IsValid](GDK.Net.PlayFab.Party.PartyObject.md\#GDK\_Net\_PlayFab\_Party\_PartyObject\_IsValid), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_Device"></a> Device

The device hosting the endpoint.

```csharp
public PartyDevice Device { get; }
```

#### Property Value

 [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_EntityId"></a> EntityId

The PlayFab entity id behind the endpoint, if it is user-owned.

```csharp
public string? EntityId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_EntityType"></a> EntityType

The PlayFab entity type behind the endpoint, if it is user-owned.

```csharp
public string? EntityType { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_IsLocal"></a> IsLocal

Whether the endpoint belongs to the local device.

```csharp
public bool IsLocal { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_LocalUser"></a> LocalUser

The local user that owns the endpoint, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for a device endpoint.

```csharp
public PartyLocalUser? LocalUser { get; }
```

#### Property Value

 [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)?

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_Network"></a> Network

The network the endpoint belongs to.

```csharp
public PartyNetwork Network { get; }
```

#### Property Value

 [PartyNetwork](GDK.Net.PlayFab.Party.PartyNetwork.md)

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_UniqueIdentifier"></a> UniqueIdentifier

The endpoint's per-network unique identifier.

```csharp
public ushort UniqueIdentifier { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_CancelMessages_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyEndpoint__GDK_Net_PlayFab_Party_PartyCancelMessagesFilterExpression_System_UInt32_System_UInt32_"></a> CancelMessages\(IReadOnlyList<PartyEndpoint\>, PartyCancelMessagesFilterExpression, uint, uint\)

Cancels queued messages that match the supplied filter.

```csharp
public uint CancelMessages(IReadOnlyList<PartyEndpoint> targets, PartyCancelMessagesFilterExpression filter, uint messageIdentityFilterMask, uint filteredMessageIdentitiesToMatch)
```

#### Parameters

`targets` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

The targets to filter on, or an empty list for all targets.

`filter` [PartyCancelMessagesFilterExpression](GDK.Net.PlayFab.Party.PartyCancelMessagesFilterExpression.md)

How the identity mask is interpreted.

`messageIdentityFilterMask` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The bits of the identity that participate.

`filteredMessageIdentitiesToMatch` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The masked identity value to match.

#### Returns

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The number of messages that were cancelled.

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_FlushMessages_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyEndpoint__"></a> FlushMessages\(IReadOnlyList<PartyEndpoint\>\)

Requests that queued messages to the given targets be sent immediately.

```csharp
public void FlushMessages(IReadOnlyList<PartyEndpoint> targets)
```

#### Parameters

`targets` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

The targets to flush, or an empty list for all targets.

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_GetSharedProperty_System_String_"></a> GetSharedProperty\(string\)

Reads a shared property, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it is not set.

```csharp
public byte[]? GetSharedProperty(string key)
```

#### Parameters

`key` [string](https://learn.microsoft.com/dotnet/api/system.string)

The property key.

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_GetSharedPropertyKeys"></a> GetSharedPropertyKeys\(\)

The keys of every property shared on the endpoint.

```csharp
public IReadOnlyList<string> GetSharedPropertyKeys()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_GetStatistics_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyEndpoint__System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyEndpointStatistic__"></a> GetStatistics\(IReadOnlyList<PartyEndpoint\>, IReadOnlyList<PartyEndpointStatistic\>\)

Reads per-endpoint queuing statistics.

```csharp
public IReadOnlyDictionary<PartyEndpointStatistic, ulong> GetStatistics(IReadOnlyList<PartyEndpoint> targets, IReadOnlyList<PartyEndpointStatistic> statistics)
```

#### Parameters

`targets` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

The targets to aggregate over, or an empty list for all targets.

`statistics` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpointStatistic](GDK.Net.PlayFab.Party.PartyEndpointStatistic.md)\>

The statistics to read.

#### Returns

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[PartyEndpointStatistic](GDK.Net.PlayFab.Party.PartyEndpointStatistic.md), [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_SendMessage_System_Collections_Generic_IReadOnlyList_GDK_Net_PlayFab_Party_PartyEndpoint__System_Byte___GDK_Net_PlayFab_Party_PartySendMessageOptions_GDK_Net_PlayFab_Party_PartySendMessageQueuingConfiguration_"></a> SendMessage\(IReadOnlyList<PartyEndpoint\>, byte\[\], PartySendMessageOptions, PartySendMessageQueuingConfiguration?\)

Queues a message from this local endpoint to the given targets.

```csharp
public PartyOperationId SendMessage(IReadOnlyList<PartyEndpoint> targets, byte[] message, PartySendMessageOptions options = PartySendMessageOptions.BestEffortDelivery, PartySendMessageQueuingConfiguration? queuing = null)
```

#### Parameters

`targets` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyEndpoint](GDK.Net.PlayFab.Party.PartyEndpoint.md)\>

The receiving endpoints, or an empty list to broadcast to every endpoint in the network.

`message` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

The message payload.

`options` [PartySendMessageOptions](GDK.Net.PlayFab.Party.PartySendMessageOptions.md)

Delivery options.

`queuing` [PartySendMessageQueuingConfiguration](GDK.Net.PlayFab.Party.PartySendMessageQueuingConfiguration.md)?

Optional priority and timeout for the queued message.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

The identity used to correlate the later <xref href="GDK.Net.PlayFab.Party.PartyDataBuffersReturned" data-throw-if-not-resolved="false"></xref> state change.

### <a id="GDK_Net_PlayFab_Party_PartyEndpoint_SetSharedProperties_System_Collections_Generic_IReadOnlyDictionary_System_String_System_Byte____"></a> SetSharedProperties\(IReadOnlyDictionary<string, byte\[\]?\>\)

Sets or, for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> value, removes shared properties.

```csharp
public void SetSharedProperties(IReadOnlyDictionary<string, byte[]?> properties)
```

#### Parameters

`properties` [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?\>

The properties to write.

