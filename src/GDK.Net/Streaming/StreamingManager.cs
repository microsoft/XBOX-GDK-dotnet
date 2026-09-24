using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GDK.Net.Interop;

namespace GDK.Net.Streaming;

/// <summary>
/// Game streaming features: connection state, touch-controls, client properties and display
/// details.
/// </summary>
/// <remarks>
/// <para>
/// Call <see cref="Initialize"/> once after <c>GameRuntime.Initialize()</c> and dispose before
/// <c>GameRuntime.Dispose()</c>. The manager calls <c>XGameStreamingInitialize</c> on entry and
/// <c>XGameStreamingUninitialize</c> on dispose.
/// </para>
/// <para>
/// Connection-state and client-property-change events share the same pattern used by
/// <c>UserManager</c> and <c>GameActivationManager</c>: a global trampoline dispatches to a
/// static dictionary keyed by a <see cref="GCHandle"/> stored in the native context pointer.
/// Event registrations are created lazily on the first subscriber and released on
/// <see cref="Dispose"/> with <c>wait: true</c>, so no callback can be in flight once the
/// manager is gone.
/// </para>
/// <para>
/// Per-client property-change notifications are opt-in. Call
/// <see cref="WatchClientProperties"/> for each client whose property changes you want; the
/// manager registers the native callback for that client and delivers events through
/// <see cref="ClientPropertiesChanged"/>.
/// </para>
/// </remarks>
public sealed unsafe class StreamingManager : IDisposable
{
    // Static dispatch tables, one entry per live registration context.
    private static readonly ConcurrentDictionary<IntPtr, StreamingManager> ConnectionRegistrations = new();
    private static readonly ConcurrentDictionary<IntPtr, ClientPropsRegistration> ClientPropsRegistrations = new();

    private readonly GameTaskQueue? _queue;
    private readonly object _gate = new();

    private EventHandler<StreamingConnectionStateChangedEventArgs>? _connectionStateChanged;
    private GCHandle _connectionSelf;
    private XTaskQueueRegistrationToken _connectionToken;
    private bool _connectionRegistered;

    private EventHandler<StreamingClientPropertiesChangedEventArgs>? _clientPropertiesChanged;
    private readonly ConcurrentDictionary<ulong, ClientPropsRegistration> _watchedClients = new();

    private bool _disposed;

    private StreamingManager(GameTaskQueue? queue) => _queue = queue;

    // ── Factory ────────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Initializes the XGameStreaming subsystem (<c>XGameStreamingInitialize</c>). Must be called
    /// after <c>GameRuntime.Initialize()</c>.
    /// </summary>
    /// <remarks>
    /// Event registrations name no task queue, so the Gaming Runtime resolves the process default.
    /// </remarks>
    public static StreamingManager Initialize()
    {
        Hr.ThrowIfFailed(Native.XGameStreamingInitialize());

        return new StreamingManager(queue: null);
    }

    // ── Streaming state ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns <see langword="true"/> when the game is currently being streamed
    /// (<c>XGameStreamingIsStreaming</c>).
    /// </summary>
    public bool IsStreaming
    {
        get
        {
            ThrowIfDisposed();
            return Native.XGameStreamingIsStreaming() != 0;
        }
    }

    /// <summary>
    /// Returns the number of currently connected streaming clients
    /// (<c>XGameStreamingGetClientCount</c>).
    /// </summary>
    public uint GetClientCount()
    {
        ThrowIfDisposed();
        return Native.XGameStreamingGetClientCount();
    }

    /// <summary>
    /// Returns the ids of all currently connected clients (<c>XGameStreamingGetClients</c>).
    /// </summary>
    public IReadOnlyList<StreamingClientId> GetClients()
    {
        ThrowIfDisposed();
        uint count = Native.XGameStreamingGetClientCount();
        if (count == 0)
        {
            return Array.Empty<StreamingClientId>();
        }

        ulong[] ids = new ulong[count];
        uint used;
        fixed (ulong* ptr = ids)
        {
            Hr.ThrowIfFailed(Native.XGameStreamingGetClients(count, ptr, &used));
        }

        var result = new StreamingClientId[(int)used];
        for (int i = 0; i < (int)used; i++)
        {
            result[i] = new StreamingClientId(ids[i]);
        }

        return result;
    }

