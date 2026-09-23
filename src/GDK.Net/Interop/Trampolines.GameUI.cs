// Native-to-managed thunks for the eight XGameUiUiCallbacks entry points.
//
// These are lifetime callbacks: XGameUiSetUiCallbacks hands the Gaming Runtime a table of function
// pointers that it keeps until the registration is replaced or cleared. On net8.0+ the pointers
// come from [UnmanagedCallersOnly] statics, which are ordinary function addresses and need no
// rooting. On netstandard2.0 they come from Marshal.GetFunctionPointerForDelegate, so the delegates
// are held in static readonly fields for the process lifetime -- letting one be collected would
// leave the runtime calling a freed stub.
//
// Each thunk only unpacks its arguments and forwards to CustomGameUi, which owns the copying of
// runtime-owned strings and arrays and the invocation of the title's handler. Every thunk swallows
// exceptions: unwinding a managed exception through the Gaming Runtime's C frames is undefined.
//
// The `context` parameter is always IntPtr.Zero. Handler state lives in CustomGameUi's statics
// rather than in a GCHandle passed through native memory, which keeps the registration
// AOT-friendly and avoids a handle leak when callbacks are replaced.

using System;
using System.Runtime.InteropServices;
using GDK.Net.GameUI;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr GameUiShowPlayerProfileCardCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, IntPtr, ulong, IntPtr, void>)
            &OnShowPlayerProfileCardUi;

    internal static IntPtr GameUiShowPlayerPickerCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, XGameUiPlayerPickerInfo*, IntPtr, void>)
            &OnShowPlayerPickerUi;

    internal static IntPtr GameUiShowSendGameInviteCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<
            IntPtr, IntPtr, IntPtr, byte*, byte*, byte*, byte*, byte*, IntPtr, void>)
            &OnShowSendGameInviteUi;

    internal static IntPtr GameUiShowAchievementsCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, IntPtr, uint, IntPtr, void>)
            &OnShowAchievementsUi;

    internal static IntPtr GameUiShowMultiplayerActivityGameInviteCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, IntPtr, IntPtr, void>)
            &OnShowMultiplayerActivityGameInviteUi;

    internal static IntPtr GameUiShowMessageDialogCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<
            IntPtr, IntPtr, byte*, byte*, byte*, byte*, byte*,
            XGameUiMessageDialogButton, XGameUiMessageDialogButton, IntPtr, void>)
            &OnShowMessageDialogUi;

    internal static IntPtr GameUiShowErrorDialogCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, int, byte*, IntPtr, void>)
            &OnShowErrorDialogUi;

    internal static IntPtr GameUiShowTextEntryCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<
            IntPtr, IntPtr, byte*, byte*, byte*, XGameUiTextEntryInputScope, uint, IntPtr, void>)
            &OnShowTextEntryUi;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnShowPlayerProfileCardUi(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, ulong targetPlayer, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchPlayerProfileCard(callbackHandle, queue, requestingUser, targetPlayer);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnShowPlayerPickerUi(
        IntPtr callbackHandle, IntPtr queue, XGameUiPlayerPickerInfo* info, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchPlayerPicker(callbackHandle, queue, info);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnShowSendGameInviteUi(
        IntPtr callbackHandle,
        IntPtr queue,
        IntPtr requestingUser,
        byte* sessionConfigurationId,
        byte* sessionTemplateName,
        byte* sessionId,
        byte* invitationText,
        byte* customActivationContext,
        IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchSendGameInvite(
                callbackHandle, queue, requestingUser, sessionConfigurationId,
                sessionTemplateName, sessionId, invitationText, customActivationContext);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnShowAchievementsUi(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, uint titleId, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchAchievements(callbackHandle, queue, requestingUser, titleId);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnShowMultiplayerActivityGameInviteUi(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchMultiplayerActivityGameInvite(callbackHandle, queue, requestingUser);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnShowMessageDialogUi(
        IntPtr callbackHandle,
        IntPtr queue,
        byte* titleText,
        byte* contentText,
        byte* firstButtonText,
        byte* secondButtonText,
        byte* thirdButtonText,
        XGameUiMessageDialogButton defaultButton,
        XGameUiMessageDialogButton cancelButton,
        IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchMessageDialog(
                callbackHandle, queue, titleText, contentText, firstButtonText,
                secondButtonText, thirdButtonText, defaultButton, cancelButton);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnShowErrorDialogUi(
        IntPtr callbackHandle, IntPtr queue, int errorCode, byte* serviceContext, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchErrorDialog(callbackHandle, queue, errorCode, serviceContext);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnShowTextEntryUi(
        IntPtr callbackHandle,
        IntPtr queue,
        byte* titleText,
        byte* descriptionText,
        byte* defaultText,
        XGameUiTextEntryInputScope inputScope,
        uint maxTextLength,
        IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchTextEntry(
                callbackHandle, queue, titleText, descriptionText, defaultText,
                inputScope, maxTextLength);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void ShowPlayerProfileCardUiDelegate(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, ulong targetPlayer, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private unsafe delegate void ShowPlayerPickerUiDelegate(
        IntPtr callbackHandle, IntPtr queue, XGameUiPlayerPickerInfo* info, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private unsafe delegate void ShowSendGameInviteUiDelegate(
        IntPtr callbackHandle,
        IntPtr queue,
        IntPtr requestingUser,
        byte* sessionConfigurationId,
        byte* sessionTemplateName,
        byte* sessionId,
        byte* invitationText,
        byte* customActivationContext,
        IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void ShowAchievementsUiDelegate(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, uint titleId, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void ShowMultiplayerActivityGameInviteUiDelegate(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private unsafe delegate void ShowMessageDialogUiDelegate(
        IntPtr callbackHandle,
        IntPtr queue,
        byte* titleText,
        byte* contentText,
        byte* firstButtonText,
        byte* secondButtonText,
        byte* thirdButtonText,
        XGameUiMessageDialogButton defaultButton,
        XGameUiMessageDialogButton cancelButton,
        IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private unsafe delegate void ShowErrorDialogUiDelegate(
        IntPtr callbackHandle, IntPtr queue, int errorCode, byte* serviceContext, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private unsafe delegate void ShowTextEntryUiDelegate(
        IntPtr callbackHandle,
        IntPtr queue,
        byte* titleText,
        byte* descriptionText,
        byte* defaultText,
        XGameUiTextEntryInputScope inputScope,
        uint maxTextLength,
        IntPtr context);

    // Rooted for the process lifetime: the Gaming Runtime keeps these function pointers until the
    // registration is replaced, and there is no point at which it is safe to let them be collected.
    private static readonly ShowPlayerProfileCardUiDelegate ShowPlayerProfileCardKeepAlive =
        OnShowPlayerProfileCardUi;
    private static readonly ShowPlayerPickerUiDelegate ShowPlayerPickerKeepAlive =
        OnShowPlayerPickerUi;
    private static readonly ShowSendGameInviteUiDelegate ShowSendGameInviteKeepAlive =
        OnShowSendGameInviteUi;
    private static readonly ShowAchievementsUiDelegate ShowAchievementsKeepAlive =
        OnShowAchievementsUi;
    private static readonly ShowMultiplayerActivityGameInviteUiDelegate
        ShowMultiplayerActivityGameInviteKeepAlive = OnShowMultiplayerActivityGameInviteUi;
    private static readonly ShowMessageDialogUiDelegate ShowMessageDialogKeepAlive =
        OnShowMessageDialogUi;
    private static readonly ShowErrorDialogUiDelegate ShowErrorDialogKeepAlive =
        OnShowErrorDialogUi;
    private static readonly ShowTextEntryUiDelegate ShowTextEntryKeepAlive =
        OnShowTextEntryUi;

    internal static IntPtr GameUiShowPlayerProfileCardCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ShowPlayerProfileCardKeepAlive);

    internal static IntPtr GameUiShowPlayerPickerCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ShowPlayerPickerKeepAlive);

    internal static IntPtr GameUiShowSendGameInviteCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ShowSendGameInviteKeepAlive);

    internal static IntPtr GameUiShowAchievementsCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ShowAchievementsKeepAlive);

    internal static IntPtr GameUiShowMultiplayerActivityGameInviteCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ShowMultiplayerActivityGameInviteKeepAlive);

    internal static IntPtr GameUiShowMessageDialogCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ShowMessageDialogKeepAlive);

    internal static IntPtr GameUiShowErrorDialogCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ShowErrorDialogKeepAlive);

    internal static IntPtr GameUiShowTextEntryCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(ShowTextEntryKeepAlive);

    private static void OnShowPlayerProfileCardUi(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, ulong targetPlayer, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchPlayerProfileCard(callbackHandle, queue, requestingUser, targetPlayer);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static unsafe void OnShowPlayerPickerUi(
        IntPtr callbackHandle, IntPtr queue, XGameUiPlayerPickerInfo* info, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchPlayerPicker(callbackHandle, queue, info);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static unsafe void OnShowSendGameInviteUi(
        IntPtr callbackHandle,
        IntPtr queue,
        IntPtr requestingUser,
        byte* sessionConfigurationId,
        byte* sessionTemplateName,
        byte* sessionId,
        byte* invitationText,
        byte* customActivationContext,
        IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchSendGameInvite(
                callbackHandle, queue, requestingUser, sessionConfigurationId,
                sessionTemplateName, sessionId, invitationText, customActivationContext);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void OnShowAchievementsUi(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, uint titleId, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchAchievements(callbackHandle, queue, requestingUser, titleId);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void OnShowMultiplayerActivityGameInviteUi(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchMultiplayerActivityGameInvite(callbackHandle, queue, requestingUser);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static unsafe void OnShowMessageDialogUi(
        IntPtr callbackHandle,
        IntPtr queue,
        byte* titleText,
        byte* contentText,
        byte* firstButtonText,
        byte* secondButtonText,
        byte* thirdButtonText,
        XGameUiMessageDialogButton defaultButton,
        XGameUiMessageDialogButton cancelButton,
        IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchMessageDialog(
                callbackHandle, queue, titleText, contentText, firstButtonText,
                secondButtonText, thirdButtonText, defaultButton, cancelButton);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static unsafe void OnShowErrorDialogUi(
        IntPtr callbackHandle, IntPtr queue, int errorCode, byte* serviceContext, IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchErrorDialog(callbackHandle, queue, errorCode, serviceContext);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static unsafe void OnShowTextEntryUi(
        IntPtr callbackHandle,
        IntPtr queue,
        byte* titleText,
        byte* descriptionText,
        byte* defaultText,
        XGameUiTextEntryInputScope inputScope,
        uint maxTextLength,
        IntPtr context)
    {
        try
        {
            CustomGameUi.DispatchTextEntry(
                callbackHandle, queue, titleText, descriptionText, defaultText,
                inputScope, maxTextLength);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
