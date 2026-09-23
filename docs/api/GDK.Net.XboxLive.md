# <a id="GDK_Net_XboxLive"></a> Namespace GDK.Net.XboxLive

### Classes

 [Achievement](GDK.Net.XboxLive.Achievement.md)

One Xbox Live achievement. Managed snapshot of <code>XblAchievement</code>.

 [AchievementMediaAsset](GDK.Net.XboxLive.AchievementMediaAsset.md)

A media asset attached to an achievement. Mirrors <code>XblAchievementMediaAsset</code>.

 [AchievementProgressChange](GDK.Net.XboxLive.AchievementProgressChange.md)

One achievement whose progress changed. Mirrors <code>XblAchievementProgressChangeEntry</code>.

 [AchievementProgressChangedEventArgs](GDK.Net.XboxLive.AchievementProgressChangedEventArgs.md)

Payload for <xref href="GDK.Net.XboxLive.AchievementsService.ProgressChanged" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>XblAchievementProgressChangeEventArgs</code>.

 [AchievementProgression](GDK.Net.XboxLive.AchievementProgression.md)

Progress towards an achievement. Mirrors <code>XblAchievementProgression</code>.

 [AchievementRequirement](GDK.Net.XboxLive.AchievementRequirement.md)

One requirement that makes up an achievement's progression. Mirrors
<code>XblAchievementRequirement</code>.

 [AchievementReward](GDK.Net.XboxLive.AchievementReward.md)

A reward granted for unlocking an achievement. Mirrors <code>XblAchievementReward</code>.

 [AchievementTitleAssociation](GDK.Net.XboxLive.AchievementTitleAssociation.md)

An achievement's association with a title. Mirrors <code>XblAchievementTitleAssociation</code>.

 [AchievementsManager](GDK.Net.XboxLive.AchievementsManager.md)

Process-global Xbox Live achievements manager. Mirrors <code>achievements_manager_c.h</code>.

 [AchievementsManagerAchievementProgressUpdatedEvent](GDK.Net.XboxLive.AchievementsManagerAchievementProgressUpdatedEvent.md)

An achievement's progress changed for a local user.

 [AchievementsManagerAchievementUnlockedEvent](GDK.Net.XboxLive.AchievementsManagerAchievementUnlockedEvent.md)

An achievement was unlocked for a local user.

 [AchievementsManagerEvent](GDK.Net.XboxLive.AchievementsManagerEvent.md)

One event returned by <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref>.

 [AchievementsManagerLocalUserInitialStateSyncedEvent](GDK.Net.XboxLive.AchievementsManagerLocalUserInitialStateSyncedEvent.md)

A local user's initial achievement cache has finished syncing.

 [AchievementsManagerResult](GDK.Net.XboxLive.AchievementsManagerResult.md)

The result of a synchronous achievements-manager cache query.

 [AchievementsPage](GDK.Net.XboxLive.AchievementsPage.md)

One page of achievements, plus the means to fetch the next. Wraps
<code>XblAchievementsResultHandle</code>.

 [AchievementsService](GDK.Net.XboxLive.AchievementsService.md)

Achievement queries and progress updates. Reached through
<xref href="GDK.Net.XboxLive.XboxLiveContext.Achievements" data-throw-if-not-resolved="false"></xref>. Mirrors <code>achievements_c.h</code>.

 [DevicePresenceChangedEventArgs](GDK.Net.XboxLive.DevicePresenceChangedEventArgs.md)

Payload for <xref href="GDK.Net.XboxLive.PresenceService.DevicePresenceChanged" data-throw-if-not-resolved="false"></xref>.

 [EventsService](GDK.Net.XboxLive.EventsService.md)

Xbox Live telemetry event writing. Mirrors <code>events_c.h</code>.

 [LeaderboardColumn](GDK.Net.XboxLive.LeaderboardColumn.md)

A column returned in a leaderboard page. Managed snapshot of <code>XblLeaderboardColumn</code>.

 [LeaderboardPage](GDK.Net.XboxLive.LeaderboardPage.md)

One page of leaderboard results, plus the means to fetch the next. Managed wrapper for a
caller-allocated <code>XblLeaderboardResult</code> buffer.

 [LeaderboardQuery](GDK.Net.XboxLive.LeaderboardQuery.md)

Parameters for an Xbox Live leaderboard query. Managed equivalent of
<code>XblLeaderboardQuery</code>.

 [LeaderboardRow](GDK.Net.XboxLive.LeaderboardRow.md)

