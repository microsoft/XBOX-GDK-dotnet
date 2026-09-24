# <a id="GDK_Net_PlayFab_PlayFabErrors"></a> Class PlayFabErrors

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The <code>E_PF_*</code> HRESULT codes PlayFab returns (<code>playfab/core/PFErrors.h</code>).

```csharp
public static class PlayFabErrors
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PlayFabErrors](GDK.Net.PlayFab.PlayFabErrors.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

A failing PlayFab call surfaces as <xref href="GDK.Net.PlayFab.PlayFabException" data-throw-if-not-resolved="false"></xref>, whose
<xref href="GDK.Net.GameRuntimeException.HResultCode" data-throw-if-not-resolved="false"></xref> is one of these values. They are
constants rather than an enum because the service may add codes this projection's GDK
edition does not know about, and an unknown enum member is worse than an unknown int.

## Fields

### <a id="GDK_Net_PlayFab_PlayFabErrors_AccountAlreadyExists"></a> AccountAlreadyExists

<code>E_PF_ACCOUNT_ALREADY_EXISTS</code>.

```csharp
public const int AccountAlreadyExists = -1994172485
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AccountAlreadyLinked"></a> AccountAlreadyLinked

<code>E_PF_ACCOUNT_ALREADY_LINKED</code>.

```csharp
public const int AccountAlreadyLinked = -1994173396
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AccountBanned"></a> AccountBanned

<code>E_PF_ACCOUNT_BANNED</code>.

```csharp
public const int AccountBanned = -1994173405
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AccountDeleted"></a> AccountDeleted

<code>E_PF_ACCOUNT_DELETED</code>.

```csharp
public const int AccountDeleted = -1994173097
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AccountLinkedToABannedPlayer"></a> AccountLinkedToABannedPlayer

<code>E_PF_ACCOUNT_LINKED_TO_A_BANNED_PLAYER</code>.

```csharp
public const int AccountLinkedToABannedPlayer = -1994172457
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AccountNotFound"></a> AccountNotFound

<code>E_PF_ACCOUNT_NOT_FOUND</code>.

```csharp
public const int AccountNotFound = -1994173406
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AccountNotLinked"></a> AccountNotLinked

<code>E_PF_ACCOUNT_NOT_LINKED</code>.

```csharp
public const int AccountNotLinked = -1994173393
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ActionGroupNotFound"></a> ActionGroupNotFound

<code>E_PF_ACTION_GROUP_NOT_FOUND</code>.

```csharp
public const int ActionGroupNotFound = -1994173168
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AddonAlreadyExists"></a> AddonAlreadyExists

<code>E_PF_ADDON_ALREADY_EXISTS</code>.

```csharp
public const int AddonAlreadyExists = -1994172666
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AddonDoesntExist"></a> AddonDoesntExist

<code>E_PF_ADDON_DOESNT_EXIST</code>.

```csharp
public const int AddonDoesntExist = -1994172665
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AggregationTypeNotAllowedForLinkedStat"></a> AggregationTypeNotAllowedForLinkedStat

<code>E_PF_AGGREGATION_TYPE_NOT_ALLOWED_FOR_LINKED_STAT</code>.

```csharp
public const int AggregationTypeNotAllowedForLinkedStat = -1994172472
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AggregationTypeNotAllowedForMultiColumnStatistic"></a> AggregationTypeNotAllowedForMultiColumnStatistic

<code>E_PF_AGGREGATION_TYPE_NOT_ALLOWED_FOR_MULTI_COLUMN_STATISTIC</code>.

```csharp
public const int AggregationTypeNotAllowedForMultiColumnStatistic = -1994172541
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AllAdPlacementViewsAlreadyConsumed"></a> AllAdPlacementViewsAlreadyConsumed

<code>E_PF_ALL_AD_PLACEMENT_VIEWS_ALREADY_CONSUMED</code>.

```csharp
public const int AllAdPlacementViewsAlreadyConsumed = -1994173149
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AllowNonUniquePlayerDisplayNamesDisableNotAllowed"></a> AllowNonUniquePlayerDisplayNamesDisableNotAllowed

<code>E_PF_ALLOW_NON_UNIQUE_PLAYER_DISPLAY_NAMES_DISABLE_NOT_ALLOWED</code>.

```csharp
public const int AllowNonUniquePlayerDisplayNamesDisableNotAllowed = -1994172682
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AmazonValidationError"></a> AmazonValidationError

<code>E_PF_AMAZON_VALIDATION_ERROR</code>.

```csharp
public const int AmazonValidationError = -1994173268
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AnalysisSubscriptionFailed"></a> AnalysisSubscriptionFailed

<code>E_PF_ANALYSIS_SUBSCRIPTION_FAILED</code>.

```csharp
public const int AnalysisSubscriptionFailed = -1994172871
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AnalysisSubscriptionFoundAlready"></a> AnalysisSubscriptionFoundAlready

<code>E_PF_ANALYSIS_SUBSCRIPTION_FOUND_ALREADY</code>.

```csharp
public const int AnalysisSubscriptionFoundAlready = -1994172870
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AnalysisSubscriptionManagementInvalidInput"></a> AnalysisSubscriptionManagementInvalidInput

<code>E_PF_ANALYSIS_SUBSCRIPTION_MANAGEMENT_INVALID_INPUT</code>.

```csharp
public const int AnalysisSubscriptionManagementInvalidInput = -1994172869
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AnalysisSubscriptionNotFound"></a> AnalysisSubscriptionNotFound

<code>E_PF_ANALYSIS_SUBSCRIPTION_NOT_FOUND</code>.

```csharp
public const int AnalysisSubscriptionNotFound = -1994172872
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AnalyticsSegmentCountOverLimit"></a> AnalyticsSegmentCountOverLimit

<code>E_PF_ANALYTICS_SEGMENT_COUNT_OVER_LIMIT</code>.

```csharp
public const int AnalyticsSegmentCountOverLimit = -1994172729
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiClientRequestRateLimitExceeded"></a> ApiClientRequestRateLimitExceeded

<code>E_PF_API_CLIENT_REQUEST_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int ApiClientRequestRateLimitExceeded = -1994173219
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiConcurrentRequestLimitExceeded"></a> ApiConcurrentRequestLimitExceeded

<code>E_PF_API_CONCURRENT_REQUEST_LIMIT_EXCEEDED</code>.

```csharp
public const int ApiConcurrentRequestLimitExceeded = -1994173077
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiDisabledForMigration"></a> ApiDisabledForMigration

<code>E_PF_API_DISABLED_FOR_MIGRATION</code>.

```csharp
public const int ApiDisabledForMigration = -1994172902
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiNotEnabledForGameClientAccess"></a> ApiNotEnabledForGameClientAccess

<code>E_PF_API_NOT_ENABLED_FOR_GAME_CLIENT_ACCESS</code>.

```csharp
public const int ApiNotEnabledForGameClientAccess = -1994173326
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiNotEnabledForGameServerAccess"></a> ApiNotEnabledForGameServerAccess

<code>E_PF_API_NOT_ENABLED_FOR_GAME_SERVER_ACCESS</code>.

```csharp
public const int ApiNotEnabledForGameServerAccess = -1994173224
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiNotEnabledForTitle"></a> ApiNotEnabledForTitle

<code>E_PF_API_NOT_ENABLED_FOR_TITLE</code>.

```csharp
public const int ApiNotEnabledForTitle = -1994172900
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiNotIncludedInAzurePlayFabFeatureSet"></a> ApiNotIncludedInAzurePlayFabFeatureSet

<code>E_PF_API_NOT_INCLUDED_IN_AZURE_PLAY_FAB_FEATURE_SET</code>.

```csharp
public const int ApiNotIncludedInAzurePlayFabFeatureSet = -1994172889
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiNotIncludedInTitleUsageTier"></a> ApiNotIncludedInTitleUsageTier

<code>E_PF_API_NOT_INCLUDED_IN_TITLE_USAGE_TIER</code>.

```csharp
public const int ApiNotIncludedInTitleUsageTier = -1994173290
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiRequestLimitExceeded"></a> ApiRequestLimitExceeded

<code>E_PF_API_REQUEST_LIMIT_EXCEEDED</code>.

```csharp
public const int ApiRequestLimitExceeded = -1994173288
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ApiRequestsDisabledForTitle"></a> ApiRequestsDisabledForTitle

<code>E_PF_API_REQUESTS_DISABLED_FOR_TITLE</code>.

```csharp
public const int ApiRequestsDisabledForTitle = -1994173124
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AppleNotEnabledForTitle"></a> AppleNotEnabledForTitle

<code>E_PF_APPLE_NOT_ENABLED_FOR_TITLE</code>.

```csharp
public const int AppleNotEnabledForTitle = -1994172919
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AsyncExportNotFound"></a> AsyncExportNotFound

<code>E_PF_ASYNC_EXPORT_NOT_FOUND</code>.

```csharp
public const int AsyncExportNotFound = -1994172731
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AsyncExportNotInFlight"></a> AsyncExportNotInFlight

<code>E_PF_ASYNC_EXPORT_NOT_IN_FLIGHT</code>.

```csharp
public const int AsyncExportNotInFlight = -1994172732
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AsyncExportRateLimitExceeded"></a> AsyncExportRateLimitExceeded

<code>E_PF_ASYNC_EXPORT_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int AsyncExportRateLimitExceeded = -1994172730
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AuthTokenAlreadyUsedToResetPassword"></a> AuthTokenAlreadyUsedToResetPassword

<code>E_PF_AUTH_TOKEN_ALREADY_USED_TO_RESET_PASSWORD</code>.

```csharp
public const int AuthTokenAlreadyUsedToResetPassword = -1994173090
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AuthTokenDoesNotExist"></a> AuthTokenDoesNotExist

<code>E_PF_AUTH_TOKEN_DOES_NOT_EXIST</code>.

```csharp
public const int AuthTokenDoesNotExist = -1994173092
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AuthTokenExpired"></a> AuthTokenExpired

<code>E_PF_AUTH_TOKEN_EXPIRED</code>.

```csharp
public const int AuthTokenExpired = -1994173091
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AutomationInvalidInput"></a> AutomationInvalidInput

<code>E_PF_AUTOMATION_INVALID_INPUT</code>.

```csharp
public const int AutomationInvalidInput = -1994172880
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AutomationInvalidRuleName"></a> AutomationInvalidRuleName

<code>E_PF_AUTOMATION_INVALID_RULE_NAME</code>.

```csharp
public const int AutomationInvalidRuleName = -1994172879
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AutomationRuleAlreadyExists"></a> AutomationRuleAlreadyExists

<code>E_PF_AUTOMATION_RULE_ALREADY_EXISTS</code>.

```csharp
public const int AutomationRuleAlreadyExists = -1994172878
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AutomationRuleLimitExceeded"></a> AutomationRuleLimitExceeded

<code>E_PF_AUTOMATION_RULE_LIMIT_EXCEEDED</code>.

```csharp
public const int AutomationRuleLimitExceeded = -1994172877
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AutomationRuleNotFound"></a> AutomationRuleNotFound

<code>E_PF_AUTOMATION_RULE_NOT_FOUND</code>.

```csharp
public const int AutomationRuleNotFound = -1994172974
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AzureResourceConcurrentOperationInProgress"></a> AzureResourceConcurrentOperationInProgress

<code>E_PF_AZURE_RESOURCE_CONCURRENT_OPERATION_IN_PROGRESS</code>.

```csharp
public const int AzureResourceConcurrentOperationInProgress = -1994172892
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AzureResourceManagerNotSupportedInStamp"></a> AzureResourceManagerNotSupportedInStamp

<code>E_PF_AZURE_RESOURCE_MANAGER_NOT_SUPPORTED_IN_STAMP</code>.

```csharp
public const int AzureResourceManagerNotSupportedInStamp = -1994172890
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AzureSubscriptionNotEligibleForLinking"></a> AzureSubscriptionNotEligibleForLinking

<code>E_PF_AZURE_SUBSCRIPTION_NOT_ELIGIBLE_FOR_LINKING</code>.

```csharp
public const int AzureSubscriptionNotEligibleForLinking = -1994172456
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_AzureTitleCreationInProgress"></a> AzureTitleCreationInProgress

<code>E_PF_AZURE_TITLE_CREATION_IN_PROGRESS</code>.

```csharp
public const int AzureTitleCreationInProgress = -1994172898
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_BadPartnerConfiguration"></a> BadPartnerConfiguration

<code>E_PF_BAD_PARTNER_CONFIGURATION</code>.

```csharp
public const int BadPartnerConfiguration = -1994173115
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_BattleNetNotEnabledForTitle"></a> BattleNetNotEnabledForTitle

<code>E_PF_BATTLE_NET_NOT_ENABLED_FOR_TITLE</code>.

```csharp
public const int BattleNetNotEnabledForTitle = -1994172498
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_BillingInformationRequired"></a> BillingInformationRequired

<code>E_PF_BILLING_INFORMATION_REQUIRED</code>.

```csharp
public const int BillingInformationRequired = -1994173152
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_BodyTooLarge"></a> BodyTooLarge

<code>E_PF_BODY_TOO_LARGE</code>.

```csharp
public const int BodyTooLarge = -1994173339
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_BuildAlreadyExists"></a> BuildAlreadyExists

<code>E_PF_BUILD_ALREADY_EXISTS</code>.

```csharp
public const int BuildAlreadyExists = -1994173324
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_BuildNotAvailable"></a> BuildNotAvailable

<code>E_PF_BUILD_NOT_AVAILABLE</code>.

```csharp
public const int BuildNotAvailable = -1994173286
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_BuildNotFound"></a> BuildNotFound

<code>E_PF_BUILD_NOT_FOUND</code>.

```csharp
public const int BuildNotFound = -1994173375
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_BuildPackageDoesNotExist"></a> BuildPackageDoesNotExist

<code>E_PF_BUILD_PACKAGE_DOES_NOT_EXIST</code>.

```csharp
public const int BuildPackageDoesNotExist = -1994173323
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CannotEnableAnonymousPlayerCreation"></a> CannotEnableAnonymousPlayerCreation

<code>E_PF_CANNOT_ENABLE_ANONYMOUS_PLAYER_CREATION</code>.

```csharp
public const int CannotEnableAnonymousPlayerCreation = -1994172460
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CannotEnableMultiplayerServersForTitle"></a> CannotEnableMultiplayerServersForTitle

<code>E_PF_CANNOT_ENABLE_MULTIPLAYER_SERVERS_FOR_TITLE</code>.

```csharp
public const int CannotEnableMultiplayerServersForTitle = -1994172977
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CannotEnablePartiesForTitle"></a> CannotEnablePartiesForTitle

<code>E_PF_CANNOT_ENABLE_PARTIES_FOR_TITLE</code>.

```csharp
public const int CannotEnablePartiesForTitle = -1994172990
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogApiNotImplemented"></a> CatalogApiNotImplemented

<code>E_PF_CATALOG_API_NOT_IMPLEMENTED</code>.

```csharp
public const int CatalogApiNotImplemented = -1994172829
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogBadRequest"></a> CatalogBadRequest

<code>E_PF_CATALOG_BAD_REQUEST</code>.

```csharp
public const int CatalogBadRequest = -1994172817
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogClientIdentityInvalid"></a> CatalogClientIdentityInvalid

<code>E_PF_CATALOG_CLIENT_IDENTITY_INVALID</code>.

```csharp
public const int CatalogClientIdentityInvalid = -1994172825
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogConfigInvalid"></a> CatalogConfigInvalid

<code>E_PF_CATALOG_CONFIG_INVALID</code>.

```csharp
public const int CatalogConfigInvalid = -1994172819
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogEntityInvalid"></a> CatalogEntityInvalid

<code>E_PF_CATALOG_ENTITY_INVALID</code>.

```csharp
public const int CatalogEntityInvalid = -1994172828
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogFeatureDisabled"></a> CatalogFeatureDisabled

<code>E_PF_CATALOG_FEATURE_DISABLED</code>.

```csharp
public const int CatalogFeatureDisabled = -1994172820
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogItemIdInvalid"></a> CatalogItemIdInvalid

<code>E_PF_CATALOG_ITEM_ID_INVALID</code>.

```csharp
public const int CatalogItemIdInvalid = -1994172822
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogItemMetadataInvalid"></a> CatalogItemMetadataInvalid

<code>E_PF_CATALOG_ITEM_METADATA_INVALID</code>.

```csharp
public const int CatalogItemMetadataInvalid = -1994172823
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogItemTypeInvalid"></a> CatalogItemTypeInvalid

<code>E_PF_CATALOG_ITEM_TYPE_INVALID</code>.

```csharp
public const int CatalogItemTypeInvalid = -1994172818
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogNotConfigured"></a> CatalogNotConfigured

<code>E_PF_CATALOG_NOT_CONFIGURED</code>.

```csharp
public const int CatalogNotConfigured = -1994173200
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogOneOrMoreFilesInvalid"></a> CatalogOneOrMoreFilesInvalid

<code>E_PF_CATALOG_ONE_OR_MORE_FILES_INVALID</code>.

```csharp
public const int CatalogOneOrMoreFilesInvalid = -1994172824
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogPlayerIdMissing"></a> CatalogPlayerIdMissing

<code>E_PF_CATALOG_PLAYER_ID_MISSING</code>.

```csharp
public const int CatalogPlayerIdMissing = -1994172826
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogSearchParameterInvalid"></a> CatalogSearchParameterInvalid

<code>E_PF_CATALOG_SEARCH_PARAMETER_INVALID</code>.

```csharp
public const int CatalogSearchParameterInvalid = -1994172821
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogTitleIdMissing"></a> CatalogTitleIdMissing

<code>E_PF_CATALOG_TITLE_ID_MISSING</code>.

```csharp
public const int CatalogTitleIdMissing = -1994172827
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CatalogTooManyRequests"></a> CatalogTooManyRequests

<code>E_PF_CATALOG_TOO_MANY_REQUESTS</code>.

```csharp
public const int CatalogTooManyRequests = -1994172816
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CharacterNotFound"></a> CharacterNotFound

<code>E_PF_CHARACTER_NOT_FOUND</code>.

```csharp
public const int CharacterNotFound = -1994173283
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptApiRequestCountExceeded"></a> CloudScriptApiRequestCountExceeded

<code>E_PF_CLOUD_SCRIPT_API_REQUEST_COUNT_EXCEEDED</code>.

```csharp
public const int CloudScriptApiRequestCountExceeded = -1994173209
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptApiRequestError"></a> CloudScriptApiRequestError

<code>E_PF_CLOUD_SCRIPT_API_REQUEST_ERROR</code>.

```csharp
public const int CloudScriptApiRequestError = -1994173208
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptAzureFunctionsArgumentSizeExceeded"></a> CloudScriptAzureFunctionsArgumentSizeExceeded

<code>E_PF_CLOUD_SCRIPT_AZURE_FUNCTIONS_ARGUMENT_SIZE_EXCEEDED</code>.

```csharp
public const int CloudScriptAzureFunctionsArgumentSizeExceeded = -1994172949
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptAzureFunctionsEventHubRequestError"></a> CloudScriptAzureFunctionsEventHubRequestError

<code>E_PF_CLOUD_SCRIPT_AZURE_FUNCTIONS_EVENT_HUB_REQUEST_ERROR</code>.

```csharp
public const int CloudScriptAzureFunctionsEventHubRequestError = -1994172607
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptAzureFunctionsExecutionTimeLimitExceeded"></a> CloudScriptAzureFunctionsExecutionTimeLimitExceeded

<code>E_PF_CLOUD_SCRIPT_AZURE_FUNCTIONS_EXECUTION_TIME_LIMIT_EXCEEDED</code>.

```csharp
public const int CloudScriptAzureFunctionsExecutionTimeLimitExceeded = -1994172950
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptAzureFunctionsHttpRequestError"></a> CloudScriptAzureFunctionsHttpRequestError

<code>E_PF_CLOUD_SCRIPT_AZURE_FUNCTIONS_HTTP_REQUEST_ERROR</code>.

```csharp
public const int CloudScriptAzureFunctionsHttpRequestError = -1994172947
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptAzureFunctionsQueueRequestError"></a> CloudScriptAzureFunctionsQueueRequestError

<code>E_PF_CLOUD_SCRIPT_AZURE_FUNCTIONS_QUEUE_REQUEST_ERROR</code>.

```csharp
public const int CloudScriptAzureFunctionsQueueRequestError = -1994172926
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptAzureFunctionsReturnSizeExceeded"></a> CloudScriptAzureFunctionsReturnSizeExceeded

<code>E_PF_CLOUD_SCRIPT_AZURE_FUNCTIONS_RETURN_SIZE_EXCEEDED</code>.

```csharp
public const int CloudScriptAzureFunctionsReturnSizeExceeded = -1994172948
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptExecutionTimeLimitExceeded"></a> CloudScriptExecutionTimeLimitExceeded

<code>E_PF_CLOUD_SCRIPT_EXECUTION_TIME_LIMIT_EXCEEDED</code>.

```csharp
public const int CloudScriptExecutionTimeLimitExceeded = -1994173212
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptFunctionArgumentSizeExceeded"></a> CloudScriptFunctionArgumentSizeExceeded

<code>E_PF_CLOUD_SCRIPT_FUNCTION_ARGUMENT_SIZE_EXCEEDED</code>.

```csharp
public const int CloudScriptFunctionArgumentSizeExceeded = -1994173210
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptFunctionNameSizeExceeded"></a> CloudScriptFunctionNameSizeExceeded

<code>E_PF_CLOUD_SCRIPT_FUNCTION_NAME_SIZE_EXCEEDED</code>.

```csharp
public const int CloudScriptFunctionNameSizeExceeded = -1994172928
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptHttpRequestError"></a> CloudScriptHttpRequestError

<code>E_PF_CLOUD_SCRIPT_HTTP_REQUEST_ERROR</code>.

```csharp
public const int CloudScriptHttpRequestError = -1994173207
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptNotFound"></a> CloudScriptNotFound

<code>E_PF_CLOUD_SCRIPT_NOT_FOUND</code>.

```csharp
public const int CloudScriptNotFound = -1994173282
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CloudScriptUnableToDeleteProductionRevision"></a> CloudScriptUnableToDeleteProductionRevision

<code>E_PF_CLOUD_SCRIPT_UNABLE_TO_DELETE_PRODUCTION_REVISION</code>.

```csharp
public const int CloudScriptUnableToDeleteProductionRevision = -1994172882
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ConcurrentEditError"></a> ConcurrentEditError

<code>E_PF_CONCURRENT_EDIT_ERROR</code>.

```csharp
public const int ConcurrentEditError = -1994173285
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ContainerKeyInvalid"></a> ContainerKeyInvalid

<code>E_PF_CONTAINER_KEY_INVALID</code>.

```csharp
public const int ContainerKeyInvalid = -1994173213
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ContainerNotOwned"></a> ContainerNotOwned

<code>E_PF_CONTAINER_NOT_OWNED</code>.

```csharp
public const int ContainerNotOwned = -1994173389
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ContentNotFound"></a> ContentNotFound

<code>E_PF_CONTENT_NOT_FOUND</code>.

```csharp
public const int ContentNotFound = -1994173284
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ContentQuotaExceeded"></a> ContentQuotaExceeded

<code>E_PF_CONTENT_QUOTA_EXCEEDED</code>.

```csharp
public const int ContentQuotaExceeded = -1994173281
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ContentS3OriginBucketNotConfigured"></a> ContentS3OriginBucketNotConfigured

<code>E_PF_CONTENT_S_3_ORIGIN_BUCKET_NOT_CONFIGURED</code>.

```csharp
public const int ContentS3OriginBucketNotConfigured = -1994173120
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CopilotDisabled"></a> CopilotDisabled

<code>E_PF_COPILOT_DISABLED</code>.

```csharp
public const int CopilotDisabled = -1994172664
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CopilotInvalidRequest"></a> CopilotInvalidRequest

<code>E_PF_COPILOT_INVALID_REQUEST</code>.

```csharp
public const int CopilotInvalidRequest = -1994172663
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CoreAlreadyInitialized"></a> CoreAlreadyInitialized

<code>E_PF_CORE_ALREADY_INITIALIZED</code>.

```csharp
public const int CoreAlreadyInitialized = -1994173439
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CoreEventPipelineBufferFull"></a> CoreEventPipelineBufferFull

<code>E_PF_CORE_EVENT_PIPELINE_BUFFER_FULL</code>.

```csharp
public const int CoreEventPipelineBufferFull = -1994173436
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CoreLocalUserMissingId"></a> CoreLocalUserMissingId

<code>E_PF_CORE_LOCAL_USER_MISSING_ID</code>.

```csharp
public const int CoreLocalUserMissingId = -1994173432
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CoreLocalUserNoPlatformContext"></a> CoreLocalUserNoPlatformContext

<code>E_PF_CORE_LOCAL_USER_NO_PLATFORM_CONTEXT</code>.

```csharp
public const int CoreLocalUserNoPlatformContext = -1994173435
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CoreLocalUserNotLoggedIn"></a> CoreLocalUserNotLoggedIn

<code>E_PF_CORE_LOCAL_USER_NOT_LOGGED_IN</code>.

```csharp
public const int CoreLocalUserNotLoggedIn = -1994173434
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CoreMissingPlatform"></a> CoreMissingPlatform

<code>E_PF_CORE_MISSING_PLATFORM</code>.

```csharp
public const int CoreMissingPlatform = -1994173433
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CoreMissingPlatformHandler"></a> CoreMissingPlatformHandler

<code>E_PF_CORE_MISSING_PLATFORM_HANDLER</code>.

```csharp
public const int CoreMissingPlatformHandler = -1994173437
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CoreNotInitialized"></a> CoreNotInitialized

<code>E_PF_CORE_NOT_INITIALIZED</code>.

```csharp
public const int CoreNotInitialized = -1994173440
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CouponAlreadyRedeemed"></a> CouponAlreadyRedeemed

<code>E_PF_COUPON_ALREADY_REDEEMED</code>.

```csharp
public const int CouponAlreadyRedeemed = -1994173192
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CouponCodeNotFound"></a> CouponCodeNotFound

<code>E_PF_COUPON_CODE_NOT_FOUND</code>.

```csharp
public const int CouponCodeNotFound = -1994173391
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CreateSegmentRateLimitExceeded"></a> CreateSegmentRateLimitExceeded

<code>E_PF_CREATE_SEGMENT_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int CreateSegmentRateLimitExceeded = -1994172735
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CustomAnalyticsEventsNotEnabledForTitle"></a> CustomAnalyticsEventsNotEnabledForTitle

<code>E_PF_CUSTOM_ANALYTICS_EVENTS_NOT_ENABLED_FOR_TITLE</code>.

```csharp
public const int CustomAnalyticsEventsNotEnabledForTitle = -1994173322
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CustomIdNotFound"></a> CustomIdNotFound

<code>E_PF_CUSTOM_ID_NOT_FOUND</code>.

```csharp
public const int CustomIdNotFound = -1994172881
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_CustomIdNotLinked"></a> CustomIdNotLinked

<code>E_PF_CUSTOM_ID_NOT_LINKED</code>.

```csharp
public const int CustomIdNotLinked = -1994173233
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DataIntegrityError"></a> DataIntegrityError

<code>E_PF_DATA_INTEGRITY_ERROR</code>.

```csharp
public const int DataIntegrityError = -1994173017
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DataLengthExceeded"></a> DataLengthExceeded

<code>E_PF_DATA_LENGTH_EXCEEDED</code>.

```csharp
public const int DataLengthExceeded = -1994173272
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DataNotAvailable"></a> DataNotAvailable

<code>E_PF_DATA_NOT_AVAILABLE</code>.

```csharp
public const int DataNotAvailable = -1994172496
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DataUpdateRateExceeded"></a> DataUpdateRateExceeded

<code>E_PF_DATA_UPDATE_RATE_EXCEEDED</code>.

```csharp
public const int DataUpdateRateExceeded = -1994173132
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DatabaseThroughputExceeded"></a> DatabaseThroughputExceeded

<code>E_PF_DATABASE_THROUGHPUT_EXCEEDED</code>.

```csharp
public const int DatabaseThroughputExceeded = -1994173304
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DauLimitExceeded"></a> DauLimitExceeded

