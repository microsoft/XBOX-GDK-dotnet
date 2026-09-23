# <a id="GDK_Net_PlayFab_MembershipModel"></a> Class MembershipModel

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFMembershipModel</code>.

```csharp
public sealed class MembershipModel
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MembershipModel](GDK.Net.PlayFab.MembershipModel.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_MembershipModel_IsActive"></a> IsActive

<code>IsActive</code>.

```csharp
public bool IsActive { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GDK_Net_PlayFab_MembershipModel_MembershipExpiration"></a> MembershipExpiration

<code>MembershipExpiration</code>.

```csharp
public DateTimeOffset MembershipExpiration { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

### <a id="GDK_Net_PlayFab_MembershipModel_MembershipId"></a> MembershipId

<code>MembershipId</code>.

```csharp
public string? MembershipId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_MembershipModel_OverrideExpiration"></a> OverrideExpiration

<code>OverrideExpiration</code>.

```csharp
public DateTimeOffset? OverrideExpiration { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_PlayFab_MembershipModel_Subscriptions"></a> Subscriptions

<code>Subscriptions</code>.

```csharp
public IReadOnlyList<SubscriptionModel>? Subscriptions { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SubscriptionModel](GDK.Net.PlayFab.SubscriptionModel.md)\>?

