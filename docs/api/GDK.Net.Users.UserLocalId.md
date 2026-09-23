# <a id="GDK_Net_Users_UserLocalId"></a> Struct UserLocalId

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

A machine-stable identifier for a signed-in user. Mirrors <code>XUserLocalId</code>.

```csharp
public readonly struct UserLocalId : IEquatable<UserLocalId>
```

#### Implements

[IEquatable<UserLocalId\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Handles that <code>XUserCompare</code> treats as equal always share one local id, which is why
<xref href="GDK.Net.Users.User.GetHashCode" data-throw-if-not-resolved="false"></xref> hashes this value.

## Constructors

### <a id="GDK_Net_Users_UserLocalId__ctor_System_UInt64_"></a> UserLocalId\(ulong\)

Initialises the local id from its raw 64-bit value.

```csharp
public UserLocalId(ulong value)
```

#### Parameters

`value` [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Properties

### <a id="GDK_Net_Users_UserLocalId_IsNull"></a> IsNull

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this is the null local id.

```csharp
public bool IsNull { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Users_UserLocalId_Null"></a> Null

The null local id (<code>XUserNullUserLocalId</code>).

```csharp
public static UserLocalId Null { get; }
```

#### Property Value

 [UserLocalId](GDK.Net.Users.UserLocalId.md)

### <a id="GDK_Net_Users_UserLocalId_Value"></a> Value

The raw 64-bit local id.

```csharp
public ulong Value { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_Users_UserLocalId_Equals_GDK_Net_Users_UserLocalId_"></a> Equals\(UserLocalId\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(UserLocalId other)
```

#### Parameters

`other` [UserLocalId](GDK.Net.Users.UserLocalId.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_Users_UserLocalId_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_Users_UserLocalId_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_Users_UserLocalId_ToString"></a> ToString\(\)

Returns the local id as a 16-digit hexadecimal string.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Operators

### <a id="GDK_Net_Users_UserLocalId_op_Equality_GDK_Net_Users_UserLocalId_GDK_Net_Users_UserLocalId_"></a> operator ==\(UserLocalId, UserLocalId\)

Equality operator.

```csharp
public static bool operator ==(UserLocalId left, UserLocalId right)
```

#### Parameters

`left` [UserLocalId](GDK.Net.Users.UserLocalId.md)

`right` [UserLocalId](GDK.Net.Users.UserLocalId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Users_UserLocalId_op_Inequality_GDK_Net_Users_UserLocalId_GDK_Net_Users_UserLocalId_"></a> operator \!=\(UserLocalId, UserLocalId\)

Inequality operator.

```csharp
public static bool operator !=(UserLocalId left, UserLocalId right)
```

#### Parameters

`left` [UserLocalId](GDK.Net.Users.UserLocalId.md)

`right` [UserLocalId](GDK.Net.Users.UserLocalId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

