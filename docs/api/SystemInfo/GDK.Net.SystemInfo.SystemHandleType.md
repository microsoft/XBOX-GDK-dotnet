# <a id="GDK_Net_SystemInfo_SystemHandleType"></a> Enum SystemHandleType

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

The type of a GDK handle passed to <code>XSystemHandleCallback</code>. Mirrors
<code>enum class XSystemHandleType</code> from XSystem.h.

```csharp
public enum SystemHandleType : uint
```

## Fields

`AppCaptureScreenshotStream = 0` 

An app-capture screenshot stream handle.



`DisplayTimeoutDeferral = 1` 

A display-timeout deferral handle.



`GameSaveContainer = 2` 

A game-save container handle.



`GameSaveProvider = 3` 

A game-save provider handle.



`GameSaveUpdate = 4` 

A game-save update handle.



`GameUiTextEntry = 15` 

A Game UI text-entry handle.



`PFXGameSaveConfig = 16` 

A PlayFab Game Save configuration handle.



`PackageInstallationMonitor = 5` 

A package-installation monitor handle.



`PackageMount = 6` 

A package mount handle.



`SpeechSynthesizer = 7` 

A speech synthesizer handle.



`SpeechSynthesizerStream = 8` 

A speech synthesizer stream handle.



`StoreContext = 9` 

A Store context handle.



`StoreLicense = 10` 

A Store licence handle.



`StoreProductQuery = 11` 

A Store product-query handle.



`TaskQueue = 12` 

A task queue handle.



`User = 13` 

A user handle.



`UserSignOutDeferral = 14` 

A user sign-out deferral handle.



