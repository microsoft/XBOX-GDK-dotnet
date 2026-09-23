using System;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab;

/// <summary>
/// Identifies the PlayFab title and API endpoint a login is made against. Wraps
/// <c>PFServiceConfigHandle</c>.
/// </summary>
public sealed unsafe class PlayFabServiceConfig : IDisposable
{
    private readonly PlayFabServiceConfigHandle _handle;
    private bool _disposed;

    /// <summary>
    /// Creates a service configuration for a title (<c>PFServiceConfigCreateHandle</c>).
    /// </summary>
    /// <param name="apiEndpoint">
    /// The title's PlayFab API endpoint, for example <c>https://ABCDE.playfabapi.com</c>.
    /// </param>
    /// <param name="titleId">The title's PlayFab title id.</param>
    public PlayFabServiceConfig(string apiEndpoint, string titleId)
    {
        if (apiEndpoint is null)
        {
            throw new ArgumentNullException(nameof(apiEndpoint));
        }

        if (titleId is null)
        {
            throw new ArgumentNullException(nameof(titleId));
        }

        using var arena = new PlayFabArena();
        IntPtr raw;
        Hr.ThrowIfFailed(
            NativePlayFab.PFServiceConfigCreateHandle(arena.String(apiEndpoint), arena.String(titleId), &raw));
        _handle = new PlayFabServiceConfigHandle(raw);
    }

    private PlayFabServiceConfig(IntPtr rawHandle)
    {
        _handle = new PlayFabServiceConfigHandle(rawHandle);
    }

    /// <summary>Takes ownership of a raw <c>PFServiceConfigHandle</c>.</summary>
    internal static PlayFabServiceConfig FromRawHandle(IntPtr rawHandle) => new(rawHandle);

    /// <summary>The configured PlayFab API endpoint (<c>PFServiceConfigGetAPIEndpoint</c>).</summary>
    public string ApiEndpoint => PlayFabInterop.GetString(
        Handle,
        NativePlayFab.PFServiceConfigGetAPIEndpointSize,
        NativePlayFab.PFServiceConfigGetAPIEndpoint);

    /// <summary>The configured PlayFab title id (<c>PFServiceConfigGetTitleId</c>).</summary>
    public string TitleId => PlayFabInterop.GetString(
        Handle,
        NativePlayFab.PFServiceConfigGetTitleIdSize,
        NativePlayFab.PFServiceConfigGetTitleId);

    /// <summary>The raw <c>PFServiceConfigHandle</c>. Only valid while this instance is alive.</summary>
    internal IntPtr Handle
    {
        get
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PlayFabServiceConfig));
            }

            return _handle.DangerousGetHandle();
        }
    }

    /// <summary>
    /// Returns an independent instance backed by its own native handle
    /// (<c>PFServiceConfigDuplicateHandle</c>).
    /// </summary>
    public PlayFabServiceConfig Duplicate()
    {
        IntPtr duplicate;
        Hr.ThrowIfFailed(NativePlayFab.PFServiceConfigDuplicateHandle(Handle, &duplicate));
        return new PlayFabServiceConfig(duplicate);
    }

    /// <summary>Releases the native handle.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle.Dispose();
    }
}