    /// <summary>
    /// Returns the current connection state of <paramref name="client"/>
    /// (<c>XGameStreamingGetConnectionState</c>).
    /// </summary>
    public StreamingConnectionState GetConnectionState(StreamingClientId client)
    {
        ThrowIfDisposed();
        return (StreamingConnectionState)Native.XGameStreamingGetConnectionState(client.Value);
    }

    // ── Events ──────────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Raised when a streaming client connects or disconnects
    /// (<c>XGameStreamingRegisterConnectionStateChanged</c>).
    /// Delivered on the manager's task queue.
    /// </summary>
    public event EventHandler<StreamingConnectionStateChangedEventArgs>? ConnectionStateChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _connectionStateChanged += value;
                EnsureConnectionRegistered();
            }
        }
        remove
        {
            lock (_gate)
            {
                _connectionStateChanged -= value;
            }
        }
    }

    /// <summary>
    /// Raised when a watched streaming client's properties change
    /// (<c>XGameStreamingRegisterClientPropertiesChanged</c>).
    /// Call <see cref="WatchClientProperties"/> to opt a client in.
    /// Delivered on the manager's task queue.
    /// </summary>
    public event EventHandler<StreamingClientPropertiesChangedEventArgs>? ClientPropertiesChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _clientPropertiesChanged += value;
            }
        }
        remove
        {
            lock (_gate)
            {
                _clientPropertiesChanged -= value;
            }
        }
    }

    /// <summary>
    /// Registers for property-change notifications for <paramref name="client"/>
    /// (<c>XGameStreamingRegisterClientPropertiesChanged</c>). Events fire through
    /// <see cref="ClientPropertiesChanged"/>. Idempotent: safe to call more than once for the
    /// same client.
    /// </summary>
    public void WatchClientProperties(StreamingClientId client)
    {
        ThrowIfDisposed();
        if (_watchedClients.ContainsKey(client.Value))
        {
            return;
        }

        lock (_gate)
        {
            if (_watchedClients.ContainsKey(client.Value))
            {
                return;
            }

            var reg = new ClientPropsRegistration(this, client.Value);
            reg.Handle = GCHandle.Alloc(reg, GCHandleType.Normal);
            IntPtr context = GCHandle.ToIntPtr(reg.Handle);
            ClientPropsRegistrations[context] = reg;

            XTaskQueueRegistrationToken token;
            int hr = Native.XGameStreamingRegisterClientPropertiesChanged(
                client.Value,
                _queue.RawHandle(),
                context,
                Trampolines.StreamingClientPropertiesChangedCallback,
                &token);

            if (HResult.Failed(hr))
            {
                ClientPropsRegistrations.TryRemove(context, out _);
                reg.Handle.Free();
                Hr.ThrowIfFailed(hr);
            }

            reg.Token = token;
            _watchedClients[client.Value] = reg;
        }
    }

    /// <summary>
    /// Unregisters property-change notifications for <paramref name="client"/>
    /// (<c>XGameStreamingUnregisterClientPropertiesChanged</c>). Idempotent.
    /// </summary>
    public void UnwatchClientProperties(StreamingClientId client)
    {
        lock (_gate)
        {
            ReleaseClientPropsRegistration(client.Value, wait: 1);
        }
    }

    // ── Touch controls ──────────────────────────────────────────────────────────────────────────

    /// <summary>Hides touch controls on all connected clients (<c>XGameStreamingHideTouchControls</c>).</summary>
    public void HideTouchControls()
    {
        ThrowIfDisposed();
        Native.XGameStreamingHideTouchControls();
    }

    /// <summary>Hides touch controls on a specific client (<c>XGameStreamingHideTouchControlsOnClient</c>).</summary>
    public void HideTouchControls(StreamingClientId client)
    {
        ThrowIfDisposed();
        Native.XGameStreamingHideTouchControlsOnClient(client.Value);
    }

    /// <summary>
    /// Shows the named touch-control layout on all clients
    /// (<c>XGameStreamingShowTouchControlLayout</c>).
    /// </summary>
    /// <param name="layout">
    /// Name of the layout defined in the touch-adaptation bundle, or <see langword="null"/> to
    /// show the default layout.
    /// </param>
    public void ShowTouchControlLayout(string? layout)
    {
        ThrowIfDisposed();
        IntPtr layoutPtr = Utf8.Allocate(layout);
        try
        {
            Native.XGameStreamingShowTouchControlLayout((byte*)layoutPtr);
        }
        finally
        {
            Utf8.Free(layoutPtr);
        }
    }

    /// <summary>
    /// Shows the named touch-control layout on a specific client
    /// (<c>XGameStreamingShowTouchControlLayoutOnClient</c>).
    /// </summary>
    public void ShowTouchControlLayout(StreamingClientId client, string? layout)
    {
        ThrowIfDisposed();
        IntPtr layoutPtr = Utf8.Allocate(layout);
        try
        {
            Native.XGameStreamingShowTouchControlLayoutOnClient(client.Value, (byte*)layoutPtr);
        }
        finally
        {
            Utf8.Free(layoutPtr);
        }
    }

    /// <summary>
    /// Updates touch-control state variables on all clients
    /// (<c>XGameStreamingUpdateTouchControlsState</c>).
    /// </summary>
    public void UpdateTouchControlsState(IReadOnlyList<TouchControlsStateOperation>? operations)
    {
        ThrowIfDisposed();
        if (operations is null || operations.Count == 0)
        {
            Hr.ThrowIfFailed(Native.XGameStreamingUpdateTouchControlsState(0, null));
            return;
        }

        ApplyOperations(operations, (count, ops) =>
            Hr.ThrowIfFailed(Native.XGameStreamingUpdateTouchControlsState(count, ops)));
    }

    /// <summary>
    /// Updates touch-control state variables on a specific client
    /// (<c>XGameStreamingUpdateTouchControlsStateOnClient</c>).
    /// </summary>
    public void UpdateTouchControlsState(
        StreamingClientId client,
        IReadOnlyList<TouchControlsStateOperation>? operations)
    {
        ThrowIfDisposed();
        if (operations is null || operations.Count == 0)
        {
            Hr.ThrowIfFailed(Native.XGameStreamingUpdateTouchControlsStateOnClient(client.Value, 0, null));
            return;
        }

        ulong clientValue = client.Value;
        ApplyOperations(operations, (count, ops) =>
            Hr.ThrowIfFailed(Native.XGameStreamingUpdateTouchControlsStateOnClient(clientValue, count, ops)));
    }

    /// <summary>
    /// Shows a layout and updates state variables simultaneously on all clients
    /// (<c>XGameStreamingShowTouchControlsWithStateUpdate</c>).
    /// </summary>
    public void ShowTouchControlsWithStateUpdate(
        string? layout,
        IReadOnlyList<TouchControlsStateOperation>? operations)
    {
        ThrowIfDisposed();
        IntPtr layoutPtr = Utf8.Allocate(layout);
        try
        {
            if (operations is null || operations.Count == 0)
            {
                Hr.ThrowIfFailed(Native.XGameStreamingShowTouchControlsWithStateUpdate(
                    (byte*)layoutPtr, 0, null));
                return;
            }

            ApplyOperations(operations, (count, ops) =>
                Hr.ThrowIfFailed(Native.XGameStreamingShowTouchControlsWithStateUpdate(
                    (byte*)layoutPtr, count, ops)));
        }
        finally
        {
            Utf8.Free(layoutPtr);
        }
    }

    /// <summary>
    /// Shows a layout and updates state variables simultaneously on a specific client
    /// (<c>XGameStreamingShowTouchControlsWithStateUpdateOnClient</c>).
    /// </summary>
    public void ShowTouchControlsWithStateUpdate(
        StreamingClientId client,
        string? layout,
        IReadOnlyList<TouchControlsStateOperation>? operations)
    {
        ThrowIfDisposed();
        IntPtr layoutPtr = Utf8.Allocate(layout);
        try
        {
            ulong clientValue = client.Value;
            if (operations is null || operations.Count == 0)
            {
                Hr.ThrowIfFailed(Native.XGameStreamingShowTouchControlsWithStateUpdateOnClient(
                    clientValue, (byte*)layoutPtr, 0, null));
                return;
            }

            ApplyOperations(operations, (count, ops) =>
                Hr.ThrowIfFailed(Native.XGameStreamingShowTouchControlsWithStateUpdateOnClient(
                    clientValue, (byte*)layoutPtr, count, ops)));
        }
        finally
        {
            Utf8.Free(layoutPtr);
        }
    }

    // ── Per-client getters ──────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the physical display dimensions (in mm) of a streaming client's device
    /// (<c>XGameStreamingGetStreamPhysicalDimensions</c>).
    /// </summary>
    public StreamingPhysicalDimensions GetStreamPhysicalDimensions(StreamingClientId client)
    {
        ThrowIfDisposed();
        uint h, v;
        Hr.ThrowIfFailed(Native.XGameStreamingGetStreamPhysicalDimensions(client.Value, &h, &v));
        return new StreamingPhysicalDimensions(h, v);
    }

    /// <summary>
    /// Returns the most recent latency measurements for a streaming client
    /// (<c>XGameStreamingGetStreamAddedLatency</c>).
    /// </summary>
    public StreamingLatencyStats GetStreamAddedLatency(StreamingClientId client)
    {
        ThrowIfDisposed();
        uint avgIn, avgOut, stdDev;
        Hr.ThrowIfFailed(Native.XGameStreamingGetStreamAddedLatency(
            client.Value, &avgIn, &avgOut, &stdDev));
        return new StreamingLatencyStats(avgIn, avgOut, stdDev);
    }

    /// <summary>
    /// Returns <see langword="true"/> when the streaming client will send touch input to the game
    /// (<c>XGameStreamingIsTouchInputEnabled</c>).
    /// </summary>
    public bool IsTouchInputEnabled(StreamingClientId client)
    {
        ThrowIfDisposed();
        byte enabled;
        Hr.ThrowIfFailed(Native.XGameStreamingIsTouchInputEnabled(client.Value, &enabled));
        return enabled != 0;
    }

    /// <summary>
    /// Returns the version and name of the touch-adaptation bundle active on a streaming client
    /// (<c>XGameStreamingGetTouchBundleVersion</c>), or <see langword="null"/> when no bundle is
    /// active.
    /// </summary>
    public TouchBundleVersionInfo? GetTouchBundleVersion(StreamingClientId client)
    {
        ThrowIfDisposed();
        nuint nameSize = Native.XGameStreamingGetTouchBundleVersionNameSize(client.Value);
        if (nameSize == 0)
        {
            return null;
        }

        byte[] nameBuffer = new byte[(int)nameSize];
        XVersion rawVersion;
        fixed (byte* namePtr = nameBuffer)
        {
            Hr.ThrowIfFailed(Native.XGameStreamingGetTouchBundleVersion(
                client.Value, &rawVersion, nameSize, namePtr));
        }

        int nameLen = 0;
        while (nameLen < nameBuffer.Length && nameBuffer[nameLen] != 0)
        {
            nameLen++;
        }

        string name = nameLen == 0
            ? string.Empty
            : Encoding.UTF8.GetString(nameBuffer, 0, nameLen);

        var version = new Version(rawVersion.Major, rawVersion.Minor, rawVersion.Build, rawVersion.Revision);
        return new TouchBundleVersionInfo(version, name);
    }

    /// <summary>
    /// Returns the session id of a streaming client
    /// (<c>XGameStreamingGetSessionId</c>).
    /// </summary>
    public string GetSessionId(StreamingClientId client)
    {
        ThrowIfDisposed();
        const int SessionIdMaxBytes = 256;
        byte[] buffer = new byte[SessionIdMaxBytes];
        nuint used;
        fixed (byte* ptr = buffer)
        {
            Hr.ThrowIfFailed(Native.XGameStreamingGetSessionId(
                client.Value, (nuint)SessionIdMaxBytes, ptr, &used));
        }

        int length = used == 0 ? 0 : (int)used - 1; // used includes the null terminator
        return length <= 0 ? string.Empty : Encoding.UTF8.GetString(buffer, 0, length);
    }

    /// <summary>
    /// Returns the display details for a streaming client
    /// (<c>XGameStreamingGetDisplayDetails</c>).
    /// </summary>
    /// <param name="client">The streaming client to query.</param>
    /// <param name="maxSupportedPixels">
    /// The maximum pixel count this title can render. Pass <c>0</c> for no limit.
    /// </param>
    /// <param name="widestSupportedAspectRatio">Widest aspect ratio the title supports (e.g. 16/9f).</param>
    /// <param name="tallestSupportedAspectRatio">Tallest aspect ratio the title supports (e.g. 9/16f).</param>
    public StreamingDisplayDetails GetDisplayDetails(
        StreamingClientId client,
        uint maxSupportedPixels,
        float widestSupportedAspectRatio,
        float tallestSupportedAspectRatio)
    {
        ThrowIfDisposed();
        XGameStreamingDisplayDetails raw;
        Hr.ThrowIfFailed(Native.XGameStreamingGetDisplayDetails(
            client.Value,
            maxSupportedPixels,
            widestSupportedAspectRatio,
            tallestSupportedAspectRatio,
            &raw));
        return new StreamingDisplayDetails(in raw);
    }

    // ── Global / server ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the streaming server's location name, or <see langword="null"/> when streaming is
    /// not active (<c>XGameStreamingGetServerLocationName</c>).
    /// </summary>
    public string? GetServerLocationName()
    {
        ThrowIfDisposed();
        nuint size = Native.XGameStreamingGetServerLocationNameSize();
        if (size == 0)
        {
            return null;
        }

        byte[] buffer = new byte[(int)size];
        fixed (byte* ptr = buffer)
        {
            Hr.ThrowIfFailed(Native.XGameStreamingGetServerLocationName(size, ptr));
        }

        int len = 0;
        while (len < buffer.Length && buffer[len] != 0)
        {
            len++;
        }

        return len == 0 ? string.Empty : Encoding.UTF8.GetString(buffer, 0, len);
    }

    /// <summary>
    /// Sets the stream resolution (<c>XGameStreamingSetResolution</c>).
    /// Throws <see cref="GameRuntimeException"/> when the resolution is not supported.
    /// </summary>
    public void SetResolution(uint width, uint height)
    {
        ThrowIfDisposed();
        Hr.ThrowIfFailed(Native.XGameStreamingSetResolution(width, height));
    }


    /// <summary>
    /// Reports which inputs in a gamepad reading came from physical hardware and which were
    /// synthesised by an on-screen touch layout (<c>XGameStreamingGetGamepadPhysicality</c>).
    /// </summary>
    /// <remarks>
    /// <para>
    /// <paramref name="gamepadReading"/> is an opaque <c>IGameInputReading*</c>. GameInput is out
    /// of scope for this projection, so the pointer is passed through untouched: obtain it from
    /// your own GameInput binding and keep the reading alive across this call. The runtime only
    /// inspects it; it is never retained.
    /// </para>
    /// </remarks>
    /// <param name="gamepadReading">
    /// A non-null <c>IGameInputReading*</c> for a gamepad reading.
    /// </param>
    /// <exception cref="ArgumentException"><paramref name="gamepadReading"/> is <c>IntPtr.Zero</c>.</exception>
    public StreamingGamepadPhysicality GetGamepadPhysicality(IntPtr gamepadReading)
    {
        ThrowIfDisposed();

        if (gamepadReading == IntPtr.Zero)
        {
            throw new ArgumentException(
                "gamepadReading must be a non-null IGameInputReading*.", nameof(gamepadReading));
        }

        XGameStreamingGamepadPhysicality physicality;
        Hr.ThrowIfFailed(Native.XGameStreamingGetGamepadPhysicality(gamepadReading, &physicality));
        return (StreamingGamepadPhysicality)physicality;
    }

    // ── Dispose ──────────────────────────────────────────────────────────────────────────────────

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
            _connectionStateChanged = null;
            _clientPropertiesChanged = null;

            // Release per-client registrations first.
            foreach (ulong clientId in _watchedClients.Keys)
            {
                ReleaseClientPropsRegistration(clientId, wait: 1);
            }

            // Release the connection-state registration.
            if (_connectionRegistered)
            {
                _connectionRegistered = false;
                Native.XGameStreamingUnregisterConnectionStateChanged(_connectionToken, wait: 1);
                _connectionToken = default;
            }

            if (_connectionSelf.IsAllocated)
            {
                ConnectionRegistrations.TryRemove(GCHandle.ToIntPtr(_connectionSelf), out _);
                _connectionSelf.Free();
            }
        }

        Native.XGameStreamingUninitialize();
    }

    // ── Internal dispatch (called from Trampolines.Streaming.cs) ────────────────────────────────

    /// <summary>Entry point used by <see cref="Trampolines"/> for <c>XGameStreamingConnectionStateChangedCallback</c>.</summary>
    internal static void DispatchConnectionStateChanged(
        IntPtr context,
        ulong clientId,
        XGameStreamingConnectionState state)
    {
        if (!ConnectionRegistrations.TryGetValue(context, out StreamingManager? manager))
        {
            return;
        }

        manager.RaiseConnectionStateChanged(
            new StreamingClientId(clientId),
            (StreamingConnectionState)state);
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> for <c>XGameStreamingClientPropertiesChangedCallback</c>.</summary>
    internal static void DispatchClientPropertiesChanged(
        IntPtr context,
        ulong clientId,
        uint updatedPropertiesCount,
        XGameStreamingClientProperty* updatedProperties)
    {
        if (!ClientPropsRegistrations.TryGetValue(context, out ClientPropsRegistration? reg))
        {
            return;
        }

        var props = new StreamingClientProperty[(int)updatedPropertiesCount];
        for (int i = 0; i < (int)updatedPropertiesCount; i++)
        {
            props[i] = (StreamingClientProperty)updatedProperties[i];
        }

        reg.Owner.RaiseClientPropertiesChanged(new StreamingClientId(clientId), props);
    }

    // ── Private helpers ──────────────────────────────────────────────────────────────────────────

    private void EnsureConnectionRegistered()
    {
        if (_connectionRegistered)
        {
            return;
        }

        _connectionSelf = GCHandle.Alloc(this, GCHandleType.Normal);
        IntPtr context = GCHandle.ToIntPtr(_connectionSelf);
        ConnectionRegistrations[context] = this;

        XTaskQueueRegistrationToken token;
        int hr = Native.XGameStreamingRegisterConnectionStateChanged(
            _queue.RawHandle(),
            context,
            Trampolines.StreamingConnectionStateChangedCallback,
            &token);

        if (HResult.Failed(hr))
        {
            ConnectionRegistrations.TryRemove(context, out _);
            _connectionSelf.Free();
            Hr.ThrowIfFailed(hr);
        }

        _connectionToken    = token;
        _connectionRegistered = true;
    }

    // Must be called under _gate.
    private void ReleaseClientPropsRegistration(ulong clientId, byte wait)
    {
        if (!_watchedClients.TryRemove(clientId, out ClientPropsRegistration? reg))
        {
            return;
        }

        Native.XGameStreamingUnregisterClientPropertiesChanged(clientId, reg.Token, wait);
        ClientPropsRegistrations.TryRemove(GCHandle.ToIntPtr(reg.Handle), out _);
        reg.Handle.Free();
    }

    private void RaiseConnectionStateChanged(StreamingClientId client, StreamingConnectionState state)
    {
        EventHandler<StreamingConnectionStateChangedEventArgs>? handler;
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            handler = _connectionStateChanged;
        }

        handler?.Invoke(this, new StreamingConnectionStateChangedEventArgs(client, state));
    }

    private void RaiseClientPropertiesChanged(
        StreamingClientId client,
        StreamingClientProperty[] props)
    {
        EventHandler<StreamingClientPropertiesChangedEventArgs>? handler;
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            handler = _clientPropertiesChanged;
        }

        handler?.Invoke(this, new StreamingClientPropertiesChangedEventArgs(client, props));
    }

    // Custom delegate so a pointer type can be a parameter (Action<T> cannot hold pointer T).
    private unsafe delegate void NativeOpsInvoker(nuint count, XGameStreamingTouchControlsStateOperation* ops);

    private static unsafe void ApplyOperations(
        IReadOnlyList<TouchControlsStateOperation> operations,
        NativeOpsInvoker invoke)
    {
        int count    = operations.Count;
        int itemSize = sizeof(XGameStreamingTouchControlsStateOperation);
        var nativeOps = (XGameStreamingTouchControlsStateOperation*)
            Marshal.AllocHGlobal(count * itemSize);

        // Track every UTF-8 string allocation so they can all be freed in the finally block.
        IntPtr[] stringAllocs = new IntPtr[count * 2];
        int allocCount = 0;

        try
        {
            for (int i = 0; i < count; i++)
            {
                TouchControlsStateOperation op = operations[i];

                nativeOps[i] = default;
                nativeOps[i].OperationKind =
                    (XGameStreamingTouchControlsStateOperationKind)op.Kind;

                IntPtr pathPtr = Utf8.Allocate(op.Path);
                stringAllocs[allocCount++] = pathPtr;
                nativeOps[i].Path = (byte*)pathPtr;

                nativeOps[i].Value.ValueKind =
                    (XGameStreamingTouchControlsStateValueKind)op.Value.Kind;

                switch (op.Value.Kind)
                {
                    case TouchControlsStateValueKind.Boolean:
                        nativeOps[i].Value.BooleanValue = op.Value.BooleanValue ? (byte)1 : (byte)0;
                        break;
                    case TouchControlsStateValueKind.Integer:
                        nativeOps[i].Value.IntegerValue = op.Value.IntegerValue;
                        break;
                    case TouchControlsStateValueKind.Double:
                        nativeOps[i].Value.DoubleValue = op.Value.DoubleValue;
                        break;
                    case TouchControlsStateValueKind.String:
                        IntPtr strPtr = Utf8.Allocate(op.Value.StringValue);
                        stringAllocs[allocCount++] = strPtr;
                        nativeOps[i].Value.StringValue = (byte*)strPtr;
                        break;
                }
            }

            invoke((nuint)count, nativeOps);
        }
        finally
        {
            for (int i = 0; i < allocCount; i++)
            {
                Utf8.Free(stringAllocs[i]);
            }

            Marshal.FreeHGlobal((IntPtr)nativeOps);
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(StreamingManager));
        }
    }

    // ── Inner types ──────────────────────────────────────────────────────────────────────────────

    private sealed class ClientPropsRegistration
    {
        internal ClientPropsRegistration(StreamingManager owner, ulong clientId)
        {
            Owner    = owner;
            ClientId = clientId;
        }

        internal StreamingManager Owner    { get; }
        internal ulong            ClientId { get; }
        internal GCHandle         Handle   { get; set; }
        internal XTaskQueueRegistrationToken Token { get; set; }
    }
}
