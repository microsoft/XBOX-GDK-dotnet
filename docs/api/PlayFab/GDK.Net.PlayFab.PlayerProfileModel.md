# <a id="GDK_Net_PlayFab_PlayerProfileModel"></a> Class PlayerProfileModel

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFPlayerProfileModel</code>.

```csharp
public sealed class PlayerProfileModel
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayerProfileModel](GDK.Net.PlayFab.PlayerProfileModel.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_PlayerProfileModel_AdCampaignAttributions"></a> AdCampaignAttributions

<code>AdCampaignAttributions</code>.

```csharp
public IReadOnlyList<AdCampaignAttributionModel>? AdCampaignAttributions { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[AdCampaignAttributionModel](GDK.Net.PlayFab.AdCampaignAttributionModel.md)\>?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_AvatarUrl"></a> AvatarUrl

<code>AvatarUrl</code>.

```csharp
public string? AvatarUrl { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_BannedUntil"></a> BannedUntil

<code>BannedUntil</code>.

```csharp
public DateTimeOffset? BannedUntil { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_ContactEmailAddresses"></a> ContactEmailAddresses

<code>ContactEmailAddresses</code>.

```csharp
public IReadOnlyList<ContactEmailInfoModel>? ContactEmailAddresses { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ContactEmailInfoModel](GDK.Net.PlayFab.ContactEmailInfoModel.md)\>?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_Created"></a> Created

<code>Created</code>.

```csharp
public DateTimeOffset? Created { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_DisplayName"></a> DisplayName

<code>DisplayName</code>.

```csharp
public string? DisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_ExperimentVariants"></a> ExperimentVariants

<code>ExperimentVariants</code>.

```csharp
public IReadOnlyList<string>? ExperimentVariants { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_LastLogin"></a> LastLogin

<code>LastLogin</code>.

```csharp
public DateTimeOffset? LastLogin { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_LinkedAccounts"></a> LinkedAccounts

<code>LinkedAccounts</code>.

```csharp
public IReadOnlyList<LinkedPlatformAccountModel>? LinkedAccounts { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LinkedPlatformAccountModel](GDK.Net.PlayFab.LinkedPlatformAccountModel.md)\>?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_Locations"></a> Locations

<code>Locations</code>.

```csharp
public IReadOnlyList<LocationModel>? Locations { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[LocationModel](GDK.Net.PlayFab.LocationModel.md)\>?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_Memberships"></a> Memberships

<code>Memberships</code>.

```csharp
public IReadOnlyList<MembershipModel>? Memberships { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[MembershipModel](GDK.Net.PlayFab.MembershipModel.md)\>?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_Origination"></a> Origination

<code>Origination</code>.

```csharp
public LoginIdentityProvider? Origination { get; set; }
```

#### Property Value

 [LoginIdentityProvider](GDK.Net.PlayFab.LoginIdentityProvider.md)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_PlayerId"></a> PlayerId

<code>PlayerId</code>.

```csharp
public string? PlayerId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_PublisherId"></a> PublisherId

<code>PublisherId</code>.

```csharp
public string? PublisherId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_PushNotificationRegistrations"></a> PushNotificationRegistrations

<code>PushNotificationRegistrations</code>.

```csharp
public IReadOnlyList<PushNotificationRegistrationModel>? PushNotificationRegistrations { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PushNotificationRegistrationModel](GDK.Net.PlayFab.PushNotificationRegistrationModel.md)\>?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_Statistics"></a> Statistics

<code>Statistics</code>.

```csharp
public IReadOnlyList<StatisticModel>? Statistics { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[StatisticModel](GDK.Net.PlayFab.StatisticModel.md)\>?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_Tags"></a> Tags

<code>Tags</code>.

```csharp
public IReadOnlyList<TagModel>? Tags { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[TagModel](GDK.Net.PlayFab.TagModel.md)\>?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_TitleId"></a> TitleId

<code>TitleId</code>.

```csharp
public string? TitleId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_TotalValueToDateInUSD"></a> TotalValueToDateInUSD

<code>TotalValueToDateInUSD</code>.

```csharp
public uint? TotalValueToDateInUSD { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="GDK_Net_PlayFab_PlayerProfileModel_ValuesToDate"></a> ValuesToDate

<code>ValuesToDate</code>.

```csharp
public IReadOnlyList<ValueToDateModel>? ValuesToDate { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ValueToDateModel](GDK.Net.PlayFab.ValueToDateModel.md)\>?

