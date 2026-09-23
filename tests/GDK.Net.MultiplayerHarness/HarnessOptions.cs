using System;
using System.IO;

namespace GDK.Net.MultiplayerHarness;

/// <summary>Command line for both roles.</summary>
internal sealed class HarnessOptions
{
    /// <summary>The PlayFab title the participants authenticate against.</summary>
    public const string DefaultTitleId = "10D176";

    /// <summary>The orchestrator: spawns participants and runs the scenarios.</summary>
    public const string OrchestratorRole = "orchestrator";

    /// <summary>A participant: one player, driven over stdio.</summary>
    public const string ParticipantRole = "participant";

    private HarnessOptions(
        string role,
        string participantName,
        string titleId,
        int participants,
        string outputDirectory,
        string? only,
        bool verbose)
    {
        Role = role;
        ParticipantName = participantName;
        TitleId = titleId;
        Participants = participants;
        OutputDirectory = outputDirectory;
        Only = only;
        Verbose = verbose;
    }

    public string Role { get; }

    /// <summary>This participant's name. Meaningless in the orchestrator.</summary>
    public string ParticipantName { get; }

    public string TitleId { get; }

    /// <summary>How many participant processes to spawn.</summary>
    public int Participants { get; }

    public string OutputDirectory { get; }

    /// <summary>A scenario name prefix filter, or <see langword="null"/> to run them all.</summary>
    public string? Only { get; }

    /// <summary>Echo every protocol line, for debugging the harness itself.</summary>
    public bool Verbose { get; }

    public string ApiEndpoint => $"https://{TitleId}.playfabapi.com";

    /// <summary>Where the orchestrator writes the machine-readable result.</summary>
    public string ReportPath => Path.Combine(OutputDirectory, "mp-report.json");

    /// <summary>
    /// The developer secret key, from the environment, or <see langword="null"/> if there is none.
    /// </summary>
    /// <remarks>
    /// A title secret is a server credential, so it is only ever read from the environment and
    /// never echoed to the console, the protocol or the report -- only its presence is reported.
    /// The names match the ones <c>tests/GDK.Net.LiveHarness</c> accepts.
    /// </remarks>
    public static string? FindSecretKey()
    {
        foreach (string name in new[]
        {
            "GDKNET_PLAYFAB_SECRET_KEY",
            "PLAYFAB_DEVELOPER_SECRET_KEY",
            "PLAYFAB_SECRET_KEY",
        })
        {
            string? value = Environment.GetEnvironmentVariable(name);
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
        }

        return null;
    }

    public static HarnessOptions Parse(string[] args)
    {
        string role = OrchestratorRole;
        string name = "participant";
        string? titleId = null;
        int participants = 2;
        string? outputDirectory = null;
        string? only = null;
        bool verbose = false;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--role" when i + 1 < args.Length:
                    role = args[++i];
                    break;
                case "--name" when i + 1 < args.Length:
                    name = args[++i];
                    break;
                case "--title-id" when i + 1 < args.Length:
                    titleId = args[++i];
                    break;
                case "--participants" when i + 1 < args.Length:
                    participants = int.Parse(args[++i]);
                    break;
                case "--out" when i + 1 < args.Length:
                    outputDirectory = args[++i];
                    break;
                case "--only" when i + 1 < args.Length:
                    only = args[++i];
                    break;
                case "--verbose":
                    verbose = true;
                    break;
            }
        }

        outputDirectory ??= Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GDK.Net.MultiplayerHarness");

        titleId ??= Environment.GetEnvironmentVariable("GDKNET_PLAYFAB_TITLE_ID") ?? DefaultTitleId;

        if (participants < 2)
        {
            throw new ArgumentException(
                "A multiplayer scenario needs at least two participants.", nameof(args));
        }

        Directory.CreateDirectory(outputDirectory);
        return new HarnessOptions(
            role, name, titleId, participants, outputDirectory, only, verbose);
    }
}
