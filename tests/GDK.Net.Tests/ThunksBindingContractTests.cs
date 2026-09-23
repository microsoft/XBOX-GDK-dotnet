using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using GDK.Net;
using GDK.Net.Activation;
using GDK.Net.Interop;
using GDK.Net.Streaming;
using GDK.Net.Users;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for the core-runtime APIs that <c>xgameruntime.thunks.dll</c> did not export
/// before GDK edition 260404: the XGameActivation registrations, the XStore operations whose
/// starters were missing, user-controlled XAppCapture recording,
/// <c>XGameStreamingGetGamepadPhysicality</c>, <c>XPersistentLocalStorageMountForPackage</c>, and
/// the process-global XUserPlatform prompt hooks.
/// </summary>
/// <remarks>
/// None of these require a Gaming Runtime to be present. They pin the binding target, the native
/// struct and enum layouts read from GDK edition 260404's headers, and the argument validation that
/// happens before any native call. They also pin the handful of entry points that remain
/// unreachable in 260404, so a future edition that exports them is noticed rather than assumed.
/// </remarks>
public class ThunksBindingContractTests
{
    private static Type NativeType =>
        typeof(GameRuntime).Assembly.GetType("GDK.Net.Interop.Native", throwOnError: true)!;

    private static MethodInfo Method(string name) =>
        NativeType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static)
        ?? throw new InvalidOperationException($"Native.{name} is not declared.");

    // ── every entry point 260404 added binds to the one redistributable module ──

    public static TheoryData<string> NewlyExportedEntryPoints => new()
    {
        "XGameActivationRegisterForEvent",
        "XGameActivationUnregisterForEvent",
        "XGameActivationAcceptPendingInvite",

        "XStoreQueryPackageUpdatesAsync",
        "XStoreQueryPackageUpdatesResultCount",
        "XStoreQueryPackageUpdatesResult",
        "XStoreShowGiftingUIAsync",
        "XStoreShowGiftingUIResult",
        "XStoreQueryAssociatedProductsForStoreIdAsync",
        "XStoreQueryAssociatedProductsForStoreIdResult",
        "XAppCaptureStartUserRecord",
        "XAppCaptureStopUserRecord",
        "XAppCaptureCancelUserRecord",
        "XGameStreamingGetGamepadPhysicality",
        "XPersistentLocalStorageMountForPackage",
        "XSystemAllowFullDownloadBandwidth",
        "XUserSignOutAsync",
        "XUserSignOutResult",
        "XUserIsSignOutPresent",
        "XPackageGetPackageKind",
        "XUserPlatformRemoteConnectSetEventHandlers",
        "XUserPlatformRemoteConnectCancelPrompt",
        "XUserPlatformSpopPromptSetEventHandlers",
        "XUserPlatformSpopPromptComplete",
    };

    [Theory]
    [MemberData(nameof(NewlyExportedEntryPoints))]
    public void NewlyExportedEntryPointsAreDeclared(string name) => Assert.NotNull(Method(name));

    [Theory]
    [MemberData(nameof(NewlyExportedEntryPoints))]
    public void NewlyExportedEntryPointsBindToTheThunksModule(string name)
    {
        var import = Method(name).GetCustomAttribute<DllImportAttribute>();

        Assert.NotNull(import);

        // The projection ships no native code of its own. Everything resolves out of the Gaming
        // Runtime's redistributable thunks DLL. See eng/unexported-apis.md.
        Assert.Equal("xgameruntime.thunks.dll", import!.Value);
    }

    [Fact]
    public void EveryNativeEntryPointBindsToAKnownGamingRuntimeModule()
    {
        // A stray module name -- for instance a reintroduced projection-owned shim -- compiles
        // cleanly and fails only at first call, with a DllNotFoundException naming a file the
        // packaging layout never produces.
        var modules = NativeType
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(m => m.GetCustomAttribute<DllImportAttribute>())
            .Where(a => a is not null)
            .Select(a => a!.Value)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        Assert.NotEmpty(modules);
        Assert.All(modules, m => Assert.Equal("xgameruntime.thunks.dll", m));
    }

    /// <summary>
    /// The entry points <c>xgameruntime.lib</c> declares but edition 260404's
    /// <c>xgameruntime.thunks.dll</c> still does not export.
    /// </summary>
    public static TheoryData<string> StillUnexportedEntryPoints => new()
    {
        "XErrorReport",
        "XGameInviteRegisterForPendingEvent",
        "XGameInviteUnregisterForPendingEvent",
        "XGameInviteAcceptPendingInvite",
        "XNetworkingSetConfigurationSetting",
        "XThreadVerifyNotTimeSensitive",
        "XGameStreamingGetAssociatedFrame",
        "XGameStreamingSendDebugMessageToClient",
        "XGameUiShowManageSpaceAsync",
        "XGameUiShowManageSpaceResult",
        "XPackageGetIdentifier",
        "XUserGetMsaTokenSilentlyAsync",
        "XUserGetMsaTokenSilentlyResult",
        "XUserGetMsaTokenSilentlyResultSize",
    };

    [Theory]
    [MemberData(nameof(StillUnexportedEntryPoints))]
    public void StillUnexportedEntryPointsAreNotBound(string name)
    {
        // Binding one of these compiles, then throws EntryPointNotFoundException at first call --
        // which GameRuntime misreports as E_GAMERUNTIME_VERSION_MISMATCH. If a future edition
        // exports one, move it to NewlyExportedEntryPoints and project it.
        Assert.Null(NativeType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static));
    }

    [Fact]
    public void DeliberatelyOmittedEntryPointsAreNotDeclared()
    {
        // The async provider entry points exist to *implement* async operations. AsyncOperation<T>
        // does that once and the projection exposes Task/async/await instead.
        Assert.Null(NativeType.GetMethod("XAsyncRun", BindingFlags.NonPublic | BindingFlags.Static));
        Assert.Null(NativeType.GetMethod(
            "XAsyncGetResultSize", BindingFlags.NonPublic | BindingFlags.Static));
    }

    [Fact]
    public void MembersThatOnlyTheRetiredShimCouldReachAreGone()
    {
        // These three had no other native route. Deleting the shim deleted them; a reappearance
        // means someone bound an unexported entry point again.
        Assert.Null(typeof(GDK.Net.Networking.NetworkingManager).GetMethod("SetConfigurationSetting"));
        Assert.Null(typeof(GDK.Net.SystemInfo.GameErrorHandling).GetMethod("Report"));
        Assert.Null(typeof(GDK.Net.SystemInfo.GameThread).GetMethod("VerifyNotTimeSensitive"));

        // AssertNotTimeSensitive covers the same ground and is exported, so nothing is lost.
        Assert.NotNull(typeof(GDK.Net.SystemInfo.GameThread).GetMethod("AssertNotTimeSensitive"));

        Assert.Null(typeof(GameRuntime).Assembly.GetType("GDK.Net.GameRuntimeExtras"));
    }

    // ── native struct layouts ──

    [Fact]
    public void XGameActivationInfoMatchesTheHeaderLayout()
    {
        // struct { XGameActivationType type; union { const char* protocolUri/file/inviteUri; }; }
        // A 4-byte enum followed by a pointer, so the compiler pads to the pointer's alignment.
        Assert.Equal(IntPtr.Size == 8 ? 16 : 8, Marshal.SizeOf<XGameActivationInfo>());
        Assert.Equal(0, (int)Marshal.OffsetOf<XGameActivationInfo>(nameof(XGameActivationInfo.Type)));
        Assert.Equal(IntPtr.Size, (int)Marshal.OffsetOf<XGameActivationInfo>(nameof(XGameActivationInfo.Uri)));
    }

    [Fact]
    public void XUserPlatformRemoteConnectEventHandlersIsThreePointers()
    {
        Assert.Equal(IntPtr.Size * 3, Marshal.SizeOf<XUserPlatformRemoteConnectEventHandlers>());
        Assert.Equal(
            0,
            (int)Marshal.OffsetOf<XUserPlatformRemoteConnectEventHandlers>(
                nameof(XUserPlatformRemoteConnectEventHandlers.Show)));
        Assert.Equal(
            IntPtr.Size,
            (int)Marshal.OffsetOf<XUserPlatformRemoteConnectEventHandlers>(
                nameof(XUserPlatformRemoteConnectEventHandlers.Close)));
        Assert.Equal(
            IntPtr.Size * 2,
            (int)Marshal.OffsetOf<XUserPlatformRemoteConnectEventHandlers>(
                nameof(XUserPlatformRemoteConnectEventHandlers.Context)));
    }

    // ── enum parity with the headers ──

    [Fact]
    public void XGameActivationTypeMatchesTheHeader()
    {
        Assert.Equal(0u, (uint)XGameActivationType.Protocol);
        Assert.Equal(1u, (uint)XGameActivationType.File);
        Assert.Equal(2u, (uint)XGameActivationType.PendingGameInvite);
        Assert.Equal(3u, (uint)XGameActivationType.AcceptedGameInvite);
    }

    [Fact]
    public void SpopOperationResultMatchesTheNativeEnumValueForValue()
    {
        // The public enum is cast straight to the native one, so the numbering has to agree.
        Assert.Equal(
            (int)XUserPlatformSpopOperationResult.SignInHere, (int)SpopOperationResult.SignInHere);
        Assert.Equal(
            (int)XUserPlatformSpopOperationResult.SwitchAccount, (int)SpopOperationResult.SwitchAccount);
        Assert.Equal(
            (int)XUserPlatformSpopOperationResult.Failure, (int)SpopOperationResult.Failure);
        Assert.Equal(
            (int)XUserPlatformSpopOperationResult.Canceled, (int)SpopOperationResult.Canceled);
    }

    [Fact]
    public void GameActivationKindKeepsItsOriginalNumbering()
    {
        // GameActivationType is a projection-level concept and does NOT mirror XGameActivationType.
        // The three new members were appended rather than renumbered, so existing persisted or
        // switch-mapped values keep meaning what they meant.
        Assert.Equal(0, (int)GameActivationType.Protocol);
        Assert.Equal(1, (int)GameActivationType.Invite);
        Assert.Equal(2, (int)GameActivationType.File);
        Assert.Equal(3, (int)GameActivationType.PendingGameInvite);
        Assert.Equal(4, (int)GameActivationType.AcceptedGameInvite);
    }

    [Fact]
    public void StreamingGamepadPhysicalityIsA64BitFlagsEnum()
    {
        // XGameStreamingGamepadPhysicality is uint64_t; a narrower managed enum would silently
        // truncate the high flags.
        Assert.Equal(typeof(ulong), Enum.GetUnderlyingType(typeof(StreamingGamepadPhysicality)));
        Assert.NotNull(typeof(StreamingGamepadPhysicality).GetCustomAttribute<FlagsAttribute>());
    }

    // ── public surface ──

    [Fact]
    public void GameActivationManagerExposesTheUnifiedActivationSurface()
    {
        Type t = typeof(GameActivationManager);

        // XGameActivationRegisterForEvent is the GDK's single activation entry point, so the
        // projection exposes exactly one event. The per-kind XGameInvite and XGameProtocol
        // registrations it replaced are deprecated in the headers and are not projected.
        Assert.NotNull(t.GetEvent("Activated"));
        Assert.NotNull(t.GetMethod("AcceptPendingInvite", new[] { typeof(string) }));
        Assert.Null(t.GetEvent("InviteReceived"));
        Assert.Null(t.GetEvent("ProtocolActivated"));
        Assert.Null(t.GetEvent("PendingInviteReceived"));
    }

    [Fact]
    public void StoreContextExposesTheOperations260404Unlocked()
    {
        Type t = typeof(GDK.Net.Store.StoreContext);

        Assert.NotNull(t.GetMethod("QueryPackageUpdatesAsync"));
        Assert.NotNull(t.GetMethod("QueryAssociatedProductsForStoreIdAsync"));
        Assert.NotNull(t.GetMethod("ShowGiftingUIAsync"));
    }

    [Fact]
    public void NetworkingManagerExposesBothUrlEncodings()
    {
        Type t = typeof(GDK.Net.Networking.NetworkingManager);

        Assert.NotNull(t.GetMethod("QuerySecurityInformationForUrlAsync"));
        Assert.NotNull(t.GetMethod("QuerySecurityInformationForUrlUtf16Async"));
    }

    [Fact]
    public void GamePackageExposesAnExplicitUnregisterForItsStaticEvent()
    {
        // PackageInstalled is a static event with no owning instance, so without this the native
        // registration would necessarily outlive every subscriber.
        MethodInfo? unregister = typeof(GDK.Net.Package.GamePackage)
            .GetMethod("UnregisterPackageInstalled", BindingFlags.Public | BindingFlags.Static);

        Assert.NotNull(unregister);
        Assert.Empty(unregister!.GetParameters());
    }

    // ── the process-global prompt hooks ──

    [Fact]
    public void UserPlatformIsStaticBecauseTheHandlerTablesAreProcessGlobal()
    {
        Type t = typeof(UserPlatform);

        Assert.True(t.IsAbstract && t.IsSealed, "UserPlatform must be a static class.");

        // No token comes back from XUserPlatform*SetEventHandlers, so there is nothing to
        // unregister and nothing to dispose.
        Assert.DoesNotContain(
            typeof(IDisposable), t.GetInterfaces());
    }

    [Fact]
    public void UnsubscribingFromUserPlatformNeverThrows()
    {
        // The remove accessors must never touch native code: a title that subscribed has to be
        // able to unwind on a machine where the Gaming Runtime is gone.
        EventHandler<SpopPromptEventArgs> spop = static (_, _) => { };
        EventHandler<RemoteConnectShowPromptEventArgs> show = static (_, _) => { };
        EventHandler<RemoteConnectClosePromptEventArgs> close = static (_, _) => { };

        UserPlatform.SpopPrompt -= spop;
        UserPlatform.RemoteConnectShowPrompt -= show;
        UserPlatform.RemoteConnectClosePrompt -= close;
    }

    // ── argument validation happens before the native boundary ──

    [Fact]
    public void RemoteConnectPromptClosedEventArgsRejectsANullRequest()
    {
        var closed = (RemoteConnectClosePromptEventArgs)Activator.CreateInstance(
            typeof(RemoteConnectClosePromptEventArgs),
            BindingFlags.NonPublic | BindingFlags.Instance,
            binder: null,
            args: new object[] { 1u, new IntPtr(0x1234) },
            culture: null)!;

        Assert.Throws<ArgumentNullException>(() => closed.Matches(null!));
    }

    [Fact]
    public void RemoteConnectPromptClosedEventArgsPairsOnTheOperationToken()
    {
        var operation = new IntPtr(0x1234);
        var other = new IntPtr(0x5678);

        var request = NewPromptArgs(operation);

        Assert.True(NewClosedArgs(operation).Matches(request));
        Assert.False(NewClosedArgs(other).Matches(request));
    }

    [Fact]
    public void RemoteConnectPromptEventArgsSurfacesAnEmptyQrCodeRatherThanNull()
    {
        Assert.NotNull(NewPromptArgs(new IntPtr(1)).QrCode);
    }

    private static RemoteConnectShowPromptEventArgs NewPromptArgs(IntPtr operation) =>
        (RemoteConnectShowPromptEventArgs)Activator.CreateInstance(
            typeof(RemoteConnectShowPromptEventArgs),
            BindingFlags.NonPublic | BindingFlags.Instance,
            binder: null,
            args: new object[] { 1u, operation, "https://aka.ms/", "ABC123", Array.Empty<byte>() },
            culture: null)!;

    private static RemoteConnectClosePromptEventArgs NewClosedArgs(IntPtr operation) =>
        (RemoteConnectClosePromptEventArgs)Activator.CreateInstance(
            typeof(RemoteConnectClosePromptEventArgs),
            BindingFlags.NonPublic | BindingFlags.Instance,
            binder: null,
            args: new object[] { 1u, operation },
            culture: null)!;
}
