using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GDK.Net.MultiplayerHarness.Scenarios;

/// <summary>
/// The PlayFab Lobby round trip across processes: one host creates a lobby, everyone else joins it
/// by connection string, and a property the host posts reaches every guest.
/// </summary>
/// <remarks>
/// This is the scenario that cannot be written in a single process. A lobby's membership and its
/// property fan-out are service-side state distributed to each client's own
/// <c>PFMultiplayerStartProcessingLobbyStateChanges</c> queue, so "did the guest actually see it"
/// is only a real question when the guest is a different process with its own SDK instance.
/// </remarks>
internal static class LobbyScenario
{
    public static Scenario MembershipAndProperties { get; } = new(
        "lobby.membership-and-properties",
        MinimumParticipants: 2,
        RunAsync: RunAsync);

    private static async Task<string> RunAsync(IReadOnlyList<ParticipantHandle> participants)
    {
        ParticipantHandle host = participants[0];

        // 1. The host creates the lobby and hands back the connection string, which is the only
        //    thing a joiner needs. The lobby is sized with headroom rather than to exactly the
        //    participant count so that a run's capacity is not itself a variable under test.
        Message created = await host.SendAsync(Verbs.LobbyCreate, new Dictionary<string, string>
        {
            ["maxMembers"] = Math.Max(participants.Count, 8).ToString(),
        }).ConfigureAwait(false);

        string connectionString = created.Value("connectionString")
            ?? throw new ScenarioException("The host created a lobby but reported no connection string.");

        string lobbyId = created.Value("lobbyId") ?? "(unknown)";

        // 2. Every guest joins. Joins are issued one at a time rather than concurrently: the Lobby
        //    service serialises membership changes to a single lobby, and two joins in flight at
        //    once simply never complete rather than one of them losing and reporting a conflict.
        for (int i = 1; i < participants.Count; i++)
        {
            await participants[i].SendAsync(Verbs.LobbyJoin, new Dictionary<string, string>
            {
                ["connectionString"] = connectionString,
            }).ConfigureAwait(false);
        }

        // 3. Everyone, host included, has to observe the full membership. Asking each process
        //    separately is the assertion: joining is not the same as the others noticing.
        var memberships = new List<Task<Message>>();
        foreach (ParticipantHandle participant in participants)
        {
            memberships.Add(participant.SendAsync(Verbs.LobbyAwaitMembers, new Dictionary<string, string>
            {
                ["count"] = participants.Count.ToString(),
            }));
        }

        await Task.WhenAll(memberships).ConfigureAwait(false);

        // 4. The host changes a lobby property -- "the map changed" -- and every guest must see the
        //    new value arrive on its own queue.
        string value = $"map-{Guid.NewGuid():N}"[..12];
        await host.SendAsync(Verbs.LobbyPostUpdate, new Dictionary<string, string>
        {
            ["key"] = "map",
            ["value"] = value,
        }).ConfigureAwait(false);

        var propagations = new List<Task<Message>>();
        foreach (ParticipantHandle participant in participants)
        {
            propagations.Add(participant.SendAsync(Verbs.LobbyAwaitProperty, new Dictionary<string, string>
            {
                ["key"] = "map",
                ["value"] = value,
            }));
        }

        await Task.WhenAll(propagations).ConfigureAwait(false);

        // 5. Leaving cleanly matters: a lobby the members never left keeps the entities in it, and
        //    the next run would find them still there.
        var departures = new List<Task<Message>>();
        foreach (ParticipantHandle participant in participants)
        {
            departures.Add(participant.SendAsync(Verbs.LobbyLeave));
        }

        await Task.WhenAll(departures).ConfigureAwait(false);

        return $"lobby {lobbyId}: {participants.Count} process(es) joined, all observed the full " +
               $"membership and the host's '{value}' property, then all left";
    }
}
