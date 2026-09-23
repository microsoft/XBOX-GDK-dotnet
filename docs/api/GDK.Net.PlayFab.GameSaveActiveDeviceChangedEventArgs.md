# <a id="GDK_Net_PlayFab_GameSaveActiveDeviceChangedEventArgs"></a> Class GameSaveActiveDeviceChangedEventArgs

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Raised when another device took over as the active device for a user's save, which means the
title should return to its main menu (<code>PFGameSaveFilesActiveDeviceChangedCallback</code>).

```csharp
public sealed class GameSaveActiveDeviceChangedEventArgs : GameSaveEventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[GameSaveEventArgs](GDK.Net.PlayFab.GameSaveEventArgs.md) ← 
[GameSaveActiveDeviceChangedEventArgs](GDK.Net.PlayFab.GameSaveActiveDeviceChangedEventArgs.md)

#### Inherited Members

[GameSaveEventArgs.IsFor\(PlayFabLocalUser\)](GDK.Net.PlayFab.GameSaveEventArgs.md\#GDK\_Net\_PlayFab\_GameSaveEventArgs\_IsFor\_GDK\_Net\_PlayFab\_PlayFabLocalUser\_), 
[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_GameSaveActiveDeviceChangedEventArgs_ActiveDevice"></a> ActiveDevice

The save now owned by the new active device, when the runtime supplied one.

```csharp
public GameSaveDescriptor? ActiveDevice { get; }
```

#### Property Value

 [GameSaveDescriptor](GDK.Net.PlayFab.GameSaveDescriptor.md)?

