using System;
using System.Threading.Tasks;
using GDK.Net.UserSample.Demos;

namespace GDK.Net.UserSample;

/// <summary>
/// A minimal Microsoft GDK title that demonstrates the GDK.Net projection.
/// </summary>
/// <remarks>
/// <para>
/// Everything here is ordinary C#: properties instead of two-call size buffers, <c>Task</c>
/// instead of <c>XAsyncBlock</c>, exceptions instead of <c>HRESULT</c>s, <c>IDisposable</c>
/// instead of handle-close calls and events instead of registration tokens.
/// </para>
/// <para>
/// It must run as a packaged title: the Gaming Runtime refuses to initialize in a process without
/// package identity. See README.md for how to build and register the package.
/// </para>
/// <para>
/// This sample is for reading. The exhaustive pass/fail version, which writes a machine-readable
/// report for <c>eng/run-package-tests.ps1</c>, is <c>tests/GDK.Net.LiveHarness</c>.
/// </para>
/// </remarks>
internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        SampleOptions options = SampleOptions.Parse(args);

        try
        {
            // GameRuntime is the entry point and the lifetime owner: disposing it cleans up every
            // event registration and calls XGameRuntimeUninitialize.
            using GameRuntime runtime = GameRuntime.Initialize();

            Log.Write("Gaming Runtime initialized (operations resolve the process default task queue).");
            Log.Write($"XUser feature available: {runtime.IsFeatureAvailable(GameRuntimeFeature.User)}");

            await UserDemo.RunAsync(runtime, options).ConfigureAwait(false);
            await GameUiDemo.RunAsync(runtime).ConfigureAwait(false);
            ActivationDemo.Run(runtime);
            NetworkingDemo.Run(runtime);

            Log.Write("Done.");
            return 0;
        }
        catch (GameRuntimeException ex)
        {
            // Every native failure arrives as an exception. The numeric HRESULT is preserved for
            // diagnostics, but callers never have to check a return code.
            Log.Write($"GDK call failed: {ex.Message} (HRESULT 0x{ex.HResultCode:X8})");
            return 1;
        }
        catch (Exception ex)
        {
            Log.Write($"Unhandled {ex.GetType().Name}: {ex.Message}");
            return 1;
        }
        finally
        {
            Log.Write($"Log written to {Log.Path}");
        }
    }
}
