# <a id="GDK_Net_Users_UserChangeEvent"></a> Enum UserChangeEvent

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

Mirrors <code>XUserChangeEvent</code>.

```csharp
public enum UserChangeEvent : uint
```

## Fields

`GamerPicture = 4` 

The user's gamer picture changed.



`Gamertag = 3` 

The user's gamertag changed.



`Privileges = 5` 

The user's privileges changed.



`SignedInAgain = 0` 

The user signed in again.



`SignedOut = 2` 

The user has signed out.



`SigningOut = 1` 

The user is signing out. Take a deferral from
<xref href="GDK.Net.Users.UserManager.GetSignOutDeferral" data-throw-if-not-resolved="false"></xref> to finish work before sign-out completes.



