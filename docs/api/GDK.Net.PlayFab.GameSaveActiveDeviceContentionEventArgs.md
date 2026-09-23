# <a id="GDK_Net_PlayFab_GameSaveActiveDeviceContentionEventArgs"></a> Class GameSaveActiveDeviceContentionEventArgs

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Raised when another device is already the active device for this save
(<code>PFGameSaveFilesUiActiveDeviceContentionCallback</code>).

```csharp
public sealed class GameSaveActiveDeviceContentionEventArgs : GameSaveUserActionEventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[GameSaveEventArgs](GDK.Net.PlayFab.GameSaveEventArgs.md) ← 
[GameSaveUserActionEventArgs](GDK.Net.PlayFab.GameSaveUserActionEventArgs.md) ← 
[GameSaveActiveDeviceContentionEventArgs](GDK.Net.PlayFab.GameSaveActiveDeviceContentionEventArgs.md)

#### Inherited Members

[GameSaveUserActionEventArgs.HasResponded](GDK.Net.PlayFab.GameSaveUserActionEventArgs.md\#GDK\_Net\_PlayFab\_GameSaveUserActionEventArgs\_HasResponded), 
[GameSaveEventArgs.IsFor\(PlayFabLocalUser\)](GDK.Net.PlayFab.GameSaveEventArgs.md\#GDK\_Net\_PlayFab\_GameSaveEventArgs\_IsFor\_GDK\_Net\_PlayFab\_PlayFabLocalUser\_), 
[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_GameSaveActiveDeviceContentionEventArgs_LocalGameSave"></a> LocalGameSave

The save on this device.

```csharp
public GameSaveDescriptor? LocalGameSave { get; }
```

#### Property Value

 [GameSaveDescriptor](GDK.Net.PlayFab.GameSaveDescriptor.md)?

### <a id="GDK_Net_PlayFab_GameSaveActiveDeviceContentionEventArgs_RemoteGameSave"></a> RemoteGameSave

The save on the device that currently owns the slot.

```csharp
public GameSaveDescriptor? RemoteGameSave { get; }
```

#### Property Value

 [GameSaveDescriptor](GDK.Net.PlayFab.GameSaveDescriptor.md)?

## Methods

### <a id="GDK_Net_PlayFab_GameSaveActiveDeviceContentionEventArgs_Respond_GDK_Net_PlayFab_GameSaveFilesUiActiveDeviceContentionUserAction_"></a> Respond\(GameSaveFilesUiActiveDeviceContentionUserAction\)

Dismisses the dialog with the user's choice
(<code>PFGameSaveFilesSetUiActiveDeviceContentionResponse</code>).

```csharp
public void Respond(GameSaveFilesUiActiveDeviceContentionUserAction action)
```

#### Parameters

`action` [GameSaveFilesUiActiveDeviceContentionUserAction](GDK.Net.PlayFab.GameSaveFilesUiActiveDeviceContentionUserAction.md)