<code>E_PF_DAU_LIMIT_EXCEEDED</code>.

```csharp
public const int DauLimitExceeded = -1994173289
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DeleteKeyConflict"></a> DeleteKeyConflict

<code>E_PF_DELETE_KEY_CONFLICT</code>.

```csharp
public const int DeleteKeyConflict = -1994173231
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DeleteSegmentRateLimitExceeded"></a> DeleteSegmentRateLimitExceeded

<code>E_PF_DELETE_SEGMENT_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int DeleteSegmentRateLimitExceeded = -1994172736
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DeviceAlreadyLinked"></a> DeviceAlreadyLinked

<code>E_PF_DEVICE_ALREADY_LINKED</code>.

```csharp
public const int DeviceAlreadyLinked = -1994173299
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DeviceNotLinked"></a> DeviceNotLinked

<code>E_PF_DEVICE_NOT_LINKED</code>.

```csharp
public const int DeviceNotLinked = -1994173298
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DownstreamServiceUnavailable"></a> DownstreamServiceUnavailable

<code>E_PF_DOWNSTREAM_SERVICE_UNAVAILABLE</code>.

```csharp
public const int DownstreamServiceUnavailable = -1994173291
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateColumnNameFound"></a> DuplicateColumnNameFound

<code>E_PF_DUPLICATE_COLUMN_NAME_FOUND</code>.

```csharp
public const int DuplicateColumnNameFound = -1994172557
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateDropTableId"></a> DuplicateDropTableId

<code>E_PF_DUPLICATE_DROP_TABLE_ID</code>.

```csharp
public const int DuplicateDropTableId = -1994173041
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateEmail"></a> DuplicateEmail

<code>E_PF_DUPLICATE_EMAIL</code>.

```csharp
public const int DuplicateEmail = -1994173361
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateKeys"></a> DuplicateKeys

<code>E_PF_DUPLICATE_KEYS</code>.

```csharp
public const int DuplicateKeys = -1994172911
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateLinkedStatisticColumnNameFound"></a> DuplicateLinkedStatisticColumnNameFound

<code>E_PF_DUPLICATE_LINKED_STATISTIC_COLUMN_NAME_FOUND</code>.

```csharp
public const int DuplicateLinkedStatisticColumnNameFound = -1994172542
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicatePurchaseTransactionId"></a> DuplicatePurchaseTransactionId

<code>E_PF_DUPLICATE_PURCHASE_TRANSACTION_ID</code>.

```csharp
public const int DuplicatePurchaseTransactionId = -1994172931
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateRoleId"></a> DuplicateRoleId

<code>E_PF_DUPLICATE_ROLE_ID</code>.

```csharp
public const int DuplicateRoleId = -1994173059
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateStatisticName"></a> DuplicateStatisticName

<code>E_PF_DUPLICATE_STATISTIC_NAME</code>.

```csharp
public const int DuplicateStatisticName = -1994173165
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateStudioName"></a> DuplicateStudioName

<code>E_PF_DUPLICATE_STUDIO_NAME</code>.

```csharp
public const int DuplicateStudioName = -1994172962
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateTitleName"></a> DuplicateTitleName

<code>E_PF_DUPLICATE_TITLE_NAME</code>.

```csharp
public const int DuplicateTitleName = -1994172955
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateTitleNameForPublisher"></a> DuplicateTitleNameForPublisher

<code>E_PF_DUPLICATE_TITLE_NAME_FOR_PUBLISHER</code>.

```csharp
public const int DuplicateTitleNameForPublisher = -1994172899
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_DuplicateUsername"></a> DuplicateUsername

<code>E_PF_DUPLICATE_USERNAME</code>.

```csharp
public const int DuplicateUsername = -1994173342
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EconomyServiceInternalError"></a> EconomyServiceInternalError

<code>E_PF_ECONOMY_SERVICE_INTERNAL_ERROR</code>.

```csharp
public const int EconomyServiceInternalError = -1994172969
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EconomyServiceUnavailable"></a> EconomyServiceUnavailable

<code>E_PF_ECONOMY_SERVICE_UNAVAILABLE</code>.

```csharp
public const int EconomyServiceUnavailable = -1994172970
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ElasticSearchOperationFailed"></a> ElasticSearchOperationFailed

<code>E_PF_ELASTIC_SEARCH_OPERATION_FAILED</code>.

```csharp
public const int ElasticSearchOperationFailed = -1994172906
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailAddressNotAvailable"></a> EmailAddressNotAvailable

<code>E_PF_EMAIL_ADDRESS_NOT_AVAILABLE</code>.

```csharp
public const int EmailAddressNotAvailable = -1994173401
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailClientCanceledTask"></a> EmailClientCanceledTask

<code>E_PF_EMAIL_CLIENT_CANCELED_TASK</code>.

```csharp
public const int EmailClientCanceledTask = -1994173102
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailClientTimeout"></a> EmailClientTimeout

<code>E_PF_EMAIL_CLIENT_TIMEOUT</code>.

```csharp
public const int EmailClientTimeout = -1994173103
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailConfirmationTokenDoesNotExist"></a> EmailConfirmationTokenDoesNotExist

<code>E_PF_EMAIL_CONFIRMATION_TOKEN_DOES_NOT_EXIST</code>.

```csharp
public const int EmailConfirmationTokenDoesNotExist = -1994173099
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailConfirmationTokenExpired"></a> EmailConfirmationTokenExpired

<code>E_PF_EMAIL_CONFIRMATION_TOKEN_EXPIRED</code>.

```csharp
public const int EmailConfirmationTokenExpired = -1994173098
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailMessageFromAddressIsMissing"></a> EmailMessageFromAddressIsMissing

<code>E_PF_EMAIL_MESSAGE_FROM_ADDRESS_IS_MISSING</code>.

```csharp
public const int EmailMessageFromAddressIsMissing = -1994173110
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailMessageToAddressIsMissing"></a> EmailMessageToAddressIsMissing

<code>E_PF_EMAIL_MESSAGE_TO_ADDRESS_IS_MISSING</code>.

```csharp
public const int EmailMessageToAddressIsMissing = -1994173109
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailRecipientBlacklisted"></a> EmailRecipientBlacklisted

<code>E_PF_EMAIL_RECIPIENT_BLACKLISTED</code>.

```csharp
public const int EmailRecipientBlacklisted = -1994172993
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailReportAlreadySent"></a> EmailReportAlreadySent

<code>E_PF_EMAIL_REPORT_ALREADY_SENT</code>.

```csharp
public const int EmailReportAlreadySent = -1994173050
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailReportRecipientBlacklisted"></a> EmailReportRecipientBlacklisted

<code>E_PF_EMAIL_REPORT_RECIPIENT_BLACKLISTED</code>.

```csharp
public const int EmailReportRecipientBlacklisted = -1994173049
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailTemplateInvalidSyntax"></a> EmailTemplateInvalidSyntax

<code>E_PF_EMAIL_TEMPLATE_INVALID_SYNTAX</code>.

```csharp
public const int EmailTemplateInvalidSyntax = -1994173014
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailTemplateMissing"></a> EmailTemplateMissing

<code>E_PF_EMAIL_TEMPLATE_MISSING</code>.

```csharp
public const int EmailTemplateMissing = -1994173101
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailTemplateMissingCallback"></a> EmailTemplateMissingCallback

<code>E_PF_EMAIL_TEMPLATE_MISSING_CALLBACK</code>.

```csharp
public const int EmailTemplateMissingCallback = -1994173013
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EmailTemplateMissingDefaultVersion"></a> EmailTemplateMissingDefaultVersion

<code>E_PF_EMAIL_TEMPLATE_MISSING_DEFAULT_VERSION</code>.

```csharp
public const int EmailTemplateMissingDefaultVersion = -1994173025
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EncryptedRequestNotAllowed"></a> EncryptedRequestNotAllowed

<code>E_PF_ENCRYPTED_REQUEST_NOT_ALLOWED</code>.

```csharp
public const int EncryptedRequestNotAllowed = -1994173118
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EncryptionKeyBroken"></a> EncryptionKeyBroken

<code>E_PF_ENCRYPTION_KEY_BROKEN</code>.

```csharp
public const int EncryptionKeyBroken = -1994173128
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EncryptionKeyDisabled"></a> EncryptionKeyDisabled

<code>E_PF_ENCRYPTION_KEY_DISABLED</code>.

```csharp
public const int EncryptionKeyDisabled = -1994173130
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EncryptionKeyMissing"></a> EncryptionKeyMissing

<code>E_PF_ENCRYPTION_KEY_MISSING</code>.

```csharp
public const int EncryptionKeyMissing = -1994173129
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityApiKeyCreationDisabledForEntity"></a> EntityApiKeyCreationDisabledForEntity

<code>E_PF_ENTITY_API_KEY_CREATION_DISABLED_FOR_ENTITY</code>.

```csharp
public const int EntityApiKeyCreationDisabledForEntity = -1994172967
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityApiKeyLimitExceeded"></a> EntityApiKeyLimitExceeded

<code>E_PF_ENTITY_API_KEY_LIMIT_EXCEEDED</code>.

```csharp
public const int EntityApiKeyLimitExceeded = -1994172973
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityApiKeyNotFound"></a> EntityApiKeyNotFound

<code>E_PF_ENTITY_API_KEY_NOT_FOUND</code>.

```csharp
public const int EntityApiKeyNotFound = -1994172972
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityApiKeyOrSecretInvalid"></a> EntityApiKeyOrSecretInvalid

<code>E_PF_ENTITY_API_KEY_OR_SECRET_INVALID</code>.

```csharp
public const int EntityApiKeyOrSecretInvalid = -1994172971
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityApiKeysNotSupported"></a> EntityApiKeysNotSupported

<code>E_PF_ENTITY_API_KEYS_NOT_SUPPORTED</code>.

```csharp
public const int EntityApiKeysNotSupported = -1994172866
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityBlockedByGroup"></a> EntityBlockedByGroup

<code>E_PF_ENTITY_BLOCKED_BY_GROUP</code>.

```csharp
public const int EntityBlockedByGroup = -1994173062
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityFileOperationPending"></a> EntityFileOperationPending

<code>E_PF_ENTITY_FILE_OPERATION_PENDING</code>.

```csharp
public const int EntityFileOperationPending = -1994173069
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityIsAlreadyMember"></a> EntityIsAlreadyMember

<code>E_PF_ENTITY_IS_ALREADY_MEMBER</code>.

```csharp
public const int EntityIsAlreadyMember = -1994173060
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityLineageBanned"></a> EntityLineageBanned

<code>E_PF_ENTITY_LINEAGE_BANNED</code>.

```csharp
public const int EntityLineageBanned = -1994172864
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityProfileConstraintValidationFailed"></a> EntityProfileConstraintValidationFailed

<code>E_PF_ENTITY_PROFILE_CONSTRAINT_VALIDATION_FAILED</code>.

```csharp
public const int EntityProfileConstraintValidationFailed = -1994173021
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityProfileVersionMismatch"></a> EntityProfileVersionMismatch

<code>E_PF_ENTITY_PROFILE_VERSION_MISMATCH</code>.

```csharp
public const int EntityProfileVersionMismatch = -1994173067
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityTokenExpired"></a> EntityTokenExpired

<code>E_PF_ENTITY_TOKEN_EXPIRED</code>.

```csharp
public const int EntityTokenExpired = -1994173083
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityTokenInvalid"></a> EntityTokenInvalid

<code>E_PF_ENTITY_TOKEN_INVALID</code>.

```csharp
public const int EntityTokenInvalid = -1994173084
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityTokenMissing"></a> EntityTokenMissing

<code>E_PF_ENTITY_TOKEN_MISSING</code>.

```csharp
public const int EntityTokenMissing = -1994173085
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityTokenRevoked"></a> EntityTokenRevoked

<code>E_PF_ENTITY_TOKEN_REVOKED</code>.

```csharp
public const int EntityTokenRevoked = -1994173082
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityTypeMismatchWithStatDefinition"></a> EntityTypeMismatchWithStatDefinition

<code>E_PF_ENTITY_TYPE_MISMATCH_WITH_STAT_DEFINITION</code>.

```csharp
public const int EntityTypeMismatchWithStatDefinition = -1994172560
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EntityTypeSpecifiedRequiresAggregationSource"></a> EntityTypeSpecifiedRequiresAggregationSource

<code>E_PF_ENTITY_TYPE_SPECIFIED_REQUIRES_AGGREGATION_SOURCE</code>.

```csharp
public const int EntityTypeSpecifiedRequiresAggregationSource = -1994172463
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ErrorCreatingStream"></a> ErrorCreatingStream

<code>E_PF_ERROR_CREATING_STREAM</code>.

```csharp
public const int ErrorCreatingStream = -1994173331
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EvaluationModePlayerCountExceeded"></a> EvaluationModePlayerCountExceeded

<code>E_PF_EVALUATION_MODE_PLAYER_COUNT_EXCEEDED</code>.

```csharp
public const int EvaluationModePlayerCountExceeded = -1994172930
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EvaluationModeTitleCountExceeded"></a> EvaluationModeTitleCountExceeded

<code>E_PF_EVALUATION_MODE_TITLE_COUNT_EXCEEDED</code>.

```csharp
public const int EvaluationModeTitleCountExceeded = -1994172925
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventEntityNotAllowed"></a> EventEntityNotAllowed

<code>E_PF_EVENT_ENTITY_NOT_ALLOWED</code>.

```csharp
public const int EventEntityNotAllowed = -1994173047
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventNamespaceNotAllowed"></a> EventNamespaceNotAllowed

<code>E_PF_EVENT_NAMESPACE_NOT_ALLOWED</code>.

```csharp
public const int EventNamespaceNotAllowed = -1994173048
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventNotFound"></a> EventNotFound

<code>E_PF_EVENT_NOT_FOUND</code>.

```csharp
public const int EventNotFound = -1994173203
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSamplingInvalidEventName"></a> EventSamplingInvalidEventName

<code>E_PF_EVENT_SAMPLING_INVALID_EVENT_NAME</code>.

```csharp
public const int EventSamplingInvalidEventName = -1994172706
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSamplingInvalidEventNamespace"></a> EventSamplingInvalidEventNamespace

<code>E_PF_EVENT_SAMPLING_INVALID_EVENT_NAMESPACE</code>.

```csharp
public const int EventSamplingInvalidEventNamespace = -1994172707
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSamplingInvalidRatio"></a> EventSamplingInvalidRatio

<code>E_PF_EVENT_SAMPLING_INVALID_RATIO</code>.

```csharp
public const int EventSamplingInvalidRatio = -1994172708
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSamplingRatioNotFound"></a> EventSamplingRatioNotFound

<code>E_PF_EVENT_SAMPLING_RATIO_NOT_FOUND</code>.

```csharp
public const int EventSamplingRatioNotFound = -1994172705
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkAadNotFound"></a> EventSinkAadNotFound

<code>E_PF_EVENT_SINK_AAD_NOT_FOUND</code>.

```csharp
public const int EventSinkAadNotFound = -1994172687
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkAccessDenied"></a> EventSinkAccessDenied

<code>E_PF_EVENT_SINK_ACCESS_DENIED</code>.

```csharp
public const int EventSinkAccessDenied = -1994172510
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkBucketNameInvalid"></a> EventSinkBucketNameInvalid

<code>E_PF_EVENT_SINK_BUCKET_NAME_INVALID</code>.

```csharp
public const int EventSinkBucketNameInvalid = -1994172506
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkConnectionInvalid"></a> EventSinkConnectionInvalid

<code>E_PF_EVENT_SINK_CONNECTION_INVALID</code>.

```csharp
public const int EventSinkConnectionInvalid = -1994172697
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkConnectionUnauthorized"></a> EventSinkConnectionUnauthorized

<code>E_PF_EVENT_SINK_CONNECTION_UNAUTHORIZED</code>.

```csharp
public const int EventSinkConnectionUnauthorized = -1994172696
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkContainerNotFound"></a> EventSinkContainerNotFound

<code>E_PF_EVENT_SINK_CONTAINER_NOT_FOUND</code>.

```csharp
public const int EventSinkContainerNotFound = -1994172652
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkDatabaseNotFound"></a> EventSinkDatabaseNotFound

<code>E_PF_EVENT_SINK_DATABASE_NOT_FOUND</code>.

```csharp
public const int EventSinkDatabaseNotFound = -1994172686
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkInsufficientRoleAssignment"></a> EventSinkInsufficientRoleAssignment

<code>E_PF_EVENT_SINK_INSUFFICIENT_ROLE_ASSIGNMENT</code>.

```csharp
public const int EventSinkInsufficientRoleAssignment = -1994172653
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkLimitExceeded"></a> EventSinkLimitExceeded

<code>E_PF_EVENT_SINK_LIMIT_EXCEEDED</code>.

```csharp
public const int EventSinkLimitExceeded = -1994172694
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkNameInvalid"></a> EventSinkNameInvalid

<code>E_PF_EVENT_SINK_NAME_INVALID</code>.

```csharp
public const int EventSinkNameInvalid = -1994172691
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkNotFound"></a> EventSinkNotFound

<code>E_PF_EVENT_SINK_NOT_FOUND</code>.

```csharp
public const int EventSinkNotFound = -1994172692
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkRegionInvalid"></a> EventSinkRegionInvalid

<code>E_PF_EVENT_SINK_REGION_INVALID</code>.

```csharp
public const int EventSinkRegionInvalid = -1994172695
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkResourceFeatureNotSupported"></a> EventSinkResourceFeatureNotSupported

<code>E_PF_EVENT_SINK_RESOURCE_FEATURE_NOT_SUPPORTED</code>.

```csharp
public const int EventSinkResourceFeatureNotSupported = -1994172507
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkResourceMisconfigured"></a> EventSinkResourceMisconfigured

<code>E_PF_EVENT_SINK_RESOURCE_MISCONFIGURED</code>.

```csharp
public const int EventSinkResourceMisconfigured = -1994172511
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkResourceNotFound"></a> EventSinkResourceNotFound

<code>E_PF_EVENT_SINK_RESOURCE_NOT_FOUND</code>.

```csharp
public const int EventSinkResourceNotFound = -1994172508
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkResourceUnavailable"></a> EventSinkResourceUnavailable

<code>E_PF_EVENT_SINK_RESOURCE_UNAVAILABLE</code>.

```csharp
public const int EventSinkResourceUnavailable = -1994172505
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkSasTokenInvalid"></a> EventSinkSasTokenInvalid

<code>E_PF_EVENT_SINK_SAS_TOKEN_INVALID</code>.

```csharp
public const int EventSinkSasTokenInvalid = -1994172693
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkSasTokenPermissionInvalid"></a> EventSinkSasTokenPermissionInvalid

<code>E_PF_EVENT_SINK_SAS_TOKEN_PERMISSION_INVALID</code>.

```csharp
public const int EventSinkSasTokenPermissionInvalid = -1994172690
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkSecretInvalid"></a> EventSinkSecretInvalid

<code>E_PF_EVENT_SINK_SECRET_INVALID</code>.

```csharp
public const int EventSinkSecretInvalid = -1994172689
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkTenantIdInvalid"></a> EventSinkTenantIdInvalid

<code>E_PF_EVENT_SINK_TENANT_ID_INVALID</code>.

```csharp
public const int EventSinkTenantIdInvalid = -1994172552
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkTenantNotFound"></a> EventSinkTenantNotFound

<code>E_PF_EVENT_SINK_TENANT_NOT_FOUND</code>.

```csharp
public const int EventSinkTenantNotFound = -1994172688
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkTitleUnauthorized"></a> EventSinkTitleUnauthorized

<code>E_PF_EVENT_SINK_TITLE_UNAUTHORIZED</code>.

```csharp
public const int EventSinkTitleUnauthorized = -1994172685
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_EventSinkWriteConflict"></a> EventSinkWriteConflict

<code>E_PF_EVENT_SINK_WRITE_CONFLICT</code>.

```csharp
public const int EventSinkWriteConflict = -1994172509
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentInvalidId"></a> ExperimentInvalidId

<code>E_PF_EXPERIMENT_INVALID_ID</code>.

```csharp
public const int ExperimentInvalidId = -1994172760
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationClientTimeout"></a> ExperimentationClientTimeout

<code>E_PF_EXPERIMENTATION_CLIENT_TIMEOUT</code>.

```csharp
public const int ExperimentationClientTimeout = -1994172763
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExclusionGroupCannotDelete"></a> ExperimentationExclusionGroupCannotDelete

<code>E_PF_EXPERIMENTATION_EXCLUSION_GROUP_CANNOT_DELETE</code>.

```csharp
public const int ExperimentationExclusionGroupCannotDelete = -1994172748
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExclusionGroupInsufficientCapacity"></a> ExperimentationExclusionGroupInsufficientCapacity

<code>E_PF_EXPERIMENTATION_EXCLUSION_GROUP_INSUFFICIENT_CAPACITY</code>.

```csharp
public const int ExperimentationExclusionGroupInsufficientCapacity = -1994172749
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExclusionGroupInvalidName"></a> ExperimentationExclusionGroupInvalidName

<code>E_PF_EXPERIMENTATION_EXCLUSION_GROUP_INVALID_NAME</code>.

```csharp
public const int ExperimentationExclusionGroupInvalidName = -1994172746
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExclusionGroupInvalidTrafficAllocation"></a> ExperimentationExclusionGroupInvalidTrafficAllocation

<code>E_PF_EXPERIMENTATION_EXCLUSION_GROUP_INVALID_TRAFFIC_ALLOCATION</code>.

```csharp
public const int ExperimentationExclusionGroupInvalidTrafficAllocation = -1994172747
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExclusionGroupNotFound"></a> ExperimentationExclusionGroupNotFound

<code>E_PF_EXPERIMENTATION_EXCLUSION_GROUP_NOT_FOUND</code>.

```csharp
public const int ExperimentationExclusionGroupNotFound = -1994172750
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExperimentDeleted"></a> ExperimentationExperimentDeleted

<code>E_PF_EXPERIMENTATION_EXPERIMENT_DELETED</code>.

```csharp
public const int ExperimentationExperimentDeleted = -1994172764
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExperimentNeverStarted"></a> ExperimentationExperimentNeverStarted

<code>E_PF_EXPERIMENTATION_EXPERIMENT_NEVER_STARTED</code>.

```csharp
public const int ExperimentationExperimentNeverStarted = -1994172765
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExperimentNotFound"></a> ExperimentationExperimentNotFound

<code>E_PF_EXPERIMENTATION_EXPERIMENT_NOT_FOUND</code>.

```csharp
public const int ExperimentationExperimentNotFound = -1994172766
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExperimentRunning"></a> ExperimentationExperimentRunning

<code>E_PF_EXPERIMENTATION_EXPERIMENT_RUNNING</code>.

```csharp
public const int ExperimentationExperimentRunning = -1994172767
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExperimentSchedulingInProgress"></a> ExperimentationExperimentSchedulingInProgress

<code>E_PF_EXPERIMENTATION_EXPERIMENT_SCHEDULING_IN_PROGRESS</code>.

```csharp
public const int ExperimentationExperimentSchedulingInProgress = -1994172754
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExperimentStopFailed"></a> ExperimentationExperimentStopFailed

<code>E_PF_EXPERIMENTATION_EXPERIMENT_STOP_FAILED</code>.

```csharp
public const int ExperimentationExperimentStopFailed = -1994172455
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationExperimentStopped"></a> ExperimentationExperimentStopped

<code>E_PF_EXPERIMENTATION_EXPERIMENT_STOPPED</code>.

```csharp
public const int ExperimentationExperimentStopped = -1994172768
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationInvalidDuration"></a> ExperimentationInvalidDuration

<code>E_PF_EXPERIMENTATION_INVALID_DURATION</code>.

```csharp
public const int ExperimentationInvalidDuration = -1994172756
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationInvalidEndDate"></a> ExperimentationInvalidEndDate

<code>E_PF_EXPERIMENTATION_INVALID_END_DATE</code>.

```csharp
public const int ExperimentationInvalidEndDate = -1994172753
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationInvalidStartDate"></a> ExperimentationInvalidStartDate

<code>E_PF_EXPERIMENTATION_INVALID_START_DATE</code>.

```csharp
public const int ExperimentationInvalidStartDate = -1994172752
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationInvalidVariableConfiguration"></a> ExperimentationInvalidVariableConfiguration

<code>E_PF_EXPERIMENTATION_INVALID_VARIABLE_CONFIGURATION</code>.

```csharp
public const int ExperimentationInvalidVariableConfiguration = -1994172761
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationInvalidVariantConfiguration"></a> ExperimentationInvalidVariantConfiguration

<code>E_PF_EXPERIMENTATION_INVALID_VARIANT_CONFIGURATION</code>.

```csharp
public const int ExperimentationInvalidVariantConfiguration = -1994172762
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationLegacyExperimentInvalidOperation"></a> ExperimentationLegacyExperimentInvalidOperation

<code>E_PF_EXPERIMENTATION_LEGACY_EXPERIMENT_INVALID_OPERATION</code>.

```csharp
public const int ExperimentationLegacyExperimentInvalidOperation = -1994172512
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationMaxDurationExceeded"></a> ExperimentationMaxDurationExceeded

<code>E_PF_EXPERIMENTATION_MAX_DURATION_EXCEEDED</code>.

```csharp
public const int ExperimentationMaxDurationExceeded = -1994172751
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationMaxExperimentsReached"></a> ExperimentationMaxExperimentsReached

<code>E_PF_EXPERIMENTATION_MAX_EXPERIMENTS_REACHED</code>.

```csharp
public const int ExperimentationMaxExperimentsReached = -1994172755
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationNoScorecard"></a> ExperimentationNoScorecard

<code>E_PF_EXPERIMENTATION_NO_SCORECARD</code>.

```csharp
public const int ExperimentationNoScorecard = -1994172759
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationTreatmentAssignmentDisabled"></a> ExperimentationTreatmentAssignmentDisabled

<code>E_PF_EXPERIMENTATION_TREATMENT_ASSIGNMENT_DISABLED</code>.

```csharp
public const int ExperimentationTreatmentAssignmentDisabled = -1994172757
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExperimentationTreatmentAssignmentFailed"></a> ExperimentationTreatmentAssignmentFailed

<code>E_PF_EXPERIMENTATION_TREATMENT_ASSIGNMENT_FAILED</code>.

```csharp
public const int ExperimentationTreatmentAssignmentFailed = -1994172758
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExpiredAuthToken"></a> ExpiredAuthToken

<code>E_PF_EXPIRED_AUTH_TOKEN</code>.

```csharp
public const int ExpiredAuthToken = -1994173265
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExpiredContinuationToken"></a> ExpiredContinuationToken

<code>E_PF_EXPIRED_CONTINUATION_TOKEN</code>.

```csharp
public const int ExpiredContinuationToken = -1994173177
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExpiredGameTicket"></a> ExpiredGameTicket

<code>E_PF_EXPIRED_GAME_TICKET</code>.

```csharp
public const int ExpiredGameTicket = -1994173302
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExpiredXboxLiveToken"></a> ExpiredXboxLiveToken

<code>E_PF_EXPIRED_XBOX_LIVE_TOKEN</code>.

```csharp
public const int ExpiredXboxLiveToken = -1994173229
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplicitContentDetected"></a> ExplicitContentDetected

<code>E_PF_EXPLICIT_CONTENT_DETECTED</code>.

```csharp
public const int ExplicitContentDetected = -1994173030
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicCreateQueryError"></a> ExplorerBasicCreateQueryError

<code>E_PF_EXPLORER_BASIC_CREATE_QUERY_ERROR</code>.

```csharp
public const int ExplorerBasicCreateQueryError = -1994172780
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicDeleteQueryError"></a> ExplorerBasicDeleteQueryError

<code>E_PF_EXPLORER_BASIC_DELETE_QUERY_ERROR</code>.

```csharp
public const int ExplorerBasicDeleteQueryError = -1994172779
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicInvalidQueryAggregateProperty"></a> ExplorerBasicInvalidQueryAggregateProperty

