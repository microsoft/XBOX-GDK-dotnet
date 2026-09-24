using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GDK.Net.MultiplayerHarness.Scenarios;

/// <summary>
/// The PlayFab Party round trip across processes: one host creates a network, the others connect to
/// it with the serialized descriptor, everyone creates an endpoint, and a message sent from one
/// process is received by the rest.
/// </summary>
/// <remarks>
/// <para>
/// Party is real peer transport, so this is the scenario that proves the projection's message path
/// end to end: <c>PartyEndpointSendMessage</c> on one process arriving as a
/// <c>PartyEndpointMessageReceivedStateChange</c> on another, with the payload intact through two
/// marshalling boundaries in opposite directions.
/// </para>
/// <para>
/// Note that the host connects to its own network. Creating a network does not join it:
/// <c>PartyCreateNewNetwork</c> only provisions it and hands back a descriptor, so every
/// participant runs the same connect-and-authenticate sequence.
/// </para>
/// </remarks>
internal static class PartyNetworkScenario
{
    /// <summary>
    /// Party's own connect and QoS work is slower than a service call, and slower still with
    /// several participants probing the same endpoints from one machine.
    /// </summary>
    private static readonly TimeSpan PartyTimeout = TimeSpan.FromSeconds(120);

    public static Scenario ConnectAndExchangeMessages { get; } = new(
        "party.network-messages",
        MinimumParticipants: 2,
        RunAsync: RunAsync);

    private static async Task<string> RunAsync(IReadOnlyList<ParticipantHandle> participants)
    {
        ParticipantHandle host = participants[0];

        // 0. Start every Party manager first. Region latency measurement begins when the manager
        //    does and a network cannot be created until it has reported, so doing this for all
        //    participants up front overlaps the probes instead of serialising them behind step 1.
        var prepared = new List<Task<Message>>();
        foreach (ParticipantHandle participant in participants)
        {
            prepared.Add(participant.SendAsync(Verbs.PartyPrepare, timeout: PartyTimeout));
        }

        await Task.WhenAll(prepared).ConfigureAwait(false);

        // 1. The host provisions a network. This is what waits on region latency measurement, so it
        //    is the slowest step in the scenario.
        Message network = await host.SendAsync(
            Verbs.PartyCreateNetwork,
            new Dictionary<string, string>
            {
                ["maxUsers"] = participants.Count.ToString(),
                ["maxDevices"] = participants.Count.ToString(),
            },
            PartyTimeout).ConfigureAwait(false);

        string descriptor = network.Value("descriptor")
            ?? throw new ScenarioException("The host created a network but reported no descriptor.");

        string region = network.Value("region") ?? "(unknown)";
        string? invitation = network.Value("invitation");

        var connectArgs = new Dictionary<string, string> { ["descriptor"] = descriptor };
        if (!string.IsNullOrEmpty(invitation))
        {
            connectArgs["invitation"] = invitation;
        }

        // 2. Everyone connects and authenticates, the host included.
        var connections = new List<Task<Message>>();
        foreach (ParticipantHandle participant in participants)
        {
            connections.Add(participant.SendAsync(Verbs.PartyConnect, connectArgs, PartyTimeout));
        }

        await Task.WhenAll(connections).ConfigureAwait(false);

        // 3. An endpoint is what makes a participant addressable; without one it is in the network
        //    but cannot be sent to.
        var endpoints = new List<Task<Message>>();
        foreach (ParticipantHandle participant in participants)
        {
            endpoints.Add(participant.SendAsync(Verbs.PartyCreateEndpoint, timeout: PartyTimeout));
        }

        await Task.WhenAll(endpoints).ConfigureAwait(false);

        // 4. Each process must see every endpoint, its own and the remote ones, before a broadcast
        //    means anything.
        var visibility = new List<Task<Message>>();
        foreach (ParticipantHandle participant in participants)
        {
            visibility.Add(participant.SendAsync(
                Verbs.PartyAwaitEndpoints,
                new Dictionary<string, string> { ["count"] = participants.Count.ToString() },
                PartyTimeout));
        }

        await Task.WhenAll(visibility).ConfigureAwait(false);

        // 5. The payload round trip. The receivers are armed first: a message sent before anyone is
        //    waiting would be delivered into a pump nobody is matching against.
        string text = $"hello-from-{host.Name}-{Guid.NewGuid():N}"[..24];

        var listeners = new List<Task<Message>>();
        for (int i = 1; i < participants.Count; i++)
        {
            listeners.Add(participants[i].SendAsync(
                Verbs.PartyAwaitMessage,
                new Dictionary<string, string> { ["text"] = text },
                PartyTimeout));
        }

        await host.SendAsync(
            Verbs.PartySend,
            new Dictionary<string, string> { ["text"] = text },
            PartyTimeout).ConfigureAwait(false);

        await Task.WhenAll(listeners).ConfigureAwait(false);

        // 6. And back the other way, so the test does not pass on a transport that only works from
        //    the network's creator.
        ParticipantHandle guest = participants[1];
        string reply = $"reply-from-{guest.Name}-{Guid.NewGuid():N}"[..24];

        Task<Message> hostListening = host.SendAsync(
            Verbs.PartyAwaitMessage,
            new Dictionary<string, string> { ["text"] = reply },
            PartyTimeout);

        await guest.SendAsync(
            Verbs.PartySend,
            new Dictionary<string, string> { ["text"] = reply },
            PartyTimeout).ConfigureAwait(false);

        await hostListening.ConfigureAwait(false);

        var departures = new List<Task<Message>>();
        foreach (ParticipantHandle participant in participants)
        {
            departures.Add(participant.SendAsync(Verbs.PartyLeave, timeout: PartyTimeout));
        }

        await Task.WhenAll(departures).ConfigureAwait(false);

        return $"network in {region}: {participants.Count} process(es) connected and created " +
               $"endpoints, a broadcast from {host.Name} reached them all, and {guest.Name}'s reply " +
               $"reached {host.Name}";
    }
}
