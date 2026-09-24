# <a id="GDK_Net_Streaming_TouchBundleVersionInfo"></a> Class TouchBundleVersionInfo

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

Version and name of the touch-adaptation bundle active on a streaming client.
Returned by <xref href="GDK.Net.Streaming.StreamingManager.GetTouchBundleVersion(GDK.Net.Streaming.StreamingClientId)" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class TouchBundleVersionInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TouchBundleVersionInfo](GDK.Net.Streaming.TouchBundleVersionInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Streaming_TouchBundleVersionInfo_Name"></a> Name

The version name string.

```csharp
public string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_Streaming_TouchBundleVersionInfo_Version"></a> Version

The bundle version (<code>XVersion</code>).

```csharp
public Version Version { get; }
```

#### Property Value

 [Version](https://learn.microsoft.com/dotnet/api/system.version)

