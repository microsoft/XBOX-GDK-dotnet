# <a id="GDK_Net_GameSave_GameSaveBlob"></a> Class GameSaveBlob

Namespace: [GDK.Net.GameSave](GDK.Net.GameSave.md)  
Assembly: GDK.Net.dll  

A blob read from a container: the name, expected size, and raw data bytes.

```csharp
public sealed class GameSaveBlob
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameSaveBlob](GDK.Net.GameSave.GameSaveBlob.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<xref href="GDK.Net.GameSave.GameSaveBlob.Data" data-throw-if-not-resolved="false"></xref> is a deep copy made before the native buffer was released; it is safe to
hold and inspect after the read operation completes.

## Properties

### <a id="GDK_Net_GameSave_GameSaveBlob_Data"></a> Data

Raw blob data.

```csharp
public byte[] Data { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="GDK_Net_GameSave_GameSaveBlob_Name"></a> Name

Blob name.

```csharp
public string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_GameSave_GameSaveBlob_Size"></a> Size

Declared size in bytes (matches <code>Data.Length</code>).

```csharp
public uint Size { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

## Methods

### <a id="GDK_Net_GameSave_GameSaveBlob_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

