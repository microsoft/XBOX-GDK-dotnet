# <a id="GDK_Net_PlayFab_FriendsClientGetFriendsListRequest"></a> Class FriendsClientGetFriendsListRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFFriendsClientGetFriendsListRequest</code>.

```csharp
public sealed class FriendsClientGetFriendsListRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[FriendsClientGetFriendsListRequest](GDK.Net.PlayFab.FriendsClientGetFriendsListRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_FriendsClientGetFriendsListRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_FriendsClientGetFriendsListRequest_ExternalPlatformFriends"></a> ExternalPlatformFriends

<code>ExternalPlatformFriends</code>.

```csharp
public FriendsExternalFriendSources? ExternalPlatformFriends { get; set; }
```

#### Property Value

 [FriendsExternalFriendSources](GDK.Net.PlayFab.FriendsExternalFriendSources.md)?

### <a id="GDK_Net_PlayFab_FriendsClientGetFriendsListRequest_ProfileConstraints"></a> ProfileConstraints

<code>ProfileConstraints</code>.

```csharp
public PlayerProfileViewConstraints? ProfileConstraints { get; set; }
```

#### Property Value

 [PlayerProfileViewConstraints](GDK.Net.PlayFab.PlayerProfileViewConstraints.md)?

### <a id="GDK_Net_PlayFab_FriendsClientGetFriendsListRequest_User"></a> User

The Xbox user this request is made on behalf of.

```csharp
public User? User { get; set; }
```

#### Property Value

 [User](../Users/GDK.Net.Users.User.md)?

