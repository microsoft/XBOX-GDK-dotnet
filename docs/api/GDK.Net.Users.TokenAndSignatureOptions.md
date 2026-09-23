# <a id="GDK_Net_Users_TokenAndSignatureOptions"></a> Enum TokenAndSignatureOptions

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

Options for token-and-signature requests. Mirrors <code>XUserGetTokenAndSignatureOptions</code>.

```csharp
[Flags]
public enum TokenAndSignatureOptions : uint
```

## Fields

`AllUsers = 2` 

Apply the check to all signed-in users, not just the primary one.



`ForceRefresh = 1` 

Bypass the cache and always request a new token from the service.



`None = 0` 

No options.



