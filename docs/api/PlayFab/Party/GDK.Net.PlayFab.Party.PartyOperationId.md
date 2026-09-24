# <a id="GDK_Net_PlayFab_Party_PartyOperationId"></a> Struct PartyOperationId

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Correlates a Party operation with the state change that completes it.

```csharp
public readonly struct PartyOperationId : IEquatable<PartyOperationId>
```

#### Implements

[IEquatable<PartyOperationId\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Party operations start synchronously and report completion on a later pump rather than through
an <code>XAsyncBlock</code>, so they are not awaitable. Every start returns an id, which the matching
completion record echoes back in its <code>Operation</code> property.

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyOperationId_IsValid"></a> IsValid

Whether this id refers to an operation the title started.

```csharp
public bool IsValid { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyOperationId_None"></a> None

An id that matches no operation.

```csharp
public static PartyOperationId None { get; }
```

#### Property Value

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyOperationId_Equals_GDK_Net_PlayFab_Party_PartyOperationId_"></a> Equals\(PartyOperationId\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(PartyOperationId other)
```

#### Parameters

`other` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_PlayFab_Party_PartyOperationId_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_PlayFab_Party_PartyOperationId_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

### <a id="GDK_Net_PlayFab_Party_PartyOperationId_ToString"></a> ToString\(\)

Returns the fully qualified type name of this instance.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The fully qualified type name.

## Operators

### <a id="GDK_Net_PlayFab_Party_PartyOperationId_op_Equality_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyOperationId_"></a> operator ==\(PartyOperationId, PartyOperationId\)

Compares two ids for equality.

```csharp
public static bool operator ==(PartyOperationId left, PartyOperationId right)
```

#### Parameters

`left` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

`right` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyOperationId_op_Inequality_GDK_Net_PlayFab_Party_PartyOperationId_GDK_Net_PlayFab_Party_PartyOperationId_"></a> operator \!=\(PartyOperationId, PartyOperationId\)

Compares two ids for inequality.

```csharp
public static bool operator !=(PartyOperationId left, PartyOperationId right)
```

#### Parameters

`left` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

`right` [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

