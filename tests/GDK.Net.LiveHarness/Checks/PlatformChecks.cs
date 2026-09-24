using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Activation;
using GDK.Net.GameUI;
using GDK.Net.Networking;

namespace GDK.Net.LiveHarness;

/// <summary>
/// Platform surface that needs no Xbox Live service: custom game UI, activation, networking.
/// </summary>
internal static class PlatformChecks
{
    private const MessageDialogButton Answer = MessageDialogButton.Second;

    private static readonly TimeSpan UiTimeout = TimeSpan.FromSeconds(15);

    /// <summary>
    /// A pending activation is replayed on registration rather than raised live, so a short wait
    /// after subscribing is enough to observe one if the title was launched from an invite. The
    /// callback arrives on the process default queue's thread pool, so there is nothing to pump.
    /// </summary>
    private static readonly TimeSpan SettleDuration = TimeSpan.FromMilliseconds(50);

    public static IEnumerable<LiveCheck> All()
    {
        yield return LiveCheck.Async("gameui.custom-ui-roundtrip", VerifyCustomGameUiAsync, "runtime.initialize");

        yield return LiveCheck.Sync("activation.unified-event", ctx =>
        {
            var seen = new List<string>();
            void OnActivated(object? _, GameActivationEventArgs e) => seen.Add($"{e.Kind}:{e.Uri}");

            ctx.RequireRuntime.Activation.Activated += OnActivated;
            try
            {
                Thread.Sleep(SettleDuration);
            }
            finally
            {
                ctx.RequireRuntime.Activation.Activated -= OnActivated;
            }

            return seen.Count == 0
                ? "XGameActivationRegisterForEvent registered through the shim; no activation replayed " +
                  "(expected unless the title was launched from a protocol, file or invite)"
                : $"XGameActivationRegisterForEvent reported {seen.Count}: {string.Join("; ", seen)}";
        }, "runtime.initialize");


        yield return LiveCheck.Sync("networking.configuration-setting", ctx =>
        {
            ulong value = ctx.RequireRuntime.Networking.QueryConfigurationSetting(
                NetworkingConfigurationSetting.MaxTitleTcpQueuedReceiveBufferSize);
            return $"XNetworkingQueryConfigurationSetting = {value}";
        }, "runtime.initialize");

        yield return LiveCheck.Sync("networking.connectivity-hint", ctx =>
        {
            NetworkingConnectivityHint hint = ctx.RequireRuntime.Networking.GetConnectivityHint();
            return $"XNetworkingGetConnectivityHint: level={hint.ConnectivityLevel} " +
                   $"cost={hint.ConnectivityCost} iana={hint.IanaInterfaceType} " +
                   $"initialized={hint.NetworkInitialized} approachingDataLimit={hint.ApproachingDataLimit} " +
                   $"overDataLimit={hint.OverDataLimit} roaming={hint.Roaming}";
        }, "runtime.initialize");
    }

    /// <summary>
    /// Proves the title-implemented UI path end to end.
    /// </summary>
    /// <remarks>
    /// Registration (<c>XGameUiSetUiCallbacks</c>) and display
    /// (<c>XGameUiShowMessageDialogAsync</c>) are separate entry points, and nothing in the headers
    /// promises that a table registered through one is observed by the other. Disassembly says it
    /// is (<c>XGameUiSetUiCallbacks</c> is only a <c>QueryApiImpl</c> lookup plus a virtual call,
    /// so the registration is held by the Gaming Runtime rather than in module-static state) but
    /// that is an inference from generated code. This check is the empirical test.
    /// </remarks>
    private static async Task<string> VerifyCustomGameUiAsync(CheckContext ctx)
    {
        // Completed by the handler, which the projection runs on the thread pool rather than on the
        // runtime's callback thread. Responding from *this* thread afterwards is therefore not
        // required for correctness any more, but it is still the more demanding shape to prove:
        // it exercises the native contract's promise that the callback handle stays answerable
        // long after the callback returned, and from a different thread than the one that got it.
        var received = new TaskCompletionSource<MessageDialogUiRequest>(
            TaskCreationOptions.RunContinuationsAsynchronously);

        CustomGameUi.SetHandlers(new CustomGameUiHandlers
        {
            MessageDialog = request => received.TrySetResult(request),
        });

        try
        {
            Task<MessageDialogButton> pending = ctx.RequireRuntime.GameUi
                .ShowMessageDialogAsync("Custom UI", "Rendered by the title.", "First", "Second");

            MessageDialogUiRequest seen;
            try
            {
                seen = await received.Task.WaitAsync(UiTimeout).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                throw new InvalidOperationException(
                    $"The message-dialog handler did not run within {UiTimeout.TotalSeconds:N0}s. " +
                    "The table registered through XGameUiSetUiCallbacks appears not to be visible " +
                    "to XGameUiShowMessageDialogAsync, so the Gaming Runtime does not hold the " +
                    "registration after all.");
            }

            seen.Respond(Answer);

            MessageDialogButton chosen;
            try
            {
                chosen = await pending.WaitAsync(UiTimeout).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                throw new InvalidOperationException(
                    "The handler ran and XGameUiSetMessageDialogUiResponse returned success, but " +
                    $"XGameUiShowMessageDialogAsync did not complete within {UiTimeout.TotalSeconds:N0}s.");
            }

            if (chosen != Answer)
            {
                throw new InvalidOperationException(
                    $"The handler responded {Answer} but XGameUiShowMessageDialogResult reported {chosen}.");
            }

            return
                $"Handler ran across the module boundary and its answer ({chosen}) round-tripped: " +
                $"registered via the shim, invoked via the thunks DLL. " +
                $"Title text seen by the handler: \"{seen.TitleText}\".";
        }
        finally
        {
            CustomGameUi.ClearHandlers();
        }
    }
}
