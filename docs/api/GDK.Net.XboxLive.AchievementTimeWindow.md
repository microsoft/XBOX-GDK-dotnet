# <a id="GDK_Net_XboxLive_AchievementTimeWindow"></a> Struct AchievementTimeWindow

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

The window a challenge achievement is available in. Mirrors <code>XblAchievementTimeWindow</code>.

```csharp
public readonly struct AchievementTimeWindow : IEquatable<AchievementTimeWindow>
```

#### Implements

[IEquatable<AchievementTimeWindow\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_AchievementTimeWindow_EndDate"></a> EndDate

When the achievement stops being available.

```csharp
public DateTimeOffset EndDate { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_XboxLive_AchievementTimeWindow_StartDate"></a> StartDate

When the achievement becomes available.

```csharp
public DateTimeOffset StartDate { get; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

## Methods

### <a id="GDK_Net_XboxLive_AchievementTimeWindow_Equals_GDK_Net_XboxLive_AchievementTimeWindow_"></a> Equals\(AchievementTimeWindow\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(AchievementTimeWindow other)
```

#### Parameters

`other` [AchievementTimeWindow](GDK.Net.XboxLive.AchievementTimeWindow.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_XboxLive_AchievementTimeWindow_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_XboxLive_AchievementTimeWindow_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

## Operators

### <a id="GDK_Net_XboxLive_AchievementTimeWindow_op_Equality_GDK_Net_XboxLive_AchievementTimeWindow_GDK_Net_XboxLive_AchievementTimeWindow_"></a> operator ==\(AchievementTimeWindow, AchievementTimeWindow\)

Equality operator.

```csharp
public static bool operator ==(AchievementTimeWindow left, AchievementTimeWindow right)
```

#### Parameters

`left` [AchievementTimeWindow](GDK.Net.XboxLive.AchievementTimeWindow.md)

`right` [AchievementTimeWindow](GDK.Net.XboxLive.AchievementTimeWindow.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_AchievementTimeWindow_op_Inequality_GDK_Net_XboxLive_AchievementTimeWindow_GDK_Net_XboxLive_AchievementTimeWindow_"></a> operator \!=\(AchievementTimeWindow, AchievementTimeWindow\)

Inequality operator.

```csharp
public static bool operator !=(AchievementTimeWindow left, AchievementTimeWindow right)
```

#### Parameters

`left` [AchievementTimeWindow](GDK.Net.XboxLive.AchievementTimeWindow.md)

`right` [AchievementTimeWindow](GDK.Net.XboxLive.AchievementTimeWindow.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

