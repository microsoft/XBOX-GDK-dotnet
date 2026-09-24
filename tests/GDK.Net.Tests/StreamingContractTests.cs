using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.Streaming;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Guards the XGameStreaming raw interop layer and idiomatic types against silent drift from the
/// GDK headers (<c>XGameStreaming.h</c>, edition 260404). These are pure compile-time / layout
/// checks: nothing here loads <c>xgameruntime.thunks.dll</c>.
/// </summary>
public sealed unsafe class StreamingContractTests
{
    // ── Native enum values ─────────────────────────────────────────────────────────────────────

    [Theory]
    [InlineData(0u, 0u)] // XGameStreamingConnectionState.Disconnected
    [InlineData(1u, 1u)] // XGameStreamingConnectionState.Connected
    public void NativeConnectionStateMatchesHeader(uint value, uint expected)
        => Assert.Equal(expected, value);

    [Theory]
    [InlineData(0u, 0u)] // None
    [InlineData(1u, 1u)] // StreamPhysicalDimensions
    [InlineData(2u, 2u)] // TouchInputEnabled
    [InlineData(4u, 4u)] // TouchBundleVersion
    [InlineData(5u, 5u)] // IPAddress
    [InlineData(6u, 6u)] // SessionId
    [InlineData(7u, 7u)] // DisplayDetails
    public void NativeClientPropertyMatchesHeader(uint value, uint expected)
        => Assert.Equal(expected, value);

    [Theory]
    [InlineData(0x0u, 0x0u)] // None
    [InlineData(0x1u, 0x1u)] // SupportsCustomAspectRatio
    [InlineData(0x2u, 0x2u)] // SupportsPresentScaling
    [InlineData(0x3u, 0x3u)] // All
    public void NativeVideoFlagsMatchesHeader(uint value, uint expected)
        => Assert.Equal(expected, value);

    [Theory]
    [InlineData(0u, 0u)] // Boolean
    [InlineData(1u, 1u)] // Integer
    [InlineData(2u, 2u)] // Double
    [InlineData(3u, 3u)] // String
    public void NativeTouchValueKindMatchesHeader(uint value, uint expected)
        => Assert.Equal(expected, value);

    // ── Public enum projection ──────────────────────────────────────────────────────────────────

    [Theory]
    [InlineData(StreamingConnectionState.Disconnected, 0u)]
    [InlineData(StreamingConnectionState.Connected,    1u)]
    public void PublicConnectionStateProjectsOntoNative(StreamingConnectionState pub, uint expected)
    {
        Assert.Equal(expected, (uint)pub);
        Assert.Equal(expected, (uint)(XGameStreamingConnectionState)pub);
    }

    [Theory]
    [InlineData(StreamingClientProperty.StreamPhysicalDimensions, 1u)]
    [InlineData(StreamingClientProperty.DisplayDetails,           7u)]
    public void PublicClientPropertyProjectsOntoNative(StreamingClientProperty pub, uint expected)
    {
        Assert.Equal(expected, (uint)pub);
        Assert.Equal(expected, (uint)(XGameStreamingClientProperty)pub);
    }

    [Theory]
    [InlineData(StreamingVideoFlags.SupportsCustomAspectRatio, 0x1u)]
    [InlineData(StreamingVideoFlags.SupportsPresentScaling,    0x2u)]
    [InlineData(StreamingVideoFlags.All,                       0x3u)]
    public void PublicVideoFlagsIsAFlagsEnumMatchingNative(StreamingVideoFlags pub, uint expected)
    {
        Assert.Equal(expected, (uint)pub);
        Assert.Equal(expected, (uint)(XGameStreamingVideoFlags)pub);
    }

    [Theory]
    [InlineData(TouchControlsStateValueKind.Boolean, 0u)]
    [InlineData(TouchControlsStateValueKind.Integer, 1u)]
    [InlineData(TouchControlsStateValueKind.Double,  2u)]
    [InlineData(TouchControlsStateValueKind.String,  3u)]
    public void PublicTouchValueKindProjectsOntoNative(TouchControlsStateValueKind pub, uint expected)
    {
        Assert.Equal(expected, (uint)pub);
        Assert.Equal(expected, (uint)(XGameStreamingTouchControlsStateValueKind)pub);
    }