<code>E_PF_EXPLORER_BASIC_INVALID_QUERY_AGGREGATE_PROPERTY</code>.

```csharp
public const int ExplorerBasicInvalidQueryAggregateProperty = -1994172783
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicInvalidQueryAggregateType"></a> ExplorerBasicInvalidQueryAggregateType

<code>E_PF_EXPLORER_BASIC_INVALID_QUERY_AGGREGATE_TYPE</code>.

```csharp
public const int ExplorerBasicInvalidQueryAggregateType = -1994172784
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicInvalidQueryConditions"></a> ExplorerBasicInvalidQueryConditions

<code>E_PF_EXPLORER_BASIC_INVALID_QUERY_CONDITIONS</code>.

```csharp
public const int ExplorerBasicInvalidQueryConditions = -1994172788
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicInvalidQueryDescription"></a> ExplorerBasicInvalidQueryDescription

<code>E_PF_EXPLORER_BASIC_INVALID_QUERY_DESCRIPTION</code>.

```csharp
public const int ExplorerBasicInvalidQueryDescription = -1994172789
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicInvalidQueryEndDate"></a> ExplorerBasicInvalidQueryEndDate

<code>E_PF_EXPLORER_BASIC_INVALID_QUERY_END_DATE</code>.

```csharp
public const int ExplorerBasicInvalidQueryEndDate = -1994172786
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicInvalidQueryGroupBy"></a> ExplorerBasicInvalidQueryGroupBy

<code>E_PF_EXPLORER_BASIC_INVALID_QUERY_GROUP_BY</code>.

```csharp
public const int ExplorerBasicInvalidQueryGroupBy = -1994172785
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicInvalidQueryName"></a> ExplorerBasicInvalidQueryName

<code>E_PF_EXPLORER_BASIC_INVALID_QUERY_NAME</code>.

```csharp
public const int ExplorerBasicInvalidQueryName = -1994172790
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicInvalidQueryStartDate"></a> ExplorerBasicInvalidQueryStartDate

<code>E_PF_EXPLORER_BASIC_INVALID_QUERY_START_DATE</code>.

```csharp
public const int ExplorerBasicInvalidQueryStartDate = -1994172787
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicLoadQueriesError"></a> ExplorerBasicLoadQueriesError

<code>E_PF_EXPLORER_BASIC_LOAD_QUERIES_ERROR</code>.

```csharp
public const int ExplorerBasicLoadQueriesError = -1994172782
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicLoadQueryError"></a> ExplorerBasicLoadQueryError

<code>E_PF_EXPLORER_BASIC_LOAD_QUERY_ERROR</code>.

```csharp
public const int ExplorerBasicLoadQueryError = -1994172781
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicSavedQueriesLimit"></a> ExplorerBasicSavedQueriesLimit

<code>E_PF_EXPLORER_BASIC_SAVED_QUERIES_LIMIT</code>.

```csharp
public const int ExplorerBasicSavedQueriesLimit = -1994172777
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicSavedQueryNotFound"></a> ExplorerBasicSavedQueryNotFound

<code>E_PF_EXPLORER_BASIC_SAVED_QUERY_NOT_FOUND</code>.

```csharp
public const int ExplorerBasicSavedQueryNotFound = -1994172776
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExplorerBasicUpdateQueryError"></a> ExplorerBasicUpdateQueryError

<code>E_PF_EXPLORER_BASIC_UPDATE_QUERY_ERROR</code>.

```csharp
public const int ExplorerBasicUpdateQueryError = -1994172778
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportAmazonBucketDoesNotExist"></a> ExportAmazonBucketDoesNotExist

<code>E_PF_EXPORT_AMAZON_BUCKET_DOES_NOT_EXIST</code>.

```csharp
public const int ExportAmazonBucketDoesNotExist = -1994172809
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportBlobContainerDoesNotExist"></a> ExportBlobContainerDoesNotExist

<code>E_PF_EXPORT_BLOB_CONTAINER_DOES_NOT_EXIST</code>.

```csharp
public const int ExportBlobContainerDoesNotExist = -1994172813
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportCannotDetermineEventQuery"></a> ExportCannotDetermineEventQuery

<code>E_PF_EXPORT_CANNOT_DETERMINE_EVENT_QUERY</code>.

```csharp
public const int ExportCannotDetermineEventQuery = -1994172797
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportCannotParseQuery"></a> ExportCannotParseQuery

<code>E_PF_EXPORT_CANNOT_PARSE_QUERY</code>.

```csharp
public const int ExportCannotParseQuery = -1994172794
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportCantEditPendingExport"></a> ExportCantEditPendingExport

<code>E_PF_EXPORT_CANT_EDIT_PENDING_EXPORT</code>.

```csharp
public const int ExportCantEditPendingExport = -1994172804
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportControlCommandsNotAllowed"></a> ExportControlCommandsNotAllowed

<code>E_PF_EXPORT_CONTROL_COMMANDS_NOT_ALLOWED</code>.

```csharp
public const int ExportControlCommandsNotAllowed = -1994172793
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportCouldNotCreate"></a> ExportCouldNotCreate

<code>E_PF_EXPORT_COULD_NOT_CREATE</code>.

```csharp
public const int ExportCouldNotCreate = -1994172800
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportCouldNotDelete"></a> ExportCouldNotDelete

<code>E_PF_EXPORT_COULD_NOT_DELETE</code>.

```csharp
public const int ExportCouldNotDelete = -1994172798
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportCouldNotUpdate"></a> ExportCouldNotUpdate

<code>E_PF_EXPORT_COULD_NOT_UPDATE</code>.

```csharp
public const int ExportCouldNotUpdate = -1994172811
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportInsightsV1Deprecated"></a> ExportInsightsV1Deprecated

<code>E_PF_EXPORT_INSIGHTS_V_1_DEPRECATED</code>.

```csharp
public const int ExportInsightsV1Deprecated = -1994172791
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportInvalidBlobStorage"></a> ExportInvalidBlobStorage

<code>E_PF_EXPORT_INVALID_BLOB_STORAGE</code>.

```csharp
public const int ExportInvalidBlobStorage = -1994172808
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportInvalidPartitionStatusModification"></a> ExportInvalidPartitionStatusModification

<code>E_PF_EXPORT_INVALID_PARTITION_STATUS_MODIFICATION</code>.

```csharp
public const int ExportInvalidPartitionStatusModification = -1994172801
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportInvalidPrefix"></a> ExportInvalidPrefix

<code>E_PF_EXPORT_INVALID_PREFIX</code>.

```csharp
public const int ExportInvalidPrefix = -1994172814
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportInvalidQuerySchemaModification"></a> ExportInvalidQuerySchemaModification

<code>E_PF_EXPORT_INVALID_QUERY_SCHEMA_MODIFICATION</code>.

```csharp
public const int ExportInvalidQuerySchemaModification = -1994172796
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportInvalidStatusUpdate"></a> ExportInvalidStatusUpdate

<code>E_PF_EXPORT_INVALID_STATUS_UPDATE</code>.

```csharp
public const int ExportInvalidStatusUpdate = -1994172815
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportInvalidStorageType"></a> ExportInvalidStorageType

<code>E_PF_EXPORT_INVALID_STORAGE_TYPE</code>.

```csharp
public const int ExportInvalidStorageType = -1994172810
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportKustoConnectionFailed"></a> ExportKustoConnectionFailed

<code>E_PF_EXPORT_KUSTO_CONNECTION_FAILED</code>.

```csharp
public const int ExportKustoConnectionFailed = -1994172806
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportKustoException"></a> ExportKustoException

<code>E_PF_EXPORT_KUSTO_EXCEPTION</code>.

```csharp
public const int ExportKustoException = -1994172807
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportLimitEvents"></a> ExportLimitEvents

<code>E_PF_EXPORT_LIMIT_EVENTS</code>.

```csharp
public const int ExportLimitEvents = -1994172802
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportLimitExports"></a> ExportLimitExports

<code>E_PF_EXPORT_LIMIT_EXPORTS</code>.

```csharp
public const int ExportLimitExports = -1994172803
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportNoBackingDatabaseFound"></a> ExportNoBackingDatabaseFound

<code>E_PF_EXPORT_NO_BACKING_DATABASE_FOUND</code>.

```csharp
public const int ExportNoBackingDatabaseFound = -1994172799
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportNotFound"></a> ExportNotFound

<code>E_PF_EXPORT_NOT_FOUND</code>.

```csharp
public const int ExportNotFound = -1994172812
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportQueryMissingTableReference"></a> ExportQueryMissingTableReference

<code>E_PF_EXPORT_QUERY_MISSING_TABLE_REFERENCE</code>.

```csharp
public const int ExportQueryMissingTableReference = -1994172792
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportQuerySchemaMissingRequiredColumns"></a> ExportQuerySchemaMissingRequiredColumns

<code>E_PF_EXPORT_QUERY_SCHEMA_MISSING_REQUIRED_COLUMNS</code>.

```csharp
public const int ExportQuerySchemaMissingRequiredColumns = -1994172795
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExportUnknownError"></a> ExportUnknownError

<code>E_PF_EXPORT_UNKNOWN_ERROR</code>.

```csharp
public const int ExportUnknownError = -1994172805
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExpressionInvokeFailure"></a> ExpressionInvokeFailure

<code>E_PF_EXPRESSION_INVOKE_FAILURE</code>.

```csharp
public const int ExpressionInvokeFailure = -1994173134
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExpressionParseFailure"></a> ExpressionParseFailure

<code>E_PF_EXPRESSION_PARSE_FAILURE</code>.

```csharp
public const int ExpressionParseFailure = -1994173135
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExpressionTooLong"></a> ExpressionTooLong

<code>E_PF_EXPRESSION_TOO_LONG</code>.

```csharp
public const int ExpressionTooLong = -1994173133
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ExternalEntityNotAllowedForTier"></a> ExternalEntityNotAllowedForTier

<code>E_PF_EXTERNAL_ENTITY_NOT_ALLOWED_FOR_TIER</code>.

```csharp
public const int ExternalEntityNotAllowedForTier = -1994172581
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FacebookApiError"></a> FacebookApiError

<code>E_PF_FACEBOOK_API_ERROR</code>.

```csharp
public const int FacebookApiError = -1994173275
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FacebookInstantGamesAuthNotConfiguredForTitle"></a> FacebookInstantGamesAuthNotConfiguredForTitle

<code>E_PF_FACEBOOK_INSTANT_GAMES_AUTH_NOT_CONFIGURED_FOR_TITLE</code>.

```csharp
public const int FacebookInstantGamesAuthNotConfiguredForTitle = -1994173022
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FacebookInstantGamesIdNotLinked"></a> FacebookInstantGamesIdNotLinked

<code>E_PF_FACEBOOK_INSTANT_GAMES_ID_NOT_LINKED</code>.

```csharp
public const int FacebookInstantGamesIdNotLinked = -1994173024
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FailedByPaymentProvider"></a> FailedByPaymentProvider

<code>E_PF_FAILED_BY_PAYMENT_PROVIDER</code>.

```csharp
public const int FailedByPaymentProvider = -1994173392
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FailedLoginAttemptRateLimitExceeded"></a> FailedLoginAttemptRateLimitExceeded

<code>E_PF_FAILED_LOGIN_ATTEMPT_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int FailedLoginAttemptRateLimitExceeded = -1994173063
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FailedToConsumeEntitlement"></a> FailedToConsumeEntitlement

<code>E_PF_FAILED_TO_CONSUME_ENTITLEMENT</code>.

```csharp
public const int FailedToConsumeEntitlement = -1994173263
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FailedToGetEntitlements"></a> FailedToGetEntitlements

<code>E_PF_FAILED_TO_GET_ENTITLEMENTS</code>.

```csharp
public const int FailedToGetEntitlements = -1994173264
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FeatureNotConfiguredForTitle"></a> FeatureNotConfiguredForTitle

<code>E_PF_FEATURE_NOT_CONFIGURED_FOR_TITLE</code>.

```csharp
public const int FeatureNotConfiguredForTitle = -1994173241
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FileNotFound"></a> FileNotFound

<code>E_PF_FILE_NOT_FOUND</code>.

```csharp
public const int FileNotFound = -1994173362
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FileTooLarge"></a> FileTooLarge

<code>E_PF_FILE_TOO_LARGE</code>.

```csharp
public const int FileTooLarge = -1994173073
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ForbiddenByEntityPolicy"></a> ForbiddenByEntityPolicy

<code>E_PF_FORBIDDEN_BY_ENTITY_POLICY</code>.

```csharp
public const int ForbiddenByEntityPolicy = -1994172966
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_FreeTierCannotHaveVirtualCurrency"></a> FreeTierCannotHaveVirtualCurrency

<code>E_PF_FREE_TIER_CANNOT_HAVE_VIRTUAL_CURRENCY</code>.

```csharp
public const int FreeTierCannotHaveVirtualCurrency = -1994173270
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameCenterAuthenticationFailed"></a> GameCenterAuthenticationFailed

<code>E_PF_GAME_CENTER_AUTHENTICATION_FAILED</code>.

```csharp
public const int GameCenterAuthenticationFailed = -1994172991
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameModeNotFound"></a> GameModeNotFound

<code>E_PF_GAME_MODE_NOT_FOUND</code>.

```csharp
public const int GameModeNotFound = -1994173382
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameNotFound"></a> GameNotFound

<code>E_PF_GAME_NOT_FOUND</code>.

```csharp
public const int GameNotFound = -1994173383
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveBadRequest"></a> GameSaveBadRequest

<code>E_PF_GAME_SAVE_BAD_REQUEST</code>.

```csharp
public const int GameSaveBadRequest = -1994172529
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveBaseVersionNotAvailable"></a> GameSaveBaseVersionNotAvailable

<code>E_PF_GAME_SAVE_BASE_VERSION_NOT_AVAILABLE</code>.

```csharp
public const int GameSaveBaseVersionNotAvailable = -1994172520
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveConflict"></a> GameSaveConflict

<code>E_PF_GAME_SAVE_CONFLICT</code>.

```csharp
public const int GameSaveConflict = -1994172474
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveConflictUpdatingManifest"></a> GameSaveConflictUpdatingManifest

<code>E_PF_GAME_SAVE_CONFLICT_UPDATING_MANIFEST</code>.

```csharp
public const int GameSaveConflictUpdatingManifest = -1994172543
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveDataStorageQuotaExceeded"></a> GameSaveDataStorageQuotaExceeded

<code>E_PF_GAME_SAVE_DATA_STORAGE_QUOTA_EXCEEDED</code>.

```csharp
public const int GameSaveDataStorageQuotaExceeded = -1994172523
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveFileAlreadyExists"></a> GameSaveFileAlreadyExists

<code>E_PF_GAME_SAVE_FILE_ALREADY_EXISTS</code>.

```csharp
public const int GameSaveFileAlreadyExists = -1994172537
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveFileExceededReportedSize"></a> GameSaveFileExceededReportedSize

<code>E_PF_GAME_SAVE_FILE_EXCEEDED_REPORTED_SIZE</code>.

```csharp
public const int GameSaveFileExceededReportedSize = -1994172534
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveFileNotUploaded"></a> GameSaveFileNotUploaded

<code>E_PF_GAME_SAVE_FILE_NOT_UPLOADED</code>.

```csharp
public const int GameSaveFileNotUploaded = -1994172533
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestDescriptionUpdateNotAllowed"></a> GameSaveManifestDescriptionUpdateNotAllowed

<code>E_PF_GAME_SAVE_MANIFEST_DESCRIPTION_UPDATE_NOT_ALLOWED</code>.

```csharp
public const int GameSaveManifestDescriptionUpdateNotAllowed = -1994172491
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestFilesLimitExceeded"></a> GameSaveManifestFilesLimitExceeded

<code>E_PF_GAME_SAVE_MANIFEST_FILES_LIMIT_EXCEEDED</code>.

```csharp
public const int GameSaveManifestFilesLimitExceeded = -1994172499
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestNotEligibleAsConflictingVersion"></a> GameSaveManifestNotEligibleAsConflictingVersion

<code>E_PF_GAME_SAVE_MANIFEST_NOT_ELIGIBLE_AS_CONFLICTING_VERSION</code>.

```csharp
public const int GameSaveManifestNotEligibleAsConflictingVersion = -1994172476
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestNotEligibleForRollback"></a> GameSaveManifestNotEligibleForRollback

<code>E_PF_GAME_SAVE_MANIFEST_NOT_ELIGIBLE_FOR_ROLLBACK</code>.

```csharp
public const int GameSaveManifestNotEligibleForRollback = -1994172473
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestNotFound"></a> GameSaveManifestNotFound

<code>E_PF_GAME_SAVE_MANIFEST_NOT_FOUND</code>.

```csharp
public const int GameSaveManifestNotFound = -1994172545
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestUpdatesNotAllowed"></a> GameSaveManifestUpdatesNotAllowed

<code>E_PF_GAME_SAVE_MANIFEST_UPDATES_NOT_ALLOWED</code>.

```csharp
public const int GameSaveManifestUpdatesNotAllowed = -1994172538
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestUploadProgressUpdateNotAllowed"></a> GameSaveManifestUploadProgressUpdateNotAllowed

<code>E_PF_GAME_SAVE_MANIFEST_UPLOAD_PROGRESS_UPDATE_NOT_ALLOWED</code>.

```csharp
public const int GameSaveManifestUploadProgressUpdateNotAllowed = -1994172504
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestVersionAlreadyExists"></a> GameSaveManifestVersionAlreadyExists

<code>E_PF_GAME_SAVE_MANIFEST_VERSION_ALREADY_EXISTS</code>.

```csharp
public const int GameSaveManifestVersionAlreadyExists = -1994172544
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestVersionNotFinalized"></a> GameSaveManifestVersionNotFinalized

<code>E_PF_GAME_SAVE_MANIFEST_VERSION_NOT_FINALIZED</code>.

```csharp
public const int GameSaveManifestVersionNotFinalized = -1994172536
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveManifestVersionQuarantined"></a> GameSaveManifestVersionQuarantined

<code>E_PF_GAME_SAVE_MANIFEST_VERSION_QUARANTINED</code>.

```csharp
public const int GameSaveManifestVersionQuarantined = -1994172517
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveNewerManifestExists"></a> GameSaveNewerManifestExists

<code>E_PF_GAME_SAVE_NEWER_MANIFEST_EXISTS</code>.

```csharp
public const int GameSaveNewerManifestExists = -1994172522
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveNoUpdatesRequested"></a> GameSaveNoUpdatesRequested

<code>E_PF_GAME_SAVE_NO_UPDATES_REQUESTED</code>.

```csharp
public const int GameSaveNoUpdatesRequested = -1994172502
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveNotFinalizedManifestNotEligibleAsKnownGood"></a> GameSaveNotFinalizedManifestNotEligibleAsKnownGood

<code>E_PF_GAME_SAVE_NOT_FINALIZED_MANIFEST_NOT_ELIGIBLE_AS_KNOWN_GOOD</code>.

```csharp
public const int GameSaveNotFinalizedManifestNotEligibleAsKnownGood = -1994172503
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveOperationNotAllowed"></a> GameSaveOperationNotAllowed

<code>E_PF_GAME_SAVE_OPERATION_NOT_ALLOWED</code>.

```csharp
public const int GameSaveOperationNotAllowed = -1994172528
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveOperationNotAllowedForTitle"></a> GameSaveOperationNotAllowedForTitle

<code>E_PF_GAME_SAVE_OPERATION_NOT_ALLOWED_FOR_TITLE</code>.

```csharp
public const int GameSaveOperationNotAllowedForTitle = -1994172500
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveServiceNotEnabledForTitle"></a> GameSaveServiceNotEnabledForTitle

<code>E_PF_GAME_SAVE_SERVICE_NOT_ENABLED_FOR_TITLE</code>.

```csharp
public const int GameSaveServiceNotEnabledForTitle = -1994172488
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveServiceOnboardingPending"></a> GameSaveServiceOnboardingPending

<code>E_PF_GAME_SAVE_SERVICE_ONBOARDING_PENDING</code>.

```csharp
public const int GameSaveServiceOnboardingPending = -1994172487
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveServiceUnavailable"></a> GameSaveServiceUnavailable

<code>E_PF_GAME_SAVE_SERVICE_UNAVAILABLE</code>.

```csharp
public const int GameSaveServiceUnavailable = -1994172475
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveTitleAlreadyOnboarded"></a> GameSaveTitleAlreadyOnboarded

<code>E_PF_GAME_SAVE_TITLE_ALREADY_ONBOARDED</code>.

```csharp
public const int GameSaveTitleAlreadyOnboarded = -1994172489
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveTitleClientAnonymousAccountCreationNotDisabled"></a> GameSaveTitleClientAnonymousAccountCreationNotDisabled

<code>E_PF_GAME_SAVE_TITLE_CLIENT_ANONYMOUS_ACCOUNT_CREATION_NOT_DISABLED</code>.

```csharp
public const int GameSaveTitleClientAnonymousAccountCreationNotDisabled = -1994172458
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveTitleConfigNotFound"></a> GameSaveTitleConfigNotFound

<code>E_PF_GAME_SAVE_TITLE_CONFIG_NOT_FOUND</code>.

```csharp
public const int GameSaveTitleConfigNotFound = -1994172490
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveTitleDoesNotExist"></a> GameSaveTitleDoesNotExist

<code>E_PF_GAME_SAVE_TITLE_DOES_NOT_EXIST</code>.

```csharp
public const int GameSaveTitleDoesNotExist = -1994172501
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameSaveUnknownFileInManifest"></a> GameSaveUnknownFileInManifest

<code>E_PF_GAME_SAVE_UNKNOWN_FILE_IN_MANIFEST</code>.

```csharp
public const int GameSaveUnknownFileInManifest = -1994172535
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameServerBuildCountLimitExceeded"></a> GameServerBuildCountLimitExceeded

<code>E_PF_GAME_SERVER_BUILD_COUNT_LIMIT_EXCEEDED</code>.

```csharp
public const int GameServerBuildCountLimitExceeded = -1994173190
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameServerBuildSizeLimitExceeded"></a> GameServerBuildSizeLimitExceeded

<code>E_PF_GAME_SERVER_BUILD_SIZE_LIMIT_EXCEEDED</code>.

```csharp
public const int GameServerBuildSizeLimitExceeded = -1994173191
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameServerHostCountLimitExceeded"></a> GameServerHostCountLimitExceeded

<code>E_PF_GAME_SERVER_HOST_COUNT_LIMIT_EXCEEDED</code>.

```csharp
public const int GameServerHostCountLimitExceeded = -1994173171
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GameTicketDoesNotMatchLobby"></a> GameTicketDoesNotMatchLobby

<code>E_PF_GAME_TICKET_DOES_NOT_MATCH_LOBBY</code>.

```csharp
public const int GameTicketDoesNotMatchLobby = -1994173301
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GetPlayersInSegmentRateLimitExceeded"></a> GetPlayersInSegmentRateLimitExceeded

<code>E_PF_GET_PLAYERS_IN_SEGMENT_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int GetPlayersInSegmentRateLimitExceeded = -1994172929
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GetPlayersInSegmentRetired"></a> GetPlayersInSegmentRetired

<code>E_PF_GET_PLAYERS_IN_SEGMENT_RETIRED</code>.

```csharp
public const int GetPlayersInSegmentRetired = -1994172452
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GetSegmentsRateLimitExceeded"></a> GetSegmentsRateLimitExceeded

<code>E_PF_GET_SEGMENTS_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int GetSegmentsRateLimitExceeded = -1994172733
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GoogleApiServiceUnavailable"></a> GoogleApiServiceUnavailable

<code>E_PF_GOOGLE_API_SERVICE_UNAVAILABLE</code>.

```csharp
public const int GoogleApiServiceUnavailable = -1994172887
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GoogleApiServiceUnknownError"></a> GoogleApiServiceUnknownError

<code>E_PF_GOOGLE_API_SERVICE_UNKNOWN_ERROR</code>.

```csharp
public const int GoogleApiServiceUnknownError = -1994172886
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GoogleOAuthError"></a> GoogleOAuthError

<code>E_PF_GOOGLE_O_AUTH_ERROR</code>.

```csharp
public const int GoogleOAuthError = -1994173147
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GoogleOAuthNoIdTokenIncludedInResponse"></a> GoogleOAuthNoIdTokenIncludedInResponse

<code>E_PF_GOOGLE_O_AUTH_NO_ID_TOKEN_INCLUDED_IN_RESPONSE</code>.

```csharp
public const int GoogleOAuthNoIdTokenIncludedInResponse = -1994173143
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GoogleOAuthNotConfiguredForTitle"></a> GoogleOAuthNotConfiguredForTitle

<code>E_PF_GOOGLE_O_AUTH_NOT_CONFIGURED_FOR_TITLE</code>.

```csharp
public const int GoogleOAuthNotConfiguredForTitle = -1994173148
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GoogleServiceAccountFailedAuth"></a> GoogleServiceAccountFailedAuth

<code>E_PF_GOOGLE_SERVICE_ACCOUNT_FAILED_AUTH</code>.

```csharp
public const int GoogleServiceAccountFailedAuth = -1994172888
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GoogleServiceAccountInvalid"></a> GoogleServiceAccountInvalid

<code>E_PF_GOOGLE_SERVICE_ACCOUNT_INVALID</code>.

```csharp
public const int GoogleServiceAccountInvalid = -1994173087
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GoogleServiceAccountParseFailure"></a> GoogleServiceAccountParseFailure

<code>E_PF_GOOGLE_SERVICE_ACCOUNT_PARSE_FAILURE</code>.

```csharp
public const int GoogleServiceAccountParseFailure = -1994173086
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GroupApplicationNotFound"></a> GroupApplicationNotFound

<code>E_PF_GROUP_APPLICATION_NOT_FOUND</code>.

```csharp
public const int GroupApplicationNotFound = -1994173057
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GroupInvitationNotFound"></a> GroupInvitationNotFound

<code>E_PF_GROUP_INVITATION_NOT_FOUND</code>.

```csharp
public const int GroupInvitationNotFound = -1994173058
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GroupNameNotAvailable"></a> GroupNameNotAvailable

<code>E_PF_GROUP_NAME_NOT_AVAILABLE</code>.

```csharp
public const int GroupNameNotAvailable = -1994173051
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_GuildNotFound"></a> GuildNotFound

<code>E_PF_GUILD_NOT_FOUND</code>.

```csharp
public const int GuildNotFound = -1994173205
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_IdentifierAlreadyClaimed"></a> IdentifierAlreadyClaimed

<code>E_PF_IDENTIFIER_ALREADY_CLAIMED</code>.

```csharp
public const int IdentifierAlreadyClaimed = -1994173180
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_IdentifierNotLinked"></a> IdentifierNotLinked

<code>E_PF_IDENTIFIER_NOT_LINKED</code>.

```csharp
public const int IdentifierNotLinked = -1994173179
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementDatabaseNotFound"></a> InsightsManagementDatabaseNotFound

<code>E_PF_INSIGHTS_MANAGEMENT_DATABASE_NOT_FOUND</code>.

```csharp
public const int InsightsManagementDatabaseNotFound = -1994172938
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementErrorPendingOperationExists"></a> InsightsManagementErrorPendingOperationExists

<code>E_PF_INSIGHTS_MANAGEMENT_ERROR_PENDING_OPERATION_EXISTS</code>.

```csharp
public const int InsightsManagementErrorPendingOperationExists = -1994172936
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementGetOperationStatusInvalidParameter"></a> InsightsManagementGetOperationStatusInvalidParameter

<code>E_PF_INSIGHTS_MANAGEMENT_GET_OPERATION_STATUS_INVALID_PARAMETER</code>.

```csharp
public const int InsightsManagementGetOperationStatusInvalidParameter = -1994172932
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementGetStorageUsageInvalidParameter"></a> InsightsManagementGetStorageUsageInvalidParameter

<code>E_PF_INSIGHTS_MANAGEMENT_GET_STORAGE_USAGE_INVALID_PARAMETER</code>.

```csharp
public const int InsightsManagementGetStorageUsageInvalidParameter = -1994172933
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementNewActiveEventExportLimitInvalid"></a> InsightsManagementNewActiveEventExportLimitInvalid

