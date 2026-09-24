// P/Invoke declarations for the XGameStreaming family.
//
// All entry points below are verified present in xgameruntime.thunks.dll
// (Native.LibraryName) via the authoritative lib→DLL diff (xgameruntime.lib 404 symbols
// vs xgameruntime.thunks.dll 390 exports in edition 260404; 14 total gaps across all families).
//
// OMITTED: present in xgameruntime.lib / public headers but NOT exported by
// xgameruntime.thunks.dll. Declaring a P/Invoke for these would throw
// EntryPointNotFoundException at runtime (surfaced as E_GAMERUNTIME_VERSION_MISMATCH):
//
//   XGameStreamingGetAssociatedFrame: deprecated in the header; never made it to thunks.
//                                             Also out of scope: it returns a GXDK console type
//                                             and takes an IGameInputReading*.
//   XGameStreamingSendDebugMessageToClient, not exported and declared in no header; use
//                                             server-side tooling instead.
//
// XGameStreamingGetGamepadPhysicality was in that list until edition 260404 exported it; it is
// bound below.
//
// BOUND BUT DEPRECATED in the header (exported, so bindable):
//   XGameStreamingGetLastFrameDisplayed (deprecated; prefer XGameStreamingGetDisplayDetails.
//   XGameStreamingGetClientIPAddress) deprecated; returns E_GAMESTREAMING_NO_DATA on the
//                                             converged (WebRTC) streaming stack.
//
// See Interop/Native.cs for the dual-shim rules these declarations follow.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XGameStreaming.h: lifecycle ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingInitialize();

    [LibraryImport(LibraryName)]
    internal static partial void XGameStreamingUninitialize();

    // --- XGameStreaming.h: streaming state ---

    [LibraryImport(LibraryName)]
    internal static partial byte XGameStreamingIsStreaming();

    [LibraryImport(LibraryName)]
    internal static partial uint XGameStreamingGetClientCount();

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingGetClients(
        uint clientCount,
        ulong* clients,
        uint* clientsUsed);

    [LibraryImport(LibraryName)]
    internal static partial XGameStreamingConnectionState XGameStreamingGetConnectionState(
        ulong client);

    // --- XGameStreaming.h: connection state events ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingRegisterConnectionStateChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XGameStreamingUnregisterConnectionStateChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XGameStreaming.h: touch controls ---

    [LibraryImport(LibraryName)]
    internal static partial void XGameStreamingHideTouchControls();

    [LibraryImport(LibraryName)]
    internal static partial void XGameStreamingHideTouchControlsOnClient(ulong client);

    [LibraryImport(LibraryName)]
    internal static partial void XGameStreamingShowTouchControlLayout(byte* layout);

    [LibraryImport(LibraryName)]
    internal static partial void XGameStreamingShowTouchControlLayoutOnClient(
        ulong client,
        byte* layout);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingUpdateTouchControlsState(
        nuint operationCount,
        XGameStreamingTouchControlsStateOperation* operations);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingUpdateTouchControlsStateOnClient(
        ulong client,
        nuint operationCount,
        XGameStreamingTouchControlsStateOperation* operations);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingShowTouchControlsWithStateUpdate(
        byte* layout,
        nuint operationCount,
        XGameStreamingTouchControlsStateOperation* operations);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingShowTouchControlsWithStateUpdateOnClient(
        ulong client,
        byte* layout,
        nuint operationCount,
        XGameStreamingTouchControlsStateOperation* operations);

    // --- XGameStreaming.h: per-client properties events ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingRegisterClientPropertiesChanged(
        ulong client,
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XGameStreamingUnregisterClientPropertiesChanged(
        ulong client,
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XGameStreaming.h: per-client getters ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingGetStreamPhysicalDimensions(
        ulong client,
        uint* horizontalMm,
        uint* verticalMm);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingGetStreamAddedLatency(
        ulong client,
        uint* averageInputLatencyUs,
        uint* averageOutputLatencyUs,
        uint* standardDeviationUs);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingIsTouchInputEnabled(
        ulong client,
        byte* touchInputEnabled);

    [LibraryImport(LibraryName)]
    internal static partial nuint XGameStreamingGetTouchBundleVersionNameSize(ulong client);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingGetTouchBundleVersion(
        ulong client,
        XVersion* version,
        nuint versionNameSize,
        byte* versionName);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingGetSessionId(
        ulong client,
        nuint sessionIdSize,
        byte* sessionId,
        nuint* sessionIdUsed);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingGetDisplayDetails(
        ulong client,
        uint maxSupportedPixels,
        float widestSupportedAspectRatio,
        float tallestSupportedAspectRatio,
        XGameStreamingDisplayDetails* displayDetails);

    // --- XGameStreaming.h: server / global ---

    [LibraryImport(LibraryName)]
    internal static partial nuint XGameStreamingGetServerLocationNameSize();

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingGetServerLocationName(
        nuint serverLocationNameSize,
        byte* serverLocationName);

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingSetResolution(uint width, uint height);

    // --- XGameStreaming.h: deprecated but exported ---


    // --- XGameStreaming.h: gamepad physicality ---
    //
    // Although this takes an IGameInputReading*, that type is only forward-declared in the header
    // ("struct IGameInputReading;") and never dereferenced across the ABI, so it projects as an
    // opaque pointer without pulling in a GameInput dependency.

    [LibraryImport(LibraryName)]
    internal static partial int XGameStreamingGetGamepadPhysicality(
        IntPtr gamepadReading,
        XGameStreamingGamepadPhysicality* gamepadPhysicality);
}

