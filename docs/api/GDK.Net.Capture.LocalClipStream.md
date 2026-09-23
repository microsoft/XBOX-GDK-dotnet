# <a id="GDK_Net_Capture_LocalClipStream"></a> Class LocalClipStream

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

Provides random-access reading of a local video clip returned by
<xref href="GDK.Net.Capture.AppCaptureManager.RecordTimespan(System.UInt64)" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.Capture.AppCaptureManager.RecordTimespan(System.DateTime%2cSystem.UInt64)" data-throw-if-not-resolved="false"></xref>. Owns the native
<code>XAppCaptureLocalStreamHandle</code>; dispose to release it via
<code>XAppCaptureCloseLocalStream</code>.

```csharp
public sealed class LocalClipStream : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LocalClipStream](GDK.Net.Capture.LocalClipStream.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Capture_LocalClipStream_ColorFormat"></a> ColorFormat

Color format (<code>colorFormat</code>).

```csharp
public AppCaptureVideoColorFormat ColorFormat { get; }
```

#### Property Value

 [AppCaptureVideoColorFormat](GDK.Net.Capture.AppCaptureVideoColorFormat.md)

### <a id="GDK_Net_Capture_LocalClipStream_DurationInMilliseconds"></a> DurationInMilliseconds

Clip duration in milliseconds (<code>durationInMilliseconds</code>).

```csharp
public ulong DurationInMilliseconds { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_Capture_LocalClipStream_Encoding"></a> Encoding

Video encoding (<code>encoding</code>).

```csharp
public AppCaptureVideoEncoding Encoding { get; }
```

#### Property Value

 [AppCaptureVideoEncoding](GDK.Net.Capture.AppCaptureVideoEncoding.md)

### <a id="GDK_Net_Capture_LocalClipStream_FileSizeInBytes"></a> FileSizeInBytes

Total clip data size in bytes (<code>fileSizeInBytes</code>).

```csharp
public long FileSizeInBytes { get; }
```

#### Property Value

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

### <a id="GDK_Net_Capture_LocalClipStream_Height"></a> Height

Video height in pixels (<code>height</code>).

```csharp
public uint Height { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Capture_LocalClipStream_StartTimestamp"></a> StartTimestamp

When the clip started, in UTC (<code>clipStartTimestamp</code>).

```csharp
public DateTime StartTimestamp { get; }
```

#### Property Value

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

### <a id="GDK_Net_Capture_LocalClipStream_Width"></a> Width

Video width in pixels (<code>width</code>).

```csharp
public uint Width { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

## Methods

### <a id="GDK_Net_Capture_LocalClipStream_Dispose"></a> Dispose\(\)

Closes the native stream handle (<code>XAppCaptureCloseLocalStream</code>). After disposal, calling
any other method throws <xref href="System.ObjectDisposedException" data-throw-if-not-resolved="false"></xref>.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Capture_LocalClipStream_Read_System_UIntPtr_System_Byte___System_Int32_System_Int32_"></a> Read\(nuint, byte\[\], int, int\)

Reads up to <code class="paramref">count</code> bytes of clip data starting at
<code class="paramref">startPosition</code> into <code class="paramref">buffer</code>.
Returns the number of bytes written (<code>XAppCaptureReadLocalStream</code>).

```csharp
public int Read(nuint startPosition, byte[] buffer, int bufferOffset, int count)
```

#### Parameters

`startPosition` [nuint](https://learn.microsoft.com/dotnet/api/system.uintptr)

Byte offset from the beginning of the stream.

`buffer` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Destination buffer.

`bufferOffset` [int](https://learn.microsoft.com/dotnet/api/system.int32)

Offset within <code class="paramref">buffer</code> at which to write.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

Maximum number of bytes to read.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

