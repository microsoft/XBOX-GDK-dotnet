using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.Networking;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Guards the XNetworking raw interop layer and public projection against silent drift from the
/// GDK headers (%GameDKCoreLatest%windows\include\XNetworking.h, edition 260404).
/// These are pure compile-time/layout checks — nothing here loads xgameruntime.thunks.dll.
/// </summary>
public sealed unsafe class NetworkingContractTests
{
    // ── Struct layout ────────────────────────────────────────────────────────────────────────────

    [Fact]
    public void ConnectivityHintStructMatchesNativeLayout()
    {
        // struct XNetworkingConnectivityHint: 3×uint32 (12) + 4×bool/byte (4) = 16 bytes, no padding.
        Assert.Equal(16, Marshal.SizeOf<XNetworkingConnectivityHint>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XNetworkingConnectivityHint>(nameof(XNetworkingConnectivityHint.connectivityLevel)));
        Assert.Equal(4,  (int)Marshal.OffsetOf<XNetworkingConnectivityHint>(nameof(XNetworkingConnectivityHint.connectivityCost)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XNetworkingConnectivityHint>(nameof(XNetworkingConnectivityHint.ianaInterfaceType)));
        Assert.Equal(12, (int)Marshal.OffsetOf<XNetworkingConnectivityHint>(nameof(XNetworkingConnectivityHint.networkInitialized)));
        Assert.Equal(13, (int)Marshal.OffsetOf<XNetworkingConnectivityHint>(nameof(XNetworkingConnectivityHint.approachingDataLimit)));
        Assert.Equal(14, (int)Marshal.OffsetOf<XNetworkingConnectivityHint>(nameof(XNetworkingConnectivityHint.overDataLimit)));
        Assert.Equal(15, (int)Marshal.OffsetOf<XNetworkingConnectivityHint>(nameof(XNetworkingConnectivityHint.roaming)));
    }

    [Fact]
    public void ThumbprintStructMatchesNativeLayout()
    {
        // struct XNetworkingThumbprint (x64):
        //   thumbprintType (4) + implicit padding (4) + thumbprintBufferByteCount (8) + thumbprintBuffer (8) = 24 bytes.
        Assert.Equal(24, Marshal.SizeOf<XNetworkingThumbprint>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XNetworkingThumbprint>(nameof(XNetworkingThumbprint.thumbprintType)));
        // offset 4 is implicit padding; next field at 8.
        Assert.Equal(8,  (int)Marshal.OffsetOf<XNetworkingThumbprint>(nameof(XNetworkingThumbprint.thumbprintBufferByteCount)));
        Assert.Equal(8 + IntPtr.Size, (int)Marshal.OffsetOf<XNetworkingThumbprint>(nameof(XNetworkingThumbprint.thumbprintBuffer)));
    }

    [Fact]
    public void SecurityInformationStructMatchesNativeLayout()
    {
        // struct XNetworkingSecurityInformation (x64):
        //   enabledHttpSecurityProtocolFlags (4) + implicit padding (4) + thumbprintCount (8) + thumbprints (8) = 24 bytes.
        Assert.Equal(24, Marshal.SizeOf<XNetworkingSecurityInformation>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XNetworkingSecurityInformation>(nameof(XNetworkingSecurityInformation.enabledHttpSecurityProtocolFlags)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XNetworkingSecurityInformation>(nameof(XNetworkingSecurityInformation.thumbprintCount)));
        Assert.Equal(8 + IntPtr.Size, (int)Marshal.OffsetOf<XNetworkingSecurityInformation>(nameof(XNetworkingSecurityInformation.thumbprints)));
    }

    [Fact]
    public void TcpStatisticsStructMatchesNativeLayout()
    {
        // struct XNetworkingTcpQueuedReceivedBufferUsageStatistics: 5 × uint64 = 40 bytes.
        Assert.Equal(40, Marshal.SizeOf<XNetworkingTcpQueuedReceivedBufferUsageStatistics>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XNetworkingTcpQueuedReceivedBufferUsageStatistics>(nameof(XNetworkingTcpQueuedReceivedBufferUsageStatistics.numBytesCurrentlyQueued)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XNetworkingTcpQueuedReceivedBufferUsageStatistics>(nameof(XNetworkingTcpQueuedReceivedBufferUsageStatistics.peakNumBytesEverQueued)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XNetworkingTcpQueuedReceivedBufferUsageStatistics>(nameof(XNetworkingTcpQueuedReceivedBufferUsageStatistics.totalNumBytesQueued)));
        Assert.Equal(24, (int)Marshal.OffsetOf<XNetworkingTcpQueuedReceivedBufferUsageStatistics>(nameof(XNetworkingTcpQueuedReceivedBufferUsageStatistics.numBytesDroppedForExceedingConfiguredMax)));
        Assert.Equal(32, (int)Marshal.OffsetOf<XNetworkingTcpQueuedReceivedBufferUsageStatistics>(nameof(XNetworkingTcpQueuedReceivedBufferUsageStatistics.numBytesDroppedDueToAnyFailure)));
    }

    [Fact]
    public void StatisticsBufferUnionMatchesNativeLayout()
    {
        // union XNetworkingStatisticsBuffer: single member = 40 bytes; explicit layout at offset 0.
        Assert.Equal(40, Marshal.SizeOf<XNetworkingStatisticsBuffer>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XNetworkingStatisticsBuffer>(nameof(XNetworkingStatisticsBuffer.tcpQueuedReceiveBufferUsage)));
    }

    // ── Enum values ──────────────────────────────────────────────────────────────────────────────

    [Fact]
    public void ConnectivityLevelHintNativeValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XNetworkingConnectivityLevelHint.Unknown);
        Assert.Equal(1u, (uint)XNetworkingConnectivityLevelHint.None);
        Assert.Equal(2u, (uint)XNetworkingConnectivityLevelHint.LocalAccess);
        Assert.Equal(3u, (uint)XNetworkingConnectivityLevelHint.InternetAccess);
        Assert.Equal(4u, (uint)XNetworkingConnectivityLevelHint.ConstrainedInternetAccess);
    }

    [Fact]
    public void ConnectivityCostHintNativeValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XNetworkingConnectivityCostHint.Unknown);
        Assert.Equal(1u, (uint)XNetworkingConnectivityCostHint.Unrestricted);
        Assert.Equal(2u, (uint)XNetworkingConnectivityCostHint.Fixed);
        Assert.Equal(3u, (uint)XNetworkingConnectivityCostHint.Variable);
    }

