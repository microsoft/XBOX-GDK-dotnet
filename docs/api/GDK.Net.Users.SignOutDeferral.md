# <a id="GDK_Net_Users_SignOutDeferral"></a> Class SignOutDeferral

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

A sign-out deferral that prevents the Gaming Runtime from completing a user sign-out until the
deferral is disposed. Wraps <code>XUserSignOutDeferralHandle</code>.

```csharp
public sealed class SignOutDeferral : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SignOutDeferral](GDK.Net.Users.SignOutDeferral.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Obtain an instance from <xref href="GDK.Net.Users.UserManager.GetSignOutDeferral" data-throw-if-not-resolved="false"></xref> inside the
<code>XUserChangeEvent.SigningOut</code> callback. Dispose as soon as the title has finished any
work that must complete before sign-out (for example, saving game state). Keeping this object
alive indefinitely will block the sign-out flow.

## Methods

### <a id="GDK_Net_Users_SignOutDeferral_Dispose"></a> Dispose\(\)

Releases the deferral, allowing the Gaming Runtime to complete the pending sign-out.

```csharp
public void Dispose()
```

