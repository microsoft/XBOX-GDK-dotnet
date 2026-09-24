using System;
using System.Threading.Tasks;
using GDK.Net.PlayFab;
using GDK.Net.PlayFab.Party;
using GDK.Net.PlayFabSample.Demos;

namespace GDK.Net.PlayFabSample;

/// <summary>
/// A minimal Microsoft GDK title that demonstrates the PlayFab half of the GDK.Net projection.
/// </summary>
/// <remarks>
/// <para>
/// PlayFab ships with the GDK as six flat C libraries (Core, Services, GameSave, Multiplayer,
/// Party and Party Xbox Live) totalling about a thousand exports. GDK.Net projects all of them.
/// This sample walks the shapes a title actually meets: authenticating, calling a service, and
/// pumping Party.
/// </para>
/// <para>
/// Everything here is ordinary C#: <c>Task</c> instead of <c>XAsyncBlock</c>, request and result
/// objects instead of packed native structs with count-plus-pointer pairs, exceptions instead of
/// <c>HRESULT</c>s and <c>IDisposable</c> instead of handle-close calls.
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
            // event registration, drains PlayFab and calls XGameRuntimeUninitialize, in that order.
            using GameRuntime runtime = GameRuntime.Initialize();
            Log.Write("Gaming Runtime initialized.");

            // PFInitialize. PlayFab keeps its own process-wide state, so it is initialized
            // separately -- but GameRuntime.Dispose shuts it down, because PlayFab's shutdown runs
            // on the process default task queue and so has to finish before the runtime goes away.
            PlayFabRuntime.Initialize();
            Log.Write($"PlayFab initialized (retry allowed: {PlayFabHttpSettings.AllowRetry}, " +
                      $"timeout window: {PlayFabHttpSettings.TimeoutWindowInSeconds}s).");

            // The service config names the title every later call is made against. Both it and the
            // entity are scoped so they are released before the shutdown above runs, even on a
            // failure: PFUninitializeAsync fails while any handle is outstanding.
            using PlayFabServiceConfig config = new(options.ApiEndpoint, options.TitleId);
            Log.Write($"Service config for title {config.TitleId} at {config.ApiEndpoint}.");

            using PlayFabEntity? entity = await LoginDemo
                .RunAsync(runtime, config, options)
                .ConfigureAwait(false);

            await ServicesDemo.RunAsync(config, entity).ConfigureAwait(false);
            PartyDemo.Run(options.TitleId, entity);

            Log.Write("");
            Log.Write("Done.");
            return 0;
        }
        catch (PlayFabException ex)
        {
            // PlayFab errors are their own exception type because they carry a PlayFab error code
            // and a service message, not just an HRESULT.
            Log.Write($"PlayFab call failed: {ex.Message} (HRESULT 0x{ex.HResultCode:X8})");
            return 1;
        }
        catch (PartyException ex)
        {
            Log.Write($"Party call failed: {ex.Message}");
            return 1;
        }
        catch (GameRuntimeException ex)
        {
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
