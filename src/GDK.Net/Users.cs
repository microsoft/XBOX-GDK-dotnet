using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.Users;

/// <summary>
/// User sign-in and change notifications. Reached through <see cref="GameRuntime.Users"/>.
/// </summary>
/// <remarks>
/// The native change-event registration is created lazily on the first
/// <see cref="UserChanged"/> subscription and released on <see cref="Dispose"/> with
/// <c>XUserUnregisterForChangeEvent(token, wait: true)</c>, so no callback can be in flight once
/// the manager is gone.
/// </remarks>
public sealed unsafe class UserManager : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, UserManager> Registrations = new();
    private static readonly ConcurrentDictionary<IntPtr, UserManager> DeviceAssociationRegistrations = new();
    private static readonly ConcurrentDictionary<IntPtr, UserManager> AudioEndpointRegistrations = new();

    private readonly GameTaskQueue? _queue;
    private readonly object _gate = new();
    private readonly ConcurrentDictionary<ulong, WeakReference<User>> _knownUsers = new();

    private EventHandler<UserChangedEventArgs>? _userChanged;
    private XTaskQueueRegistrationToken _token;
    private GCHandle _self;
    private bool _registered;

    private EventHandler<UserDeviceAssociationChangedEventArgs>? _deviceAssociationChanged;
    private XTaskQueueRegistrationToken _deviceAssociationToken;
    private GCHandle _deviceAssociationSelf;
    private bool _deviceAssociationRegistered;

    private EventHandler<UserDefaultAudioEndpointChangedEventArgs>? _audioEndpointChanged;
    private XTaskQueueRegistrationToken _audioEndpointToken;
    private GCHandle _audioEndpointSelf;
    private bool _audioEndpointRegistered;
    private bool _disposed;

    internal UserManager(GameTaskQueue? queue)
    {
        _queue = queue;
    }

    /// <summary>
    /// Raised when a user's sign-in state, gamertag, gamer picture or privileges change
    /// (<c>XUserRegisterForChangeEvent</c>).
    /// </summary>
    /// <remarks>
    /// Delivered on the manager's task queue. When the manager names no queue the Gaming Runtime
    /// resolves the process default, so handlers arrive on the thread pool; a manager constructed
    /// over a manual queue delivers them on whichever thread pumps that queue.
    /// </remarks>
    public event EventHandler<UserChangedEventArgs>? UserChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _userChanged += value;
                EnsureRegistered();
            }
        }

        remove
        {
            lock (_gate)
            {
                _userChanged -= value;
            }
        }
    }

    /// <summary>
    /// Raised when a device's user association changes
    /// (<c>XUserRegisterForDeviceAssociationChanged</c>).
    /// </summary>
    /// <remarks>
    /// The runtime replays all current associations on the first subscription. Delivered on the
    /// manager's task queue.
    /// </remarks>
    public event EventHandler<UserDeviceAssociationChangedEventArgs>? DeviceAssociationChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _deviceAssociationChanged += value;
                EnsureDeviceAssociationRegistered();
            }
        }

        remove
        {
            lock (_gate)
            {
                _deviceAssociationChanged -= value;
            }
        }
    }

    /// <summary>
    /// Raised when a user's default audio endpoint changes
    /// (<c>XUserRegisterForDefaultAudioEndpointUtf16Changed</c>).
    /// </summary>
    public event EventHandler<UserDefaultAudioEndpointChangedEventArgs>? DefaultAudioEndpointChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _audioEndpointChanged += value;
                EnsureAudioEndpointRegistered();
            }
        }

        remove
        {
            lock (_gate)
            {
                _audioEndpointChanged -= value;
            }
        }
    }

    /// <summary>
    /// Returns the maximum number of users that can be signed in simultaneously
    /// (<c>XUserGetMaxUsers</c>).
    /// </summary>
    public uint GetMaxUsers()
    {
        ThrowIfDisposed();
        uint max;
        Hr.ThrowIfFailed(Native.XUserGetMaxUsers(&max));
        return max;
    }

    /// <summary>
    /// Finds an already-signed-in user by Xbox user id (<c>XUserFindUserById</c>).
    /// </summary>
    /// <param name="userId">The Xbox user id to look up.</param>
    /// <returns>A new <see cref="User"/> for the located user.</returns>
    /// <exception cref="UserException">
    /// Thrown when no user with <paramref name="userId"/> is signed in
    /// (<see cref="HResult.EGameUserUserNotFound"/>).
    /// </exception>
    public User FindUserById(ulong userId)
    {
        ThrowIfDisposed();
        IntPtr raw;
        Hr.ThrowIfFailed(Native.XUserFindUserById(userId, &raw));
        var user = new User(new UserHandle(raw), _queue);
        Track(user);
        return user;
    }

    /// <summary>
    /// Finds an already-signed-in user by local id (<c>XUserFindUserByLocalId</c>).
    /// </summary>
    /// <param name="localId">The local id to look up.</param>
    /// <returns>A new <see cref="User"/> for the located user.</returns>
    public User FindUserByLocalId(UserLocalId localId)
    {
        ThrowIfDisposed();
        XUserLocalId nativeId = new XUserLocalId { Value = localId.Value };
        IntPtr raw;
        Hr.ThrowIfFailed(Native.XUserFindUserByLocalId(nativeId, &raw));
        var user = new User(new UserHandle(raw), _queue);
        Track(user);
        return user;
    }

    /// <summary>
    /// Finds the user associated with a device (<c>XUserFindForDevice</c>).
    /// </summary>
    /// <param name="deviceId">The device identifier.</param>
    /// <returns>A new <see cref="User"/> for the user associated with the device.</returns>
    public User FindForDevice(AppLocalDeviceId deviceId)
    {
        ThrowIfDisposed();
        XAppLocalDeviceId nativeId = deviceId.ToNative();
        IntPtr raw;
        Hr.ThrowIfFailed(Native.XUserFindForDevice(&nativeId, &raw));
        var user = new User(new UserHandle(raw), _queue);
        Track(user);
        return user;
    }

    /// <summary>
    /// Adds a user by Xbox user id, showing system UI if needed
    /// (<c>XUserAddByIdWithUiAsync</c> / <c>XUserAddByIdWithUiResult</c>).
    /// </summary>
    /// <param name="userId">The Xbox user id of the user to add.</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>.</param>
    public Task<User> AddByIdWithUiAsync(
        ulong userId,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        GameTaskQueue? queue = _queue;

        return AsyncOperation<User>.RunAsync(
            queue.RawHandle(),
            block => Native.XUserAddByIdWithUiAsync(userId, (XAsyncBlock*)block),
            (IntPtr block, out User value) =>
            {
                value = null!;
                IntPtr raw;
                int hr = Native.XUserAddByIdWithUiResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr)) return hr;
                var user = new User(new UserHandle(raw), queue);
                Track(user);
                value = user;
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Acquires a sign-out deferral that prevents the Gaming Runtime from completing the current
    /// sign-out until the returned object is disposed (<c>XUserGetSignOutDeferral</c>).
    /// </summary>
    /// <remarks>
    /// Call this inside a <see cref="UserChanged"/> handler when
    /// <c>UserChangeEvent.SigningOut</c> fires to delay sign-out while the title completes any
    /// work that must finish before the user is removed (for example, saving game state).
    /// </remarks>
    public SignOutDeferral GetSignOutDeferral()
    {
        ThrowIfDisposed();
        IntPtr raw;
        Hr.ThrowIfFailed(Native.XUserGetSignOutDeferral(&raw));
        return new SignOutDeferral(new SignOutDeferralHandle(raw));
    }

    /// <summary>
    /// Adds a user (<c>XUserAddAsync</c> / <c>XUserAddResult</c>).
    /// </summary>
    /// <param name="options">
    /// Use <see cref="UserAddOptions.AddDefaultUserSilently"/> at startup and fall back to
    /// <see cref="UserAddOptions.AddDefaultUserAllowingUI"/> when that fails.
    /// </param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<User> AddAsync(
        UserAddOptions options = UserAddOptions.AddDefaultUserSilently,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        GameTaskQueue? queue = _queue;

        return AsyncOperation<User>.RunAsync(
            queue.RawHandle(),
            block => Native.XUserAddAsync((XUserAddOptions)options, (XAsyncBlock*)block),
            (IntPtr block, out User value) =>
            {
                value = null!;

                IntPtr raw;
                int hr = Native.XUserAddResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                var user = new User(new UserHandle(raw), queue);
                Track(user);
                value = user;
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _userChanged = null;
            _deviceAssociationChanged = null;
            _audioEndpointChanged = null;

            if (_registered)
            {
                _registered = false;
                // wait: true: returns only once no callback is running.
                Native.XUserUnregisterForChangeEvent(_token, wait: 1);
                _token = default;
            }

            if (_deviceAssociationRegistered)
            {
                _deviceAssociationRegistered = false;
                Native.XUserUnregisterForDeviceAssociationChanged(_deviceAssociationToken, wait: 1);
                _deviceAssociationToken = default;
            }

            if (_audioEndpointRegistered)
            {
                _audioEndpointRegistered = false;
                Native.XUserUnregisterForDefaultAudioEndpointUtf16Changed(_audioEndpointToken, wait: 1);
                _audioEndpointToken = default;
            }

            if (_self.IsAllocated)
            {
                Registrations.TryRemove(GCHandle.ToIntPtr(_self), out _);
                _self.Free();
            }

            if (_deviceAssociationSelf.IsAllocated)
            {
                DeviceAssociationRegistrations.TryRemove(GCHandle.ToIntPtr(_deviceAssociationSelf), out _);
                _deviceAssociationSelf.Free();
            }

            if (_audioEndpointSelf.IsAllocated)
            {
                AudioEndpointRegistrations.TryRemove(GCHandle.ToIntPtr(_audioEndpointSelf), out _);
                _audioEndpointSelf.Free();
            }
        }

        _knownUsers.Clear();
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> for <c>XUserChangeEventCallback</c>.</summary>
    internal static void DispatchUserChanged(IntPtr context, ulong localId, UserChangeEvent change)
    {
        if (!Registrations.TryGetValue(context, out UserManager? manager))
        {
            return;
        }

        manager.RaiseUserChanged(new UserLocalId(localId), change);
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> for <c>XUserDeviceAssociationChangedCallback</c>.</summary>
    internal static void DispatchDeviceAssociationChanged(IntPtr context, XUserDeviceAssociationChange* change)
    {
        if (!DeviceAssociationRegistrations.TryGetValue(context, out UserManager? manager))
        {
            return;
        }

        var args = new UserDeviceAssociationChangedEventArgs(
            new AppLocalDeviceId(change->DeviceId),
            new UserLocalId(change->OldUser.Value),
            new UserLocalId(change->NewUser.Value));
        manager.RaiseDeviceAssociationChanged(args);
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> for <c>XUserDefaultAudioEndpointUtf16ChangedCallback</c>.</summary>
    internal static void DispatchDefaultAudioEndpointChanged(
        IntPtr context,
        XUserLocalId user,
        XUserDefaultAudioEndpointKind kind,
        char* endpointId)
    {
        if (!AudioEndpointRegistrations.TryGetValue(context, out UserManager? manager))
        {
            return;
        }

        string? endpoint = endpointId != null ? new string(endpointId) : null;
        var args = new UserDefaultAudioEndpointChangedEventArgs(
            new UserLocalId(user.Value),
            (UserDefaultAudioEndpointKind)kind,
            endpoint);
        manager.RaiseDefaultAudioEndpointChanged(args);
    }

    private void EnsureRegistered()
    {
        if (_registered)
        {
            return;
        }

        _self = GCHandle.Alloc(this, GCHandleType.Weak);
        IntPtr context = GCHandle.ToIntPtr(_self);
        Registrations[context] = this;

        XTaskQueueRegistrationToken token;
        int hr = Native.XUserRegisterForChangeEvent(
            _queue.RawHandle(),
            context,
            Trampolines.UserChangeEventCallback,
            &token);

        if (HResult.Failed(hr))
        {
            Registrations.TryRemove(context, out _);
            _self.Free();
            Hr.ThrowIfFailed(hr);
        }

        _token = token;
        _registered = true;
    }

    private void EnsureDeviceAssociationRegistered()
    {
        if (_deviceAssociationRegistered)
        {
            return;
        }

        _deviceAssociationSelf = GCHandle.Alloc(this, GCHandleType.Weak);
        IntPtr context = GCHandle.ToIntPtr(_deviceAssociationSelf);
        DeviceAssociationRegistrations[context] = this;

        XTaskQueueRegistrationToken token;
        int hr = Native.XUserRegisterForDeviceAssociationChanged(
            _queue.RawHandle(),
            context,
            Trampolines.DeviceAssociationChangedCallback,
            &token);

        if (HResult.Failed(hr))
        {
            DeviceAssociationRegistrations.TryRemove(context, out _);
            _deviceAssociationSelf.Free();
            Hr.ThrowIfFailed(hr);
        }

        _deviceAssociationToken = token;
        _deviceAssociationRegistered = true;
    }

    private void EnsureAudioEndpointRegistered()
    {
        if (_audioEndpointRegistered)
        {
            return;
        }

        _audioEndpointSelf = GCHandle.Alloc(this, GCHandleType.Weak);
        IntPtr context = GCHandle.ToIntPtr(_audioEndpointSelf);
        AudioEndpointRegistrations[context] = this;

        XTaskQueueRegistrationToken token;
        int hr = Native.XUserRegisterForDefaultAudioEndpointUtf16Changed(
            _queue.RawHandle(),
            context,
            Trampolines.DefaultAudioEndpointChangedCallback,
            &token);

        if (HResult.Failed(hr))
        {
            AudioEndpointRegistrations.TryRemove(context, out _);
            _audioEndpointSelf.Free();
            Hr.ThrowIfFailed(hr);
        }

        _audioEndpointToken = token;
        _audioEndpointRegistered = true;
    }

    private void RaiseUserChanged(UserLocalId localId, UserChangeEvent change)
    {
        EventHandler<UserChangedEventArgs>? handler;
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            handler = _userChanged;
        }

        handler?.Invoke(this, new UserChangedEventArgs(localId, change, Resolve(localId)));
    }

    private void RaiseDeviceAssociationChanged(UserDeviceAssociationChangedEventArgs args)
    {
        EventHandler<UserDeviceAssociationChangedEventArgs>? handler;
        lock (_gate)
        {
            if (_disposed) return;
            handler = _deviceAssociationChanged;
        }

        handler?.Invoke(this, args);
    }

    private void RaiseDefaultAudioEndpointChanged(UserDefaultAudioEndpointChangedEventArgs args)
    {
        EventHandler<UserDefaultAudioEndpointChangedEventArgs>? handler;
        lock (_gate)
        {
            if (_disposed) return;
            handler = _audioEndpointChanged;
        }

        handler?.Invoke(this, args);
    }

    private void Track(User user) => _knownUsers[user.LocalId.Value] = new WeakReference<User>(user);

    private User? Resolve(UserLocalId localId)
    {
        if (!_knownUsers.TryGetValue(localId.Value, out WeakReference<User>? reference))
        {
            return null;
        }

        if (reference.TryGetTarget(out User? user))
        {
            return user;
        }

        _knownUsers.TryRemove(localId.Value, out _);
        return null;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(UserManager));
        }
    }
}

/// <summary>Payload for <see cref="UserManager.UserChanged"/>.</summary>
public sealed class UserChangedEventArgs : EventArgs
{
    internal UserChangedEventArgs(UserLocalId localId, UserChangeEvent change, User? user)
    {
        LocalId = localId;
        Change = change;
        User = user;
    }

    /// <summary>The affected user's local id. Always present.</summary>
    public UserLocalId LocalId { get; }

    /// <summary>What changed.</summary>
    public UserChangeEvent Change { get; }

    /// <summary>
    /// The live <see cref="User"/> for <see cref="LocalId"/> when this manager added it and the
    /// instance is still alive; otherwise <see langword="null"/>. The native callback carries only a
    /// local id, so a user added elsewhere cannot be resolved.
    /// </summary>
    public User? User { get; }
}
