using System;
using System.Collections.Generic;

namespace GDK.Net.PlayFab.Multiplayer;

/// <summary>
/// One entry from the lobby state-change queue
/// (<c>PFLobbyStateChange</c>, drained by <see cref="PlayFabMultiplayer.ProcessLobbyStateChanges"/>).
/// </summary>
/// <remarks>
/// Native change memory is only valid between <c>StartProcessingLobbyStateChanges</c> and
/// <c>FinishProcessingLobbyStateChanges</c>, so every record snapshots the values it exposes while
/// the enumerator holds the batch. <see cref="Lobby"/> instances are identity-mapped, so the same
/// lobby always surfaces as the same object.
/// </remarks>
public abstract record LobbyStateChange
{
    private protected LobbyStateChange(LobbyStateChangeType changeType, Lobby? lobby)
    {
        ChangeType = changeType;
        Lobby = lobby;
    }

    /// <summary>The native discriminator, mirroring <c>PFLobbyStateChange::stateChangeType</c>.</summary>
    public LobbyStateChangeType ChangeType { get; }

    /// <summary>The lobby the change is about, when the change carries one.</summary>
    public Lobby? Lobby { get; }
}

/// <summary>
/// Base class for the changes that complete an operation started earlier on the same manager.
/// </summary>
public abstract record LobbyOperationCompleted : LobbyStateChange
{
    private protected LobbyOperationCompleted(
        LobbyStateChangeType changeType, Lobby? lobby, OperationId operation, int resultCode)
        : base(changeType, lobby)
    {
        Operation = operation;
        ResultCode = resultCode;
    }

    /// <summary>The id returned when the title started this operation.</summary>
    public OperationId Operation { get; }

    /// <summary>The raw native HRESULT.</summary>
    public int ResultCode { get; }

    /// <summary>Whether the operation failed.</summary>
    public bool Failed => HResult.Failed(ResultCode);

    /// <summary>
    /// The failure, or <see langword="null"/> when the operation succeeded. The exception is
    /// created on demand, so a successful pump allocates nothing.
    /// </summary>
    public Exception? Error => Failed ? Hr.ToException(ResultCode) : null;
}

/// <summary>A <see cref="PlayFabMultiplayer.CreateAndJoinLobby(EntityKey, LobbyCreateConfiguration, LobbyJoinConfiguration?)"/> operation finished.</summary>
public sealed record CreateAndJoinLobbyCompleted : LobbyOperationCompleted
{
    internal CreateAndJoinLobbyCompleted(Lobby? lobby, OperationId operation, int resultCode)
        : base(LobbyStateChangeType.CreateAndJoinLobbyCompleted, lobby, operation, resultCode)
    {
    }
}

/// <summary>A <see cref="PlayFabMultiplayer.JoinLobby(EntityKey, string, LobbyJoinConfiguration?)"/> operation finished.</summary>
public sealed record JoinLobbyCompleted : LobbyOperationCompleted
{
    internal JoinLobbyCompleted(
        Lobby? lobby, OperationId operation, int resultCode, EntityKey newMember)
        : base(LobbyStateChangeType.JoinLobbyCompleted, lobby, operation, resultCode)
    {
        NewMember = newMember;
    }

    /// <summary>The entity that joined.</summary>
    public EntityKey NewMember { get; }
}

/// <summary>A <see cref="PlayFabMultiplayer.ConnectToLobby(EntityKey, string)"/> operation finished.</summary>
public sealed record ConnectToLobbyCompleted : LobbyOperationCompleted
{
    internal ConnectToLobbyCompleted(
        Lobby? lobby, OperationId operation, int resultCode, EntityKey newMember, string? lobbyId)
        : base(LobbyStateChangeType.ConnectToLobbyCompleted, lobby, operation, resultCode)
    {
        NewMember = newMember;
        LobbyId = lobbyId;
    }

    /// <summary>The entity that connected.</summary>
    public EntityKey NewMember { get; }

    /// <summary>The id of the lobby that was connected to.</summary>
    public string? LobbyId { get; }
}

/// <summary>A member joined the lobby.</summary>
public sealed record LobbyMemberAdded : LobbyStateChange
{
    internal LobbyMemberAdded(Lobby? lobby, EntityKey member)
        : base(LobbyStateChangeType.MemberAdded, lobby)
    {
        Member = member;
    }

    /// <summary>The member that joined.</summary>
    public EntityKey Member { get; }
}