One row returned in a leaderboard page. Managed snapshot of <code>XblLeaderboardRow</code>.

 [LeaderboardService](GDK.Net.XboxLive.LeaderboardService.md)

Xbox Live leaderboard queries. Mirrors <code>leaderboard_c.h</code>.

 [LocalUserAddedSocialManagerEvent](GDK.Net.XboxLive.LocalUserAddedSocialManagerEvent.md)

A local user's initial social graph finished loading.

 [MultiplayerActivityInfo](GDK.Net.XboxLive.MultiplayerActivityInfo.md)

Multiplayer activity information. Managed snapshot of <code>XblMultiplayerActivityInfo</code>.

 [MultiplayerActivityRecentPlayerUpdate](GDK.Net.XboxLive.MultiplayerActivityRecentPlayerUpdate.md)

One recent-player encounter to append to the local user's recent-player list. Managed
equivalent of <code>XblMultiplayerActivityRecentPlayerUpdate</code>.

 [MultiplayerActivityService](GDK.Net.XboxLive.MultiplayerActivityService.md)

Xbox Live multiplayer activity, invites and recent-player reporting. Mirrors
<code>multiplayer_activity_c.h</code>.

 [PresenceBroadcastRecord](GDK.Net.XboxLive.PresenceBroadcastRecord.md)

Broadcast details attached to a title presence record. Managed snapshot of <code>XblPresenceBroadcastRecord</code>.

 [PresenceChangedSocialManagerEvent](GDK.Net.XboxLive.PresenceChangedSocialManagerEvent.md)

One or more users' presence changed.

 [PresenceDeviceRecord](GDK.Net.XboxLive.PresenceDeviceRecord.md)

Presence for one device. Managed snapshot of <code>XblPresenceDeviceRecord</code>.

 [PresenceQueryFilters](GDK.Net.XboxLive.PresenceQueryFilters.md)

Filters for batch and social-group presence queries. Mirrors <code>XblPresenceQueryFilters</code>.

 [PresenceRecord](GDK.Net.XboxLive.PresenceRecord.md)

A user's Xbox Live presence. Managed snapshot of <code>XblPresenceRecordHandle</code>.

 [PresenceRichPresenceIds](GDK.Net.XboxLive.PresenceRichPresenceIds.md)

Rich presence string identifiers supplied to <xref href="GDK.Net.XboxLive.PresenceService.SetPresenceAsync(System.Boolean%2cGDK.Net.XboxLive.PresenceRichPresenceIds%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

 [PresenceService](GDK.Net.XboxLive.PresenceService.md)

Xbox Live presence queries, rich presence updates and real-time presence notifications.
Mirrors <code>presence_c.h</code>.

 [PresenceTitleRecord](GDK.Net.XboxLive.PresenceTitleRecord.md)

Presence for one title on a device. Managed snapshot of <code>XblPresenceTitleRecord</code>.

 [PrivacyPermissionCheckResult](GDK.Net.XboxLive.PrivacyPermissionCheckResult.md)

Managed result of an Xbox Live privacy permission check.

 [PrivacyPermissionDenyReasonDetail](GDK.Net.XboxLive.PrivacyPermissionDenyReasonDetail.md)

Detailed policy reason for a denied Xbox Live privacy permission check.

 [PrivacyService](GDK.Net.XboxLive.PrivacyService.md)

Xbox Live privacy permission checks and privacy lists. Mirrors <code>privacy_c.h</code>.

 [ProfileService](GDK.Net.XboxLive.ProfileService.md)

Xbox Live profile lookups. Reached through <xref href="GDK.Net.XboxLive.XboxLiveContext.Profiles" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>profile_c.h</code>.

 [ProfilesChangedSocialManagerEvent](GDK.Net.XboxLive.ProfilesChangedSocialManagerEvent.md)

One or more users' profile fields changed.

 [RealTimeActivityConnectionStateChangedEventArgs](GDK.Net.XboxLive.RealTimeActivityConnectionStateChangedEventArgs.md)

Payload for <xref href="GDK.Net.XboxLive.RealTimeActivityService.ConnectionStateChanged" data-throw-if-not-resolved="false"></xref>.

 [RealTimeActivityService](GDK.Net.XboxLive.RealTimeActivityService.md)

Real-time activity websocket lifetime and notifications for an <xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref>.
Mirrors <code>real_time_activity_c.h</code>.

 [ReputationFeedbackItem](GDK.Net.XboxLive.ReputationFeedbackItem.md)

