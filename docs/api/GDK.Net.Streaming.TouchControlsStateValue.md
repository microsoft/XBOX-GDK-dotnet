# <a id="GDK_Net_Streaming_TouchControlsStateValue"></a> Struct TouchControlsStateValue

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

A typed value for a touch-controls state operation.
Use the static factory methods <xref href="GDK.Net.Streaming.TouchControlsStateValue.FromBoolean(System.Boolean)" data-throw-if-not-resolved="false"></xref>, <xref href="GDK.Net.Streaming.TouchControlsStateValue.FromInteger(System.Int64)" data-throw-if-not-resolved="false"></xref>,
<xref href="GDK.Net.Streaming.TouchControlsStateValue.FromDouble(System.Double)" data-throw-if-not-resolved="false"></xref>, and <xref href="GDK.Net.Streaming.TouchControlsStateValue.FromString(System.String)" data-throw-if-not-resolved="false"></xref> to construct instances.

```csharp
public readonly struct TouchControlsStateValue
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Streaming_TouchControlsStateValue_BooleanValue"></a> BooleanValue

The boolean value when <xref href="GDK.Net.Streaming.TouchControlsStateValue.Kind" data-throw-if-not-resolved="false"></xref> is <xref href="GDK.Net.Streaming.TouchControlsStateValueKind.Boolean" data-throw-if-not-resolved="false"></xref>.

```csharp
public bool BooleanValue { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Streaming_TouchControlsStateValue_DoubleValue"></a> DoubleValue

The double value when <xref href="GDK.Net.Streaming.TouchControlsStateValue.Kind" data-throw-if-not-resolved="false"></xref> is <xref href="GDK.Net.Streaming.TouchControlsStateValueKind.Double" data-throw-if-not-resolved="false"></xref>.

```csharp
public double DoubleValue { get; }
```

#### Property Value

 [double](https://learn.microsoft.com/dotnet/api/system.double)

### <a id="GDK_Net_Streaming_TouchControlsStateValue_IntegerValue"></a> IntegerValue

The integer value when <xref href="GDK.Net.Streaming.TouchControlsStateValue.Kind" data-throw-if-not-resolved="false"></xref> is <xref href="GDK.Net.Streaming.TouchControlsStateValueKind.Integer" data-throw-if-not-resolved="false"></xref>.

```csharp
public long IntegerValue { get; }
```

#### Property Value

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

### <a id="GDK_Net_Streaming_TouchControlsStateValue_Kind"></a> Kind

The kind of value stored.

```csharp
public TouchControlsStateValueKind Kind { get; }
```

#### Property Value

 [TouchControlsStateValueKind](GDK.Net.Streaming.TouchControlsStateValueKind.md)

### <a id="GDK_Net_Streaming_TouchControlsStateValue_StringValue"></a> StringValue

The string value when <xref href="GDK.Net.Streaming.TouchControlsStateValue.Kind" data-throw-if-not-resolved="false"></xref> is <xref href="GDK.Net.Streaming.TouchControlsStateValueKind.String" data-throw-if-not-resolved="false"></xref>.

```csharp
public string? StringValue { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="GDK_Net_Streaming_TouchControlsStateValue_FromBoolean_System_Boolean_"></a> FromBoolean\(bool\)

Creates a boolean state value (<code>XGameStreamingTouchControlsStateValueKind::Boolean</code>).

```csharp
public static TouchControlsStateValue FromBoolean(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Returns

 [TouchControlsStateValue](GDK.Net.Streaming.TouchControlsStateValue.md)

### <a id="GDK_Net_Streaming_TouchControlsStateValue_FromDouble_System_Double_"></a> FromDouble\(double\)

Creates a double state value (<code>XGameStreamingTouchControlsStateValueKind::Double</code>).

```csharp
public static TouchControlsStateValue FromDouble(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

#### Returns

 [TouchControlsStateValue](GDK.Net.Streaming.TouchControlsStateValue.md)

### <a id="GDK_Net_Streaming_TouchControlsStateValue_FromInteger_System_Int64_"></a> FromInteger\(long\)

Creates an integer state value (<code>XGameStreamingTouchControlsStateValueKind::Integer</code>).

```csharp
public static TouchControlsStateValue FromInteger(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

#### Returns

 [TouchControlsStateValue](GDK.Net.Streaming.TouchControlsStateValue.md)

### <a id="GDK_Net_Streaming_TouchControlsStateValue_FromString_System_String_"></a> FromString\(string?\)

Creates a string state value (<code>XGameStreamingTouchControlsStateValueKind::String</code>).

```csharp
public static TouchControlsStateValue FromString(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

#### Returns

 [TouchControlsStateValue](GDK.Net.Streaming.TouchControlsStateValue.md)

