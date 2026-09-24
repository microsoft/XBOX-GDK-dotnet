# <a id="GDK_Net_Capture_VideoCaptureSettings"></a> Class VideoCaptureSettings

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

Current video capture configuration. Returned by <xref href="GDK.Net.Capture.AppCaptureManager.GetVideoCaptureSettings" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>XAppCaptureVideoCaptureSettings</code>.

```csharp
public sealed class VideoCaptureSettings
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VideoCaptureSettings](GDK.Net.Capture.VideoCaptureSettings.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Capture_VideoCaptureSettings_ColorFormat"></a> ColorFormat

Color format of the captured video (<code>colorFormat</code>).

```csharp
public AppCaptureVideoColorFormat ColorFormat { get; }
```

#### Property Value

 [AppCaptureVideoColorFormat](GDK.Net.Capture.AppCaptureVideoColorFormat.md)

### <a id="GDK_Net_Capture_VideoCaptureSettings_Encoding"></a> Encoding

Video encoding format (<code>encoding</code>).

```csharp
public AppCaptureVideoEncoding Encoding { get; }
```

#### Property Value

 [AppCaptureVideoEncoding](GDK.Net.Capture.AppCaptureVideoEncoding.md)

### <a id="GDK_Net_Capture_VideoCaptureSettings_Height"></a> Height

Height of the captured video in pixels (<code>height</code>).

```csharp
public uint Height { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Capture_VideoCaptureSettings_IsCaptureByGamesAllowed"></a> IsCaptureByGamesAllowed

Whether in-game capture triggered by the system is permitted (<code>isCaptureByGamesAllowed</code>).

```csharp
public bool IsCaptureByGamesAllowed { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Capture_VideoCaptureSettings_MaxRecordTimespanDurationInMs"></a> MaxRecordTimespanDurationInMs

Maximum clip duration in milliseconds (<code>maxRecordTimespanDurationInMs</code>).

```csharp
public ulong MaxRecordTimespanDurationInMs { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Capture_VideoCaptureSettings_Width"></a> Width

Width of the captured video in pixels (<code>width</code>).

```csharp
public uint Width { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

