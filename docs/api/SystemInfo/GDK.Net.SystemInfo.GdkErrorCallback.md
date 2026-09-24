# <a id="GDK_Net_SystemInfo_GdkErrorCallback"></a> Delegate GdkErrorCallback

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Callback invoked by the GDK when an error is reported via <code>XErrorReport</code>.
Return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> to suppress the error (mark it as handled);
return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> to let the GDK apply its default behaviour.

```csharp
public delegate bool GdkErrorCallback(int hresult, string message)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The HRESULT that was reported.

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

A human-readable description of the error.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

