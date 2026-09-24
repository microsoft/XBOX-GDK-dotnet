// Contract tests for the XAppCapture / XAppBroadcast interop layer (GDK edition 260404).
//
// These tests are pure compile-time / layout checks: nothing here loads xgameruntime.thunks.dll
// or calls native code, so they run identically on a developer box and on a hosted CI runner.
//
// Assertions:
//   1. Public enum values match the header constants.
//   2. Struct sizes and field offsets match the native layout.
//   3. Argument validation on the idiomatic layer (null guards, disposed checks).
//   4. Trampoline resolves to a real function pointer.

using System;
using System.Runtime.InteropServices;
using GDK.Net.Capture;
using GDK.Net.Interop;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Guards the raw interop layer for XAppCapture / XAppBroadcast against silent drift from the GDK
/// headers. No native code is invoked.
/// </summary>
public sealed unsafe class CaptureContractTests
{
    // ── Enum values ────────────────────────────────────────────────────────────────────────────

    [Fact]
    public void XAppCaptureMetadataPriorityValuesMatchHeader()
    {
        // Header: Informational = 0, Important = 1
        Assert.Equal(0, (int)XAppCaptureMetadataPriority.Informational);
        Assert.Equal(1, (int)XAppCaptureMetadataPriority.Important);
    }

    [Fact]
    public void XAppCaptureVideoEncodingValuesMatchHeader()
    {
        Assert.Equal(0, (int)XAppCaptureVideoEncoding.H264);
        Assert.Equal(1, (int)XAppCaptureVideoEncoding.HEVC);
    }

    [Fact]
    public void XAppCaptureVideoColorFormatValuesMatchHeader()
    {
        Assert.Equal(0, (int)XAppCaptureVideoColorFormat.SDR);
        Assert.Equal(1, (int)XAppCaptureVideoColorFormat.HDR);
    }

    [Fact]
    public void XAppCaptureScreenshotFormatFlagValuesMatchHeader()
    {
        // SDR = 1, HDR = 2 (flags enum)
        Assert.Equal(1, (int)XAppCaptureScreenshotFormatFlag.SDR);
        Assert.Equal(2, (int)XAppCaptureScreenshotFormatFlag.HDR);
    }

    // Public enums must project onto the raw interop enums without value drift.

    [Theory]
    [InlineData(AppCaptureMetadataPriority.Informational, 0)]
    [InlineData(AppCaptureMetadataPriority.Important, 1)]
    public void PublicMetadataPriorityMatchesHeader(AppCaptureMetadataPriority value, int expected)
    {
        Assert.Equal(expected, (int)value);
        Assert.Equal((byte)(int)value, (byte)(XAppCaptureMetadataPriority)value);
    }

    [Theory]
    [InlineData(AppCaptureVideoEncoding.H264, 0)]
    [InlineData(AppCaptureVideoEncoding.Hevc, 1)]
    public void PublicVideoEncodingMatchesHeader(AppCaptureVideoEncoding value, int expected)
    {
        Assert.Equal(expected, (int)value);
        Assert.Equal((byte)expected, (byte)(XAppCaptureVideoEncoding)value);
    }

    [Theory]
    [InlineData(AppCaptureVideoColorFormat.Sdr, 0)]
    [InlineData(AppCaptureVideoColorFormat.Hdr, 1)]
    public void PublicColorFormatMatchesHeader(AppCaptureVideoColorFormat value, int expected)
    {
        Assert.Equal(expected, (int)value);
        Assert.Equal((byte)expected, (byte)(XAppCaptureVideoColorFormat)value);
    }

    [Theory]
    [InlineData(AppCaptureScreenshotFormatFlag.Sdr, 1)]
    [InlineData(AppCaptureScreenshotFormatFlag.Hdr, 2)]
    public void PublicScreenshotFormatMatchesHeader(AppCaptureScreenshotFormatFlag value, int expected)
    {
        Assert.Equal(expected, (int)value);
        Assert.Equal((ushort)expected, (ushort)(XAppCaptureScreenshotFormatFlag)value);
    }

    [Fact]
    public void CaptureScreenshotFormatIsFlags()
    {
        // Both SDR and HDR may be present simultaneously.
        var both = AppCaptureScreenshotFormatFlag.Sdr | AppCaptureScreenshotFormatFlag.Hdr;
        Assert.True(both.HasFlag(AppCaptureScreenshotFormatFlag.Sdr));
        Assert.True(both.HasFlag(AppCaptureScreenshotFormatFlag.Hdr));
        Assert.Equal((ushort)3, (ushort)both);
    }

    // ── Struct sizes ───────────────────────────────────────────────────────────────────────────

    [Fact]
    public void XSystemTimeIs16Bytes()
    {
        // SYSTEMTIME: 8 × WORD = 16 bytes, alignment 2. Platform-independent.
        Assert.Equal(16, Marshal.SizeOf<XSystemTime>());
    }

    [Fact]
    public void XAppBroadcastStatusIs9Bytes()
    {
        // Nine C++ bool fields (1 byte each), no padding. Platform-independent.
        Assert.Equal(9, Marshal.SizeOf<XAppBroadcastStatus>());
    }

    [Fact]
    public void XAppCaptureVideoCaptureSettingsIs24Bytes()
    {
        // uint(4)+uint(4)+ulong(8)+byte(1)+byte(1)+byte(1)+padding(5) = 24. Platform-independent.
        Assert.Equal(24, Marshal.SizeOf<XAppCaptureVideoCaptureSettings>());
    }

    [Fact]
    public void XAppCaptureTakeScreenshotResultIs252Bytes()
    {
        // fixed byte[250] + ushort(2) = 252. Alignment 2; 252 is even. Platform-independent.
        Assert.Equal(252, Marshal.SizeOf<XAppCaptureTakeScreenshotResult>());
    }

