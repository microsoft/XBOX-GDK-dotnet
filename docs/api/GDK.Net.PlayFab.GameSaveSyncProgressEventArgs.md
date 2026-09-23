# <a id="GDK_Net_PlayFab_GameSaveSyncProgressEventArgs"></a> Class GameSaveSyncProgressEventArgs

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Raised while a save is syncing so the title can show progress
(<code>PFGameSaveFilesUiProgressCallback</code>).

```csharp
public sealed class GameSaveSyncProgressEventArgs : GameSaveUserActionEventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[GameSaveEventArgs](GDK.Net.PlayFab.GameSaveEventArgs.md) ← 
[GameSaveUserActionEventArgs](GDK.Net.PlayFab.GameSaveUserActionEventArgs.md) ← 
[GameSaveSyncProgressEventArgs](GDK.Net.PlayFab.GameSaveSyncProgressEventArgs.md)

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

### <a id="GDK_Net_PlayFab_GameSaveSyncProgressEventArgs_State"></a> State

The stage the sync has reached.

```csharp
public GameSaveFilesSyncState State { get; }
```

#### Property Value

 [GameSaveFilesSyncState](GDK.Net.PlayFab.GameSaveFilesSyncState.md)

## Methods

### <a id="GDK_Net_PlayFab_GameSaveSyncProgressEventArgs_GetProgress"></a> GetProgress\(\)

Reads the current byte counts for this sync (<code>PFGameSaveFilesUiProgressGetProgress</code>).

```csharp
public GameSaveSyncProgress GetProgress()
```

#### Returns

 [GameSaveSyncProgress](GDK.Net.PlayFab.GameSaveSyncProgress.md)

### <a id="GDK_Net_PlayFab_GameSaveSyncProgressEventArgs_Respond_GDK_Net_PlayFab_GameSaveFilesUiProgressUserAction_"></a> Respond\(GameSaveFilesUiProgressUserAction\)

Dismisses the progress dialog with the user's choice
(<code>PFGameSaveFilesSetUiProgressResponse</code>).

```csharp
public void Respond(GameSaveFilesUiProgressUserAction action)
```

#### Parameters

`action` [GameSaveFilesUiProgressUserAction](GDK.Net.PlayFab.GameSaveFilesUiProgressUserAction.md)

