# <a id="GDK_Net_SystemInfo_GameErrorHandling"></a> Class GameErrorHandling

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

GDK error-reporting hooks. Wrap <code>XErrorSetCallback</code> and <code>XErrorSetOptions</code> from
XError.h.

```csharp
public static class GameErrorHandling
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameErrorHandling](GDK.Net.SystemInfo.GameErrorHandling.md)

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
Use <xref href="GDK.Net.SystemInfo.GameErrorHandling.SetErrorCallback(GDK.Net.SystemInfo.GdkErrorCallback)" data-throw-if-not-resolved="false"></xref> to receive a notification whenever the Gaming Runtime calls
<code>XErrorReport</code> internally. Return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> from the callback to suppress the
error; return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> to let the runtime apply its configured
<xref href="GDK.Net.SystemInfo.ErrorOptions" data-throw-if-not-resolved="false"></xref> behaviour.
</p>
<p>
Only one callback can be active at a time (the native API is a last-writer-wins register).
Passing <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> clears the registration.
</p>

## Methods

### <a id="GDK_Net_SystemInfo_GameErrorHandling_SetErrorCallback_GDK_Net_SystemInfo_GdkErrorCallback_"></a> SetErrorCallback\(GdkErrorCallback?\)

Registers or clears the error callback (<code>XErrorSetCallback</code>).

```csharp
public static void SetErrorCallback(GdkErrorCallback? callback)
```

#### Parameters

`callback` [GdkErrorCallback](GDK.Net.SystemInfo.GdkErrorCallback.md)?

The managed callback to invoke on each error, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear.
The callback must not throw; any exception is silently swallowed at the native boundary.

### <a id="GDK_Net_SystemInfo_GameErrorHandling_SetOptions_GDK_Net_SystemInfo_ErrorOptions_GDK_Net_SystemInfo_ErrorOptions_"></a> SetOptions\(ErrorOptions, ErrorOptions\)

Configures error-reporting behaviour (<code>XErrorSetOptions</code>).

```csharp
public static void SetOptions(ErrorOptions optionsDebuggerPresent, ErrorOptions optionsDebuggerNotPresent)
```

#### Parameters

`optionsDebuggerPresent` [ErrorOptions](GDK.Net.SystemInfo.ErrorOptions.md)

Options applied when a debugger is attached.

`optionsDebuggerNotPresent` [ErrorOptions](GDK.Net.SystemInfo.ErrorOptions.md)

Options applied when no debugger is present.

