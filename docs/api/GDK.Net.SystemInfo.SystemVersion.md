# <a id="GDK_Net_SystemInfo_SystemVersion"></a> Struct SystemVersion

Namespace: [GDK.Net.SystemInfo](GDK.Net.SystemInfo.md)  
Assembly: GDK.Net.dll  

A GDK version number (<code>XVersion</code> from XGameRuntimeTypes.h). The four 16-bit components
pack into a single uint64 for ordered comparisons.

```csharp
public readonly struct SystemVersion : IEquatable<SystemVersion>
```

#### Implements

[IEquatable<SystemVersion\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_SystemInfo_SystemVersion__ctor_System_UInt16_System_UInt16_System_UInt16_System_UInt16_"></a> SystemVersion\(ushort, ushort, ushort, ushort\)

```csharp
public SystemVersion(ushort major, ushort minor, ushort build, ushort revision)
```

#### Parameters

`major` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

Major version component.

`minor` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

Minor version component.

`build` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

Build version component.

`revision` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

Revision version component.

## Properties

### <a id="GDK_Net_SystemInfo_SystemVersion_Build"></a> Build

Build number.

```csharp
public ushort Build { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_SystemInfo_SystemVersion_Major"></a> Major

Major version component.

```csharp
public ushort Major { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_SystemInfo_SystemVersion_Minor"></a> Minor

Minor version component.

```csharp
public ushort Minor { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_SystemInfo_SystemVersion_PackedValue"></a> PackedValue

The bit-packed uint64 representation used by the GDK for version comparisons.

```csharp
public ulong PackedValue { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_SystemInfo_SystemVersion_Revision"></a> Revision

Revision number.

```csharp
public ushort Revision { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

## Methods

### <a id="GDK_Net_SystemInfo_SystemVersion_Equals_GDK_Net_SystemInfo_SystemVersion_"></a> Equals\(SystemVersion\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(SystemVersion other)
```

#### Parameters

`other` [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_SystemInfo_SystemVersion_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_SystemInfo_SystemVersion_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_SystemInfo_SystemVersion_ToString"></a> ToString\(\)

Returns the version formatted as <code>Major.Minor.Build.Revision</code>.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Operators

### <a id="GDK_Net_SystemInfo_SystemVersion_op_Equality_GDK_Net_SystemInfo_SystemVersion_GDK_Net_SystemInfo_SystemVersion_"></a> operator ==\(SystemVersion, SystemVersion\)

Equality operator.

```csharp
public static bool operator ==(SystemVersion left, SystemVersion right)
```

#### Parameters

`left` [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

`right` [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_SystemInfo_SystemVersion_op_Inequality_GDK_Net_SystemInfo_SystemVersion_GDK_Net_SystemInfo_SystemVersion_"></a> operator \!=\(SystemVersion, SystemVersion\)

Inequality operator.

```csharp
public static bool operator !=(SystemVersion left, SystemVersion right)
```

#### Parameters

`left` [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

`right` [SystemVersion](GDK.Net.SystemInfo.SystemVersion.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

