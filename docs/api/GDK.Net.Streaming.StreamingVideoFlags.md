# <a id="GDK_Net_Streaming_StreamingVideoFlags"></a> Enum StreamingVideoFlags

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

Video capabilities reported by a streaming client. Mirrors <code>XGameStreamingVideoFlags</code>.

```csharp
[Flags]
public enum StreamingVideoFlags : uint
```

## Fields

`All = 3` 

All capability flags.



`None = 0` 

No special capabilities.



`SupportsCustomAspectRatio = 1` 

The client supports a custom aspect ratio.



`SupportsPresentScaling = 2` 

The client supports present scaling.



