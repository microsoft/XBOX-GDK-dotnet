# <a id="GDK_Net_Capture_AppCaptureMetadataPriority"></a> Enum AppCaptureMetadataPriority

Namespace: [GDK.Net.Capture](GDK.Net.Capture.md)  
Assembly: GDK.Net.dll  

Priority hint for game-capture metadata events and states. Mirrors <code>XAppCaptureMetadataPriority</code>.

```csharp
public enum AppCaptureMetadataPriority : byte
```

## Fields

`Important = 1` 

Higher-priority detail; retained longer under storage pressure.



`Informational = 0` 

Low-priority detail; may be discarded first when storage is constrained.



