# <a id="GDK_Net_XboxLive_TitleStorageType"></a> Enum TitleStorageType

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Where a title storage blob is stored. Mirrors <code>XblTitleStorageType</code>.

```csharp
public enum TitleStorageType : uint
```

## Fields

`GlobalStorage = 1` 

Global title storage, writable only through title configuration tools.



`TrustedPlatformStorage = 0` 

Per-user storage restricted to Xbox consoles.



`Universal = 2` 

Per-user storage available to Xbox consoles, Windows and mobile devices.



