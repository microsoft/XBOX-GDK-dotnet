# <a id="GDK_Net_XboxLive_SocialManagerPresenceRecord"></a> Class SocialManagerPresenceRecord

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

A social-manager presence snapshot for one user.

```csharp
public sealed class SocialManagerPresenceRecord
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialManagerPresenceRecord](GDK.Net.XboxLive.SocialManagerPresenceRecord.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_SocialManagerPresenceRecord_TitleRecords"></a> TitleRecords

The title presence records reported for the user.

```csharp
public IReadOnlyList<SocialManagerPresenceTitleRecord> TitleRecords { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SocialManagerPresenceTitleRecord](GDK.Net.XboxLive.SocialManagerPresenceTitleRecord.md)\>

### <a id="GDK_Net_XboxLive_SocialManagerPresenceRecord_UserState"></a> UserState

The user's aggregate presence state.

```csharp
public PresenceUserState UserState { get; }
```

#### Property Value

 [PresenceUserState](GDK.Net.XboxLive.PresenceUserState.md)

## Methods

### <a id="GDK_Net_XboxLive_SocialManagerPresenceRecord_IsUserPlayingTitle_System_UInt32_"></a> IsUserPlayingTitle\(uint\)

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when this snapshot shows the user playing <code class="paramref">titleId</code>.

```csharp
public bool IsUserPlayingTitle(uint titleId)
```

#### Parameters

`titleId` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

The native <code>XblSocialManagerPresenceRecordIsUserPlayingTitle</code> helper is bound for
completeness, but this method evaluates the managed snapshot so it remains safe after the
next <xref href="GDK.Net.XboxLive.SocialManager.DoWork" data-throw-if-not-resolved="false"></xref> call invalidates native event memory.

