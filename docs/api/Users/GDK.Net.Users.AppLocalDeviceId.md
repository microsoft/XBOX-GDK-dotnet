# <a id="GDK_Net_Users_AppLocalDeviceId"></a> Struct AppLocalDeviceId

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

A 32-byte opaque device identifier. Mirrors <code>APP_LOCAL_DEVICE_ID</code> from windef.h.

```csharp
public struct AppLocalDeviceId : IEquatable<AppLocalDeviceId>
```

#### Implements

[IEquatable<AppLocalDeviceId\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The underlying bytes are stored as four 64-bit integers so the struct is naturally aligned and
blittable on both x64 and arm64. The layout is identical to the native <code>APP_LOCAL_DEVICE_ID</code>.

## Properties

### <a id="GDK_Net_Users_AppLocalDeviceId_IsNull"></a> IsNull

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this is the null device id.

```csharp
public bool IsNull { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Users_AppLocalDeviceId_Null"></a> Null

The null device id (<code>XUserNullDeviceId</code>).

```csharp
public static AppLocalDeviceId Null { get; }
```

#### Property Value

 [AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)

## Methods

### <a id="GDK_Net_Users_AppLocalDeviceId_Equals_GDK_Net_Users_AppLocalDeviceId_"></a> Equals\(AppLocalDeviceId\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(AppLocalDeviceId other)
```

#### Parameters

`other` [AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_Users_AppLocalDeviceId_Equals_System_Object_"></a> Equals\(object?\)

Indicates whether this instance and a specified object are equal.

```csharp
public override bool Equals(object? obj)
```

#### Parameters

`obj` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The object to compare with the current instance.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if <code class="paramref">obj</code> and this instance are the same type and represent the same value; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_Users_AppLocalDeviceId_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_Users_AppLocalDeviceId_ToString"></a> ToString\(\)

Returns the fully qualified type name of this instance.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The fully qualified type name.

## Operators

### <a id="GDK_Net_Users_AppLocalDeviceId_op_Equality_GDK_Net_Users_AppLocalDeviceId_GDK_Net_Users_AppLocalDeviceId_"></a> operator ==\(AppLocalDeviceId, AppLocalDeviceId\)

Equality operator.

```csharp
public static bool operator ==(AppLocalDeviceId left, AppLocalDeviceId right)
```

#### Parameters

`left` [AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)

`right` [AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Users_AppLocalDeviceId_op_Inequality_GDK_Net_Users_AppLocalDeviceId_GDK_Net_Users_AppLocalDeviceId_"></a> operator \!=\(AppLocalDeviceId, AppLocalDeviceId\)

Inequality operator.

```csharp
public static bool operator !=(AppLocalDeviceId left, AppLocalDeviceId right)
```

#### Parameters

`left` [AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)

`right` [AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

