# <a id="GDK_Net_XboxLive_UserProfile"></a> Class UserProfile

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

A user's Xbox Live profile. Managed snapshot of <code>XblUserProfile</code>.

```csharp
public sealed class UserProfile
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[UserProfile](GDK.Net.XboxLive.UserProfile.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The native struct carries fixed-size inline UTF-8 buffers rather than pointers, so this type is
a straight copy and stays valid indefinitely: nothing here points into runtime-owned memory.

## Properties

### <a id="GDK_Net_XboxLive_UserProfile_AppDisplayName"></a> AppDisplayName

Display name for application UI. Always equal to <xref href="GDK.Net.XboxLive.UserProfile.GameDisplayName" data-throw-if-not-resolved="false"></xref>.

```csharp
public string AppDisplayName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_UserProfile_AppDisplayPictureUri"></a> AppDisplayPictureUri

Resizable gamer-picture URI for application UI. Append
<code>&amp;format=png&amp;w={width}&amp;h={height}</code>: 64, 208 and 424 are the supported sizes.

```csharp
public string AppDisplayPictureUri { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_UserProfile_GameDisplayName"></a> GameDisplayName

Display name for in-game UI. Always equal to <xref href="GDK.Net.XboxLive.UserProfile.AppDisplayName" data-throw-if-not-resolved="false"></xref>.

```csharp
public string GameDisplayName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_UserProfile_GameDisplayPictureUri"></a> GameDisplayPictureUri

Resizable gamer-picture URI for in-game UI. See <xref href="GDK.Net.XboxLive.UserProfile.AppDisplayPictureUri" data-throw-if-not-resolved="false"></xref>.

```csharp
public string GameDisplayPictureUri { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_UserProfile_Gamerscore"></a> Gamerscore

The user's gamerscore, as the service formats it.

```csharp
public string Gamerscore { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_UserProfile_Gamertag"></a> Gamertag

The classic gamertag: ASCII only, with no suffix.

```csharp
public string Gamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_UserProfile_ModernGamertag"></a> ModernGamertag

The modern gamertag, with no suffix. Not guaranteed unique.

```csharp
public string ModernGamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_UserProfile_ModernGamertagSuffix"></a> ModernGamertagSuffix

The numeric suffix that makes <xref href="GDK.Net.XboxLive.UserProfile.ModernGamertag" data-throw-if-not-resolved="false"></xref> unique. May be empty.

```csharp
public string ModernGamertagSuffix { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_UserProfile_UniqueModernGamertag"></a> UniqueModernGamertag

The unique modern gamertag, formatted <code>modernGamertag#suffix</code>.

```csharp
public string UniqueModernGamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_UserProfile_XboxUserId"></a> XboxUserId

The user's Xbox user id.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_UserProfile_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

