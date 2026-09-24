# <a id="GDK_Net_Streaming_StreamingDisplayDetails"></a> Struct StreamingDisplayDetails

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

Display details for a streaming client, returned by
<xref href="GDK.Net.Streaming.StreamingManager.GetDisplayDetails(GDK.Net.Streaming.StreamingClientId%2cSystem.UInt32%2cSystem.Single%2cSystem.Single)" data-throw-if-not-resolved="false"></xref>. Mirrors <code>XGameStreamingDisplayDetails</code>.

```csharp
public readonly struct StreamingDisplayDetails
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_MaxHeight"></a> MaxHeight

Maximum supported height in pixels.

```csharp
public uint MaxHeight { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_MaxPixels"></a> MaxPixels

Maximum supported pixel count.

```csharp
public uint MaxPixels { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_MaxWidth"></a> MaxWidth

Maximum supported width in pixels.

```csharp
public uint MaxWidth { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_PreferredHeight"></a> PreferredHeight

The client's preferred render height in pixels.

```csharp
public uint PreferredHeight { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_PreferredWidth"></a> PreferredWidth

The client's preferred render width in pixels.

```csharp
public uint PreferredWidth { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_SafeAreaBottom"></a> SafeAreaBottom

Bottom edge of the safe area (RECT.bottom).

```csharp
public int SafeAreaBottom { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_SafeAreaLeft"></a> SafeAreaLeft

Left edge of the safe area (RECT.left).

```csharp
public int SafeAreaLeft { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_SafeAreaRight"></a> SafeAreaRight

Right edge of the safe area (RECT.right).

```csharp
public int SafeAreaRight { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_SafeAreaTop"></a> SafeAreaTop

Top edge of the safe area (RECT.top).

```csharp
public int SafeAreaTop { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_Streaming_StreamingDisplayDetails_VideoFlags"></a> VideoFlags

Video capability flags reported by the client.

```csharp
public StreamingVideoFlags VideoFlags { get; }
```

#### Property Value

 [StreamingVideoFlags](GDK.Net.Streaming.StreamingVideoFlags.md)

