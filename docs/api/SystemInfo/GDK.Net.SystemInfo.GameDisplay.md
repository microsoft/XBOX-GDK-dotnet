# <a id="GDK_Net_SystemInfo_GameDisplay"></a> Class GameDisplay

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

HDR mode control and display-timeout deferral APIs.

```csharp
public static class GameDisplay
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameDisplay](GDK.Net.SystemInfo.GameDisplay.md)

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

## Methods

### <a id="GDK_Net_SystemInfo_GameDisplay_AcquireTimeoutDeferral"></a> AcquireTimeoutDeferral\(\)

Acquires a display-timeout deferral that prevents the screen saver from activating while
the deferral is held (<code>XDisplayAcquireTimeoutDeferral</code>).

```csharp
public static DisplayTimeoutDeferral AcquireTimeoutDeferral()
```

#### Returns

 [DisplayTimeoutDeferral](GDK.Net.SystemInfo.DisplayTimeoutDeferral.md)

An <xref href="System.IDisposable" data-throw-if-not-resolved="false"></xref> that closes the native deferral handle when disposed.

#### Remarks

Dispose the returned object as soon as the deferral is no longer needed.
Forgetting to dispose it will keep the screen saver suppressed indefinitely.

### <a id="GDK_Net_SystemInfo_GameDisplay_TryEnableHdrMode_GDK_Net_SystemInfo_HdrModePreference_GDK_Net_SystemInfo_HdrModeInfo__"></a> TryEnableHdrMode\(HdrModePreference, out HdrModeInfo?\)

Attempts to enable HDR output on the primary display (<code>XDisplayTryEnableHdrMode</code>).

```csharp
public static HdrModeResult TryEnableHdrMode(HdrModePreference preference, out HdrModeInfo? info)
```

#### Parameters

`preference` [HdrModePreference](GDK.Net.SystemInfo.HdrModePreference.md)

Whether to prefer HDR or a higher refresh rate.

`info` [HdrModeInfo](GDK.Net.SystemInfo.HdrModeInfo.md)?

When HDR was successfully enabled, receives the display's luminance capabilities;
otherwise <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [HdrModeResult](GDK.Net.SystemInfo.HdrModeResult.md)

<xref href="GDK.Net.SystemInfo.HdrModeResult.Enabled" data-throw-if-not-resolved="false"></xref> when HDR is active after this call;
<xref href="GDK.Net.SystemInfo.HdrModeResult.Disabled" data-throw-if-not-resolved="false"></xref> when HDR is unavailable;
<xref href="GDK.Net.SystemInfo.HdrModeResult.Unknown" data-throw-if-not-resolved="false"></xref> when the display state cannot be determined.

