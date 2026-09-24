# <a id="GDK_Net_Capture_ScreenshotStream"></a> Class ScreenshotStream

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

Provides random-access reading of a screenshot opened via
<xref href="GDK.Net.Capture.AppCaptureManager.OpenScreenshotStream(System.String%2cGDK.Net.Capture.AppCaptureScreenshotFormatFlag)" data-throw-if-not-resolved="false"></xref>. Owns the native
<code>XAppCaptureScreenshotStreamHandle</code>; dispose to release it via
<code>XAppCaptureCloseScreenshotStream</code>.

```csharp
public sealed class ScreenshotStream : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ScreenshotStream](GDK.Net.Capture.ScreenshotStream.md)

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

### <a id="GDK_Net_Capture_ScreenshotStream_TotalBytes"></a> TotalBytes

Total size of the screenshot data in bytes (<code>totalBytes</code>).

```csharp
public ulong TotalBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_Capture_ScreenshotStream_Dispose"></a> Dispose\(\)

Closes the native stream handle (<code>XAppCaptureCloseScreenshotStream</code>). After disposal,
calling any other method throws <xref href="System.ObjectDisposedException" data-throw-if-not-resolved="false"></xref>.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Capture_ScreenshotStream_Read_System_UInt64_System_Byte___System_Int32_System_Int32_"></a> Read\(ulong, byte\[\], int, int\)

Reads up to <code class="paramref">count</code> bytes of screenshot data starting at
<code class="paramref">startPosition</code> into <code class="paramref">buffer</code>.
Returns the number of bytes written (<code>XAppCaptureReadScreenshotStream</code>).

```csharp
public int Read(ulong startPosition, byte[] buffer, int bufferOffset, int count)
```

#### Parameters

`startPosition` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

Byte offset from the beginning of the screenshot.

`buffer` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Destination buffer.

`bufferOffset` [int](https://learn.microsoft.com/dotnet/api/system.int32)

Offset within <code class="paramref">buffer</code> at which to write.

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

Maximum number of bytes to read.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

