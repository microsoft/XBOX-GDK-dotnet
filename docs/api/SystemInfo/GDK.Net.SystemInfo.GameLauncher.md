# <a id="GDK_Net_SystemInfo_GameLauncher"></a> Class GameLauncher

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Title identity and cross-game launch APIs.

```csharp
public static class GameLauncher
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameLauncher](GDK.Net.SystemInfo.GameLauncher.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Requires the Gaming Runtime to be initialized (<xref href="GDK.Net.GameRuntime.Initialize" data-throw-if-not-resolved="false"></xref>).
All methods require a packaged title with a valid <code>MicrosoftGame.config</code> unless noted.

## Methods

### <a id="GDK_Net_SystemInfo_GameLauncher_GetXboxTitleId"></a> GetXboxTitleId\(\)

Returns the Xbox title id declared in <code>MicrosoftGame.config</code>
(<code>XGameGetXboxTitleId</code>).

```csharp
public static uint GetXboxTitleId()
```

#### Returns

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

#### Exceptions

 [GameRuntimeException](../Core/GDK.Net.GameRuntimeException.md)

Thrown when the title id cannot be found (HRESULT_FROM_WIN32(ERROR_NOT_FOUND)).

### <a id="GDK_Net_SystemInfo_GameLauncher_LaunchNewGame_System_String_System_String_"></a> LaunchNewGame\(string, string?\)

Launches a new game process in place of the current one and terminates this process
(<code>XLaunchNewGame</code>). This method never returns.

```csharp
public static void LaunchNewGame(string exePath, string? args = null)
```

#### Parameters

`exePath` [string](https://learn.microsoft.com/dotnet/api/system.string)

Absolute path to the executable to launch. Must be inside the package layout.

`args` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional command-line arguments to pass to the new process.

#### Remarks

The process is terminated after the native call. Resources allocated on the heap
(including managed objects) are not cleaned up: call <xref href="GDK.Net.GameRuntime.Dispose" data-throw-if-not-resolved="false"></xref>
and flush any pending I/O before calling this method.

### <a id="GDK_Net_SystemInfo_GameLauncher_LaunchUri_System_String_GDK_Net_Users_User_"></a> LaunchUri\(string, User?\)

Opens a URI using the appropriate handler on the current platform
(<code>XLaunchUri</code>).

```csharp
public static void LaunchUri(string uri, User? requestingUser = null)
```

#### Parameters

`uri` [string](https://learn.microsoft.com/dotnet/api/system.string)

The URI to open (e.g. <code>ms-xbl-multiplayer://...</code>).

`requestingUser` [User](../Users/GDK.Net.Users.User.md)?

The user the launch is attributed to, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to let the system choose.

#### Remarks

<code class="paramref">uri</code> must be a URI registered in the platform URI scheme registry.

### <a id="GDK_Net_SystemInfo_GameLauncher_RestartOnCrash_System_String_"></a> RestartOnCrash\(string?\)

Requests that the current game process be restarted automatically when it crashes
(<code>XLaunchRestartOnCrash</code>). Only active during development; the call is a no-op in
retail packages.

```csharp
public static void RestartOnCrash(string? args = null)
```

#### Parameters

`args` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional arguments forwarded to the restarted process.

