# <a id="GDK_Net_Package_PackageVersion"></a> Struct PackageVersion

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

A GDK package version. Mirrors the four <code>uint16_t</code> fields of <code>XVersion</code>.

```csharp
public readonly struct PackageVersion : IEquatable<PackageVersion>
```

#### Implements

[IEquatable<PackageVersion\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_Package_PackageVersion__ctor_System_UInt16_System_UInt16_System_UInt16_System_UInt16_"></a> PackageVersion\(ushort, ushort, ushort, ushort\)

Constructs a version from its four components.

```csharp
public PackageVersion(ushort major, ushort minor, ushort build, ushort revision)
```

#### Parameters

`major` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

`minor` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

`build` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

`revision` [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

## Properties

### <a id="GDK_Net_Package_PackageVersion_Build"></a> Build

Gets the value of the build component of the version number for the current <xref href="System.Version" data-throw-if-not-resolved="false"></xref> object.

```csharp
public ushort Build { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_Package_PackageVersion_Major"></a> Major

Gets the value of the major component of the version number for the current <xref href="System.Version" data-throw-if-not-resolved="false"></xref> object.

```csharp
public ushort Major { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_Package_PackageVersion_Minor"></a> Minor

Gets the value of the minor component of the version number for the current <xref href="System.Version" data-throw-if-not-resolved="false"></xref> object.

```csharp
public ushort Minor { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

### <a id="GDK_Net_Package_PackageVersion_Revision"></a> Revision

Gets the value of the revision component of the version number for the current <xref href="System.Version" data-throw-if-not-resolved="false"></xref> object.

```csharp
public ushort Revision { get; }
```

#### Property Value

 [ushort](https://learn.microsoft.com/dotnet/api/system.uint16)

## Methods

### <a id="GDK_Net_Package_PackageVersion_Equals_GDK_Net_Package_PackageVersion_"></a> Equals\(PackageVersion\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(PackageVersion other)
```

#### Parameters

`other` [PackageVersion](GDK.Net.Package.PackageVersion.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_Package_PackageVersion_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_Package_PackageVersion_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_Package_PackageVersion_ToString"></a> ToString\(\)

Returns the fully qualified type name of this instance.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The fully qualified type name.

## Operators

### <a id="GDK_Net_Package_PackageVersion_op_Equality_GDK_Net_Package_PackageVersion_GDK_Net_Package_PackageVersion_"></a> operator ==\(PackageVersion, PackageVersion\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public static bool operator ==(PackageVersion l, PackageVersion r)
```

#### Parameters

`l` [PackageVersion](GDK.Net.Package.PackageVersion.md)

`r` [PackageVersion](GDK.Net.Package.PackageVersion.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_Package_PackageVersion_op_Inequality_GDK_Net_Package_PackageVersion_GDK_Net_Package_PackageVersion_"></a> operator \!=\(PackageVersion, PackageVersion\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public static bool operator !=(PackageVersion l, PackageVersion r)
```

#### Parameters

`l` [PackageVersion](GDK.Net.Package.PackageVersion.md)

`r` [PackageVersion](GDK.Net.Package.PackageVersion.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

