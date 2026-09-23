# <a id="GDK_Net_Users_UserDefaultAudioEndpointChangedEventArgs"></a> Class UserDefaultAudioEndpointChangedEventArgs

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.Users.UserManager.DefaultAudioEndpointChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class UserDefaultAudioEndpointChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[UserDefaultAudioEndpointChangedEventArgs](GDK.Net.Users.UserDefaultAudioEndpointChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Users_UserDefaultAudioEndpointChangedEventArgs_EndpointId"></a> EndpointId

The new endpoint id, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the user no longer has a default
endpoint for this role.

```csharp
public string? EndpointId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_Users_UserDefaultAudioEndpointChangedEventArgs_Kind"></a> Kind

Which endpoint role changed.

```csharp
public UserDefaultAudioEndpointKind Kind { get; }
```

#### Property Value

 [UserDefaultAudioEndpointKind](GDK.Net.Users.UserDefaultAudioEndpointKind.md)

### <a id="GDK_Net_Users_UserDefaultAudioEndpointChangedEventArgs_User"></a> User

The user whose default audio endpoint changed.

```csharp
public UserLocalId User { get; }
```

#### Property Value

 [UserLocalId](GDK.Net.Users.UserLocalId.md)

