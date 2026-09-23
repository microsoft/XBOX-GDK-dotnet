using System;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab.Multiplayer;

/// <summary>
/// A matchmaking ticket (<c>PFMatchmakingTicketHandle</c>). Instances are identity-mapped by their
/// owning <see cref="PlayFabMultiplayer"/>.
/// </summary>
/// <remarks>
/// The ticket stays usable until <see cref="Destroy"/> is called; the multiplayer library owns the
/// handle, so there is nothing else to release. Progress arrives as
/// <see cref="MatchmakingTicketStatusChanged"/> and <see cref="MatchmakingTicketCompleted"/> on the
/// matchmaking pump.
/// </remarks>
public sealed unsafe class MatchmakingTicket
{
    private readonly PlayFabMultiplayer _owner;
    private bool _destroyed;

    internal MatchmakingTicket(PlayFabMultiplayer owner, IntPtr handle)
    {
        _owner = owner;
        RawHandle = handle;
    }

    internal IntPtr RawHandle { get; }

    internal IntPtr Handle
    {
        get
        {
            if (_destroyed)
            {
                throw new ObjectDisposedException(nameof(MatchmakingTicket));
            }

            return RawHandle;
        }
    }

    /// <summary>Whether the ticket is still usable.</summary>
    public bool IsValid => !_destroyed;

    /// <summary>Where the ticket is in the matchmaking flow (<c>PFMatchmakingTicketGetStatus</c>).</summary>
    public MatchmakingTicketStatus Status
    {
        get
        {
            PFMatchmakingTicketStatus value;
            Hr.ThrowIfFailed(NativePlayFab.PFMatchmakingTicketGetStatus(Handle, &value));
            return (MatchmakingTicketStatus)value;
        }
    }

    /// <summary>
    /// The service-assigned ticket id, available once the ticket has been created
    /// (<c>PFMatchmakingTicketGetTicketId</c>).
    /// </summary>
    public string? TicketId
    {
        get
        {
            byte* value;
            Hr.ThrowIfFailed(NativePlayFab.PFMatchmakingTicketGetTicketId(Handle, &value));
            return Utf8.ToString(value);
        }
    }

    /// <summary>
    /// The match this ticket produced, or <see langword="null"/> while matchmaking is still running
    /// (<c>PFMatchmakingTicketGetMatch</c>).
    /// </summary>
    public MatchmakingMatchDetails? Match
    {
        get
        {
            PFMatchmakingMatchDetails* value;
            Hr.ThrowIfFailed(NativePlayFab.PFMatchmakingTicketGetMatch(Handle, &value));
            return value is null ? null : MatchmakingMatchDetails.FromNative(value);
        }
    }

    /// <summary>Cancels matchmaking for this ticket (<c>PFMatchmakingTicketCancel</c>).</summary>
    public void Cancel() => Hr.ThrowIfFailed(NativePlayFab.PFMatchmakingTicketCancel(Handle));

    /// <summary>
    /// Releases the ticket (<c>PFMultiplayerDestroyMatchmakingTicket</c>). Later use of this
    /// instance throws <see cref="ObjectDisposedException"/>.
    /// </summary>
    public void Destroy()
    {
        if (_destroyed)
        {
            return;
        }

        _owner.DestroyTicket(this);
    }

    internal void Invalidate() => _destroyed = true;
}
