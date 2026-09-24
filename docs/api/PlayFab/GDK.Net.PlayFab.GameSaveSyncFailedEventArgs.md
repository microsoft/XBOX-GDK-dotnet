# <a id="GDK_Net_PlayFab_GameSaveSyncFailedEventArgs"></a> Class GameSaveSyncFailedEventArgs

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Raised when a sync failed and the title must ask the user how to continue
(<code>PFGameSaveFilesUiSyncFailedCallback</code>).

```csharp
public sealed class GameSaveSyncFailedEventArgs : GameSaveUserActionEventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[GameSaveEventArgs](GDK.Net.PlayFab.GameSaveEventArgs.md) ← 
[GameSaveUserActionEventArgs](GDK.Net.PlayFab.GameSaveUserActionEventArgs.md) ← 
[GameSaveSyncFailedEventArgs](GDK.Net.PlayFab.GameSaveSyncFailedEventArgs.md)

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

### <a id="GDK_Net_PlayFab_GameSaveSyncFailedEventArgs_Error"></a> Error

Why the sync failed.

```csharp
public Exception Error { get; }
```

#### Property Value

 [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

### <a id="GDK_Net_PlayFab_GameSaveSyncFailedEventArgs_State"></a> State

The stage the sync failed in.

```csharp
public GameSaveFilesSyncState State { get; }
```

#### Property Value

 [GameSaveFilesSyncState](GDK.Net.PlayFab.GameSaveFilesSyncState.md)

## Methods

### <a id="GDK_Net_PlayFab_GameSaveSyncFailedEventArgs_Respond_GDK_Net_PlayFab_GameSaveFilesUiSyncFailedUserAction_"></a> Respond\(GameSaveFilesUiSyncFailedUserAction\)

Dismisses the dialog with the user's choice
(<code>PFGameSaveFilesSetUiSyncFailedResponse</code>).

```csharp
public void Respond(GameSaveFilesUiSyncFailedUserAction action)
```

#### Parameters

`action` [GameSaveFilesUiSyncFailedUserAction](GDK.Net.PlayFab.GameSaveFilesUiSyncFailedUserAction.md)

