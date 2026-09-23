using System;
using GDK.Net.Interop;

namespace GDK.Net.Users;

// ──────────────────────────────────────────────────────────────────────────────────────────────
// Identity
// ──────────────────────────────────────────────────────────────────────────────────────────────

/// <summary>
/// A machine-stable identifier for a signed-in user. Mirrors <c>XUserLocalId</c>.
/// </summary>
/// <remarks>
/// Handles that <c>XUserCompare</c> treats as equal always share one local id, which is why
/// <see cref="User.GetHashCode"/> hashes this value.
/// </remarks>
public readonly struct UserLocalId : IEquatable<UserLocalId>
{
    /// <summary>Initialises the local id from its raw 64-bit value.</summary>
    public UserLocalId(ulong value)
    {
        Value = value;
    }

    /// <summary>The raw 64-bit local id.</summary>
    public ulong Value { get; }

    /// <summary>The null local id (<c>XUserNullUserLocalId</c>).</summary>
    public static UserLocalId Null => default;

    /// <summary><see langword="true"/> when this is the null local id.</summary>
    public bool IsNull => Value == 0;

    /// <inheritdoc/>
    public bool Equals(UserLocalId other) => Value == other.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is UserLocalId other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>Returns the local id as a 16-digit hexadecimal string.</summary>
    public override string ToString() => Value.ToString("X16");

    /// <summary>Equality operator.</summary>
    public static bool operator ==(UserLocalId left, UserLocalId right) => left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(UserLocalId left, UserLocalId right) => !left.Equals(right);
}

/// <summary>
/// A 32-byte opaque device identifier. Mirrors <c>APP_LOCAL_DEVICE_ID</c> from windef.h.
/// </summary>
/// <remarks>
/// The underlying bytes are stored as four 64-bit integers so the struct is naturally aligned and
/// blittable on both x64 and arm64. The layout is identical to the native <c>APP_LOCAL_DEVICE_ID</c>.
/// </remarks>
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
public struct AppLocalDeviceId : IEquatable<AppLocalDeviceId>
{
    // Four ulongs = 32 bytes, matching APP_LOCAL_DEVICE_ID { BYTE value[32]; }.
    private ulong _0, _1, _2, _3;

    /// <summary>The null device id (<c>XUserNullDeviceId</c>).</summary>
    public static AppLocalDeviceId Null => default;

    /// <summary><see langword="true"/> when this is the null device id.</summary>
    public bool IsNull => _0 == 0 && _1 == 0 && _2 == 0 && _3 == 0;

    /// <summary>Converts a native <c>XAppLocalDeviceId</c> into the managed representation.</summary>
    internal unsafe AppLocalDeviceId(XAppLocalDeviceId native)
    {
        // native is a stack-local; &native is valid without fixed.
        ulong* src = (ulong*)&native;
        _0 = src[0];
        _1 = src[1];
        _2 = src[2];
        _3 = src[3];
    }

    /// <summary>Converts this managed value into a native <c>XAppLocalDeviceId</c>.</summary>
    internal unsafe XAppLocalDeviceId ToNative()
    {
        // copy is a stack-local; &copy is valid without fixed.
        AppLocalDeviceId copy = this;
        return *(XAppLocalDeviceId*)&copy;
    }

    /// <inheritdoc/>
    public bool Equals(AppLocalDeviceId other) =>
        _0 == other._0 && _1 == other._1 && _2 == other._2 && _3 == other._3;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is AppLocalDeviceId other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            int h = (int)(_0 ^ (_0 >> 32));
            h = h * 397 ^ (int)(_1 ^ (_1 >> 32));
            h = h * 397 ^ (int)(_2 ^ (_2 >> 32));
            h = h * 397 ^ (int)(_3 ^ (_3 >> 32));
            return h;
        }
    }

    /// <inheritdoc/>
    public override string ToString() => $"{_0:X16}{_1:X16}{_2:X16}{_3:X16}";

    /// <summary>Equality operator.</summary>
    public static bool operator ==(AppLocalDeviceId left, AppLocalDeviceId right) => left.Equals(right);

    /// <summary>Inequality operator.</summary>
    public static bool operator !=(AppLocalDeviceId left, AppLocalDeviceId right) => !left.Equals(right);
}

