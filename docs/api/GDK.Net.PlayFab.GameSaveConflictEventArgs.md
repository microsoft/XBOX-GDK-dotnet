# <a id="GDK_Net_PlayFab_GameSaveConflictEventArgs"></a> Class GameSaveConflictEventArgs

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Raised when the local and cloud saves diverged and the user must pick one
(<code>PFGameSaveFilesUiConflictCallback</code>).

```csharp
public sealed class GameSaveConflictEventArgs : GameSaveUserActionEventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[GameSaveEventArgs](GDK.Net.PlayFab.GameSaveEventArgs.md) ← 
[GameSaveUserActionEventArgs](GDK.Net.PlayFab.GameSaveUserActionEventArgs.md) ← 
[GameSaveConflictEventArgs](GDK.Net.PlayFab.GameSaveConflictEventArgs.md)

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

### <a id="GDK_Net_PlayFab_GameSaveConflictEventArgs_LocalGameSave"></a> LocalGameSave

The save on this device.

```csharp
public GameSaveDescriptor? LocalGameSave { get; }
```

#### Property Value

 [GameSaveDescriptor](GDK.Net.PlayFab.GameSaveDescriptor.md)?

### <a id="GDK_Net_PlayFab_GameSaveConflictEventArgs_RemoteGameSave"></a> RemoteGameSave

The save in the cloud.

```csharp
public GameSaveDescriptor? RemoteGameSave { get; }
```

#### Property Value

 [GameSaveDescriptor](GDK.Net.PlayFab.GameSaveDescriptor.md)?

## Methods

### <a id="GDK_Net_PlayFab_GameSaveConflictEventArgs_Respond_GDK_Net_PlayFab_GameSaveFilesUiConflictUserAction_"></a> Respond\(GameSaveFilesUiConflictUserAction\)

Dismisses the dialog with the user's choice
(<code>PFGameSaveFilesSetUiConflictResponse</code>).

```csharp
public void Respond(GameSaveFilesUiConflictUserAction action)
```

#### Parameters

`action` [GameSaveFilesUiConflictUserAction](GDK.Net.PlayFab.GameSaveFilesUiConflictUserAction.md)

