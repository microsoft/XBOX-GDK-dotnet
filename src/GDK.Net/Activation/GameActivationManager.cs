using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using GDK.Net.Interop;

namespace GDK.Net.Activation;

/// <summary>
/// Protocol and game-invite activation notifications. Reached through
/// <see cref="GameRuntime.Activation"/>.
/// </summary>
/// <remarks>
/// <para>
/// Each native registration is created lazily on the first subscription to the corresponding event
/// and released on <see cref="Dispose"/> with <c>wait: true</c>, so no callback can be in flight
/// once the manager is gone.
/// </para>
/// <para>
/// <c>XGameActivation.h</c> publishes <c>XGameActivationRegisterForEvent</c> as the single unified
/// entry point; it reports protocol launches, file launches and both pending and accepted invites,
/// discriminated by <see cref="GameActivationEventArgs.Kind"/>. The older per-kind
/// <c>XGameInvite</c> and <c>XGameProtocol</c> registrations are deprecated in the GDK headers and
/// are not projected.
/// </para>
/// </remarks>
public sealed unsafe class GameActivationManager : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, Registration> Registrations = new();

    private readonly GameTaskQueue? _queue;
    private readonly object _gate = new();

    private EventHandler<GameActivationEventArgs>? _activated;

    private Registration? _activationRegistration;
    private bool _disposed;

    internal GameActivationManager(GameTaskQueue? queue)
    {
        _queue = queue;
    }


    /// <summary>
    /// Raised for every kind of activation through the unified
    /// <c>XGameActivationRegisterForEvent</c> registration: protocol launches, file launches, and
    /// both pending and accepted invites. <see cref="GameActivationEventArgs.Kind"/> discriminates.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="GameActivationType.PendingGameInvite"/> activation is not consumed until the title
    /// calls <see cref="AcceptPendingInvite(string)"/> with the URI from the event.
    /// </para>
    /// </remarks>
    public event EventHandler<GameActivationEventArgs>? Activated
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _activated += value;
                _activationRegistration ??= Register();
            }
        }

        remove
        {
            lock (_gate)
            {
                _activated -= value;
            }
        }
    }


    /// <summary>
    /// Consumes a pending invite reported by <see cref="Activated"/> with
    /// <see cref="GameActivationType.PendingGameInvite"/>, so it is not replayed again
    /// (<c>XGameActivationAcceptPendingInvite</c>).
    /// </summary>
    /// <param name="inviteUri">The URI carried by the activation event.</param>
    /// <exception cref="ArgumentNullException"><paramref name="inviteUri"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="inviteUri"/> is empty.</exception>
    public void AcceptPendingInvite(string inviteUri)
    {
        ThrowIfDisposed();

        if (inviteUri is null)
        {
            throw new ArgumentNullException(nameof(inviteUri));
        }

        if (inviteUri.Length == 0)
        {
            throw new ArgumentException("The invite URI must not be empty.", nameof(inviteUri));
        }

        IntPtr uri = Utf8.Allocate(inviteUri);
        try
        {
            Hr.ThrowIfFailed(Native.XGameActivationAcceptPendingInvite((byte*)uri));
        }
        finally
        {
            Utf8.Free(uri);
        }
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
            _activated = null;

            Release(ref _activationRegistration);
        }
    }


    /// <summary>
    /// Entry point used by <see cref="Trampolines"/> for the unified
    /// <c>XGameActivationRegisterForEvent</c> callback.
    /// </summary>
    internal static void DispatchActivationInfo(IntPtr context, XGameActivationInfo* info)
    {
        if (info is null || !Registrations.TryGetValue(context, out Registration? registration))
        {
            return;
        }

        GameActivationType kind = info->Type switch
        {
            XGameActivationType.File => GameActivationType.File,
            XGameActivationType.PendingGameInvite => GameActivationType.PendingGameInvite,
            XGameActivationType.AcceptedGameInvite => GameActivationType.AcceptedGameInvite,
            _ => GameActivationType.Protocol,
        };

        registration.Owner.Raise(kind, Utf8.ToString(info->Uri) ?? string.Empty);
    }

    private Registration Register()
    {
        var registration = new Registration(this);
        registration.Handle = GCHandle.Alloc(registration, GCHandleType.Normal);

        IntPtr context = GCHandle.ToIntPtr(registration.Handle);
        Registrations[context] = registration;

        XTaskQueueRegistrationToken token;
        int hr = Native.XGameActivationRegisterForEvent(
            _queue.RawHandle(), context, Trampolines.ActivationInfoCallback, &token);

        if (HResult.Failed(hr))
        {
            Registrations.TryRemove(context, out _);
            registration.Handle.Free();
            Hr.ThrowIfFailed(hr);
        }

        registration.Token = token;
        return registration;
    }

    private static void Release(ref Registration? registration)
    {
        if (registration is null)
        {
            return;
        }

        // wait: true — returns only once no callback is running.
        Native.XGameActivationUnregisterForEvent(registration.Token, wait: 1);

        Registrations.TryRemove(GCHandle.ToIntPtr(registration.Handle), out _);
        registration.Handle.Free();
        registration = null;
    }

    private void Raise(GameActivationType kind, string uri)
    {
        EventHandler<GameActivationEventArgs>? handler;
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            handler = _activated;
        }

        handler?.Invoke(this, new GameActivationEventArgs(kind, uri));
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(GameActivationManager));
        }
    }

    /// <summary>One native registration, kept alive by a GCHandle used as the callback context.</summary>
    private sealed class Registration
    {
        internal Registration(GameActivationManager owner) => Owner = owner;

        internal GameActivationManager Owner { get; }

        internal GCHandle Handle { get; set; }

        internal XTaskQueueRegistrationToken Token { get; set; }
    }
}