// ──────────────────────────────────────────────────────────────────────────────────────────────
// Sign-in and sign-out
// ──────────────────────────────────────────────────────────────────────────────────────────────

/// <summary>Options for <see cref="UserManager.AddAsync"/>. Mirrors <c>XUserAddOptions</c>.</summary>
[Flags]
public enum UserAddOptions : uint
{
    /// <summary>No options; the account picker UI is shown.</summary>
    None = 0x00,

    /// <summary>
    /// Add the default user without showing any UI. Fails with
    /// <see cref="HResult.EGameUserNoDefaultUser"/> when there is no default user.
    /// </summary>
    AddDefaultUserSilently = 0x01,

    /// <summary>Allow a guest account to be selected.</summary>
    AllowGuests = 0x02,

    /// <summary>Add the default user, showing UI only when it is needed.</summary>
    AddDefaultUserAllowingUI = 0x04,
}

/// <summary>Mirrors <c>XUserState</c>.</summary>
public enum UserState : uint
{
    /// <summary>The user is signed in.</summary>
    SignedIn = 0,

    /// <summary>The user is signing out; a deferral may still hold the sign-out open.</summary>
    SigningOut = 1,

    /// <summary>The user is signed out.</summary>
    SignedOut = 2,
}

/// <summary>Mirrors <c>XUserAgeGroup</c>.</summary>
public enum UserAgeGroup : uint
{
    /// <summary>The age group could not be determined.</summary>
    Unknown = 0,

    /// <summary>A child account.</summary>
    Child = 1,

    /// <summary>A teenage account.</summary>
    Teen = 2,

    /// <summary>An adult account.</summary>
    Adult = 3,
}

/// <summary>
/// A sign-out deferral that prevents the Gaming Runtime from completing a user sign-out until the
/// deferral is disposed. Wraps <c>XUserSignOutDeferralHandle</c>.
/// </summary>
/// <remarks>
/// Obtain an instance from <see cref="UserManager.GetSignOutDeferral"/> inside the
/// <c>XUserChangeEvent.SigningOut</c> callback. Dispose as soon as the title has finished any
/// work that must complete before sign-out (for example, saving game state). Keeping this object
/// alive indefinitely will block the sign-out flow.
/// </remarks>
public sealed class SignOutDeferral : IDisposable
{
    private readonly SignOutDeferralHandle _handle;

    internal SignOutDeferral(SignOutDeferralHandle handle)
    {
        _handle = handle;
    }

    /// <summary>
    /// Releases the deferral, allowing the Gaming Runtime to complete the pending sign-out.
    /// </summary>
    public void Dispose() => _handle.Dispose();
}

// ──────────────────────────────────────────────────────────────────────────────────────────────
// Gamertag and gamer picture
// ──────────────────────────────────────────────────────────────────────────────────────────────

/// <summary>Which part of the gamertag to read. Mirrors <c>XUserGamertagComponent</c>.</summary>
public enum GamertagComponent : uint
{
    /// <summary>The classic gamertag.</summary>
    Classic = 0,

    /// <summary>The modern gamertag without its numeric suffix.</summary>
    Modern = 1,

    /// <summary>The modern gamertag's suffix, or empty when there is none.</summary>
    ModernSuffix = 2,

    /// <summary>The modern gamertag combined with its suffix.</summary>
    UniqueModern = 3,
}

/// <summary>Mirrors <c>XUserGamerPictureSize</c>.</summary>
public enum GamerPictureSize : uint
{
    /// <summary>64x64.</summary>
    Small = 0,

