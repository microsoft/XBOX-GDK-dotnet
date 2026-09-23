using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.Package;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for the XPackage projection.
/// No native code is called; every test is a pure layout, enum-value, or error-model check.
/// </summary>
public sealed unsafe class PackageTests
{
    // ---- enum values match the header -------------------------------------------

    [Theory]
    [InlineData(PackageKind.Game, 0u)]
    [InlineData(PackageKind.Content, 1u)]
    public void PackageKindMatchesHeader(PackageKind kind, uint expected)
    {
        Assert.Equal(expected, (uint)kind);
        Assert.Equal(expected, (uint)(XPackageKind)kind);
    }

    [Theory]
    [InlineData(PackageEnumerationScope.ThisOnly, 0u)]
    [InlineData(PackageEnumerationScope.ThisAndRelated, 1u)]
    [InlineData(PackageEnumerationScope.ThisPublisher, 2u)]
    public void PackageEnumerationScopeMatchesHeader(PackageEnumerationScope scope, uint expected)
    {
        Assert.Equal(expected, (uint)scope);
        Assert.Equal(expected, (uint)(XPackageEnumerationScope)scope);
    }

    [Theory]
    [InlineData(PackageChunkSelectorType.Language, 0u)]
    [InlineData(PackageChunkSelectorType.Tag, 1u)]
    [InlineData(PackageChunkSelectorType.Chunk, 2u)]
    [InlineData(PackageChunkSelectorType.Feature, 3u)]
    public void PackageChunkSelectorTypeMatchesHeader(PackageChunkSelectorType type, uint expected)
    {
        Assert.Equal(expected, (uint)type);
        Assert.Equal(expected, (uint)(XPackageChunkSelectorType)type);
    }

    [Theory]
    [InlineData(PackageChunkAvailability.Ready, 0u)]
    [InlineData(PackageChunkAvailability.Pending, 1u)]
    [InlineData(PackageChunkAvailability.Installable, 2u)]
    [InlineData(PackageChunkAvailability.Unavailable, 3u)]
    public void PackageChunkAvailabilityMatchesHeader(PackageChunkAvailability avail, uint expected)
    {
        Assert.Equal(expected, (uint)avail);
        Assert.Equal(expected, (uint)(XPackageChunkAvailability)avail);
    }

    // ---- native struct layout matches x64 C compiler ----------------------------

    [Fact]
    public void XVersionIsEightBytes()
    {
        // XVersion is a union of 4×uint16 and uint64; total = 8 bytes.
        Assert.Equal(8, Marshal.SizeOf<XVersion>());
    }

