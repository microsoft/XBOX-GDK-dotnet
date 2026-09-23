# <a id="GDK_Net_PlayFab_SegmentsPlayerProfile"></a> Class SegmentsPlayerProfile

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

Projects <code>PFSegmentsPlayerProfile</code>.

```csharp
public sealed class SegmentsPlayerProfile
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SegmentsPlayerProfile](GDK.Net.PlayFab.SegmentsPlayerProfile.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_AdCampaignAttributions"></a> AdCampaignAttributions

<code>AdCampaignAttributions</code>.

```csharp
public IReadOnlyList<SegmentsAdCampaignAttribution>? AdCampaignAttributions { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SegmentsAdCampaignAttribution](GDK.Net.PlayFab.SegmentsAdCampaignAttribution.md)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_AvatarUrl"></a> AvatarUrl

<code>AvatarUrl</code>.

```csharp
public string? AvatarUrl { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_BannedUntil"></a> BannedUntil

<code>BannedUntil</code>.

```csharp
public DateTimeOffset? BannedUntil { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_ChurnPrediction"></a> ChurnPrediction

<code>ChurnPrediction</code>.

```csharp
public SegmentsChurnRiskLevel? ChurnPrediction { get; set; }
```

#### Property Value

 [SegmentsChurnRiskLevel](GDK.Net.PlayFab.SegmentsChurnRiskLevel.md)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_ContactEmailAddresses"></a> ContactEmailAddresses

<code>ContactEmailAddresses</code>.

```csharp
public IReadOnlyList<SegmentsContactEmailInfo>? ContactEmailAddresses { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SegmentsContactEmailInfo](GDK.Net.PlayFab.SegmentsContactEmailInfo.md)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_Created"></a> Created

<code>Created</code>.

```csharp
public DateTimeOffset? Created { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_CustomProperties"></a> CustomProperties

<code>CustomProperties</code>.

```csharp
public string? CustomProperties { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_DisplayName"></a> DisplayName

<code>DisplayName</code>.

```csharp
public string? DisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_LastLogin"></a> LastLogin

<code>LastLogin</code>.

```csharp
public DateTimeOffset? LastLogin { get; set; }
```

#### Property Value

 [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_LinkedAccounts"></a> LinkedAccounts

<code>LinkedAccounts</code>.

```csharp
public IReadOnlyList<SegmentsPlayerLinkedAccount>? LinkedAccounts { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SegmentsPlayerLinkedAccount](GDK.Net.PlayFab.SegmentsPlayerLinkedAccount.md)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_Locations"></a> Locations

<code>Locations</code>.

```csharp
public IReadOnlyDictionary<string, SegmentsPlayerLocation>? Locations { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [SegmentsPlayerLocation](GDK.Net.PlayFab.SegmentsPlayerLocation.md)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_Origination"></a> Origination

<code>Origination</code>.

```csharp
public LoginIdentityProvider? Origination { get; set; }
```

#### Property Value

 [LoginIdentityProvider](GDK.Net.PlayFab.LoginIdentityProvider.md)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_PlayerExperimentVariants"></a> PlayerExperimentVariants

<code>PlayerExperimentVariants</code>.

```csharp
public IReadOnlyList<string>? PlayerExperimentVariants { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_PlayerId"></a> PlayerId

<code>PlayerId</code>.

```csharp
public string? PlayerId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_PlayerStatistics"></a> PlayerStatistics

<code>PlayerStatistics</code>.

```csharp
public IReadOnlyList<SegmentsPlayerStatistic>? PlayerStatistics { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SegmentsPlayerStatistic](GDK.Net.PlayFab.SegmentsPlayerStatistic.md)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_PublisherId"></a> PublisherId

<code>PublisherId</code>.

```csharp
public string? PublisherId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_PushNotificationRegistrations"></a> PushNotificationRegistrations

<code>PushNotificationRegistrations</code>.

```csharp
public IReadOnlyList<SegmentsPushNotificationRegistration>? PushNotificationRegistrations { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[SegmentsPushNotificationRegistration](GDK.Net.PlayFab.SegmentsPushNotificationRegistration.md)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_Statistics"></a> Statistics

<code>Statistics</code>.

```csharp
public IReadOnlyDictionary<string, int>? Statistics { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_Tags"></a> Tags

<code>Tags</code>.

```csharp
public IReadOnlyList<string>? Tags { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_TitleId"></a> TitleId

<code>TitleId</code>.

```csharp
public string? TitleId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_TotalValueToDateInUSD"></a> TotalValueToDateInUSD

<code>TotalValueToDateInUSD</code>.

```csharp
public uint? TotalValueToDateInUSD { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_ValuesToDate"></a> ValuesToDate

<code>ValuesToDate</code>.

```csharp
public IReadOnlyDictionary<string, uint>? ValuesToDate { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [uint](https://learn.microsoft.com/dotnet/api/system.uint32)\>?

### <a id="GDK_Net_PlayFab_SegmentsPlayerProfile_VirtualCurrencyBalances"></a> VirtualCurrencyBalances

<code>VirtualCurrencyBalances</code>.

```csharp
public IReadOnlyDictionary<string, int>? VirtualCurrencyBalances { get; set; }
```

#### Property Value

 [IReadOnlyDictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

