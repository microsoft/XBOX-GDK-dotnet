# <a id="GDK_Net_PlayFab_Party_PartyXblChatUser"></a> Class PartyXblChatUser

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

An Xbox Live user known to the Party Xbox Live extension
(<code>PARTY_XBL_CHAT_USER_HANDLE</code>).

```csharp
public sealed class PartyXblChatUser
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyXblChatUser_AccessibilitySettings"></a> AccessibilitySettings

The user's chat accessibility preferences
(<code>PartyXblLocalChatUserGetAccessibilitySettings</code>). Local users only.

```csharp
public PartyXblAccessibilitySettings AccessibilitySettings { get; }
```

#### Property Value

 [PartyXblAccessibilitySettings](GDK.Net.PlayFab.Party.PartyXblAccessibilitySettings.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblChatUser_CrossNetworkCommunicationPrivacySetting"></a> CrossNetworkCommunicationPrivacySetting

Whether the user is allowed to communicate outside Xbox Live
(<code>PartyXblLocalChatUserGetCrossNetworkCommunicationPrivacySetting</code>). Local users only.

```csharp
public PartyXblCrossNetworkCommunicationPrivacySetting CrossNetworkCommunicationPrivacySetting { get; }
```

#### Property Value

 [PartyXblCrossNetworkCommunicationPrivacySetting](GDK.Net.PlayFab.Party.PartyXblCrossNetworkCommunicationPrivacySetting.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblChatUser_IsLocal"></a> IsLocal

Whether this user is signed in on this device (<code>PartyXblChatUserIsLocal</code>).

```csharp
public bool IsLocal { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_Party_PartyXblChatUser_XboxUserId"></a> XboxUserId

The user's Xbox Live user id (<code>PartyXblChatUserGetXboxUserId</code>).

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

## Methods

### <a id="GDK_Net_PlayFab_Party_PartyXblChatUser_GetRequiredChatPermissionInfo_GDK_Net_PlayFab_Party_PartyXblChatUser_"></a> GetRequiredChatPermissionInfo\(PartyXblChatUser\)

What this local user may hear from and say to another user
(<code>PartyXblLocalChatUserGetRequiredChatPermissionInfo</code>).

```csharp
public PartyXblChatPermissionInfo GetRequiredChatPermissionInfo(PartyXblChatUser targetChatUser)
```

#### Parameters

`targetChatUser` [PartyXblChatUser](GDK.Net.PlayFab.Party.PartyXblChatUser.md)

The user the permission applies to.

#### Returns

 [PartyXblChatPermissionInfo](GDK.Net.PlayFab.Party.PartyXblChatPermissionInfo.md)

### <a id="GDK_Net_PlayFab_Party_PartyXblChatUser_LoginToPlayFab"></a> LoginToPlayFab\(\)

Starts exchanging this Xbox Live user's token for a PlayFab entity
(<code>PartyXblLoginToPlayFab</code>). Completion arrives as
<xref href="GDK.Net.PlayFab.Party.PartyXblLoginToPlayFabCompleted" data-throw-if-not-resolved="false"></xref>. Local users only.

```csharp
public PartyOperationId LoginToPlayFab()
```

#### Returns

 [PartyOperationId](GDK.Net.PlayFab.Party.PartyOperationId.md)

