using System;
using System.Threading.Tasks;
using GDK.Net.GameUI;

namespace GDK.Net.UserSample.Demos;

/// <summary>
/// Title-rendered system UI. A game with its own art style can draw the Gaming Runtime's dialogs
/// itself instead of letting the system draw them.
/// </summary>
internal static class GameUiDemo
{
    public static async Task RunAsync(GameRuntime runtime)
    {
        Log.Write("");
        Log.Write("== Game UI ==");

        // Without this, ShowMessageDialogAsync draws the system dialog. With it, the runtime calls
        // back into the title and the title decides what to render.
        var dialogShown = new TaskCompletionSource<MessageDialogUiRequest>(
            TaskCreationOptions.RunContinuationsAsynchronously);

        CustomGameUi.SetHandlers(new CustomGameUiHandlers
        {
            MessageDialog = request => dialogShown.TrySetResult(request),
        });

        try
        {
            // The call a game already makes. It does not change just because the title is now
            // drawing the dialog.
            Task<MessageDialogButton> choice = runtime.GameUi.ShowMessageDialogAsync(
                "Custom UI",
                "Rendered by the title.",
                "First",
                "Second");

            // The handler runs on the thread pool, and the request stays answerable after it
            // returns — so a game can render the dialog over several frames and respond later.
            MessageDialogUiRequest request = await dialogShown.Task
                .WaitAsync(TimeSpan.FromSeconds(15))
                .ConfigureAwait(false);

            Log.Write($"  the runtime asked the title to show: \"{request.TitleText}\" / \"{request.ContentText}\"");
            request.Respond(MessageDialogButton.Second);

            Log.Write($"  ShowMessageDialogAsync returned {await choice.ConfigureAwait(false)}");
        }
        catch (TimeoutException)
        {
            Log.Write("  the message-dialog handler never ran");
        }
        finally
        {
            CustomGameUi.ClearHandlers();
        }
    }
}
