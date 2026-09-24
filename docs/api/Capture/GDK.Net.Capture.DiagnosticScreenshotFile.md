# <a id="GDK_Net_Capture_DiagnosticScreenshotFile"></a> Class DiagnosticScreenshotFile

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

Metadata for one screenshot file in a diagnostic screenshot result.
Mirrors <code>XAppCaptureScreenshotFile</code>.

```csharp
public sealed class DiagnosticScreenshotFile
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DiagnosticScreenshotFile](GDK.Net.Capture.DiagnosticScreenshotFile.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Capture_DiagnosticScreenshotFile_FileSize"></a> FileSize

File size in bytes (<code>fileSize</code>).

```csharp
public long FileSize { get; }
```

#### Property Value

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

### <a id="GDK_Net_Capture_DiagnosticScreenshotFile_Height"></a> Height

Height in pixels (<code>height</code>).

```csharp
public uint Height { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Capture_DiagnosticScreenshotFile_Path"></a> Path

Absolute path to the screenshot file on disk (<code>path</code>).

```csharp
public string Path { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Capture_DiagnosticScreenshotFile_Width"></a> Width

Width in pixels (<code>width</code>).

```csharp
public uint Width { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