One reputation feedback item for batch submission. Mirrors <code>XblReputationFeedbackItem</code>.

 [RequestedStatistics](GDK.Net.XboxLive.RequestedStatistics.md)

Statistics requested for one service configuration in a multi-SCID batch. Mirrors
<code>XblRequestedStatistics</code> without exposing native buffers.

 [ServiceConfigurationStatistic](GDK.Net.XboxLive.ServiceConfigurationStatistic.md)

The statistics returned for a single service configuration. Managed snapshot of
<code>XblServiceConfigurationStatistic</code>.

 [SocialManager](GDK.Net.XboxLive.SocialManager.md)

Process-global Xbox Live social manager. Mirrors <code>social_manager_c.h</code>.

 [SocialManagerEvent](GDK.Net.XboxLive.SocialManagerEvent.md)

Base record for one snapshotted <xref href="GDK.Net.XboxLive.SocialManager.DoWork" data-throw-if-not-resolved="false"></xref> event.

 [SocialManagerPreferredColor](GDK.Net.XboxLive.SocialManagerPreferredColor.md)

Preferred shell colors attached to a social-manager user.

 [SocialManagerPresenceRecord](GDK.Net.XboxLive.SocialManagerPresenceRecord.md)

A social-manager presence snapshot for one user.

 [SocialManagerPresenceTitleRecord](GDK.Net.XboxLive.SocialManagerPresenceTitleRecord.md)

One title presence record in a social-manager presence snapshot.

 [SocialManagerTitleHistory](GDK.Net.XboxLive.SocialManagerTitleHistory.md)

Title-history data attached to a social-manager user.

 [SocialManagerUser](GDK.Net.XboxLive.SocialManagerUser.md)

An Xbox user in the social-manager graph. Managed snapshot of <code>XblSocialManagerUser</code>.

 [SocialManagerUserGroup](GDK.Net.XboxLive.SocialManagerUserGroup.md)

A long-lived social-manager user group backed by an XSAPI native handle.

 [SocialManagerUserGroupFilters](GDK.Net.XboxLive.SocialManagerUserGroupFilters.md)

Filters associated with a filter-backed <xref href="GDK.Net.XboxLive.SocialManagerUserGroup" data-throw-if-not-resolved="false"></xref>.

 [SocialMultiplayerSessionReference](GDK.Net.XboxLive.SocialMultiplayerSessionReference.md)

Identifies an MPSD session related to reputation feedback. Mirrors
<code>XblMultiplayerSessionReference</code>.

 [SocialRelationship](GDK.Net.XboxLive.SocialRelationship.md)

Represents the relationship between the signed-in user and another Xbox user.

 [SocialRelationshipChangedEventArgs](GDK.Net.XboxLive.SocialRelationshipChangedEventArgs.md)

Payload for <xref href="GDK.Net.XboxLive.SocialService.RelationshipChanged" data-throw-if-not-resolved="false"></xref>.

 [SocialRelationshipsChangedSocialManagerEvent](GDK.Net.XboxLive.SocialRelationshipsChangedSocialManagerEvent.md)

One or more users' social relationships changed.

 [SocialRelationshipsPage](GDK.Net.XboxLive.SocialRelationshipsPage.md)

One page of social relationships, plus the means to fetch the next. Wraps
<code>XblSocialRelationshipResultHandle</code>.

 [SocialService](GDK.Net.XboxLive.SocialService.md)

Social graph queries, reputation feedback and relationship-change notifications. Mirrors
<code>social_c.h</code>.

 [SocialUserGroupLoadedSocialManagerEvent](GDK.Net.XboxLive.SocialUserGroupLoadedSocialManagerEvent.md)

A social user group's initial tracked set finished loading.

 [SocialUserGroupUpdatedSocialManagerEvent](GDK.Net.XboxLive.SocialUserGroupUpdatedSocialManagerEvent.md)

A list-backed social user group finished updating.

 [Statistic](GDK.Net.XboxLive.Statistic.md)

One Xbox Live user statistic. Managed snapshot of <code>XblStatistic</code>.

 [StatisticChangedEventArgs](GDK.Net.XboxLive.StatisticChangedEventArgs.md)

Payload for <xref href="GDK.Net.XboxLive.UserStatisticsService.StatisticChanged" data-throw-if-not-resolved="false"></xref>. Managed snapshot of
<code>XblStatisticChangeEventArgs</code>.

 [StringVerificationResult](GDK.Net.XboxLive.StringVerificationResult.md)

