using System;
using System.Threading.Tasks;

namespace GDK.Net.LiveHarness;

/// <summary>
/// Live harness: runs the GDK.Net surface against the real Gaming Runtime and Xbox Live services.
/// </summary>
/// <remarks>
/// <para>
/// This must run as a packaged GDK title: the Gaming Runtime refuses to initialize in a process
/// without package identity. A packaged app has no attached console, so every result is written to
/// a JSON report as well as stdout, and <c>eng/run-package-tests.ps1</c> reads that report back.
/// </para>
/// <para>
/// The work itself lives in <see cref="CheckRegistry"/>; this file is only wiring. Nothing here
/// decides what runs or in what order, so a new check never means editing the entry point.
/// </para>
/// <para>
/// This project is deliberately dense: it is a test harness, and the reporting scaffolding is the
/// point. For readable, straight-line demonstrations of the same APIs, see
/// <c>samples/GDK.Net.UserSample</c>.
/// </para>
/// </remarks>
internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        var options = HarnessOptions.Parse(args);
        var report = new Report(options);
        var context = new CheckContext(report, options);

        try
        {
            await new CheckRunner(report, context).RunAsync(CheckRegistry.All(), options.Only)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // CheckRunner already isolates every check, so reaching here means the runner itself
            // broke. Recorded rather than thrown so the report still gets written.
            report.Fail("unhandled", ex);
        }
        finally
        {
            // Belt and braces for the handles the teardown checks own: if teardown was filtered out
            // by --only, or failed, the process should still not exit holding native handles.
            context.XboxLiveContext?.Dispose();
            context.User?.Dispose();
            context.Runtime?.Dispose();
        }

        report.Save();
        return report.Failed == 0 ? 0 : 1;
    }
}
