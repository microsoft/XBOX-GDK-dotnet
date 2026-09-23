using System;
using System.IO;

namespace GDK.Net.LiveHarness;

/// <summary>
/// Command line for the harness, passed through by <c>eng/run-package-tests.ps1</c>.
/// </summary>
/// <remarks>
/// There are deliberately no flags gating destructive behaviour. The harness runs against a title
/// provisioned for exactly this and an account that can be reset, so a flag that skips the writes
/// would only ever hide the calls most worth proving.
/// </remarks>
internal sealed class HarnessOptions
{
    private HarnessOptions(string outputDirectory, string? scid, string? only, bool resume, string playFabTitleId, string? playFabSecretKey)
    {
        OutputDirectory = outputDirectory;
        Scid = scid;
        Only = only;
        Resume = resume;
        PlayFabTitleId = playFabTitleId;
        PlayFabSecretKey = playFabSecretKey;
    }

    /// <summary>Where the report and any captured artefacts are written.</summary>
    public string OutputDirectory { get; }

    /// <summary>
    /// The title's Service Configuration ID, for the unusual case of a title whose SCID was assigned
    /// separately in Partner Center rather than derived from its title id. Leave it unset and the
    /// harness derives one, which is right for every title that has not been told otherwise.
    /// </summary>
    public string? Scid { get; }

    /// <summary>
    /// Runs only checks whose id starts with one of these comma-separated prefixes, for iterating on
    /// one family without paying for a whole run. Checks outside the filter are not run, so anything
    /// depending on them is skipped with a reason that says so rather than failing — which is why
    /// several prefixes are accepted: a family is rarely useful without the setup it depends on
    /// (<c>--only runtime,users,playfab</c>).
    /// </summary>
    public string? Only { get; }

    /// <summary>
    /// Carries forward the report from a previous run instead of starting a new one, running only
    /// the checks it does not already account for.
    /// </summary>
    /// <remarks>
    /// This exists because an access violation in the Gaming Runtime cannot be caught in .NET — the
    /// process dies mid-run and every check after the offending one goes unmeasured. The launcher
    /// relaunches with this flag until the run completes, which turns a crash from "the rest of the
    /// suite is unknown" into a single failed check.
    /// </remarks>
    public bool Resume { get; }

    /// <summary>
    /// The PlayFab title id the <c>playfab.*</c> family runs against. This is a PlayFab title,
    /// which is a different identifier from the Xbox title id in <c>MicrosoftGame.config</c>.
    /// </summary>
    public string PlayFabTitleId { get; }

    /// <summary>
    /// The title's developer secret key, which lets the title-entity checks run without a signed-in
    /// account. Read from the environment only — never from the command line, because a command
    /// line ends up in shell history and in CI logs.
    /// </summary>
    /// <remarks>
    /// The value is never written to the report: checks report only whether a key was present.
    /// </remarks>
    public string? PlayFabSecretKey { get; }

    /// <summary>The PlayFab REST endpoint derived from <see cref="PlayFabTitleId"/>.</summary>
    public string PlayFabApiEndpoint => $"https://{PlayFabTitleId}.playfabapi.com";

    public string ReportPath => Path.Combine(OutputDirectory, "report.json");

    public static HarnessOptions Parse(string[] args)
    {
        string? outputDirectory = null;
        string? only = null;
        bool resume = false;
        string? scid = Environment.GetEnvironmentVariable("GDKNET_SCID");
        string? playFabTitleId = Environment.GetEnvironmentVariable("GDKNET_PLAYFAB_TITLE_ID");

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--out" when i + 1 < args.Length:
                    outputDirectory = args[++i];
                    break;
                case "--scid" when i + 1 < args.Length:
                    scid = args[++i];
                    break;
                case "--playfab-title-id" when i + 1 < args.Length:
                    playFabTitleId = args[++i];
                    break;
                case "--only" when i + 1 < args.Length:
                    only = args[++i];
                    break;
                case "--resume":
                    resume = true;
                    break;
            }
        }

        outputDirectory ??= Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GDK.Net.LiveHarness");

        Directory.CreateDirectory(outputDirectory);
        return new HarnessOptions(
            outputDirectory,
            string.IsNullOrWhiteSpace(scid) ? null : scid,
            string.IsNullOrWhiteSpace(only) ? null : only,
            resume,
            string.IsNullOrWhiteSpace(playFabTitleId) ? DefaultPlayFabTitleId : playFabTitleId,
            ReadSecretKey());
    }

    /// <summary>
    /// The PlayFab title provisioned for this harness. Overridable, but hard-coded rather than
    /// required so that a plain <c>run-local.ps1</c> exercises the PlayFab family too.
    /// </summary>
    private const string DefaultPlayFabTitleId = "10D176";

    private static string? ReadSecretKey()
    {
        // The two names the PlayFab tooling itself uses, then the harness-specific one.
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
}
