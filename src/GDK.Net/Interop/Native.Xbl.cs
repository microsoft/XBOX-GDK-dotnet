// P/Invoke declarations for the XSAPI (Xbox Live) core -- xsapi-c\xbox_live_global_c.h,
// xbox_live_context_c.h and xbox_live_context_settings_c.h. Each Xbox Live service keeps its own
// entry points in a Native.Xbl<Service>.cs partial (Native.XblProfile.cs,
// Native.XblAchievements.cs, ...).
//
// Why a separate class from Native: XSAPI ships as its own redistributable DLL,
// Microsoft.Xbox.Services.C.Thunks.dll -- the projection's *second* native module. It exports 419
// entry points and, crucially, exports **no** XAsync*/XTaskQueue* functions of its own. An XSAPI
// call still takes an XAsyncBlock*, but the block is created, cancelled and polled through
// xgameruntime.thunks.dll. A title using Xbox Live therefore loads *both* modules, and
// AsyncOperation<T> works against XSAPI unchanged.
//
// Microsoft.Xbox.Services.C.Thunks.dll has a hard runtime dependency on libHttpClient.dll, which
// must sit beside it in the package layout. Neither is installed system-wide. Both come from
// %GameDKCoreLatest%windows\bin\{x64,arm64}; never from the GRDK\GameKit tree, which omits the
// Xbox Live stack entirely. See eng/packaging/GdkRedist.targets.
//
// Loading is lazy: the first XSAPI call is what pulls the module in, so a title that never touches
// Xbox Live never pays for it. XboxLiveService.Initialize translates the resulting
// DllNotFoundException into E_GAMERUNTIME_DLL_NOT_FOUND, exactly as GameRuntime.Initialize does.
//
// As in Native.cs, two shims exist and must stay signature-identical:
//   * net8.0 / net10.0  -> [LibraryImport], source-generated and AOT friendly.
//   * netstandard2.0    -> [DllImport].
// C++ `bool` is one byte, so every bool parameter is declared as `byte`.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class NativeXbl
{
    internal const string LibraryName = "Microsoft.Xbox.Services.C.Thunks.dll";

    // --- xbox_live_global_c.h: lifetime ---

    [LibraryImport(LibraryName)]
    internal static partial int XblInitialize(XblInitArgs* args);

    [LibraryImport(LibraryName)]
    internal static partial int XblCleanupAsync(XAsyncBlock* async);

    [LibraryImport(LibraryName)]
    internal static partial IntPtr XblGetAsyncQueue();

    [LibraryImport(LibraryName)]
    internal static partial int XblGetScid(byte** scid);

    [LibraryImport(LibraryName)]
    internal static partial int XblSetOverrideLocale(byte* locale);

    [LibraryImport(LibraryName)]
    internal static partial void XblDisableAssertsForXboxLiveThrottlingInDevSandboxes(
        XblConfigSetting setting);

    // --- xbox_live_context_c.h ---

    [LibraryImport(LibraryName)]
    internal static partial int XblContextCreateHandle(IntPtr user, IntPtr* context);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextDuplicateHandle(IntPtr context, IntPtr* duplicatedHandle);

    [LibraryImport(LibraryName)]
    internal static partial void XblContextCloseHandle(IntPtr context);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextGetUser(IntPtr context, IntPtr* user);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextGetXboxUserId(IntPtr context, ulong* xboxUserId);

    // --- xbox_live_context_settings_c.h ---

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsGetLongHttpTimeout(IntPtr context, uint* timeoutInSeconds);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsSetLongHttpTimeout(IntPtr context, uint timeoutInSeconds);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsGetHttpRetryDelay(IntPtr context, uint* delayInSeconds);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsSetHttpRetryDelay(IntPtr context, uint delayInSeconds);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsGetHttpTimeoutWindow(IntPtr context, uint* timeoutWindowInSeconds);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsSetHttpTimeoutWindow(IntPtr context, uint timeoutWindowInSeconds);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsGetWebsocketTimeoutWindow(IntPtr context, uint* timeoutWindowInSeconds);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsSetWebsocketTimeoutWindow(IntPtr context, uint timeoutWindowInSeconds);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsGetUseCrossPlatformQosServers(IntPtr context, byte* value);

    [LibraryImport(LibraryName)]
    internal static partial int XblContextSettingsSetUseCrossPlatformQosServers(IntPtr context, byte value);
}

#else

internal static unsafe partial class NativeXbl
{
    internal const string LibraryName = "Microsoft.Xbox.Services.C.Thunks.dll";

    // --- xbox_live_global_c.h: lifetime ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblInitialize(XblInitArgs* args);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblCleanupAsync(XAsyncBlock* async);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern IntPtr XblGetAsyncQueue();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblGetScid(byte** scid);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblSetOverrideLocale(byte* locale);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XblDisableAssertsForXboxLiveThrottlingInDevSandboxes(
        XblConfigSetting setting);

    // --- xbox_live_context_c.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextCreateHandle(IntPtr user, IntPtr* context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextDuplicateHandle(IntPtr context, IntPtr* duplicatedHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XblContextCloseHandle(IntPtr context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextGetUser(IntPtr context, IntPtr* user);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextGetXboxUserId(IntPtr context, ulong* xboxUserId);

    // --- xbox_live_context_settings_c.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsGetLongHttpTimeout(IntPtr context, uint* timeoutInSeconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsSetLongHttpTimeout(IntPtr context, uint timeoutInSeconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsGetHttpRetryDelay(IntPtr context, uint* delayInSeconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsSetHttpRetryDelay(IntPtr context, uint delayInSeconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsGetHttpTimeoutWindow(IntPtr context, uint* timeoutWindowInSeconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsSetHttpTimeoutWindow(IntPtr context, uint timeoutWindowInSeconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsGetWebsocketTimeoutWindow(IntPtr context, uint* timeoutWindowInSeconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsSetWebsocketTimeoutWindow(IntPtr context, uint timeoutWindowInSeconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsGetUseCrossPlatformQosServers(IntPtr context, byte* value);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XblContextSettingsSetUseCrossPlatformQosServers(IntPtr context, byte value);
}

#endif
