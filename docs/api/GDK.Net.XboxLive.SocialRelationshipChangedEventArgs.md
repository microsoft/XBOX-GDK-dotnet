# <a id="GDK_Net_XboxLive_SocialRelationshipChangedEventArgs"></a> Class SocialRelationshipChangedEventArgs

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Payload for <xref href="GDK.Net.XboxLive.SocialService.RelationshipChanged" data-throw-if-not-resolved="false"></xref>.

```csharp
public sealed class SocialRelationshipChangedEventArgs : EventArgs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EventArgs](https://learn.microsoft.com/dotnet/api/system.eventargs) ← 
[SocialRelationshipChangedEventArgs](GDK.Net.XboxLive.SocialRelationshipChangedEventArgs.md)

#### Inherited Members

[EventArgs.Empty](https://learn.microsoft.com/dotnet/api/system.eventargs.empty), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_SocialRelationshipChangedEventArgs_CallerXboxUserId"></a> CallerXboxUserId

The Xbox user id whose social graph changed.

```csharp
public ulong CallerXboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="GDK_Net_XboxLive_SocialRelationshipChangedEventArgs_Notification"></a> Notification

The kind of relationship change.

```csharp
public SocialNotificationType Notification { get; }
```

#### Property Value

 [SocialNotificationType](GDK.Net.XboxLive.SocialNotificationType.md)

### <a id="GDK_Net_XboxLive_SocialRelationshipChangedEventArgs_XboxUserIds"></a> XboxUserIds

The Xbox user ids affected by the change.

```csharp
public IReadOnlyList<ulong> XboxUserIds { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

