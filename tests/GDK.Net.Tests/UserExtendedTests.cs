// Contract-level tests for the extended XUser API projection.
//
// All assertions are pure compile-time / layout checks: nothing here loads
// xgameruntime.thunks.dll, so the suite runs on developer machines and CI runners that have no
// Gaming Runtime installed.

using System;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.Users;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Guards the extended XUser interop layer against silent drift from XUser.h
/// (GDK edition 260404).
/// </summary>
public sealed unsafe class UserExtendedTests
{
    // ─── Native struct layout ────────────────────────────────────────────────────────────────

    [Fact]
    public void AppLocalDeviceIdNativeIs32Bytes()
    {
        // APP_LOCAL_DEVICE_ID from windef.h: struct { BYTE value[32]; }
        Assert.Equal(32, sizeof(XAppLocalDeviceId));
    }

    [Fact]
    public void XUserDeviceAssociationChangeLayoutIsCorrect()
    {
        // struct XUserDeviceAssociationChange {
        //     APP_LOCAL_DEVICE_ID deviceId;   // 32 bytes at offset 0
        //     XUserLocalId oldUser;           //  8 bytes at offset 32
        //     XUserLocalId newUser;           //  8 bytes at offset 40
        // }
        Assert.Equal(48, Marshal.SizeOf<XUserDeviceAssociationChange>());
        Assert.Equal(0, (int)Marshal.OffsetOf<XUserDeviceAssociationChange>(nameof(XUserDeviceAssociationChange.DeviceId)));
        Assert.Equal(32, (int)Marshal.OffsetOf<XUserDeviceAssociationChange>(nameof(XUserDeviceAssociationChange.OldUser)));
        Assert.Equal(40, (int)Marshal.OffsetOf<XUserDeviceAssociationChange>(nameof(XUserDeviceAssociationChange.NewUser)));
    }

    [Fact]
    public void XUserGetTokenAndSignatureDataLayoutIsCorrect()
    {
        // struct XUserGetTokenAndSignatureData {
        //     size_t tokenSize;      // nuint at offset 0
        //     size_t signatureSize;  // nuint at offset sizeof(nuint)
        //     const char* token;     // ptr at offset 2*sizeof(nuint)
        //     const char* signature; // ptr at offset 3*sizeof(nuint)
        // }
        int ptrSize = IntPtr.Size;
        Assert.Equal(ptrSize * 4, Marshal.SizeOf<XUserGetTokenAndSignatureData>());
    }

    [Fact]
    public void XUserGetTokenAndSignatureUtf16DataLayoutIsCorrect()
    {
        int ptrSize = IntPtr.Size;
        Assert.Equal(ptrSize * 4, Marshal.SizeOf<XUserGetTokenAndSignatureUtf16Data>());
    }

    // ─── Public struct / enum sanity ────────────────────────────────────────────────────────

    [Fact]
    public void AppLocalDeviceIdManagedIs32Bytes()
    {
        // The managed struct must have the same size as the native one for the reinterpret cast
        // in AppLocalDeviceId(XAppLocalDeviceId) / ToNative() to be safe.
        Assert.Equal(32, Marshal.SizeOf<AppLocalDeviceId>());
    }

    [Fact]
    public void AppLocalDeviceIdNullIsAllZeros()
    {
        Assert.True(AppLocalDeviceId.Null.IsNull);
        Assert.Equal(default, AppLocalDeviceId.Null);
    }

    [Fact]
    public void AppLocalDeviceIdEqualityWorks()
    {
        var a = default(AppLocalDeviceId);
        var b = default(AppLocalDeviceId);
        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact]
    public void AppLocalDeviceIdNativeRoundtrip()
    {
        // Construct a non-zero native id, convert to managed, convert back, compare bytes.
        XAppLocalDeviceId native;
        for (int i = 0; i < 32; i++)
        {
            native.Value[i] = (byte)(i + 1);
        }

        AppLocalDeviceId managed = new AppLocalDeviceId(native);
        XAppLocalDeviceId back = managed.ToNative();

        for (int i = 0; i < 32; i++)
        {
            Assert.Equal(native.Value[i], back.Value[i]);
        }
    }