/// <summary>A <see cref="Lobby.AddMember"/> operation finished.</summary>
public sealed record AddMemberCompleted : LobbyOperationCompleted
{
    internal AddMemberCompleted(
        Lobby? lobby, OperationId operation, int resultCode, EntityKey localUser)
        : base(LobbyStateChangeType.AddMemberCompleted, lobby, operation, resultCode)
    {
        LocalUser = localUser;
    }

    /// <summary>The local user that was added.</summary>
    public EntityKey LocalUser { get; }
}

/// <summary>A member left or was removed from the lobby.</summary>
public sealed record LobbyMemberRemoved : LobbyStateChange
{
    internal LobbyMemberRemoved(Lobby? lobby, EntityKey member, LobbyMemberRemovedReason reason)
        : base(LobbyStateChangeType.MemberRemoved, lobby)
    {
        Member = member;
        Reason = reason;
    }

    /// <summary>The member that left.</summary>
    public EntityKey Member { get; }

    /// <summary>Why the member left.</summary>
    public LobbyMemberRemovedReason Reason { get; }
}

/// <summary>A <see cref="Lobby.ForceRemoveMember"/> operation finished.</summary>
public sealed record ForceRemoveMemberCompleted : LobbyOperationCompleted
{
    internal ForceRemoveMemberCompleted(
        Lobby? lobby, OperationId operation, int resultCode, EntityKey targetMember)
        : base(LobbyStateChangeType.ForceRemoveMemberCompleted, lobby, operation, resultCode)
    {
        TargetMember = targetMember;
    }

    /// <summary>The member that was removed.</summary>
    public EntityKey TargetMember { get; }
}

/// <summary>A <see cref="Lobby.Leave"/> operation finished; the lobby is now invalid.</summary>
public sealed record LeaveLobbyCompleted : LobbyOperationCompleted
{
    internal LeaveLobbyCompleted(Lobby? lobby, OperationId operation, EntityKey? localUser)
        : base(LobbyStateChangeType.LeaveLobbyCompleted, lobby, operation, HResult.SOk)
    {
        LocalUser = localUser;
    }

    /// <summary>
    /// The local user that left, or <see langword="null"/> when every local member left at once.
    /// </summary>
    public EntityKey? LocalUser { get; }
}

/// <summary>The lobby's shared state changed.</summary>
public sealed record LobbyUpdated : LobbyStateChange
{
    internal LobbyUpdated(
        Lobby? lobby,
        bool ownerUpdated,
        bool maxMembersUpdated,
        bool accessPolicyUpdated,
        bool membershipLockUpdated,
        bool restrictInvitesToLobbyOwnerUpdated,
        bool serverUpdated,
        bool serverConnectionStatusUpdated,
        IReadOnlyList<string> updatedSearchPropertyKeys,
        IReadOnlyList<string> updatedLobbyPropertyKeys,
        IReadOnlyList<string> updatedServerPropertyKeys,
        IReadOnlyList<LobbyMemberUpdateSummary> memberUpdates)
        : base(LobbyStateChangeType.Updated, lobby)
    {
        OwnerUpdated = ownerUpdated;
        MaxMembersUpdated = maxMembersUpdated;
        AccessPolicyUpdated = accessPolicyUpdated;
        MembershipLockUpdated = membershipLockUpdated;
        RestrictInvitesToLobbyOwnerUpdated = restrictInvitesToLobbyOwnerUpdated;
        ServerUpdated = serverUpdated;
        ServerConnectionStatusUpdated = serverConnectionStatusUpdated;
        UpdatedSearchPropertyKeys = updatedSearchPropertyKeys;
        UpdatedLobbyPropertyKeys = updatedLobbyPropertyKeys;
        UpdatedServerPropertyKeys = updatedServerPropertyKeys;
        MemberUpdates = memberUpdates;
    }

    /// <summary>Whether the lobby owner changed.</summary>
    public bool OwnerUpdated { get; }

    /// <summary>Whether the maximum member count changed.</summary>
    public bool MaxMembersUpdated { get; }

    /// <summary>Whether the access policy changed.</summary>
    public bool AccessPolicyUpdated { get; }

    /// <summary>Whether the membership lock changed.</summary>
    public bool MembershipLockUpdated { get; }

    /// <summary>Whether the invite restriction changed.</summary>
    public bool RestrictInvitesToLobbyOwnerUpdated { get; }

    /// <summary>Whether the lobby's server changed.</summary>
    public bool ServerUpdated { get; }

    /// <summary>Whether the server's connection status changed.</summary>
    public bool ServerConnectionStatusUpdated { get; }

    /// <summary>The search properties that changed.</summary>
    public IReadOnlyList<string> UpdatedSearchPropertyKeys { get; }

