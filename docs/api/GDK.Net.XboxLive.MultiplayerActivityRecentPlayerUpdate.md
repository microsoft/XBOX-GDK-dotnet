# <a id="GDK_Net_XboxLive_MultiplayerActivityRecentPlayerUpdate"></a> Class MultiplayerActivityRecentPlayerUpdate

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

One recent-player encounter to append to the local user's recent-player list. Managed
equivalent of <code>XblMultiplayerActivityRecentPlayerUpdate</code>.

```csharp
public sealed class MultiplayerActivityRecentPlayerUpdate
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MultiplayerActivityRecentPlayerUpdate](GDK.Net.XboxLive.MultiplayerActivityRecentPlayerUpdate.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Recent-player data is privacy-sensitive social data. Report only real multiplayer encounters
for the signed-in user, with the least specific <xref href="GDK.Net.XboxLive.MultiplayerActivityRecentPlayerUpdate.EncounterType" data-throw-if-not-resolved="false"></xref> that satisfies the
title's scenario.

## Constructors

### <a id="GDK_Net_XboxLive_MultiplayerActivityRecentPlayerUpdate__ctor_System_UInt64_GDK_Net_XboxLive_MultiplayerActivityEncounterType_"></a> MultiplayerActivityRecentPlayerUpdate\(ulong, MultiplayerActivityEncounterType\)

Creates a recent-player update.

```csharp
public MultiplayerActivityRecentPlayerUpdate(ulong xboxUserId, MultiplayerActivityEncounterType encounterType = MultiplayerActivityEncounterType.Default)
```

#### Parameters

`xboxUserId` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Xbox user id of the encountered player.

`encounterType` [MultiplayerActivityEncounterType](GDK.Net.XboxLive.MultiplayerActivityEncounterType.md)

Type of encounter.

## Properties

### <a id="GDK_Net_XboxLive_MultiplayerActivityRecentPlayerUpdate_EncounterType"></a> EncounterType

Type of encounter.

```csharp
public MultiplayerActivityEncounterType EncounterType { get; }
```

#### Property Value

 [MultiplayerActivityEncounterType](GDK.Net.XboxLive.MultiplayerActivityEncounterType.md)

### <a id="GDK_Net_XboxLive_MultiplayerActivityRecentPlayerUpdate_XboxUserId"></a> XboxUserId

Xbox user id of the encountered player.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_MultiplayerActivityRecentPlayerUpdate_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

