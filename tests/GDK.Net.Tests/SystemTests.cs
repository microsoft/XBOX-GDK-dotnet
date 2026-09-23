using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.SystemInfo;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for the System, Thread, Error, Game, Launcher, and Display API families.
/// No test here loads xgameruntime.thunks.dll; all assertions are pure layout and enum-value checks.
/// </summary>
public sealed unsafe class SystemTests
{
    // ─── XVersion ────────────────────────────────────────────────────────────────

    [Fact]
    public void XVersionSizeIsEight()
    {
        Assert.Equal(8, Marshal.SizeOf<XVersion>());
    }

    [Fact]
    public void XVersionUnionOverlaysFieldsWithPackedValue()
    {
        // On little-endian (all GDK targets): Value = Major | (Minor << 16) | (Build << 32) | (Revision << 48)
        var v = new XVersion { Major = 1, Minor = 2, Build = 3, Revision = 4 };
        ulong expected = 1UL | (2UL << 16) | (3UL << 32) | (4UL << 48);
        Assert.Equal(expected, v.Value);
    }

    [Fact]
    public void XVersionOffsetsMajorAtZero()
    {
        Assert.Equal(0, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Major)));
    }

    // ─── XSystemAnalyticsInfo ────────────────────────────────────────────────────

    [Fact]
    public void XSystemAnalyticsInfoSizeIs144()
    {
        // 8 (OsVersion) + 8 (HostingOsVersion) + 64 (Family) + 64 (Form) = 144
        Assert.Equal(144, Marshal.SizeOf<XSystemAnalyticsInfo>());
    }

    [Fact]
    public void XSystemAnalyticsInfoOsVersionAtOffset0()
    {
        Assert.Equal(0, (int)Marshal.OffsetOf<XSystemAnalyticsInfo>(nameof(XSystemAnalyticsInfo.OsVersion)));
    }

    [Fact]
    public void XSystemAnalyticsInfoHostingOsVersionAtOffset8()
    {
        Assert.Equal(8, (int)Marshal.OffsetOf<XSystemAnalyticsInfo>(nameof(XSystemAnalyticsInfo.HostingOsVersion)));
    }

    // ─── XSystemRuntimeInfo ──────────────────────────────────────────────────────

    [Fact]
    public void XSystemRuntimeInfoSizeIs16()
    {
        // 8 (RuntimeVersion) + 8 (AvailableVersion)
        Assert.Equal(16, Marshal.SizeOf<XSystemRuntimeInfo>());
    }

    // ─── XDisplayHdrModeInfo ─────────────────────────────────────────────────────

    [Fact]
    public void XDisplayHdrModeInfoSizeIs12()
    {
        // 3 × float (4 bytes each)
        Assert.Equal(12, Marshal.SizeOf<XDisplayHdrModeInfo>());
    }

    // ─── Buffer size constants ────────────────────────────────────────────────────

    [Fact]
    public void ConsoleIdBytesMatchesHeader()
    {
        Assert.Equal(39u, (uint)XSystemConstants.ConsoleIdBytes);
    }

    [Fact]
    public void XboxLiveSandboxIdMaxBytesMatchesHeader()
    {
        Assert.Equal(16u, (uint)XSystemConstants.XboxLiveSandboxIdMaxBytes);
    }

    [Fact]
    public void AppSpecificDeviceIdBytesMatchesHeader()
    {
        Assert.Equal(45u, (uint)XSystemConstants.AppSpecificDeviceIdBytes);
    }

    // ─── SystemDeviceType ────────────────────────────────────────────────────────

    [Theory]
    [InlineData(SystemDeviceType.Unknown,              0x00u)]
    [InlineData(SystemDeviceType.Pc,                   0x01u)]
    [InlineData(SystemDeviceType.XboxOne,              0x02u)]
    [InlineData(SystemDeviceType.XboxOneS,             0x03u)]
    [InlineData(SystemDeviceType.XboxOneX,             0x04u)]
    [InlineData(SystemDeviceType.XboxOneXDevkit,       0x05u)]
    [InlineData(SystemDeviceType.XboxScarlettLockhart, 0x06u)]
    [InlineData(SystemDeviceType.XboxScarlettAnaconda, 0x07u)]
    [InlineData(SystemDeviceType.XboxScarlettDevkit,   0x08u)]
    public void SystemDeviceTypeMatchesHeader(SystemDeviceType deviceType, uint expected)
    {
        Assert.Equal(expected, (uint)deviceType);
        Assert.Equal(expected, (uint)(XSystemDeviceType)deviceType);
    }

    // ─── SystemHandleType ────────────────────────────────────────────────────────

    [Theory]
    [InlineData(SystemHandleType.DisplayTimeoutDeferral, 0x01u)]
    [InlineData(SystemHandleType.TaskQueue,             0x0cu)]
    [InlineData(SystemHandleType.User,                  0x0du)]
    public void SystemHandleTypeMatchesHeader(SystemHandleType handleType, uint expected)
    {
        Assert.Equal(expected, (uint)handleType);
        Assert.Equal(expected, (uint)(XSystemHandleType)handleType);
    }

    // ─── HdrModeResult / HdrModePreference ───────────────────────────────────────

    [Theory]
    [InlineData(HdrModeResult.Unknown,  0u)]
    [InlineData(HdrModeResult.Enabled,  1u)]
    [InlineData(HdrModeResult.Disabled, 2u)]
    public void HdrModeResultMatchesHeader(HdrModeResult result, uint expected)
    {
        Assert.Equal(expected, (uint)result);
        Assert.Equal(expected, (uint)(XDisplayHdrModeResult)result);
    }

    [Theory]
    [InlineData(HdrModePreference.PreferHdr,         0u)]
    [InlineData(HdrModePreference.PreferRefreshRate, 1u)]
    public void HdrModePreferenceMatchesHeader(HdrModePreference pref, uint expected)
    {
        Assert.Equal(expected, (uint)pref);
        Assert.Equal(expected, (uint)(XDisplayHdrModePreference)pref);
    }

    // ─── ErrorOptions ────────────────────────────────────────────────────────────

    [Fact]
    public void ErrorOptionsIsAFlagsEnum()
    {
        var combined = ErrorOptions.OutputDebugStringOnError | ErrorOptions.DebugBreakOnError;

        Assert.True(combined.HasFlag(ErrorOptions.OutputDebugStringOnError));
        Assert.True(combined.HasFlag(ErrorOptions.DebugBreakOnError));
        Assert.False(combined.HasFlag(ErrorOptions.FailFastOnError));
        Assert.Equal(0x03u, (uint)combined);
    }

    [Theory]
    [InlineData(ErrorOptions.None,                     0x00u)]
    [InlineData(ErrorOptions.OutputDebugStringOnError, 0x01u)]
    [InlineData(ErrorOptions.DebugBreakOnError,        0x02u)]
    [InlineData(ErrorOptions.FailFastOnError,          0x04u)]
    public void ErrorOptionsValuesMatchHeader(ErrorOptions options, uint expected)
    {
        Assert.Equal(expected, (uint)options);
        Assert.Equal(expected, (uint)(XErrorOptions)options);
    }

    // ─── SystemVersion ───────────────────────────────────────────────────────────

    [Fact]
    public void SystemVersionPackedValueMatchesComponentArithmetic()
    {
        var v = new SystemVersion(1, 2, 3, 4);
        ulong expected = 1UL | (2UL << 16) | (3UL << 32) | (4UL << 48);
        Assert.Equal(expected, v.PackedValue);
    }

    [Fact]
    public void SystemVersionEqualityUsesPackedValue()
    {
        var a = new SystemVersion(1, 2, 3, 4);
        var b = new SystemVersion(1, 2, 3, 4);
        var c = new SystemVersion(5, 6, 7, 8);

        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    // ─── DisplayTimeoutDeferral ───────────────────────────────────────────────────

    [Fact]
    public void DisplayTimeoutDeferralImplementsIDisposable()
    {
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(DisplayTimeoutDeferral)));
    }

    // ─── Error callback trampoline resolves ──────────────────────────────────────

    [Fact]
    public void ErrorCallbackFunctionPointerIsNonZero()
    {
        Assert.NotEqual(IntPtr.Zero, GameErrorHandling.ErrorCallbackFunctionPointer);
    }

    // ─── GameRuntimeFeature enum includes System / Thread / Error / Game / Display ──────

    [Theory]
    [InlineData(GameRuntimeFeature.System,  15u)]
    [InlineData(GameRuntimeFeature.Thread,  17u)]
    [InlineData(GameRuntimeFeature.Error,   19u)]
    [InlineData(GameRuntimeFeature.Display,  4u)]
    [InlineData(GameRuntimeFeature.Game,     5u)]
    [InlineData(GameRuntimeFeature.Launcher, 9u)]
    public void GameFeatureCoversNewFamilies(GameRuntimeFeature feature, uint expected)
    {
        Assert.Equal(expected, (uint)feature);
    }
}
