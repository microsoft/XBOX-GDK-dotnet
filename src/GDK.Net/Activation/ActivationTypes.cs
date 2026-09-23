using System;

namespace GDK.Net.Activation;

/// <summary>
/// How the title was activated.
/// </summary>
public enum GameActivationType
{
    /// <summary>
    /// Activated through a registered protocol URI (<c>XGameProtocolRegisterForActivation</c>).
    /// </summary>
    Protocol = 0,

    /// <summary>
    /// A multiplayer game invite (<c>XGameInviteRegisterForEvent</c>). The URI is the invite handle
    /// to hand to the multiplayer service.
    /// </summary>
    Invite = 1,

    /// <summary>
    /// Activated by launching an associated file type
    /// (<c>XGameActivationType::File</c>). The URI is the file path.
    /// </summary>
    /// <remarks>
    /// Only reported by <see cref="GameActivationManager.Activated"/>, which is backed by the
    /// unified <c>XGameActivationRegisterForEvent</c>.
    /// </remarks>
    File = 2,

    /// <summary>
    /// An invite the user accepted while the title was not running, or accepted from outside it
    /// (<c>XGameActivationType::PendingGameInvite</c>).
    /// </summary>
    /// <remarks>
    /// A pending invite is replayed on registration rather than raised live, and is only consumed
    /// once the title calls <see cref="GameActivationManager.AcceptPendingInvite"/>.
    /// </remarks>
    PendingGameInvite = 3,

    /// <summary>
    /// An invite the user accepted while the title was already running
    /// (<c>XGameActivationType::AcceptedGameInvite</c>).
    /// </summary>
    AcceptedGameInvite = 4,
}

/// <summary>
/// Describes a single activation of the title.
/// </summary>
public sealed class GameActivationEventArgs : EventArgs
{
    internal GameActivationEventArgs(GameActivationType kind, string uri)
    {
        Kind = kind;
        Uri = uri;
    }

    /// <summary>What kind of activation this is.</summary>
    public GameActivationType Kind { get; }

    /// <summary>The activation URI, as delivered by the Gaming Runtime.</summary>
    public string Uri { get; }
}
