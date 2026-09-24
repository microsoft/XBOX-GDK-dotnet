# <a id="GDK_Net_Users_UserAddOptions"></a> Enum UserAddOptions

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

Options for <xref href="GDK.Net.Users.UserManager.AddAsync(GDK.Net.Users.UserAddOptions%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>. Mirrors <code>XUserAddOptions</code>.

```csharp
[Flags]
public enum UserAddOptions : uint
```

## Fields

`AddDefaultUserAllowingUI = 4` 

Add the default user, showing UI only when it is needed.



`AddDefaultUserSilently = 1` 

Add the default user without showing any UI. Fails with
<xref href="GDK.Net.HResult.EGameUserNoDefaultUser" data-throw-if-not-resolved="false"></xref> when there is no default user.



`AllowGuests = 2` 

Allow a guest account to be selected.



`None = 0` 

No options; the account picker UI is shown.



