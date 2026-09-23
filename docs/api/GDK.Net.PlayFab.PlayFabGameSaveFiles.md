# <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles"></a> Class PlayFabGameSaveFiles

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

PlayFab Game Save Files (<code>PFGameSaveFiles.h</code>, <code>PFGameSaveFilesUi.h</code>): whole-folder
save synchronization between the device and PlayFab, driven by
<xref href="GDK.Net.PlayFab.PlayFabLocalUser" data-throw-if-not-resolved="false"></xref>.

```csharp
public static class PlayFabGameSaveFiles
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabGameSaveFiles](GDK.Net.PlayFab.PlayFabGameSaveFiles.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>
The library is process-wide, so this is a static class. <xref href="GDK.Net.PlayFab.PlayFabGameSaveFiles.Initialize(System.String%2cGDK.Net.PlayFab.GameSaveInitOptions)" data-throw-if-not-resolved="false"></xref> must run after
<xref href="GDK.Net.PlayFab.PlayFabRuntime.Initialize" data-throw-if-not-resolved="false"></xref> and before any other member here.
</p>
<p>
The <code>...UiRequested</code> events replace the native UI callbacks. The sync stays blocked until
the handler calls <code>Respond</code> on the event arguments, so a title that subscribes must always
respond — cancelling is a valid response. Events raised without a subscriber are answered with
the cancel action automatically, which keeps an unhandled prompt from hanging the sync.
</p>

## Properties

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_IsInitialized"></a> IsInitialized

Whether <xref href="GDK.Net.PlayFab.PlayFabGameSaveFiles.Initialize(System.String%2cGDK.Net.PlayFab.GameSaveInitOptions)" data-throw-if-not-resolved="false"></xref> has run without a matching uninitialize.

```csharp
public static bool IsInitialized { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_AddUserAsync_GDK_Net_PlayFab_PlayFabLocalUser_GDK_Net_PlayFab_GameSaveFilesAddUserOptions_System_Threading_CancellationToken_"></a> AddUserAsync\(PlayFabLocalUser, GameSaveFilesAddUserOptions, CancellationToken\)

Adds a user to the game save system, showing the platform sync UI when needed
(<code>PFGameSaveFilesAddUserWithUiAsync</code>).

```csharp
public static Task AddUserAsync(PlayFabLocalUser user, GameSaveFilesAddUserOptions options = GameSaveFilesAddUserOptions.None, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

`options` [GameSaveFilesAddUserOptions](GDK.Net.PlayFab.GameSaveFilesAddUserOptions.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_GetProgress_GDK_Net_PlayFab_PlayFabLocalUser_"></a> GetProgress\(PlayFabLocalUser\)

Reads how far the user's sync has progressed
(<code>PFGameSaveFilesUiProgressGetProgress</code>).

```csharp
public static GameSaveSyncProgress GetProgress(PlayFabLocalUser user)
```

#### Parameters

`user` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

#### Returns

 [GameSaveSyncProgress](GDK.Net.PlayFab.GameSaveSyncProgress.md)

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_GetRemainingQuota_GDK_Net_PlayFab_PlayFabLocalUser_"></a> GetRemainingQuota\(PlayFabLocalUser\)

How many more bytes the user may upload before hitting their quota
(<code>PFGameSaveFilesGetRemainingQuota</code>).

```csharp
public static long GetRemainingQuota(PlayFabLocalUser user)
```

#### Parameters

`user` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_GetSaveFolder_GDK_Net_PlayFab_PlayFabLocalUser_"></a> GetSaveFolder\(PlayFabLocalUser\)

The folder the user's saves are stored in
(<code>PFGameSaveFilesGetFolderSize</code>, <code>PFGameSaveFilesGetFolder</code>).

```csharp
public static string GetSaveFolder(PlayFabLocalUser user)
```

#### Parameters

`user` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_Initialize_System_String_GDK_Net_PlayFab_GameSaveInitOptions_"></a> Initialize\(string?, GameSaveInitOptions\)

Initializes the game save library (<code>PFGameSaveFilesInitialize</code>).

```csharp
public static void Initialize(string? saveFolder = null, GameSaveInitOptions options = GameSaveInitOptions.None)
```

#### Parameters

`saveFolder` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The folder the title's saves live in, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to let the library pick the
platform default.

`options` [GameSaveInitOptions](GDK.Net.PlayFab.GameSaveInitOptions.md)

Reserved initialization flags.

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_IsConnectedToCloud_GDK_Net_PlayFab_PlayFabLocalUser_"></a> IsConnectedToCloud\(PlayFabLocalUser\)

Whether the user's saves are currently reaching the cloud
(<code>PFGameSaveFilesIsConnectedToCloud</code>).

```csharp
public static bool IsConnectedToCloud(PlayFabLocalUser user)
```

#### Parameters

`user` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_ResetCloudAsync_GDK_Net_PlayFab_PlayFabLocalUser_System_Threading_CancellationToken_"></a> ResetCloudAsync\(PlayFabLocalUser, CancellationToken\)

Deletes the user's cloud save and makes the local one authoritative
(<code>PFGameSaveFilesResetCloudAsync</code>).

```csharp
public static Task ResetCloudAsync(PlayFabLocalUser user, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_SetSaveDescriptionAsync_GDK_Net_PlayFab_PlayFabLocalUser_System_String_System_Threading_CancellationToken_"></a> SetSaveDescriptionAsync\(PlayFabLocalUser, string, CancellationToken\)

Sets the short description shown next to the user's save
(<code>PFGameSaveFilesSetSaveDescriptionAsync</code>).

```csharp
public static Task SetSaveDescriptionAsync(PlayFabLocalUser user, string description, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

`description` [string](https://learn.microsoft.com/dotnet/api/system.string)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_UninitializeAsync_System_Threading_CancellationToken_"></a> UninitializeAsync\(CancellationToken\)

Shuts the game save library down and waits for its background work to drain
(<code>PFGameSaveFilesUninitializeAsync</code>).

```csharp
public static Task UninitializeAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_UploadAsync_GDK_Net_PlayFab_PlayFabLocalUser_GDK_Net_PlayFab_GameSaveFilesUploadOption_System_Threading_CancellationToken_"></a> UploadAsync\(PlayFabLocalUser, GameSaveFilesUploadOption, CancellationToken\)

Uploads the user's save folder to the cloud, showing the platform sync UI when needed
(<code>PFGameSaveFilesUploadWithUiAsync</code>).

```csharp
public static Task UploadAsync(PlayFabLocalUser user, GameSaveFilesUploadOption option = GameSaveFilesUploadOption.KeepDeviceActive, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [PlayFabLocalUser](GDK.Net.PlayFab.PlayFabLocalUser.md)

The user whose save is uploaded.

`option` [GameSaveFilesUploadOption](GDK.Net.PlayFab.GameSaveFilesUploadOption.md)

Whether this device stays the active device once the upload completes.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the upload.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_ActiveDeviceChanged"></a> ActiveDeviceChanged

Raised when a user's save moved to another device, which means this title should return to
its main menu (<code>PFGameSaveFilesSetActiveDeviceChangedCallback</code>).

```csharp
public static event EventHandler<GameSaveActiveDeviceChangedEventArgs>? ActiveDeviceChanged
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[GameSaveActiveDeviceChangedEventArgs](GDK.Net.PlayFab.GameSaveActiveDeviceChangedEventArgs.md)\>?

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_ActiveDeviceContentionUiRequested"></a> ActiveDeviceContentionUiRequested

Raised when another device already owns the user's save.

```csharp
public static event EventHandler<GameSaveActiveDeviceContentionEventArgs>? ActiveDeviceContentionUiRequested
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[GameSaveActiveDeviceContentionEventArgs](GDK.Net.PlayFab.GameSaveActiveDeviceContentionEventArgs.md)\>?

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_ConflictUiRequested"></a> ConflictUiRequested

Raised when the local and cloud saves diverged.

```csharp
public static event EventHandler<GameSaveConflictEventArgs>? ConflictUiRequested
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[GameSaveConflictEventArgs](GDK.Net.PlayFab.GameSaveConflictEventArgs.md)\>?

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_OutOfStorageUiRequested"></a> OutOfStorageUiRequested

Raised when the user's cloud quota cannot hold the pending upload.

```csharp
public static event EventHandler<GameSaveOutOfStorageEventArgs>? OutOfStorageUiRequested
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[GameSaveOutOfStorageEventArgs](GDK.Net.PlayFab.GameSaveOutOfStorageEventArgs.md)\>?

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_SyncFailedUiRequested"></a> SyncFailedUiRequested

Raised when a sync failed and the user must choose how to continue.

```csharp
public static event EventHandler<GameSaveSyncFailedEventArgs>? SyncFailedUiRequested
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[GameSaveSyncFailedEventArgs](GDK.Net.PlayFab.GameSaveSyncFailedEventArgs.md)\>?

### <a id="GDK_Net_PlayFab_PlayFabGameSaveFiles_SyncProgressUiRequested"></a> SyncProgressUiRequested

Raised while a sync runs so the title can show a progress dialog.

```csharp
public static event EventHandler<GameSaveSyncProgressEventArgs>? SyncProgressUiRequested
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler\-1)<[GameSaveSyncProgressEventArgs](GDK.Net.PlayFab.GameSaveSyncProgressEventArgs.md)\>?

