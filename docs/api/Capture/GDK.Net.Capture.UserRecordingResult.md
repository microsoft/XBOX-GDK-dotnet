# <a id="GDK_Net_Capture_UserRecordingResult"></a> Class UserRecordingResult

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

The finished clip produced by <xref href="GDK.Net.Capture.AppCaptureManager.StopUserRecord(System.String)" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class UserRecordingResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[UserRecordingResult](GDK.Net.Capture.UserRecordingResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Unlike <xref href="GDK.Net.Capture.LocalClipStream" data-throw-if-not-resolved="false"></xref>, a user recording is written straight to the user's own
capture library, so there is no stream handle to read or dispose: this type is a plain
description of what was recorded.

## Properties

### <a id="GDK_Net_Capture_UserRecordingResult_ColorFormat"></a> ColorFormat

Color format (<code>colorFormat</code>).

```csharp
public AppCaptureVideoColorFormat ColorFormat { get; }
```

#### Property Value

 [AppCaptureVideoColorFormat](GDK.Net.Capture.AppCaptureVideoColorFormat.md)

### <a id="GDK_Net_Capture_UserRecordingResult_DurationInMilliseconds"></a> DurationInMilliseconds

Clip duration in milliseconds (<code>durationInMilliseconds</code>).

```csharp
public ulong DurationInMilliseconds { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Capture_UserRecordingResult_Encoding"></a> Encoding

Video encoding (<code>encoding</code>).

```csharp
public AppCaptureVideoEncoding Encoding { get; }
```

#### Property Value

 [AppCaptureVideoEncoding](GDK.Net.Capture.AppCaptureVideoEncoding.md)

### <a id="GDK_Net_Capture_UserRecordingResult_FileSizeInBytes"></a> FileSizeInBytes

Total clip data size in bytes (<code>fileSizeInBytes</code>).

```csharp
public long FileSizeInBytes { get; }
```

#### Property Value

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

### <a id="GDK_Net_Capture_UserRecordingResult_Height"></a> Height

Video height in pixels (<code>height</code>).

```csharp
public uint Height { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Capture_UserRecordingResult_StartTimestamp"></a> StartTimestamp

When the clip started, in UTC (<code>clipStartTimestamp</code>).

```csharp
public DateTime StartTimestamp { get; }
```

#### Property Value

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

### <a id="GDK_Net_Capture_UserRecordingResult_Width"></a> Width

Video width in pixels (<code>width</code>).

```csharp
public uint Width { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

