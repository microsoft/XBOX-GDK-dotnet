# <a id="GDK_Net_SystemInfo_ErrorOptions"></a> Enum ErrorOptions

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Error reporting options for <xref href="GDK.Net.SystemInfo.GameErrorHandling.SetOptions(GDK.Net.SystemInfo.ErrorOptions%2cGDK.Net.SystemInfo.ErrorOptions)" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>enum class XErrorOptions</code> from XError.h.

```csharp
[Flags]
public enum ErrorOptions : uint
```

## Fields

`DebugBreakOnError = 2` 

Break into the debugger when an error is reported.



`FailFastOnError = 4` 

Call <code>RaiseFailFastException</code> when an error is reported.



`None = 0` 

No special error reporting.



`OutputDebugStringOnError = 1` 

Call <code>OutputDebugString</code> when an error is reported.