    /// <summary>The lobby properties that changed.</summary>
    public IReadOnlyList<string> UpdatedLobbyPropertyKeys { get; }

    /// <summary>The server properties that changed.</summary>
    public IReadOnlyList<string> UpdatedServerPropertyKeys { get; }

    /// <summary>Per-member summaries of what changed.</summary>
    public IReadOnlyList<LobbyMemberUpdateSummary> MemberUpdates { get; }
}

/// <summary>A <see cref="Lobby.PostUpdate"/> operation finished.</summary>
public sealed record PostUpdateCompleted : LobbyOperationCompleted
{
    internal PostUpdateCompleted(
        Lobby? lobby, OperationId operation, int resultCode, EntityKey localUser)
        : base(LobbyStateChangeType.PostUpdateCompleted, lobby, operation, resultCode)
    {
        LocalUser = localUser;
    }

    /// <summary>The local user whose update was posted.</summary>
    public EntityKey LocalUser { get; }
}

/// <summary>The client lost its connection to the lobby and is trying to recover.</summary>
public sealed record LobbyDisconnecting : LobbyStateChange
{
    internal LobbyDisconnecting(Lobby? lobby, LobbyDisconnectingReason reason)
        : base(LobbyStateChangeType.Disconnecting, lobby)
    {
        Reason = reason;
    }

    /// <summary>Why the client is disconnecting.</summary>
    public LobbyDisconnectingReason Reason { get; }
}

/// <summary>The lobby was disconnected and is now invalid.</summary>
public sealed record LobbyDisconnected : LobbyStateChange
{
    internal LobbyDisconnected(Lobby? lobby)
        : base(LobbyStateChangeType.Disconnected, lobby)
    {
    }
}

/// <summary>A <see cref="PlayFabMultiplayer.JoinArrangedLobby(EntityKey, string, LobbyArrangedJoinConfiguration)"/> operation finished.</summary>
public sealed record JoinArrangedLobbyCompleted : LobbyOperationCompleted
{
    internal JoinArrangedLobbyCompleted(
        Lobby? lobby, OperationId operation, int resultCode, EntityKey newMember)
        : base(LobbyStateChangeType.JoinArrangedLobbyCompleted, lobby, operation, resultCode)
    {
        NewMember = newMember;
    }

    /// <summary>The entity that joined.</summary>
    public EntityKey NewMember { get; }
}

/// <summary>A <see cref="PlayFabMultiplayer.FindLobbies(EntityKey, LobbySearchConfiguration)"/> operation finished.</summary>
public sealed record FindLobbiesCompleted : LobbyOperationCompleted
{
    internal FindLobbiesCompleted(
        OperationId operation,
        int resultCode,
        EntityKey searchingEntity,
        IReadOnlyList<LobbySearchResult> results)
        : base(LobbyStateChangeType.FindLobbiesCompleted, null, operation, resultCode)
    {
        SearchingEntity = searchingEntity;
        Results = results;
    }

    /// <summary>The entity the search was run for.</summary>
    public EntityKey SearchingEntity { get; }

    /// <summary>The lobbies that matched.</summary>
    public IReadOnlyList<LobbySearchResult> Results { get; }
}

/// <summary>An invite to a lobby arrived for an entity the title is listening for.</summary>
public sealed record LobbyInviteReceived : LobbyStateChange
{
    internal LobbyInviteReceived(
        EntityKey listeningEntity,
        EntityKey invitingEntity,
        string? connectionString,
        string? lobbyId)
        : base(LobbyStateChangeType.InviteReceived, null)
    {
        ListeningEntity = listeningEntity;
        InvitingEntity = invitingEntity;
        ConnectionString = connectionString;
        LobbyId = lobbyId;
    }

    /// <summary>The local entity the invite was sent to.</summary>
    public EntityKey ListeningEntity { get; }

    /// <summary>The entity that sent the invite.</summary>
    public EntityKey InvitingEntity { get; }

    /// <summary>The connection string to pass to <see cref="PlayFabMultiplayer.JoinLobby(EntityKey, string, LobbyJoinConfiguration?)"/>.</summary>
    public string? ConnectionString { get; }

    /// <summary>The id of the lobby the invite is for.</summary>
    public string? LobbyId { get; }
}

/// <summary>An invite listener's status changed.</summary>
public sealed record LobbyInviteListenerStatusChanged : LobbyStateChange
{
    internal LobbyInviteListenerStatusChanged(EntityKey listeningEntity, LobbyInviteListenerStatus status)
        : base(LobbyStateChangeType.InviteListenerStatusChanged, null)
    {
        ListeningEntity = listeningEntity;
        Status = status;
    }

