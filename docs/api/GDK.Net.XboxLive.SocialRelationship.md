# <a id="GDK_Net_XboxLive_SocialRelationship"></a> Class SocialRelationship

Namespace: [GDK.Net.XboxLive](GDK.Net.XboxLive.md)  
Assembly: GDK.Net.dll  

Represents the relationship between the signed-in user and another Xbox user.

```csharp
public sealed class SocialRelationship
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SocialRelationship](GDK.Net.XboxLive.SocialRelationship.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_XboxLive_SocialRelationship_IsFavorite"></a> IsFavorite

Whether this person is marked as a favorite.

```csharp
public bool IsFavorite { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialRelationship_IsFollowingCaller"></a> IsFollowingCaller

Compatibility field derived by XSAPI from <xref href="GDK.Net.XboxLive.SocialRelationship.IsFriend" data-throw-if-not-resolved="false"></xref> rather than a distinct
following relationship.

```csharp
public bool IsFollowingCaller { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialRelationship_IsFriend"></a> IsFriend

Whether there is a mutual follower/following relationship with this person.

```csharp
public bool IsFriend { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_XboxLive_SocialRelationship_SocialNetworks"></a> SocialNetworks

The social networks on which this relationship exists.

```csharp
public IReadOnlyList<string> SocialNetworks { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="GDK_Net_XboxLive_SocialRelationship_XboxUserId"></a> XboxUserId

The related person's Xbox user id.

```csharp
public ulong XboxUserId { get; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

