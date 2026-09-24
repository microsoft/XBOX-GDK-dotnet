// Contract tests for the XStore family.
//
// Nothing here loads xgameruntime.thunks.dll; all tests run on hosted CI with no Gaming Runtime.
// Checks: enum values against header constants, struct sizes/field offsets, HRESULT codes, and
// idiomatic API shape (flags, sealed classes, etc.).

using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.Store;
using Xunit;

namespace GDK.Net.Tests;

public sealed unsafe class StoreTests
{
    // -----------------------------------------------------------------------
    // Enum value contracts: values are taken verbatim from XStore.h (GDK 260404).
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(StoreProductKind.None, 0x00u)]
    [InlineData(StoreProductKind.Consumable, 0x01u)]
    [InlineData(StoreProductKind.Durable, 0x02u)]
    [InlineData(StoreProductKind.Game, 0x04u)]
    [InlineData(StoreProductKind.Pass, 0x08u)]
    [InlineData(StoreProductKind.UnmanagedConsumable, 0x10u)]
    public void StoreProductKindMatchesHeader(StoreProductKind kind, uint expected)
    {
        Assert.Equal(expected, (uint)kind);
        Assert.Equal(expected, (uint)(XStoreProductKind)kind);
    }

    [Fact]
    public void StoreProductKindIsAFlagsEnum()
    {
        var combo = StoreProductKind.Consumable | StoreProductKind.Durable;

        Assert.True(combo.HasFlag(StoreProductKind.Consumable));
        Assert.True(combo.HasFlag(StoreProductKind.Durable));
        Assert.False(combo.HasFlag(StoreProductKind.Game));
        Assert.Equal(0x03u, (uint)combo);
    }

    [Theory]
    [InlineData(StoreCanLicenseStatus.NotLicensableToUser, 0u)]
    [InlineData(StoreCanLicenseStatus.Licensable, 1u)]
    [InlineData(StoreCanLicenseStatus.LicenseActionNotApplicableToProduct, 2u)]
    public void StoreCanLicenseStatusMatchesHeader(StoreCanLicenseStatus status, uint expected)
    {
        Assert.Equal(expected, (uint)status);
        Assert.Equal(expected, (uint)(XStoreCanLicenseStatus)status);
    }

    [Theory]
    [InlineData(StoreDurationUnit.Minute, 0u)]
    [InlineData(StoreDurationUnit.Hour, 1u)]
    [InlineData(StoreDurationUnit.Day, 2u)]
    [InlineData(StoreDurationUnit.Week, 3u)]
    [InlineData(StoreDurationUnit.Month, 4u)]
    [InlineData(StoreDurationUnit.Year, 5u)]
    public void StoreDurationUnitMatchesHeader(StoreDurationUnit unit, uint expected)
    {
        Assert.Equal(expected, (uint)unit);
        Assert.Equal(expected, (uint)(XStoreDurationUnit)unit);
    }

    // -----------------------------------------------------------------------
    // HRESULT code contracts: values from XGameErr.h (GDK 260404).
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(HResult.EGameStoreLicenseActionNotApplicableToProduct, unchecked((int)0x89245300u))]
    [InlineData(HResult.EGameStoreNetworkError, unchecked((int)0x89245301u))]
    [InlineData(HResult.EGameStoreServerError, unchecked((int)0x89245302u))]
    [InlineData(HResult.EGameStoreInsufficientQuantity, unchecked((int)0x89245303u))]
    [InlineData(HResult.EGameStoreAlreadyPurchased, unchecked((int)0x89245304u))]
    [InlineData(HResult.EGameStoreLicenseActionThrottled, unchecked((int)0x89245305u))]
    public void StoreHResultCodesMatchHeader(int code, int expected)
    {
        Assert.Equal(expected, code);
        Assert.True(HResult.Failed(code));
    }

    [Theory]
    [InlineData(HResult.EGameStoreLicenseActionNotApplicableToProduct)]
    [InlineData(HResult.EGameStoreNetworkError)]
    [InlineData(HResult.EGameStoreServerError)]
    [InlineData(HResult.EGameStoreInsufficientQuantity)]
    [InlineData(HResult.EGameStoreAlreadyPurchased)]
    [InlineData(HResult.EGameStoreLicenseActionThrottled)]
    public void StoreHResultCodesHaveDescriptiveMessages(int code)
    {
        var ex = new GameRuntimeException(code);

        Assert.NotNull(ex.Message);
        Assert.DoesNotContain("0x", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData(HResult.EGameStoreLicenseActionNotApplicableToProduct)]
    [InlineData(HResult.EGameStoreNetworkError)]
    public void StoreFailuresMapToGameRuntimeException(int hresult)
    {
        var ex = Assert.IsType<GameRuntimeException>(Hr.ToException(hresult));

        Assert.Equal(hresult, ex.HResultCode);
        Assert.False(ex is UserException);
    }

    // -----------------------------------------------------------------------
    // Struct layout contracts (x64; values from XStore.h).
    // -----------------------------------------------------------------------

    [Fact]
    public void XStorePriceSizeMatchesHeader()
    {
        // float(4) + float(4) + float(4) + pad(4) + ptr(8) + char[16](16)
        //   + char[16](16) + char[16](16) + bool(1) + pad(7) + time_t(8) = 88
        Assert.Equal(88, Marshal.SizeOf<XStorePrice>());
    }

    [Fact]
    public void XStoreImageSizeMatchesHeader()
    {
        // 3 ptr(8) + 2 uint(4)*2 with padding + 2 ptr(8) = ptr ptr uint uint ptr ptr = 8+8+4+4+8+8 = 40
        // Actually: uri(8) + height(4) + width(4) + caption(8) + imagePurposeTag(8) = 32
        Assert.Equal(32, Marshal.SizeOf<XStoreImage>());
    }

    [Fact]
    public void XStoreVideoSizeMatchesHeader()
    {
        // Same prefix as image (32 - previewImage) = 32 + 32 = 64
        Assert.Equal(64, Marshal.SizeOf<XStoreVideo>());
    }

    [Fact]
    public void XStoreConsumableResultSizeMatchesHeader()
    {
        Assert.Equal(4, Marshal.SizeOf<XStoreConsumableResult>());
    }

    [Fact]
    public void XStoreRateAndReviewResultSizeMatchesHeader()
    {
        Assert.Equal(1, Marshal.SizeOf<XStoreRateAndReviewResult>());
    }

    [Fact]
    public void XStorePackageUpdateSizeMatchesHeader()
    {
        // char[33](33) + bool(1) = 34
        Assert.Equal(34, Marshal.SizeOf<XStorePackageUpdate>());
    }

    [Fact]
    public void XStoreSubscriptionInfoSizeMatchesHeader()
    {
        // bool(1) + pad(3) + uint(4) + uint(4) + uint(4) + uint(4) = 20
        Assert.Equal(20, Marshal.SizeOf<XStoreSubscriptionInfo>());
    }

    [Fact]
    public void XStoreCanAcquireLicenseResultSizeMatchesHeader()
    {
        // char[5](5) + pad(3) + uint(4) = 12
        Assert.Equal(12, Marshal.SizeOf<XStoreCanAcquireLicenseResult>());
    }

    [Fact]
    public void XStoreCollectionDataSizeMatchesHeader()
    {
        // long(8)*3 + bool(1) + pad(3) + uint(4)*2 + pad(4) + ptr(8)*2 = 56
        Assert.Equal(56, Marshal.SizeOf<XStoreCollectionData>());
    }

    // -----------------------------------------------------------------------
    // Callback pointer shape.
    // -----------------------------------------------------------------------

    [Fact]
    public void StoreCallbackPointersAreNonZeroAndDistinct()
    {
        Assert.NotEqual(IntPtr.Zero, StoreLicenseCallbacks.PackageLicenseLostCallback);
        Assert.NotEqual(IntPtr.Zero, StoreProductQueryCallbacks.ProductQueryCallback);
        Assert.NotEqual(IntPtr.Zero, StoreContextCallbacks.GameLicenseChangedCallback);

        // All three must be distinct function pointers.
        Assert.NotEqual(StoreLicenseCallbacks.PackageLicenseLostCallback,
                        StoreProductQueryCallbacks.ProductQueryCallback);
        Assert.NotEqual(StoreContextCallbacks.GameLicenseChangedCallback,
                        StoreProductQueryCallbacks.ProductQueryCallback);
    }

    // -----------------------------------------------------------------------
    // Idiomatic type shape.
    // -----------------------------------------------------------------------

    [Fact]
    public void StoreProductIsSealed()
    {
        Assert.True(typeof(StoreProduct).IsSealed);
    }

    [Fact]
    public void StoreContextIsSealed()
    {
        Assert.True(typeof(StoreContext).IsSealed);
    }

    [Fact]
    public void StoreLicenseIsSealed()
    {
        Assert.True(typeof(StoreLicense).IsSealed);
    }

    [Fact]
    public void StoreProductQueryIsSealed()
    {
        Assert.True(typeof(StoreProductQuery).IsSealed);
    }

    [Fact]
    public void StoreConsumableResultIsValueType()
    {
        Assert.True(typeof(StoreConsumableResult).IsValueType);
    }

    [Fact]
    public void StoreRateAndReviewResultIsValueType()
    {
        Assert.True(typeof(StoreRateAndReviewResult).IsValueType);
    }

    // -----------------------------------------------------------------------
    // StoreContext.Create() throws without the Gaming Runtime.
    // -----------------------------------------------------------------------

    [Fact]
    public void StoreContextCreateThrowsWithoutRuntime()
    {
        // Without the Gaming Runtime DLLs the call raises DllNotFoundException or
        // GameRuntimeException (depending on which failure mode the host triggers).
        // Either way an exception must propagate; it must never silently succeed.
        var ex = Assert.ThrowsAny<Exception>(() => StoreContext.Create());
        Assert.True(
            ex is DllNotFoundException || ex is EntryPointNotFoundException || ex is GameRuntimeException,
            $"Unexpected exception type: {ex.GetType().FullName}");
    }

    [Fact]
    public void StoreContextCreateForUserThrowsOnNullUser()
    {
        Assert.Throws<ArgumentNullException>(() => StoreContext.CreateForUser(null!));
    }

    // -----------------------------------------------------------------------
    // StoreProductFactory deep-copy helpers (pure managed, no native).
    // -----------------------------------------------------------------------

    [Fact]
    public void StoreProductFactoryPtrToStringHandlesNull()
    {
        Assert.Equal(string.Empty, StoreProductFactory.PtrToString(null));
    }

    [Fact]
    public void StoreProductFactoryPtrToStringReturnsEmptyForEmptyPtr()
    {
        byte zero = 0;
        Assert.Equal(string.Empty, StoreProductFactory.PtrToString(&zero));
    }

    [Fact]
    public void StoreProductFactoryPtrToStringDecodesUtf8()
    {
        byte[] bytes = { (byte)'H', (byte)'i', 0 };
        fixed (byte* ptr = bytes)
        {
            Assert.Equal("Hi", StoreProductFactory.PtrToString(ptr));
        }
    }

    [Fact]
    public void StoreProductFactoryFixedBufferToStringDecodesUtf8()
    {
        byte[] bytes = { (byte)'A', (byte)'B', 0, 0, 0 };
        fixed (byte* ptr = bytes)
        {
            Assert.Equal("AB", StoreProductFactory.FixedBufferToString(ptr, 5));
        }
    }

    [Fact]
    public void StoreProductFactoryFixedBufferToStringRespectsMaxLen()
    {
        byte[] bytes = { (byte)'X', (byte)'Y', (byte)'Z' }; // no null terminator within maxLen=2
        fixed (byte* ptr = bytes)
        {
            Assert.Equal("XY", StoreProductFactory.FixedBufferToString(ptr, 2));
        }
    }

    // -----------------------------------------------------------------------
    // UTF-8 helpers (internal, no native).
    // -----------------------------------------------------------------------

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ToUtf8NullTerminatedReturnsNullTerminatorForNullOrEmpty(string? input)
    {
        byte[] result = StoreContext.ToUtf8NullTerminated(input);

        Assert.Single(result);
        Assert.Equal(0, result[0]);
    }

    [Fact]
    public void ToUtf8NullTerminatedEncodesAscii()
    {
        byte[] result = StoreContext.ToUtf8NullTerminated("Hi");

        Assert.Equal(3, result.Length);
        Assert.Equal((byte)'H', result[0]);
        Assert.Equal((byte)'i', result[1]);
        Assert.Equal(0, result[2]);
    }

    // -----------------------------------------------------------------------
    // StoreConsumableResult / StoreRateAndReviewResult value construction.
    // -----------------------------------------------------------------------

    [Fact]
    public void StoreConsumableResultReportsQuantity()
    {
        var r = new StoreConsumableResult(42u);

        Assert.Equal(42u, r.Quantity);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void StoreRateAndReviewResultReportsWasUpdated(bool wasUpdated)
    {
        var r = new StoreRateAndReviewResult(wasUpdated);

        Assert.Equal(wasUpdated, r.WasUpdated);
    }
}
