# <a id="GDK_Net_Accessibility_GameColor"></a> Struct GameColor

Namespace: [GDK.Net.Accessibility](GDK.Net.Accessibility.md)  
Assembly: GDK.Net.dll  

An ARGB colour used in closed-caption properties.
Mirrors the <code>XColor</code> union from XGameRuntimeTypes.h.

```csharp
public readonly struct GameColor : IEquatable<GameColor>
```

#### Implements

[IEquatable<GameColor\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GDK_Net_Accessibility_GameColor__ctor_System_Byte_System_Byte_System_Byte_System_Byte_"></a> GameColor\(byte, byte, byte, byte\)

Initializes a colour from individual channel bytes.

```csharp
public GameColor(byte a, byte r, byte g, byte b)
```

#### Parameters

`a` [byte](https://learn.microsoft.com/dotnet/api/system.byte)

`r` [byte](https://learn.microsoft.com/dotnet/api/system.byte)

`g` [byte](https://learn.microsoft.com/dotnet/api/system.byte)

`b` [byte](https://learn.microsoft.com/dotnet/api/system.byte)

### <a id="GDK_Net_Accessibility_GameColor__ctor_System_UInt32_"></a> GameColor\(uint\)

Initializes a colour from a packed ARGB <code>uint</code>.

```csharp
public GameColor(uint argb)
```

#### Parameters

`argb` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

## Properties

### <a id="GDK_Net_Accessibility_GameColor_A"></a> A

Alpha channel (0 = fully transparent, 255 = fully opaque).

```csharp
public byte A { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)

### <a id="GDK_Net_Accessibility_GameColor_B"></a> B

Blue channel.

```csharp
public byte B { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)

### <a id="GDK_Net_Accessibility_GameColor_G"></a> G

Green channel.

```csharp
public byte G { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)

### <a id="GDK_Net_Accessibility_GameColor_PackedValue"></a> PackedValue

The colour packed as a little-endian ARGB <code>uint</code> (matching the native wire format).

```csharp
public uint PackedValue { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_Accessibility_GameColor_R"></a> R

Red channel.

```csharp
public byte R { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)

## Methods

### <a id="GDK_Net_Accessibility_GameColor_Equals_GDK_Net_Accessibility_GameColor_"></a> Equals\(GameColor\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(GameColor other)
```

#### Parameters

`other` [GameColor](GDK.Net.Accessibility.GameColor.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_Accessibility_GameColor_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_Accessibility_GameColor_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_Accessibility_GameColor_ToString"></a> ToString\(\)

Returns the colour formatted as <code>#AARRGGBB</code>.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Operators

### <a id="GDK_Net_Accessibility_GameColor_op_Equality_GDK_Net_Accessibility_GameColor_GDK_Net_Accessibility_GameColor_"></a> operator ==\(GameColor, GameColor\)

Equality operator.

```csharp
public static bool operator ==(GameColor left, GameColor right)
```

#### Parameters

`left` [GameColor](GDK.Net.Accessibility.GameColor.md)

`right` [GameColor](GDK.Net.Accessibility.GameColor.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Accessibility_GameColor_op_Inequality_GDK_Net_Accessibility_GameColor_GDK_Net_Accessibility_GameColor_"></a> operator \!=\(GameColor, GameColor\)

Inequality operator.

```csharp
public static bool operator !=(GameColor left, GameColor right)
```

#### Parameters

`left` [GameColor](GDK.Net.Accessibility.GameColor.md)

`right` [GameColor](GDK.Net.Accessibility.GameColor.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