    [Fact]
    public void ThumbprintTypeNativeValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XNetworkingThumbprintType.Leaf);
        Assert.Equal(1u, (uint)XNetworkingThumbprintType.Issuer);
        Assert.Equal(2u, (uint)XNetworkingThumbprintType.Root);
    }

    [Fact]
    public void ConfigurationSettingNativeValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XNetworkingConfigurationSetting.MaxTitleTcpQueuedReceiveBufferSize);
        Assert.Equal(1u, (uint)XNetworkingConfigurationSetting.MaxSystemTcpQueuedReceiveBufferSize);
        Assert.Equal(2u, (uint)XNetworkingConfigurationSetting.MaxToolsTcpQueuedReceiveBufferSize);
    }

    [Fact]
    public void StatisticsTypeNativeValuesMatchHeader()
    {
        Assert.Equal(0u, (uint)XNetworkingStatisticsType.TitleTcpQueuedReceivedBufferUsage);
        Assert.Equal(1u, (uint)XNetworkingStatisticsType.SystemTcpQueuedReceivedBufferUsage);
        Assert.Equal(2u, (uint)XNetworkingStatisticsType.ToolsTcpQueuedReceivedBufferUsage);
    }

    // ── Public enum projection ────────────────────────────────────────────────────────────────────

    [Theory]
    [InlineData(NetworkingConnectivityLevelHint.Unknown, 0u)]
    [InlineData(NetworkingConnectivityLevelHint.None, 1u)]
    [InlineData(NetworkingConnectivityLevelHint.LocalAccess, 2u)]
    [InlineData(NetworkingConnectivityLevelHint.InternetAccess, 3u)]
    [InlineData(NetworkingConnectivityLevelHint.ConstrainedInternetAccess, 4u)]
    public void PublicConnectivityLevelHintMatchesNative(NetworkingConnectivityLevelHint value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XNetworkingConnectivityLevelHint)value);
    }

    [Theory]
    [InlineData(NetworkingConnectivityCostHint.Unknown, 0u)]
    [InlineData(NetworkingConnectivityCostHint.Unrestricted, 1u)]
    [InlineData(NetworkingConnectivityCostHint.Fixed, 2u)]
    [InlineData(NetworkingConnectivityCostHint.Variable, 3u)]
    public void PublicConnectivityCostHintMatchesNative(NetworkingConnectivityCostHint value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XNetworkingConnectivityCostHint)value);
    }

    [Theory]
    [InlineData(NetworkingThumbprintType.Leaf, 0u)]
    [InlineData(NetworkingThumbprintType.Issuer, 1u)]
    [InlineData(NetworkingThumbprintType.Root, 2u)]
    public void PublicThumbprintTypeMatchesNative(NetworkingThumbprintType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XNetworkingThumbprintType)value);
    }

    [Theory]
    [InlineData(NetworkingConfigurationSetting.MaxTitleTcpQueuedReceiveBufferSize, 0u)]
    [InlineData(NetworkingConfigurationSetting.MaxSystemTcpQueuedReceiveBufferSize, 1u)]
    [InlineData(NetworkingConfigurationSetting.MaxToolsTcpQueuedReceiveBufferSize, 2u)]
    public void PublicConfigurationSettingMatchesNative(NetworkingConfigurationSetting value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XNetworkingConfigurationSetting)value);
    }

    [Theory]
    [InlineData(NetworkingStatisticsType.TitleTcpQueuedReceivedBufferUsage, 0u)]
    [InlineData(NetworkingStatisticsType.SystemTcpQueuedReceivedBufferUsage, 1u)]
    [InlineData(NetworkingStatisticsType.ToolsTcpQueuedReceivedBufferUsage, 2u)]
    public void PublicStatisticsTypeMatchesNative(NetworkingStatisticsType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XNetworkingStatisticsType)value);
    }

    // ── Callback thunks ──────────────────────────────────────────────────────────────────────────

    [Fact]
    public void NetworkingTrampolinesResolveToRealFunctionPointers()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.NetworkingUdpPortChangedCallback);
        Assert.NotEqual(IntPtr.Zero, Trampolines.NetworkingConnectivityHintChangedCallback);
        Assert.NotEqual(
            Trampolines.NetworkingUdpPortChangedCallback,
            Trampolines.NetworkingConnectivityHintChangedCallback);
    }

    [Fact]
    public void NetworkingTrampolinesAreDistinctFromOtherTrampolines()
    {
        Assert.NotEqual(Trampolines.AsyncCompletionRoutine, Trampolines.NetworkingUdpPortChangedCallback);
        Assert.NotEqual(Trampolines.AsyncCompletionRoutine, Trampolines.NetworkingConnectivityHintChangedCallback);
    }

    // ── Argument validation ───────────────────────────────────────────────────────────────────────

    [Fact]
    public void SecurityInformationDisposeIsIdempotent()
    {
        // Construct a minimal NetworkingSecurityInformation via GCHandle.Pinned of a dummy buffer.
        byte[] buffer = new byte[64];
        var pin = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var sinfo = new NetworkingSecurityInformation(pin, 0, 0u, Array.Empty<NetworkingThumbprint>());

        // First dispose releases the pin.
        sinfo.Dispose();

        // Second dispose must not throw.
        sinfo.Dispose();
    }

    [Fact]
    public void SecurityInformationGetNativePointerThrowsAfterDispose()
    {
        byte[] buffer = new byte[64];
        var pin = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var sinfo = new NetworkingSecurityInformation(pin, 0, 0u, Array.Empty<NetworkingThumbprint>());

        sinfo.Dispose();

        // GetNativePointer() returns a pointer, so it cannot be called inside a lambda for
        // Assert.Throws; use a local try/catch instead.
        bool threw = false;
        try
        {
            void* p = sinfo.GetNativePointer();
            _ = p;
        }
        catch (ObjectDisposedException)
        {
            threw = true;
        }

        Assert.True(threw);
    }

    [Fact]
    public void ConnectivityHintFromNativeRoundTrips()
    {
        var native = new XNetworkingConnectivityHint
        {
            connectivityLevel = XNetworkingConnectivityLevelHint.InternetAccess,
            connectivityCost = XNetworkingConnectivityCostHint.Unrestricted,
            ianaInterfaceType = 6u,
            networkInitialized = 1,
            approachingDataLimit = 0,
            overDataLimit = 0,
            roaming = 1,
        };

        NetworkingConnectivityHint hint = NetworkingConnectivityHint.FromNative(native);

        Assert.Equal(NetworkingConnectivityLevelHint.InternetAccess, hint.ConnectivityLevel);
        Assert.Equal(NetworkingConnectivityCostHint.Unrestricted, hint.ConnectivityCost);
        Assert.Equal(6u, hint.IanaInterfaceType);
        Assert.True(hint.NetworkInitialized);
        Assert.False(hint.ApproachingDataLimit);
        Assert.False(hint.OverDataLimit);
        Assert.True(hint.Roaming);
    }

    [Fact]
    public void TcpStatisticsRoundTrips()
    {
        var stats = new NetworkingTcpStatistics(10, 20, 30, 4, 5);

        Assert.Equal(10uL, stats.NumBytesCurrentlyQueued);
        Assert.Equal(20uL, stats.PeakNumBytesEverQueued);
        Assert.Equal(30uL, stats.TotalNumBytesQueued);
        Assert.Equal(4uL,  stats.NumBytesDroppedForExceedingConfiguredMax);
        Assert.Equal(5uL,  stats.NumBytesDroppedDueToAnyFailure);
    }
}
