# <a id="GDK_Net_Capture_DiagnosticScreenshotResult"></a> Class DiagnosticScreenshotResult

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

Result of <xref href="GDK.Net.Capture.AppCaptureManager.TakeDiagnosticScreenshot(System.Boolean%2cGDK.Net.Capture.AppCaptureScreenshotFormatFlag%2cSystem.String)" data-throw-if-not-resolved="false"></xref>. Contains up to
<code>APPCAPTURE_MAX_CAPTURE_FILES</code> (10) screenshot file entries.
Mirrors <code>XAppCaptureDiagnosticScreenshotResult</code>.

```csharp
public sealed class DiagnosticScreenshotResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DiagnosticScreenshotResult](GDK.Net.Capture.DiagnosticScreenshotResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GDK_Net_Capture_DiagnosticScreenshotResult_MaxFiles"></a> MaxFiles

Maximum number of screenshot files the runtime can return in a single call (10).

```csharp
public const int MaxFiles = 10
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Properties

### <a id="GDK_Net_Capture_DiagnosticScreenshotResult_Files"></a> Files

Screenshot files captured in this call (up to <xref href="GDK.Net.Capture.DiagnosticScreenshotResult.MaxFiles" data-throw-if-not-resolved="false"></xref>).

```csharp
public DiagnosticScreenshotFile[] Files { get; }
```

#### Property Value

 [DiagnosticScreenshotFile](GDK.Net.Capture.DiagnosticScreenshotFile.md)\[\]

