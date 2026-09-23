// Raw interop types for XUser.h (GDK edition 260404).
//
// Types mirror the header one-for-one and keep their native names. See NativeTypes.cs for the
// conventions governing this layer.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

// ─── identity ─────────────────────────────────────────────────────────────────

/// <summary>Mirrors <c>struct XUserLocalId</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XUserLocalId
{
    public ulong Value;
}

/// <summary>
/// Mirrors <c>APP_LOCAL_DEVICE_ID</c> from windef.h — a 32-byte opaque device identifier.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XAppLocalDeviceId
{
    public fixed byte Value[32];
}

// ─── sign-in ──────────────────────────────────────────────────────────────────

[Flags]
internal enum XUserAddOptions : uint
{
    None = 0x00,
    AddDefaultUserSilently = 0x01,
    AllowGuests = 0x02,
    AddDefaultUserAllowingUI = 0x04,
}

internal enum XUserState : uint
{
    SignedIn = 0,
    SigningOut = 1,
    SignedOut = 2,
}

internal enum XUserAgeGroup : uint
{
    Unknown = 0,
    Child = 1,
    Teen = 2,
    Adult = 3,
}

// ─── gamertag and gamer picture ───────────────────────────────────────────────

internal enum XUserGamertagComponent : uint
{
    Classic = 0,
    Modern = 1,
    ModernSuffix = 2,
    UniqueModern = 3,
}

internal enum XUserGamerPictureSize : uint
{
    Small = 0,
    Medium = 1,
    Large = 2,
    ExtraLarge = 3,
}

// ─── privileges ───────────────────────────────────────────────────────────────

internal enum XUserPrivilege : uint
{
    CrossPlay = 185,
    Clubs = 188,
    Sessions = 189,
    Broadcast = 190,
    ManageProfilePrivacy = 196,
    GameDvr = 198,
    MultiplayerParties = 203,
    CloudManageSession = 207,
    CloudJoinSession = 208,
    CloudSavedGames = 209,
    SocialNetworkSharing = 220,
    UserGeneratedContent = 247,
    Communications = 252,
    Multiplayer = 254,
    AddFriends = 255,
}

internal enum XUserPrivilegeDenyReason : uint
{
    None = 0,
    PurchaseRequired = 1,
    Restricted = 2,
    Banned = 3,
    Unknown = 0xFFFFFFFF,
}

[Flags]
internal enum XUserPrivilegeOptions : uint
{
    None = 0x00,
    AllUsers = 0x01,
}

// ─── token and signature ──────────────────────────────────────────────────────

/// <summary>Mirrors <c>XUserGetTokenAndSignatureOptions</c>.</summary>
[Flags]
internal enum XUserGetTokenAndSignatureOptions : uint
{
    None = 0x00,
    ForceRefresh = 0x01,
    AllUsers = 0x02,
}

/// <summary>Mirrors <c>struct XUserGetTokenAndSignatureHttpHeader</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XUserGetTokenAndSignatureHttpHeader
{
    public byte* Name;
    public byte* Value;
}

/// <summary>Mirrors <c>struct XUserGetTokenAndSignatureData</c>.</summary>
/// <remarks>
/// <c>Token</c> and <c>Signature</c> are pointers into the caller's result buffer. Copy them into
/// managed strings before releasing the buffer.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XUserGetTokenAndSignatureData
{
    public nuint TokenSize;
    public nuint SignatureSize;
    public byte* Token;
    public byte* Signature;
}

/// <summary>Mirrors <c>struct XUserGetTokenAndSignatureUtf16HttpHeader</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XUserGetTokenAndSignatureUtf16HttpHeader
{
    public char* Name;
    public char* Value;
}

/// <summary>Mirrors <c>struct XUserGetTokenAndSignatureUtf16Data</c>.</summary>
/// <remarks>
/// <c>Token</c> and <c>Signature</c> are pointers into the caller's result buffer. Copy them into
/// managed strings before releasing the buffer.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XUserGetTokenAndSignatureUtf16Data
{
    public nuint TokenCount;
    public nuint SignatureCount;
    public char* Token;
    public char* Signature;
}

// ─── audio endpoints ──────────────────────────────────────────────────────────

/// <summary>Mirrors <c>XUserDefaultAudioEndpointKind</c>.</summary>
internal enum XUserDefaultAudioEndpointKind : uint
{
    CommunicationRender = 0,
    CommunicationCapture = 1,
}

// ─── change events ────────────────────────────────────────────────────────────

internal enum XUserChangeEvent : uint
{
    SignedInAgain = 0,
    SigningOut = 1,
    SignedOut = 2,
    Gamertag = 3,
    GamerPicture = 4,
    Privileges = 5,
}

/// <summary>Mirrors <c>struct XUserDeviceAssociationChange</c>.</summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct XUserDeviceAssociationChange
{
    public XAppLocalDeviceId DeviceId;
    public XUserLocalId OldUser;
    public XUserLocalId NewUser;
}
