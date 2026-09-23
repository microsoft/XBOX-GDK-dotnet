// Blittable mirrors of the XSAPI social types -- xsapi-c\social_c.h, GDK edition 260404.
//
// XblSocialRelationshipResultHandle owns the returned relationship graph. The projection snapshots
// every string and array into managed objects while the handle is alive; see XboxLive\SocialTypes.cs.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblSocialRelationshipFilter</c>.</summary>
internal enum XblSocialRelationshipFilter : uint
{
    All = 0,
    Favorite = 1,
    LegacyXboxLiveFriends = 2,
}

/// <summary>Mirrors <c>XblReputationFeedbackType</c>.</summary>
internal enum XblReputationFeedbackType : uint
{
    FairPlayKillsTeammates = 0,
    FairPlayCheater = 1,
    FairPlayTampering = 2,
    FairPlayQuitter = 3,
    FairPlayKicked = 4,
    CommunicationsInappropriateVideo = 5,
    CommunicationsAbusiveVoice = 6,
    InappropriateUserGeneratedContent = 7,
    PositiveSkilledPlayer = 8,
    PositiveHelpfulPlayer = 9,
    PositiveHighQualityUserGeneratedContent = 10,
    CommsPhishing = 11,
    CommsPictureMessage = 12,
    CommsSpam = 13,
    CommsTextMessage = 14,
    CommsVoiceMessage = 15,
    FairPlayConsoleBanRequest = 16,
    FairPlayIdler = 17,
    FairPlayUserBanRequest = 18,
    UserContentGamerpic = 19,
    UserContentPersonalInfo = 20,
    FairPlayUnsporting = 21,
    FairPlayLeaderboardCheater = 22,
}

/// <summary>Mirrors <c>XblSocialNotificationType</c>.</summary>
internal enum XblSocialNotificationType : uint
{
    Unknown = 0,
    Added = 1,
    Changed = 2,
    Removed = 3,
    IncomingFriendRequestCountChanged = 4,
}

/// <summary>Mirrors <c>XblSocialRelationship</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblSocialRelationship
{
    internal ulong XboxUserId;
    internal byte IsFavorite;
    internal byte IsFriend;
    internal byte IsFollowingCaller;
    internal byte** SocialNetworks;
    internal nuint SocialNetworksCount;
}

/// <summary>Mirrors <c>XblSocialRelationshipChangeEventArgs</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblSocialRelationshipChangeEventArgs
{
    internal ulong CallerXboxUserId;
    internal XblSocialNotificationType SocialNotification;
    internal ulong* XboxUserIds;
    internal nuint XboxUserIdsCount;
}

/// <summary>
/// Mirrors <c>XblSocialFriendRequestCountChangedEventArgs</c>. Its handler entry points are not
/// exported by the GDK 260404 XSAPI thunks DLL, but the type is part of <c>social_c.h</c>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XblSocialFriendRequestCountChangedEventArgs
{
    internal ulong CallerXboxUserId;
    internal nuint IncomingFriendRequestCount;
}

/// <summary>Mirrors <c>XblMultiplayerSessionReference</c>, used by reputation feedback.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblMultiplayerSessionReference
{
    internal const int ScidLength = 40;
    internal const int SessionTemplateNameMaxLength = 100;
    internal const int SessionNameMaxLength = 100;

    internal fixed byte Scid[ScidLength];
    internal fixed byte SessionTemplateName[SessionTemplateNameMaxLength];
    internal fixed byte SessionName[SessionNameMaxLength];
}

/// <summary>Mirrors <c>XblReputationFeedbackItem</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblReputationFeedbackItem
{
    internal ulong XboxUserId;
    internal XblReputationFeedbackType FeedbackType;
    internal XblMultiplayerSessionReference* SessionReference;
    internal byte* ReasonMessage;
    internal byte* EvidenceResourceId;
}