    /// <summary>The entity whose listener changed.</summary>
    public EntityKey ListeningEntity { get; }

    /// <summary>The listener's new status.</summary>
    public LobbyInviteListenerStatus Status { get; }
}

/// <summary>A <see cref="Lobby.SendInvite"/> operation finished.</summary>
public sealed record SendInviteCompleted : LobbyOperationCompleted
{
    internal SendInviteCompleted(
        Lobby? lobby, OperationId operation, int resultCode, EntityKey sender, EntityKey invitee)
        : base(LobbyStateChangeType.SendInviteCompleted, lobby, operation, resultCode)
    {
        Sender = sender;
        Invitee = invitee;
    }

    /// <summary>The entity that sent the invite.</summary>
    public EntityKey Sender { get; }

    /// <summary>The entity the invite was sent to.</summary>
    public EntityKey Invitee { get; }
}

/// <summary>A <see cref="PlayFabMultiplayer.CreateAndClaimServerLobby(EntityKey, LobbyCreateConfiguration)"/> operation finished.</summary>
public sealed record CreateAndClaimServerLobbyCompleted : LobbyOperationCompleted
{
    internal CreateAndClaimServerLobbyCompleted(Lobby? lobby, OperationId operation, int resultCode)
        : base(LobbyStateChangeType.CreateAndClaimServerLobbyCompleted, lobby, operation, resultCode)
    {
    }
}

/// <summary>A <see cref="PlayFabMultiplayer.ClaimServerLobby(EntityKey, string)"/> operation finished.</summary>
public sealed record ClaimServerLobbyCompleted : LobbyOperationCompleted
{
    internal ClaimServerLobbyCompleted(
        Lobby? lobby, OperationId operation, int resultCode, string? lobbyId)
        : base(LobbyStateChangeType.ClaimServerLobbyCompleted, lobby, operation, resultCode)
    {
        LobbyId = lobbyId;
    }

    /// <summary>The id of the claimed lobby.</summary>
    public string? LobbyId { get; }
}

/// <summary>A <see cref="PlayFabMultiplayer.JoinLobbyAsServer(EntityKey, string, LobbyServerJoinConfiguration?)"/> operation finished.</summary>
public sealed record JoinLobbyAsServerCompleted : LobbyOperationCompleted
{
    internal JoinLobbyAsServerCompleted(
        Lobby? lobby, OperationId operation, int resultCode, EntityKey newServer)
        : base(LobbyStateChangeType.JoinLobbyAsServerCompleted, lobby, operation, resultCode)
    {
        NewServer = newServer;
    }

    /// <summary>The server entity that joined.</summary>
    public EntityKey NewServer { get; }
}

/// <summary>A <see cref="Lobby.ServerPostUpdate"/> operation finished.</summary>
public sealed record ServerPostUpdateCompleted : LobbyOperationCompleted
{
    internal ServerPostUpdateCompleted(Lobby? lobby, OperationId operation, int resultCode)
        : base(LobbyStateChangeType.ServerPostUpdateCompleted, lobby, operation, resultCode)
    {
    }
}

/// <summary>A <see cref="Lobby.ServerPostUpdateAsServer"/> operation finished.</summary>
public sealed record ServerPostUpdateAsServerCompleted : LobbyOperationCompleted
{
    internal ServerPostUpdateAsServerCompleted(Lobby? lobby, OperationId operation, int resultCode)
        : base(LobbyStateChangeType.ServerPostUpdateAsServerCompleted, lobby, operation, resultCode)
    {
    }
}

/// <summary>A <see cref="Lobby.ServerLeaveAsServer"/> operation finished; the lobby is now invalid.</summary>
public sealed record ServerLeaveLobbyAsServerCompleted : LobbyOperationCompleted
{
    internal ServerLeaveLobbyAsServerCompleted(Lobby? lobby, OperationId operation)
        : base(
            LobbyStateChangeType.ServerLeaveLobbyAsServerCompleted,
            lobby,
            operation,
            HResult.SOk)
    {
    }
}

/// <summary>A <see cref="Lobby.ServerDeleteLobby"/> operation finished; the lobby is now invalid.</summary>
public sealed record ServerDeleteLobbyCompleted : LobbyOperationCompleted
{
    internal ServerDeleteLobbyCompleted(Lobby? lobby, OperationId operation)
        : base(LobbyStateChangeType.ServerDeleteLobbyCompleted, lobby, operation, HResult.SOk)
    {
    }
}
