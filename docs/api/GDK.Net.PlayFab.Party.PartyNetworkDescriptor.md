# <a id="GDK_Net_PlayFab_Party_PartyNetworkDescriptor"></a> Class PartyNetworkDescriptor

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_NETWORK_DESCRIPTOR</code>: the opaque token that identifies a network and
carries enough information for another device to connect to it.

```csharp
public sealed class PartyNetworkDescriptor
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyNetworkDescriptor](GDK.Net.PlayFab.Party.PartyNetworkDescriptor.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The payload is deliberately opaque. Use <xref href="GDK.Net.PlayFab.Party.PartyNetworkDescriptor.Serialize" data-throw-if-not-resolved="false"></xref> to obtain the string form the
title distributes through its own invite channel, and <xref href="GDK.Net.PlayFab.Party.PartyNetworkDescriptor.Deserialize(System.String)" data-throw-if-not-resolved="false"></xref> to recover it.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyNetworkDescriptor_NetworkIdentifier"></a> NetworkIdentifier

The network's unique identifier.

```csharp
public string NetworkIdentifier { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Party_PartyNetworkDescriptor_RegionName"></a> RegionName

The Azure region hosting the network.

```csharp
public string RegionName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyNetworkDescriptor_Deserialize_System_String_"></a> Deserialize\(string\)

Recovers a descriptor from the string produced by <xref href="GDK.Net.PlayFab.Party.PartyNetworkDescriptor.Serialize" data-throw-if-not-resolved="false"></xref>.

```csharp
public static PartyNetworkDescriptor Deserialize(string serialized)
```

#### Parameters

`serialized` [string](https://learn.microsoft.com/dotnet/api/system.string)

The serialized descriptor.

#### Returns

 [PartyNetworkDescriptor](GDK.Net.PlayFab.Party.PartyNetworkDescriptor.md)

### <a id="GDK_Net_PlayFab_Party_PartyNetworkDescriptor_Serialize"></a> Serialize\(\)

Serializes the descriptor to the string form a title distributes to joiners.

```csharp
public string Serialize()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

