# <a id="GDK_Net_PlayFab_Party_PartyDevicePropertiesChanged"></a> Class PartyDevicePropertiesChanged

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Properties shared on a device changed.

```csharp
public sealed record PartyDevicePropertiesChanged : PartyStateChange, IEquatable<PartyStateChange>, IEquatable<PartyDevicePropertiesChanged>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyStateChange](GDK.Net.PlayFab.Party.PartyStateChange.md) ← 
[PartyDevicePropertiesChanged](GDK.Net.PlayFab.Party.PartyDevicePropertiesChanged.md)

#### Implements

[IEquatable<PartyStateChange\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1), 
[IEquatable<PartyDevicePropertiesChanged\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[PartyStateChange.Kind](GDK.Net.PlayFab.Party.PartyStateChange.md\#GDK\_Net\_PlayFab\_Party\_PartyStateChange\_Kind), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyDevicePropertiesChanged__ctor_GDK_Net_PlayFab_Party_PartyDevice_System_Collections_Generic_IReadOnlyList_System_String__"></a> PartyDevicePropertiesChanged\(PartyDevice?, IReadOnlyList<string\>\)

Properties shared on a device changed.

```csharp
public PartyDevicePropertiesChanged(PartyDevice? Device, IReadOnlyList<string> Keys)
```

#### Parameters

`Device` [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

The device.

`Keys` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

The keys whose values changed.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyDevicePropertiesChanged_Device"></a> Device

The device.

```csharp
public PartyDevice? Device { get; init; }
```

#### Property Value

 [PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)?

### <a id="GDK_Net_PlayFab_Party_PartyDevicePropertiesChanged_Keys"></a> Keys

The keys whose values changed.

```csharp
public IReadOnlyList<string> Keys { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