    // ── Struct layout ───────────────────────────────────────────────────────────────────────────

    [Fact]
    public void XVersionExplicitLayoutIsCorrect()
    {
        // The union has four uint16 components and a uint64 Value at offset 0.
        Assert.Equal(8, Marshal.SizeOf<XVersion>());
        Assert.Equal(0, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Major)));
        Assert.Equal(2, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Minor)));
        Assert.Equal(4, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Build)));
        Assert.Equal(6, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Revision)));
        Assert.Equal(0, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Value)));
    }

    [Fact]
    public void XVersionRoundtripsComponentsThroughValue()
    {
        // On little-endian x64: Value = major | (minor<<16) | (build<<32) | (revision<<48)
        XVersion v;
        v.Value    = 0;
        v.Major    = 1;
        v.Minor    = 2;
        v.Build    = 3;
        v.Revision = 4;

        Assert.Equal(1u, v.Major);
        Assert.Equal(2u, v.Minor);
        Assert.Equal(3u, v.Build);
        Assert.Equal(4u, v.Revision);
    }

    [Fact]
    public void XGameStreamingDisplayDetailsIs40Bytes()
    {
        // preferredW(4) + preferredH(4) + RECT(16) + maxPixels(4) + maxW(4) + maxH(4) + flags(4)
        Assert.Equal(40, Marshal.SizeOf<XGameStreamingDisplayDetails>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.PreferredWidth)));
        Assert.Equal(4,  (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.PreferredHeight)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.SafeAreaLeft)));
        Assert.Equal(12, (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.SafeAreaTop)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.SafeAreaRight)));
        Assert.Equal(20, (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.SafeAreaBottom)));
        Assert.Equal(24, (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.MaxPixels)));
        Assert.Equal(28, (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.MaxWidth)));
        Assert.Equal(32, (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.MaxHeight)));
        Assert.Equal(36, (int)Marshal.OffsetOf<XGameStreamingDisplayDetails>(nameof(XGameStreamingDisplayDetails.Flags)));
    }

    [Fact]
    public void XGameStreamingTouchControlsStateValueIs16Bytes()
    {
        // valueKind(4) + pad(4) + union(8) = 16 bytes; union starts at offset 8.
        Assert.Equal(16, sizeof(XGameStreamingTouchControlsStateValue));
        Assert.Equal(0,  (int)Marshal.OffsetOf<XGameStreamingTouchControlsStateValue>(nameof(XGameStreamingTouchControlsStateValue.ValueKind)));
        // All union members are at offset 8:
        Assert.Equal(8, (int)Marshal.OffsetOf<XGameStreamingTouchControlsStateValue>(nameof(XGameStreamingTouchControlsStateValue.BooleanValue)));
        Assert.Equal(8, (int)Marshal.OffsetOf<XGameStreamingTouchControlsStateValue>(nameof(XGameStreamingTouchControlsStateValue.IntegerValue)));
        Assert.Equal(8, (int)Marshal.OffsetOf<XGameStreamingTouchControlsStateValue>(nameof(XGameStreamingTouchControlsStateValue.DoubleValue)));
    }

    [Fact]
    public void XGameStreamingTouchControlsStateOperationIs32Bytes()
    {
        // operationKind(4) + pad(4) + path*(8) + value(16) = 32 bytes
        Assert.Equal(32, sizeof(XGameStreamingTouchControlsStateOperation));
        Assert.Equal(0,  (int)Marshal.OffsetOf<XGameStreamingTouchControlsStateOperation>(nameof(XGameStreamingTouchControlsStateOperation.OperationKind)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XGameStreamingTouchControlsStateOperation>(nameof(XGameStreamingTouchControlsStateOperation.Path)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XGameStreamingTouchControlsStateOperation>(nameof(XGameStreamingTouchControlsStateOperation.Value)));
    }

    // ── Trampoline pointers ─────────────────────────────────────────────────────────────────────

    [Fact]
    public void StreamingTrampolinesAreNonNullAndDistinct()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.StreamingConnectionStateChangedCallback);
        Assert.NotEqual(IntPtr.Zero, Trampolines.StreamingClientPropertiesChangedCallback);
        Assert.NotEqual(
            Trampolines.StreamingConnectionStateChangedCallback,
            Trampolines.StreamingClientPropertiesChangedCallback);
    }

    // ── HResult codes ───────────────────────────────────────────────────────────────────────────

    [Theory]
    [InlineData(HResult.EGameStreamingNotInitialized,                unchecked((int)0x89245400))]
    [InlineData(HResult.EGameStreamingClientNotConnected,            unchecked((int)0x89245401))]
    [InlineData(HResult.EGameStreamingNoData,                        unchecked((int)0x89245402))]
    [InlineData(HResult.EGameStreamingNoDataCenter,                  unchecked((int)0x89245403))]
    [InlineData(HResult.EGameStreamingNotStreamingController,        unchecked((int)0x89245404))]
    [InlineData(HResult.EGameStreamingNoMatch,                       unchecked((int)0x89245405))]
    [InlineData(HResult.EGameStreamingTooManyCalls,                  unchecked((int)0x89245406))]
    [InlineData(HResult.EGameStreamingCustomResolutionNotSupported,  unchecked((int)0x89245407))]
    [InlineData(HResult.EGameStreamingCustomResolutionTooSmall,      unchecked((int)0x89245408))]
    [InlineData(HResult.EGameStreamingCustomResolutionTooLarge,      unchecked((int)0x89245409))]
    [InlineData(HResult.EGameStreamingCustomResolutionTooManyPixels, unchecked((int)0x8924540A))]
    [InlineData(HResult.EGameStreamingInvalidCustomResolution,       unchecked((int)0x8924540B))]
    public void StreamingHResultMatchesHeader(int code, int expected)
        => Assert.Equal(expected, code);

    [Fact]
    public void StreamingHResultHasFriendlyDescription()
    {
        string msg = HResult.Describe(HResult.EGameStreamingNotInitialized);
        Assert.False(string.IsNullOrWhiteSpace(msg));
        Assert.DoesNotContain("0x", msg, StringComparison.Ordinal);
    }

    // ── Argument validation ─────────────────────────────────────────────────────────────────────

    [Fact]
    public void TouchControlsStateOperationThrowsOnNullPath()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new TouchControlsStateOperation(null!, TouchControlsStateValue.FromBoolean(true)));
    }

    [Fact]
    public void TouchControlsStateValueFactoriesReturnCorrectKind()
    {
        Assert.Equal(TouchControlsStateValueKind.Boolean, TouchControlsStateValue.FromBoolean(true).Kind);
        Assert.Equal(TouchControlsStateValueKind.Integer, TouchControlsStateValue.FromInteger(42).Kind);
        Assert.Equal(TouchControlsStateValueKind.Double,  TouchControlsStateValue.FromDouble(3.14).Kind);
        Assert.Equal(TouchControlsStateValueKind.String,  TouchControlsStateValue.FromString("x").Kind);
    }

    [Fact]
    public void TouchControlsStateValuePreservesPayloads()
    {
        Assert.True(TouchControlsStateValue.FromBoolean(true).BooleanValue);
        Assert.False(TouchControlsStateValue.FromBoolean(false).BooleanValue);
        Assert.Equal(99L, TouchControlsStateValue.FromInteger(99).IntegerValue);
        Assert.Equal(2.71, TouchControlsStateValue.FromDouble(2.71).DoubleValue);
        Assert.Equal("hello", TouchControlsStateValue.FromString("hello").StringValue);
    }

    [Fact]
    public void StreamingClientIdNullIsZero()
    {
        Assert.Equal(0UL, StreamingClientId.Null.Value);
        Assert.True(StreamingClientId.Null.IsNull);
    }

    [Fact]
    public void StreamingClientIdEqualityIsByValue()
    {
        var a = new StreamingClientId(42);
        var b = new StreamingClientId(42);
        var c = new StreamingClientId(99);

        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.True(a == b);
        Assert.True(a != c);
    }
}
