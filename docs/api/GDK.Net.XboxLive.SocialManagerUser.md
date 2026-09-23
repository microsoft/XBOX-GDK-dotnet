# <a id="GDK_Net_XboxLive_SocialManagerUser"></a> Class SocialManagerUser

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

An Xbox user in the social-manager graph. Managed snapshot of <code>XblSocialManagerUser</code>.

```csharp
public sealed class SocialManagerUser
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManagerUser](GDK.Net.XboxLive.SocialManagerUser.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_SocialManagerUser_DisplayName"></a> DisplayName

The user's display name, when XSAPI returned one.

```csharp
public string DisplayName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialManagerUser_DisplayPictureUri"></a> DisplayPictureUri

The raw display-picture URI.

```csharp
public string DisplayPictureUri { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialManagerUser_Gamerscore"></a> Gamerscore

The user's gamerscore as a formatted string.

```csharp
public string Gamerscore { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialManagerUser_Gamertag"></a> Gamertag

The user's classic gamertag.

```csharp
public string Gamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialManagerUser_IsFavorite"></a> IsFavorite

Whether the user is marked as a favorite.

```csharp
public bool IsFavorite { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialManagerUser_IsFollowedByCaller"></a> IsFollowedByCaller

Compatibility field derived by XSAPI from <xref href="GDK.Net.XboxLive.SocialManagerUser.IsFriend" data-throw-if-not-resolved="false"></xref>.

```csharp
public bool IsFollowedByCaller { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialManagerUser_IsFollowingUser"></a> IsFollowingUser

Compatibility field derived by XSAPI from <xref href="GDK.Net.XboxLive.SocialManagerUser.IsFriend" data-throw-if-not-resolved="false"></xref>.

```csharp
public bool IsFollowingUser { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialManagerUser_IsFriend"></a> IsFriend

Whether this user is a friend of the local user.

```csharp
public bool IsFriend { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialManagerUser_ModernGamertag"></a> ModernGamertag

The user's modern gamertag, without suffix.

```csharp
public string ModernGamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialManagerUser_ModernGamertagSuffix"></a> ModernGamertagSuffix

The suffix that disambiguates <xref href="GDK.Net.XboxLive.SocialManagerUser.ModernGamertag" data-throw-if-not-resolved="false"></xref>.

```csharp
public string ModernGamertagSuffix { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialManagerUser_PreferredColor"></a> PreferredColor

The user's preferred color snapshot.

```csharp
public SocialManagerPreferredColor PreferredColor { get; }
```

#### Property Value

 [SocialManagerPreferredColor](GDK.Net.XboxLive.SocialManagerPreferredColor.md)

### <a id="GDK_Net_XboxLive_SocialManagerUser_PresenceRecord"></a> PresenceRecord

The user's presence snapshot.

```csharp
public SocialManagerPresenceRecord PresenceRecord { get; }
```

#### Property Value

 [SocialManagerPresenceRecord](GDK.Net.XboxLive.SocialManagerPresenceRecord.md)

### <a id="GDK_Net_XboxLive_SocialManagerUser_RealName"></a> RealName

The user's real name, when available to the title.

```csharp
public string RealName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialManagerUser_TitleHistory"></a> TitleHistory

The user's title-history snapshot.

```csharp
public SocialManagerTitleHistory TitleHistory { get; }
```

#### Property Value

 [SocialManagerTitleHistory](GDK.Net.XboxLive.SocialManagerTitleHistory.md)

### <a id="GDK_Net_XboxLive_SocialManagerUser_UniqueModernGamertag"></a> UniqueModernGamertag

The modern gamertag and suffix combined.

```csharp
public string UniqueModernGamertag { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_XboxLive_SocialManagerUser_UseAvatar"></a> UseAvatar

Whether the shell avatar should be used.

```csharp
public bool UseAvatar { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialManagerUser_XboxUserId"></a> XboxUserId

The user's Xbox user id.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_XboxLive_SocialManagerUser_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

