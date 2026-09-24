# <a id="GDK_Net_XboxLive_TitleManagedStatisticValue"></a> Struct TitleManagedStatisticValue

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Immutable discriminated value for a title-managed statistic.

```csharp
public readonly struct TitleManagedStatisticValue : IEquatable<TitleManagedStatisticValue>
```

#### Implements

[IEquatable<TitleManagedStatisticValue\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

XSAPI carries both native payload fields in <code>XblTitleManagedStatistic</code> and uses the
<code>statisticType</code> tag to select one. This type keeps the same tag but exposes only the active
arm to callers.

## Properties

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_NumberValue"></a> NumberValue

The numeric value.

```csharp
public double NumberValue { get; }
```

#### Property Value

 [double](https://learn.microsoft.com/dotnet/api/system.double)

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<xref href="GDK.Net.XboxLive.TitleManagedStatisticValue.Type" data-throw-if-not-resolved="false"></xref> is not <xref href="GDK.Net.XboxLive.TitleManagedStatType.Number" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_StringValue"></a> StringValue

The string value.

```csharp
public string StringValue { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

<xref href="GDK.Net.XboxLive.TitleManagedStatisticValue.Type" data-throw-if-not-resolved="false"></xref> is not <xref href="GDK.Net.XboxLive.TitleManagedStatType.String" data-throw-if-not-resolved="false"></xref>.

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_Type"></a> Type

The active value arm.

```csharp
public TitleManagedStatType Type { get; }
```

#### Property Value

 [TitleManagedStatType](GDK.Net.XboxLive.TitleManagedStatType.md)

## Methods

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_Equals_GDK_Net_XboxLive_TitleManagedStatisticValue_"></a> Equals\(TitleManagedStatisticValue\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(TitleManagedStatisticValue other)
```

#### Parameters

`other` [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_FromNumber_System_Double_"></a> FromNumber\(double\)

Creates a numeric statistic value.

```csharp
public static TitleManagedStatisticValue FromNumber(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

#### Returns

 [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_FromString_System_String_"></a> FromString\(string\)

Creates a string statistic value.

```csharp
public static TitleManagedStatisticValue FromString(string value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">value</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_ToString"></a> ToString\(\)

Returns the fully qualified type name of this instance.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The fully qualified type name.

## Operators

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_op_Equality_GDK_Net_XboxLive_TitleManagedStatisticValue_GDK_Net_XboxLive_TitleManagedStatisticValue_"></a> operator ==\(TitleManagedStatisticValue, TitleManagedStatisticValue\)

Equality operator.

```csharp
public static bool operator ==(TitleManagedStatisticValue left, TitleManagedStatisticValue right)
```

#### Parameters

`left` [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

`right` [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_TitleManagedStatisticValue_op_Inequality_GDK_Net_XboxLive_TitleManagedStatisticValue_GDK_Net_XboxLive_TitleManagedStatisticValue_"></a> operator \!=\(TitleManagedStatisticValue, TitleManagedStatisticValue\)

Inequality operator.

```csharp
public static bool operator !=(TitleManagedStatisticValue left, TitleManagedStatisticValue right)
```

#### Parameters

`left` [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

`right` [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

