using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Privacy settings that can restrict an Xbox Live permission check.</summary>
public enum PrivacySetting : uint
{
    /// <summary>The setting is unknown or was not supplied by the service.</summary>
    Unknown = 0,

    /// <summary>Controls sharing the user's friends list.</summary>
    ShareFriendList = 1,

    /// <summary>Controls sharing played game history.</summary>
    ShareGameHistory = 2,

    /// <summary>Controls text and voice communication.</summary>
    CommunicateUsingTextAndVoice = 3,

    /// <summary>Controls sharing online presence.</summary>
    SharePresence = 4,

    /// <summary>Controls sharing the user's profile.</summary>
    ShareProfile = 5,

    /// <summary>Controls sharing video and music status.</summary>
    ShareVideoAndMusicStatus = 6,

    /// <summary>Controls video communication.</summary>
    CommunicateUsingVideo = 7,

    /// <summary>Controls voice data collection.</summary>
    CollectVoiceData = 8,

    /// <summary>Controls sharing Xbox Music activity.</summary>
    ShareXboxMusicActivity = 9,

    /// <summary>Controls sharing exercise and fitness information.</summary>
    ShareExerciseInfo = 11,

    /// <summary>Controls sharing real identity information.</summary>
    ShareIdentity = 12,

    /// <summary>Controls sharing real identity information in games.</summary>
    ShareIdentityInGame = 13,

    /// <summary>Controls sharing recorded game sessions.</summary>
    ShareRecordedGameSessions = 14,

    /// <summary>Controls collecting Live TV viewing data.</summary>
    CollectLiveTvData = 15,

    /// <summary>Controls collecting Xbox Video viewing data.</summary>
    CollectXboxVideoData = 16,

    /// <summary>Controls transitive sharing of real identity information.</summary>
    ShareIdentityTransitively = 17,

    /// <summary>Controls sharing video viewing history.</summary>
    ShareVideoHistory = 18,

    /// <summary>Controls sharing music listening history.</summary>
    ShareMusicHistory = 19,

    /// <summary>Controls whether the user may view user-created content from other users.</summary>
    AllowUserCreatedContentViewing = 20,

    /// <summary>Controls whether the user may view profiles of other users.</summary>
    AllowProfileViewing = 21,

    /// <summary>Controls whether real-time activity is shown.</summary>
    ShowRealTimeActivity = 22,

    /// <summary>Controls collecting full voice data on Xbox One.</summary>
    CollectVoiceDataXboxOneFull = 23,

    /// <summary>Enforcement setting for whether the user may share identity.</summary>
    CanShareIdentity = 24,

    /// <summary>Controls sharing Xbox Live content to external social networks.</summary>
    ShareContentToExternalNetworks = 25,

    /// <summary>Controls collecting voice search data.</summary>
    CollectVoiceSearchData = 26,

    /// <summary>Controls sharing public club membership.</summary>
    ShareClubMembership = 27,

    /// <summary>Controls collecting voice data from game chats.</summary>
    CollectVoiceGameChatData = 28,

    /// <summary>Controls sharing activity feed posts.</summary>
    ShareActivityFeed = 29,

    /// <summary>Controls communication during cross-network play.</summary>
    CommunicateDuringCrossNetworkPlay = 30,
}

/// <summary>Xbox Live privileges that can restrict a privacy permission check.</summary>
public enum Privilege : uint
{
    /// <summary>The privilege is unknown or was not supplied by the service.</summary>
    Unknown = 0,

    /// <summary>Controls whether the user may use in-game voice communication.</summary>
    AllowIngameVoiceCommunications = 205,

    /// <summary>Controls whether the user may communicate by video.</summary>
    AllowVideoCommunications = 235,

    /// <summary>Controls whether the user may view profiles.</summary>
    AllowProfileViewing = 249,

    /// <summary>Controls whether the user may communicate with other users.</summary>
    AllowCommunications = 252,

    /// <summary>Controls whether the user may play multiplayer.</summary>
    AllowMultiplayer = 254,

    /// <summary>Controls whether the user may add friends.</summary>
    AllowAddFriend = 255,
}

/// <summary>Actions that Xbox Live can check against privacy and privilege policy.</summary>
public enum Permission : uint
{
    /// <summary>The permission is unknown.</summary>
    Unknown = 0,

    /// <summary>Send text messages or invitations to a target user.</summary>
    CommunicateUsingText = 1000,

    /// <summary>Use video communication with a target user.</summary>
    CommunicateUsingVideo = 1001,

    /// <summary>Use voice communication with a target user; mute-list restrictions apply.</summary>
    CommunicateUsingVoice = 1002,

