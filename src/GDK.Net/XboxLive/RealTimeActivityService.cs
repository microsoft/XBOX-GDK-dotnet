using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// Real-time activity websocket lifetime and notifications for an <see cref="XboxLiveContext"/>.
/// Mirrors <c>real_time_activity_c.h</c>.
/// </summary>
/// <remarks>
/// <para>
/// Real-time activity is a long-lived websocket connection, not a request/response call. XSAPI opens
/// and closes it on demand: tracking presence, social relationship or statistic changes brings it
/// up, and dropping the last subscription takes it down. That lifetime is deliberately not
/// projected, because the native APIs that exposed it are all deprecated:
/// <c>XblRealTimeActivityActivate</c> and <c>Deactivate</c> are documented as no longer required,
/// <c>XblRealTimeActivityAddSubscriptionErrorHandler</c> is documented as never invoked because
/// XSAPI now handles those errors internally, and <c>XblRealTimeActivitySubscriptionGetState</c> and
/// <c>GetId</c> are documented as returning <c>Unknown</c> and a meaningless client-side id. This
/// service therefore exposes only the two live notifications.
/// </para>
/// <para>
/// XSAPI notification handlers take no task queue. The events are therefore raised on an
/// XSAPI-internal thread, not on the <see cref="XboxLiveContext"/> queue. Handlers must be
/// thread-safe or marshal to the game thread themselves.
/// </para>
/// </remarks>
public sealed class RealTimeActivityService
{
    private readonly XboxLiveContext _context;
    private readonly RealTimeActivityRegistry _registry;

    internal RealTimeActivityService(XboxLiveContext context)
    {
        _context = context;
        _registry = new RealTimeActivityRegistry(this, context);
    }

    /// <summary>
    /// Raised when the real-time activity websocket connects, starts connecting or disconnects
    /// (<c>XblRealTimeActivityAddConnectionStateChangeHandler</c>).
    /// </summary>
    /// <remarks>
    /// Delivered on an XSAPI-internal thread, not on the projection's task queue.
    /// </remarks>
    public event EventHandler<RealTimeActivityConnectionStateChangedEventArgs>? ConnectionStateChanged
    {
        add => _registry.AddConnectionStateChanged(value);
        remove => _registry.RemoveConnectionStateChanged(value);
    }

    /// <summary>
    /// Raised when the real-time activity service reports that locally cached subscription state
    /// may be stale (<c>XblRealTimeActivityAddResyncHandler</c>).
    /// </summary>
    /// <remarks>
    /// <para>
    /// A resync notification means the websocket may have dropped or reordered subscription
    /// messages. A title must assume any data it cached from RTA notifications may be stale and
    /// refetch the authoritative state with the corresponding REST APIs. Ignoring this event can
    /// leave the title silently showing stale data.
    /// </para>
    /// <para>
    /// XSAPI automatically resyncs some subscriptions and invokes their normal handlers again
    /// where possible. Multiplayer session-changed subscriptions are not fully resynced by XSAPI;
    /// titles must refetch their sessions themselves.
    /// </para>
    /// <para>
    /// Delivered on an XSAPI-internal thread, not on the projection's task queue.
    /// </para>
    /// </remarks>
    public event EventHandler? ResyncRequired
    {
        add => _registry.AddResyncRequired(value);
        remove => _registry.RemoveResyncRequired(value);
    }
}

/// <summary>
/// Owns the native real-time activity registrations that back
/// <see cref="RealTimeActivityService"/>'s events.
/// </summary>
internal sealed class RealTimeActivityRegistry : IXboxLiveHandlerRegistry
{
    private static readonly ConcurrentDictionary<IntPtr, RealTimeActivityRegistry> Registrations = new();

    private readonly RealTimeActivityService _service;
    private readonly XboxLiveContext _context;
    private readonly object _gate = new();

    private EventHandler<RealTimeActivityConnectionStateChangedEventArgs>? _connectionStateChanged;
    private EventHandler? _resyncRequired;

    private GCHandle _self;
    private int _connectionStateToken;
    private int _resyncToken;
    private bool _connectionStateRegistered;
    private bool _resyncRegistered;

    internal RealTimeActivityRegistry(RealTimeActivityService service, XboxLiveContext context)
    {
        _service = service;
        _context = context;
    }

    internal void AddConnectionStateChanged(EventHandler<RealTimeActivityConnectionStateChangedEventArgs>? handler)
    {
        if (handler is null)
        {
            return;
        }

        lock (_gate)
        {
            _connectionStateChanged += handler;
            try
            {
                EnsureConnectionStateRegistered();
            }
            catch
            {
                _connectionStateChanged -= handler;
                throw;
            }
        }
    }

    internal void RemoveConnectionStateChanged(EventHandler<RealTimeActivityConnectionStateChangedEventArgs>? handler)
    {
        if (handler is null)
        {
            return;
        }

        lock (_gate)
        {
            _connectionStateChanged -= handler;
            if (_connectionStateChanged is null)
            {
                UnregisterConnectionState();
            }
        }
    }

