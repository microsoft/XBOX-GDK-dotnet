using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDK.Net.SystemInfo;

namespace GDK.Net.LiveHarness;

/// <summary>
/// Runs a check list, isolating every check and skipping those whose prerequisites did not pass.
/// </summary>
/// <remarks>
/// <para>
/// The point of this type is that <b>a failing check never stops the run</b>. The previous harness
/// called its check families in an unguarded sequence, so the first exception aborted everything
/// after it: one broken API hid the state of every other one, which is precisely backwards for a
/// harness whose job is to report coverage.
/// </para>
/// <para>
/// Prerequisites are the other half of that. Isolation alone would turn one failure into a screen of
/// identical downstream errors, every Xbox Live check failing with "no context" tells you nothing
/// the first one did not. A check whose prerequisite did not pass is skipped and says which
/// prerequisite, so the report has exactly one line describing the actual problem.
/// </para>
/// </remarks>
internal sealed class CheckRunner
{
    private readonly Report _report;
    private readonly CheckContext _context;
    private readonly HashSet<string> _passed = new(StringComparer.Ordinal);
    private readonly HashSet<string> _ran = new(StringComparer.Ordinal);

    public CheckRunner(Report report, CheckContext context)
    {
        _report = report;
        _context = context;

        // Results carried over from a crashed run count as having happened: they keep prerequisite
        // resolution honest and stop the relaunch loop repeating work it already has answers for.
        foreach (KeyValuePair<string, string> prior in report.Prior)
        {
            _ran.Add(prior.Key);
            if (prior.Value == "passed")
            {
                _passed.Add(prior.Key);
            }
        }
    }

    public async Task RunAsync(IEnumerable<LiveCheck> checks, string? onlyPrefix)
    {
        // A comma-separated list, because a family on its own is rarely runnable: it needs the
        // setup checks it declares as prerequisites (--only runtime,users,playfab).
        string[]? prefixes = onlyPrefix
            ?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (LiveCheck check in checks)
        {
            if (prefixes is { Length: > 0 } &&
                !prefixes.Any(p => check.Id.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
            {
                continue;
            }

            if (_report.Prior.ContainsKey(check.Id) && !check.Replay)
            {
                continue;
            }

            _ran.Add(check.Id);

            string? blocker = check.Requires.FirstOrDefault(r => !_passed.Contains(r));
            if (blocker is not null)
            {
                // Distinguish "the prerequisite failed" from "the prerequisite was filtered out",
                // because with --only the second is expected and says nothing about the API.
                string reason = _ran.Contains(blocker)
                    ? $"Prerequisite '{blocker}' did not pass."
                    : $"Prerequisite '{blocker}' was not part of this run.";

                _report.Skip(check.Id, reason);
                continue;
            }

            await RunOneAsync(check).ConfigureAwait(false);
        }
    }

    private async Task RunOneAsync(LiveCheck check)
    {
        _report.Begin(check.Id);

        try
        {
            string detail = await check.Run(_context).ConfigureAwait(false);
            _report.Pass(check.Id, detail);
            _passed.Add(check.Id);
        }
        catch (SkipCheckException skip)
        {
            // A check can decide at run time that its subject is not applicable, no SCID, no
            // network, an optional feature the console does not have. That is not a failure, and
            // reporting it as one would train the reader to ignore red.
            _report.Skip(check.Id, skip.Message);
            _passed.Remove(check.Id);
        }
        catch (Exception ex)
        {
            _report.Fail(check.Id, ex);
            _passed.Remove(check.Id);
        }
    }
}

/// <summary>
/// Thrown by a check that has determined it cannot run, to record a skip rather than a failure.
/// </summary>
internal sealed class SkipCheckException : Exception
{
    public SkipCheckException(string message)
        : base(message)
    {
    }
}

/// <summary>
/// Guards for service calls whose failure means the title is not configured yet, rather than that
/// the projection is wrong.
/// </summary>
/// <remarks>
/// A title that has not been published, or has no leaderboard or stat rule defined, makes the
/// service answer 404 or 400. Reporting those as failures trains the reader to ignore red, and the
/// interesting signal (that the call reached the service, authenticated, and got a well-formed
/// answer) is lost. The HRESULT is still named in the skip so a genuinely new failure cannot hide
/// behind the same branch.
/// </remarks>
internal static class ServiceGate
{
    public const int HttpBadRequest = unchecked((int)0x80190190);
    public const int HttpNotFound = unchecked((int)0x80190194);

    public static SkipCheckException NotConfigured(GameRuntimeException ex, string explanation) =>
        new($"{explanation} (service returned 0x{ex.HResultCode:X8}).");
}

/// <summary>
/// Guards for APIs that only exist on some device classes.
/// </summary>
internal static class DeviceGate
{
    /// <summary>
    /// Skips the current check unless it is running on an Xbox console.
    /// </summary>
    /// <remarks>
    /// Several families are console-only and behave differently on PC rather than reporting
    /// themselves absent. Sign-out and the capture settings return <c>E_NOINTERFACE</c>, which is at
    /// least diagnosable; <c>XAppBroadcastGetStatus</c> takes the process down with an access
    /// violation. So this has to be checked <b>before</b> the call, not inferred from its result.
    /// </remarks>
    public static void RequireConsole(string api)
    {
        SystemDeviceType device = GameSystem.DeviceType;
        if (device == SystemDeviceType.Pc || device == SystemDeviceType.Unknown)
        {
            throw new SkipCheckException(
                $"{api} is console-only; this is a {device} and the API is not implemented here.");
        }
    }
}
