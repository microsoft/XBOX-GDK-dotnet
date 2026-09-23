using System;
using System.Collections.Generic;
using System.Linq;
using GDK.Net.Capture;
using GDK.Net.Package;

namespace GDK.Net.LiveHarness;

/// <summary>
/// Package identity, chunk availability, and the game DVR / capture surface.
/// </summary>
/// <remarks>
/// The two families share a file because they share a precondition and a shape: both are almost
/// entirely queries against the running package, and both are only meaningful in a packaged process.
/// </remarks>
internal static class PackageChecks
{
    public static IEnumerable<LiveCheck> All()
    {
        yield return LiveCheck.Sync("package.is-packaged", _ =>
            GamePackage.IsPackagedProcess
                ? "XPackageIsPackagedProcess = true"
                : throw new SkipCheckException(
                    "XPackageIsPackagedProcess = false. `wdapp register` without administrator " +
                    "rights registers the title as an Application rather than a Game, and XPackage " +
                    "does not recognise that as a packaged game process. Run the Install tier, or " +
                    "the Register tier from an elevated shell, to exercise this family."));

        yield return LiveCheck.Sync("package.identifier", ctx =>
        {
            string identifier = GamePackage.GetCurrentPackageIdentifier();
            ctx.State["package.id"] = identifier;
            return $"XPackageGetCurrentProcessPackageIdentifier = '{identifier}'";
        }, "package.is-packaged");

        yield return LiveCheck.Sync("package.user-locale", _ =>
            $"XPackageGetUserLocale = '{GamePackage.GetUserLocale()}'", "package.is-packaged");

        yield return LiveCheck.Sync("package.write-stats", _ =>
        {
            PackageWriteStats stats = GamePackage.GetWriteStats();
            return $"XPackageGetWriteStats: budget={stats.Budget} elapsed={stats.Elapsed} " +
                   $"interval={stats.Interval} bytesWritten={stats.BytesWritten}";
        }, "package.is-packaged");

        yield return LiveCheck.Sync("package.enumerate", ctx =>
        {
            IReadOnlyList<PackageInfo> packages = GamePackage.EnumeratePackages();
            string self = ctx.Get<string>("package.id");

            return packages.Any(p => p.PackageIdentifier == self)
                ? $"XPackageEnumeratePackages returned {packages.Count}, including this title: " +
                  string.Join(", ", packages.Select(p => p.DisplayName))
                : throw new InvalidOperationException(
                    $"XPackageEnumeratePackages returned {packages.Count} package(s) but not the " +
                    $"running title '{self}'.");
        }, "package.identifier");

        yield return LiveCheck.Sync("package.features", ctx =>
        {
            IReadOnlyList<PackageFeature> features =
                GamePackage.EnumerateFeatures(ctx.Get<string>("package.id"));
            return features.Count == 0
                ? "XPackageEnumerateFeatures returned none (expected unless the game config declares features)"
                : $"XPackageEnumerateFeatures returned {features.Count}: " +
                  string.Join(", ", features.Select(f => f.Id));
        }, "package.identifier");

        yield return LiveCheck.Sync("package.chunk-availability", ctx =>
        {
            IReadOnlyList<PackageChunkAvailabilityInfo> chunks = GamePackage.EnumerateChunkAvailability(
                ctx.Get<string>("package.id"),
                PackageChunkSelectorType.Chunk);

            return chunks.Count == 0
                ? "XPackageEnumerateChunkAvailability returned none (expected for a single-chunk package)"
                : $"XPackageEnumerateChunkAvailability returned {chunks.Count}: " +
                  string.Join(", ", chunks.Select(c => $"{c.Availability}"));
        }, "package.identifier");

        yield return LiveCheck.Sync("package.installed-event", _ =>
        {
            // Registration round-trip only: firing it needs another package to finish installing,
            // which is not something the harness can arrange within a run.
            void Handler(object? sender, PackageInstalledEventArgs e) { }

            GamePackage.PackageInstalled += Handler;
            GamePackage.PackageInstalled -= Handler;

            return "XPackageRegisterPackageInstalled and its unregister counterpart round-tripped";
        }, "package.is-packaged");

        foreach (LiveCheck check in CaptureFamily())
        {
            yield return check;
        }
    }

