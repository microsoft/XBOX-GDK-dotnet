# <a id="GDK_Net_SystemInfo_DisplayTimeoutDeferral"></a> Class DisplayTimeoutDeferral

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

Wraps an <code>XDisplayTimeoutDeferralHandle</code>; released with
<code>XDisplayCloseTimeoutDeferralHandle</code> on disposal.

```csharp
public sealed class DisplayTimeoutDeferral : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DisplayTimeoutDeferral](GDK.Net.SystemInfo.DisplayTimeoutDeferral.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_SystemInfo_DisplayTimeoutDeferral_Dispose"></a> Dispose\(\)

Releases the deferral, allowing the screen saver to activate normally.

```csharp
public void Dispose()
```

