# <a id="GDK_Net_GameSave_GameSaveSyncState"></a> Enum GameSaveSyncState

Namespace: [GDK.Net.GameSave](GDK.Net.GameSave.md)  
Assembly: GDK.Net.dll  

Current cloud synchronisation state of the game-save provider. Mirrors <code>XGameSaveSyncState</code>.

```csharp
public enum GameSaveSyncState : uint
```

## Fields

`Downloading = 2` 

Actively downloading cloud data.



`NotStarted = 0` 

Synchronisation has not started.



`PreparingForDownload = 1` 

Preparing the download phase.



`PreparingForUpload = 3` 

Preparing the upload phase.



`SyncComplete = 5` 

Synchronisation completed successfully.



`Uploading = 4` 

Actively uploading local data to the cloud.