Managed result of verifying one user-generated string with Xbox Live.

 [StringVerificationService](GDK.Net.XboxLive.StringVerificationService.md)

Xbox Live user-generated text verification. Mirrors <code>string_verify_c.h</code>.

 [TitleManagedStatistic](GDK.Net.XboxLive.TitleManagedStatistic.md)

A title-managed statistic to write, update or delete.

 [TitleManagedStatisticsService](GDK.Net.XboxLive.TitleManagedStatisticsService.md)

Writes title-managed statistics for the signed-in user. Mirrors
<code>title_managed_statistics_c.h</code>.

 [TitlePresenceChangedEventArgs](GDK.Net.XboxLive.TitlePresenceChangedEventArgs.md)

Payload for <xref href="GDK.Net.XboxLive.PresenceService.TitlePresenceChanged" data-throw-if-not-resolved="false"></xref>.

 [TitleStorageBlobDownloadResult](GDK.Net.XboxLive.TitleStorageBlobDownloadResult.md)

Blob bytes returned by a title storage download, plus the service metadata.

 [TitleStorageBlobMetadata](GDK.Net.XboxLive.TitleStorageBlobMetadata.md)

Managed snapshot of <code>XblTitleStorageBlobMetadata</code>.

 [TitleStorageBlobMetadataPage](GDK.Net.XboxLive.TitleStorageBlobMetadataPage.md)

One page of title storage blob metadata, plus the means to fetch the next. Wraps
<code>XblTitleStorageBlobMetadataResultHandle</code>.

 [TitleStorageService](GDK.Net.XboxLive.TitleStorageService.md)

Xbox Live title storage quota, metadata and blob transfer operations.

 [UnknownSocialManagerEvent](GDK.Net.XboxLive.UnknownSocialManagerEvent.md)

An event kind unknown to this projection was returned by XSAPI.

 [UserProfile](GDK.Net.XboxLive.UserProfile.md)

A user's Xbox Live profile. Managed snapshot of <code>XblUserProfile</code>.

 [UserStatisticsResult](GDK.Net.XboxLive.UserStatisticsResult.md)

Statistics returned for one Xbox user. Managed snapshot of <code>XblUserStatisticsResult</code>.

 [UserStatisticsService](GDK.Net.XboxLive.UserStatisticsService.md)

Xbox Live user statistic queries and real-time statistic change notifications. Mirrors
<code>user_statistics_c.h</code>.

 [UsersAddedToSocialGraphSocialManagerEvent](GDK.Net.XboxLive.UsersAddedToSocialGraphSocialManagerEvent.md)

One or more users were added to the social graph.

 [UsersRemovedFromSocialGraphSocialManagerEvent](GDK.Net.XboxLive.UsersRemovedFromSocialGraphSocialManagerEvent.md)

One or more users were removed from the social graph.

 [XboxLiveContext](GDK.Net.XboxLive.XboxLiveContext.md)

An Xbox Live context: the per-user, per-sign-in handle every Xbox Live service call is made
through. Wraps <code>XblContextHandle</code>.

 [XboxLiveContextSettings](GDK.Net.XboxLive.XboxLiveContextSettings.md)

HTTP and websocket tuning for one <xref href="GDK.Net.XboxLive.XboxLiveContext" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>xbox_live_context_settings_c.h</code>.

 [XboxLiveErrors](GDK.Net.XboxLive.XboxLiveErrors.md)

Helpers for mapping Xbox Live HRESULTs to actionable error conditions.

 [XboxLiveOptions](GDK.Net.XboxLive.XboxLiveOptions.md)

Options for <xref href="GDK.Net.XboxLive.XboxLiveService.Initialize(GDK.Net.XboxLive.XboxLiveOptions)" data-throw-if-not-resolved="false"></xref>. Mirrors
<code>XblInitArgs</code> as it is declared for a GDK title.

 [XboxLiveService](GDK.Net.XboxLive.XboxLiveService.md)

Xbox Live Services (XSAPI). Reached through <xref href="GDK.Net.GameRuntime.XboxLive" data-throw-if-not-resolved="false"></xref>.

### Structs

 [AchievementTimeWindow](GDK.Net.XboxLive.AchievementTimeWindow.md)

The window a challenge achievement is available in. Mirrors <code>XblAchievementTimeWindow</code>.

 [TitleManagedStatisticValue](GDK.Net.XboxLive.TitleManagedStatisticValue.md)

