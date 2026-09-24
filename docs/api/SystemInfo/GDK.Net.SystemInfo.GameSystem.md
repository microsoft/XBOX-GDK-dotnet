# <a id="GDK_Net_SystemInfo_GameSystem"></a> Class GameSystem

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Device and runtime identity for the current GDK title. All members call directly into the
Gaming Runtime; initialize the runtime with <xref href="GDK.Net.GameRuntime.Initialize" data-throw-if-not-resolved="false"></xref> before use.

```csharp
public static class GameSystem
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameSystem](GDK.Net.SystemInfo.GameSystem.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_SystemInfo_GameSystem_AnalyticsInfo"></a> AnalyticsInfo

Returns analytics information for the current device (<code>XSystemGetAnalyticsInfo</code>).

```csharp
public static SystemAnalyticsInfo AnalyticsInfo { get; }
```

#### Property Value

 [SystemAnalyticsInfo](GDK.Net.SystemInfo.SystemAnalyticsInfo.md)

#### Remarks

Does not require a packaged title.

### <a id="GDK_Net_SystemInfo_GameSystem_ConsoleId"></a> ConsoleId

Returns the console id string, which is unique per device
(<code>XSystemGetConsoleId</code>).

```csharp
public static string ConsoleId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Requires a packaged title and an active internet connection.

### <a id="GDK_Net_SystemInfo_GameSystem_DeviceType"></a> DeviceType

Returns the device type of the current machine (<code>XSystemGetDeviceType</code>).

```csharp
public static SystemDeviceType DeviceType { get; }
```

#### Property Value

 [SystemDeviceType](GDK.Net.SystemInfo.SystemDeviceType.md)

### <a id="GDK_Net_SystemInfo_GameSystem_RuntimeInfo"></a> RuntimeInfo

Returns the runtime and available GDK version information (<code>XSystemGetRuntimeInfo</code>).

```csharp
public static SystemRuntimeInfo RuntimeInfo { get; }
```

#### Property Value

 [SystemRuntimeInfo](GDK.Net.SystemInfo.SystemRuntimeInfo.md)

### <a id="GDK_Net_SystemInfo_GameSystem_XboxLiveSandboxId"></a> XboxLiveSandboxId

Returns the Xbox Live sandbox id for the current device
(<code>XSystemGetXboxLiveSandboxId</code>).

```csharp
public static string XboxLiveSandboxId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="GDK_Net_SystemInfo_GameSystem_AllowFullDownloadBandwidth_System_Boolean_"></a> AllowFullDownloadBandwidth\(bool\)

Requests that the title be allowed to use the full available download bandwidth
(<code>XSystemAllowFullDownloadBandwidth</code>). Pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> to return to the
throttled default.

```csharp
public static void AllowFullDownloadBandwidth(bool allow)
```

#### Parameters

`allow` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Use this only while the title is genuinely blocked on a download, for example on a loading
or install-progress screen. Leaving it enabled during gameplay starves other traffic.

#### Exceptions

 [GameRuntimeException](../Core/GDK.Net.GameRuntimeException.md)

The runtime rejected the request.

### <a id="GDK_Net_SystemInfo_GameSystem_GetAppSpecificDeviceId"></a> GetAppSpecificDeviceId\(\)

Returns an app-specific device id that is stable per (app, device) pair
(<code>XSystemGetAppSpecificDeviceId</code>).

```csharp
public static string GetAppSpecificDeviceId()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Requires a packaged title.

### <a id="GDK_Net_SystemInfo_GameSystem_IsHandleValid_System_IntPtr_"></a> IsHandleValid\(nint\)

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when <code class="paramref">handle</code> is a live GDK handle
(<code>XSystemIsHandleValid</code>).

```csharp
public static bool IsHandleValid(nint handle)
```

#### Parameters

`handle` [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_SystemInfo_GameSystem_SetHandleTrackingCallback_System_Action_System_IntPtr_GDK_Net_SystemInfo_SystemHandleType_GDK_Net_SystemInfo_SystemHandleCallbackReason__"></a> SetHandleTrackingCallback\(Action<nint, SystemHandleType, SystemHandleCallbackReason\>?\)

Registers a process-wide callback that fires whenever any GDK handle is created or
destroyed (<code>XSystemHandleTrack</code>).

```csharp
public static void SetHandleTrackingCallback(Action<nint, SystemHandleType, SystemHandleCallbackReason>? callback)
```

#### Parameters

`callback` [Action](https://learn.microsoft.com/dotnet/api/system.action\-3)<[nint](https://learn.microsoft.com/dotnet/api/system.intptr), [SystemHandleType](GDK.Net.SystemInfo.SystemHandleType.md), [SystemHandleCallbackReason](GDK.Net.SystemInfo.SystemHandleCallbackReason.md)\>?

#### Remarks

<p>
The GDK provides no unregister function for this callback, so the first non-null call
registers the native trampoline permanently; subsequent calls update only the managed
target. Passing <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> stops managed dispatch but the native trampoline
remains registered.
</p>
<p>
Only available during development; the call is a no-op in retail packages.
Requires the Gaming Runtime to be initialized.
</p>

