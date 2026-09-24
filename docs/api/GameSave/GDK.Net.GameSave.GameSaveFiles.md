# <a id="GDK_Net_GameSave_GameSaveFiles"></a> Class GameSaveFiles

Namespace: [GDK.Net.GameSave](GDK.Net.GameSave.md)  
Assembly: GDK.Net.dll  

Simple-file access to game saves via the file system
(<code>XGameSaveFiles</code> family in <code>XGameSaveFiles.h</code>).

```csharp
public static class GameSaveFiles
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameSaveFiles](GDK.Net.GameSave.GameSaveFiles.md)

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
<code>XGameSaveFiles</code> is an alternative to the blob-based <xref href="GDK.Net.GameSave.GameSaveProvider" data-throw-if-not-resolved="false"></xref> API.
Instead of named blobs, it exposes a folder path that the title can read and write using
ordinary file I/O. Cloud sync is handled by the Gaming Runtime in the background.
</p>
<p>
Requires a signed-in <xref href="GDK.Net.Users.User" data-throw-if-not-resolved="false"></xref> and the service configuration ID from
<code>MicrosoftGame.config</code>, just like <xref href="GDK.Net.GameSave.GameSaveProvider" data-throw-if-not-resolved="false"></xref>.
</p>

## Methods

### <a id="GDK_Net_GameSave_GameSaveFiles_GetFolderWithUiAsync_GDK_Net_Users_User_System_String_System_Threading_CancellationToken_"></a> GetFolderWithUiAsync\(User, string, CancellationToken\)

Returns the local folder path for this user's game saves, showing system UI to resolve
sync conflicts when necessary (<code>XGameSaveFilesGetFolderWithUiAsync</code> /
<code>XGameSaveFilesGetFolderWithUiResult</code>).

```csharp
public static Task<string> GetFolderWithUiAsync(User user, string serviceConfigurationId, CancellationToken cancellationToken = default)
```

#### Parameters

`user` [User](../Users/GDK.Net.Users.User.md)

The signed-in user whose save folder is requested.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The SCID from <code>MicrosoftGame.config</code>.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels via <code>XAsyncCancel</code>.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

Absolute path to the folder; writable by the title.

### <a id="GDK_Net_GameSave_GameSaveFiles_GetRemainingQuota_GDK_Net_Users_User_System_String_"></a> GetRemainingQuota\(User, string\)

Returns the remaining quota in bytes for this user's game-save files
(<code>XGameSaveFilesGetRemainingQuota</code>).

```csharp
public static long GetRemainingQuota(User user, string serviceConfigurationId)
```

#### Parameters

`user` [User](../Users/GDK.Net.Users.User.md)

The signed-in user.

`serviceConfigurationId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The SCID from <code>MicrosoftGame.config</code>.

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

