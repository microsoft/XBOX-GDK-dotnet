using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GDK.Net.MultiplayerHarness;

/// <summary>
/// A named multi-process test. Given the running participants, it drives them and either returns a
/// description of what it proved or throws.
/// </summary>
/// <param name="Name">The id used by <c>--only</c> and written to the report.</param>
/// <param name="MinimumParticipants">How many processes the scenario needs.</param>
/// <param name="RunAsync">The body.</param>
internal sealed record Scenario(
    string Name,
    int MinimumParticipants,
    Func<IReadOnlyList<ParticipantHandle>, Task<string>> RunAsync);

/// <summary>
/// Spawns the participants, logs them in, runs the scenarios in order and writes the report.
/// </summary>
/// <remarks>
/// The orchestrator holds no PlayFab state of its own -- it never initializes the Gaming Runtime or
/// PlayFab. Everything it knows it learned from a participant's reply. That is deliberate: it keeps
/// the thing under test entirely inside the child processes, so the harness cannot accidentally
/// prove something using a handle no real title would share.
/// </remarks>
internal sealed class Orchestrator
{
    private readonly HarnessOptions _options;
    private readonly List<ScenarioResult> _results = new();

    public Orchestrator(HarnessOptions options) => _options = options;

    public async Task<int> RunAsync()
    {
        Console.WriteLine($"GDK.Net multiplayer harness: {_options.Participants} participant(s), " +
                          $"PlayFab title {_options.TitleId}");
        Console.WriteLine(HarnessOptions.FindSecretKey() is null
            ? "  No developer secret key in the environment; participants will ask the client " +
              "login to create their accounts, which only works on a permissive title."
            : "  Developer secret key found; participant accounts are provisioned server-side.");

        var participants = new List<ParticipantHandle>();
        try
        {
            await StartParticipantsAsync(participants).ConfigureAwait(false);

            foreach (Scenario scenario in AllScenarios())
            {
                await RunScenarioAsync(scenario, participants).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Harness failed before the scenarios finished: {ex.Message}");
            _results.Add(new ScenarioResult("harness", "failed", ex.Message, 0));
        }
        finally
        {
            foreach (ParticipantHandle participant in participants)
            {
                await participant.StopAsync().ConfigureAwait(false);
                participant.Dispose();
            }
        }

        return WriteReport();
    }

    private async Task StartParticipantsAsync(List<ParticipantHandle> participants)
    {
        // A run stamp keeps each run's custom ids distinct, so a rerun is a fresh set of players
        // rather than the same accounts still holding the previous run's lobby membership.
        string stamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

        for (int i = 0; i < _options.Participants; i++)
        {
            participants.Add(ParticipantHandle.Start($"p{i}", _options));
        }

        foreach (ParticipantHandle participant in participants)
        {
            await participant.ReadyAsync(TimeSpan.FromSeconds(30)).ConfigureAwait(false);
        }

        // Logging in is done in parallel because it is a real round trip per participant, and
        // serialising it would put the whole run's latency on the critical path. The starts are
        // staggered slightly so a large participant count does not trip the title's per-second
        // request rate limit before the first attempt has even been made.
        var logins = new List<Task<Message>>();
        foreach (ParticipantHandle participant in participants)
        {
            if (logins.Count > 0)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(300)).ConfigureAwait(false);
            }

            logins.Add(participant.SendAsync(Verbs.Login, new Dictionary<string, string>
            {
                ["customId"] = $"gdknet-mp-{stamp}-{participant.Name}",
            }));
        }

        await Task.WhenAll(logins).ConfigureAwait(false);

        foreach (ParticipantHandle participant in participants)
        {
            Console.WriteLine($"  {participant.Name} is entity {participant.EntityId}");
        }

        await SettleAsync(participants.Count).ConfigureAwait(false);
    }

    /// <summary>
    /// Pauses after the login burst before the scenarios start.
    /// </summary>
    /// <remarks>
    /// Signing several players in at once trips the title's request rate limit, and the throttle
    /// that follows is applied to the whole title rather than to the participant that earned it --
    /// including to the Lobby service, whose operations then simply never complete rather than
    /// failing. Letting the limit's window drain before the first scenario is what keeps a run with
    /// more than a couple of participants deterministic.
    /// </remarks>
    private static async Task SettleAsync(int participants)
    {
        if (participants < 3)
        {
            return;
        }

        TimeSpan settle = TimeSpan.FromSeconds(5 * (participants - 2));
        Console.WriteLine($"  Letting the title's rate limit drain for {settle.TotalSeconds:0}s.");
        await Task.Delay(settle).ConfigureAwait(false);
    }

    private async Task RunScenarioAsync(
        Scenario scenario, IReadOnlyList<ParticipantHandle> participants)
    {
        if (_options.Only is string filter && !scenario.Name.StartsWith(filter, StringComparison.Ordinal))
        {
            return;
        }

        if (participants.Count < scenario.MinimumParticipants)
        {
            Console.WriteLine($"[skipped] {scenario.Name}: needs {scenario.MinimumParticipants} participants");
            _results.Add(new ScenarioResult(
                scenario.Name,
                "skipped",
                $"Needs {scenario.MinimumParticipants} participants; {participants.Count} were started.",
                0));
            return;
        }

        Console.WriteLine($"[running] {scenario.Name}");
        var clock = Stopwatch.StartNew();
        try
        {
            string detail = await scenario.RunAsync(participants).ConfigureAwait(false);
            clock.Stop();
            Console.WriteLine($"[passed ] {scenario.Name}: {detail} ({clock.ElapsedMilliseconds}ms)");
            _results.Add(new ScenarioResult(scenario.Name, "passed", detail, clock.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            clock.Stop();
            Console.WriteLine($"[failed ] {scenario.Name}: {ex.Message}");
            _results.Add(new ScenarioResult(scenario.Name, "failed", ex.Message, clock.ElapsedMilliseconds));
        }
    }

    private static IEnumerable<Scenario> AllScenarios()
    {
        yield return Scenarios.LobbyScenario.MembershipAndProperties;
        yield return Scenarios.PartyNetworkScenario.ConnectAndExchangeMessages;
    }

    private int WriteReport()
    {
        int failed = 0;
        int passed = 0;
        int skipped = 0;

        foreach (ScenarioResult result in _results)
        {
            switch (result.Outcome)
            {
                case "passed": passed++; break;
                case "failed": failed++; break;
                default: skipped++; break;
            }
        }

        var payload = new ReportPayload(
            DateTimeOffset.UtcNow,
            _options.TitleId,
            _options.Participants,
            passed,
            failed,
            skipped,
            _results);

        File.WriteAllText(
            _options.ReportPath,
            JsonSerializer.Serialize(payload, ReportJsonContext.Default.ReportPayload));

        Console.WriteLine();
        Console.WriteLine($"{passed} passed, {failed} failed, {skipped} skipped.");
        Console.WriteLine($"Report written to {_options.ReportPath}");
        return failed == 0 ? 0 : 1;
    }
}

/// <summary>One line of the report.</summary>
internal sealed record ScenarioResult(string Name, string Outcome, string Detail, long ElapsedMs);

/// <summary>The report <c>eng/run-local.ps1</c> and CI read back.</summary>
internal sealed record ReportPayload(
    DateTimeOffset CompletedUtc,
    string TitleId,
    int Participants,
    int Passed,
    int Failed,
    int Skipped,
    IReadOnlyList<ScenarioResult> Scenarios);

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(ReportPayload))]
internal sealed partial class ReportJsonContext : JsonSerializerContext;
