using System;

namespace GDK.Net.PlayFab.Multiplayer;

/// <summary>
/// One entry from the matchmaking state-change queue (<c>PFMatchmakingStateChange</c>, drained by
/// <see cref="PlayFabMultiplayer.ProcessMatchmakingStateChanges"/>).
/// </summary>
public abstract record MatchmakingStateChange
{
    private protected MatchmakingStateChange(
        MatchmakingStateChangeType changeType, MatchmakingTicket ticket)
    {
        ChangeType = changeType;
        Ticket = ticket;
    }

    /// <summary>The native discriminator, mirroring <c>PFMatchmakingStateChange::stateChangeType</c>.</summary>
    public MatchmakingStateChangeType ChangeType { get; }

    /// <summary>The ticket the change is about.</summary>
    public MatchmakingTicket Ticket { get; }
}

/// <summary>The ticket moved to a new status; read it from <see cref="MatchmakingTicket.Status"/>.</summary>
public sealed record MatchmakingTicketStatusChanged : MatchmakingStateChange
{
    internal MatchmakingTicketStatusChanged(MatchmakingTicket ticket)
        : base(MatchmakingStateChangeType.TicketStatusChanged, ticket)
    {
    }
}

/// <summary>The ticket reached a terminal state, with a match or a failure.</summary>
public sealed record MatchmakingTicketCompleted : MatchmakingStateChange
{
    internal MatchmakingTicketCompleted(
        MatchmakingTicket ticket, OperationId operation, int resultCode)
        : base(MatchmakingStateChangeType.TicketCompleted, ticket)
    {
        Operation = operation;
        ResultCode = resultCode;
    }

    /// <summary>The id returned when the ticket was created.</summary>
    public OperationId Operation { get; }

    /// <summary>The raw native HRESULT.</summary>
    public int ResultCode { get; }

    /// <summary>Whether matchmaking failed.</summary>
    public bool Failed => HResult.Failed(ResultCode);

    /// <summary>The failure, or <see langword="null"/> when a match was found.</summary>
    public Exception? Error => Failed ? Hr.ToException(ResultCode) : null;
}
