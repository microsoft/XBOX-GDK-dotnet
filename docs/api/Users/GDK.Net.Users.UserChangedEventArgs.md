# <a id="GDK_Net_Users_UserChangedEventArgs"></a> Class UserChangedEventArgs

Namespace: [GDK.Net.Users](GDK.Net.Users.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.Users.UserManager.UserChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class UserChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[UserChangedEventArgs](GDK.Net.Users.UserChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_Users_UserChangedEventArgs_Change"></a> Change

What changed.

```csharp
public UserChangeEvent Change { get; }
```

#### Property Value

 [UserChangeEvent](GDK.Net.Users.UserChangeEvent.md)

### <a id="GDK_Net_Users_UserChangedEventArgs_LocalId"></a> LocalId

The affected user's local id. Always present.

```csharp
public UserLocalId LocalId { get; }
```

#### Property Value

 [UserLocalId](GDK.Net.Users.UserLocalId.md)

### <a id="GDK_Net_Users_UserChangedEventArgs_User"></a> User

The live <xref href="GDK.Net.Users.UserChangedEventArgs.User" data-throw-if-not-resolved="false"></xref> for <xref href="GDK.Net.Users.UserChangedEventArgs.LocalId" data-throw-if-not-resolved="false"></xref> when this manager added it and the
instance is still alive; otherwise <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>. The native callback carries only a
local id, so a user added elsewhere cannot be resolved.

```csharp
public User? User { get; }
```

#### Property Value

 [User](GDK.Net.Users.User.md)?

