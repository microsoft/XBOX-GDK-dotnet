# <a id="GDK_Net_PlayFab_Party_PartyAudioDeviceSelection"></a> Struct PartyAudioDeviceSelection

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

The audio device a chat control captures from or renders to.

```csharp
public readonly record struct PartyAudioDeviceSelection : IEquatable<PartyAudioDeviceSelection>
```

#### Implements

[IEquatable<PartyAudioDeviceSelection\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_PlayFab_Party_PartyAudioDeviceSelection__ctor_GDK_Net_PlayFab_Party_PartyAudioDeviceSelectionType_System_String_System_String_"></a> PartyAudioDeviceSelection\(PartyAudioDeviceSelectionType, string?, string?\)

The audio device a chat control captures from or renders to.

```csharp
public PartyAudioDeviceSelection(PartyAudioDeviceSelectionType SelectionType, string? SelectionContext, string? DeviceId)
```

#### Parameters

`SelectionType` [PartyAudioDeviceSelectionType](GDK.Net.PlayFab.Party.PartyAudioDeviceSelectionType.md)

How the device was chosen.

`SelectionContext` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The platform-specific selection context, when one applies.

`DeviceId` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The resolved platform device id, when one is in use.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyAudioDeviceSelection_DeviceId"></a> DeviceId

The resolved platform device id, when one is in use.

```csharp
public string? DeviceId { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyAudioDeviceSelection_SelectionContext"></a> SelectionContext

The platform-specific selection context, when one applies.

```csharp
public string? SelectionContext { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_Party_PartyAudioDeviceSelection_SelectionType"></a> SelectionType

How the device was chosen.

```csharp
public PartyAudioDeviceSelectionType SelectionType { get; init; }
```

#### Property Value

 [PartyAudioDeviceSelectionType](GDK.Net.PlayFab.Party.PartyAudioDeviceSelectionType.md)

