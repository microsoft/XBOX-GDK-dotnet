using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GDK.Net.Interop;

namespace GDK.Net.Networking;

/// <summary>
/// Mirrors <c>XNetworkingConnectivityLevelHint</c> from XNetworking.h.
/// Describes the internet connectivity level observed by the device.
/// </summary>
public enum NetworkingConnectivityLevelHint : uint
{
    /// <summary>The connectivity level cannot be determined.</summary>
    Unknown = 0,

    /// <summary>No network connectivity is available.</summary>
    None = 1,

    /// <summary>The device can reach the local network only.</summary>
    LocalAccess = 2,

    /// <summary>The device has unrestricted internet access.</summary>
    InternetAccess = 3,

    /// <summary>The device has internet access but it is behind a captive portal or similar constraint.</summary>
    ConstrainedInternetAccess = 4,
}

/// <summary>
/// Mirrors <c>XNetworkingConnectivityCostHint</c> from XNetworking.h.
/// Describes the monetary or data cost of the active connection.
/// </summary>
public enum NetworkingConnectivityCostHint : uint
{
    /// <summary>The cost type cannot be determined.</summary>
    Unknown = 0,

    /// <summary>The connection is unrestricted (e.g. wired Ethernet or Wi-Fi on a flat-rate plan).</summary>
    Unrestricted = 1,

    /// <summary>The connection has a fixed data cap.</summary>
    Fixed = 2,

    /// <summary>The connection is metered (e.g. cellular).</summary>
    Variable = 3,
}

/// <summary>
/// Mirrors <c>XNetworkingConfigurationSetting</c> from XNetworking.h.
/// Identifies a per-partition TCP receive-buffer configuration value.
/// </summary>
public enum NetworkingConfigurationSetting : uint
{
    /// <summary>Maximum queued TCP receive bytes for the title partition.</summary>
    MaxTitleTcpQueuedReceiveBufferSize = 0,

    /// <summary>Maximum queued TCP receive bytes for the system partition.</summary>
    MaxSystemTcpQueuedReceiveBufferSize = 1,

    /// <summary>Maximum queued TCP receive bytes for the tools partition.</summary>
    MaxToolsTcpQueuedReceiveBufferSize = 2,
}

/// <summary>
/// Mirrors <c>XNetworkingStatisticsType</c> from XNetworking.h.
/// Identifies a statistics counter set to query.
/// </summary>
public enum NetworkingStatisticsType : uint
{
    /// <summary>TCP queued receive buffer counters for the title partition.</summary>
    TitleTcpQueuedReceivedBufferUsage = 0,

    /// <summary>TCP queued receive buffer counters for the system partition.</summary>
    SystemTcpQueuedReceivedBufferUsage = 1,

    /// <summary>TCP queued receive buffer counters for the tools partition.</summary>
    ToolsTcpQueuedReceivedBufferUsage = 2,
}

/// <summary>
/// Mirrors <c>XNetworkingThumbprintType</c> from XNetworking.h.
/// </summary>
public enum NetworkingThumbprintType : uint
{
    /// <summary>The thumbprint is for the leaf (end-entity) certificate.</summary>
    Leaf = 0,

    /// <summary>The thumbprint is for an intermediate issuer certificate.</summary>
    Issuer = 1,

    /// <summary>The thumbprint is for the root certificate.</summary>
    Root = 2,
}

/// <summary>
/// A snapshot of the device's network connectivity state.
/// Mirrors <c>XNetworkingConnectivityHint</c> from XNetworking.h.
/// </summary>
public readonly struct NetworkingConnectivityHint
{
    /// <summary>The device's current internet connectivity level.</summary>
    public NetworkingConnectivityLevelHint ConnectivityLevel { get; }

    /// <summary>The monetary or data cost associated with the active network connection.</summary>
    public NetworkingConnectivityCostHint ConnectivityCost { get; }

    /// <summary>The IANA interface type (see <c>IF_TYPE_*</c> in ipifcons.h).</summary>
    public uint IanaInterfaceType { get; }

    /// <summary>Whether the network stack has fully initialized.</summary>
    public bool NetworkInitialized { get; }

    /// <summary>Whether the device is approaching its data limit.</summary>
    public bool ApproachingDataLimit { get; }

    /// <summary>Whether the device has exceeded its data limit.</summary>
    public bool OverDataLimit { get; }

    /// <summary>Whether the device is roaming.</summary>
    public bool Roaming { get; }

    internal NetworkingConnectivityHint(
        NetworkingConnectivityLevelHint connectivityLevel,
        NetworkingConnectivityCostHint connectivityCost,
        uint ianaInterfaceType,
        bool networkInitialized,
        bool approachingDataLimit,
        bool overDataLimit,
        bool roaming)
    {
        ConnectivityLevel = connectivityLevel;
        ConnectivityCost = connectivityCost;
        IanaInterfaceType = ianaInterfaceType;
        NetworkInitialized = networkInitialized;
        ApproachingDataLimit = approachingDataLimit;
        OverDataLimit = overDataLimit;
        Roaming = roaming;
    }

    internal static NetworkingConnectivityHint FromNative(XNetworkingConnectivityHint native) =>
        new NetworkingConnectivityHint(
            (NetworkingConnectivityLevelHint)native.connectivityLevel,
            (NetworkingConnectivityCostHint)native.connectivityCost,
            native.ianaInterfaceType,
            native.networkInitialized != 0,
            native.approachingDataLimit != 0,
            native.overDataLimit != 0,
            native.roaming != 0);
}

/// <summary>
/// A certificate thumbprint for TLS certificate pinning.
/// Mirrors <c>XNetworkingThumbprint</c> from XNetworking.h.
/// </summary>
public sealed class NetworkingThumbprint
{
    internal NetworkingThumbprint(NetworkingThumbprintType type, byte[] data)
    {
        ThumbprintType = type;
        Data = data;
    }