    /// <summary>View a target user's profile.</summary>
    ViewTargetProfile = 1004,

    /// <summary>View a target user's game history.</summary>
    ViewTargetGameHistory = 1005,

    /// <summary>View a target user's detailed video watching history.</summary>
    ViewTargetVideoHistory = 1006,

    /// <summary>View a target user's detailed music listening history.</summary>
    ViewTargetMusicHistory = 1007,

    /// <summary>View a target user's exercise information.</summary>
    ViewTargetExerciseInfo = 1009,

    /// <summary>View a target user's online presence.</summary>
    ViewTargetPresence = 1011,

    /// <summary>View details of a target user's video status.</summary>
    ViewTargetVideoStatus = 1012,

    /// <summary>View details of a target user's music status.</summary>
    ViewTargetMusicStatus = 1013,

    /// <summary>Play multiplayer with a target user.</summary>
    PlayMultiplayer = 1014,

    /// <summary>View user-created content produced by a target user.</summary>
    ViewTargetUserCreatedContent = 1018,

    /// <summary>Broadcast sessions on Twitch.</summary>
    BroadcastWithTwitch = 1019,

    /// <summary>Write a comment on an object owned by a target user.</summary>
    WriteComment = 1022,

    /// <summary>Share an item owned by a target user.</summary>
    ShareItem = 1024,

    /// <summary>Share target-owned content to external social networks.</summary>
    ShareTargetContentToExternalNetworks = 1025,
}

/// <summary>Reasons Xbox Live can report for denying a privacy permission check.</summary>
public enum PermissionDenyReason : uint
{
    /// <summary>No specific reason was supplied, or the permission check could not be completed.</summary>
    Unknown = 0,

    /// <summary>The request completed successfully, but the action is not allowed.</summary>
    NotAllowed = 2,

    /// <summary>The requester is missing a privilege required for the action.</summary>
    MissingPrivilege = 3,

    /// <summary>A requester privilege restricts interaction with the target.</summary>
    PrivilegeRestrictsTarget = 4,

    /// <summary>The requester's block list restricts interaction with the target.</summary>
    BlockListRestrictsTarget = 5,

    /// <summary>The requester's mute list restricts interaction with the target.</summary>
    MuteListRestrictsTarget = 7,

    /// <summary>A requester privacy setting restricts interaction with the target.</summary>
    PrivacySettingRestrictsTarget = 9,

    /// <summary>The cross-network target must be an in-game friend before the action is allowed.</summary>
    CrossNetworkUserMustBeFriend = 12,
}

/// <summary>Classes of non-Xbox Live users that can be targets of a privacy check.</summary>
public enum AnonymousUserType : uint
{
    /// <summary>The target is not an anonymous user, or the user type is unknown.</summary>
    Unknown = 0,

    /// <summary>A non-Xbox Live user.</summary>
    CrossNetworkUser = 1,

    /// <summary>A non-Xbox Live user that the title recognizes as an in-game friend.</summary>
    CrossNetworkFriend = 2,
}

/// <summary>Detailed policy reason for a denied Xbox Live privacy permission check.</summary>
public sealed class PrivacyPermissionDenyReasonDetail
{
    internal PrivacyPermissionDenyReasonDetail(
        PermissionDenyReason reason,
        Privilege restrictedPrivilege,
        PrivacySetting restrictedPrivacySetting)
    {
        Reason = reason;
        RestrictedPrivilege = restrictedPrivilege;
        RestrictedPrivacySetting = restrictedPrivacySetting;
    }

    /// <summary>The broad reason the permission was denied.</summary>
    public PermissionDenyReason Reason { get; }

    /// <summary>
    /// The privilege involved when <see cref="Reason"/> is
    /// <see cref="PermissionDenyReason.MissingPrivilege"/> or
    /// <see cref="PermissionDenyReason.PrivilegeRestrictsTarget"/>; otherwise
    /// <see cref="Privilege.Unknown"/>.
    /// </summary>
    public Privilege RestrictedPrivilege { get; }

    /// <summary>
    /// The privacy setting involved when <see cref="Reason"/> is
    /// <see cref="PermissionDenyReason.PrivacySettingRestrictsTarget"/>; otherwise
    /// <see cref="PrivacySetting.Unknown"/>.
    /// </summary>
    public PrivacySetting RestrictedPrivacySetting { get; }

    /// <inheritdoc/>
    public override string ToString() => Reason.ToString();

    internal static PrivacyPermissionDenyReasonDetail Unknown { get; } =
        new(
            PermissionDenyReason.Unknown,
            Privilege.Unknown,
            PrivacySetting.Unknown);

