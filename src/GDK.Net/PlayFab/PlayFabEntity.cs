using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab;

/// <summary>
/// An authenticated PlayFab entity — the credential every Services call is made against. Wraps
/// <c>PFEntityHandle</c>.
/// </summary>
/// <remarks>
/// <para>
/// Instances come from a login (<see cref="Authentication"/>) or from
/// <see cref="PlayFabLocalUser.LoginAsync"/>; the projection never hands out the raw handle. The
/// handle is owned by a <see cref="System.Runtime.InteropServices.SafeHandle"/>, so a missed
/// <see cref="Dispose"/> still releases it at finalization.
/// </para>
/// <para>
/// The entity token is refreshed by the PlayFab library in the background; use
/// <see cref="GetEntityTokenAsync"/> to read the current token.
/// </para>
/// </remarks>
public sealed unsafe class PlayFabEntity : IDisposable
{
    private readonly PlayFabEntityHandle _handle;
    private bool _disposed;

    internal PlayFabEntity(IntPtr rawHandle)
    {
        _handle = new PlayFabEntityHandle(rawHandle);
    }

    /// <summary>The entity's id and type (<c>PFEntityGetEntityKey</c>).</summary>
    public EntityKey Key
    {
        get
        {
            IntPtr handle = Handle;
            nuint size;
            Hr.ThrowIfFailed(NativePlayFab.PFEntityGetEntityKeySize(handle, &size));

            IntPtr buffer = Marshal.AllocHGlobal(checked((int)size));
            try
            {
                PFEntityKey* key;
                nuint used;
                Hr.ThrowIfFailed(
                    NativePlayFab.PFEntityGetEntityKey(handle, size, (void*)buffer, &key, &used));
                return EntityKey.FromNative(key);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
    }

    /// <summary>Whether this entity is a title player (<c>PFEntityIsTitlePlayer</c>).</summary>
    public bool IsTitlePlayer
    {
        get
        {
            byte value;
            Hr.ThrowIfFailed(NativePlayFab.PFEntityIsTitlePlayer(Handle, &value));
            return value != 0;
        }
    }

    /// <summary>The PlayFab API endpoint this entity authenticated against (<c>PFEntityGetAPIEndpoint</c>).</summary>
    public string ApiEndpoint => PlayFabInterop.GetString(
        Handle,
        NativePlayFab.PFEntityGetAPIEndpointSize,
        NativePlayFab.PFEntityGetAPIEndpoint);

    /// <summary>The PlayFab title id this entity authenticated against (<c>PFEntityGetTitleId</c>).</summary>
    public string TitleId => PlayFabInterop.GetString(
        Handle,
        NativePlayFab.PFEntityGetTitleIdSize,
        NativePlayFab.PFEntityGetTitleId);

    /// <summary>
    /// The developer secret key this entity was created with (<c>PFEntityGetSecretKey</c>), or an
    /// empty string for a client entity.
    /// </summary>
    /// <remarks>Only server entities carry a secret key; it must never be shipped in a client.</remarks>
    public string SecretKey => PlayFabInterop.GetString(
        Handle,
        NativePlayFab.PFEntityGetSecretKeySize,
        NativePlayFab.PFEntityGetSecretKey);

    /// <summary>
    /// The raw <c>PFEntityHandle</c>. Only valid while this instance is alive and undisposed.
    /// </summary>
    internal IntPtr Handle
    {
        get
        {
            ThrowIfDisposed();
            return _handle.DangerousGetHandle();
        }
    }

    /// <summary>
    /// Returns an independent instance backed by its own native handle
    /// (<c>PFEntityDuplicateHandle</c>).
    /// </summary>
    public PlayFabEntity Duplicate()
    {
        IntPtr duplicate;
        Hr.ThrowIfFailed(NativePlayFab.PFEntityDuplicateHandle(Handle, &duplicate));
        return new PlayFabEntity(duplicate);
    }

    /// <summary>
    /// Reads the entity's current token, refreshing it if required
    /// (<c>PFEntityGetEntityTokenAsync</c>).
    /// </summary>
    public Task<EntityToken> GetEntityTokenAsync(CancellationToken cancellationToken = default)
    {
        IntPtr handle = Handle;
        return AsyncOperation<EntityToken>.RunAsync(
            IntPtr.Zero,
            block => NativePlayFab.PFEntityGetEntityTokenAsync(handle, (XAsyncBlock*)block),
            static (IntPtr block, out EntityToken value) =>
            {
                value = null!;
                nuint size;
                int hr = NativePlayFab.PFEntityGetEntityTokenResultSize((XAsyncBlock*)block, &size);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                IntPtr buffer = Marshal.AllocHGlobal(checked((int)size));
                try
                {
                    PFEntityToken* token;
                    nuint used;
                    hr = NativePlayFab.PFEntityGetEntityTokenResult(
                        (XAsyncBlock*)block, size, (void*)buffer, &token, &used);
                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    value = EntityToken.FromNative(token);
                    return HResult.SOk;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            },
            cancellationToken);
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

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(PlayFabEntity));
        }
    }
}
