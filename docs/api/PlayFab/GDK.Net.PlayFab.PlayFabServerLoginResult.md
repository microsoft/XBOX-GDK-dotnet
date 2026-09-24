# <a id="GDK_Net_PlayFab_PlayFabServerLoginResult"></a> Class PlayFabServerLoginResult

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The outcome of a PlayFab server login: the entity token response plus the login payload.

```csharp
public sealed class PlayFabServerLoginResult
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabServerLoginResult](GDK.Net.PlayFab.PlayFabServerLoginResult.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Server logins authenticate with a developer secret key and hand back a token rather than an
entity handle, so there is nothing to dispose.

## Properties

### <a id="GDK_Net_PlayFab_PlayFabServerLoginResult_EntityToken"></a> EntityToken

The entity token the server may use for subsequent calls.

```csharp
public AuthenticationEntityTokenResponse EntityToken { get; }
```

#### Property Value

 [AuthenticationEntityTokenResponse](GDK.Net.PlayFab.AuthenticationEntityTokenResponse.md)

### <a id="GDK_Net_PlayFab_PlayFabServerLoginResult_Result"></a> Result

The login payload PlayFab returned.

```csharp
public AuthenticationLoginResult Result { get; }
```

#### Property Value

 [AuthenticationLoginResult](GDK.Net.PlayFab.AuthenticationLoginResult.md)

