// P/Invoke declarations for xgameruntime.thunks.dll: the runtime core: XGameRuntimeInit.h,
// XGameRuntimeFeature.h, XTaskQueue.h and XAsync.h. Every other family lives in its own
// Native.<Family>.cs partial; see Native.User.cs, Native.Store.cs and so on.
//
// IMPORTANT: the entry points are NOT in XGameRuntime.dll. That module (in System32) exports only
// four private ordinals used for version negotiation -- InitializeApiImpl, InitializeApiImplEx,
// InitializeApiImplEx2 and UninitializeApiImpl. Every public X* API is a statically linked stub
// inside xgameruntime.lib, so a C++ title reaches them through the static library and a P/Invoke
// against XGameRuntime.dll can only ever raise EntryPointNotFoundException.
//
// The GDK also ships xgameruntime.thunks.dll (%GameDKCoreLatest%windows\bin\{x64,arm64}), which
// re-exports the full surface -- 355 flat __stdcall C entry points -- as an ordinary DLL. That is
// the module every non-C++ projection must bind to, and it must be redistributed next to the game
// executable inside the package layout: it is not present in System32.
//
// On a machine without the Gaming Runtime the first call raises DllNotFoundException; the idiomatic
// layer translates that into E_GAMERUNTIME_DLL_NOT_FOUND (see GameRuntime.Initialize).
//
// Two shims exist per docs/plan.md section 3:
//   * net8.0 / net10.0    -> [LibraryImport], source-generated and trimming/AOT friendly.
//   * netstandard2.0      -> [DllImport], the only option on the older surface.
// Both blocks must stay signature-identical; every parameter is blittable so the two generate
// equivalent stubs.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    internal const string LibraryName = "xgameruntime.thunks.dll";

    // --- XGameRuntimeInit.h / XGameRuntimeFeature.h ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameRuntimeInitialize();

    [LibraryImport(LibraryName)]
    internal static partial void XGameRuntimeUninitialize();

    [LibraryImport(LibraryName)]
    internal static partial byte XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature feature);

    // --- XTaskQueue.h ---

    [LibraryImport(LibraryName)]
    internal static partial int XTaskQueueCreate(
        XTaskQueueDispatchMode workDispatchMode,
        XTaskQueueDispatchMode completionDispatchMode,
        IntPtr* queue);

    [LibraryImport(LibraryName)]
    internal static partial int XTaskQueueDuplicateHandle(IntPtr queueHandle, IntPtr* duplicatedHandle);

    [LibraryImport(LibraryName)]
    internal static partial byte XTaskQueueDispatch(IntPtr queue, XTaskQueuePort port, uint timeoutInMs);

    [LibraryImport(LibraryName)]
    internal static partial int XTaskQueueTerminate(
        IntPtr queue,
        byte wait,
        IntPtr callbackContext,
        IntPtr callback);

    [LibraryImport(LibraryName)]
    internal static partial void XTaskQueueCloseHandle(IntPtr queue);

    // --- XAsync.h ---

    [LibraryImport(LibraryName)]
    internal static partial void XAsyncCancel(XAsyncBlock* asyncBlock);

    [LibraryImport(LibraryName)]
    internal static partial int XAsyncGetStatus(XAsyncBlock* asyncBlock, byte wait);
}

#else

internal static unsafe partial class Native
{
    internal const string LibraryName = "xgameruntime.thunks.dll";

    // --- XGameRuntimeInit.h / XGameRuntimeFeature.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameRuntimeInitialize();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameRuntimeUninitialize();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature feature);

    // --- XTaskQueue.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XTaskQueueCreate(
        XTaskQueueDispatchMode workDispatchMode,
        XTaskQueueDispatchMode completionDispatchMode,
        IntPtr* queue);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XTaskQueueDuplicateHandle(IntPtr queueHandle, IntPtr* duplicatedHandle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XTaskQueueDispatch(IntPtr queue, XTaskQueuePort port, uint timeoutInMs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XTaskQueueTerminate(
        IntPtr queue,
        byte wait,
        IntPtr callbackContext,
        IntPtr callback);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XTaskQueueCloseHandle(IntPtr queue);

    // --- XAsync.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XAsyncCancel(XAsyncBlock* asyncBlock);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XAsyncGetStatus(XAsyncBlock* asyncBlock, byte wait);
}

#endif
