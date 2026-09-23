# <a id="GDK_Net_Package_PackageChunkSelector"></a> Struct PackageChunkSelector

Namespace: [GDK.Net.Package](GDK.Net.Package.md)  
Assembly: GDK.Net.dll  

A selector that identifies one or more chunks within a package by type and value.
Mirrors <code>XPackageChunkSelector</code>. Use the factory methods to construct.

```csharp
public readonly struct PackageChunkSelector : IEquatable<PackageChunkSelector>
```

#### Implements

[IEquatable<PackageChunkSelector\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Package_PackageChunkSelector_ChunkId"></a> ChunkId

The chunk id for <xref href="GDK.Net.Package.PackageChunkSelectorType.Chunk" data-throw-if-not-resolved="false"></xref> selectors.

```csharp
public uint ChunkId { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Package_PackageChunkSelector_StringValue"></a> StringValue

The string value for <xref href="GDK.Net.Package.PackageChunkSelectorType.Language" data-throw-if-not-resolved="false"></xref>,
    <xref href="GDK.Net.Package.PackageChunkSelectorType.Tag" data-throw-if-not-resolved="false"></xref>, or <xref href="GDK.Net.Package.PackageChunkSelectorType.Feature" data-throw-if-not-resolved="false"></xref> selectors.

```csharp
public string? StringValue { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_Package_PackageChunkSelector_Type"></a> Type

The selector category.

```csharp
public PackageChunkSelectorType Type { get; }
```

#### Property Value

 [PackageChunkSelectorType](GDK.Net.Package.PackageChunkSelectorType.md)

## Methods

### <a id="GDK_Net_Package_PackageChunkSelector_ByChunkId_System_UInt32_"></a> ByChunkId\(uint\)

Creates a selector that picks a single chunk by its numeric id.

```csharp
public static PackageChunkSelector ByChunkId(uint chunkId)
```

#### Parameters

`chunkId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

#### Returns

 [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

### <a id="GDK_Net_Package_PackageChunkSelector_ByFeature_System_String_"></a> ByFeature\(string\)

Creates a selector that picks chunks belonging to a named feature.

```csharp
public static PackageChunkSelector ByFeature(string feature)
```

#### Parameters

`feature` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

### <a id="GDK_Net_Package_PackageChunkSelector_ByLanguage_System_String_"></a> ByLanguage\(string\)

Creates a selector that picks chunks by installed language tag (e.g. <code>"en-US"</code>).

```csharp
public static PackageChunkSelector ByLanguage(string language)
```

#### Parameters

`language` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

### <a id="GDK_Net_Package_PackageChunkSelector_ByTag_System_String_"></a> ByTag\(string\)

Creates a selector that picks chunks by tag string.

```csharp
public static PackageChunkSelector ByTag(string tag)
```

#### Parameters

`tag` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

### <a id="GDK_Net_Package_PackageChunkSelector_Equals_GDK_Net_Package_PackageChunkSelector_"></a> Equals\(PackageChunkSelector\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(PackageChunkSelector other)
```

#### Parameters

`other` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_Package_PackageChunkSelector_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_Package_PackageChunkSelector_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_Package_PackageChunkSelector_ToString"></a> ToString\(\)

Returns the fully qualified type name of this instance.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The fully qualified type name.

## Operators

### <a id="GDK_Net_Package_PackageChunkSelector_op_Equality_GDK_Net_Package_PackageChunkSelector_GDK_Net_Package_PackageChunkSelector_"></a> operator ==\(PackageChunkSelector, PackageChunkSelector\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public static bool operator ==(PackageChunkSelector l, PackageChunkSelector r)
```

#### Parameters

`l` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

`r` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_Package_PackageChunkSelector_op_Inequality_GDK_Net_Package_PackageChunkSelector_GDK_Net_Package_PackageChunkSelector_"></a> operator \!=\(PackageChunkSelector, PackageChunkSelector\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public static bool operator !=(PackageChunkSelector l, PackageChunkSelector r)
```

#### Parameters

`l` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

`r` [PackageChunkSelector](GDK.Net.Package.PackageChunkSelector.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

