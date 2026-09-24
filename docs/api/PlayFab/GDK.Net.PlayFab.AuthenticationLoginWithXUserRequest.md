# <a id="GDK_Net_PlayFab_AuthenticationLoginWithXUserRequest"></a> Class AuthenticationLoginWithXUserRequest

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFAuthenticationLoginWithXUserRequest</code>.

```csharp
public sealed class AuthenticationLoginWithXUserRequest
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticationLoginWithXUserRequest](GDK.Net.PlayFab.AuthenticationLoginWithXUserRequest.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_AuthenticationLoginWithXUserRequest_CreateAccount"></a> CreateAccount

<code>CreateAccount</code>.

```csharp
public bool CreateAccount { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_AuthenticationLoginWithXUserRequest_CustomTags"></a> CustomTags

<code>CustomTags</code>.

```csharp
public IReadOnlyDictionary<string, string>? CustomTags { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_AuthenticationLoginWithXUserRequest_InfoRequestParameters"></a> InfoRequestParameters

<code>InfoRequestParameters</code>.

```csharp
public GetPlayerCombinedInfoRequestParams? InfoRequestParameters { get; set; }
```

#### Property Value

 [GetPlayerCombinedInfoRequestParams](GDK.Net.PlayFab.GetPlayerCombinedInfoRequestParams.md)?

### <a id="GDK_Net_PlayFab_AuthenticationLoginWithXUserRequest_PlayerSecret"></a> PlayerSecret

<code>PlayerSecret</code>.

```csharp
public string? PlayerSecret { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_AuthenticationLoginWithXUserRequest_User"></a> User

The Xbox user this request is made on behalf of.

```csharp
public User? User { get; set; }
```

#### Property Value

 [User](../Users/GDK.Net.Users.User.md)?