Immutable discriminated value for a title-managed statistic.

 [TitleStorageQuota](GDK.Net.XboxLive.TitleStorageQuota.md)

How much title storage quota is used and available, in bytes.

### Enums

 [AchievementMediaAssetType](GDK.Net.XboxLive.AchievementMediaAssetType.md)

Kind of media asset. Mirrors <code>XblAchievementMediaAssetType</code>.

 [AchievementOrderBy](GDK.Net.XboxLive.AchievementOrderBy.md)

Sort order for an achievement query. Mirrors <code>XblAchievementOrderBy</code>.

 [AchievementParticipationType](GDK.Net.XboxLive.AchievementParticipationType.md)

How an achievement is earned. Mirrors <code>XblAchievementParticipationType</code>.

 [AchievementProgressState](GDK.Net.XboxLive.AchievementProgressState.md)

A player's progress towards an achievement. Mirrors <code>XblAchievementProgressState</code>.

 [AchievementRarityCategory](GDK.Net.XboxLive.AchievementRarityCategory.md)

How rare an achievement is. Mirrors <code>XblAchievementRarityCategory</code>.

 [AchievementRewardType](GDK.Net.XboxLive.AchievementRewardType.md)

Kind of reward. Mirrors <code>XblAchievementRewardType</code>.

 [AchievementType](GDK.Net.XboxLive.AchievementType.md)

Kind of achievement. Mirrors <code>XblAchievementType</code>.

 [AchievementsManagerEventType](GDK.Net.XboxLive.AchievementsManagerEventType.md)

Kind of event returned by <xref href="GDK.Net.XboxLive.AchievementsManager.DoWork" data-throw-if-not-resolved="false"></xref>.

 [AchievementsManagerSortOrder](GDK.Net.XboxLive.AchievementsManagerSortOrder.md)

Sort direction for cached achievements-manager queries.

 [AnonymousUserType](GDK.Net.XboxLive.AnonymousUserType.md)

Classes of non-Xbox Live users that can be targets of a privacy check.

 [ErrorCondition](GDK.Net.XboxLive.ErrorCondition.md)

Actionable Xbox Live error condition buckets. Mirrors <code>XblErrorCondition</code>.

 [LeaderboardQueryType](GDK.Net.XboxLive.LeaderboardQueryType.md)

The backing store used by a leaderboard query. Mirrors <code>XblLeaderboardQueryType</code>.

 [LeaderboardSortOrder](GDK.Net.XboxLive.LeaderboardSortOrder.md)

The order to sort a leaderboard in. Mirrors <code>XblLeaderboardSortOrder</code>.

 [LeaderboardStatType](GDK.Net.XboxLive.LeaderboardStatType.md)

The data type of a leaderboard statistic. Mirrors <code>XblLeaderboardStatType</code>.

 [MultiplayerActivityEncounterType](GDK.Net.XboxLive.MultiplayerActivityEncounterType.md)

Type of recent-player encounter. Mirrors <code>XblMultiplayerActivityEncounterType</code>.

 [MultiplayerActivityJoinRestriction](GDK.Net.XboxLive.MultiplayerActivityJoinRestriction.md)

Who can join a player's current activity. Mirrors <code>XblMultiplayerActivityJoinRestriction</code>.

 [MultiplayerActivityPlatform](GDK.Net.XboxLive.MultiplayerActivityPlatform.md)

Platform on which an activity is joinable. Mirrors <code>XblMultiplayerActivityPlatform</code>.

 [Permission](GDK.Net.XboxLive.Permission.md)

Actions that Xbox Live can check against privacy and privilege policy.

 [PermissionDenyReason](GDK.Net.XboxLive.PermissionDenyReason.md)

Reasons Xbox Live can report for denying a privacy permission check.

 [PresenceBroadcastProvider](GDK.Net.XboxLive.PresenceBroadcastProvider.md)

Broadcast provider for a presence record. Mirrors <code>XblPresenceBroadcastProvider</code>.

 [PresenceDetailLevel](GDK.Net.XboxLive.PresenceDetailLevel.md)

How much presence detail a query should request. Mirrors <code>XblPresenceDetailLevel</code>.

 [PresenceDeviceType](GDK.Net.XboxLive.PresenceDeviceType.md)

Device family reported by Xbox Live presence. Mirrors <code>XblPresenceDeviceType</code>.

 [PresenceFilter](GDK.Net.XboxLive.PresenceFilter.md)