    /// <summary>
    /// Game DVR. The metadata calls are writes, and they run: they are cheap, they are scoped to the
    /// title's own clips, and the storage-remaining query below them only means anything once
    /// something has actually been written.
    /// </summary>
    private static IEnumerable<LiveCheck> CaptureFamily()
    {
        yield return LiveCheck.Sync("capture.broadcast-status", ctx =>
        {
            // Before the call, not after: on PC this does not fail politely, it access-violates.
            DeviceGate.RequireConsole("XAppBroadcastGetStatus");

            BroadcastStatus status = ctx.RequireRuntime.Capture.GetBroadcastStatus(ctx.RequireUser);
            bool broadcasting = ctx.RequireRuntime.Capture.IsAppBroadcasting();
            return $"XAppBroadcastGetStatus: canStart={status.CanStartBroadcast} " +
                   $"isAnyAppBroadcasting={status.IsAnyAppBroadcasting} " +
                   $"captureResourceUnavailable={status.IsCaptureResourceUnavailable} " +
                   $"disabledByUser={status.IsDisabledByUser} disabledBySystem={status.IsDisabledBySystem}; " +
                   $"XAppBroadcastIsAppBroadcasting={broadcasting}";
        }, "users.add");

        yield return LiveCheck.Sync("capture.video-settings", ctx =>
        {
            DeviceGate.RequireConsole("XAppCaptureGetVideoCaptureSettings");

            VideoCaptureSettings settings = ctx.RequireRuntime.Capture.GetVideoCaptureSettings();
            return $"XAppCaptureGetVideoCaptureSettings: {settings.Width}x{settings.Height} " +
                   $"encoding={settings.Encoding} colorFormat={settings.ColorFormat} " +
                   $"maxClipMs={settings.MaxRecordTimespanDurationInMs} " +
                   $"captureByGamesAllowed={settings.IsCaptureByGamesAllowed}";
        }, "runtime.initialize");

        yield return LiveCheck.Sync("capture.metadata", ctx =>
        {
            AppCaptureManager capture = ctx.RequireRuntime.Capture;

            capture.AddMetadataString("harness.run", DateTime.UtcNow.ToString("O"));
            capture.AddMetadataInt32("harness.checkIndex", 1);
            capture.AddMetadataDouble("harness.score", 1.5);

            capture.StartMetadataStringState("harness.phase", "capture");
            capture.StartMetadataInt32State("harness.level", 1);
            capture.StartMetadataDoubleState("harness.health", 100.0);
            capture.StopMetadataState("harness.phase");
            capture.StopAllMetadataStates();

            ulong remaining = capture.GetMetadataRemainingStorageBytes();
            return "XAppCaptureAddStringEvent, AddInt32Event, AddDoubleEvent, the three " +
                   "StartStringState/Int32State/DoubleState forms, StopState and StopAllStates all " +
                   $"succeeded; XAppCaptureGetMetadataRemainingStorage = {remaining} bytes";
        }, "runtime.initialize");

        yield return LiveCheck.Sync("capture.diagnostic-screenshot", ctx =>
        {
            try
            {
                DiagnosticScreenshotResult result = ctx.RequireRuntime.Capture.TakeDiagnosticScreenshot(
                    gamescreenOnly: true,
                    AppCaptureScreenshotFormatFlag.Sdr);
                return $"XAppCaptureTakeDiagnosticScreenshot: {result}";
            }
            catch (GameRuntimeException ex) when (
                ex.HResultCode == unchecked((int)0x80004003) ||   // E_POINTER
                ex.HResultCode == unchecked((int)0x80070490))     // ERROR_NOT_FOUND
            {
                throw new SkipCheckException(
                    "XAppCaptureTakeDiagnosticScreenshot is documented as working on development " +
                    "kits only, and returns immediately on a retail kit. It also has nothing to " +
                    "capture from this harness, which renders no game screen. The failing HRESULT " +
                    $"varies between runs; this one was 0x{ex.HResultCode:X8}.");
            }
        }, "runtime.initialize");

        yield return LiveCheck.Sync("capture.broadcast-changed-event", ctx =>
        {
            void Handler(object? sender, EventArgs e) { }

            AppCaptureManager capture = ctx.RequireRuntime.Capture;
            capture.BroadcastingChanged += Handler;
            capture.BroadcastingChanged -= Handler;
            capture.MetadataPurged += Handler;
            capture.MetadataPurged -= Handler;

            return "XAppBroadcastRegisterIsAppBroadcastingChanged and " +
                   "XAppCaptureRegisterMetadataPurged, with their unregister counterparts, round-tripped";
        }, "runtime.initialize");
    }
}
