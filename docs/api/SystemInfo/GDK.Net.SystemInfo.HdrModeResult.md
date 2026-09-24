# <a id="GDK_Net_SystemInfo_HdrModeResult"></a> Enum HdrModeResult

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Result of a call to <xref href="GDK.Net.SystemInfo.GameDisplay.TryEnableHdrMode(GDK.Net.SystemInfo.HdrModePreference%2cGDK.Net.SystemInfo.HdrModeInfo%40)" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>enum class XDisplayHdrModeResult</code> from XDisplay.h.

```csharp
public enum HdrModeResult : uint
```

## Fields

`Disabled = 2` 

HDR mode could not be enabled (display does not support it, or preference was <xref href="GDK.Net.SystemInfo.HdrModePreference.PreferRefreshRate" data-throw-if-not-resolved="false"></xref>).



`Enabled = 1` 

HDR mode was successfully enabled.



`Unknown = 0` 

The HDR status is unknown.