    /// <summary>Which certificate in the chain this thumbprint corresponds to.</summary>
    public NetworkingThumbprintType ThumbprintType { get; }

    /// <summary>The raw thumbprint bytes.</summary>
    public IReadOnlyList<byte> Data { get; }
}

/// <summary>
/// TLS security information for a URL, used with <see cref="NetworkingManager.VerifyServerCertificate"/>.
/// Mirrors <c>XNetworkingSecurityInformation</c> from XNetworking.h.
/// </summary>
/// <remarks>
/// <para>
/// This object holds a pinned managed buffer whose address was given to the native result call.
/// The native <c>XNetworkingSecurityInformation*</c> pointer (and its embedded thumbprint pointers)
/// all point into that buffer, so the buffer must stay pinned and at the same address until the
/// object is disposed.
/// </para>
/// <para>
/// Call <see cref="Dispose"/> when the object is no longer needed. After disposal it must not be
/// passed to <see cref="NetworkingManager.VerifyServerCertificate"/>.
/// </para>
/// </remarks>
public sealed unsafe class NetworkingSecurityInformation : IDisposable
{
    // GCHandle.Pinned keeps the byte[] at a fixed address. The native struct and its embedded
    // thumbprint pointers all live inside this buffer; they are valid for the object's lifetime.
    private System.Runtime.InteropServices.GCHandle _pin;
    private readonly int _nativeInfoOffset;
    private bool _disposed;

    /// <summary>
    /// Accepts a GCHandle that is already allocated with GCHandleType.Pinned. Ownership is
    /// transferred; the caller must not free the handle independently.
    /// </summary>
    internal NetworkingSecurityInformation(
        System.Runtime.InteropServices.GCHandle pin,
        int nativeInfoOffset,
        uint enabledHttpSecurityProtocolFlags,
        NetworkingThumbprint[] thumbprints)
    {
        _pin = pin;
        _nativeInfoOffset = nativeInfoOffset;
        EnabledHttpSecurityProtocolFlags = enabledHttpSecurityProtocolFlags;
        Thumbprints = Array.AsReadOnly(thumbprints);
    }

    /// <summary>The set of enabled HTTP security protocol flags.</summary>
    public uint EnabledHttpSecurityProtocolFlags { get; }

    /// <summary>The certificate thumbprints associated with this URL's server certificate chain.</summary>
    public IReadOnlyList<NetworkingThumbprint> Thumbprints { get; }

    /// <summary>
    /// Disposes the object and releases the pinned buffer. After this call the object
    /// must not be passed to <see cref="NetworkingManager.VerifyServerCertificate"/>.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        if (_pin.IsAllocated)
        {
            _pin.Free();
        }
    }

    internal GDK.Net.Interop.XNetworkingSecurityInformation* GetNativePointer()
    {
        ThrowIfDisposed();
        return (GDK.Net.Interop.XNetworkingSecurityInformation*)(
            (byte*)_pin.AddrOfPinnedObject() + _nativeInfoOffset);
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(NetworkingSecurityInformation));
        }
    }
}

/// <summary>
/// TCP receive-buffer usage statistics. Mirrors <c>XNetworkingTcpQueuedReceivedBufferUsageStatistics</c>.
/// </summary>
public readonly struct NetworkingTcpStatistics
{
    internal NetworkingTcpStatistics(
        ulong numBytesCurrentlyQueued,
        ulong peakNumBytesEverQueued,
        ulong totalNumBytesQueued,
        ulong numBytesDroppedForExceedingConfiguredMax,
        ulong numBytesDroppedDueToAnyFailure)
    {
        NumBytesCurrentlyQueued = numBytesCurrentlyQueued;
        PeakNumBytesEverQueued = peakNumBytesEverQueued;
        TotalNumBytesQueued = totalNumBytesQueued;
        NumBytesDroppedForExceedingConfiguredMax = numBytesDroppedForExceedingConfiguredMax;
        NumBytesDroppedDueToAnyFailure = numBytesDroppedDueToAnyFailure;
    }

    /// <summary>Bytes currently sitting in the queued receive buffer.</summary>
    public ulong NumBytesCurrentlyQueued { get; }

    /// <summary>Peak number of bytes ever simultaneously queued.</summary>
    public ulong PeakNumBytesEverQueued { get; }

    /// <summary>Total bytes queued since the runtime started.</summary>
    public ulong TotalNumBytesQueued { get; }

    /// <summary>Bytes dropped because the configured maximum was exceeded.</summary>
    public ulong NumBytesDroppedForExceedingConfiguredMax { get; }

    /// <summary>Bytes dropped for any failure reason.</summary>
    public ulong NumBytesDroppedDueToAnyFailure { get; }
}

/// <summary>Payload for <see cref="NetworkingManager.PreferredLocalUdpMultiplayerPortChanged"/>.</summary>
public sealed class PreferredLocalUdpMultiplayerPortChangedEventArgs : EventArgs
{
    internal PreferredLocalUdpMultiplayerPortChangedEventArgs(ushort port)
    {
        Port = port;
    }

    /// <summary>The new preferred local UDP multiplayer port.</summary>
    public ushort Port { get; }
}

/// <summary>Payload for <see cref="NetworkingManager.ConnectivityHintChanged"/>.</summary>
public sealed class ConnectivityHintChangedEventArgs : EventArgs
{
    internal ConnectivityHintChangedEventArgs(NetworkingConnectivityHint hint)
    {
        Hint = hint;
    }

    /// <summary>The updated connectivity hint.</summary>
    public NetworkingConnectivityHint Hint { get; }
}