    [Fact]
    public void XVersionFieldsAreInCorrectOrder()
    {
        // On little-endian x64, Major is at offset 0.
        Assert.Equal(0, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Major)));
        Assert.Equal(2, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Minor)));
        Assert.Equal(4, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Build)));
        Assert.Equal(6, (int)Marshal.OffsetOf<XVersion>(nameof(XVersion.Revision)));
    }

    [Fact]
    public void XPackageChunkSelectorSizeMatchesNativeLayout()
    {
        // struct XPackageChunkSelector { XPackageChunkSelectorType type (4); [4 pad]; union ptr/uint32 (8) }
        // = 16 bytes on x64.
        Assert.Equal(16, Marshal.SizeOf<XPackageChunkSelector>());
        Assert.Equal(0, (int)Marshal.OffsetOf<XPackageChunkSelector>(nameof(XPackageChunkSelector.Type)));
        // Value is at offset 8 (pointer-aligned).
        Assert.Equal(8, (int)Marshal.OffsetOf<XPackageChunkSelector>(nameof(XPackageChunkSelector.Value)));
    }

    [Fact]
    public void XPackageInstallationProgressSizeMatchesNativeLayout()
    {
        // 3 × uint64 + 2 × bool(1 byte) + 6 pad = 32 bytes.
        Assert.Equal(32, Marshal.SizeOf<XPackageInstallationProgress>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XPackageInstallationProgress>(nameof(XPackageInstallationProgress.totalBytes)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XPackageInstallationProgress>(nameof(XPackageInstallationProgress.installedBytes)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XPackageInstallationProgress>(nameof(XPackageInstallationProgress.launchBytes)));
        Assert.Equal(24, (int)Marshal.OffsetOf<XPackageInstallationProgress>(nameof(XPackageInstallationProgress.launchable)));
        Assert.Equal(25, (int)Marshal.OffsetOf<XPackageInstallationProgress>(nameof(XPackageInstallationProgress.completed)));
    }

    [Fact]
    public void XPackageWriteStatsSizeMatchesNativeLayout()
    {
        // 4 × uint64 = 32 bytes.
        Assert.Equal(32, Marshal.SizeOf<XPackageWriteStats>());
    }

    [Fact]
    public void XPackageDetailsSizeMatchesNativeLayout()
    {
        // char*(8) + XVersion(8) + kind(4) + [4 pad] + 4×char*(32) + bool(1) + [3 pad]
        // + 2×uint(8) + bool(1) + [7 pad] + char*(8) = 88 bytes on x64.
        Assert.Equal(88, Marshal.SizeOf<XPackageDetails>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XPackageDetails>(nameof(XPackageDetails.packageIdentifier)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XPackageDetails>(nameof(XPackageDetails.version)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XPackageDetails>(nameof(XPackageDetails.kind)));
        Assert.Equal(24, (int)Marshal.OffsetOf<XPackageDetails>(nameof(XPackageDetails.displayName)));
        Assert.Equal(56, (int)Marshal.OffsetOf<XPackageDetails>(nameof(XPackageDetails.installing)));
        Assert.Equal(60, (int)Marshal.OffsetOf<XPackageDetails>(nameof(XPackageDetails.index)));
        Assert.Equal(64, (int)Marshal.OffsetOf<XPackageDetails>(nameof(XPackageDetails.count)));
        Assert.Equal(68, (int)Marshal.OffsetOf<XPackageDetails>(nameof(XPackageDetails.ageRestricted)));
        Assert.Equal(76, (int)Marshal.OffsetOf<XPackageDetails>(nameof(XPackageDetails.titleId)));
    }

    [Fact]
    public void XPackageFeatureSizeMatchesNativeLayout()
    {
        // 3×char*(24) + bool(1) + [3 pad] + uint(4) + char**(8) = 40 bytes on x64.
        Assert.Equal(40, Marshal.SizeOf<XPackageFeature>());
        Assert.Equal(0,  (int)Marshal.OffsetOf<XPackageFeature>(nameof(XPackageFeature.id)));
        Assert.Equal(8,  (int)Marshal.OffsetOf<XPackageFeature>(nameof(XPackageFeature.displayName)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XPackageFeature>(nameof(XPackageFeature.tags)));
        Assert.Equal(24, (int)Marshal.OffsetOf<XPackageFeature>(nameof(XPackageFeature.hidden)));
        Assert.Equal(28, (int)Marshal.OffsetOf<XPackageFeature>(nameof(XPackageFeature.storeIdCount)));
        Assert.Equal(32, (int)Marshal.OffsetOf<XPackageFeature>(nameof(XPackageFeature.storeIds)));
    }

    // ---- HRESULT values match XGameErr.h ----------------------------------------

    [Theory]
    [InlineData(HResult.EGamePackageAppNotPackaged,       unchecked((int)0x89245200))]
    [InlineData(HResult.EGamePackageNoInstalledLanguages, unchecked((int)0x89245201))]
    [InlineData(HResult.EGamePackageNoStoreId,            unchecked((int)0x89245202))]
    [InlineData(HResult.EGamePackageInvalidSelector,      unchecked((int)0x89245203))]
    [InlineData(HResult.EGamePackageDownloadRequired,     unchecked((int)0x89245204))]
    [InlineData(HResult.EGamePackageNoTagChange,          unchecked((int)0x89245205))]
    [InlineData(HResult.EGamePackageDlcNotSupported,      unchecked((int)0x89245206))]
    [InlineData(HResult.EGamePackageDuplicateIdValues,    unchecked((int)0x89245207))]
    [InlineData(HResult.EGamePackageNoPackageIdentifier,  unchecked((int)0x89245208))]
    public void PackageHResultsMatchXGameErr(int constant, int expected)
    {
        Assert.Equal(expected, constant);
    }

    [Fact]
    public void PackageHResultsHaveDescriptiveMessages()
    {
        var ex = new GameRuntimeException(HResult.EGamePackageAppNotPackaged);
        Assert.Contains("packaged", ex.Message, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("0x", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PackageDownloadRequiredHasDescriptiveMessage()
    {
        var ex = new GameRuntimeException(HResult.EGamePackageDownloadRequired);
        Assert.Contains("download", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    // ---- PackageChunkSelector factory methods -----------------------------------

    [Fact]
    public void ChunkSelectorByLanguageHasCorrectTypeAndValue()
    {
        var sel = PackageChunkSelector.ByLanguage("en-US");
        Assert.Equal(PackageChunkSelectorType.Language, sel.Type);
        Assert.Equal("en-US", sel.StringValue);
        Assert.Equal(0u, sel.ChunkId);
    }

    [Fact]
    public void ChunkSelectorByChunkIdHasCorrectTypeAndValue()
    {
        var sel = PackageChunkSelector.ByChunkId(42u);
        Assert.Equal(PackageChunkSelectorType.Chunk, sel.Type);
        Assert.Null(sel.StringValue);
        Assert.Equal(42u, sel.ChunkId);
    }

    [Fact]
    public void ChunkSelectorByTagHasCorrectTypeAndValue()
    {
        var sel = PackageChunkSelector.ByTag("4K");
        Assert.Equal(PackageChunkSelectorType.Tag, sel.Type);
        Assert.Equal("4K", sel.StringValue);
    }

    [Fact]
    public void ChunkSelectorByFeatureHasCorrectTypeAndValue()
    {
        var sel = PackageChunkSelector.ByFeature("HighResTextures");
        Assert.Equal(PackageChunkSelectorType.Feature, sel.Type);
        Assert.Equal("HighResTextures", sel.StringValue);
    }

    [Fact]
    public void ChunkSelectorNullStringThrows()
    {
        Assert.Throws<ArgumentNullException>(() => PackageChunkSelector.ByLanguage(null!));
        Assert.Throws<ArgumentNullException>(() => PackageChunkSelector.ByTag(null!));
        Assert.Throws<ArgumentNullException>(() => PackageChunkSelector.ByFeature(null!));
    }

    [Fact]
    public void ChunkSelectorEqualityWorks()
    {
        var a = PackageChunkSelector.ByChunkId(1u);
        var b = PackageChunkSelector.ByChunkId(1u);
        var c = PackageChunkSelector.ByChunkId(2u);
        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    // ---- PackageVersion ---------------------------------------------------------

    [Fact]
    public void PackageVersionRoundTripsComponents()
    {
        var v = new PackageVersion(1, 2, 3, 4);
        Assert.Equal(1, v.Major);
        Assert.Equal(2, v.Minor);
        Assert.Equal(3, v.Build);
        Assert.Equal(4, v.Revision);
        Assert.Equal("1.2.3.4", v.ToString());
    }

    [Fact]
    public void PackageVersionEquality()
    {
        var a = new PackageVersion(1, 0, 0, 0);
        var b = new PackageVersion(1, 0, 0, 0);
        var c = new PackageVersion(2, 0, 0, 0);
        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void PackageVersionFromNativeXVersion()
    {
        XVersion native = default;
        native.Major = 26;
        native.Minor = 4;
        native.Build = 1000;
        native.Revision = 0;
        var v = new PackageVersion(native);
        Assert.Equal(26, v.Major);
        Assert.Equal(4, v.Minor);
        Assert.Equal(1000, v.Build);
        Assert.Equal(0, v.Revision);
    }

    // ---- PackageInstallationProgress from native --------------------------------

    [Fact]
    public void PackageInstallationProgressMapsNativeFields()
    {
        XPackageInstallationProgress native = default;
        native.totalBytes = 1000;
        native.installedBytes = 500;
        native.launchBytes = 100;
        native.launchable = 1;
        native.completed = 0;

        var progress = new PackageInstallationProgress(native);
        Assert.Equal(1000UL, progress.TotalBytes);
        Assert.Equal(500UL, progress.InstalledBytes);
        Assert.Equal(100UL, progress.LaunchBytes);
        Assert.True(progress.Launchable);
        Assert.False(progress.Completed);
        Assert.Equal(0.5, progress.Fraction, 5);
    }

    [Fact]
    public void PackageInstallationProgressFractionIsZeroWhenTotalIsZero()
    {
        XPackageInstallationProgress native = default;
        var progress = new PackageInstallationProgress(native);
        Assert.Equal(0.0, progress.Fraction);
    }

    // ---- PackageWriteStats from native ------------------------------------------

    [Fact]
    public void PackageWriteStatsMapsNativeFields()
    {
        XPackageWriteStats native = default;
        native.interval = 1000;
        native.budget = 1024 * 1024;
        native.elapsed = 500;
        native.bytesWritten = 256 * 1024;

        var stats = new PackageWriteStats(native);
        Assert.Equal(1000UL, stats.Interval);
        Assert.Equal(1024UL * 1024, stats.Budget);
        Assert.Equal(500UL, stats.Elapsed);
        Assert.Equal(256UL * 1024, stats.BytesWritten);
    }

    // ---- null argument guards ---------------------------------------------------

    [Fact]
    public void GamePackageNullArgumentsThrow()
    {
        Assert.Throws<ArgumentNullException>(() => GamePackage.EnumerateFeatures(null!));
        Assert.Throws<ArgumentNullException>(() => GamePackage.EnumerateChunkAvailability(null!, PackageChunkSelectorType.Chunk));
        Assert.Throws<ArgumentNullException>(() => GamePackage.FindChunkAvailability(null!, Array.Empty<PackageChunkSelector>()));
        Assert.Throws<ArgumentNullException>(() => GamePackage.FindChunkAvailability("id", null!));
        Assert.Throws<ArgumentNullException>(() => GamePackage.UninstallPackage(null!));
        Assert.Throws<ArgumentNullException>(() => GamePackage.UninstallUwpInstance(null!));
        Assert.Throws<ArgumentNullException>(() => GamePackage.EstimateDownloadSize(null!, Array.Empty<PackageChunkSelector>(), out _));
        Assert.Throws<ArgumentNullException>(() => GamePackage.EstimateDownloadSize("id", null!, out _));
        Assert.Throws<ArgumentNullException>(() => GamePackage.UninstallChunks(null!, Array.Empty<PackageChunkSelector>()));
        Assert.Throws<ArgumentNullException>(() => GamePackage.UninstallChunks("id", null!));
        Assert.Throws<ArgumentNullException>(() => GamePackage.InstallChunks(null!, Array.Empty<PackageChunkSelector>()));
        Assert.Throws<ArgumentNullException>(() => GamePackage.InstallChunks("id", null!));
    }

    [Fact]
    public void PackageMountNullArgumentThrows()
    {
        // ArgumentNullException is thrown synchronously before any async work starts.
        Assert.Throws<ArgumentNullException>(() => { _ = PackageMount.MountWithUiAsync(null!); });
    }

    [Fact]
    public void PackageInstallationMonitorNullArgumentThrows()
    {
        Assert.Throws<ArgumentNullException>(() => PackageInstallationMonitor.Create(null!));
    }


    // ---- disposed object guards -------------------------------------------------

    [Fact]
    public void PackageInstallationMonitorThrowsAfterDispose()
    {
        // Use the internal constructor (visible via InternalsVisibleTo) with a zero handle.
        // SafeHandleZeroOrMinusOneIsInvalid treats zero as invalid, so ReleaseHandle is a no-op.
        var monitor = new PackageInstallationMonitor(IntPtr.Zero);
        monitor.Dispose();
        Assert.Throws<ObjectDisposedException>(() => monitor.GetProgress());
    }

    // ---- PackageChunkAvailabilityInfo -------------------------------------------

    [Fact]
    public void ChunkAvailabilityInfoHoldsData()
    {
        var sel = PackageChunkSelector.ByChunkId(7u);
        var info = new PackageChunkAvailabilityInfo(sel, PackageChunkAvailability.Pending);
        Assert.Equal(sel, info.Selector);
        Assert.Equal(PackageChunkAvailability.Pending, info.Availability);
    }
}

/// <summary>
/// Dummy placeholder so the test file continues to compile if the test accessor class is removed.
/// </summary>
internal static class PackageTestHelpers { }
