using System;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Platform on which an activity is joinable. Mirrors <c>XblMultiplayerActivityPlatform</c>.</summary>
public enum MultiplayerActivityPlatform : uint
{
    /// <summary>The platform is unknown.</summary>
    Unknown = 0,

    /// <summary>Xbox One.</summary>
    XboxOne = 1,

    /// <summary>Windows OneCore.</summary>
    WindowsOneCore = 2,

    /// <summary>Win32.</summary>
    Win32 = 3,

    /// <summary>Xbox Series X|S.</summary>
    Scarlett = 4,

    /// <summary>iOS.</summary>
    Ios = 20,

    /// <summary>Android.</summary>
    Android = 30,

    /// <summary>Nintendo.</summary>
    Nintendo = 40,

    /// <summary>PlayStation.</summary>
    PlayStation = 50,

    /// <summary>All platforms supported by the title.</summary>
    All = 60,
}

/// <summary>Who can join a player's current activity. Mirrors <c>XblMultiplayerActivityJoinRestriction</c>.</summary>
public enum MultiplayerActivityJoinRestriction : uint
{
    /// <summary>Any eligible player can join.</summary>
    Public = 0,

    /// <summary>Only invited players can join.</summary>
    InviteOnly = 1,

    /// <summary>Only players followed by the activity owner can join.</summary>
    Followed = 2,
}

/// <summary>Type of recent-player encounter. Mirrors <c>XblMultiplayerActivityEncounterType</c>.</summary>
public enum MultiplayerActivityEncounterType : uint
{
    /// <summary>No title-independent meaning.</summary>
    Default = 0,

    /// <summary>The encountered player was a teammate.</summary>
    Teammate = 1,

    /// <summary>The encountered player was an opponent.</summary>
    Opponent = 2,
}

/// <summary>
/// Multiplayer activity information. Managed snapshot of <c>XblMultiplayerActivityInfo</c>.
/// </summary>
/// <remarks>
/// When used with <see cref="MultiplayerActivityService.SetActivityAsync"/>, the
/// <see cref="ConnectionString"/> and <see cref="GroupId"/> must describe the joinable activity
/// accurately. The advertised activity is what lets friends join; stale or orphaned activity data
/// is a certification failure.
/// </remarks>
public sealed class MultiplayerActivityInfo
{
    /// <summary>
    /// Creates multiplayer activity information.
    /// </summary>
    /// <param name="xboxUserId">Xbox user id that owns the activity.</param>
    /// <param name="connectionString">Connection string passed to a joining client.</param>
    /// <param name="joinRestriction">Who can join the activity.</param>
    /// <param name="maxPlayers">Maximum joinable players. 0 means no players can join, or lets XSAPI ignore it when setting.</param>
    /// <param name="currentPlayers">Current players in the activity. 0 means no other players, or lets XSAPI ignore it when setting.</param>
    /// <param name="groupId">Title-defined identifier shared by users in the same activity.</param>
    /// <param name="platform">Platform on which the activity is happening.</param>
    public MultiplayerActivityInfo(
        ulong xboxUserId,
        string? connectionString,
        MultiplayerActivityJoinRestriction joinRestriction = MultiplayerActivityJoinRestriction.Public,
        uint maxPlayers = 0,
        uint currentPlayers = 0,
        string? groupId = null,
        MultiplayerActivityPlatform platform = MultiplayerActivityPlatform.Unknown)
    {
        XboxUserId = xboxUserId;
        ConnectionString = connectionString;
        JoinRestriction = joinRestriction;
        MaxPlayers = maxPlayers;
        CurrentPlayers = currentPlayers;
        GroupId = groupId;
        Platform = platform;
    }

    /// <summary>The Xbox user id that owns the activity.</summary>
    public ulong XboxUserId { get; }

    /// <summary>
    /// Connection string passed to a joining client. Queries can return <see langword="null"/> when
    /// privacy or join restrictions hide the join data.
    /// </summary>
    public string? ConnectionString { get; }

    /// <summary>Who can join the activity.</summary>
    public MultiplayerActivityJoinRestriction JoinRestriction { get; }

    /// <summary>Maximum joinable players.</summary>
    public uint MaxPlayers { get; }

    /// <summary>Current players in the activity.</summary>
    public uint CurrentPlayers { get; }

    /// <summary>Title-defined identifier shared by users in the same activity.</summary>
    public string? GroupId { get; }

    /// <summary>Platform on which the activity is happening.</summary>
    public MultiplayerActivityPlatform Platform { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{XboxUserId}: {JoinRestriction} ({CurrentPlayers}/{MaxPlayers})";

    internal static unsafe MultiplayerActivityInfo FromNative(XblMultiplayerActivityInfo* native)
    {
        return new MultiplayerActivityInfo(
            native->Xuid,
            Utf8.ToString(native->ConnectionString),
            (MultiplayerActivityJoinRestriction)native->JoinRestriction,
            checked((uint)native->MaxPlayers),
            checked((uint)native->CurrentPlayers),
            Utf8.ToString(native->GroupId),
            (MultiplayerActivityPlatform)native->Platform);
    }
}

/// <summary>
/// One recent-player encounter to append to the local user's recent-player list. Managed
/// equivalent of <c>XblMultiplayerActivityRecentPlayerUpdate</c>.
/// </summary>
/// <remarks>
/// Recent-player data is privacy-sensitive social data. Report only real multiplayer encounters
/// for the signed-in user, with the least specific <see cref="EncounterType"/> that satisfies the
/// title's scenario.
/// </remarks>
public sealed class MultiplayerActivityRecentPlayerUpdate
{
    /// <summary>Creates a recent-player update.</summary>
    /// <param name="xboxUserId">Xbox user id of the encountered player.</param>
    /// <param name="encounterType">Type of encounter.</param>
    public MultiplayerActivityRecentPlayerUpdate(
        ulong xboxUserId,
        MultiplayerActivityEncounterType encounterType = MultiplayerActivityEncounterType.Default)
    {
        XboxUserId = xboxUserId;
        EncounterType = encounterType;
    }

    /// <summary>Xbox user id of the encountered player.</summary>
    public ulong XboxUserId { get; }

    /// <summary>Type of encounter.</summary>
    public MultiplayerActivityEncounterType EncounterType { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{XboxUserId}: {EncounterType}";
}
