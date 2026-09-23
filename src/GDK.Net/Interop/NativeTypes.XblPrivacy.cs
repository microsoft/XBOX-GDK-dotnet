// Blittable mirrors of the XSAPI privacy types -- xsapi-c\privacy_c.h, GDK edition 260404.
//
// The privacy notification handler functions are declared in the header but are not exported by
// Microsoft.Xbox.Services.C.Thunks.dll in this GDK edition, so only their argument structs are
// mirrored here for layout completeness; Native.XblPrivacy.cs deliberately does not bind them.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblPrivacySetting</c>.</summary>
internal enum XblPrivacySetting : uint
{
    Unknown = 0,
    ShareFriendList = 1,
    ShareGameHistory = 2,
    CommunicateUsingTextAndVoice = 3,
    SharePresence = 4,
    ShareProfile = 5,
    ShareVideoAndMusicStatus = 6,
    CommunicateUsingVideo = 7,
    CollectVoiceData = 8,
    ShareXboxMusicActivity = 9,
    ShareExerciseInfo = 11,
    ShareIdentity = 12,
    ShareIdentityInGame = 13,
    ShareRecordedGameSessions = 14,
    CollectLiveTvData = 15,
    CollectXboxVideoData = 16,
    ShareIdentityTransitively = 17,
    ShareVideoHistory = 18,
    ShareMusicHistory = 19,
    AllowUserCreatedContentViewing = 20,
    AllowProfileViewing = 21,
    ShowRealTimeActivity = 22,
    CollectVoiceDataXboxOneFull = 23,
    CanShareIdentity = 24,
    ShareContentToExternalNetworks = 25,
    CollectVoiceSearchData = 26,
    ShareClubMembership = 27,
    CollectVoiceGameChatData = 28,
    ShareActivityFeed = 29,
    CommunicateDuringCrossNetworkPlay = 30,
}

/// <summary>Mirrors <c>XblPrivilege</c>.</summary>
internal enum XblPrivilege : uint
{
    Unknown = 0,
    AllowIngameVoiceCommunications = 205,
    AllowVideoCommunications = 235,
    AllowProfileViewing = 249,
    AllowCommunications = 252,
    AllowMultiplayer = 254,
    AllowAddFriend = 255,
}

/// <summary>Mirrors <c>XblPermission</c>.</summary>
internal enum XblPermission : uint
{
    Unknown = 0,
    CommunicateUsingText = 1000,
    CommunicateUsingVideo = 1001,
    CommunicateUsingVoice = 1002,
    ViewTargetProfile = 1004,
    ViewTargetGameHistory = 1005,
    ViewTargetVideoHistory = 1006,
    ViewTargetMusicHistory = 1007,
    ViewTargetExerciseInfo = 1009,
    ViewTargetPresence = 1011,
    ViewTargetVideoStatus = 1012,
    ViewTargetMusicStatus = 1013,
    PlayMultiplayer = 1014,
    ViewTargetUserCreatedContent = 1018,
    BroadcastWithTwitch = 1019,
    WriteComment = 1022,
    ShareItem = 1024,
    ShareTargetContentToExternalNetworks = 1025,
}

/// <summary>Mirrors <c>XblPermissionDenyReason</c>.</summary>
internal enum XblPermissionDenyReason : uint
{
    Unknown = 0,
    NotAllowed = 2,
    MissingPrivilege = 3,
    PrivilegeRestrictsTarget = 4,
    BlockListRestrictsTarget = 5,
    MuteListRestrictsTarget = 7,
    PrivacySettingRestrictsTarget = 9,
    CrossNetworkUserMustBeFriend = 12,
}

/// <summary>Mirrors <c>XblAnonymousUserType</c>.</summary>
internal enum XblAnonymousUserType : uint
{
    Unknown = 0,
    CrossNetworkUser = 1,
    CrossNetworkFriend = 2,
}

/// <summary>Mirrors <c>XblPrivacyListChangeType</c>.</summary>
internal enum XblPrivacyListChangeType : uint
{
    Added = 0,
    Removed = 1,
}

/// <summary>Mirrors <c>XblPermissionDenyReasonDetails</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XblPermissionDenyReasonDetails
{
    internal XblPermissionDenyReason Reason;
    internal XblPrivilege RestrictedPrivilege;
    internal XblPrivacySetting RestrictedPrivacySetting;
}

/// <summary>Mirrors <c>XblPermissionCheckResult</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblPermissionCheckResult
{
    internal byte IsAllowed;
    internal ulong TargetXuid;
    internal XblAnonymousUserType TargetUserType;
    internal XblPermission PermissionRequested;
    internal XblPermissionDenyReasonDetails* Reasons;
    internal nuint ReasonsCount;
}

/// <summary>Mirrors <c>XblPrivacyMuteListChangeEventArgs</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblPrivacyMuteListChangeEventArgs
{
    internal XblPrivacyListChangeType ChangeType;
    internal ulong* Xuids;
    internal nuint XuidsCount;
}

/// <summary>Mirrors <c>XblPrivacyBlockListChangeEventArgs</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblPrivacyBlockListChangeEventArgs
{
    internal XblPrivacyListChangeType ChangeType;
    internal ulong* Xuids;
    internal nuint XuidsCount;
}
