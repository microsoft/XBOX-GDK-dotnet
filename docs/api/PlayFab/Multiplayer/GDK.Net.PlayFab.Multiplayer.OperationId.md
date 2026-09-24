# <a id="GDK_Net_PlayFab_Multiplayer_OperationId"></a> Struct OperationId

Namespace: [GDK.Net.PlayFab.Multiplayer](GDK.Net.PlayFab.Multiplayer.md)  
Assembly: GDK.Net.dll  

Correlates a multiplayer operation with the state change that completes it.

```csharp
public readonly struct OperationId : IEquatable<OperationId>
```

#### Implements

[IEquatable<OperationId\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

PFMP operations start synchronously and report completion on a later pump rather than through
an <code>XAsyncBlock</code>, so they are not awaitable. Every start returns an id, which the matching
completion record echoes back in its <code>Operation</code> property.

## Properties

### <a id="GDK_Net_PlayFab_Multiplayer_OperationId_IsValid"></a> IsValid

Whether this id refers to an operation the title started.

```csharp
public bool IsValid { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_OperationId_None"></a> None

An id that matches no operation.

```csharp
public static OperationId None { get; }
```

#### Property Value

 [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

## Methods

### <a id="GDK_Net_PlayFab_Multiplayer_OperationId_Equals_GDK_Net_PlayFab_Multiplayer_OperationId_"></a> Equals\(OperationId\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(OperationId other)
```

#### Parameters

`other` [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_PlayFab_Multiplayer_OperationId_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_PlayFab_Multiplayer_OperationId_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_PlayFab_Multiplayer_OperationId_ToString"></a> ToString\(\)

Returns the fully qualified type name of this instance.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The fully qualified type name.

## Operators

### <a id="GDK_Net_PlayFab_Multiplayer_OperationId_op_Equality_GDK_Net_PlayFab_Multiplayer_OperationId_GDK_Net_PlayFab_Multiplayer_OperationId_"></a> operator ==\(OperationId, OperationId\)

Compares two ids for equality.

```csharp
public static bool operator ==(OperationId left, OperationId right)
```

#### Parameters

`left` [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

`right` [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Multiplayer_OperationId_op_Inequality_GDK_Net_PlayFab_Multiplayer_OperationId_GDK_Net_PlayFab_Multiplayer_OperationId_"></a> operator \!=\(OperationId, OperationId\)

Compares two ids for inequality.

```csharp
public static bool operator !=(OperationId left, OperationId right)
```

#### Parameters

`left` [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

`right` [OperationId](GDK.Net.PlayFab.Multiplayer.OperationId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

