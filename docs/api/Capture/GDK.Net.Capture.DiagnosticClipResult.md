# <a id="GDK_Net_Capture_DiagnosticClipResult"></a> Class DiagnosticClipResult

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

Result of <xref href="GDK.Net.Capture.AppCaptureManager.RecordDiagnosticClip(System.DateTimeOffset%2cSystem.UInt32%2cSystem.String)" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XAppCaptureRecordClipResult</code>.

```csharp
public sealed class DiagnosticClipResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DiagnosticClipResult](GDK.Net.Capture.DiagnosticClipResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Capture_DiagnosticClipResult_DurationInMs"></a> DurationInMs

Clip duration in milliseconds (<code>durationInMs</code>).

```csharp
public uint DurationInMs { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Capture_DiagnosticClipResult_Encoding"></a> Encoding

Video encoding format (<code>encoding</code>).

```csharp
public AppCaptureVideoEncoding Encoding { get; }
```

#### Property Value

 [AppCaptureVideoEncoding](GDK.Net.Capture.AppCaptureVideoEncoding.md)

### <a id="GDK_Net_Capture_DiagnosticClipResult_FileSize"></a> FileSize

File size in bytes (<code>fileSize</code>).

```csharp
public long FileSize { get; }
```

#### Property Value

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

### <a id="GDK_Net_Capture_DiagnosticClipResult_Height"></a> Height

Video height in pixels (<code>height</code>).

```csharp
public uint Height { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Capture_DiagnosticClipResult_Path"></a> Path

Absolute path to the clip file on disk (<code>path</code>).

```csharp
public string Path { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Capture_DiagnosticClipResult_StartTime"></a> StartTime

Clip start time in UTC (<code>startTime</code>, from Unix <code>time_t</code>).

```csharp
public DateTimeOffset StartTime { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_Capture_DiagnosticClipResult_StartTimePreciseOffsetHns"></a> StartTimePreciseOffsetHns

Precise offset in 100-nanosecond units from <xref href="GDK.Net.Capture.DiagnosticClipResult.StartTime" data-throw-if-not-resolved="false"></xref> (<code>startTimePreciseOffsetHns</code>).

```csharp
public uint StartTimePreciseOffsetHns { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Capture_DiagnosticClipResult_Width"></a> Width

Video width in pixels (<code>width</code>).

```csharp
public uint Width { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

