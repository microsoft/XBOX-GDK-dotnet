// Blittable mirrors of the XSAPI multiplayer activity types --
// xsapi-c\multiplayer_activity_c.h, GDK edition 260404.
//
// Invite notification types and handler registration functions are present in the header for
// non-GDK platforms, but Microsoft.Xbox.Services.C.Thunks.dll does not export that surface in this
// GDK edition. This projection therefore mirrors and exposes the query/write activity surface only.

using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblMultiplayerActivityPlatform</c>.</summary>
internal enum XblMultiplayerActivityPlatform : uint
{
    Unknown = 0,
    XboxOne = 1,
    WindowsOneCore = 2,
    Win32 = 3,
    Scarlett = 4,
    iOS = 20,
    Android = 30,
    Nintendo = 40,
    PlayStation = 50,
    All = 60,
}

/// <summary>Mirrors <c>XblMultiplayerActivityJoinRestriction</c>.</summary>
internal enum XblMultiplayerActivityJoinRestriction : uint
{
    Public = 0,
    InviteOnly = 1,
    Followed = 2,
}

/// <summary>Mirrors <c>XblMultiplayerActivityEncounterType</c>.</summary>
internal enum XblMultiplayerActivityEncounterType : uint
{
    Default = 0,
    Teammate = 1,
    Opponent = 2,
}

/// <summary>Mirrors <c>XblMultiplayerActivityInfo</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XblMultiplayerActivityInfo
{
    internal ulong Xuid;
    internal byte* ConnectionString;
    internal XblMultiplayerActivityJoinRestriction JoinRestriction;
    internal nuint MaxPlayers;
    internal nuint CurrentPlayers;
    internal byte* GroupId;
    internal XblMultiplayerActivityPlatform Platform;
}

/// <summary>Mirrors <c>XblMultiplayerActivityRecentPlayerUpdate</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XblMultiplayerActivityRecentPlayerUpdate
{
    internal ulong Xuid;
    internal XblMultiplayerActivityEncounterType EncounterType;
}