<code>E_PF_INSIGHTS_MANAGEMENT_NEW_ACTIVE_EVENT_EXPORT_LIMIT_INVALID</code>.

```csharp
public const int InsightsManagementNewActiveEventExportLimitInvalid = -1994172918
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementOperationNotFound"></a> InsightsManagementOperationNotFound

<code>E_PF_INSIGHTS_MANAGEMENT_OPERATION_NOT_FOUND</code>.

```csharp
public const int InsightsManagementOperationNotFound = -1994172937
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementSetPerformanceLevelInvalidParameter"></a> InsightsManagementSetPerformanceLevelInvalidParameter

<code>E_PF_INSIGHTS_MANAGEMENT_SET_PERFORMANCE_LEVEL_INVALID_PARAMETER</code>.

```csharp
public const int InsightsManagementSetPerformanceLevelInvalidParameter = -1994172935
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementSetPerformanceRateLimited"></a> InsightsManagementSetPerformanceRateLimited

<code>E_PF_INSIGHTS_MANAGEMENT_SET_PERFORMANCE_RATE_LIMITED</code>.

```csharp
public const int InsightsManagementSetPerformanceRateLimited = -1994172917
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementSetStorageRetentionAboveMaximum"></a> InsightsManagementSetStorageRetentionAboveMaximum

<code>E_PF_INSIGHTS_MANAGEMENT_SET_STORAGE_RETENTION_ABOVE_MAXIMUM</code>.

```csharp
public const int InsightsManagementSetStorageRetentionAboveMaximum = -1994172920
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementSetStorageRetentionBelowMinimum"></a> InsightsManagementSetStorageRetentionBelowMinimum

<code>E_PF_INSIGHTS_MANAGEMENT_SET_STORAGE_RETENTION_BELOW_MINIMUM</code>.

```csharp
public const int InsightsManagementSetStorageRetentionBelowMinimum = -1994172921
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementSetStorageRetentionInvalidParameter"></a> InsightsManagementSetStorageRetentionInvalidParameter

<code>E_PF_INSIGHTS_MANAGEMENT_SET_STORAGE_RETENTION_INVALID_PARAMETER</code>.

```csharp
public const int InsightsManagementSetStorageRetentionInvalidParameter = -1994172934
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsManagementTitleNotInFlight"></a> InsightsManagementTitleNotInFlight

<code>E_PF_INSIGHTS_MANAGEMENT_TITLE_NOT_IN_FLIGHT</code>.

```csharp
public const int InsightsManagementTitleNotInFlight = -1994172924
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsightsV1Deprecated"></a> InsightsV1Deprecated

<code>E_PF_INSIGHTS_V_1_DEPRECATED</code>.

```csharp
public const int InsightsV1Deprecated = -1994172873
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsufficientFunds"></a> InsufficientFunds

<code>E_PF_INSUFFICIENT_FUNDS</code>.

```csharp
public const int InsufficientFunds = -1994173348
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InsufficientGuildRole"></a> InsufficientGuildRole

<code>E_PF_INSUFFICIENT_GUILD_ROLE</code>.

```csharp
public const int InsufficientGuildRole = -1994173206
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InternalServerError"></a> InternalServerError

<code>E_PF_INTERNAL_SERVER_ERROR</code>.

```csharp
public const int InternalServerError = -1994173307
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidAccount"></a> InvalidAccount

<code>E_PF_INVALID_ACCOUNT</code>.

```csharp
public const int InvalidAccount = -1994173329
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidAdPlacementAndReward"></a> InvalidAdPlacementAndReward

<code>E_PF_INVALID_AD_PLACEMENT_AND_REWARD</code>.

```csharp
public const int InvalidAdPlacementAndReward = -1994173150
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidApiEndpoint"></a> InvalidApiEndpoint

<code>E_PF_INVALID_API_ENDPOINT</code>.

```csharp
public const int InvalidApiEndpoint = -1994173287
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidAttributeStatisticsSpecified"></a> InvalidAttributeStatisticsSpecified

<code>E_PF_INVALID_ATTRIBUTE_STATISTICS_SPECIFIED</code>.

```csharp
public const int InvalidAttributeStatisticsSpecified = -1994172654
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidAuthToken"></a> InvalidAuthToken

<code>E_PF_INVALID_AUTH_TOKEN</code>.

```csharp
public const int InvalidAuthToken = -1994173093
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidBaseTimeForInterval"></a> InvalidBaseTimeForInterval

<code>E_PF_INVALID_BASE_TIME_FOR_INTERVAL</code>.

```csharp
public const int InvalidBaseTimeForInterval = -1994172561
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidBundleId"></a> InvalidBundleId

<code>E_PF_INVALID_BUNDLE_ID</code>.

```csharp
public const int InvalidBundleId = -1994173311
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidBuyerInfo"></a> InvalidBuyerInfo

<code>E_PF_INVALID_BUYER_INFO</code>.

```csharp
public const int InvalidBuyerInfo = -1994173341
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidCatalogItemConfiguration"></a> InvalidCatalogItemConfiguration

<code>E_PF_INVALID_CATALOG_ITEM_CONFIGURATION</code>.

```csharp
public const int InvalidCatalogItemConfiguration = -1994172655
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidCertificateForAad"></a> InvalidCertificateForAad

<code>E_PF_INVALID_CERTIFICATE_FOR_AAD</code>.

```csharp
public const int InvalidCertificateForAad = -1994173042
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidCharacterStatistics"></a> InvalidCharacterStatistics

<code>E_PF_INVALID_CHARACTER_STATISTICS</code>.

```csharp
public const int InvalidCharacterStatistics = -1994173280
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidContainerItem"></a> InvalidContainerItem

<code>E_PF_INVALID_CONTAINER_ITEM</code>.

```csharp
public const int InvalidContainerItem = -1994173390
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidContentType"></a> InvalidContentType

<code>E_PF_INVALID_CONTENT_TYPE</code>.

```csharp
public const int InvalidContentType = -1994173274
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidContinuationToken"></a> InvalidContinuationToken

<code>E_PF_INVALID_CONTINUATION_TOKEN</code>.

```csharp
public const int InvalidContinuationToken = -1994173178
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidCurrencyCode"></a> InvalidCurrencyCode

<code>E_PF_INVALID_CURRENCY_CODE</code>.

```csharp
public const int InvalidCurrencyCode = -1994173239
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidDeveloper"></a> InvalidDeveloper

<code>E_PF_INVALID_DEVELOPER</code>.

```csharp
public const int InvalidDeveloper = -1994173372
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidDeviceId"></a> InvalidDeviceId

<code>E_PF_INVALID_DEVICE_ID</code>.

```csharp
public const int InvalidDeviceId = -1994173347
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidDisplayNameRandomSuffixLength"></a> InvalidDisplayNameRandomSuffixLength

<code>E_PF_INVALID_DISPLAY_NAME_RANDOM_SUFFIX_LENGTH</code>.

```csharp
public const int InvalidDisplayNameRandomSuffixLength = -1994172683
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidDropTable"></a> InvalidDropTable

<code>E_PF_INVALID_DROP_TABLE</code>.

```csharp
public const int InvalidDropTable = -1994173217
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidEmailAddress"></a> InvalidEmailAddress

<code>E_PF_INVALID_EMAIL_ADDRESS</code>.

```csharp
public const int InvalidEmailAddress = -1994173402
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidEmailOrPassword"></a> InvalidEmailOrPassword

<code>E_PF_INVALID_EMAIL_OR_PASSWORD</code>.

```csharp
public const int InvalidEmailOrPassword = -1994173276
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidEntityId"></a> InvalidEntityId

<code>E_PF_INVALID_ENTITY_ID</code>.

```csharp
public const int InvalidEntityId = -1994173112
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidEntityType"></a> InvalidEntityType

<code>E_PF_INVALID_ENTITY_TYPE</code>.

```csharp
public const int InvalidEntityType = -1994173046
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidEntityTypeForAggregation"></a> InvalidEntityTypeForAggregation

<code>E_PF_INVALID_ENTITY_TYPE_FOR_AGGREGATION</code>.

```csharp
public const int InvalidEntityTypeForAggregation = -1994172478
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidEnvironmentForReceipt"></a> InvalidEnvironmentForReceipt

<code>E_PF_INVALID_ENVIRONMENT_FOR_RECEIPT</code>.

```csharp
public const int InvalidEnvironmentForReceipt = -1994173119
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidEventContents"></a> InvalidEventContents

<code>E_PF_INVALID_EVENT_CONTENTS</code>.

```csharp
public const int InvalidEventContents = -1994172874
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidEventField"></a> InvalidEventField

<code>E_PF_INVALID_EVENT_FIELD</code>.

```csharp
public const int InvalidEventField = -1994173202
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidEventName"></a> InvalidEventName

<code>E_PF_INVALID_EVENT_NAME</code>.

```csharp
public const int InvalidEventName = -1994173201
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidExternalEntityId"></a> InvalidExternalEntityId

<code>E_PF_INVALID_EXTERNAL_ENTITY_ID</code>.

```csharp
public const int InvalidExternalEntityId = -1994172527
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidFacebookInstantGamesSignature"></a> InvalidFacebookInstantGamesSignature

<code>E_PF_INVALID_FACEBOOK_INSTANT_GAMES_SIGNATURE</code>.

```csharp
public const int InvalidFacebookInstantGamesSignature = -1994173023
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidFacebookToken"></a> InvalidFacebookToken

<code>E_PF_INVALID_FACEBOOK_TOKEN</code>.

```csharp
public const int InvalidFacebookToken = -1994173394
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidGameCenterAuthRequest"></a> InvalidGameCenterAuthRequest

<code>E_PF_INVALID_GAME_CENTER_AUTH_REQUEST</code>.

```csharp
public const int InvalidGameCenterAuthRequest = -1994172992
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidGameCenterId"></a> InvalidGameCenterId

<code>E_PF_INVALID_GAME_CENTER_ID</code>.

```csharp
public const int InvalidGameCenterId = -1994172868
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidGameTicket"></a> InvalidGameTicket

<code>E_PF_INVALID_GAME_TICKET</code>.

```csharp
public const int InvalidGameTicket = -1994173303
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidGooglePlayGamesServerAuthCode"></a> InvalidGooglePlayGamesServerAuthCode

<code>E_PF_INVALID_GOOGLE_PLAY_GAMES_SERVER_AUTH_CODE</code>.

```csharp
public const int InvalidGooglePlayGamesServerAuthCode = -1994172876
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidGoogleToken"></a> InvalidGoogleToken

<code>E_PF_INVALID_GOOGLE_TOKEN</code>.

```csharp
public const int InvalidGoogleToken = -1994173381
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidHostForTitleId"></a> InvalidHostForTitleId

<code>E_PF_INVALID_HOST_FOR_TITLE_ID</code>.

```csharp
public const int InvalidHostForTitleId = -1994173100
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidIdentityProviderId"></a> InvalidIdentityProviderId

<code>E_PF_INVALID_IDENTITY_PROVIDER_ID</code>.

```csharp
public const int InvalidIdentityProviderId = -1994173155
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidItemId"></a> InvalidItemId

<code>E_PF_INVALID_ITEM_ID</code>.

```csharp
public const int InvalidItemId = -1994173316
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidItemIdInTable"></a> InvalidItemIdInTable

<code>E_PF_INVALID_ITEM_ID_IN_TABLE</code>.

```csharp
public const int InvalidItemIdInTable = -1994173387
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidItemProperties"></a> InvalidItemProperties

<code>E_PF_INVALID_ITEM_PROPERTIES</code>.

```csharp
public const int InvalidItemProperties = -1994173318
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidJsonContent"></a> InvalidJsonContent

<code>E_PF_INVALID_JSON_CONTENT</code>.

```csharp
public const int InvalidJsonContent = -1994173218
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidKongregateToken"></a> InvalidKongregateToken

<code>E_PF_INVALID_KONGREGATE_TOKEN</code>.

```csharp
public const int InvalidKongregateToken = -1994173242
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidLocalizedPushNotificationLanguage"></a> InvalidLocalizedPushNotificationLanguage

<code>E_PF_INVALID_LOCALIZED_PUSH_NOTIFICATION_LANGUAGE</code>.

```csharp
public const int InvalidLocalizedPushNotificationLanguage = -1994173011
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidNamespaceMismatch"></a> InvalidNamespaceMismatch

<code>E_PF_INVALID_NAMESPACE_MISMATCH</code>.

```csharp
public const int InvalidNamespaceMismatch = -1994172861
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidNintendoSwitchAccountId"></a> InvalidNintendoSwitchAccountId

<code>E_PF_INVALID_NINTENDO_SWITCH_ACCOUNT_ID</code>.

```csharp
public const int InvalidNintendoSwitchAccountId = -1994172867
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidOrderInfo"></a> InvalidOrderInfo

<code>E_PF_INVALID_ORDER_INFO</code>.

```csharp
public const int InvalidOrderInfo = -1994173371
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidParams"></a> InvalidParams

<code>E_PF_INVALID_PARAMS</code>.

```csharp
public const int InvalidParams = -1994173407
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPartnerResponse"></a> InvalidPartnerResponse

<code>E_PF_INVALID_PARTNER_RESPONSE</code>.

```csharp
public const int InvalidPartnerResponse = -1994173225
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPassword"></a> InvalidPassword

<code>E_PF_INVALID_PASSWORD</code>.

```csharp
public const int InvalidPassword = -1994173399
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPaymentProvider"></a> InvalidPaymentProvider

<code>E_PF_INVALID_PAYMENT_PROVIDER</code>.

```csharp
public const int InvalidPaymentProvider = -1994173344
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPlatform"></a> InvalidPlatform

<code>E_PF_INVALID_PLATFORM</code>.

```csharp
public const int InvalidPlatform = -1994173369
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPlayerAccountPoolId"></a> InvalidPlayerAccountPoolId

<code>E_PF_INVALID_PLAYER_ACCOUNT_POOL_ID</code>.

```csharp
public const int InvalidPlayerAccountPoolId = -1994172896
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidProductForSubscription"></a> InvalidProductForSubscription

<code>E_PF_INVALID_PRODUCT_FOR_SUBSCRIPTION</code>.

```csharp
public const int InvalidProductForSubscription = -1994173081
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPsnAuthCode"></a> InvalidPsnAuthCode

<code>E_PF_INVALID_PSN_AUTH_CODE</code>.

```csharp
public const int InvalidPsnAuthCode = -1994173317
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPsnIssuerId"></a> InvalidPsnIssuerId

<code>E_PF_INVALID_PSN_ISSUER_ID</code>.

```csharp
public const int InvalidPsnIssuerId = -1994173267
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPublicKey"></a> InvalidPublicKey

<code>E_PF_INVALID_PUBLIC_KEY</code>.

```csharp
public const int InvalidPublicKey = -1994173144
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPublisherId"></a> InvalidPublisherId

<code>E_PF_INVALID_PUBLISHER_ID</code>.

```csharp
public const int InvalidPublisherId = -1994173292
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPurchaseTransactionStatus"></a> InvalidPurchaseTransactionStatus

<code>E_PF_INVALID_PURCHASE_TRANSACTION_STATUS</code>.

```csharp
public const int InvalidPurchaseTransactionStatus = -1994173327
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidPushNotificationToken"></a> InvalidPushNotificationToken

<code>E_PF_INVALID_PUSH_NOTIFICATION_TOKEN</code>.

```csharp
public const int InvalidPushNotificationToken = -1994173346
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidReceipt"></a> InvalidReceipt

<code>E_PF_INVALID_RECEIPT</code>.

```csharp
public const int InvalidReceipt = -1994173386
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidRegion"></a> InvalidRegion

<code>E_PF_INVALID_REGION</code>.

```csharp
public const int InvalidRegion = -1994173352
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidReportDate"></a> InvalidReportDate

<code>E_PF_INVALID_REPORT_DATE</code>.

```csharp
public const int InvalidReportDate = -1994173306
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidReportName"></a> InvalidReportName

<code>E_PF_INVALID_REPORT_NAME</code>.

```csharp
public const int InvalidReportName = -1994172495
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidRequest"></a> InvalidRequest

<code>E_PF_INVALID_REQUEST</code>.

```csharp
public const int InvalidRequest = -1994173336
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidScheduledTaskName"></a> InvalidScheduledTaskName

<code>E_PF_INVALID_SCHEDULED_TASK_NAME</code>.

```csharp
public const int InvalidScheduledTaskName = -1994173162
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidScheduledTaskParameter"></a> InvalidScheduledTaskParameter

<code>E_PF_INVALID_SCHEDULED_TASK_PARAMETER</code>.

```csharp
public const int InvalidScheduledTaskParameter = -1994173028
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidScheduledTaskType"></a> InvalidScheduledTaskType

<code>E_PF_INVALID_SCHEDULED_TASK_TYPE</code>.

```csharp
public const int InvalidScheduledTaskType = -1994173153
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSearchTerm"></a> InvalidSearchTerm

<code>E_PF_INVALID_SEARCH_TERM</code>.

```csharp
public const int InvalidSearchTerm = -1994173173
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSegment"></a> InvalidSegment

<code>E_PF_INVALID_SEGMENT</code>.

```csharp
public const int InvalidSegment = -1994173176
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidServiceConfiguration"></a> InvalidServiceConfiguration

<code>E_PF_INVALID_SERVICE_CONFIGURATION</code>.

```csharp
public const int InvalidServiceConfiguration = -1994172862
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidServiceLimitLevel"></a> InvalidServiceLimitLevel

<code>E_PF_INVALID_SERVICE_LIMIT_LEVEL</code>.

```csharp
public const int InvalidServiceLimitLevel = -1994173194
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSessionId"></a> InvalidSessionId

<code>E_PF_INVALID_SESSION_ID</code>.

```csharp
public const int InvalidSessionId = -1994173175
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSessionTicket"></a> InvalidSessionTicket

<code>E_PF_INVALID_SESSION_TICKET</code>.

```csharp
public const int InvalidSessionTicket = -1994173309
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSharedGroupId"></a> InvalidSharedGroupId

<code>E_PF_INVALID_SHARED_GROUP_ID</code>.

```csharp
public const int InvalidSharedGroupId = -1994173321
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSharedSecretKey"></a> InvalidSharedSecretKey

<code>E_PF_INVALID_SHARED_SECRET_KEY</code>.

```csharp
public const int InvalidSharedSecretKey = -1994173123
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSignature"></a> InvalidSignature

<code>E_PF_INVALID_SIGNATURE</code>.

```csharp
public const int InvalidSignature = -1994173145
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSignatureTime"></a> InvalidSignatureTime

<code>E_PF_INVALID_SIGNATURE_TIME</code>.

```csharp
public const int InvalidSignatureTime = -1994173095
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidStatistic"></a> InvalidStatistic

<code>E_PF_INVALID_STATISTIC</code>.

```csharp
public const int InvalidStatistic = -1994173136
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidStatisticName"></a> InvalidStatisticName

<code>E_PF_INVALID_STATISTIC_NAME</code>.

```csharp
public const int InvalidStatisticName = -1994173196
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidStatisticScore"></a> InvalidStatisticScore

<code>E_PF_INVALID_STATISTIC_SCORE</code>.

```csharp
public const int InvalidStatisticScore = -1994172859
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSteamTicket"></a> InvalidSteamTicket

<code>E_PF_INVALID_STEAM_TICKET</code>.

```csharp
public const int InvalidSteamTicket = -1994173397
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidSteamUsername"></a> InvalidSteamUsername

<code>E_PF_INVALID_STEAM_USERNAME</code>.

```csharp
public const int InvalidSteamUsername = -1994172516
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidTaskSchedule"></a> InvalidTaskSchedule

<code>E_PF_INVALID_TASK_SCHEDULE</code>.

```csharp
public const int InvalidTaskSchedule = -1994173161
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidTicket"></a> InvalidTicket

<code>E_PF_INVALID_TICKET</code>.

```csharp
public const int InvalidTicket = -1994173373
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidTitleForDeveloper"></a> InvalidTitleForDeveloper

<code>E_PF_INVALID_TITLE_FOR_DEVELOPER</code>.

```csharp
public const int InvalidTitleForDeveloper = -1994173379
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidTitleId"></a> InvalidTitleId

<code>E_PF_INVALID_TITLE_ID</code>.

```csharp
public const int InvalidTitleId = -1994173403
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidTokenResultFromAad"></a> InvalidTokenResultFromAad

<code>E_PF_INVALID_TOKEN_RESULT_FROM_AAD</code>.

```csharp
public const int InvalidTokenResultFromAad = -1994173044
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidTwitchToken"></a> InvalidTwitchToken

<code>E_PF_INVALID_TWITCH_TOKEN</code>.

```csharp
public const int InvalidTwitchToken = -1994173186
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidTypeInBody"></a> InvalidTypeInBody

<code>E_PF_INVALID_TYPE_IN_BODY</code>.

```csharp
public const int InvalidTypeInBody = -1994173337
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidUserStatistics"></a> InvalidUserStatistics

<code>E_PF_INVALID_USER_STATISTICS</code>.

```csharp
public const int InvalidUserStatistics = -1994173334
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidUsername"></a> InvalidUsername

<code>E_PF_INVALID_USERNAME</code>.

```csharp
public const int InvalidUsername = -1994173400
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidUsernameOrPassword"></a> InvalidUsernameOrPassword

<code>E_PF_INVALID_USERNAME_OR_PASSWORD</code>.

```csharp
public const int InvalidUsernameOrPassword = -1994173404
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidVersionResetForLinkedLeaderboard"></a> InvalidVersionResetForLinkedLeaderboard

<code>E_PF_INVALID_VERSION_RESET_FOR_LINKED_LEADERBOARD</code>.

```csharp
public const int InvalidVersionResetForLinkedLeaderboard = -1994172513
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidVirtualCurrency"></a> InvalidVirtualCurrency

<code>E_PF_INVALID_VIRTUAL_CURRENCY</code>.

```csharp
public const int InvalidVirtualCurrency = -1994173356
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidVirtualCurrencyCode"></a> InvalidVirtualCurrencyCode

<code>E_PF_INVALID_VIRTUAL_CURRENCY_CODE</code>.

```csharp
public const int InvalidVirtualCurrencyCode = -1994173182
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InvalidXboxLiveToken"></a> InvalidXboxLiveToken

<code>E_PF_INVALID_XBOX_LIVE_TOKEN</code>.

```csharp
public const int InvalidXboxLiveToken = -1994173230
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_Invalidhandle"></a> Invalidhandle

<code>E_PF_INVALIDHANDLE</code>.

```csharp
public const int Invalidhandle = -1994173438
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InventoryApiNotImplemented"></a> InventoryApiNotImplemented

<code>E_PF_INVENTORY_API_NOT_IMPLEMENTED</code>.

```csharp
public const int InventoryApiNotImplemented = -1994172727
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_InventoryCollectionDeletionDisallowed"></a> InventoryCollectionDeletionDisallowed

<code>E_PF_INVENTORY_COLLECTION_DELETION_DISALLOWED</code>.

```csharp
public const int InventoryCollectionDeletionDisallowed = -1994172515
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_IpAddressBanned"></a> IpAddressBanned

<code>E_PF_IP_ADDRESS_BANNED</code>.

```csharp
public const int IpAddressBanned = -1994172865
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ItemNotAffordable"></a> ItemNotAffordable

<code>E_PF_ITEM_NOT_AFFORDABLE</code>.

```csharp
public const int ItemNotAffordable = -1994173357
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ItemNotFound"></a> ItemNotFound

<code>E_PF_ITEM_NOT_FOUND</code>.

```csharp
public const int ItemNotFound = -1994173360
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ItemNotOwned"></a> ItemNotOwned

<code>E_PF_ITEM_NOT_OWNED</code>.

```csharp
public const int ItemNotOwned = -1994173359
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ItemNotRecycleable"></a> ItemNotRecycleable

<code>E_PF_ITEM_NOT_RECYCLEABLE</code>.

```csharp
public const int ItemNotRecycleable = -1994173358
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_JavascriptException"></a> JavascriptException

<code>E_PF_JAVASCRIPT_EXCEPTION</code>.

```csharp
public const int JavascriptException = -1994173310
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_KeyLengthExceeded"></a> KeyLengthExceeded

<code>E_PF_KEY_LENGTH_EXCEEDED</code>.

```csharp
public const int KeyLengthExceeded = -1994173273
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_KeyNotOwned"></a> KeyNotOwned

<code>E_PF_KEY_NOT_OWNED</code>.

```csharp
public const int KeyNotOwned = -1994173388
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardColumnLengthMismatch"></a> LeaderboardColumnLengthMismatch

<code>E_PF_LEADERBOARD_COLUMN_LENGTH_MISMATCH</code>.

```csharp
public const int LeaderboardColumnLengthMismatch = -1994172860
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardColumnLengthMismatchWithStatDefinition"></a> LeaderboardColumnLengthMismatchWithStatDefinition

<code>E_PF_LEADERBOARD_COLUMN_LENGTH_MISMATCH_WITH_STAT_DEFINITION</code>.

```csharp
public const int LeaderboardColumnLengthMismatchWithStatDefinition = -1994172558
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardColumnsNotSpecified"></a> LeaderboardColumnsNotSpecified

<code>E_PF_LEADERBOARD_COLUMNS_NOT_SPECIFIED</code>.

```csharp
public const int LeaderboardColumnsNotSpecified = -1994172662
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardCountLimitExceeded"></a> LeaderboardCountLimitExceeded

<code>E_PF_LEADERBOARD_COUNT_LIMIT_EXCEEDED</code>.

```csharp
public const int LeaderboardCountLimitExceeded = -1994172612
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardDefinitionModificationNotAllowedWhileLinked"></a> LeaderboardDefinitionModificationNotAllowedWhileLinked

<code>E_PF_LEADERBOARD_DEFINITION_MODIFICATION_NOT_ALLOWED_WHILE_LINKED</code>.

```csharp
public const int LeaderboardDefinitionModificationNotAllowedWhileLinked = -1994172610
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardMaxSizeTooLarge"></a> LeaderboardMaxSizeTooLarge

<code>E_PF_LEADERBOARD_MAX_SIZE_TOO_LARGE</code>.

```csharp
public const int LeaderboardMaxSizeTooLarge = -1994172656
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardNameConflict"></a> LeaderboardNameConflict

<code>E_PF_LEADERBOARD_NAME_CONFLICT</code>.

```csharp
public const int LeaderboardNameConflict = -1994172633
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardNotFound"></a> LeaderboardNotFound

<code>E_PF_LEADERBOARD_NOT_FOUND</code>.

```csharp
public const int LeaderboardNotFound = -1994172635
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardSizeLimitExceeded"></a> LeaderboardSizeLimitExceeded

<code>E_PF_LEADERBOARD_SIZE_LIMIT_EXCEEDED</code>.

```csharp
public const int LeaderboardSizeLimitExceeded = -1994172611
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardUpdateNotAllowedWhileLinked"></a> LeaderboardUpdateNotAllowedWhileLinked

<code>E_PF_LEADERBOARD_UPDATE_NOT_ALLOWED_WHILE_LINKED</code>.

```csharp
public const int LeaderboardUpdateNotAllowedWhileLinked = -1994172608
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LeaderboardVersionNotAvailable"></a> LeaderboardVersionNotAvailable

<code>E_PF_LEADERBOARD_VERSION_NOT_AVAILABLE</code>.

```csharp
public const int LeaderboardVersionNotAvailable = -1994173141
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LegacyEconomyDisabled"></a> LegacyEconomyDisabled

<code>E_PF_LEGACY_ECONOMY_DISABLED</code>.

```csharp
public const int LegacyEconomyDisabled = -1994172453
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LegacyMultiplayerServersDeprecated"></a> LegacyMultiplayerServersDeprecated

<code>E_PF_LEGACY_MULTIPLAYER_SERVERS_DEPRECATED</code>.

```csharp
public const int LegacyMultiplayerServersDeprecated = -1994172909
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LimitNotAnUpgradeOption"></a> LimitNotAnUpgradeOption

<code>E_PF_LIMIT_NOT_AN_UPGRADE_OPTION</code>.

```csharp
public const int LimitNotAnUpgradeOption = -1994173159
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LimitNotAvailableViaApi"></a> LimitNotAvailableViaApi

