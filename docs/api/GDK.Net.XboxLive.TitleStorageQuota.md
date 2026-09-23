# <a id="GDK_Net_XboxLive_TitleStorageQuota"></a> Struct TitleStorageQuota

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

How much title storage quota is used and available, in bytes.

```csharp
public readonly struct TitleStorageQuota : IEquatable<TitleStorageQuota>
```

#### Implements

[IEquatable<TitleStorageQuota\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_TitleStorageQuota_QuotaBytes"></a> QuotaBytes

Soft quota for the requested title storage area. The service may report usage above this
value.

```csharp
public ulong QuotaBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_XboxLive_TitleStorageQuota_UsedBytes"></a> UsedBytes

Bytes currently used in the requested title storage area.

```csharp
public ulong UsedBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_TitleStorageQuota_Equals_GDK_Net_XboxLive_TitleStorageQuota_"></a> Equals\(TitleStorageQuota\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(TitleStorageQuota other)
```

#### Parameters

`other` [TitleStorageQuota](GDK.Net.XboxLive.TitleStorageQuota.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_XboxLive_TitleStorageQuota_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_XboxLive_TitleStorageQuota_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

## Operators

### <a id="GDK_Net_XboxLive_TitleStorageQuota_op_Equality_GDK_Net_XboxLive_TitleStorageQuota_GDK_Net_XboxLive_TitleStorageQuota_"></a> operator ==\(TitleStorageQuota, TitleStorageQuota\)

Equality operator.

```csharp
public static bool operator ==(TitleStorageQuota left, TitleStorageQuota right)
```

#### Parameters

`left` [TitleStorageQuota](GDK.Net.XboxLive.TitleStorageQuota.md)

`right` [TitleStorageQuota](GDK.Net.XboxLive.TitleStorageQuota.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_TitleStorageQuota_op_Inequality_GDK_Net_XboxLive_TitleStorageQuota_GDK_Net_XboxLive_TitleStorageQuota_"></a> operator \!=\(TitleStorageQuota, TitleStorageQuota\)

Inequality operator.

```csharp
public static bool operator !=(TitleStorageQuota left, TitleStorageQuota right)
```

#### Parameters

`left` [TitleStorageQuota](GDK.Net.XboxLive.TitleStorageQuota.md)

`right` [TitleStorageQuota](GDK.Net.XboxLive.TitleStorageQuota.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

