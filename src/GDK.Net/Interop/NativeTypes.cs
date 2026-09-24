// Raw interop layer for the Microsoft GDK flat C API.
//
// Types here mirror the headers under %GameDKCoreLatest%windows\include one-for-one and keep the
// native `X*` names on purpose: this layer is the hand-maintained stand-in for the ClangSharp
// output described in docs/plan.md section 3, so swapping in generated code must not disturb the
// idiomatic layer above it.
//
// Sources: XAsync.h, XTaskQueue.h, XGameRuntimeFeature.h (GDK edition 260404). Each API family
// has its own NativeTypes.<Family>.cs partner, see NativeTypes.User.cs, NativeTypes.Store.cs and
// so on.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>struct XAsyncBlock</c> from XAsync.h.</summary>
/// <remarks>
/// The trailing <c>unsigned char internal[sizeof(void*) * 4]</c> is opaque to callers and is
/// modelled as four pointer-sized fields so the layout stays correct on both x64 and arm64.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct XAsyncBlock
{
    public IntPtr Queue;
    public IntPtr Context;
    public IntPtr Callback;
    public IntPtr Internal0;
    public IntPtr Internal1;
    public IntPtr Internal2;
    public IntPtr Internal3;
}

/// <summary>Mirrors <c>struct XTaskQueueRegistrationToken</c> from XTaskQueue.h.</summary>
[StructLayout(LayoutKind.Sequential)]
internal struct XTaskQueueRegistrationToken
{
    public ulong Token;
}

internal enum XTaskQueueDispatchMode : uint
{
    Manual = 0,
    ThreadPool = 1,
    SerializedThreadPool = 2,
    Immediate = 3,
}

internal enum XTaskQueuePort : uint
{
    Work = 0,
    Completion = 1,
}

internal enum XGameRuntimeFeature : uint
{
    XAccessibility = 0,
    XAppCapture = 1,
    XAsync = 2,
    XAsyncProvider = 3,
    XDisplay = 4,
    XGame = 5,
    XGameInvite = 6,
    XGameSave = 7,
    XGameUI = 8,
    XLauncher = 9,
    XNetworking = 10,
    XPackage = 11,
    XPersistentLocalStorage = 12,
    XSpeechSynthesizer = 13,
    XStore = 14,
    XSystem = 15,
    XTaskQueue = 16,
    XThread = 17,
    XUser = 18,
    XError = 19,
    XGameEvent = 20,
    XGameStreaming = 21,
}
