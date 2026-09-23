using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.Networking;

/// <summary>
/// Networking connectivity, UDP port negotiation, TLS certificate pinning, configuration and
/// statistics. Reached through <see cref="GDK.Net.GameRuntime.Networking"/>.
/// </summary>
/// <remarks>
/// <para>
/// Each native event registration is created lazily on the first subscription to the corresponding
/// event and released on <see cref="Dispose"/> with <c>wait: true</c>, so no callback can be in
/// flight once the manager is gone.
/// </para>
/// </remarks>
public sealed unsafe class NetworkingManager : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, NetworkingManager> UdpPortRegistrations = new();
    private static readonly ConcurrentDictionary<IntPtr, NetworkingManager> ConnectivityRegistrations = new();

    private readonly GameTaskQueue? _queue;
    private readonly object _gate = new();

    private EventHandler<PreferredLocalUdpMultiplayerPortChangedEventArgs>? _udpPortChanged;
    private EventHandler<ConnectivityHintChangedEventArgs>? _connectivityHintChanged;

    private XTaskQueueRegistrationToken _udpPortToken;
    private XTaskQueueRegistrationToken _connectivityToken;
    private GCHandle _udpPortSelf;
    private GCHandle _connectivitySelf;
    private bool _udpPortRegistered;
    private bool _connectivityRegistered;
    private bool _disposed;

    internal NetworkingManager(GameTaskQueue? queue)
    {
        _queue = queue;
    }

    /// <summary>
    /// Raised when the preferred local UDP multiplayer port changes
    /// (<c>XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged</c>).
    /// </summary>
    /// <remarks>
    /// Delivered on the manager's task queue. When the manager names no queue the Gaming Runtime
    /// resolves the process default, so handlers arrive on the thread pool; a manager constructed
    /// over a manual queue delivers them on whichever thread pumps that queue.
    /// </remarks>
    public event EventHandler<PreferredLocalUdpMultiplayerPortChangedEventArgs>? PreferredLocalUdpMultiplayerPortChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _udpPortChanged += value;
                EnsureUdpPortRegistered();
            }
        }

        remove
        {
            lock (_gate)
            {
                _udpPortChanged -= value;
            }
        }
    }

    /// <summary>
    /// Raised when the device's connectivity hint changes
    /// (<c>XNetworkingRegisterConnectivityHintChanged</c>).
    /// </summary>
    public event EventHandler<ConnectivityHintChangedEventArgs>? ConnectivityHintChanged
    {
        add
        {
            ThrowIfDisposed();
            lock (_gate)
            {
                _connectivityHintChanged += value;
                EnsureConnectivityRegistered();
            }
        }

        remove
        {
            lock (_gate)
            {
                _connectivityHintChanged -= value;
            }
        }
    }

    /// <summary>
    /// Returns the current connectivity hint synchronously
    /// (<c>XNetworkingGetConnectivityHint</c>).
    /// </summary>
    public NetworkingConnectivityHint GetConnectivityHint()
    {
        ThrowIfDisposed();

        XNetworkingConnectivityHint hint;
        Hr.ThrowIfFailed(Native.XNetworkingGetConnectivityHint(&hint));
        return NetworkingConnectivityHint.FromNative(hint);
    }

    /// <summary>
    /// Queries the current preferred local UDP port for multiplayer synchronously
    /// (<c>XNetworkingQueryPreferredLocalUdpMultiplayerPort</c>).
    /// </summary>
    public ushort QueryPreferredLocalUdpMultiplayerPort()
    {
        ThrowIfDisposed();

        ushort port;
        Hr.ThrowIfFailed(Native.XNetworkingQueryPreferredLocalUdpMultiplayerPort(&port));
        return port;
    }

    /// <summary>
    /// Asynchronously queries the preferred local UDP port for multiplayer
    /// (<c>XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync</c> /
    /// <c>XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult</c>).
    /// </summary>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<ushort> QueryPreferredLocalUdpMultiplayerPortAsync(
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        GameTaskQueue? queue = _queue;

        return AsyncOperation<ushort>.RunAsync(
            queue.RawHandle(),
            block => Native.XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync((XAsyncBlock*)block),
            static (IntPtr block, out ushort value) =>
            {
                value = 0;
                ushort port;
                int hr = Native.XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult(
                    (XAsyncBlock*)block, &port);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = port;
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Asynchronously retrieves TLS security information for the given URL
    /// (<c>XNetworkingQuerySecurityInformationForUrlAsync</c>). The returned
    /// <see cref="NetworkingSecurityInformation"/> must be disposed after use.
    /// </summary>
    /// <param name="url">The UTF-8 URL to query (e.g. <c>"https://contoso.com/"</c>).</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<NetworkingSecurityInformation> QuerySecurityInformationForUrlAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        if (url is null)
        {
            throw new ArgumentNullException(nameof(url));
        }

        ThrowIfDisposed();
        GameTaskQueue? queue = _queue;

        // Allocate a null-terminated UTF-8 copy of the URL. The GDK copies the string during the
        // XNetworkingQuerySecurityInformationForUrlAsync call (_In_z_ annotation), so we free it
        // after Start() returns — before the async operation ever completes.
        IntPtr urlPtr = Utf8.Allocate(url);
        try
        {
            return AsyncOperation<NetworkingSecurityInformation>.RunAsync(
                queue.RawHandle(),
                block => Native.XNetworkingQuerySecurityInformationForUrlAsync(
                    (byte*)urlPtr, (XAsyncBlock*)block),
                static (IntPtr block, out NetworkingSecurityInformation value) =>
                    ReadSecurityInformation(block, utf16: false, out value),
                cancellationToken);
        }
        finally
        {
            Utf8.Free(urlPtr);
        }
    }

    /// <summary>
    /// Asynchronously retrieves TLS security information for the given URL, passing it to the
    /// Gaming Runtime as UTF-16 rather than UTF-8
    /// (<c>XNetworkingQuerySecurityInformationForUrlUtf16Async</c>). The returned
    /// <see cref="NetworkingSecurityInformation"/> must be disposed after use.
    /// </summary>
    /// <param name="url">The URL to query (e.g. <c>"https://contoso.com/"</c>).</param>
    /// <param name="cancellationToken">Cancels via <c>XAsyncCancel</c>; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <remarks>
    /// Functionally identical to <see cref="QuerySecurityInformationForUrlAsync"/>; it exists
    /// because the GDK publishes both encodings and titles that already hold UTF-16 strings avoid a
    /// transcode. Prefer the UTF-8 overload when the encoding is not already decided.
    /// </remarks>
    public Task<NetworkingSecurityInformation> QuerySecurityInformationForUrlUtf16Async(
        string url,
        CancellationToken cancellationToken = default)
    {
        if (url is null)
        {
            throw new ArgumentNullException(nameof(url));
        }

        ThrowIfDisposed();
        GameTaskQueue? queue = _queue;

        // Same lifetime reasoning as the UTF-8 overload: the GDK copies the string during the
        // starter call, so the native copy only has to survive Start().
        IntPtr urlPtr = Marshal.StringToHGlobalUni(url);
        try
        {
            return AsyncOperation<NetworkingSecurityInformation>.RunAsync(
                queue.RawHandle(),
                block => Native.XNetworkingQuerySecurityInformationForUrlUtf16Async(
                    (char*)urlPtr, (XAsyncBlock*)block),
                static (IntPtr block, out NetworkingSecurityInformation value) =>
                    ReadSecurityInformation(block, utf16: true, out value),
                cancellationToken);
        }
        finally
        {
            Marshal.FreeHGlobal(urlPtr);
        }
    }

    /// <summary>
    /// Shared result reader for both encodings of
    /// <c>XNetworkingQuerySecurityInformationForUrl*Async</c>. The two differ only in which pair of
    /// result entry points they call.
    /// </summary>
    private static int ReadSecurityInformation(
        IntPtr block,
        bool utf16,
        out NetworkingSecurityInformation value)
    {
        value = null!;

        nuint bufferSize;
        int hr = utf16
            ? Native.XNetworkingQuerySecurityInformationForUrlUtf16AsyncResultSize(
                (XAsyncBlock*)block, &bufferSize)
            : Native.XNetworkingQuerySecurityInformationForUrlAsyncResultSize(
                (XAsyncBlock*)block, &bufferSize);
        if (HResult.Failed(hr))
        {
            return hr;
        }

        // Allocate and pin the buffer BEFORE calling the native result function so that
        // the buffer address is stable. The pin is transferred into the result object
        // and kept active for the lifetime of that object.
        byte[] buffer = new byte[(int)bufferSize];
        GCHandle pin = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        try
        {
            byte* pinnedPtr = (byte*)pin.AddrOfPinnedObject();
            XNetworkingSecurityInformation* nativeInfo;
            nuint used;
            hr = utf16
                ? Native.XNetworkingQuerySecurityInformationForUrlUtf16AsyncResult(
                    (XAsyncBlock*)block, bufferSize, &used, pinnedPtr, &nativeInfo)
                : Native.XNetworkingQuerySecurityInformationForUrlAsyncResult(
                    (XAsyncBlock*)block, bufferSize, &used, pinnedPtr, &nativeInfo);

            if (HResult.Failed(hr))
            {
                return hr;
            }

            int offset = (int)((byte*)nativeInfo - pinnedPtr);
            uint flags = nativeInfo->enabledHttpSecurityProtocolFlags;

            var thumbprints = new NetworkingThumbprint[(int)nativeInfo->thumbprintCount];
            for (int i = 0; i < thumbprints.Length; i++)
            {
                XNetworkingThumbprint* t = &nativeInfo->thumbprints[i];
                byte[] data = new byte[(int)t->thumbprintBufferByteCount];
                if (data.Length > 0)
                {
                    Marshal.Copy((IntPtr)t->thumbprintBuffer, data, 0, data.Length);
                }

                thumbprints[i] = new NetworkingThumbprint(
                    (NetworkingThumbprintType)t->thumbprintType, data);
            }

            // Transfer pin ownership. If the constructor throws (OOM), the finally
            // block below frees the pin.
            var result = new NetworkingSecurityInformation(pin, offset, flags, thumbprints);
            pin = default; // ownership transferred; do not Free in finally
            value = result;
            return HResult.SOk;
        }
        finally
        {
            if (pin.IsAllocated)
            {
                pin.Free();
            }
        }
    }

    /// <summary>
    /// Verifies that the server certificate presented during a WinHTTP request matches the
    /// pre-queried security information (<c>XNetworkingVerifyServerCertificate</c>).
    /// </summary>
    /// <param name="requestHandle">The WinHTTP request handle (from <c>WinHttpOpenRequest</c>).</param>
    /// <param name="securityInformation">Security information retrieved via <see cref="QuerySecurityInformationForUrlAsync"/>.</param>
    public void VerifyServerCertificate(
        IntPtr requestHandle,
        NetworkingSecurityInformation securityInformation)
    {
        if (securityInformation is null)
        {
            throw new ArgumentNullException(nameof(securityInformation));
        }

        ThrowIfDisposed();
        Hr.ThrowIfFailed(Native.XNetworkingVerifyServerCertificate(
            requestHandle, securityInformation.GetNativePointer()));
    }

    /// <summary>
    /// Queries a networking configuration setting
    /// (<c>XNetworkingQueryConfigurationSetting</c>).
    /// </summary>
    /// <param name="setting">Which setting to read.</param>
    /// <returns>The current value of the setting in bytes.</returns>
    public ulong QueryConfigurationSetting(NetworkingConfigurationSetting setting)
    {
        ThrowIfDisposed();

        ulong value;
        Hr.ThrowIfFailed(Native.XNetworkingQueryConfigurationSetting(
            (XNetworkingConfigurationSetting)setting, &value));
        return value;
    }

    /// <summary>
    /// Queries TCP receive-buffer statistics for the specified partition
    /// (<c>XNetworkingQueryStatistics</c>).
    /// </summary>
    /// <param name="statisticsType">Which partition's statistics to retrieve.</param>
    /// <returns>A snapshot of the TCP queued receive buffer usage counters.</returns>
    public NetworkingTcpStatistics QueryTcpStatistics(NetworkingStatisticsType statisticsType)
    {
        ThrowIfDisposed();

        XNetworkingStatisticsBuffer buf;
        Hr.ThrowIfFailed(Native.XNetworkingQueryStatistics(
            (XNetworkingStatisticsType)statisticsType, &buf));

        ref XNetworkingTcpQueuedReceivedBufferUsageStatistics s =
            ref buf.tcpQueuedReceiveBufferUsage;
        return new NetworkingTcpStatistics(
            s.numBytesCurrentlyQueued,
            s.peakNumBytesEverQueued,
            s.totalNumBytesQueued,
            s.numBytesDroppedForExceedingConfiguredMax,
            s.numBytesDroppedDueToAnyFailure);
    }

    /// <summary>Releases all native registrations.</summary>
    public void Dispose()
    {
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _udpPortChanged = null;
            _connectivityHintChanged = null;

            ReleaseUdpPortRegistration();
            ReleaseConnectivityRegistration();
        }
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> for <c>XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback</c>.</summary>
    internal static void DispatchUdpPortChanged(IntPtr context, ushort port)
    {
        if (!UdpPortRegistrations.TryGetValue(context, out NetworkingManager? manager))
        {
            return;
        }

        manager.RaiseUdpPortChanged(port);
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> for <c>XNetworkingConnectivityHintChangedCallback</c>.</summary>
    internal static void DispatchConnectivityHintChanged(IntPtr context, XNetworkingConnectivityHint* hint)
    {
        if (!ConnectivityRegistrations.TryGetValue(context, out NetworkingManager? manager))
        {
            return;
        }

        manager.RaiseConnectivityHintChanged(hint);
    }

    private void EnsureUdpPortRegistered()
    {
        if (_udpPortRegistered)
        {
            return;
        }

        _udpPortSelf = GCHandle.Alloc(this, GCHandleType.Weak);
        IntPtr context = GCHandle.ToIntPtr(_udpPortSelf);
        UdpPortRegistrations[context] = this;

        XTaskQueueRegistrationToken token;
        int hr = Native.XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged(
            _queue.RawHandle(),
            context,
            Trampolines.NetworkingUdpPortChangedCallback,
            &token);

        if (HResult.Failed(hr))
        {
            UdpPortRegistrations.TryRemove(context, out _);
            _udpPortSelf.Free();
            Hr.ThrowIfFailed(hr);
        }

        _udpPortToken = token;
        _udpPortRegistered = true;
    }

    private void EnsureConnectivityRegistered()
    {
        if (_connectivityRegistered)
        {
            return;
        }

        _connectivitySelf = GCHandle.Alloc(this, GCHandleType.Weak);
        IntPtr context = GCHandle.ToIntPtr(_connectivitySelf);
        ConnectivityRegistrations[context] = this;

        XTaskQueueRegistrationToken token;
        int hr = Native.XNetworkingRegisterConnectivityHintChanged(
            _queue.RawHandle(),
            context,
            Trampolines.NetworkingConnectivityHintChangedCallback,
            &token);

        if (HResult.Failed(hr))
        {
            ConnectivityRegistrations.TryRemove(context, out _);
            _connectivitySelf.Free();
            Hr.ThrowIfFailed(hr);
        }

        _connectivityToken = token;
        _connectivityRegistered = true;
    }

    private void ReleaseUdpPortRegistration()
    {
        if (!_udpPortRegistered)
        {
            return;
        }

        _udpPortRegistered = false;
        // wait: true — returns only once no callback is running.
        Native.XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged(_udpPortToken, wait: 1);
        _udpPortToken = default;

        IntPtr context = GCHandle.ToIntPtr(_udpPortSelf);
        UdpPortRegistrations.TryRemove(context, out _);
        _udpPortSelf.Free();
    }

    private void ReleaseConnectivityRegistration()
    {
        if (!_connectivityRegistered)
        {
            return;
        }

        _connectivityRegistered = false;
        // wait: true — returns only once no callback is running.
        Native.XNetworkingUnregisterConnectivityHintChanged(_connectivityToken, wait: 1);
        _connectivityToken = default;

        IntPtr context = GCHandle.ToIntPtr(_connectivitySelf);
        ConnectivityRegistrations.TryRemove(context, out _);
        _connectivitySelf.Free();
    }

    private void RaiseUdpPortChanged(ushort port)
    {
        EventHandler<PreferredLocalUdpMultiplayerPortChangedEventArgs>? handler;
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            handler = _udpPortChanged;
        }

        handler?.Invoke(this, new PreferredLocalUdpMultiplayerPortChangedEventArgs(port));
    }

    private void RaiseConnectivityHintChanged(XNetworkingConnectivityHint* hint)
    {
        EventHandler<ConnectivityHintChangedEventArgs>? handler;
        lock (_gate)
        {
            if (_disposed)
            {
                return;
            }

            handler = _connectivityHintChanged;
        }

        handler?.Invoke(this, new ConnectivityHintChangedEventArgs(NetworkingConnectivityHint.FromNative(*hint)));
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(NetworkingManager));
        }
    }
}
