# <a id="GDK_Net_Store_StoreVideo"></a> Class StoreVideo

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

A video associated with a product or SKU. Mirrors <code>XStoreVideo</code>.

```csharp
public sealed class StoreVideo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreVideo](GDK.Net.Store.StoreVideo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Store_StoreVideo_Caption"></a> Caption

Descriptive caption.

```csharp
public string Caption { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreVideo_Height"></a> Height

Height in pixels.

```csharp
public uint Height { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Store_StoreVideo_PreviewImage"></a> PreviewImage

Preview image for the video.

```csharp
public StoreImage PreviewImage { get; }
```

#### Property Value

 [StoreImage](GDK.Net.Store.StoreImage.md)

### <a id="GDK_Net_Store_StoreVideo_Uri"></a> Uri

URI of the video.

```csharp
public string Uri { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreVideo_VideoPurposeTag"></a> VideoPurposeTag

Tag describing the video's purpose.

```csharp
public string VideoPurposeTag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Store_StoreVideo_Width"></a> Width

Width in pixels.

```csharp
public uint Width { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

