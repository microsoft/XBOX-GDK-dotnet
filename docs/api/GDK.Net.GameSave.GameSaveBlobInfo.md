# <a id="GDK_Net_GameSave_GameSaveBlobInfo"></a> Class GameSaveBlobInfo

Namespace: [GDK.Net.GameSave](GDK.Net.GameSave.md)  
Assembly: GDK.Net.dll  

Metadata for a blob within a container returned by enumeration.

```csharp
public sealed class GameSaveBlobInfo
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameSaveBlobInfo](GDK.Net.GameSave.GameSaveBlobInfo.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_GameSave_GameSaveBlobInfo_Name"></a> Name

Unique blob name within its container. Maximum 64 characters (GS_MAX_BLOB_NAME_SIZE).

```csharp
public string Name { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_GameSave_GameSaveBlobInfo_Size"></a> Size

Size of the saved data in bytes. Maximum 16 MB (GS_MAX_BLOB_SIZE).

```csharp
public uint Size { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

## Methods

### <a id="GDK_Net_GameSave_GameSaveBlobInfo_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

