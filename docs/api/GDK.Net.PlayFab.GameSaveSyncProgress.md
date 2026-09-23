# <a id="GDK_Net_PlayFab_GameSaveSyncProgress"></a> Struct GameSaveSyncProgress

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

How far a PlayFab game save sync has progressed
(<code>PFGameSaveFilesUiProgressGetProgress</code>).

```csharp
public readonly struct GameSaveSyncProgress : IEquatable<GameSaveSyncProgress>
```

#### Implements

[IEquatable<GameSaveSyncProgress\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_GameSaveSyncProgress_CurrentBytes"></a> CurrentBytes

Bytes transferred so far.

```csharp
public ulong CurrentBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_PlayFab_GameSaveSyncProgress_Fraction"></a> Fraction

The completed fraction in the range 0 to 1, or zero when the total is unknown.

```csharp
public double Fraction { get; }
```

#### Property Value

 [double](https://learn.microsoft.com/dotnet/api/system.double)

### <a id="GDK_Net_PlayFab_GameSaveSyncProgress_State"></a> State

The stage the sync is currently in.

```csharp
public GameSaveFilesSyncState State { get; }
```

#### Property Value

 [GameSaveFilesSyncState](GDK.Net.PlayFab.GameSaveFilesSyncState.md)

### <a id="GDK_Net_PlayFab_GameSaveSyncProgress_TotalBytes"></a> TotalBytes

Bytes the sync will transfer in total, or zero when it is not yet known.

```csharp
public ulong TotalBytes { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_PlayFab_GameSaveSyncProgress_Equals_GDK_Net_PlayFab_GameSaveSyncProgress_"></a> Equals\(GameSaveSyncProgress\)

Indicates whether the current object is equal to another object of the same type.

```csharp
public bool Equals(GameSaveSyncProgress other)
```

#### Parameters

`other` [GameSaveSyncProgress](GDK.Net.PlayFab.GameSaveSyncProgress.md)

An object to compare with this object.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the current object is equal to the <code class="paramref">other</code> parameter; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GDK_Net_PlayFab_GameSaveSyncProgress_Equals_System_Object_"></a> Equals\(object?\)

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

### <a id="GDK_Net_PlayFab_GameSaveSyncProgress_GetHashCode"></a> GetHashCode\(\)

Returns the hash code for this instance.

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A 32-bit signed integer that is the hash code for this instance.

## Operators

### <a id="GDK_Net_PlayFab_GameSaveSyncProgress_op_Equality_GDK_Net_PlayFab_GameSaveSyncProgress_GDK_Net_PlayFab_GameSaveSyncProgress_"></a> operator ==\(GameSaveSyncProgress, GameSaveSyncProgress\)

Compares two progress snapshots for equality.

```csharp
public static bool operator ==(GameSaveSyncProgress left, GameSaveSyncProgress right)
```

#### Parameters

`left` [GameSaveSyncProgress](GDK.Net.PlayFab.GameSaveSyncProgress.md)

`right` [GameSaveSyncProgress](GDK.Net.PlayFab.GameSaveSyncProgress.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_GameSaveSyncProgress_op_Inequality_GDK_Net_PlayFab_GameSaveSyncProgress_GDK_Net_PlayFab_GameSaveSyncProgress_"></a> operator \!=\(GameSaveSyncProgress, GameSaveSyncProgress\)

Compares two progress snapshots for inequality.

```csharp
public static bool operator !=(GameSaveSyncProgress left, GameSaveSyncProgress right)
```

#### Parameters

`left` [GameSaveSyncProgress](GDK.Net.PlayFab.GameSaveSyncProgress.md)

`right` [GameSaveSyncProgress](GDK.Net.PlayFab.GameSaveSyncProgress.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