    /// <summary>208x208.</summary>
    Medium = 1,

    /// <summary>424x424.</summary>
    Large = 2,

    /// <summary>1080x1080.</summary>
    ExtraLarge = 3,
}

// ──────────────────────────────────────────────────────────────────────────────────────────────
// Privileges
// ──────────────────────────────────────────────────────────────────────────────────────────────

/// <summary>Mirrors <c>XUserPrivilege</c>.</summary>
public enum UserPrivilege : uint
{
    /// <summary>Play multiplayer games with users on other platforms.</summary>
    CrossPlay = 185,

    /// <summary>Create and participate in clubs.</summary>
    Clubs = 188,

    /// <summary>Join multiplayer sessions.</summary>
    Sessions = 189,

    /// <summary>Broadcast live gameplay to a streaming service.</summary>
    Broadcast = 190,

    /// <summary>Change profile privacy settings.</summary>
    ManageProfilePrivacy = 196,

    /// <summary>Record and upload game clips via Game DVR.</summary>
    GameDvr = 198,

    /// <summary>Join multiplayer parties.</summary>
    MultiplayerParties = 203,

    /// <summary>Manage cloud-hosted multiplayer sessions.</summary>
    CloudManageSession = 207,

    /// <summary>Join cloud-hosted multiplayer sessions.</summary>
    CloudJoinSession = 208,

    /// <summary>Store saved games in the cloud.</summary>
    CloudSavedGames = 209,

    /// <summary>Share content to social networks.</summary>
    SocialNetworkSharing = 220,

    /// <summary>View and upload user-generated content.</summary>
    UserGeneratedContent = 247,

    /// <summary>Use voice and text communication with other users.</summary>
    Communications = 252,

    /// <summary>Play multiplayer games on Xbox network.</summary>
    Multiplayer = 254,

    /// <summary>Add other users as friends.</summary>
    AddFriends = 255,
}

/// <summary>Mirrors <c>XUserPrivilegeDenyReason</c>.</summary>
public enum UserPrivilegeDenyReason : uint
{
    /// <summary>The privilege is not denied.</summary>
    None = 0,

    /// <summary>A subscription purchase is required before the privilege is granted.</summary>
    PurchaseRequired = 1,

    /// <summary>The privilege is restricted, typically by parental controls or account settings.</summary>
    Restricted = 2,

    /// <summary>The account is banned from using the privilege.</summary>
    Banned = 3,

    /// <summary>The reason could not be determined.</summary>
    Unknown = 0xFFFFFFFF,
}

/// <summary>Mirrors <c>XUserPrivilegeOptions</c>.</summary>
[Flags]
public enum UserPrivilegeOptions : uint
{
    /// <summary>No options.</summary>
    None = 0x00,

    /// <summary>Apply the check to all signed-in users rather than the specified one.</summary>
    AllUsers = 0x01,
}

// ──────────────────────────────────────────────────────────────────────────────────────────────
// Token and signature
// ──────────────────────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Options for token-and-signature requests. Mirrors <c>XUserGetTokenAndSignatureOptions</c>.
/// </summary>
[Flags]
public enum TokenAndSignatureOptions : uint
{
    /// <summary>No options.</summary>
    None = 0x00,

    /// <summary>Bypass the cache and always request a new token from the service.</summary>
    ForceRefresh = 0x01,

    /// <summary>Apply the check to all signed-in users, not just the primary one.</summary>
    AllUsers = 0x02,
}

