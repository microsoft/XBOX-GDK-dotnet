// Blittable mirrors of the XSAPI social manager types -- xsapi-c\social_manager_c.h,
// GDK edition 260404.
//
// The social-manager pump returns pointers into XSAPI-owned memory that are valid only until the
// next XblSocialManagerDoWork call. The public projection snapshots every user and event before
// returning from DoWork. XblSocialManagerUserGroupHandle is different: it is a long-lived native
// object and is resolved through a handle-to-wrapper identity map.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

internal static class XblSocialManagerConstants
{
    internal const int MaxUsersFromList = 100;
}

/// <summary>Mirrors <c>XblSocialManagerExtraDetailLevel</c>.</summary>
[Flags]
internal enum XblSocialManagerExtraDetailLevel : uint
{
    NoExtraDetail = 0,
    TitleHistoryLevel = 1,
    PreferredColorLevel = 2,
    All = 3,
}

/// <summary>Mirrors <c>XblPresenceFilter</c> from <c>social_manager_c.h</c>.</summary>
internal enum XblSocialManagerPresenceFilter : uint
{
    Unknown = 0,
    TitleOnline = 1,
    TitleOffline = 2,
    TitleOnlineOutsideTitle = 3,
    AllOnline = 4,
    AllOffline = 5,
    AllTitle = 6,
    All = 7,
}

/// <summary>Mirrors <c>XblSocialManagerEventType</c>.</summary>
internal enum XblSocialManagerEventType : uint
{
    UsersAddedToSocialGraph = 0,
    UsersRemovedFromSocialGraph = 1,
    PresenceChanged = 2,
    ProfilesChanged = 3,
    SocialRelationshipsChanged = 4,
    LocalUserAdded = 5,
    SocialUserGroupLoaded = 6,
    SocialUserGroupUpdated = 7,
    UnknownEvent = 8,
}

/// <summary>Mirrors <c>XblRelationshipFilter</c> from <c>social_manager_c.h</c>.</summary>
internal enum XblSocialManagerRelationshipFilter : uint
{
    Unknown = 0,
    Friends = 1,
    Favorite = 2,
}

/// <summary>Mirrors <c>XblSocialUserGroupType</c>.</summary>
internal enum XblSocialUserGroupType : uint
{
    FilterType = 0,
    UserListType = 1,
}

/// <summary>Mirrors <c>XblTitleHistory</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblTitleHistory
{
    internal const int LastTimePlayedCharSize = 75;

    internal byte HasUserPlayed;
    internal long LastTimeUserPlayed;
    internal fixed byte LastTimeUserPlayedText[LastTimePlayedCharSize];
}

/// <summary>Mirrors <c>XblPreferredColor</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblPreferredColor
{
    internal const int ColorCharSize = 21;

    internal fixed byte PrimaryColor[ColorCharSize];
    internal fixed byte SecondaryColor[ColorCharSize];
    internal fixed byte TertiaryColor[ColorCharSize];
}

/// <summary>Mirrors <c>XblSocialManagerPresenceTitleRecord</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblSocialManagerPresenceTitleRecord
{
    internal const int TitleNameCharSize = 300;
    internal const int RichPresenceCharSize = 300;

    internal uint TitleId;
    internal fixed byte TitleName[TitleNameCharSize];
    internal byte IsTitleActive;
    internal fixed byte PresenceText[RichPresenceCharSize];
    internal byte IsBroadcasting;
    internal XblPresenceDeviceType DeviceType;
    internal byte IsPrimary;
}

/// <summary>Mirrors <c>XblSocialManagerPresenceRecord</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblSocialManagerPresenceRecord
{
    internal const int NumPresenceRecords = 6;
    internal const int PresenceTitleRecordSize = 616;

    internal XblPresenceUserState UserState;
    internal fixed byte PresenceTitleRecords[PresenceTitleRecordSize * NumPresenceRecords];
    internal uint PresenceTitleRecordCount;
}

/// <summary>Mirrors <c>XblSocialManagerUser</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblSocialManagerUser
{
    internal const int DisplayNameCharSize = 90;
    internal const int RealNameCharSize = 765;
    internal const int DisplayPicUrlRawCharSize = 675;
    internal const int GamerscoreCharSize = 48;
    internal const int GamertagCharSize = 48;
    internal const int ModernGamertagCharSize = 97;
    internal const int ModernGamertagSuffixCharSize = 15;
    internal const int UniqueModernGamertagCharSize = 101;

    internal ulong XboxUserId;
    internal byte IsFavorite;
    internal byte IsFriend;
    internal byte IsFollowingUser;
    internal byte IsFollowedByCaller;
    internal fixed byte DisplayName[DisplayNameCharSize];
    internal fixed byte RealName[RealNameCharSize];
    internal fixed byte DisplayPicUrlRaw[DisplayPicUrlRawCharSize];
    internal byte UseAvatar;
    internal fixed byte Gamerscore[GamerscoreCharSize];
    internal fixed byte Gamertag[GamertagCharSize];
    internal fixed byte ModernGamertag[ModernGamertagCharSize];
    internal fixed byte ModernGamertagSuffix[ModernGamertagSuffixCharSize];
    internal fixed byte UniqueModernGamertag[UniqueModernGamertagCharSize];
    internal XblSocialManagerPresenceRecord PresenceRecord;
    internal XblTitleHistory TitleHistory;
    internal XblPreferredColor PreferredColor;
}

/// <summary>Mirrors <c>XblSocialManagerEvent</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XblSocialManagerEvent
{
    internal const int MaxAffectedUsers = 10;

    internal IntPtr User;
    internal XblSocialManagerEventType EventType;
    internal int Hr;
    internal IntPtr GroupAffected;
    internal IntPtr UsersAffected0;
    internal IntPtr UsersAffected1;
    internal IntPtr UsersAffected2;
    internal IntPtr UsersAffected3;
    internal IntPtr UsersAffected4;
    internal IntPtr UsersAffected5;
    internal IntPtr UsersAffected6;
    internal IntPtr UsersAffected7;
    internal IntPtr UsersAffected8;
    internal IntPtr UsersAffected9;
}
