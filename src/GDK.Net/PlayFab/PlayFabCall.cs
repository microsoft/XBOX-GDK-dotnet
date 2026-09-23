using System;
using System.Threading;
using System.Threading.Tasks;

namespace GDK.Net.PlayFab;

/// <summary>
/// Ties the lifetime of a request's <see cref="PlayFabArena"/> to the native call that reads it.
/// </summary>
/// <remarks>
/// A PlayFab <c>...Async</c> entry point returns as soon as the request is queued; the native
/// library keeps reading the request graph until the HTTP round trip finishes. Releasing the arena
/// in a <c>finally</c> around the start call would therefore free memory still in use, so the arena
/// is released only once the operation completes, faults or is canceled.
/// </remarks>
internal static class PlayFabCall
{
    /// <summary>Runs a value-producing PlayFab call, releasing <paramref name="arena"/> afterwards.</summary>
    internal static async Task<TResult> InvokeAsync<TResult>(
        PlayFabArena arena,
        AsyncStarter starter,
        AsyncResultReader<TResult> reader,
        CancellationToken cancellationToken)
    {
        try
        {
            return await AsyncOperation<TResult>
                .RunAsync(IntPtr.Zero, starter, reader, cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            arena.Dispose();
        }
    }

    /// <summary>Runs a PlayFab call with no result, releasing <paramref name="arena"/> afterwards.</summary>
    internal static async Task InvokeAsync(
        PlayFabArena arena,
        AsyncStarter starter,
        AsyncCompleter completer,
        CancellationToken cancellationToken)
    {
        try
        {
            await AsyncOperation
                .RunAsync(IntPtr.Zero, starter, completer, cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            arena.Dispose();
        }
    }
}