/// <summary>
/// A single HTTP header for a token-and-signature request.
/// </summary>
public readonly struct TokenAndSignatureHttpHeader
{
    /// <summary>Initialises the header with a name and value.</summary>
    public TokenAndSignatureHttpHeader(string name, string value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>The header field name (e.g. <c>Content-Type</c>).</summary>
    public string Name { get; }

    /// <summary>The header field value.</summary>
    public string Value { get; }
}

/// <summary>
/// The Xbox Live token and optional body-signature returned by
/// <c>XUserGetTokenAndSignature[Utf16]Async</c>.
/// </summary>
public sealed class TokenAndSignature
{
    internal TokenAndSignature(string token, string signature)
    {
        Token = token;
        Signature = signature;
    }

    /// <summary>The Xbox Live authentication token.</summary>
    public string Token { get; }

    /// <summary>
    /// The HMAC-SHA256 signature of the request body, or <see cref="string.Empty"/> when no body
    /// was provided or signing was not requested.
    /// </summary>
    public string Signature { get; }
}

// ──────────────────────────────────────────────────────────────────────────────────────────────
// Audio endpoints
// ──────────────────────────────────────────────────────────────────────────────────────────────

/// <summary>
/// The audio endpoint role. Mirrors <c>XUserDefaultAudioEndpointKind</c> from XUser.h.
/// </summary>
public enum UserDefaultAudioEndpointKind : uint
{
    /// <summary>The communications render (speaker/headphones) endpoint.</summary>
    CommunicationRender = 0,

    /// <summary>The communications capture (microphone) endpoint.</summary>
    CommunicationCapture = 1,
}

/// <summary>
/// Payload for <see cref="UserManager.DefaultAudioEndpointChanged"/>.
/// </summary>
public sealed class UserDefaultAudioEndpointChangedEventArgs : EventArgs
{
    internal UserDefaultAudioEndpointChangedEventArgs(
        UserLocalId user,
        UserDefaultAudioEndpointKind kind,
        string? endpointId)
    {
        User = user;
        Kind = kind;
        EndpointId = endpointId;
    }

    /// <summary>The user whose default audio endpoint changed.</summary>
    public UserLocalId User { get; }

    /// <summary>Which endpoint role changed.</summary>
    public UserDefaultAudioEndpointKind Kind { get; }

    /// <summary>
    /// The new endpoint id, or <see langword="null"/> when the user no longer has a default
    /// endpoint for this role.
    /// </summary>
    public string? EndpointId { get; }
}

// ──────────────────────────────────────────────────────────────────────────────────────────────
// Change events
// ──────────────────────────────────────────────────────────────────────────────────────────────

/// <summary>Mirrors <c>XUserChangeEvent</c>.</summary>
public enum UserChangeEvent : uint
{
    /// <summary>The user signed in again.</summary>
    SignedInAgain = 0,

    /// <summary>
    /// The user is signing out. Take a deferral from
    /// <see cref="UserManager.GetSignOutDeferral"/> to finish work before sign-out completes.
    /// </summary>
    SigningOut = 1,

    /// <summary>The user has signed out.</summary>
    SignedOut = 2,

    /// <summary>The user's gamertag changed.</summary>
    Gamertag = 3,

    /// <summary>The user's gamer picture changed.</summary>
    GamerPicture = 4,

    /// <summary>The user's privileges changed.</summary>
    Privileges = 5,
}

/// <summary>
/// Payload for <see cref="UserManager.DeviceAssociationChanged"/>.
/// </summary>
public sealed class UserDeviceAssociationChangedEventArgs : EventArgs
{
    internal UserDeviceAssociationChangedEventArgs(
        AppLocalDeviceId deviceId,
        UserLocalId oldUser,
        UserLocalId newUser)
    {
        DeviceId = deviceId;
        OldUser = oldUser;
        NewUser = newUser;
    }

    /// <summary>The device whose user association changed.</summary>
    public AppLocalDeviceId DeviceId { get; }

    /// <summary>The local id of the user previously associated with the device, or <see cref="UserLocalId.Null"/>.</summary>
    public UserLocalId OldUser { get; }

    /// <summary>The local id of the user now associated with the device, or <see cref="UserLocalId.Null"/>.</summary>
    public UserLocalId NewUser { get; }
}
