using System;
using System.Threading.Tasks;

namespace GDK.Net.MultiplayerHarness;

/// <summary>
/// A multi-process harness for the PlayFab multiplayer surface: Lobby and Party.
/// </summary>
/// <remarks>
/// <para>
/// <c>tests/GDK.Net.LiveHarness</c> can prove that a lobby is created and that the Party pump
/// brackets correctly, because those are single-process facts. It cannot prove that a member a
/// second client added shows up, or that a message one peer sent arrives at another, for that
/// there has to be another client, in another process, with its own copy of the SDK's state.
/// </para>
/// <para>
/// So this executable is both halves. Run with no arguments it is the orchestrator: it spawns
/// itself N times with <c>--role participant</c>, talks to each child over that child's own stdio,
/// and scripts the scenarios. Run with <c>--role participant</c> it is one player, running the
/// frame loop a real title runs.
/// </para>
/// <para>
/// Participants authenticate with <c>LoginWithCustomID</c>, which is what makes this practical on a
/// developer's machine: no signed-in Xbox account is needed, and N processes are simply N players.
/// </para>
/// </remarks>
internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        HarnessOptions options;
        try
        {
            options = HarnessOptions.Parse(args);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Bad command line: {ex.Message}");
            return 2;
        }

        try
        {
            if (options.Role == HarnessOptions.ParticipantRole)
            {
                using var participant = new Participant(options);
                return await participant.RunAsync().ConfigureAwait(false);
            }

            return await new Orchestrator(options).RunAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // A participant's stdout carries the protocol and nothing else, so its failures have to
            // go to stderr -- which the orchestrator forwards with the participant's name on it.
            Console.Error.WriteLine($"Unhandled {ex.GetType().Name}: {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);
            return 1;
        }
    }
}
