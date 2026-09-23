# <a id="GDK_Net_GameRuntimeGameConfigSource"></a> Enum GameRuntimeGameConfigSource

Namespace: [GDK.Net](GDK.Net.md)  
Assembly: GDK.Net.dll  

Controls where <code>XGameRuntimeInitializeWithOptions</code> loads the game configuration from.
Mirrors <code>XGameRuntimeGameConfigSource</code> from XGameRuntimeInit.h.

```csharp
public enum GameRuntimeGameConfigSource
```

## Fields

`Default = 0` 

Use the default MicrosoftGame.config from the package layout.



`File = 2` 

The <xref href="GDK.Net.GameRuntimeOptions.GameConfig" data-throw-if-not-resolved="false"></xref> property is a file path.



`Inline = 1` 

The <xref href="GDK.Net.GameRuntimeOptions.GameConfig" data-throw-if-not-resolved="false"></xref> property is an inline XML string.



