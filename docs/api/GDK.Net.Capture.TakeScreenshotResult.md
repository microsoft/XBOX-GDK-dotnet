# <a id="GDK_Net_Capture_TakeScreenshotResult"></a> Class TakeScreenshotResult

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

The local identifier and available formats returned by <xref href="GDK.Net.Capture.AppCaptureManager.TakeScreenshot(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref>.
Pass <xref href="GDK.Net.Capture.TakeScreenshotResult.LocalId" data-throw-if-not-resolved="false"></xref> and a format flag to <xref href="GDK.Net.Capture.AppCaptureManager.OpenScreenshotStream(System.String%2cGDK.Net.Capture.AppCaptureScreenshotFormatFlag)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XAppCaptureTakeScreenshotResult</code>.

```csharp
public sealed class TakeScreenshotResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TakeScreenshotResult](GDK.Net.Capture.TakeScreenshotResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Capture_TakeScreenshotResult_AvailableFormats"></a> AvailableFormats

Which HDR/SDR formats are available for this screenshot (<code>availableScreenshotFormats</code>).

```csharp
public AppCaptureScreenshotFormatFlag AvailableFormats { get; }
```

#### Property Value

 [AppCaptureScreenshotFormatFlag](GDK.Net.Capture.AppCaptureScreenshotFormatFlag.md)

### <a id="GDK_Net_Capture_TakeScreenshotResult_LocalId"></a> LocalId

Opaque local identifier for the screenshot (<code>localId</code>).

```csharp
public string LocalId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