    internal static PrivacyPermissionDenyReasonDetail FromNative(in XblPermissionDenyReasonDetails native) =>
        new(
            (PermissionDenyReason)native.Reason,
            (Privilege)native.RestrictedPrivilege,
            (PrivacySetting)native.RestrictedPrivacySetting);
}

/// <summary>
/// Managed result of an Xbox Live privacy permission check.
/// </summary>
/// <remarks>
/// Privacy gates are a certification-sensitive surface: communications, multiplayer and
/// user-generated content must fail closed. Therefore <see cref="IsAllowed"/> is
/// <see langword="true"/> only when Xbox Live completed the check and explicitly granted the
/// permission. If the native call cannot start, is canceled, or any result-shaping step fails, the
/// projection returns a result with <see cref="WasChecked"/> <see langword="false"/>,
/// <see cref="IsAllowed"/> <see langword="false"/> and an
/// <see cref="PermissionDenyReason.Unknown"/> reason instead of surfacing an exception that
/// a title could accidentally convert into access.
/// </remarks>
public sealed class PrivacyPermissionCheckResult
{
    internal PrivacyPermissionCheckResult(
        bool wasChecked,
        bool isAllowed,
        ulong targetXboxUserId,
        AnonymousUserType targetAnonymousUserType,
        Permission permission,
        IReadOnlyList<PrivacyPermissionDenyReasonDetail> denyReasons)
    {
        WasChecked = wasChecked;
        IsAllowed = wasChecked && isAllowed;
        TargetXboxUserId = targetXboxUserId;
        TargetAnonymousUserType = targetAnonymousUserType;
        Permission = permission;
        DenyReasons = denyReasons;
    }

    /// <summary>
    /// Whether Xbox Live completed the permission check. If this is <see langword="false"/>,
    /// <see cref="IsAllowed"/> is guaranteed to be <see langword="false"/>.
    /// </summary>
    public bool WasChecked { get; }

    /// <summary>
    /// Whether the action is allowed. This is fail-closed: it can only be <see langword="true"/>
    /// when <see cref="WasChecked"/> is <see langword="true"/> and Xbox Live explicitly granted the
    /// permission.
    /// </summary>
    public bool IsAllowed { get; }

    /// <summary>The target Xbox user id, or 0 when the target was an anonymous user class.</summary>
    public ulong TargetXboxUserId { get; }

    /// <summary>
    /// The anonymous target user class, or <see cref="AnonymousUserType.Unknown"/> when the
    /// target was an Xbox Live user.
    /// </summary>
    public AnonymousUserType TargetAnonymousUserType { get; }

    /// <summary>The permission that was checked.</summary>
    public Permission Permission { get; }

    /// <summary>
    /// Reasons reported by Xbox Live when the permission is denied. A failed or canceled check
    /// yields one <see cref="PermissionDenyReason.Unknown"/> reason.
    /// </summary>
    public IReadOnlyList<PrivacyPermissionDenyReasonDetail> DenyReasons { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{Permission}: {(IsAllowed ? "allowed" : "denied")}";

    internal static unsafe PrivacyPermissionCheckResult FromNative(XblPermissionCheckResult* native)
    {
        if (native is null)
        {
            return FailClosed(Permission.Unknown, 0, AnonymousUserType.Unknown);
        }

        var reasons = new PrivacyPermissionDenyReasonDetail[(int)native->ReasonsCount];
        for (int i = 0; i < reasons.Length; i++)
        {
            reasons[i] = PrivacyPermissionDenyReasonDetail.FromNative(native->Reasons[i]);
        }

        return new PrivacyPermissionCheckResult(
            wasChecked: true,
            isAllowed: native->IsAllowed != 0,
            targetXboxUserId: native->TargetXuid,
            targetAnonymousUserType: (AnonymousUserType)native->TargetUserType,
            permission: (Permission)native->PermissionRequested,
            denyReasons: new ReadOnlyCollection<PrivacyPermissionDenyReasonDetail>(reasons));
    }

    internal static PrivacyPermissionCheckResult FailClosed(
        Permission permission,
        ulong targetXboxUserId,
        AnonymousUserType targetAnonymousUserType) =>
        new(
            wasChecked: false,
            isAllowed: false,
            targetXboxUserId: targetXboxUserId,
            targetAnonymousUserType: targetAnonymousUserType,
            permission: permission,
            denyReasons: new ReadOnlyCollection<PrivacyPermissionDenyReasonDetail>(
                new[] { PrivacyPermissionDenyReasonDetail.Unknown }));
}
