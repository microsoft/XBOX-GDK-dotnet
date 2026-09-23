using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.PlayFab;

/// <summary>
/// Binds a platform user to a PlayFab title so the PlayFab libraries can log in and silently
/// re-authenticate on the title's behalf. Wraps <c>PFLocalUserHandle</c>.
/// </summary>
/// <remarks>
/// A local user is the GDK-recommended entry point: it keeps the association between an
/// <see cref="User"/> and the PlayFab entity, so an expired entity token is refreshed without the
/// title re-running the login flow.
/// </remarks>
public sealed unsafe class PlayFabLocalUser : IDisposable, IEquatable<PlayFabLocalUser>
{
    private readonly PlayFabLocalUserHandle _handle;
    private bool _disposed;

    private PlayFabLocalUser(IntPtr rawHandle)
    {
        _handle = new PlayFabLocalUserHandle(rawHandle);
    }

    /// <summary>
    /// Creates a local user for a signed-in Xbox user
    /// (<c>PFLocalUserCreateHandleWithXboxUser</c>).
    /// </summary>
    public static PlayFabLocalUser CreateForXboxUser(PlayFabServiceConfig serviceConfig, User user)
    {
        if (serviceConfig is null)
        {
            throw new ArgumentNullException(nameof(serviceConfig));
        }

        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        IntPtr raw;
        Hr.ThrowIfFailed(NativePlayFab.PFLocalUserCreateHandleWithXboxUser(
            serviceConfig.Handle, user.Handle, null, &raw));
        return new PlayFabLocalUser(raw);
    }

    /// <summary>
    /// Creates a local user for the signed-in Steam user
    /// (<c>PFLocalUserCreateHandleWithSteamUser</c>).
    /// </summary>
    public static PlayFabLocalUser CreateForSteamUser(PlayFabServiceConfig serviceConfig)
    {
        if (serviceConfig is null)
        {
            throw new ArgumentNullException(nameof(serviceConfig));
        }

        IntPtr raw;
        Hr.ThrowIfFailed(
            NativePlayFab.PFLocalUserCreateHandleWithSteamUser(serviceConfig.Handle, null, &raw));
        return new PlayFabLocalUser(raw);
    }

    /// <summary>The machine-stable local id for this user (<c>PFLocalUserGetLocalId</c>).</summary>
    public string LocalId => PlayFabInterop.GetString(
        Handle,
        NativePlayFab.PFLocalUserGetLocalIdSize,
        NativePlayFab.PFLocalUserGetLocalId);

    /// <summary>
    /// The service configuration this local user was created with
    /// (<c>PFLocalUserGetServiceConfigHandle</c>).
    /// </summary>
    public PlayFabServiceConfig ServiceConfig
    {
        get
        {
            IntPtr raw;
            Hr.ThrowIfFailed(NativePlayFab.PFLocalUserGetServiceConfigHandle(Handle, &raw));

            // The handle is borrowed, so it is duplicated before being given an owner.
            IntPtr duplicate;
            Hr.ThrowIfFailed(NativePlayFab.PFServiceConfigDuplicateHandle(raw, &duplicate));
            return PlayFabServiceConfig.FromRawHandle(duplicate);
        }
    }

    internal IntPtr Handle
    {
        get
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PlayFabLocalUser));
            }

            return _handle.DangerousGetHandle();
        }
    }

    /// <summary>
    /// Returns the entity this local user is already logged in as, or <see langword="null"/> when it
    /// has not logged in yet (<c>PFLocalUserTryGetEntityHandle</c>).
    /// </summary>
    public PlayFabEntity? TryGetEntity()
    {
        IntPtr raw;
        int hr = NativePlayFab.PFLocalUserTryGetEntityHandle(Handle, &raw);
        if (HResult.Failed(hr) || raw == IntPtr.Zero)
        {
            return null;
        }

        return new PlayFabEntity(raw);
    }

    /// <summary>
    /// Returns the Xbox user this local user was created from, or <see langword="null"/> when it was
    /// not created from one (<c>PFLocalUserTryGetXUser</c>).
    /// </summary>
    public User? TryGetXboxUser()
    {
        IntPtr raw;
        int hr = NativePlayFab.PFLocalUserTryGetXUser(Handle, &raw);
        if (HResult.Failed(hr) || raw == IntPtr.Zero)
        {
            return null;
        }

        // The handle is borrowed from the local user, so it is duplicated before being owned.
        IntPtr duplicate;
        Hr.ThrowIfFailed(Native.XUserDuplicateHandle(raw, &duplicate));
        return new User(new UserHandle(duplicate), null);
    }

    /// <summary>
    /// Logs the local user in to PlayFab (<c>PFLocalUserLoginAsync</c>).
    /// </summary>
    /// <param name="createAccount">
    /// Whether PlayFab may create an account when the platform user has never played the title.
    /// </param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<PlayFabLoginResult> LoginAsync(
        bool createAccount = true,
        CancellationToken cancellationToken = default)
    {
        IntPtr handle = Handle;
        return AsyncOperation<PlayFabLoginResult>.RunAsync(
            IntPtr.Zero,
            block => NativePlayFab.PFLocalUserLoginAsync(
                handle, createAccount ? (byte)1 : (byte)0, (XAsyncBlock*)block),
            static (IntPtr block, out PlayFabLoginResult value) =>
            {
                value = null!;
                nuint size;
                int hr = NativePlayFab.PFLocalUserLoginGetResultSize((XAsyncBlock*)block, &size);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                IntPtr buffer = Marshal.AllocHGlobal(checked((int)size));
                try
                {
                    IntPtr entityHandle;
                    PFAuthenticationLoginResult* result;
                    nuint used;
                    hr = NativePlayFab.PFLocalUserLoginGetResult(
                        (XAsyncBlock*)block, &entityHandle, size, (void*)buffer, &result, &used);
                    if (HResult.Failed(hr))
                    {
                        return hr;
                    }

                    value = new PlayFabLoginResult(
                        new PlayFabEntity(entityHandle), AuthenticationLoginResult.FromNative(result));
                    return HResult.SOk;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            },
            cancellationToken);
    }

    /// <summary>
    /// Returns an independent instance backed by its own native handle
    /// (<c>PFLocalUserDuplicateHandle</c>).
    /// </summary>
    public PlayFabLocalUser Duplicate()
    {
        IntPtr duplicate;
        Hr.ThrowIfFailed(NativePlayFab.PFLocalUserDuplicateHandle(Handle, &duplicate));
        return new PlayFabLocalUser(duplicate);
    }

    /// <summary>Compares two local users by identity (<c>PFLocalUserHandleCompare</c>).</summary>
    public bool Equals(PlayFabLocalUser? other) =>
        other is not null && NativePlayFab.PFLocalUserHandleCompare(Handle, other.Handle) == 0;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as PlayFabLocalUser);

    /// <inheritdoc/>
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(LocalId);

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