    [Theory]
    // XUserGetTokenAndSignatureOptions from XUser.h
    [InlineData(TokenAndSignatureOptions.None, 0x00u)]
    [InlineData(TokenAndSignatureOptions.ForceRefresh, 0x01u)]
    [InlineData(TokenAndSignatureOptions.AllUsers, 0x02u)]
    public void TokenAndSignatureOptionsMatchTheHeader(TokenAndSignatureOptions value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XUserGetTokenAndSignatureOptions)value);
    }

    [Theory]
    // XUserDefaultAudioEndpointKind from XUser.h
    [InlineData(UserDefaultAudioEndpointKind.CommunicationRender, 0u)]
    [InlineData(UserDefaultAudioEndpointKind.CommunicationCapture, 1u)]
    public void UserDefaultAudioEndpointKindMatchesTheHeader(
        UserDefaultAudioEndpointKind value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XUserDefaultAudioEndpointKind)value);
    }

    [Fact]
    public void TokenAndSignatureOptionsIsAFlagsEnum()
    {
        var combined = TokenAndSignatureOptions.ForceRefresh | TokenAndSignatureOptions.AllUsers;
        Assert.Equal(0x03u, (uint)combined);
    }

    // ─── Callback thunk resolution ───────────────────────────────────────────────────────────

    [Fact]
    public void DeviceAssociationAndAudioEndpointThunksAreNonNull()
    {
        Assert.NotEqual(IntPtr.Zero, Trampolines.DeviceAssociationChangedCallback);
        Assert.NotEqual(IntPtr.Zero, Trampolines.DefaultAudioEndpointChangedCallback);
    }

    [Fact]
    public void AllUser2ThunksAreDistinct()
    {
        IntPtr async = Trampolines.AsyncCompletionRoutine;
        IntPtr userChanged = Trampolines.UserChangeEventCallback;
        IntPtr devAssoc = Trampolines.DeviceAssociationChangedCallback;
        IntPtr audioEndpt = Trampolines.DefaultAudioEndpointChangedCallback;

        Assert.NotEqual(async, devAssoc);
        Assert.NotEqual(async, audioEndpt);
        Assert.NotEqual(userChanged, devAssoc);
        Assert.NotEqual(userChanged, audioEndpt);
        Assert.NotEqual(devAssoc, audioEndpt);
    }

    // ─── Argument-validation (no native call required) ───────────────────────────────────────

    [Fact]
    public void TokenAndSignatureHttpHeaderStoresValues()
    {
        var h = new TokenAndSignatureHttpHeader("Content-Type", "application/json");
        Assert.Equal("Content-Type", h.Name);
        Assert.Equal("application/json", h.Value);
    }

    [Fact]
    public void TokenAndSignatureStoresTokenAndSignature()
    {
        var result = new TokenAndSignature("tok", "sig");
        Assert.Equal("tok", result.Token);
        Assert.Equal("sig", result.Signature);
    }

    [Fact]
    public void SignOutDeferralDisposeIsIdempotent()
    {
        // We cannot acquire a real deferral without the runtime, but we can exercise the
        // IDisposable contract via a zero-handle SafeHandle. A zero handle is IsInvalid and
        // ReleaseHandle will not be called, so no native call is made.
        var rawHandle = new SignOutDeferralHandle(IntPtr.Zero);
        var deferral = new SignOutDeferral(rawHandle);
        deferral.Dispose(); // must not throw
        deferral.Dispose(); // idempotent
    }

    // ─── Event-args types ────────────────────────────────────────────────────────────────────

    [Fact]
    public void UserDeviceAssociationChangedEventArgsExposesAllFields()
    {
        var deviceId = default(AppLocalDeviceId);
        var oldUser = new UserLocalId(0x1122);
        var newUser = new UserLocalId(0x3344);
        var args = new UserDeviceAssociationChangedEventArgs(deviceId, oldUser, newUser);

        Assert.Equal(deviceId, args.DeviceId);
        Assert.Equal(oldUser, args.OldUser);
        Assert.Equal(newUser, args.NewUser);
    }

    [Fact]
    public void UserDefaultAudioEndpointChangedEventArgsExposesAllFields()
    {
        var user = new UserLocalId(0xABCD);
        var args = new UserDefaultAudioEndpointChangedEventArgs(
            user,
            UserDefaultAudioEndpointKind.CommunicationCapture,
            "{GuidEndpoint}");

        Assert.Equal(user, args.User);
        Assert.Equal(UserDefaultAudioEndpointKind.CommunicationCapture, args.Kind);
        Assert.Equal("{GuidEndpoint}", args.EndpointId);
    }

    [Fact]
    public void UserDefaultAudioEndpointChangedEventArgsAllowsNullEndpointId()
    {
        var args = new UserDefaultAudioEndpointChangedEventArgs(
            UserLocalId.Null,
            UserDefaultAudioEndpointKind.CommunicationRender,
            null);

        Assert.Null(args.EndpointId);
    }
}