<code>E_PF_LIMIT_NOT_AVAILABLE_VIA_API</code>.

```csharp
public const int LimitNotAvailableViaApi = -1994172922
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LimitNotFound"></a> LimitNotFound

<code>E_PF_LIMIT_NOT_FOUND</code>.

```csharp
public const int LimitNotFound = -1994172923
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LimitedEditionItemUnavailable"></a> LimitedEditionItemUnavailable

<code>E_PF_LIMITED_EDITION_ITEM_UNAVAILABLE</code>.

```csharp
public const int LimitedEditionItemUnavailable = -1994173151
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LinkedAccountAlreadyClaimed"></a> LinkedAccountAlreadyClaimed

<code>E_PF_LINKED_ACCOUNT_ALREADY_CLAIMED</code>.

```csharp
public const int LinkedAccountAlreadyClaimed = -1994173395
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LinkedDeviceAlreadyClaimed"></a> LinkedDeviceAlreadyClaimed

<code>E_PF_LINKED_DEVICE_ALREADY_CLAIMED</code>.

```csharp
public const int LinkedDeviceAlreadyClaimed = -1994173300
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LinkedIdentifierAlreadyClaimed"></a> LinkedIdentifierAlreadyClaimed

<code>E_PF_LINKED_IDENTIFIER_ALREADY_CLAIMED</code>.

```csharp
public const int LinkedIdentifierAlreadyClaimed = -1994173234
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LinkedStatisticColumnMismatch"></a> LinkedStatisticColumnMismatch

<code>E_PF_LINKED_STATISTIC_COLUMN_MISMATCH</code>.

```csharp
public const int LinkedStatisticColumnMismatch = -1994172632
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LinkedStatisticColumnNotFound"></a> LinkedStatisticColumnNotFound

<code>E_PF_LINKED_STATISTIC_COLUMN_NOT_FOUND</code>.

```csharp
public const int LinkedStatisticColumnNotFound = -1994172556
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LinkedStatisticColumnRequired"></a> LinkedStatisticColumnRequired

<code>E_PF_LINKED_STATISTIC_COLUMN_REQUIRED</code>.

```csharp
public const int LinkedStatisticColumnRequired = -1994172555
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LinkingStatsNotAllowedForEntityType"></a> LinkingStatsNotAllowedForEntityType

<code>E_PF_LINKING_STATS_NOT_ALLOWED_FOR_ENTITY_TYPE</code>.

```csharp
public const int LinkingStatsNotAllowedForEntityType = -1994172629
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyBadRequest"></a> LobbyBadRequest

<code>E_PF_LOBBY_BAD_REQUEST</code>.

```csharp
public const int LobbyBadRequest = -1994172719
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyCurrentOwnerStillConnected"></a> LobbyCurrentOwnerStillConnected

<code>E_PF_LOBBY_CURRENT_OWNER_STILL_CONNECTED</code>.

```csharp
public const int LobbyCurrentOwnerStillConnected = -1994172716
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyCurrentPlayersMoreThanMaxPlayers"></a> LobbyCurrentPlayersMoreThanMaxPlayers

<code>E_PF_LOBBY_CURRENT_PLAYERS_MORE_THAN_MAX_PLAYERS</code>.

```csharp
public const int LobbyCurrentPlayersMoreThanMaxPlayers = -1994172721
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyDifferentServerAlreadyJoined"></a> LobbyDifferentServerAlreadyJoined

<code>E_PF_LOBBY_DIFFERENT_SERVER_ALREADY_JOINED</code>.

```csharp
public const int LobbyDifferentServerAlreadyJoined = -1994172659
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyDoesNotExist"></a> LobbyDoesNotExist

<code>E_PF_LOBBY_DOES_NOT_EXIST</code>.

```csharp
public const int LobbyDoesNotExist = -1994172726
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyDoesNotUseConnections"></a> LobbyDoesNotUseConnections

<code>E_PF_LOBBY_DOES_NOT_USE_CONNECTIONS</code>.

```csharp
public const int LobbyDoesNotUseConnections = -1994172709
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyIsNotClientOwned"></a> LobbyIsNotClientOwned

<code>E_PF_LOBBY_IS_NOT_CLIENT_OWNED</code>.

```csharp
public const int LobbyIsNotClientOwned = -1994172710
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyMemberCannotRejoin"></a> LobbyMemberCannotRejoin

<code>E_PF_LOBBY_MEMBER_CANNOT_REJOIN</code>.

```csharp
public const int LobbyMemberCannotRejoin = -1994172722
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyMemberIsNotOwner"></a> LobbyMemberIsNotOwner

<code>E_PF_LOBBY_MEMBER_IS_NOT_OWNER</code>.

```csharp
public const int LobbyMemberIsNotOwner = -1994172715
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyNewOwnerMustBeConnected"></a> LobbyNewOwnerMustBeConnected

<code>E_PF_LOBBY_NEW_OWNER_MUST_BE_CONNECTED</code>.

```csharp
public const int LobbyNewOwnerMustBeConnected = -1994172717
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyNotJoinable"></a> LobbyNotJoinable

<code>E_PF_LOBBY_NOT_JOINABLE</code>.

```csharp
public const int LobbyNotJoinable = -1994172723
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyPlayerAlreadyJoined"></a> LobbyPlayerAlreadyJoined

<code>E_PF_LOBBY_PLAYER_ALREADY_JOINED</code>.

```csharp
public const int LobbyPlayerAlreadyJoined = -1994172724
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyPlayerMaxLobbyLimitExceeded"></a> LobbyPlayerMaxLobbyLimitExceeded

<code>E_PF_LOBBY_PLAYER_MAX_LOBBY_LIMIT_EXCEEDED</code>.

```csharp
public const int LobbyPlayerMaxLobbyLimitExceeded = -1994172718
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyPlayerNotPresent"></a> LobbyPlayerNotPresent

<code>E_PF_LOBBY_PLAYER_NOT_PRESENT</code>.

```csharp
public const int LobbyPlayerNotPresent = -1994172720
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyRateLimitExceeded"></a> LobbyRateLimitExceeded

<code>E_PF_LOBBY_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int LobbyRateLimitExceeded = -1994172725
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyServerAlreadyJoined"></a> LobbyServerAlreadyJoined

<code>E_PF_LOBBY_SERVER_ALREADY_JOINED</code>.

```csharp
public const int LobbyServerAlreadyJoined = -1994172658
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyServerMismatch"></a> LobbyServerMismatch

<code>E_PF_LOBBY_SERVER_MISMATCH</code>.

```csharp
public const int LobbyServerMismatch = -1994172661
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_LobbyServerNotFound"></a> LobbyServerNotFound

<code>E_PF_LOBBY_SERVER_NOT_FOUND</code>.

```csharp
public const int LobbyServerNotFound = -1994172660
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ManageEventNameInvalid"></a> ManageEventNameInvalid

<code>E_PF_MANAGE_EVENT_NAME_INVALID</code>.

```csharp
public const int ManageEventNameInvalid = -1994172678
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ManageEventNamespaceInvalid"></a> ManageEventNamespaceInvalid

<code>E_PF_MANAGE_EVENT_NAMESPACE_INVALID</code>.

```csharp
public const int ManageEventNamespaceInvalid = -1994172679
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ManageEventsInvalidRatio"></a> ManageEventsInvalidRatio

<code>E_PF_MANAGE_EVENTS_INVALID_RATIO</code>.

```csharp
public const int ManageEventsInvalidRatio = -1994172676
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ManagedEventInvalid"></a> ManagedEventInvalid

<code>E_PF_MANAGED_EVENT_INVALID</code>.

```csharp
public const int ManagedEventInvalid = -1994172675
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ManagedEventNotFound"></a> ManagedEventNotFound

<code>E_PF_MANAGED_EVENT_NOT_FOUND</code>.

```csharp
public const int ManagedEventNotFound = -1994172677
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingAlreadyJoinedTicket"></a> MatchmakingAlreadyJoinedTicket

<code>E_PF_MATCHMAKING_ALREADY_JOINED_TICKET</code>.

```csharp
public const int MatchmakingAlreadyJoinedTicket = -1994172853
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingAttributeInvalid"></a> MatchmakingAttributeInvalid

<code>E_PF_MATCHMAKING_ATTRIBUTE_INVALID</code>.

```csharp
public const int MatchmakingAttributeInvalid = -1994172845
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingBadRequest"></a> MatchmakingBadRequest

<code>E_PF_MATCHMAKING_BAD_REQUEST</code>.

```csharp
public const int MatchmakingBadRequest = -1994172838
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingEntityInvalid"></a> MatchmakingEntityInvalid

<code>E_PF_MATCHMAKING_ENTITY_INVALID</code>.

```csharp
public const int MatchmakingEntityInvalid = -1994172858
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingMatchNotFound"></a> MatchmakingMatchNotFound

<code>E_PF_MATCHMAKING_MATCH_NOT_FOUND</code>.

```csharp
public const int MatchmakingMatchNotFound = -1994172855
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingMemberProfileInvalid"></a> MatchmakingMemberProfileInvalid

<code>E_PF_MATCHMAKING_MEMBER_PROFILE_INVALID</code>.

```csharp
public const int MatchmakingMemberProfileInvalid = -1994172850
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingNotEnabled"></a> MatchmakingNotEnabled

<code>E_PF_MATCHMAKING_NOT_ENABLED</code>.

```csharp
public const int MatchmakingNotEnabled = -1994172848
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingNumberOfPlayersInTicketTooLarge"></a> MatchmakingNumberOfPlayersInTicketTooLarge

<code>E_PF_MATCHMAKING_NUMBER_OF_PLAYERS_IN_TICKET_TOO_LARGE</code>.

```csharp
public const int MatchmakingNumberOfPlayersInTicketTooLarge = -1994172846
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingPlayerAttributesInvalid"></a> MatchmakingPlayerAttributesInvalid

<code>E_PF_MATCHMAKING_PLAYER_ATTRIBUTES_INVALID</code>.

```csharp
public const int MatchmakingPlayerAttributesInvalid = -1994172857
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingPlayerAttributesTooLarge"></a> MatchmakingPlayerAttributesTooLarge

<code>E_PF_MATCHMAKING_PLAYER_ATTRIBUTES_TOO_LARGE</code>.

```csharp
public const int MatchmakingPlayerAttributesTooLarge = -1994172847
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingPlayerHasNotJoinedTicket"></a> MatchmakingPlayerHasNotJoinedTicket

<code>E_PF_MATCHMAKING_PLAYER_HAS_NOT_JOINED_TICKET</code>.

```csharp
public const int MatchmakingPlayerHasNotJoinedTicket = -1994172844
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingQueueConfigInvalid"></a> MatchmakingQueueConfigInvalid

<code>E_PF_MATCHMAKING_QUEUE_CONFIG_INVALID</code>.

```csharp
public const int MatchmakingQueueConfigInvalid = -1994172851
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingQueueLimitExceeded"></a> MatchmakingQueueLimitExceeded

<code>E_PF_MATCHMAKING_QUEUE_LIMIT_EXCEEDED</code>.

```csharp
public const int MatchmakingQueueLimitExceeded = -1994172840
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingQueueNotFound"></a> MatchmakingQueueNotFound

<code>E_PF_MATCHMAKING_QUEUE_NOT_FOUND</code>.

```csharp
public const int MatchmakingQueueNotFound = -1994172856
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingRateLimitExceeded"></a> MatchmakingRateLimitExceeded

<code>E_PF_MATCHMAKING_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int MatchmakingRateLimitExceeded = -1994172843
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingRequestTypeMismatch"></a> MatchmakingRequestTypeMismatch

<code>E_PF_MATCHMAKING_REQUEST_TYPE_MISMATCH</code>.

```csharp
public const int MatchmakingRequestTypeMismatch = -1994172839
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingTicketAlreadyCompleted"></a> MatchmakingTicketAlreadyCompleted

<code>E_PF_MATCHMAKING_TICKET_ALREADY_COMPLETED</code>.

```csharp
public const int MatchmakingTicketAlreadyCompleted = -1994172852
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingTicketMembershipLimitExceeded"></a> MatchmakingTicketMembershipLimitExceeded

<code>E_PF_MATCHMAKING_TICKET_MEMBERSHIP_LIMIT_EXCEEDED</code>.

```csharp
public const int MatchmakingTicketMembershipLimitExceeded = -1994172842
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingTicketNotFound"></a> MatchmakingTicketNotFound

<code>E_PF_MATCHMAKING_TICKET_NOT_FOUND</code>.

```csharp
public const int MatchmakingTicketNotFound = -1994172854
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MatchmakingUnauthorized"></a> MatchmakingUnauthorized

<code>E_PF_MATCHMAKING_UNAUTHORIZED</code>.

```csharp
public const int MatchmakingUnauthorized = -1994172841
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MaxActionDepthExceeded"></a> MaxActionDepthExceeded

<code>E_PF_MAX_ACTION_DEPTH_EXCEEDED</code>.

```csharp
public const int MaxActionDepthExceeded = -1994172745
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MaxQueryableVersionsExceeded"></a> MaxQueryableVersionsExceeded

<code>E_PF_MAX_QUERYABLE_VERSIONS_EXCEEDED</code>.

```csharp
public const int MaxQueryableVersionsExceeded = -1994172451
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MaxQueryableVersionsValueNotAllowedForTier"></a> MaxQueryableVersionsValueNotAllowedForTier

<code>E_PF_MAX_QUERYABLE_VERSIONS_VALUE_NOT_ALLOWED_FOR_TIER</code>.

```csharp
public const int MaxQueryableVersionsValueNotAllowedForTier = -1994172532
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MaximumSegmentBulkActionJobsRunning"></a> MaximumSegmentBulkActionJobsRunning

<code>E_PF_MAXIMUM_SEGMENT_BULK_ACTION_JOBS_RUNNING</code>.

```csharp
public const int MaximumSegmentBulkActionJobsRunning = -1994173167
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MembershipDefinitionInUse"></a> MembershipDefinitionInUse

<code>E_PF_MEMBERSHIP_DEFINITION_IN_USE</code>.

```csharp
public const int MembershipDefinitionInUse = -1994173065
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MembershipNameTooLong"></a> MembershipNameTooLong

<code>E_PF_MEMBERSHIP_NAME_TOO_LONG</code>.

```csharp
public const int MembershipNameTooLong = -1994173089
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MembershipNotFound"></a> MembershipNotFound

<code>E_PF_MEMBERSHIP_NOT_FOUND</code>.

```csharp
public const int MembershipNotFound = -1994173088
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MetadataLengthExceeded"></a> MetadataLengthExceeded

<code>E_PF_METADATA_LENGTH_EXCEEDED</code>.

```csharp
public const int MetadataLengthExceeded = -1994172454
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MisconfiguredIdentityProvider"></a> MisconfiguredIdentityProvider

<code>E_PF_MISCONFIGURED_IDENTITY_PROVIDER</code>.

```csharp
public const int MisconfiguredIdentityProvider = -1994173154
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MissingAmazonSharedKey"></a> MissingAmazonSharedKey

<code>E_PF_MISSING_AMAZON_SHARED_KEY</code>.

```csharp
public const int MissingAmazonSharedKey = -1994173269
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MissingLocalizedPushNotificationMessage"></a> MissingLocalizedPushNotificationMessage

<code>E_PF_MISSING_LOCALIZED_PUSH_NOTIFICATION_MESSAGE</code>.

```csharp
public const int MissingLocalizedPushNotificationMessage = -1994173010
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MissingTitleGoogleProperties"></a> MissingTitleGoogleProperties

<code>E_PF_MISSING_TITLE_GOOGLE_PROPERTIES</code>.

```csharp
public const int MissingTitleGoogleProperties = -1994173319
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiLevelAggregationNotAllowed"></a> MultiLevelAggregationNotAllowed

<code>E_PF_MULTI_LEVEL_AGGREGATION_NOT_ALLOWED</code>.

```csharp
public const int MultiLevelAggregationNotAllowed = -1994172477
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerBadRequest"></a> MultiplayerServerBadRequest

<code>E_PF_MULTIPLAYER_SERVER_BAD_REQUEST</code>.

```csharp
public const int MultiplayerServerBadRequest = -1994173037
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerBuildAliasReferencedByMatchmakingQueue"></a> MultiplayerServerBuildAliasReferencedByMatchmakingQueue

<code>E_PF_MULTIPLAYER_SERVER_BUILD_ALIAS_REFERENCED_BY_MATCHMAKING_QUEUE</code>.

```csharp
public const int MultiplayerServerBuildAliasReferencedByMatchmakingQueue = -1994172770
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerBuildReferencedByBuildAlias"></a> MultiplayerServerBuildReferencedByBuildAlias

<code>E_PF_MULTIPLAYER_SERVER_BUILD_REFERENCED_BY_BUILD_ALIAS</code>.

```csharp
public const int MultiplayerServerBuildReferencedByBuildAlias = -1994172771
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerBuildReferencedByMatchmakingQueue"></a> MultiplayerServerBuildReferencedByMatchmakingQueue

<code>E_PF_MULTIPLAYER_SERVER_BUILD_REFERENCED_BY_MATCHMAKING_QUEUE</code>.

```csharp
public const int MultiplayerServerBuildReferencedByMatchmakingQueue = -1994172772
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerConflict"></a> MultiplayerServerConflict

<code>E_PF_MULTIPLAYER_SERVER_CONFLICT</code>.

```csharp
public const int MultiplayerServerConflict = -1994173033
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerError"></a> MultiplayerServerError

<code>E_PF_MULTIPLAYER_SERVER_ERROR</code>.

```csharp
public const int MultiplayerServerError = -1994173040
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerForbidden"></a> MultiplayerServerForbidden

<code>E_PF_MULTIPLAYER_SERVER_FORBIDDEN</code>.

```csharp
public const int MultiplayerServerForbidden = -1994173035
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerInternalServerError"></a> MultiplayerServerInternalServerError

<code>E_PF_MULTIPLAYER_SERVER_INTERNAL_SERVER_ERROR</code>.

```csharp
public const int MultiplayerServerInternalServerError = -1994173032
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerNoContent"></a> MultiplayerServerNoContent

<code>E_PF_MULTIPLAYER_SERVER_NO_CONTENT</code>.

```csharp
public const int MultiplayerServerNoContent = -1994173038
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerNotFound"></a> MultiplayerServerNotFound

<code>E_PF_MULTIPLAYER_SERVER_NOT_FOUND</code>.

```csharp
public const int MultiplayerServerNotFound = -1994173034
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerTitleQuotaCoresExceeded"></a> MultiplayerServerTitleQuotaCoresExceeded

<code>E_PF_MULTIPLAYER_SERVER_TITLE_QUOTA_CORES_EXCEEDED</code>.

```csharp
public const int MultiplayerServerTitleQuotaCoresExceeded = -1994172975
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerTooManyRequests"></a> MultiplayerServerTooManyRequests

<code>E_PF_MULTIPLAYER_SERVER_TOO_MANY_REQUESTS</code>.

```csharp
public const int MultiplayerServerTooManyRequests = -1994173039
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerUnauthorized"></a> MultiplayerServerUnauthorized

<code>E_PF_MULTIPLAYER_SERVER_UNAUTHORIZED</code>.

```csharp
public const int MultiplayerServerUnauthorized = -1994173036
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultiplayerServerUnavailable"></a> MultiplayerServerUnavailable

<code>E_PF_MULTIPLAYER_SERVER_UNAVAILABLE</code>.

```csharp
public const int MultiplayerServerUnavailable = -1994173031
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_MultipleLinkedStatisticsNotAllowed"></a> MultipleLinkedStatisticsNotAllowed

<code>E_PF_MULTIPLE_LINKED_STATISTICS_NOT_ALLOWED</code>.

```csharp
public const int MultipleLinkedStatisticsNotAllowed = -1994172554
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NameNotAvailable"></a> NameNotAvailable

<code>E_PF_NAME_NOT_AVAILABLE</code>.

```csharp
public const int NameNotAvailable = -1994173349
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NamespaceMismatch"></a> NamespaceMismatch

<code>E_PF_NAMESPACE_MISMATCH</code>.

```csharp
public const int NamespaceMismatch = -1994172863
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NintendoSwitchDeviceIdNotLinked"></a> NintendoSwitchDeviceIdNotLinked

<code>E_PF_NINTENDO_SWITCH_DEVICE_ID_NOT_LINKED</code>.

```csharp
public const int NintendoSwitchDeviceIdNotLinked = -1994172849
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NintendoSwitchNotEnabledForTitle"></a> NintendoSwitchNotEnabledForTitle

<code>E_PF_NINTENDO_SWITCH_NOT_ENABLED_FOR_TITLE</code>.

```csharp
public const int NintendoSwitchNotEnabledForTitle = -1994172914
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoActionsOnPlayersInSegmentJob"></a> NoActionsOnPlayersInSegmentJob

<code>E_PF_NO_ACTIONS_ON_PLAYERS_IN_SEGMENT_JOB</code>.

```csharp
public const int NoActionsOnPlayersInSegmentJob = -1994173166
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoContactEmailAddressFound"></a> NoContactEmailAddressFound

<code>E_PF_NO_CONTACT_EMAIL_ADDRESS_FOUND</code>.

```csharp
public const int NoContactEmailAddressFound = -1994173094
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoEntityFileOperationPending"></a> NoEntityFileOperationPending

<code>E_PF_NO_ENTITY_FILE_OPERATION_PENDING</code>.

```csharp
public const int NoEntityFileOperationPending = -1994173068
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoGameModeParamsSet"></a> NoGameModeParamsSet

<code>E_PF_NO_GAME_MODE_PARAMS_SET</code>.

```csharp
public const int NoGameModeParamsSet = -1994173340
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoLeaderboardForStatistic"></a> NoLeaderboardForStatistic

<code>E_PF_NO_LEADERBOARD_FOR_STATISTIC</code>.

```csharp
public const int NoLeaderboardForStatistic = -1994172999
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoLinkedStatisticToLeaderboard"></a> NoLinkedStatisticToLeaderboard

<code>E_PF_NO_LINKED_STATISTIC_TO_LEADERBOARD</code>.

```csharp
public const int NoLinkedStatisticToLeaderboard = -1994172631
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoMatchingCatalogItemForReceipt"></a> NoMatchingCatalogItemForReceipt

<code>E_PF_NO_MATCHING_CATALOG_ITEM_FOR_RECEIPT</code>.

```csharp
public const int NoMatchingCatalogItemForReceipt = -1994173240
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoPartnerEnabled"></a> NoPartnerEnabled

<code>E_PF_NO_PARTNER_ENABLED</code>.

```csharp
public const int NoPartnerEnabled = -1994173226
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoPushNotificationArnForTitle"></a> NoPushNotificationArnForTitle

<code>E_PF_NO_PUSH_NOTIFICATION_ARN_FOR_TITLE</code>.

```csharp
public const int NoPushNotificationArnForTitle = -1994173325
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoRealMoneyPriceForCatalogItem"></a> NoRealMoneyPriceForCatalogItem

<code>E_PF_NO_REAL_MONEY_PRICE_FOR_CATALOG_ITEM</code>.

```csharp
public const int NoRealMoneyPriceForCatalogItem = -1994173238
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoRemainingUses"></a> NoRemainingUses

<code>E_PF_NO_REMAINING_USES</code>.

```csharp
public const int NoRemainingUses = -1994173345
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoSecretKeyEnabledForCloudScript"></a> NoSecretKeyEnabledForCloudScript

<code>E_PF_NO_SECRET_KEY_ENABLED_FOR_CLOUD_SCRIPT</code>.

```csharp
public const int NoSecretKeyEnabledForCloudScript = -1994173158
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoSharedSecretKeyConfigured"></a> NoSharedSecretKeyConfigured

<code>E_PF_NO_SHARED_SECRET_KEY_CONFIGURED</code>.

```csharp
public const int NoSharedSecretKeyConfigured = -1994173127
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoSuchMod"></a> NoSuchMod

<code>E_PF_NO_SUCH_MOD</code>.

```csharp
public const int NoSuchMod = -1994173363
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoValidCertificateForAad"></a> NoValidCertificateForAad

<code>E_PF_NO_VALID_CERTIFICATE_FOR_AAD</code>.

```csharp
public const int NoValidCertificateForAad = -1994173043
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoValidIdentityForAad"></a> NoValidIdentityForAad

<code>E_PF_NO_VALID_IDENTITY_FOR_AAD</code>.

```csharp
public const int NoValidIdentityForAad = -1994172885
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NoWritePermissionsForEvent"></a> NoWritePermissionsForEvent

<code>E_PF_NO_WRITE_PERMISSIONS_FOR_EVENT</code>.

```csharp
public const int NoWritePermissionsForEvent = -1994173211
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_Noentitytoken"></a> Noentitytoken

<code>E_PF_NOENTITYTOKEN</code>.

```csharp
public const int Noentitytoken = -1994173423
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NonPositiveValue"></a> NonPositiveValue

<code>E_PF_NON_POSITIVE_VALUE</code>.

```csharp
public const int NonPositiveValue = -1994173353
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_Nosecretkey"></a> Nosecretkey

<code>E_PF_NOSECRETKEY</code>.

```csharp
public const int Nosecretkey = -1994173422
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NotAuthenticated"></a> NotAuthenticated

<code>E_PF_NOT_AUTHENTICATED</code>.

```csharp
public const int NotAuthenticated = -1994173333
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NotAuthorized"></a> NotAuthorized

<code>E_PF_NOT_AUTHORIZED</code>.

```csharp
public const int NotAuthorized = -1994173320
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NotAuthorizedByTitle"></a> NotAuthorizedByTitle

<code>E_PF_NOT_AUTHORIZED_BY_TITLE</code>.

```csharp
public const int NotAuthorizedByTitle = -1994173227
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NotImplemented"></a> NotImplemented

<code>E_PF_NOT_IMPLEMENTED</code>.

```csharp
public const int NotImplemented = -1994172905
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_NullTokenResultFromAad"></a> NullTokenResultFromAad

<code>E_PF_NULL_TOKEN_RESULT_FROM_AAD</code>.

```csharp
public const int NullTokenResultFromAad = -1994173045
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_OperationCanceled"></a> OperationCanceled

<code>E_PF_OPERATION_CANCELED</code>.

```csharp
public const int OperationCanceled = -1994172684
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_OperationDeniedDueToDefinitionPolicy"></a> OperationDeniedDueToDefinitionPolicy

<code>E_PF_OPERATION_DENIED_DUE_TO_DEFINITION_POLICY</code>.

```csharp
public const int OperationDeniedDueToDefinitionPolicy = -1994172466
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_OperationNotSupportedForPlatform"></a> OperationNotSupportedForPlatform

<code>E_PF_OPERATION_NOT_SUPPORTED_FOR_PLATFORM</code>.

```csharp
public const int OperationNotSupportedForPlatform = -1994173199
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_OutstandingApplicationAcceptedInstead"></a> OutstandingApplicationAcceptedInstead

<code>E_PF_OUTSTANDING_APPLICATION_ACCEPTED_INSTEAD</code>.

```csharp
public const int OutstandingApplicationAcceptedInstead = -1994173055
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_OutstandingInvitationAcceptedInstead"></a> OutstandingInvitationAcceptedInstead

<code>E_PF_OUTSTANDING_INVITATION_ACCEPTED_INSTEAD</code>.

```csharp
public const int OutstandingInvitationAcceptedInstead = -1994173056
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_OverLimit"></a> OverLimit

<code>E_PF_OVER_LIMIT</code>.

```csharp
public const int OverLimit = -1994173204
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PaidInsightsFeaturesNotEnabled"></a> PaidInsightsFeaturesNotEnabled

<code>E_PF_PAID_INSIGHTS_FEATURES_NOT_ENABLED</code>.

```csharp
public const int PaidInsightsFeaturesNotEnabled = -1994172927
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ParentCustomerAccountNotFound"></a> ParentCustomerAccountNotFound

<code>E_PF_PARENT_CUSTOMER_ACCOUNT_NOT_FOUND</code>.

```csharp
public const int ParentCustomerAccountNotFound = -1994172459
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartialFailure"></a> PartialFailure