Presence filter for a social-manager filter group. Mirrors <code>XblPresenceFilter</code>.

 [PresenceMediaIdType](GDK.Net.XboxLive.PresenceMediaIdType.md)

Media identifier type for media presence data. Mirrors <code>XblPresenceMediaIdType</code>.

 [PresenceSocialGroup](GDK.Net.XboxLive.PresenceSocialGroup.md)

Social group names accepted by <xref href="GDK.Net.XboxLive.PresenceService.GetForSocialGroupAsync(GDK.Net.XboxLive.PresenceSocialGroup%2cSystem.Nullable%7bSystem.UInt64%7d%2cGDK.Net.XboxLive.PresenceQueryFilters%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>.

 [PresenceTitleState](GDK.Net.XboxLive.PresenceTitleState.md)

Title presence transition state. Mirrors <code>XblPresenceTitleState</code>.

 [PresenceTitleViewState](GDK.Net.XboxLive.PresenceTitleViewState.md)

Screen view state for a title presence record. Mirrors <code>XblPresenceTitleViewState</code>.

 [PresenceUserState](GDK.Net.XboxLive.PresenceUserState.md)

A user's aggregate Xbox Live presence state. Mirrors <code>XblPresenceUserState</code>.

 [PrivacySetting](GDK.Net.XboxLive.PrivacySetting.md)

Privacy settings that can restrict an Xbox Live permission check.

 [Privilege](GDK.Net.XboxLive.Privilege.md)

Xbox Live privileges that can restrict a privacy permission check.

 [RealTimeActivityConnectionState](GDK.Net.XboxLive.RealTimeActivityConnectionState.md)

State of the websocket connection to the Xbox Live real-time activity service. Mirrors
<code>XblRealTimeActivityConnectionState</code>.

 [RelationshipFilter](GDK.Net.XboxLive.RelationshipFilter.md)

Relationship filter for a social-manager filter group. Mirrors <code>XblRelationshipFilter</code>.

 [ReputationFeedbackType](GDK.Net.XboxLive.ReputationFeedbackType.md)

Kind of reputation feedback to submit. Mirrors <code>XblReputationFeedbackType</code>.

 [SocialGroup](GDK.Net.XboxLive.SocialGroup.md)

The social groups <xref href="GDK.Net.XboxLive.ProfileService.GetForSocialGroupAsync(GDK.Net.XboxLive.SocialGroup%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref> understands. These are the
only two names the service accepts.

 [SocialGroupType](GDK.Net.XboxLive.SocialGroupType.md)

The social group a leaderboard query is scoped to. Mirrors <code>XblSocialGroupType</code>.

 [SocialManagerEventType](GDK.Net.XboxLive.SocialManagerEventType.md)

Native social-manager event kind. Mirrors <code>XblSocialManagerEventType</code>.

 [SocialManagerExtraDetailLevel](GDK.Net.XboxLive.SocialManagerExtraDetailLevel.md)

Extra social graph detail to load for a local user. Mirrors <code>XblSocialManagerExtraDetailLevel</code>.

 [SocialNotificationType](GDK.Net.XboxLive.SocialNotificationType.md)

Kind of social relationship change. Mirrors <code>XblSocialNotificationType</code>.

 [SocialRelationshipFilter](GDK.Net.XboxLive.SocialRelationshipFilter.md)

Which relationships a social query returns. Mirrors <code>XblSocialRelationshipFilter</code>.

 [SocialUserGroupType](GDK.Net.XboxLive.SocialUserGroupType.md)

How a social-manager user group was created. Mirrors <code>XblSocialUserGroupType</code>.

 [TitleManagedStatType](GDK.Net.XboxLive.TitleManagedStatType.md)

Kind of title-managed statistic value. Mirrors <code>XblTitleManagedStatType</code>.

 [TitleStorageBlobType](GDK.Net.XboxLive.TitleStorageBlobType.md)

The payload format of a title storage blob. Mirrors <code>XblTitleStorageBlobType</code>.

 [TitleStorageETagMatchCondition](GDK.Net.XboxLive.TitleStorageETagMatchCondition.md)

ETag condition used when reading or writing title storage. Mirrors
<code>XblTitleStorageETagMatchCondition</code>.

 [TitleStorageType](GDK.Net.XboxLive.TitleStorageType.md)

Where a title storage blob is stored. Mirrors <code>XblTitleStorageType</code>.

 [VerifyStringResultCode](GDK.Net.XboxLive.VerifyStringResultCode.md)

Result code returned by Xbox Live string verification.