    internal void AddResyncRequired(EventHandler? handler)
    {
        if (handler is null)
        {
            return;
        }

        lock (_gate)
        {
            _resyncRequired += handler;
            try
            {
                EnsureResyncRegistered();
            }
            catch
            {
                _resyncRequired -= handler;
                throw;
            }
        }
    }

    internal void RemoveResyncRequired(EventHandler? handler)
    {
        if (handler is null)
        {
            return;
        }

        lock (_gate)
        {
            _resyncRequired -= handler;
            if (_resyncRequired is null)
            {
                UnregisterResync();
            }
        }
    }

    internal static void DispatchConnectionStateChanged(
        IntPtr context,
        XblRealTimeActivityConnectionState connectionState)
    {
        if (!Registrations.TryGetValue(context, out RealTimeActivityRegistry? registry))
        {
            return;
        }

        EventHandler<RealTimeActivityConnectionStateChangedEventArgs>? handlers;
        lock (registry._gate)
        {
            handlers = registry._connectionStateChanged;
        }

        handlers?.Invoke(
            registry._service,
            new RealTimeActivityConnectionStateChangedEventArgs(
                (RealTimeActivityConnectionState)connectionState));
    }

    internal static void DispatchResyncRequired(IntPtr context)
    {
        if (!Registrations.TryGetValue(context, out RealTimeActivityRegistry? registry))
        {
            return;
        }

        EventHandler? handlers;
        lock (registry._gate)
        {
            handlers = registry._resyncRequired;
        }

        handlers?.Invoke(registry._service, EventArgs.Empty);
    }

    private void EnsureConnectionStateRegistered()
    {
        if (_connectionStateRegistered)
        {
            return;
        }

        IntPtr key = EnsureSelfKey();
        int token = NativeXbl.XblRealTimeActivityAddConnectionStateChangeHandler(
            _context.Handle,
            Trampolines.RealTimeActivityConnectionStateChangeHandler,
            key);

        if (token == 0)
        {
            ReleaseSelfIfUnused();
            ThrowRegistrationFailed("connection-state");
        }

        _connectionStateToken = token;
        _connectionStateRegistered = true;
        _context.TrackHandlerRegistry(this);
    }

    private void EnsureResyncRegistered()
    {
        if (_resyncRegistered)
        {
            return;
        }

        IntPtr key = EnsureSelfKey();
        int token = NativeXbl.XblRealTimeActivityAddResyncHandler(
            _context.Handle,
            Trampolines.RealTimeActivityResyncHandler,
            key);

        if (token == 0)
        {
            ReleaseSelfIfUnused();
            ThrowRegistrationFailed("resync");
        }

        _resyncToken = token;
        _resyncRegistered = true;
        _context.TrackHandlerRegistry(this);
    }

    /// <inheritdoc />
    public void DetachAll()
    {
        lock (_gate)
        {
            _connectionStateChanged = null;
            _resyncRequired = null;

            try
            {
                UnregisterConnectionState();
            }
            catch (GameRuntimeException)
            {
            }

            try
            {
                UnregisterResync();
            }
            catch (GameRuntimeException)
            {
            }
        }
    }

    private void UnregisterConnectionState()
    {
        if (!_connectionStateRegistered)
        {
            return;
        }

        _connectionStateRegistered = false;

        try
        {
            Hr.ThrowIfFailed(
                NativeXbl.XblRealTimeActivityRemoveConnectionStateChangeHandler(
                    _context.Handle,
                    _connectionStateToken));
        }
        finally
        {
            ReleaseSelfIfUnused();
        }
    }

    private void UnregisterResync()
    {
        if (!_resyncRegistered)
        {
            return;
        }

        _resyncRegistered = false;

        try
        {
            Hr.ThrowIfFailed(
                NativeXbl.XblRealTimeActivityRemoveResyncHandler(
                    _context.Handle,
                    _resyncToken));
        }
        finally
        {
            ReleaseSelfIfUnused();
        }
    }

    private IntPtr EnsureSelfKey()
    {
        if (!_self.IsAllocated)
        {
            _self = GCHandle.Alloc(this);
            Registrations[GCHandle.ToIntPtr(_self)] = this;
        }

        return GCHandle.ToIntPtr(_self);
    }

    private void ReleaseSelfIfUnused()
    {
        if (_connectionStateRegistered || _resyncRegistered)
        {
            return;
        }

        if (_self.IsAllocated)
        {
            Registrations.TryRemove(GCHandle.ToIntPtr(_self), out _);
            _self.Free();
        }
    }

    private static void ThrowRegistrationFailed(string handlerKind) =>
        throw new GameRuntimeException(
            HResult.EFail,
            $"Xbox Live rejected the real-time activity {handlerKind} handler registration.");
}