<code>E_PF_PARTIAL_FAILURE</code>.

```csharp
public const int PartialFailure = -1994173297
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartitionedEventCountOverLimit"></a> PartitionedEventCountOverLimit

<code>E_PF_PARTITIONED_EVENT_COUNT_OVER_LIMIT</code>.

```csharp
public const int PartitionedEventCountOverLimit = -1994172680
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartitionedEventInvalid"></a> PartitionedEventInvalid

<code>E_PF_PARTITIONED_EVENT_INVALID</code>.

```csharp
public const int PartitionedEventInvalid = -1994172681
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyBadRequest"></a> PartyBadRequest

<code>E_PF_PARTY_BAD_REQUEST</code>.

```csharp
public const int PartyBadRequest = -1994172986
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyConflict"></a> PartyConflict

<code>E_PF_PARTY_CONFLICT</code>.

```csharp
public const int PartyConflict = -1994172982
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyError"></a> PartyError

<code>E_PF_PARTY_ERROR</code>.

```csharp
public const int PartyError = -1994172989
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyForbidden"></a> PartyForbidden

<code>E_PF_PARTY_FORBIDDEN</code>.

```csharp
public const int PartyForbidden = -1994172984
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyInternalServerError"></a> PartyInternalServerError

<code>E_PF_PARTY_INTERNAL_SERVER_ERROR</code>.

```csharp
public const int PartyInternalServerError = -1994172981
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyNoContent"></a> PartyNoContent

<code>E_PF_PARTY_NO_CONTENT</code>.

```csharp
public const int PartyNoContent = -1994172987
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyNotFound"></a> PartyNotFound

<code>E_PF_PARTY_NOT_FOUND</code>.

```csharp
public const int PartyNotFound = -1994172983
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyRequests"></a> PartyRequests

<code>E_PF_PARTY_REQUESTS</code>.

```csharp
public const int PartyRequests = -1994172988
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyRequestsThrottledFromRateLimiter"></a> PartyRequestsThrottledFromRateLimiter

<code>E_PF_PARTY_REQUESTS_THROTTLED_FROM_RATE_LIMITER</code>.

```csharp
public const int PartyRequestsThrottledFromRateLimiter = -1994172916
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartySerializationError"></a> PartySerializationError

<code>E_PF_PARTY_SERIALIZATION_ERROR</code>.

```csharp
public const int PartySerializationError = -1994172769
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyTooManyRequests"></a> PartyTooManyRequests

<code>E_PF_PARTY_TOO_MANY_REQUESTS</code>.

```csharp
public const int PartyTooManyRequests = -1994172979
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyUnauthorized"></a> PartyUnauthorized

<code>E_PF_PARTY_UNAUTHORIZED</code>.

```csharp
public const int PartyUnauthorized = -1994172985
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyUnavailable"></a> PartyUnavailable

<code>E_PF_PARTY_UNAVAILABLE</code>.

```csharp
public const int PartyUnavailable = -1994172980
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PartyVersionNotFound"></a> PartyVersionNotFound

<code>E_PF_PARTY_VERSION_NOT_FOUND</code>.

```csharp
public const int PartyVersionNotFound = -1994172773
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PaymentPageNotConfigured"></a> PaymentPageNotConfigured

<code>E_PF_PAYMENT_PAGE_NOT_CONFIGURED</code>.

```csharp
public const int PaymentPageNotConfigured = -1994173064
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PerEntityEventRateLimitExceeded"></a> PerEntityEventRateLimitExceeded

<code>E_PF_PER_ENTITY_EVENT_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int PerEntityEventRateLimitExceeded = -1994173027
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PhotonApplicationIdAlreadyInUse"></a> PhotonApplicationIdAlreadyInUse

<code>E_PF_PHOTON_APPLICATION_ID_ALREADY_IN_USE</code>.

```csharp
public const int PhotonApplicationIdAlreadyInUse = -1994172883
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PhotonApplicationNotAssociatedWithTitle"></a> PhotonApplicationNotAssociatedWithTitle

<code>E_PF_PHOTON_APPLICATION_NOT_ASSOCIATED_WITH_TITLE</code>.

```csharp
public const int PhotonApplicationNotAssociatedWithTitle = -1994173277
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PhotonApplicationNotFound"></a> PhotonApplicationNotFound

<code>E_PF_PHOTON_APPLICATION_NOT_FOUND</code>.

```csharp
public const int PhotonApplicationNotFound = -1994173278
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PhotonNotEnabledForTitle"></a> PhotonNotEnabledForTitle

<code>E_PF_PHOTON_NOT_ENABLED_FOR_TITLE</code>.

```csharp
public const int PhotonNotEnabledForTitle = -1994173279
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PiiContentDetected"></a> PiiContentDetected

<code>E_PF_PII_CONTENT_DETECTED</code>.

```csharp
public const int PiiContentDetected = -1994173029
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayFabErrorEventNotSupportedForEntityType"></a> PlayFabErrorEventNotSupportedForEntityType

<code>E_PF_PLAY_FAB_ERROR_EVENT_NOT_SUPPORTED_FOR_ENTITY_TYPE</code>.

```csharp
public const int PlayFabErrorEventNotSupportedForEntityType = -1994172462
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayStreamConnectionFailed"></a> PlayStreamConnectionFailed

<code>E_PF_PLAY_STREAM_CONNECTION_FAILED</code>.

```csharp
public const int PlayStreamConnectionFailed = -1994172875
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerAccountPoolDeleted"></a> PlayerAccountPoolDeleted

<code>E_PF_PLAYER_ACCOUNT_POOL_DELETED</code>.

```csharp
public const int PlayerAccountPoolDeleted = -1994172894
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerAccountPoolNotFound"></a> PlayerAccountPoolNotFound

<code>E_PF_PLAYER_ACCOUNT_POOL_NOT_FOUND</code>.

```csharp
public const int PlayerAccountPoolNotFound = -1994172895
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerCreationDisabled"></a> PlayerCreationDisabled

<code>E_PF_PLAYER_CREATION_DISABLED</code>.

```csharp
public const int PlayerCreationDisabled = -1994172486
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerCustomPropertiesDuplicatePropertyName"></a> PlayerCustomPropertiesDuplicatePropertyName

<code>E_PF_PLAYER_CUSTOM_PROPERTIES_DUPLICATE_PROPERTY_NAME</code>.

```csharp
public const int PlayerCustomPropertiesDuplicatePropertyName = -1994172668
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerCustomPropertiesPropertyCountTooHigh"></a> PlayerCustomPropertiesPropertyCountTooHigh

<code>E_PF_PLAYER_CUSTOM_PROPERTIES_PROPERTY_COUNT_TOO_HIGH</code>.

```csharp
public const int PlayerCustomPropertiesPropertyCountTooHigh = -1994172669
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerCustomPropertiesPropertyDoesNotExist"></a> PlayerCustomPropertiesPropertyDoesNotExist

<code>E_PF_PLAYER_CUSTOM_PROPERTIES_PROPERTY_DOES_NOT_EXIST</code>.

```csharp
public const int PlayerCustomPropertiesPropertyDoesNotExist = -1994172667
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerCustomPropertiesPropertyNameIsInvalid"></a> PlayerCustomPropertiesPropertyNameIsInvalid

<code>E_PF_PLAYER_CUSTOM_PROPERTIES_PROPERTY_NAME_IS_INVALID</code>.

```csharp
public const int PlayerCustomPropertiesPropertyNameIsInvalid = -1994172673
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerCustomPropertiesPropertyNameTooLong"></a> PlayerCustomPropertiesPropertyNameTooLong

<code>E_PF_PLAYER_CUSTOM_PROPERTIES_PROPERTY_NAME_TOO_LONG</code>.

```csharp
public const int PlayerCustomPropertiesPropertyNameTooLong = -1994172674
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerCustomPropertiesStringPropertyValueTooLong"></a> PlayerCustomPropertiesStringPropertyValueTooLong

<code>E_PF_PLAYER_CUSTOM_PROPERTIES_STRING_PROPERTY_VALUE_TOO_LONG</code>.

```csharp
public const int PlayerCustomPropertiesStringPropertyValueTooLong = -1994172672
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerCustomPropertiesValueIsInvalidType"></a> PlayerCustomPropertiesValueIsInvalidType

<code>E_PF_PLAYER_CUSTOM_PROPERTIES_VALUE_IS_INVALID_TYPE</code>.

```csharp
public const int PlayerCustomPropertiesValueIsInvalidType = -1994172671
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerCustomPropertiesVersionMismatch"></a> PlayerCustomPropertiesVersionMismatch

<code>E_PF_PLAYER_CUSTOM_PROPERTIES_VERSION_MISMATCH</code>.

```csharp
public const int PlayerCustomPropertiesVersionMismatch = -1994172670
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerIdentityLinkNotFound"></a> PlayerIdentityLinkNotFound

<code>E_PF_PLAYER_IDENTITY_LINK_NOT_FOUND</code>.

```csharp
public const int PlayerIdentityLinkNotFound = -1994172884
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerNotInGame"></a> PlayerNotInGame

<code>E_PF_PLAYER_NOT_IN_GAME</code>.

```csharp
public const int PlayerNotInGame = -1994173374
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerSecretAlreadyConfigured"></a> PlayerSecretAlreadyConfigured

<code>E_PF_PLAYER_SECRET_ALREADY_CONFIGURED</code>.

```csharp
public const int PlayerSecretAlreadyConfigured = -1994173125
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerSecretNotConfigured"></a> PlayerSecretNotConfigured

<code>E_PF_PLAYER_SECRET_NOT_CONFIGURED</code>.

```csharp
public const int PlayerSecretNotConfigured = -1994173096
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PlayerTagCountLimitExceeded"></a> PlayerTagCountLimitExceeded

<code>E_PF_PLAYER_TAG_COUNT_LIMIT_EXCEEDED</code>.

```csharp
public const int PlayerTagCountLimitExceeded = -1994173170
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PreconditionFailed"></a> PreconditionFailed

<code>E_PF_PRECONDITION_FAILED</code>.

```csharp
public const int PreconditionFailed = -1994172468
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PrizeTableHasMissingRanks"></a> PrizeTableHasMissingRanks

<code>E_PF_PRIZE_TABLE_HAS_MISSING_RANKS</code>.

```csharp
public const int PrizeTableHasMissingRanks = -1994173138
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PrizeTableHasNoRanks"></a> PrizeTableHasNoRanks

<code>E_PF_PRIZE_TABLE_HAS_NO_RANKS</code>.

```csharp
public const int PrizeTableHasNoRanks = -1994173122
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PrizeTableHasOverlappingRanks"></a> PrizeTableHasOverlappingRanks

<code>E_PF_PRIZE_TABLE_HAS_OVERLAPPING_RANKS</code>.

```csharp
public const int PrizeTableHasOverlappingRanks = -1994173139
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PrizeTableRankStartsAtZero"></a> PrizeTableRankStartsAtZero

<code>E_PF_PRIZE_TABLE_RANK_STARTS_AT_ZERO</code>.

```csharp
public const int PrizeTableRankStartsAtZero = -1994173137
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ProductDisabledForTitle"></a> ProductDisabledForTitle

<code>E_PF_PRODUCT_DISABLED_FOR_TITLE</code>.

```csharp
public const int ProductDisabledForTitle = -1994172469
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ProfaneDisplayName"></a> ProfaneDisplayName

<code>E_PF_PROFANE_DISPLAY_NAME</code>.

```csharp
public const int ProfaneDisplayName = -1994173184
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ProfileDoesNotExist"></a> ProfileDoesNotExist

<code>E_PF_PROFILE_DOES_NOT_EXIST</code>.

```csharp
public const int ProfileDoesNotExist = -1994173121
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PsnInaccessible"></a> PsnInaccessible

<code>E_PF_PSN_INACCESSIBLE</code>.

```csharp
public const int PsnInaccessible = -1994173266
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PubSubConnectionHandleInvalid"></a> PubSubConnectionHandleInvalid

<code>E_PF_PUB_SUB_CONNECTION_HANDLE_INVALID</code>.

```csharp
public const int PubSubConnectionHandleInvalid = -1994172834
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PubSubConnectionNotFoundForEntity"></a> PubSubConnectionNotFoundForEntity

<code>E_PF_PUB_SUB_CONNECTION_NOT_FOUND_FOR_ENTITY</code>.

```csharp
public const int PubSubConnectionNotFoundForEntity = -1994172835
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PubSubFeatureNotEnabledForTitle"></a> PubSubFeatureNotEnabledForTitle

<code>E_PF_PUB_SUB_FEATURE_NOT_ENABLED_FOR_TITLE</code>.

```csharp
public const int PubSubFeatureNotEnabledForTitle = -1994172837
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PubSubSubscriptionLimitExceeded"></a> PubSubSubscriptionLimitExceeded

<code>E_PF_PUB_SUB_SUBSCRIPTION_LIMIT_EXCEEDED</code>.

```csharp
public const int PubSubSubscriptionLimitExceeded = -1994172833
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PubSubTooManyRequests"></a> PubSubTooManyRequests

<code>E_PF_PUB_SUB_TOO_MANY_REQUESTS</code>.

```csharp
public const int PubSubTooManyRequests = -1994172836
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PublisherDeleted"></a> PublisherDeleted

<code>E_PF_PUBLISHER_DELETED</code>.

```csharp
public const int PublisherDeleted = -1994172903
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PublisherNotFound"></a> PublisherNotFound

<code>E_PF_PUBLISHER_NOT_FOUND</code>.

```csharp
public const int PublisherNotFound = -1994172904
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PublisherNotSet"></a> PublisherNotSet

<code>E_PF_PUBLISHER_NOT_SET</code>.

```csharp
public const int PublisherNotSet = -1994173296
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PurchaseDoesNotExist"></a> PurchaseDoesNotExist

<code>E_PF_PURCHASE_DOES_NOT_EXIST</code>.

```csharp
public const int PurchaseDoesNotExist = -1994173328
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PurchaseInitializationFailure"></a> PurchaseInitializationFailure

<code>E_PF_PURCHASE_INITIALIZATION_FAILURE</code>.

```csharp
public const int PurchaseInitializationFailure = -1994173343
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotEnabledForAccount"></a> PushNotEnabledForAccount

<code>E_PF_PUSH_NOT_ENABLED_FOR_ACCOUNT</code>.

```csharp
public const int PushNotEnabledForAccount = -1994173315
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateAndroidPayloadMissingNotificationBody"></a> PushNotificationTemplateAndroidPayloadMissingNotificationBody

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_ANDROID_PAYLOAD_MISSING_NOTIFICATION_BODY</code>.

```csharp
public const int PushNotificationTemplateAndroidPayloadMissingNotificationBody = -1994173004
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateContainsInvalidAndroidPayload"></a> PushNotificationTemplateContainsInvalidAndroidPayload

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_CONTAINS_INVALID_ANDROID_PAYLOAD</code>.

```csharp
public const int PushNotificationTemplateContainsInvalidAndroidPayload = -1994173006
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateContainsInvalidIosPayload"></a> PushNotificationTemplateContainsInvalidIosPayload

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_CONTAINS_INVALID_IOS_PAYLOAD</code>.

```csharp
public const int PushNotificationTemplateContainsInvalidIosPayload = -1994173007
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateInvalidPayload"></a> PushNotificationTemplateInvalidPayload

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_INVALID_PAYLOAD</code>.

```csharp
public const int PushNotificationTemplateInvalidPayload = -1994173012
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateInvalidSyntax"></a> PushNotificationTemplateInvalidSyntax

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_INVALID_SYNTAX</code>.

```csharp
public const int PushNotificationTemplateInvalidSyntax = -1994173001
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateIosPayloadMissingNotificationBody"></a> PushNotificationTemplateIosPayloadMissingNotificationBody

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_IOS_PAYLOAD_MISSING_NOTIFICATION_BODY</code>.

```csharp
public const int PushNotificationTemplateIosPayloadMissingNotificationBody = -1994173005
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateMissingDefaultVersion"></a> PushNotificationTemplateMissingDefaultVersion

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_MISSING_DEFAULT_VERSION</code>.

```csharp
public const int PushNotificationTemplateMissingDefaultVersion = -1994173002
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateMissingName"></a> PushNotificationTemplateMissingName

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_MISSING_NAME</code>.

```csharp
public const int PushNotificationTemplateMissingName = -1994172978
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateMissingPlatformPayload"></a> PushNotificationTemplateMissingPlatformPayload

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_MISSING_PLATFORM_PAYLOAD</code>.

```csharp
public const int PushNotificationTemplateMissingPlatformPayload = -1994173009
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateNoCustomPayloadForV1"></a> PushNotificationTemplateNoCustomPayloadForV1

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_NO_CUSTOM_PAYLOAD_FOR_V_1</code>.

```csharp
public const int PushNotificationTemplateNoCustomPayloadForV1 = -1994173000
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplateNotFound"></a> PushNotificationTemplateNotFound

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_NOT_FOUND</code>.

```csharp
public const int PushNotificationTemplateNotFound = -1994173003
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushNotificationTemplatePayloadContainsInvalidJson"></a> PushNotificationTemplatePayloadContainsInvalidJson

<code>E_PF_PUSH_NOTIFICATION_TEMPLATE_PAYLOAD_CONTAINS_INVALID_JSON</code>.

```csharp
public const int PushNotificationTemplatePayloadContainsInvalidJson = -1994173008
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_PushServiceError"></a> PushServiceError

<code>E_PF_PUSH_SERVICE_ERROR</code>.

```csharp
public const int PushServiceError = -1994173314
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_QueryRateLimitExceeded"></a> QueryRateLimitExceeded

<code>E_PF_QUERY_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int QueryRateLimitExceeded = -1994172968
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ReceiptAlreadyUsed"></a> ReceiptAlreadyUsed

<code>E_PF_RECEIPT_ALREADY_USED</code>.

```csharp
public const int ReceiptAlreadyUsed = -1994173385
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ReceiptCancelled"></a> ReceiptCancelled

<code>E_PF_RECEIPT_CANCELLED</code>.

```csharp
public const int ReceiptCancelled = -1994173384
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ReceiptContainsMultipleInAppItems"></a> ReceiptContainsMultipleInAppItems

<code>E_PF_RECEIPT_CONTAINS_MULTIPLE_IN_APP_ITEMS</code>.

```csharp
public const int ReceiptContainsMultipleInAppItems = -1994173312
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ReceiptDoesNotContainInAppItems"></a> ReceiptDoesNotContainInAppItems

<code>E_PF_RECEIPT_DOES_NOT_CONTAIN_IN_APP_ITEMS</code>.

```csharp
public const int ReceiptDoesNotContainInAppItems = -1994173313
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RegionAtCapacity"></a> RegionAtCapacity

<code>E_PF_REGION_AT_CAPACITY</code>.

```csharp
public const int RegionAtCapacity = -1994173351
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RegistrationIncomplete"></a> RegistrationIncomplete

<code>E_PF_REGISTRATION_INCOMPLETE</code>.

```csharp
public const int RegistrationIncomplete = -1994173370
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RegistrationSessionNotFound"></a> RegistrationSessionNotFound

<code>E_PF_REGISTRATION_SESSION_NOT_FOUND</code>.

```csharp
public const int RegistrationSessionNotFound = -1994173364
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ReportDataNotRetrievedSuccessfully"></a> ReportDataNotRetrievedSuccessfully

<code>E_PF_REPORT_DATA_NOT_RETRIEVED_SUCCESSFULLY</code>.

```csharp
public const int ReportDataNotRetrievedSuccessfully = -1994172524
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ReportNotProcessed"></a> ReportNotProcessed

<code>E_PF_REPORT_NOT_PROCESSED</code>.

```csharp
public const int ReportNotProcessed = -1994172497
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RequestAlreadyRunning"></a> RequestAlreadyRunning

<code>E_PF_REQUEST_ALREADY_RUNNING</code>.

```csharp
public const int RequestAlreadyRunning = -1994173169
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RequestMultiplayerServersThrottledFromRateLimiter"></a> RequestMultiplayerServersThrottledFromRateLimiter

<code>E_PF_REQUEST_MULTIPLAYER_SERVERS_THROTTLED_FROM_RATE_LIMITER</code>.

```csharp
public const int RequestMultiplayerServersThrottledFromRateLimiter = -1994172913
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RequestViewConstraintParamsNotAllowed"></a> RequestViewConstraintParamsNotAllowed

<code>E_PF_REQUEST_VIEW_CONSTRAINT_PARAMS_NOT_ALLOWED</code>.

```csharp
public const int RequestViewConstraintParamsNotAllowed = -1994173116
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ReservedEventName"></a> ReservedEventName

<code>E_PF_RESERVED_EVENT_NAME</code>.

```csharp
public const int ReservedEventName = -1994173335
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ReservedWordInBody"></a> ReservedWordInBody

<code>E_PF_RESERVED_WORD_IN_BODY</code>.

```csharp
public const int ReservedWordInBody = -1994173338
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ResetIntervalCannotBeModified"></a> ResetIntervalCannotBeModified

<code>E_PF_RESET_INTERVAL_CANNOT_BE_MODIFIED</code>.

```csharp
public const int ResetIntervalCannotBeModified = -1994172519
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ResettableStatisticVersionRequired"></a> ResettableStatisticVersionRequired

<code>E_PF_RESETTABLE_STATISTIC_VERSION_REQUIRED</code>.

```csharp
public const int ResettableStatisticVersionRequired = -1994173228
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ResourceNameUpdateNotAllowed"></a> ResourceNameUpdateNotAllowed

<code>E_PF_RESOURCE_NAME_UPDATE_NOT_ALLOWED</code>.

```csharp
public const int ResourceNameUpdateNotAllowed = -1994172901
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ResourceNotModified"></a> ResourceNotModified

<code>E_PF_RESOURCE_NOT_MODIFIED</code>.

```csharp
public const int ResourceNotModified = -1994172494
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RestrictedEmailDomain"></a> RestrictedEmailDomain

<code>E_PF_RESTRICTED_EMAIL_DOMAIN</code>.

```csharp
public const int RestrictedEmailDomain = -1994173131
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RevisionNotFound"></a> RevisionNotFound

<code>E_PF_REVISION_NOT_FOUND</code>.

```csharp
public const int RevisionNotFound = -1994173293
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RoleDoesNotExist"></a> RoleDoesNotExist

<code>E_PF_ROLE_DOES_NOT_EXIST</code>.

```csharp
public const int RoleDoesNotExist = -1994173061
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RoleIsGroupAdmin"></a> RoleIsGroupAdmin

<code>E_PF_ROLE_IS_GROUP_ADMIN</code>.

```csharp
public const int RoleIsGroupAdmin = -1994173053
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RoleIsGroupDefaultMember"></a> RoleIsGroupDefaultMember

<code>E_PF_ROLE_IS_GROUP_DEFAULT_MEMBER</code>.

```csharp
public const int RoleIsGroupDefaultMember = -1994173054
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_RoleNameNotAvailable"></a> RoleNameNotAvailable

<code>E_PF_ROLE_NAME_NOT_AVAILABLE</code>.

```csharp
public const int RoleNameNotAvailable = -1994173052
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ScheduledTaskCreateConflict"></a> ScheduledTaskCreateConflict

<code>E_PF_SCHEDULED_TASK_CREATE_CONFLICT</code>.

```csharp
public const int ScheduledTaskCreateConflict = -1994173163
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ScheduledTaskNameConflict"></a> ScheduledTaskNameConflict

<code>E_PF_SCHEDULED_TASK_NAME_CONFLICT</code>.

```csharp
public const int ScheduledTaskNameConflict = -1994173164
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SecretKeyNotFound"></a> SecretKeyNotFound

<code>E_PF_SECRET_KEY_NOT_FOUND</code>.

```csharp
public const int SecretKeyNotFound = -1994173126
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SegmentManagementInvalidInput"></a> SegmentManagementInvalidInput

<code>E_PF_SEGMENT_MANAGEMENT_INVALID_INPUT</code>.

```csharp
public const int SegmentManagementInvalidInput = -1994172738
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SegmentManagementInvalidSegmentId"></a> SegmentManagementInvalidSegmentId

<code>E_PF_SEGMENT_MANAGEMENT_INVALID_SEGMENT_ID</code>.

```csharp
public const int SegmentManagementInvalidSegmentId = -1994172739
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SegmentManagementInvalidSegmentName"></a> SegmentManagementInvalidSegmentName

<code>E_PF_SEGMENT_MANAGEMENT_INVALID_SEGMENT_NAME</code>.

```csharp
public const int SegmentManagementInvalidSegmentName = -1994172737
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SegmentManagementNoExpressionTree"></a> SegmentManagementNoExpressionTree

<code>E_PF_SEGMENT_MANAGEMENT_NO_EXPRESSION_TREE</code>.

```csharp
public const int SegmentManagementNoExpressionTree = -1994172742
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SegmentManagementSegmentCountOverLimit"></a> SegmentManagementSegmentCountOverLimit

<code>E_PF_SEGMENT_MANAGEMENT_SEGMENT_COUNT_OVER_LIMIT</code>.

```csharp
public const int SegmentManagementSegmentCountOverLimit = -1994172740
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SegmentManagementTitleNotInFlight"></a> SegmentManagementTitleNotInFlight

<code>E_PF_SEGMENT_MANAGEMENT_TITLE_NOT_IN_FLIGHT</code>.

```csharp
public const int SegmentManagementTitleNotInFlight = -1994172743
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SegmentManagementTriggerActionCountOverLimit"></a> SegmentManagementTriggerActionCountOverLimit

<code>E_PF_SEGMENT_MANAGEMENT_TRIGGER_ACTION_COUNT_OVER_LIMIT</code>.

```csharp
public const int SegmentManagementTriggerActionCountOverLimit = -1994172741
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SegmentNotFound"></a> SegmentNotFound

<code>E_PF_SEGMENT_NOT_FOUND</code>.

```csharp
public const int SegmentNotFound = -1994173198
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ServerFailedToStart"></a> ServerFailedToStart

<code>E_PF_SERVER_FAILED_TO_START</code>.

```csharp
public const int ServerFailedToStart = -1994173350
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ServiceLimitLevelInTransition"></a> ServiceLimitLevelInTransition

<code>E_PF_SERVICE_LIMIT_LEVEL_IN_TRANSITION</code>.

```csharp
public const int ServiceLimitLevelInTransition = -1994173193
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ServiceUnavailable"></a> ServiceUnavailable

<code>E_PF_SERVICE_UNAVAILABLE</code>.

```csharp
public const int ServiceUnavailable = -1994173295
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SessionLogNotFound"></a> SessionLogNotFound

<code>E_PF_SESSION_LOG_NOT_FOUND</code>.

```csharp
public const int SessionLogNotFound = -1994173174
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SignedRequestNotAllowed"></a> SignedRequestNotAllowed

<code>E_PF_SIGNED_REQUEST_NOT_ALLOWED</code>.

```csharp
public const int SignedRequestNotAllowed = -1994173117
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SmtpAddonNotEnabled"></a> SmtpAddonNotEnabled

<code>E_PF_SMTP_ADDON_NOT_ENABLED</code>.

```csharp
public const int SmtpAddonNotEnabled = -1994173078
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SmtpServerAuthenticationError"></a> SmtpServerAuthenticationError

<code>E_PF_SMTP_SERVER_AUTHENTICATION_ERROR</code>.

```csharp
public const int SmtpServerAuthenticationError = -1994173108
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SmtpServerCommunicationError"></a> SmtpServerCommunicationError

<code>E_PF_SMTP_SERVER_COMMUNICATION_ERROR</code>.

```csharp
public const int SmtpServerCommunicationError = -1994173105
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SmtpServerGeneralFailure"></a> SmtpServerGeneralFailure

<code>E_PF_SMTP_SERVER_GENERAL_FAILURE</code>.

```csharp
public const int SmtpServerGeneralFailure = -1994173104
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SmtpServerInsufficientStorage"></a> SmtpServerInsufficientStorage

<code>E_PF_SMTP_SERVER_INSUFFICIENT_STORAGE</code>.

