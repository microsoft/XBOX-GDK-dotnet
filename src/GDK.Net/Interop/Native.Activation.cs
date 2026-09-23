// P/Invoke declarations for the activation and in-game event families.
//
// XGameActivation.h publishes XGameActivationRegisterForEvent / XGameActivationUnregisterForEvent /
// XGameActivationAcceptPendingInvite as the modern replacement for the XGameInvite and
// XGameProtocol entry points. GDK edition 260404 exports all three from xgameruntime.thunks.dll,
// so they are bound below. The deprecated XGameInvite and XGameProtocol registrations stay bound
// too, because a title may still use the separate events; see Activation/GameActivationManager.cs.
//
// The XGameInvite *pending* trio (XGameInviteRegisterForPendingEvent,
// XGameInviteUnregisterForPendingEvent, XGameInviteAcceptPendingInvite) remains unexported in
// 260404 and is therefore not declared here; XGameActivation reports pending invites instead.
//
// See Interop/Native.cs for the shim rules these declarations follow.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XGameInvite.h (deprecated in the header, but the only exported invite registration) ---

    // --- XGameProtocol.h ---

    // --- XGameEvent.h ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameEventWrite(
        IntPtr user,
        byte* serviceConfigId,
        byte* playSessionId,
        byte* eventName,
        byte* dimensionsJson,
        byte* measurementsJson);

    // --- XGameActivation.h ---
    //
    // XGameActivationRegisterForEvent is the unified modern replacement for the separate
    // XGameInviteRegisterForEvent and XGameProtocolRegisterForActivation pair: one registration
    // reports protocol launches, file launches, and both pending and accepted invites,
    // discriminated by XGameActivationInfo::type. The older pair remains bound above for titles
    // that still use the separate events.

    [LibraryImport(LibraryName)]
    internal static partial int XGameActivationRegisterForEvent(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial byte XGameActivationUnregisterForEvent(
        XTaskQueueRegistrationToken token,
        byte wait);

    [LibraryImport(LibraryName)]
    internal static partial int XGameActivationAcceptPendingInvite(byte* inviteUri);
}

#else

internal static unsafe partial class Native
{
    // --- XGameInvite.h (deprecated in the header, but the only exported invite registration) ---

    // --- XGameProtocol.h ---

    // --- XGameEvent.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameEventWrite(
        IntPtr user,
        byte* serviceConfigId,
        byte* playSessionId,
        byte* eventName,
        byte* dimensionsJson,
        byte* measurementsJson);

    // --- XGameActivation.h ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameActivationRegisterForEvent(
        IntPtr queue,
        IntPtr context,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XGameActivationUnregisterForEvent(
        XTaskQueueRegistrationToken token,
        byte wait);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameActivationAcceptPendingInvite(byte* inviteUri);
}

#endif
