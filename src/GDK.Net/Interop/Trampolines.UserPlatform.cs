// Native-to-managed thunks for the XUserPlatform* prompt handlers in XUser.h.
//
// Unlike the other XUser callbacks, the header typedefs these three without the CALLBACK
// (__stdcall) macro, so they use the platform default convention. The GDK targets x64 and ARM64
// only, where there is a single native calling convention, so the choice is immaterial; Cdecl is
// used here to mirror the header literally.
//
// Same dual-shim pattern as Trampolines.cs: a function pointer to an [UnmanagedCallersOnly] static
// on modern .NET, a rooted delegate on netstandard2.0. Every body swallows managed exceptions so
// none can cross back into the Gaming Runtime.

using System;
using System.Runtime.InteropServices;
using GDK.Net.Users;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Thunks for <c>XUserPlatformRemoteConnectShowPromptEventHandler</c>,
/// <c>XUserPlatformRemoteConnectClosePromptEventHandler</c> and
/// <c>XUserPlatformSpopPromptEventHandler</c>.
/// </summary>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr RemoteConnectShowPromptCallback { get; } =
        (IntPtr)(delegate* unmanaged[Cdecl]<IntPtr, uint, IntPtr, byte*, byte*, nuint, byte*, void>)&OnRemoteConnectShowPrompt;

    internal static IntPtr RemoteConnectClosePromptCallback { get; } =
        (IntPtr)(delegate* unmanaged[Cdecl]<IntPtr, uint, IntPtr, void>)&OnRemoteConnectClosePrompt;

    internal static IntPtr SpopPromptCallback { get; } =
        (IntPtr)(delegate* unmanaged[Cdecl]<IntPtr, uint, IntPtr, byte*, byte*, void>)&OnSpopPrompt;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static void OnRemoteConnectShowPrompt(
        IntPtr context,
        uint userIdentifier,
        IntPtr operation,
        byte* url,
        byte* code,
        nuint qrCodeSize,
        byte* qrCode)
    {
        try
        {
            UserPlatform.DispatchRemoteConnectShow(userIdentifier, operation, url, code, qrCodeSize, qrCode);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static void OnRemoteConnectClosePrompt(IntPtr context, uint userIdentifier, IntPtr operation)
    {
        try
        {
            UserPlatform.DispatchRemoteConnectClose(userIdentifier, operation);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static void OnSpopPrompt(
        IntPtr context,
        uint userIdentifier,
        IntPtr operation,
        byte* modernGamertag,
        byte* modernGamertagSuffix)
    {
        try
        {
            UserPlatform.DispatchSpopPrompt(userIdentifier, operation, modernGamertag, modernGamertagSuffix);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void RemoteConnectShowPromptDelegate(
        IntPtr context,
        uint userIdentifier,
        IntPtr operation,
        byte* url,
        byte* code,
        nuint qrCodeSize,
        byte* qrCode);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void RemoteConnectClosePromptDelegate(IntPtr context, uint userIdentifier, IntPtr operation);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void SpopPromptDelegate(
        IntPtr context,
        uint userIdentifier,
        IntPtr operation,
        byte* modernGamertag,
        byte* modernGamertagSuffix);

    // Rooted for the process lifetime: the handler tables are process-global and never torn down.
    private static readonly RemoteConnectShowPromptDelegate RemoteConnectShowPromptKeepAlive = OnRemoteConnectShowPrompt;

    private static readonly RemoteConnectClosePromptDelegate RemoteConnectClosePromptKeepAlive = OnRemoteConnectClosePrompt;

    private static readonly SpopPromptDelegate SpopPromptKeepAlive = OnSpopPrompt;

    internal static IntPtr RemoteConnectShowPromptCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(RemoteConnectShowPromptKeepAlive);

    internal static IntPtr RemoteConnectClosePromptCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(RemoteConnectClosePromptKeepAlive);

    internal static IntPtr SpopPromptCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(SpopPromptKeepAlive);

    private static void OnRemoteConnectShowPrompt(
        IntPtr context,
        uint userIdentifier,
        IntPtr operation,
        byte* url,
        byte* code,
        nuint qrCodeSize,
        byte* qrCode)
    {
        try
        {
            UserPlatform.DispatchRemoteConnectShow(userIdentifier, operation, url, code, qrCodeSize, qrCode);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void OnRemoteConnectClosePrompt(IntPtr context, uint userIdentifier, IntPtr operation)
    {
        try
        {
            UserPlatform.DispatchRemoteConnectClose(userIdentifier, operation);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void OnSpopPrompt(
        IntPtr context,
        uint userIdentifier,
        IntPtr operation,
        byte* modernGamertag,
        byte* modernGamertagSuffix)
    {
        try
        {
            UserPlatform.DispatchSpopPrompt(userIdentifier, operation, modernGamertag, modernGamertagSuffix);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
