# <a id="GDK_Net_Users_SpopOperationResult"></a> Enum SpopOperationResult

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

How the title resolved a "signed in on another device" (SPOP) prompt. Mirrors
<code>XUserPlatformSpopOperationResult</code>; passed to <xref href="GDK.Net.Users.SpopPromptEventArgs.Complete(GDK.Net.Users.SpopOperationResult)" data-throw-if-not-resolved="false"></xref>.

```csharp
public enum SpopOperationResult
```

## Fields

`Canceled = 3` 

The user dismissed the prompt without choosing.



`Failure = 2` 

The prompt could not be shown or failed.



`SignInHere = 0` 

The user chose to keep this device signed in and sign the other device out.



`SwitchAccount = 1` 

The user chose to switch to a different account on this device.



