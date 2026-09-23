# <a id="GDK_Net_PlayFab_Party_PartyDevice"></a> Class PartyDevice

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_DEVICE_HANDLE</code>: a physical device participating in one or more networks.

```csharp
public sealed class PartyDevice : PartyObject
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyObject](GDK.Net.PlayFab.Party.PartyObject.md) ← 
[PartyDevice](GDK.Net.PlayFab.Party.PartyDevice.md)

#### Inherited Members

[PartyObject.IsValid](GDK.Net.PlayFab.Party.PartyObject.md\#GDK\_Net\_PlayFab\_Party\_PartyObject\_IsValid), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyDevice_ChatControls"></a> ChatControls

The chat controls hosted on the device.

```csharp
public IReadOnlyList<PartyChatControl> ChatControls { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)\>

### <a id="GDK_Net_PlayFab_Party_PartyDevice_IsLocal"></a> IsLocal

Whether this is the local device.

```csharp
public bool IsLocal { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyDevice_CreateChatControl_GDK_Net_PlayFab_Party_PartyLocalUser_System_String_"></a> CreateChatControl\(PartyLocalUser, string?\)

Starts creating a chat control for a local user on this (local) device.

```csharp
public PartyOperationId CreateChatControl(PartyLocalUser localUser, string? languageCode = null)
```

#### Parameters

`localUser` [PartyLocalUser](GDK.Net.PlayFab.Party.PartyLocalUser.md)

The local user the chat control speaks for.

`languageCode` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The BCP-47 language code for transcription and translation, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for
the platform default.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyDevice_DestroyChatControl_GDK_Net_PlayFab_Party_PartyChatControl_"></a> DestroyChatControl\(PartyChatControl\)

Starts destroying a chat control hosted on this device.

```csharp
public PartyOperationId DestroyChatControl(PartyChatControl chatControl)
```

#### Parameters

`chatControl` [PartyChatControl](GDK.Net.PlayFab.Party.PartyChatControl.md)

The chat control to destroy.

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

### <a id="GDK_Net_PlayFab_Party_PartyDevice_GetSharedProperty_System_String_"></a> GetSharedProperty\(string\)

Reads a shared property, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it is not set.

```csharp
public byte[]? GetSharedProperty(string key)
```

#### Parameters

`key` [string](https://learn.microsoft.com/dotnet/api/system.string)

The property key.

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="GDK_Net_PlayFab_Party_PartyDevice_GetSharedPropertyKeys"></a> GetSharedPropertyKeys\(\)

The keys of every property shared on the device.

```csharp
public IReadOnlyList<string> GetSharedPropertyKeys()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_PlayFab_Party_PartyDevice_SetSharedProperties_System_Collections_Generic_IReadOnlyDictionary_System_String_System_Byte____"></a> SetSharedProperties\(IReadOnlyDictionary<string, byte\[\]?\>\)

Sets or, for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> value, removes shared properties.

```csharp
public void SetSharedProperties(IReadOnlyDictionary<string, byte[]?> properties)
```

#### Parameters

`properties` [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?\>

The properties to write.

