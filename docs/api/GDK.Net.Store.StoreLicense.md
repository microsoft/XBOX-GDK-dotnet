# <a id="GDK_Net_Store_StoreLicense"></a> Class StoreLicense

Namespace: [GDK.Net.Store](GDK.Net.Store.md)  
Assembly: GDK.Net.dll  

A package or durable licence, produced by
<xref href="GDK.Net.Store.StoreContext.AcquireLicenseForPackageAsync(System.String%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.Store.StoreContext.AcquireLicenseForDurablesAsync(System.String%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class StoreLicense : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StoreLicense](GDK.Net.Store.StoreLicense.md)

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

<p>
The licence handle is owned by a <xref href="System.Runtime.InteropServices.SafeHandle" data-throw-if-not-resolved="false"></xref>; disposal releases
<code>XStoreCloseLicenseHandle</code>. Any in-flight <xref href="GDK.Net.Store.StoreLicense.PackageLicenseLost" data-throw-if-not-resolved="false"></xref> callback is
waited for before the handle is released.
</p>
<p>
Subscribe to <xref href="GDK.Net.Store.StoreLicense.PackageLicenseLost" data-throw-if-not-resolved="false"></xref> to be notified when the licence is revoked (for
example, when the user signs out or the trial expires). The handler is delivered on the task
queue supplied to <xref href="GDK.Net.Store.StoreContext.Create" data-throw-if-not-resolved="false"></xref> or
<xref href="GDK.Net.Store.StoreContext.CreateForUser(GDK.Net.Users.User)" data-throw-if-not-resolved="false"></xref>.
</p>

## Properties

### <a id="GDK_Net_Store_StoreLicense_IsValid"></a> IsValid

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the licence is currently valid
(<code>XStoreIsLicenseValid</code>).

```csharp
public bool IsValid { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Requires the Gaming Runtime; throws <xref href="GDK.Net.GameRuntimeException" data-throw-if-not-resolved="false"></xref> if not initialized.

## Methods

### <a id="GDK_Net_Store_StoreLicense_Dispose"></a> Dispose\(\)

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### <a id="GDK_Net_Store_StoreLicense_PackageLicenseLost"></a> PackageLicenseLost

Raised when the licence is revoked (<code>XStoreRegisterPackageLicenseLost</code>).

```csharp
public event EventHandler? PackageLicenseLost
```

#### Event Type

 [EventHandler](https://learn.microsoft.com/dotnet/api/system.eventhandler)?

#### Remarks

The registration is lazily created on the first subscription and released, with
<code>wait: true</code>, on <xref href="GDK.Net.Store.StoreLicense.Dispose" data-throw-if-not-resolved="false"></xref>.