#else

internal static unsafe partial class Native
{
    // --- XGameStreaming.h: lifecycle ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingInitialize();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameStreamingUninitialize();

    // --- XGameStreaming.h: streaming state ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XGameStreamingIsStreaming();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern uint XGameStreamingGetClientCount();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingGetClients(
        uint clientCount,
        ulong* clients,
        uint* clientsUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern XGameStreamingConnectionState XGameStreamingGetConnectionState(
        ulong client);

    // --- XGameStreaming.h: connection state events ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingRegisterConnectionStateChanged(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XGameStreamingUnregisterConnectionStateChanged(
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XGameStreaming.h: touch controls ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameStreamingHideTouchControls();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameStreamingHideTouchControlsOnClient(ulong client);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameStreamingShowTouchControlLayout(byte* layout);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XGameStreamingShowTouchControlLayoutOnClient(
        ulong client,
        byte* layout);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingUpdateTouchControlsState(
        nuint operationCount,
        XGameStreamingTouchControlsStateOperation* operations);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingUpdateTouchControlsStateOnClient(
        ulong client,
        nuint operationCount,
        XGameStreamingTouchControlsStateOperation* operations);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingShowTouchControlsWithStateUpdate(
        byte* layout,
        nuint operationCount,
        XGameStreamingTouchControlsStateOperation* operations);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingShowTouchControlsWithStateUpdateOnClient(
        ulong client,
        byte* layout,
        nuint operationCount,
        XGameStreamingTouchControlsStateOperation* operations);

    // --- XGameStreaming.h: per-client properties events ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingRegisterClientPropertiesChanged(
        ulong client,
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XGameStreamingUnregisterClientPropertiesChanged(
        ulong client,
        XTaskQueueRegistrationToken token,
        byte wait);

    // --- XGameStreaming.h: per-client getters ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingGetStreamPhysicalDimensions(
        ulong client,
        uint* horizontalMm,
        uint* verticalMm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingGetStreamAddedLatency(
        ulong client,
        uint* averageInputLatencyUs,
        uint* averageOutputLatencyUs,
        uint* standardDeviationUs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingIsTouchInputEnabled(
        ulong client,
        byte* touchInputEnabled);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern nuint XGameStreamingGetTouchBundleVersionNameSize(ulong client);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingGetTouchBundleVersion(
        ulong client,
        XVersion* version,
        nuint versionNameSize,
        byte* versionName);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingGetSessionId(
        ulong client,
        nuint sessionIdSize,
        byte* sessionId,
        nuint* sessionIdUsed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingGetDisplayDetails(
        ulong client,
        uint maxSupportedPixels,
        float widestSupportedAspectRatio,
        float tallestSupportedAspectRatio,
        XGameStreamingDisplayDetails* displayDetails);

    // --- XGameStreaming.h: server / global ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern nuint XGameStreamingGetServerLocationNameSize();

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingGetServerLocationName(
        nuint serverLocationNameSize,
        byte* serverLocationName);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingSetResolution(uint width, uint height);

    // --- XGameStreaming.h: deprecated but exported ---


    // --- XGameStreaming.h: gamepad physicality ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameStreamingGetGamepadPhysicality(
        IntPtr gamepadReading,
        XGameStreamingGamepadPhysicality* gamepadPhysicality);
}

#endif