```csharp
public const int SmtpServerInsufficientStorage = -1994173106
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SmtpServerLimitExceeded"></a> SmtpServerLimitExceeded

<code>E_PF_SMTP_SERVER_LIMIT_EXCEEDED</code>.

```csharp
public const int SmtpServerLimitExceeded = -1994173107
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SnapshotNotFound"></a> SnapshotNotFound

<code>E_PF_SNAPSHOT_NOT_FOUND</code>.

```csharp
public const int SnapshotNotFound = -1994172728
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SpecifiedVersionLeaderboardNotFound"></a> SpecifiedVersionLeaderboardNotFound

<code>E_PF_SPECIFIED_VERSION_LEADERBOARD_NOT_FOUND</code>.

```csharp
public const int SpecifiedVersionLeaderboardNotFound = -1994172559
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatDefinitionAlreadyLinkedToLeaderboard"></a> StatDefinitionAlreadyLinkedToLeaderboard

<code>E_PF_STAT_DEFINITION_ALREADY_LINKED_TO_LEADERBOARD</code>.

```csharp
public const int StatDefinitionAlreadyLinkedToLeaderboard = -1994172630
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StateShareCreatedStatesLimitExceeded"></a> StateShareCreatedStatesLimitExceeded

<code>E_PF_STATE_SHARE_CREATED_STATES_LIMIT_EXCEEDED</code>.

```csharp
public const int StateShareCreatedStatesLimitExceeded = -1994172563
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StateShareForbidden"></a> StateShareForbidden

<code>E_PF_STATE_SHARE_FORBIDDEN</code>.

```csharp
public const int StateShareForbidden = -1994172567
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StateShareIdMissingOrMalformed"></a> StateShareIdMissingOrMalformed

<code>E_PF_STATE_SHARE_ID_MISSING_OR_MALFORMED</code>.

```csharp
public const int StateShareIdMissingOrMalformed = -1994172562
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StateShareLinkNotFound"></a> StateShareLinkNotFound

<code>E_PF_STATE_SHARE_LINK_NOT_FOUND</code>.

```csharp
public const int StateShareLinkNotFound = -1994172636
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StateShareStateNotFound"></a> StateShareStateNotFound

<code>E_PF_STATE_SHARE_STATE_NOT_FOUND</code>.

```csharp
public const int StateShareStateNotFound = -1994172637
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StateShareStateRedemptionLimitExceeded"></a> StateShareStateRedemptionLimitExceeded

<code>E_PF_STATE_SHARE_STATE_REDEMPTION_LIMIT_EXCEEDED</code>.

```csharp
public const int StateShareStateRedemptionLimitExceeded = -1994172565
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StateShareStateRedemptionLimitNotUpdated"></a> StateShareStateRedemptionLimitNotUpdated

<code>E_PF_STATE_SHARE_STATE_REDEMPTION_LIMIT_NOT_UPDATED</code>.

```csharp
public const int StateShareStateRedemptionLimitNotUpdated = -1994172564
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StateShareTitleNotInFlight"></a> StateShareTitleNotInFlight

<code>E_PF_STATE_SHARE_TITLE_NOT_IN_FLIGHT</code>.

```csharp
public const int StateShareTitleNotInFlight = -1994172566
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticAlreadyHasPrizeTable"></a> StatisticAlreadyHasPrizeTable

<code>E_PF_STATISTIC_ALREADY_HAS_PRIZE_TABLE</code>.

```csharp
public const int StatisticAlreadyHasPrizeTable = -1994173140
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticChildNameInvalid"></a> StatisticChildNameInvalid

<code>E_PF_STATISTIC_CHILD_NAME_INVALID</code>.

```csharp
public const int StatisticChildNameInvalid = -1994173018
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticColumnAggregationMismatch"></a> StatisticColumnAggregationMismatch

<code>E_PF_STATISTIC_COLUMN_AGGREGATION_MISMATCH</code>.

```csharp
public const int StatisticColumnAggregationMismatch = -1994172482
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticColumnLengthMismatch"></a> StatisticColumnLengthMismatch

<code>E_PF_STATISTIC_COLUMN_LENGTH_MISMATCH</code>.

```csharp
public const int StatisticColumnLengthMismatch = -1994172539
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticCountLimitExceeded"></a> StatisticCountLimitExceeded

<code>E_PF_STATISTIC_COUNT_LIMIT_EXCEEDED</code>.

```csharp
public const int StatisticCountLimitExceeded = -1994173215
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticDefinitionHasNullOrEmptyVersionConfiguration"></a> StatisticDefinitionHasNullOrEmptyVersionConfiguration

<code>E_PF_STATISTIC_DEFINITION_HAS_NULL_OR_EMPTY_VERSION_CONFIGURATION</code>.

```csharp
public const int StatisticDefinitionHasNullOrEmptyVersionConfiguration = -1994172540
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticDefinitionModificationNotAllowedWhileLinked"></a> StatisticDefinitionModificationNotAllowedWhileLinked

<code>E_PF_STATISTIC_DEFINITION_MODIFICATION_NOT_ALLOWED_WHILE_LINKED</code>.

```csharp
public const int StatisticDefinitionModificationNotAllowedWhileLinked = -1994172609
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticNameConflict"></a> StatisticNameConflict

<code>E_PF_STATISTIC_NAME_CONFLICT</code>.

```csharp
public const int StatisticNameConflict = -1994173222
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticNotFound"></a> StatisticNotFound

<code>E_PF_STATISTIC_NOT_FOUND</code>.

```csharp
public const int StatisticNotFound = -1994173223
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticResetIntervalMismatch"></a> StatisticResetIntervalMismatch

<code>E_PF_STATISTIC_RESET_INTERVAL_MISMATCH</code>.

```csharp
public const int StatisticResetIntervalMismatch = -1994172481
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticUpdateInProgress"></a> StatisticUpdateInProgress

<code>E_PF_STATISTIC_UPDATE_IN_PROGRESS</code>.

```csharp
public const int StatisticUpdateInProgress = -1994173142
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticUpdateNotAllowedWhileLinked"></a> StatisticUpdateNotAllowedWhileLinked

<code>E_PF_STATISTIC_UPDATE_NOT_ALLOWED_WHILE_LINKED</code>.

```csharp
public const int StatisticUpdateNotAllowedWhileLinked = -1994172465
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticValueAggregationOverflow"></a> StatisticValueAggregationOverflow

<code>E_PF_STATISTIC_VALUE_AGGREGATION_OVERFLOW</code>.

```csharp
public const int StatisticValueAggregationOverflow = -1994173111
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticVersionAlreadyIncrementedForScheduledInterval"></a> StatisticVersionAlreadyIncrementedForScheduledInterval

<code>E_PF_STATISTIC_VERSION_ALREADY_INCREMENTED_FOR_SCHEDULED_INTERVAL</code>.

```csharp
public const int StatisticVersionAlreadyIncrementedForScheduledInterval = -1994173216
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticVersionClosedForWrites"></a> StatisticVersionClosedForWrites

<code>E_PF_STATISTIC_VERSION_CLOSED_FOR_WRITES</code>.

```csharp
public const int StatisticVersionClosedForWrites = -1994173221
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticVersionIncrementRateExceeded"></a> StatisticVersionIncrementRateExceeded

<code>E_PF_STATISTIC_VERSION_INCREMENT_RATE_EXCEEDED</code>.

```csharp
public const int StatisticVersionIncrementRateExceeded = -1994173214
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StatisticVersionInvalid"></a> StatisticVersionInvalid

<code>E_PF_STATISTIC_VERSION_INVALID</code>.

```csharp
public const int StatisticVersionInvalid = -1994173220
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SteamApplicationNotOwned"></a> SteamApplicationNotOwned

<code>E_PF_STEAM_APPLICATION_NOT_OWNED</code>.

```csharp
public const int SteamApplicationNotOwned = -1994173367
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SteamNotEnabledForTitle"></a> SteamNotEnabledForTitle

<code>E_PF_STEAM_NOT_ENABLED_FOR_TITLE</code>.

```csharp
public const int SteamNotEnabledForTitle = -1994173160
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SteamUserNotFound"></a> SteamUserNotFound

<code>E_PF_STEAM_USER_NOT_FOUND</code>.

```csharp
public const int SteamUserNotFound = -1994172907
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StoreMetricsErrorRetrievingMetrics"></a> StoreMetricsErrorRetrievingMetrics

<code>E_PF_STORE_METRICS_ERROR_RETRIEVING_METRICS</code>.

```csharp
public const int StoreMetricsErrorRetrievingMetrics = -1994172461
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StoreMetricsRequestInvalidInput"></a> StoreMetricsRequestInvalidInput

<code>E_PF_STORE_METRICS_REQUEST_INVALID_INPUT</code>.

```csharp
public const int StoreMetricsRequestInvalidInput = -1994172470
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StoreNotFound"></a> StoreNotFound

<code>E_PF_STORE_NOT_FOUND</code>.

```csharp
public const int StoreNotFound = -1994173197
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StreamAlreadyExists"></a> StreamAlreadyExists

<code>E_PF_STREAM_ALREADY_EXISTS</code>.

```csharp
public const int StreamAlreadyExists = -1994173332
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StreamNotFound"></a> StreamNotFound

<code>E_PF_STREAM_NOT_FOUND</code>.

```csharp
public const int StreamNotFound = -1994173330
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StudioActivated"></a> StudioActivated

<code>E_PF_STUDIO_ACTIVATED</code>.

```csharp
public const int StudioActivated = -1994172958
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StudioCreationInProgress"></a> StudioCreationInProgress

<code>E_PF_STUDIO_CREATION_IN_PROGRESS</code>.

```csharp
public const int StudioCreationInProgress = -1994172963
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StudioCreationLimitExceeded"></a> StudioCreationLimitExceeded

<code>E_PF_STUDIO_CREATION_LIMIT_EXCEEDED</code>.

```csharp
public const int StudioCreationLimitExceeded = -1994172493
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StudioCreationRateLimited"></a> StudioCreationRateLimited

<code>E_PF_STUDIO_CREATION_RATE_LIMITED</code>.

```csharp
public const int StudioCreationRateLimited = -1994172964
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StudioDeactivated"></a> StudioDeactivated

<code>E_PF_STUDIO_DEACTIVATED</code>.

```csharp
public const int StudioDeactivated = -1994172959
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StudioDeleted"></a> StudioDeleted

<code>E_PF_STUDIO_DELETED</code>.

```csharp
public const int StudioDeleted = -1994172960
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StudioDeletionInitiated"></a> StudioDeletionInitiated

<code>E_PF_STUDIO_DELETION_INITIATED</code>.

```csharp
public const int StudioDeletionInitiated = -1994172471
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_StudioNotFound"></a> StudioNotFound

<code>E_PF_STUDIO_NOT_FOUND</code>.

```csharp
public const int StudioNotFound = -1994172961
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_SubscriptionAlreadyTaken"></a> SubscriptionAlreadyTaken

<code>E_PF_SUBSCRIPTION_ALREADY_TAKEN</code>.

```csharp
public const int SubscriptionAlreadyTaken = -1994173079
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TagInvalid"></a> TagInvalid

<code>E_PF_TAG_INVALID</code>.

```csharp
public const int TagInvalid = -1994172484
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TagTooLong"></a> TagTooLong

<code>E_PF_TAG_TOO_LONG</code>.

```csharp
public const int TagTooLong = -1994172483
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TaskInstanceNotFound"></a> TaskInstanceNotFound

<code>E_PF_TASK_INSTANCE_NOT_FOUND</code>.

```csharp
public const int TaskInstanceNotFound = -1994173156
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TaskNotFound"></a> TaskNotFound

<code>E_PF_TASK_NOT_FOUND</code>.

```csharp
public const int TaskNotFound = -1994173157
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TelemetryIngestionKeyNotFound"></a> TelemetryIngestionKeyNotFound

<code>E_PF_TELEMETRY_INGESTION_KEY_NOT_FOUND</code>.

```csharp
public const int TelemetryIngestionKeyNotFound = -1994173019
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TelemetryIngestionKeyPending"></a> TelemetryIngestionKeyPending

<code>E_PF_TELEMETRY_INGESTION_KEY_PENDING</code>.

```csharp
public const int TelemetryIngestionKeyPending = -1994173020
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TelemetryKeyAlreadyExists"></a> TelemetryKeyAlreadyExists

<code>E_PF_TELEMETRY_KEY_ALREADY_EXISTS</code>.

```csharp
public const int TelemetryKeyAlreadyExists = -1994172702
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TelemetryKeyCountOverLimit"></a> TelemetryKeyCountOverLimit

<code>E_PF_TELEMETRY_KEY_COUNT_OVER_LIMIT</code>.

```csharp
public const int TelemetryKeyCountOverLimit = -1994172700
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TelemetryKeyDeactivated"></a> TelemetryKeyDeactivated

<code>E_PF_TELEMETRY_KEY_DEACTIVATED</code>.

```csharp
public const int TelemetryKeyDeactivated = -1994172699
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TelemetryKeyInvalid"></a> TelemetryKeyInvalid

<code>E_PF_TELEMETRY_KEY_INVALID</code>.

```csharp
public const int TelemetryKeyInvalid = -1994172701
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TelemetryKeyInvalidName"></a> TelemetryKeyInvalidName

<code>E_PF_TELEMETRY_KEY_INVALID_NAME</code>.

```csharp
public const int TelemetryKeyInvalidName = -1994172703
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TelemetryKeyLongInsightsRetentionNotAllowed"></a> TelemetryKeyLongInsightsRetentionNotAllowed

<code>E_PF_TELEMETRY_KEY_LONG_INSIGHTS_RETENTION_NOT_ALLOWED</code>.

```csharp
public const int TelemetryKeyLongInsightsRetentionNotAllowed = -1994172698
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TelemetryKeyNotFound"></a> TelemetryKeyNotFound

<code>E_PF_TELEMETRY_KEY_NOT_FOUND</code>.

```csharp
public const int TelemetryKeyNotFound = -1994172704
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TemplateVersionNotDefined"></a> TemplateVersionNotDefined

<code>E_PF_TEMPLATE_VERSION_NOT_DEFINED</code>.

```csharp
public const int TemplateVersionNotDefined = -1994173074
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TemplateVersionTooOld"></a> TemplateVersionTooOld

<code>E_PF_TEMPLATE_VERSION_TOO_OLD</code>.

```csharp
public const int TemplateVersionTooOld = -1994173066
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TenantShardMapperShardNotFound"></a> TenantShardMapperShardNotFound

<code>E_PF_TENANT_SHARD_MAPPER_SHARD_NOT_FOUND</code>.

```csharp
public const int TenantShardMapperShardNotFound = -1994172775
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleActivated"></a> TitleActivated

<code>E_PF_TITLE_ACTIVATED</code>.

```csharp
public const int TitleActivated = -1994172951
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleActivationInProgress"></a> TitleActivationInProgress

<code>E_PF_TITLE_ACTIVATION_IN_PROGRESS</code>.

```csharp
public const int TitleActivationInProgress = -1994172953
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleActivationRateLimited"></a> TitleActivationRateLimited

<code>E_PF_TITLE_ACTIVATION_RATE_LIMITED</code>.

```csharp
public const int TitleActivationRateLimited = -1994172954
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleCleanupInProgress"></a> TitleCleanupInProgress

<code>E_PF_TITLE_CLEANUP_IN_PROGRESS</code>.

```csharp
public const int TitleCleanupInProgress = -1994172893
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleConfigNotFound"></a> TitleConfigNotFound

<code>E_PF_TITLE_CONFIG_NOT_FOUND</code>.

```csharp
public const int TitleConfigNotFound = -1994172832
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleConfigSerializationError"></a> TitleConfigSerializationError

<code>E_PF_TITLE_CONFIG_SERIALIZATION_ERROR</code>.

```csharp
public const int TitleConfigSerializationError = -1994172830
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleConfigUpdateConflict"></a> TitleConfigUpdateConflict

<code>E_PF_TITLE_CONFIG_UPDATE_CONFLICT</code>.

```csharp
public const int TitleConfigUpdateConflict = -1994172831
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleConstraintsPublisherDeletion"></a> TitleConstraintsPublisherDeletion

<code>E_PF_TITLE_CONSTRAINTS_PUBLISHER_DELETION</code>.

```csharp
public const int TitleConstraintsPublisherDeletion = -1994172897
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleContainsUserAccounts"></a> TitleContainsUserAccounts

<code>E_PF_TITLE_CONTAINS_USER_ACCOUNTS</code>.

```csharp
public const int TitleContainsUserAccounts = -1994173071
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleCreationInProgress"></a> TitleCreationInProgress

<code>E_PF_TITLE_CREATION_IN_PROGRESS</code>.

```csharp
public const int TitleCreationInProgress = -1994172956
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleCreationRateLimited"></a> TitleCreationRateLimited

<code>E_PF_TITLE_CREATION_RATE_LIMITED</code>.

```csharp
public const int TitleCreationRateLimited = -1994172957
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleDataOverrideNotFound"></a> TitleDataOverrideNotFound

<code>E_PF_TITLE_DATA_OVERRIDE_NOT_FOUND</code>.

```csharp
public const int TitleDataOverrideNotFound = -1994172912
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleDeactivated"></a> TitleDeactivated

<code>E_PF_TITLE_DEACTIVATED</code>.

```csharp
public const int TitleDeactivated = -1994172952
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleDefaultLanguageNotSet"></a> TitleDefaultLanguageNotSet

<code>E_PF_TITLE_DEFAULT_LANGUAGE_NOT_SET</code>.

```csharp
public const int TitleDefaultLanguageNotSet = -1994173026
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleDeleted"></a> TitleDeleted

<code>E_PF_TITLE_DELETED</code>.

```csharp
public const int TitleDeleted = -1994173072
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleDeletionPlayerCleanupFailure"></a> TitleDeletionPlayerCleanupFailure

<code>E_PF_TITLE_DELETION_PLAYER_CLEANUP_FAILURE</code>.

```csharp
public const int TitleDeletionPlayerCleanupFailure = -1994173070
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNameConflicts"></a> TitleNameConflicts

<code>E_PF_TITLE_NAME_CONFLICTS</code>.

```csharp
public const int TitleNameConflicts = -1994173378
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNewsDuplicateLanguage"></a> TitleNewsDuplicateLanguage

<code>E_PF_TITLE_NEWS_DUPLICATE_LANGUAGE</code>.

```csharp
public const int TitleNewsDuplicateLanguage = -1994172996
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNewsInvalidLanguage"></a> TitleNewsInvalidLanguage

<code>E_PF_TITLE_NEWS_INVALID_LANGUAGE</code>.

```csharp
public const int TitleNewsInvalidLanguage = -1994172994
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNewsItemCountLimitExceeded"></a> TitleNewsItemCountLimitExceeded

<code>E_PF_TITLE_NEWS_ITEM_COUNT_LIMIT_EXCEEDED</code>.

```csharp
public const int TitleNewsItemCountLimitExceeded = -1994173187
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNewsMissingDefaultLanguage"></a> TitleNewsMissingDefaultLanguage

<code>E_PF_TITLE_NEWS_MISSING_DEFAULT_LANGUAGE</code>.

```csharp
public const int TitleNewsMissingDefaultLanguage = -1994172998
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNewsMissingTitleOrBody"></a> TitleNewsMissingTitleOrBody

<code>E_PF_TITLE_NEWS_MISSING_TITLE_OR_BODY</code>.

```csharp
public const int TitleNewsMissingTitleOrBody = -1994172995
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNewsNotFound"></a> TitleNewsNotFound

<code>E_PF_TITLE_NEWS_NOT_FOUND</code>.

```csharp
public const int TitleNewsNotFound = -1994172997
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNotActivated"></a> TitleNotActivated

<code>E_PF_TITLE_NOT_ACTIVATED</code>.

```csharp
public const int TitleNotActivated = -1994173365
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNotEnabledForParty"></a> TitleNotEnabledForParty

<code>E_PF_TITLE_NOT_ENABLED_FOR_PARTY</code>.

```csharp
public const int TitleNotEnabledForParty = -1994172774
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNotOnUpdatedPricingPlan"></a> TitleNotOnUpdatedPricingPlan

<code>E_PF_TITLE_NOT_ON_UPDATED_PRICING_PLAN</code>.

```csharp
public const int TitleNotOnUpdatedPricingPlan = -1994172744
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitleNotQualifiedForLimit"></a> TitleNotQualifiedForLimit

<code>E_PF_TITLE_NOT_QUALIFIED_FOR_LIMIT</code>.

```csharp
public const int TitleNotQualifiedForLimit = -1994173195
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TitlePublisherUpdateNotAllowed"></a> TitlePublisherUpdateNotAllowed

<code>E_PF_TITLE_PUBLISHER_UPDATE_NOT_ALLOWED</code>.

```csharp
public const int TitlePublisherUpdateNotAllowed = -1994172891
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TokenSigningKeyNotFound"></a> TokenSigningKeyNotFound

<code>E_PF_TOKEN_SIGNING_KEY_NOT_FOUND</code>.

```csharp
public const int TokenSigningKeyNotFound = -1994172634
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TooManyKeys"></a> TooManyKeys

<code>E_PF_TOO_MANY_KEYS</code>.

```csharp
public const int TooManyKeys = -1994173271
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TotalDataSizeExceeded"></a> TotalDataSizeExceeded

<code>E_PF_TOTAL_DATA_SIZE_EXCEEDED</code>.

```csharp
public const int TotalDataSizeExceeded = -1994173232
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeAcceptedCatalogItemInvalid"></a> TradeAcceptedCatalogItemInvalid

<code>E_PF_TRADE_ACCEPTED_CATALOG_ITEM_INVALID</code>.

```csharp
public const int TradeAcceptedCatalogItemInvalid = -1994173248
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeAcceptedCatalogItemIsNotTradable"></a> TradeAcceptedCatalogItemIsNotTradable

<code>E_PF_TRADE_ACCEPTED_CATALOG_ITEM_IS_NOT_TRADABLE</code>.

```csharp
public const int TradeAcceptedCatalogItemIsNotTradable = -1994173236
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeAcceptedItemIsBundle"></a> TradeAcceptedItemIsBundle

<code>E_PF_TRADE_ACCEPTED_ITEM_IS_BUNDLE</code>.

```csharp
public const int TradeAcceptedItemIsBundle = -1994173251
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeAcceptedItemIsStackable"></a> TradeAcceptedItemIsStackable

<code>E_PF_TRADE_ACCEPTED_ITEM_IS_STACKABLE</code>.

```csharp
public const int TradeAcceptedItemIsStackable = -1994173250
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeAcceptedItemsMismatch"></a> TradeAcceptedItemsMismatch

<code>E_PF_TRADE_ACCEPTED_ITEMS_MISMATCH</code>.

```csharp
public const int TradeAcceptedItemsMismatch = -1994173243
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeAcceptingUserNotAllowed"></a> TradeAcceptingUserNotAllowed

<code>E_PF_TRADE_ACCEPTING_USER_NOT_ALLOWED</code>.

```csharp
public const int TradeAcceptingUserNotAllowed = -1994173262
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeAllowedUsersInvalid"></a> TradeAllowedUsersInvalid

<code>E_PF_TRADE_ALLOWED_USERS_INVALID</code>.

```csharp
public const int TradeAllowedUsersInvalid = -1994173247
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeAlreadyFilled"></a> TradeAlreadyFilled

<code>E_PF_TRADE_ALREADY_FILLED</code>.

```csharp
public const int TradeAlreadyFilled = -1994173255
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeCancelled"></a> TradeCancelled

<code>E_PF_TRADE_CANCELLED</code>.

```csharp
public const int TradeCancelled = -1994173256
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeDoesNotExist"></a> TradeDoesNotExist

<code>E_PF_TRADE_DOES_NOT_EXIST</code>.

```csharp
public const int TradeDoesNotExist = -1994173257
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeInventoryItemDoesNotExist"></a> TradeInventoryItemDoesNotExist

<code>E_PF_TRADE_INVENTORY_ITEM_DOES_NOT_EXIST</code>.

```csharp
public const int TradeInventoryItemDoesNotExist = -1994173246
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeInventoryItemExpired"></a> TradeInventoryItemExpired

<code>E_PF_TRADE_INVENTORY_ITEM_EXPIRED</code>.

```csharp
public const int TradeInventoryItemExpired = -1994173253
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeInventoryItemInvalidStatus"></a> TradeInventoryItemInvalidStatus

<code>E_PF_TRADE_INVENTORY_ITEM_INVALID_STATUS</code>.

```csharp
public const int TradeInventoryItemInvalidStatus = -1994173249
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeInventoryItemIsAssignedToCharacter"></a> TradeInventoryItemIsAssignedToCharacter

<code>E_PF_TRADE_INVENTORY_ITEM_IS_ASSIGNED_TO_CHARACTER</code>.

```csharp
public const int TradeInventoryItemIsAssignedToCharacter = -1994173261
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeInventoryItemIsBundle"></a> TradeInventoryItemIsBundle

<code>E_PF_TRADE_INVENTORY_ITEM_IS_BUNDLE</code>.

```csharp
public const int TradeInventoryItemIsBundle = -1994173260
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeInventoryItemIsConsumed"></a> TradeInventoryItemIsConsumed

<code>E_PF_TRADE_INVENTORY_ITEM_IS_CONSUMED</code>.

```csharp
public const int TradeInventoryItemIsConsumed = -1994173245
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeInventoryItemIsNotTradable"></a> TradeInventoryItemIsNotTradable

<code>E_PF_TRADE_INVENTORY_ITEM_IS_NOT_TRADABLE</code>.

```csharp
public const int TradeInventoryItemIsNotTradable = -1994173237
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeInventoryItemIsStackable"></a> TradeInventoryItemIsStackable

<code>E_PF_TRADE_INVENTORY_ITEM_IS_STACKABLE</code>.

```csharp
public const int TradeInventoryItemIsStackable = -1994173244
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeMissingOfferedAndAcceptedItems"></a> TradeMissingOfferedAndAcceptedItems

<code>E_PF_TRADE_MISSING_OFFERED_AND_ACCEPTED_ITEMS</code>.

```csharp
public const int TradeMissingOfferedAndAcceptedItems = -1994173252
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeStatusNotValidForAccepting"></a> TradeStatusNotValidForAccepting

<code>E_PF_TRADE_STATUS_NOT_VALID_FOR_ACCEPTING</code>.

```csharp
public const int TradeStatusNotValidForAccepting = -1994173258
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeStatusNotValidForCancelling"></a> TradeStatusNotValidForCancelling

<code>E_PF_TRADE_STATUS_NOT_VALID_FOR_CANCELLING</code>.

```csharp
public const int TradeStatusNotValidForCancelling = -1994173259
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TradeWaitForStatusTimeout"></a> TradeWaitForStatusTimeout

<code>E_PF_TRADE_WAIT_FOR_STATUS_TIMEOUT</code>.

```csharp
public const int TradeWaitForStatusTimeout = -1994173254
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TransactionAlreadyApplied"></a> TransactionAlreadyApplied

<code>E_PF_TRANSACTION_ALREADY_APPLIED</code>.

```csharp
public const int TransactionAlreadyApplied = -1994172525
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillActiveModelLimitExceeded"></a> TrueSkillActiveModelLimitExceeded

<code>E_PF_TRUE_SKILL_ACTIVE_MODEL_LIMIT_EXCEEDED</code>.

```csharp
public const int TrueSkillActiveModelLimitExceeded = -1994172580
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillBadPlayerIdInMatchResult"></a> TrueSkillBadPlayerIdInMatchResult

<code>E_PF_TRUE_SKILL_BAD_PLAYER_ID_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillBadPlayerIdInMatchResult = -1994172595
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillConditionKeyLimitExceeded"></a> TrueSkillConditionKeyLimitExceeded

<code>E_PF_TRUE_SKILL_CONDITION_KEY_LIMIT_EXCEEDED</code>.

```csharp
public const int TrueSkillConditionKeyLimitExceeded = -1994172570
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillConditionSetNotInModel"></a> TrueSkillConditionSetNotInModel

<code>E_PF_TRUE_SKILL_CONDITION_SET_NOT_IN_MODEL</code>.

```csharp
public const int TrueSkillConditionSetNotInModel = -1994172546
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillConditionStateIsRequired"></a> TrueSkillConditionStateIsRequired

