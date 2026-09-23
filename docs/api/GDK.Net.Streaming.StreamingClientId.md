# <a id="GDK_Net_Streaming_StreamingClientId"></a> Struct StreamingClientId

Namespace: [GDK.Net.Streaming](GDK.Net.Streaming.md)  
Assembly: GDK.Net.dll  

Identifies a streaming client connected to the game. Wraps the native
<code>XGameStreamingClientId</code> (a <code>uint64_t</code>).

```csharp
public readonly struct StreamingClientId : IEquatable<StreamingClientId>
```

#### Implements

[IEquatable<StreamingClientId\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Streaming_StreamingClientId_IsNull"></a> IsNull

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this is the null (unset) client id.

```csharp
public bool IsNull { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Streaming_StreamingClientId_Null"></a> Null

The null client id (<code>XGameStreamingNullClientId = 0</code>).

```csharp
public static StreamingClientId Null { get; }
```

#### Property Value

 [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

### <a id="GDK_Net_Streaming_StreamingClientId_Value"></a> Value

The raw 64-bit client id.

```csharp
public ulong Value { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_Streaming_StreamingClientId_Equals_GDK_Net_Streaming_StreamingClientId_"></a> Equals\(StreamingClientId\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(StreamingClientId other)
```

#### Parameters

`other` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_Streaming_StreamingClientId_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_Streaming_StreamingClientId_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_Streaming_StreamingClientId_ToString"></a> ToString\(\)

Returns the client id formatted as a hexadecimal diagnostic string.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Operators

### <a id="GDK_Net_Streaming_StreamingClientId_op_Equality_GDK_Net_Streaming_StreamingClientId_GDK_Net_Streaming_StreamingClientId_"></a> operator ==\(StreamingClientId, StreamingClientId\)

Equality operator.

```csharp
public static bool operator ==(StreamingClientId left, StreamingClientId right)
```

#### Parameters

`left` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

`right` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_Streaming_StreamingClientId_op_Inequality_GDK_Net_Streaming_StreamingClientId_GDK_Net_Streaming_StreamingClientId_"></a> operator \!=\(StreamingClientId, StreamingClientId\)

Inequality operator.

```csharp
public static bool operator !=(StreamingClientId left, StreamingClientId right)
```

#### Parameters

`left` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

`right` [StreamingClientId](GDK.Net.Streaming.StreamingClientId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

