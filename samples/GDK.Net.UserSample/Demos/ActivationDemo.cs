using System;
using GDK.Net.Activation;

namespace GDK.Net.UserSample.Demos;

/// <summary>
/// How a title finds out it was launched from a protocol link, a file association or a
/// multiplayer invite.
/// </summary>
internal static class ActivationDemo
{
    public static void Run(GameRuntime runtime)
    {
        Log.Write("");
        Log.Write("== Activation ==");

        // Activated unifies protocol, file and invite activation behind one event, including
        // pending invites. A pending activation is replayed when the handler is attached, so a
        // title that subscribes during startup still sees the launch it was started for.
        runtime.Activation.Activated += OnActivated;

        // Events are delivered on the process default task queue's thread pool, so there is
        // nothing for the title to pump; a brief wait gives anything replayed at subscription
        // time a chance to arrive before this demo unsubscribes.
        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(50));

        runtime.Activation.Activated -= OnActivated;

        Log.Write("  (nothing above means the title was launched normally, not from a link or invite)");
    }

    private static void OnActivated(object? sender, GameActivationEventArgs e) =>
        // For GameActivationType.PendingGameInvite, AcceptPendingInvite(e.Uri) would consume it and
        // join the session. Not called here, because doing so would pull this process into
        // someone's game.
        Log.Write($"  activated: {e.Kind} {e.Uri}");
}