<code>E_PF_TRUE_SKILL_CONDITION_STATE_IS_REQUIRED</code>.

```csharp
public const int TrueSkillConditionStateIsRequired = -1994172575
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillConditionValuePerKeyLimitExceeded"></a> TrueSkillConditionValuePerKeyLimitExceeded

<code>E_PF_TRUE_SKILL_CONDITION_VALUE_PER_KEY_LIMIT_EXCEEDED</code>.

```csharp
public const int TrueSkillConditionValuePerKeyLimitExceeded = -1994172569
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillDuplicateCondition"></a> TrueSkillDuplicateCondition

<code>E_PF_TRUE_SKILL_DUPLICATE_CONDITION</code>.

```csharp
public const int TrueSkillDuplicateCondition = -1994172572
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillDuplicateEvent"></a> TrueSkillDuplicateEvent

<code>E_PF_TRUE_SKILL_DUPLICATE_EVENT</code>.

```csharp
public const int TrueSkillDuplicateEvent = -1994172573
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillDuplicatePlayerInMatchResult"></a> TrueSkillDuplicatePlayerInMatchResult

<code>E_PF_TRUE_SKILL_DUPLICATE_PLAYER_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillDuplicatePlayerInMatchResult = -1994172649
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillEndTimeBeforeStartTime"></a> TrueSkillEndTimeBeforeStartTime

<code>E_PF_TRUE_SKILL_END_TIME_BEFORE_START_TIME</code>.

```csharp
public const int TrueSkillEndTimeBeforeStartTime = -1994172589
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillEndTimeMissingInMatchResult"></a> TrueSkillEndTimeMissingInMatchResult

<code>E_PF_TRUE_SKILL_END_TIME_MISSING_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillEndTimeMissingInMatchResult = -1994172592
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillEventLimitExceeded"></a> TrueSkillEventLimitExceeded

<code>E_PF_TRUE_SKILL_EVENT_LIMIT_EXCEEDED</code>.

```csharp
public const int TrueSkillEventLimitExceeded = -1994172568
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillEventStateIsRequired"></a> TrueSkillEventStateIsRequired

<code>E_PF_TRUE_SKILL_EVENT_STATE_IS_REQUIRED</code>.

```csharp
public const int TrueSkillEventStateIsRequired = -1994172574
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidAnomalyThreshold"></a> TrueSkillInvalidAnomalyThreshold

<code>E_PF_TRUE_SKILL_INVALID_ANOMALY_THRESHOLD</code>.

```csharp
public const int TrueSkillInvalidAnomalyThreshold = -1994172571
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidBotIdInMatchResult"></a> TrueSkillInvalidBotIdInMatchResult

<code>E_PF_TRUE_SKILL_INVALID_BOT_ID_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillInvalidBotIdInMatchResult = -1994172594
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidConditionAffinityWeight"></a> TrueSkillInvalidConditionAffinityWeight

<code>E_PF_TRUE_SKILL_INVALID_CONDITION_AFFINITY_WEIGHT</code>.

```csharp
public const int TrueSkillInvalidConditionAffinityWeight = -1994172598
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidConditionKey"></a> TrueSkillInvalidConditionKey

<code>E_PF_TRUE_SKILL_INVALID_CONDITION_KEY</code>.

```csharp
public const int TrueSkillInvalidConditionKey = -1994172600
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidConditionRank"></a> TrueSkillInvalidConditionRank

<code>E_PF_TRUE_SKILL_INVALID_CONDITION_RANK</code>.

```csharp
public const int TrueSkillInvalidConditionRank = -1994172521
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidConditionValue"></a> TrueSkillInvalidConditionValue

<code>E_PF_TRUE_SKILL_INVALID_CONDITION_VALUE</code>.

```csharp
public const int TrueSkillInvalidConditionValue = -1994172599
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidConditionsList"></a> TrueSkillInvalidConditionsList

<code>E_PF_TRUE_SKILL_INVALID_CONDITIONS_LIST</code>.

```csharp
public const int TrueSkillInvalidConditionsList = -1994172492
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidEntityKey"></a> TrueSkillInvalidEntityKey

<code>E_PF_TRUE_SKILL_INVALID_ENTITY_KEY</code>.

```csharp
public const int TrueSkillInvalidEntityKey = -1994172601
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidEventCount"></a> TrueSkillInvalidEventCount

<code>E_PF_TRUE_SKILL_INVALID_EVENT_COUNT</code>.

```csharp
public const int TrueSkillInvalidEventCount = -1994172591
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidEventName"></a> TrueSkillInvalidEventName

<code>E_PF_TRUE_SKILL_INVALID_EVENT_NAME</code>.

```csharp
public const int TrueSkillInvalidEventName = -1994172597
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidJobId"></a> TrueSkillInvalidJobId

<code>E_PF_TRUE_SKILL_INVALID_JOB_ID</code>.

```csharp
public const int TrueSkillInvalidJobId = -1994172588
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidMaxIterations"></a> TrueSkillInvalidMaxIterations

<code>E_PF_TRUE_SKILL_INVALID_MAX_ITERATIONS</code>.

```csharp
public const int TrueSkillInvalidMaxIterations = -1994172590
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidMetadataId"></a> TrueSkillInvalidMetadataId

<code>E_PF_TRUE_SKILL_INVALID_METADATA_ID</code>.

```csharp
public const int TrueSkillInvalidMetadataId = -1994172587
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidModelId"></a> TrueSkillInvalidModelId

<code>E_PF_TRUE_SKILL_INVALID_MODEL_ID</code>.

```csharp
public const int TrueSkillInvalidModelId = -1994172604
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidModelName"></a> TrueSkillInvalidModelName

<code>E_PF_TRUE_SKILL_INVALID_MODEL_NAME</code>.

```csharp
public const int TrueSkillInvalidModelName = -1994172603
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidPlayerId"></a> TrueSkillInvalidPlayerId

<code>E_PF_TRUE_SKILL_INVALID_PLAYER_ID</code>.

```csharp
public const int TrueSkillInvalidPlayerId = -1994172548
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidPlayerIds"></a> TrueSkillInvalidPlayerIds

<code>E_PF_TRUE_SKILL_INVALID_PLAYER_IDS</code>.

```csharp
public const int TrueSkillInvalidPlayerIds = -1994172602
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidPlayerSecondsPlayedInMatchResult"></a> TrueSkillInvalidPlayerSecondsPlayedInMatchResult

<code>E_PF_TRUE_SKILL_INVALID_PLAYER_SECONDS_PLAYED_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillInvalidPlayerSecondsPlayedInMatchResult = -1994172620
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidPlayers"></a> TrueSkillInvalidPlayers

<code>E_PF_TRUE_SKILL_INVALID_PLAYERS</code>.

```csharp
public const int TrueSkillInvalidPlayers = -1994172550
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidPreMatchPartyInMatchResult"></a> TrueSkillInvalidPreMatchPartyInMatchResult

<code>E_PF_TRUE_SKILL_INVALID_PRE_MATCH_PARTY_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillInvalidPreMatchPartyInMatchResult = -1994172622
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidRanksInMatchResult"></a> TrueSkillInvalidRanksInMatchResult

<code>E_PF_TRUE_SKILL_INVALID_RANKS_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillInvalidRanksInMatchResult = -1994172648
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidScenarioId"></a> TrueSkillInvalidScenarioId

<code>E_PF_TRUE_SKILL_INVALID_SCENARIO_ID</code>.

```csharp
public const int TrueSkillInvalidScenarioId = -1994172605
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidScenarioName"></a> TrueSkillInvalidScenarioName

<code>E_PF_TRUE_SKILL_INVALID_SCENARIO_NAME</code>.

```csharp
public const int TrueSkillInvalidScenarioName = -1994172576
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidSquadSize"></a> TrueSkillInvalidSquadSize

<code>E_PF_TRUE_SKILL_INVALID_SQUAD_SIZE</code>.

```csharp
public const int TrueSkillInvalidSquadSize = -1994172547
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidTimestamp"></a> TrueSkillInvalidTimestamp

<code>E_PF_TRUE_SKILL_INVALID_TIMESTAMP</code>.

```csharp
public const int TrueSkillInvalidTimestamp = -1994172551
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidTimestampInMatchResult"></a> TrueSkillInvalidTimestampInMatchResult

<code>E_PF_TRUE_SKILL_INVALID_TIMESTAMP_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillInvalidTimestampInMatchResult = -1994172621
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillInvalidTitleId"></a> TrueSkillInvalidTitleId

<code>E_PF_TRUE_SKILL_INVALID_TITLE_ID</code>.

```csharp
public const int TrueSkillInvalidTitleId = -1994172606
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillJobAlreadyExists"></a> TrueSkillJobAlreadyExists

<code>E_PF_TRUE_SKILL_JOB_ALREADY_EXISTS</code>.

```csharp
public const int TrueSkillJobAlreadyExists = -1994172585
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillJobNotFound"></a> TrueSkillJobNotFound

<code>E_PF_TRUE_SKILL_JOB_NOT_FOUND</code>.

```csharp
public const int TrueSkillJobNotFound = -1994172584
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillMatchResultAlreadySubmitted"></a> TrueSkillMatchResultAlreadySubmitted

<code>E_PF_TRUE_SKILL_MATCH_RESULT_ALREADY_SUBMITTED</code>.

```csharp
public const int TrueSkillMatchResultAlreadySubmitted = -1994172650
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillMatchResultCreated"></a> TrueSkillMatchResultCreated

<code>E_PF_TRUE_SKILL_MATCH_RESULT_CREATED</code>.

```csharp
public const int TrueSkillMatchResultCreated = -1994172596
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillMissingBuildVerison"></a> TrueSkillMissingBuildVerison

<code>E_PF_TRUE_SKILL_MISSING_BUILD_VERISON</code>.

```csharp
public const int TrueSkillMissingBuildVerison = -1994172586
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillMissingRequiredCondition"></a> TrueSkillMissingRequiredCondition

<code>E_PF_TRUE_SKILL_MISSING_REQUIRED_CONDITION</code>.

```csharp
public const int TrueSkillMissingRequiredCondition = -1994172628
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillMissingRequiredEvent"></a> TrueSkillMissingRequiredEvent

<code>E_PF_TRUE_SKILL_MISSING_REQUIRED_EVENT</code>.

```csharp
public const int TrueSkillMissingRequiredEvent = -1994172627
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillModelIsNotActive"></a> TrueSkillModelIsNotActive

<code>E_PF_TRUE_SKILL_MODEL_IS_NOT_ACTIVE</code>.

```csharp
public const int TrueSkillModelIsNotActive = -1994172614
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillModelStateInvalidForOperation"></a> TrueSkillModelStateInvalidForOperation

<code>E_PF_TRUE_SKILL_MODEL_STATE_INVALID_FOR_OPERATION</code>.

```csharp
public const int TrueSkillModelStateInvalidForOperation = -1994172531
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillNoModelInScenario"></a> TrueSkillNoModelInScenario

<code>E_PF_TRUE_SKILL_NO_MODEL_IN_SCENARIO</code>.

```csharp
public const int TrueSkillNoModelInScenario = -1994172616
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillNoPlayerInMatchResultTeam"></a> TrueSkillNoPlayerInMatchResultTeam

<code>E_PF_TRUE_SKILL_NO_PLAYER_IN_MATCH_RESULT_TEAM</code>.

```csharp
public const int TrueSkillNoPlayerInMatchResultTeam = -1994172624
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillNoTeamInMatchResult"></a> TrueSkillNoTeamInMatchResult

<code>E_PF_TRUE_SKILL_NO_TEAM_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillNoTeamInMatchResult = -1994172619
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillNoWinnerInMatchResult"></a> TrueSkillNoWinnerInMatchResult

<code>E_PF_TRUE_SKILL_NO_WINNER_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillNoWinnerInMatchResult = -1994172647
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillNotEnoughTeamsInMatchResult"></a> TrueSkillNotEnoughTeamsInMatchResult

<code>E_PF_TRUE_SKILL_NOT_ENOUGH_TEAMS_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillNotEnoughTeamsInMatchResult = -1994172618
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillNotSupportedForTitle"></a> TrueSkillNotSupportedForTitle

<code>E_PF_TRUE_SKILL_NOT_SUPPORTED_FOR_TITLE</code>.

```csharp
public const int TrueSkillNotSupportedForTitle = -1994172615
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillOperationCanceled"></a> TrueSkillOperationCanceled

<code>E_PF_TRUE_SKILL_OPERATION_CANCELED</code>.

```csharp
public const int TrueSkillOperationCanceled = -1994172583
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillPlayersInMatchResultExceedingLimit"></a> TrueSkillPlayersInMatchResultExceedingLimit

<code>E_PF_TRUE_SKILL_PLAYERS_IN_MATCH_RESULT_EXCEEDING_LIMIT</code>.

```csharp
public const int TrueSkillPlayersInMatchResultExceedingLimit = -1994172623
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillScenarioConfigDoesNotExist"></a> TrueSkillScenarioConfigDoesNotExist

<code>E_PF_TRUE_SKILL_SCENARIO_CONFIG_DOES_NOT_EXIST</code>.

```csharp
public const int TrueSkillScenarioConfigDoesNotExist = -1994172617
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillScenarioContainsActiveModel"></a> TrueSkillScenarioContainsActiveModel

<code>E_PF_TRUE_SKILL_SCENARIO_CONTAINS_ACTIVE_MODEL</code>.

```csharp
public const int TrueSkillScenarioContainsActiveModel = -1994172530
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillStartTimeMissingInMatchResult"></a> TrueSkillStartTimeMissingInMatchResult

<code>E_PF_TRUE_SKILL_START_TIME_MISSING_IN_MATCH_RESULT</code>.

```csharp
public const int TrueSkillStartTimeMissingInMatchResult = -1994172593
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillTotalModelLimitExceeded"></a> TrueSkillTotalModelLimitExceeded

<code>E_PF_TRUE_SKILL_TOTAL_MODEL_LIMIT_EXCEEDED</code>.

```csharp
public const int TrueSkillTotalModelLimitExceeded = -1994172579
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillTotalScenarioLimitExceeded"></a> TrueSkillTotalScenarioLimitExceeded

<code>E_PF_TRUE_SKILL_TOTAL_SCENARIO_LIMIT_EXCEEDED</code>.

```csharp
public const int TrueSkillTotalScenarioLimitExceeded = -1994172514
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillTrueSkillPlayerNull"></a> TrueSkillTrueSkillPlayerNull

<code>E_PF_TRUE_SKILL_TRUE_SKILL_PLAYER_NULL</code>.

```csharp
public const int TrueSkillTrueSkillPlayerNull = -1994172549
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillUnauthorized"></a> TrueSkillUnauthorized

<code>E_PF_TRUE_SKILL_UNAUTHORIZED</code>.

```csharp
public const int TrueSkillUnauthorized = -1994172657
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillUnauthorizedForJob"></a> TrueSkillUnauthorizedForJob

<code>E_PF_TRUE_SKILL_UNAUTHORIZED_FOR_JOB</code>.

```csharp
public const int TrueSkillUnauthorizedForJob = -1994172577
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillUnauthorizedToQueryOtherPlayerSkills"></a> TrueSkillUnauthorizedToQueryOtherPlayerSkills

<code>E_PF_TRUE_SKILL_UNAUTHORIZED_TO_QUERY_OTHER_PLAYER_SKILLS</code>.

```csharp
public const int TrueSkillUnauthorizedToQueryOtherPlayerSkills = -1994172613
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillUnknownConditionKey"></a> TrueSkillUnknownConditionKey

<code>E_PF_TRUE_SKILL_UNKNOWN_CONDITION_KEY</code>.

```csharp
public const int TrueSkillUnknownConditionKey = -1994172625
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillUnknownConditionValue"></a> TrueSkillUnknownConditionValue

<code>E_PF_TRUE_SKILL_UNKNOWN_CONDITION_VALUE</code>.

```csharp
public const int TrueSkillUnknownConditionValue = -1994172642
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillUnknownEventName"></a> TrueSkillUnknownEventName

<code>E_PF_TRUE_SKILL_UNKNOWN_EVENT_NAME</code>.

```csharp
public const int TrueSkillUnknownEventName = -1994172626
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillUnknownInitialModelId"></a> TrueSkillUnknownInitialModelId

<code>E_PF_TRUE_SKILL_UNKNOWN_INITIAL_MODEL_ID</code>.

```csharp
public const int TrueSkillUnknownInitialModelId = -1994172578
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TrueSkillUnknownModelId"></a> TrueSkillUnknownModelId

<code>E_PF_TRUE_SKILL_UNKNOWN_MODEL_ID</code>.

```csharp
public const int TrueSkillUnknownModelId = -1994172640
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TwitchResponseError"></a> TwitchResponseError

<code>E_PF_TWITCH_RESPONSE_ERROR</code>.

```csharp
public const int TwitchResponseError = -1994173185
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_TwoFactorAuthenticationTokenRequired"></a> TwoFactorAuthenticationTokenRequired

<code>E_PF_TWO_FACTOR_AUTHENTICATION_TOKEN_REQUIRED</code>.

```csharp
public const int TwoFactorAuthenticationTokenRequired = -1994173172
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UnableToConnectToDatabase"></a> UnableToConnectToDatabase

<code>E_PF_UNABLE_TO_CONNECT_TO_DATABASE</code>.

```csharp
public const int UnableToConnectToDatabase = -1994173308
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UnknownError"></a> UnknownError

<code>E_PF_UNKNOWN_ERROR</code>.

```csharp
public const int UnknownError = -1994173368
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UnkownError"></a> UnkownError

<code>E_PF_UNKOWN_ERROR</code>.

```csharp
public const int UnkownError = -1994173408
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UnsupportedEntityType"></a> UnsupportedEntityType

<code>E_PF_UNSUPPORTED_ENTITY_TYPE</code>.

```csharp
public const int UnsupportedEntityType = -1994172464
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UpdateInventoryRateLimitExceeded"></a> UpdateInventoryRateLimitExceeded

<code>E_PF_UPDATE_INVENTORY_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int UpdateInventoryRateLimitExceeded = -1994172965
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UpdateSegmentRateLimitExceeded"></a> UpdateSegmentRateLimitExceeded

<code>E_PF_UPDATE_SEGMENT_RATE_LIMIT_EXCEEDED</code>.

```csharp
public const int UpdateSegmentRateLimitExceeded = -1994172734
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UpdatingStatisticsUsingTransactionIdNotAvailableForFreeTier"></a> UpdatingStatisticsUsingTransactionIdNotAvailableForFreeTier

<code>E_PF_UPDATING_STATISTICS_USING_TRANSACTION_ID_NOT_AVAILABLE_FOR_FREE_TIER</code>.

```csharp
public const int UpdatingStatisticsUsingTransactionIdNotAvailableForFreeTier = -1994172526
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UserAlreadyAdded"></a> UserAlreadyAdded

<code>E_PF_USER_ALREADY_ADDED</code>.

```csharp
public const int UserAlreadyAdded = -1994173183
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UserIsNotPartOfDeveloper"></a> UserIsNotPartOfDeveloper

<code>E_PF_USER_IS_NOT_PART_OF_DEVELOPER</code>.

```csharp
public const int UserIsNotPartOfDeveloper = -1994173380
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UserNotFriend"></a> UserNotFriend

<code>E_PF_USER_NOT_FRIEND</code>.

```csharp
public const int UserNotFriend = -1994173146
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UserisNotValid"></a> UserisNotValid

<code>E_PF_USERIS_NOT_VALID</code>.

```csharp
public const int UserisNotValid = -1994173377
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UsernameNotAvailable"></a> UsernameNotAvailable

<code>E_PF_USERNAME_NOT_AVAILABLE</code>.

```csharp
public const int UsernameNotAvailable = -1994173398
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_UsersAlreadyFriends"></a> UsersAlreadyFriends

<code>E_PF_USERS_ALREADY_FRIENDS</code>.

```csharp
public const int UsersAlreadyFriends = -1994173235
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_ValueAlreadyExists"></a> ValueAlreadyExists

<code>E_PF_VALUE_ALREADY_EXISTS</code>.

```csharp
public const int ValueAlreadyExists = -1994173376
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VariableNotDefined"></a> VariableNotDefined

<code>E_PF_VARIABLE_NOT_DEFINED</code>.

```csharp
public const int VariableNotDefined = -1994173075
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VersionConfigurationCannotBeSpecifiedForLinkedStat"></a> VersionConfigurationCannotBeSpecifiedForLinkedStat

<code>E_PF_VERSION_CONFIGURATION_CANNOT_BE_SPECIFIED_FOR_LINKED_STAT</code>.

```csharp
public const int VersionConfigurationCannotBeSpecifiedForLinkedStat = -1994172480
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VersionConfigurationIsRequired"></a> VersionConfigurationIsRequired

<code>E_PF_VERSION_CONFIGURATION_IS_REQUIRED</code>.

```csharp
public const int VersionConfigurationIsRequired = -1994172479
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VersionIncrementRateExceeded"></a> VersionIncrementRateExceeded

<code>E_PF_VERSION_INCREMENT_RATE_EXCEEDED</code>.

```csharp
public const int VersionIncrementRateExceeded = -1994172518
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VersionNotFound"></a> VersionNotFound

<code>E_PF_VERSION_NOT_FOUND</code>.

```csharp
public const int VersionNotFound = -1994173294
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyBetaCreateError"></a> VirtualCurrencyBetaCreateError

<code>E_PF_VIRTUAL_CURRENCY_BETA_CREATE_ERROR</code>.

```csharp
public const int VirtualCurrencyBetaCreateError = -1994172945
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyBetaDeleteError"></a> VirtualCurrencyBetaDeleteError

<code>E_PF_VIRTUAL_CURRENCY_BETA_DELETE_ERROR</code>.

```csharp
public const int VirtualCurrencyBetaDeleteError = -1994172942
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyBetaGetError"></a> VirtualCurrencyBetaGetError

<code>E_PF_VIRTUAL_CURRENCY_BETA_GET_ERROR</code>.

```csharp
public const int VirtualCurrencyBetaGetError = -1994172946
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyBetaInitialDepositSaveError"></a> VirtualCurrencyBetaInitialDepositSaveError

<code>E_PF_VIRTUAL_CURRENCY_BETA_INITIAL_DEPOSIT_SAVE_ERROR</code>.

```csharp
public const int VirtualCurrencyBetaInitialDepositSaveError = -1994172944
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyBetaRestoreError"></a> VirtualCurrencyBetaRestoreError

<code>E_PF_VIRTUAL_CURRENCY_BETA_RESTORE_ERROR</code>.

```csharp
public const int VirtualCurrencyBetaRestoreError = -1994172941
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyBetaSaveConflict"></a> VirtualCurrencyBetaSaveConflict

<code>E_PF_VIRTUAL_CURRENCY_BETA_SAVE_CONFLICT</code>.

```csharp
public const int VirtualCurrencyBetaSaveConflict = -1994172940
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyBetaSaveError"></a> VirtualCurrencyBetaSaveError

<code>E_PF_VIRTUAL_CURRENCY_BETA_SAVE_ERROR</code>.

```csharp
public const int VirtualCurrencyBetaSaveError = -1994172943
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyBetaUpdateError"></a> VirtualCurrencyBetaUpdateError

<code>E_PF_VIRTUAL_CURRENCY_BETA_UPDATE_ERROR</code>.

```csharp
public const int VirtualCurrencyBetaUpdateError = -1994172939
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyCannotBeDeleted"></a> VirtualCurrencyCannotBeDeleted

<code>E_PF_VIRTUAL_CURRENCY_CANNOT_BE_DELETED</code>.

```csharp
public const int VirtualCurrencyCannotBeDeleted = -1994173181
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyCannotBeSetToOlderVersion"></a> VirtualCurrencyCannotBeSetToOlderVersion

<code>E_PF_VIRTUAL_CURRENCY_CANNOT_BE_SET_TO_OLDER_VERSION</code>.

```csharp
public const int VirtualCurrencyCannotBeSetToOlderVersion = -1994173016
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyCodeExists"></a> VirtualCurrencyCodeExists

<code>E_PF_VIRTUAL_CURRENCY_CODE_EXISTS</code>.

```csharp
public const int VirtualCurrencyCodeExists = -1994173188
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyCountLimitExceeded"></a> VirtualCurrencyCountLimitExceeded

<code>E_PF_VIRTUAL_CURRENCY_COUNT_LIMIT_EXCEEDED</code>.

```csharp
public const int VirtualCurrencyCountLimitExceeded = -1994173189
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyCurrentlyUnavailable"></a> VirtualCurrencyCurrentlyUnavailable

<code>E_PF_VIRTUAL_CURRENCY_CURRENTLY_UNAVAILABLE</code>.

```csharp
public const int VirtualCurrencyCurrentlyUnavailable = -1994172908
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_VirtualCurrencyMustBeWithinIntegerRange"></a> VirtualCurrencyMustBeWithinIntegerRange

<code>E_PF_VIRTUAL_CURRENCY_MUST_BE_WITHIN_INTEGER_RANGE</code>.

```csharp
public const int VirtualCurrencyMustBeWithinIntegerRange = -1994173015
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_WasNotCreatedWithCloudRoot"></a> WasNotCreatedWithCloudRoot

<code>E_PF_WAS_NOT_CREATED_WITH_CLOUD_ROOT</code>.

```csharp
public const int WasNotCreatedWithCloudRoot = -1994172910
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_WriteAttemptedDuringExport"></a> WriteAttemptedDuringExport

<code>E_PF_WRITE_ATTEMPTED_DURING_EXPORT</code>.

```csharp
public const int WriteAttemptedDuringExport = -1994172976
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_WrongPrice"></a> WrongPrice

<code>E_PF_WRONG_PRICE</code>.

```csharp
public const int WrongPrice = -1994173354
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_WrongSteamAccount"></a> WrongSteamAccount

<code>E_PF_WRONG_STEAM_ACCOUNT</code>.

```csharp
public const int WrongSteamAccount = -1994173366
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_WrongVirtualCurrency"></a> WrongVirtualCurrency

<code>E_PF_WRONG_VIRTUAL_CURRENCY</code>.

```csharp
public const int WrongVirtualCurrency = -1994173355
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_XboxBpCertificateFailure"></a> XboxBpCertificateFailure

<code>E_PF_XBOX_BP_CERTIFICATE_FAILURE</code>.

```csharp
public const int XboxBpCertificateFailure = -1994173114
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_XboxInaccessible"></a> XboxInaccessible

<code>E_PF_XBOX_INACCESSIBLE</code>.

```csharp
public const int XboxInaccessible = -1994173080
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_XboxRejectedXstsExchangeRequest"></a> XboxRejectedXstsExchangeRequest

<code>E_PF_XBOX_REJECTED_XSTS_EXCHANGE_REQUEST</code>.

```csharp
public const int XboxRejectedXstsExchangeRequest = -1994173076
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_XboxServiceTooManyRequests"></a> XboxServiceTooManyRequests

<code>E_PF_XBOX_SERVICE_TOO_MANY_REQUESTS</code>.

```csharp
public const int XboxServiceTooManyRequests = -1994172915
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GDK_Net_PlayFab_PlayFabErrors_XboxXassExchangeFailure"></a> XboxXassExchangeFailure

<code>E_PF_XBOX_XASS_EXCHANGE_FAILURE</code>.

```csharp
public const int XboxXassExchangeFailure = -1994173113
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Methods

### <a id="GDK_Net_PlayFab_PlayFabErrors_GetName_System_Int32_"></a> GetName\(int\)

Returns the <code>E_PF_*</code> symbol for a code, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when this
GDK edition does not define one.

```csharp
public static string? GetName(int hresult)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="GDK_Net_PlayFab_PlayFabErrors_IsPlayFab_System_Int32_"></a> IsPlayFab\(int\)

Whether <code class="paramref">hresult</code> is in the facility PlayFab and libHttpClient
share (<code>FACILITY_XBOX</code> + <code>0x23</code>).

```csharp
public static bool IsPlayFab(int hresult)
```

#### Parameters

`hresult` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