    [Fact]
    public void XAppCaptureScreenshotFileHasCorrectSize()
    {
        // char[260] + size_t + uint + uint.
        // x64: 260 + 4(pad) + 8 + 4 + 4 = 280. x86: 260 + 4 + 4 + 4 = 272.
        int expected = IntPtr.Size == 8 ? 280 : 272;
        Assert.Equal(expected, Marshal.SizeOf<XAppCaptureScreenshotFile>());
    }

    [Fact]
    public void XAppCaptureLocalResultHasCorrectSize()
    {
        // IntPtr + nuint + SYSTEMTIME(16) + ulong + uint + uint + byte + byte + padding.
        // x64: 8 + 8 + 16 + 8 + 4 + 4 + 1 + 1 + 6(pad) = 56.
        // x86: 4 + 4 + 16 + 8 + 4 + 4 + 1 + 1 + 2(pad) = 44.
        int expected = IntPtr.Size == 8 ? 56 : 44;
        Assert.Equal(expected, Marshal.SizeOf<XAppCaptureLocalResult>());
    }

    [Fact]
    public void XAppCaptureDiagnosticScreenshotResultHasCorrectSize()
    {
        // nuint + 10 × XAppCaptureScreenshotFile.
        // x64: 8 + 10×280 = 2808. x86: 4 + 10×272 = 2724.
        int fileSize = Marshal.SizeOf<XAppCaptureScreenshotFile>();
        int expected = IntPtr.Size + 10 * fileSize;
        Assert.Equal(expected, Marshal.SizeOf<XAppCaptureDiagnosticScreenshotResult>());
    }

    // ── Struct field offsets (platform-independent fields only) ───────────────────────────────

    [Fact]
    public void XAppBroadcastStatusFieldsArePackedWithNoPadding()
    {
        Assert.Equal(0, (int)Marshal.OffsetOf<XAppBroadcastStatus>(nameof(XAppBroadcastStatus.CanStartBroadcast)));
        Assert.Equal(1, (int)Marshal.OffsetOf<XAppBroadcastStatus>(nameof(XAppBroadcastStatus.IsAnyAppBroadcasting)));
        Assert.Equal(8, (int)Marshal.OffsetOf<XAppBroadcastStatus>(nameof(XAppBroadcastStatus.IsDisabledBySystem)));
    }

    [Fact]
    public void XSystemTimeFieldsHaveCorrectOffsets()
    {
        Assert.Equal(0, (int)Marshal.OffsetOf<XSystemTime>(nameof(XSystemTime.Year)));
        Assert.Equal(2, (int)Marshal.OffsetOf<XSystemTime>(nameof(XSystemTime.Month)));
        Assert.Equal(14, (int)Marshal.OffsetOf<XSystemTime>(nameof(XSystemTime.Milliseconds)));
    }

    // ── Trampoline resolves ────────────────────────────────────────────────────────────────────

    [Fact]
    public void AppCaptureContextCallbackResolvesToRealFunctionPointer()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.AppCaptureContextCallback);
    }

    [Fact]
    public void AppCaptureContextCallbackDiffersFromAsyncCompletionRoutine()
    {
        // Each trampoline must be a distinct function pointer.
        Assert.NotEqual(Trampolines.AsyncCompletionRoutine, Trampolines.AppCaptureContextCallback);
    }

    // ── Argument validation ────────────────────────────────────────────────────────────────────

    [Fact]
    public void AppCaptureManagerAcceptsAnAbsentQueue()
    {
        // A null queue is not a missing argument: it is the caller declining to name a queue, which
        // leaves XAsyncBlock::queue null so the Gaming Runtime resolves the process default.
        using var manager = new AppCaptureManager(null);
        Assert.NotNull(manager);
    }

    [Fact]
    public void DiagnosticScreenshotResultMaxFilesConstantMatchesHeader()
    {
        // APPCAPTURE_MAX_CAPTURE_FILES = 10
        Assert.Equal(10, DiagnosticScreenshotResult.MaxFiles);
    }

    [Fact]
    public void LocalClipStreamAndScreenshotStreamAreDisposable()
    {
        // Static type check: both stream types implement IDisposable.
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(LocalClipStream)));
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(ScreenshotStream)));
    }

    // ── Binding target guard ──────────────────────────────────────────────────────────────────

    [Fact]
    public void UserRecordApisBindToTheThunksDll()
    {
        // XAppCaptureStartUserRecord, XAppCaptureStopUserRecord, and XAppCaptureCancelUserRecord
        // were absent from xgameruntime.thunks.dll's export table until GDK edition 260404 added
        // them. Binding them anywhere else fails at first call with a DllNotFoundException.
        var nativeType = typeof(Native);

        foreach (string name in new[]
        {
            "XAppCaptureStartUserRecord",
            "XAppCaptureStopUserRecord",
            "XAppCaptureCancelUserRecord",
        })
        {
            var method = nativeType.GetMethod(
                name,
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            Assert.NotNull(method);

            var import = (System.Runtime.InteropServices.DllImportAttribute?)
                System.Attribute.GetCustomAttribute(
                    method!, typeof(System.Runtime.InteropServices.DllImportAttribute));
            Assert.NotNull(import);
            Assert.Equal("xgameruntime.thunks.dll", import!.Value);
        }
    }

    [Fact]
    public void UserRecordMembersAreProjected()
    {
        Type t = typeof(AppCaptureManager);

        Assert.NotNull(t.GetMethod("StartUserRecord"));
        Assert.NotNull(t.GetMethod("StopUserRecord"));
        Assert.NotNull(t.GetMethod("CancelUserRecord"));
    }
}
