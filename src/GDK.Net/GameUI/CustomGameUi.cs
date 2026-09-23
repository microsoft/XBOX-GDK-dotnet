using System;
using System.Runtime.InteropServices;
using System.Threading;
using GDK.Net.Interop;

namespace GDK.Net.GameUI;

/// <summary>
/// Lets a title render the Gaming Runtime's UI itself instead of letting the system draw it
/// (<c>XGameUiSetUiCallbacks</c> and the eight <c>XGameUiSet*UiResponse</c> completions).
/// </summary>
/// <remarks>
/// <para>
/// By default, calls such as <see cref="GameUiManager.ShowMessageDialogAsync"/> render system UI.
/// After <see cref="SetHandlers"/>, those same calls instead invoke the matching handler with a
/// <see cref="GameUiRequest"/>; the title draws its own UI and answers by calling
/// <c>Respond</c> on the request, which completes the original operation. This is how a title keeps
/// a consistent visual style, and on platforms with no system UI it is the only way these APIs
/// work at all.
/// </para>
/// <para>
/// Handlers do not run on the Gaming Runtime's callback thread. The runtime raises these callbacks
/// on the work port of the task queue driving the originating operation, and anything done before
/// returning from the callback occupies that port -- so this projection copies the request payload,
/// hands the request to the thread pool, and returns immediately. A handler is therefore free to
/// block, to <c>await</c>, and to await further Gaming Runtime operations on that same queue
/// without deadlocking.
/// </para>
/// <para>
/// The consequence is that handlers run on a thread pool thread with no synchronization context. A
/// title that must touch its renderer will need to marshal to its own thread, exactly as it would
/// for any other background callback. Responding is safe from any thread at any time, and need not
/// happen inside the handler -- capturing the request and answering frames later is the expected
/// pattern.
/// </para>
/// <para>
/// A handler that throws will not take the process down, and if it throws before responding the
/// projection answers on its behalf with the neutral response for that request kind, so a title bug
/// cannot leave the caller's <c>XGameUiShow*Async</c> operation pending forever.
/// </para>
/// <para>
/// The registration is process-wide and last-writer-wins, mirroring the native API. Only handlers
/// that are non-null are registered, so the system keeps ownership of any UI the title does not
/// implement.
/// </para>
/// </remarks>
public static unsafe class CustomGameUi
{
    private static readonly object Gate = new object();

    // The native table is allocated once and never freed. XGameUiSetUiCallbacks is documented as
    // taking a `const XGameUiUiCallbacks*` but says nothing about whether the runtime copies it, so
    // the safe reading is that the pointer may be retained. A single process-lifetime allocation
    // satisfies both readings and costs one small block.
    private static IntPtr _nativeCallbacks;

    private static CustomGameUiHandlers? _handlers;

    /// <summary>
    /// The handler set currently registered, or <see langword="null"/> when the system is drawing
    /// all UI.
    /// </summary>
    public static CustomGameUiHandlers? Handlers
    {
        get
        {
            lock (Gate)
            {
                return _handlers;
            }
        }
    }

    /// <summary>
    /// Registers the title's UI handlers (<c>XGameUiSetUiCallbacks</c>), replacing any previous
    /// registration.
    /// </summary>
    /// <param name="handlers">
    /// The UI requests the title will render. Handlers left <see langword="null"/> stay with the
    /// system.
    /// </param>
    /// <param name="useSystemUiIfAvailable">
    /// When <see langword="true"/>, the runtime prefers its own UI wherever it has one and only
    /// falls back to the title's handlers when it does not. When <see langword="false"/>, the
    /// title's handlers always win.
    /// </param>
    /// <remarks>
    /// Must not be called from a thread marked time-sensitive: the native entry point asserts
    /// against that internally (<c>XThreadAssertNotTimeSensitive</c>).
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="handlers"/> is null.</exception>
    public static void SetHandlers(CustomGameUiHandlers handlers, bool useSystemUiIfAvailable = false)
    {
        if (handlers is null)
        {
            throw new ArgumentNullException(nameof(handlers));
        }

        lock (Gate)
        {
            XGameUiUiCallbacks table = default;
            table.context = IntPtr.Zero;

            // A null function pointer tells the runtime the title does not implement that UI, so
            // the table mirrors exactly which handlers the caller supplied.
            if (handlers.PlayerProfileCard != null)
            {
                table.showPlayerProfileCardCallback = Trampolines.GameUiShowPlayerProfileCardCallback;
            }

            if (handlers.PlayerPicker != null)
            {
                table.showPlayerPickerCallback = Trampolines.GameUiShowPlayerPickerCallback;
            }

            if (handlers.SendGameInvite != null)
            {
                table.showSendGameInviteCallback = Trampolines.GameUiShowSendGameInviteCallback;
            }

            if (handlers.Achievements != null)
            {
                table.showAchievementsCallback = Trampolines.GameUiShowAchievementsCallback;
            }

            if (handlers.MultiplayerActivityGameInvite != null)
            {
                table.showMultiplayerActivityGameInviteCallback =
                    Trampolines.GameUiShowMultiplayerActivityGameInviteCallback;
            }

            if (handlers.MessageDialog != null)
            {
                table.showMessageDialogCallback = Trampolines.GameUiShowMessageDialogCallback;
            }

            if (handlers.ErrorDialog != null)
            {
                table.showErrorDialogCallback = Trampolines.GameUiShowErrorDialogCallback;
            }

            if (handlers.TextEntry != null)
            {
                table.showTextEntryCallback = Trampolines.GameUiShowTextEntryCallback;
            }

            // Publish the handlers before the runtime can invoke them.
            _handlers = handlers;

            IntPtr storage = EnsureNativeStorage();
            *(XGameUiUiCallbacks*)storage = table;

            try
            {
                Hr.ThrowIfFailed(Native.XGameUiSetUiCallbacks(
                    (XGameUiUiCallbacks*)storage,
                    useSystemUiIfAvailable ? (byte)1 : (byte)0));
            }
            catch
            {
                _handlers = null;
                throw;
            }
        }
    }

    /// <summary>
    /// Clears the registration so the system draws all UI again (<c>XGameUiSetUiCallbacks</c> with
    /// a table whose every function pointer is null).
    /// </summary>
    /// <remarks>
    /// Note that this passes a zeroed table rather than a null pointer. The native entry point
    /// dereferences the table unconditionally -- passing <c>nullptr</c> access-violates inside the
    /// Gaming Runtime and takes the process down. "The title implements nothing" is expressed the
    /// same way as "the title implements only some of these": by leaving slots null.
    /// </remarks>
    public static void ClearHandlers()
    {
        lock (Gate)
        {
            IntPtr storage = EnsureNativeStorage();
            *(XGameUiUiCallbacks*)storage = default;

            Hr.ThrowIfFailed(Native.XGameUiSetUiCallbacks((XGameUiUiCallbacks*)storage, 1));
            _handlers = null;
        }
    }

    /// <summary>
    /// Takes an owning copy of one of the borrowed <c>XUserHandle</c> values carried on a request
    /// (<c>XUserDuplicateHandle</c>), so it can outlive the handler that received it.
    /// </summary>
    /// <param name="userHandle">
    /// A handle from a request's <c>RequestingUserHandle</c> property.
    /// </param>
    /// <returns>
    /// A duplicated handle the caller owns and must eventually close, or <see cref="IntPtr.Zero"/>
    /// when <paramref name="userHandle"/> was <see cref="IntPtr.Zero"/>.
    /// </returns>
    public static IntPtr DuplicateUser(IntPtr userHandle)
    {
        if (userHandle == IntPtr.Zero)
        {
            return IntPtr.Zero;
        }

        IntPtr duplicate;
        Hr.ThrowIfFailed(Native.XUserDuplicateHandle(userHandle, &duplicate));
        return duplicate;
    }

    /// <summary>
    /// Publishes a handler set without calling the native registration API, so the dispatch path
    /// can be exercised without a Gaming Runtime. Test hook only; titles use
    /// <see cref="SetHandlers"/>.
    /// </summary>
    internal static void SetHandlersWithoutRegistering(CustomGameUiHandlers? handlers)
    {
        lock (Gate)
        {
            _handlers = handlers;
        }
    }

    private static IntPtr EnsureNativeStorage()    {
        if (_nativeCallbacks == IntPtr.Zero)
        {
            _nativeCallbacks = Marshal.AllocHGlobal(sizeof(XGameUiUiCallbacks));
        }

        return _nativeCallbacks;
    }

    // ─── Dispatch from Trampolines.GameUI.cs ─────────────────────────────────────
    //
    // TWO THINGS HAPPEN HERE, AND THE ORDER MATTERS.
    //
    // 1. Copy. Every runtime-owned string, array and scalar is copied out while the native
    //    callback frame is still live, because the pointers the runtime hands us are only valid
    //    until we return from it. That is what makes a request a self-contained value.
    //
    // 2. Defer. The request is then handed to the thread pool and this method returns immediately,
    //    releasing the runtime's callback thread.
    //
    // Step 2 is not an optimisation, it is a correctness requirement. The native callback signature
    // passes an XTaskQueueHandle precisely because the runtime is invoking us on that queue's work
    // port and expects the title to get off it. Anything done before returning -- the title's
    // handler, an await continuation that resumes inline, and above all a Respond call that
    // re-enters the Gaming Runtime while it is still inside its own callback -- occupies that port
    // for the duration. A title that awaits another operation on the same queue from inside a
    // handler would then deadlock against itself, and even one that does not still starves every
    // other callback queued behind it.
    //
    // Because step 1 already made the request independent of the callback frame, deferring is free:
    // there is nothing left that has to happen before the frame unwinds. The queue handle itself is
    // deliberately not surfaced on the request -- it is borrowed, it would dangle the moment we
    // return, and it is the one queue a handler must not schedule onto.
    //
    // A missing handler is not an error worth throwing over: the runtime should not have called a
    // slot we left null, but responding neutrally is far better than leaving the caller's async
    // operation pending forever.

    private static void Post<TRequest>(TRequest request, Action<TRequest>? handler)
        where TRequest : GameUiRequest
    {
        ThreadPool.QueueUserWorkItem(
            static state =>
            {
                var work = (Tuple<TRequest, Action<TRequest>?>)state!;
                Invoke(work.Item1, work.Item2);
            },
            Tuple.Create(request, handler));
    }

    private static void Invoke<TRequest>(TRequest request, Action<TRequest>? handler)
        where TRequest : GameUiRequest
    {
        try
        {
            if (handler is null)
            {
                request.RespondNeutral();
                return;
            }

            handler(request);
        }
        catch
        {
            // We are on a thread-pool thread now, so an escaping exception would tear the process
            // down. Swallow it, but do not let the title's bug also wedge the runtime: if the
            // handler failed before answering, answer for it.
            try
            {
                if (!request.HasResponded)
                {
                    request.RespondNeutral();
                }
            }
            catch
            {
                // Nothing further can be done; the operation stays pending.
            }
        }
    }

    internal static void DispatchPlayerProfileCard(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, ulong targetPlayer)
    {
        Post(
            new PlayerProfileCardUiRequest(callbackHandle, requestingUser, targetPlayer),
            Handlers?.PlayerProfileCard);
    }

    internal static void DispatchPlayerPicker(
        IntPtr callbackHandle, IntPtr queue, XGameUiPlayerPickerInfo* info)
    {
        IntPtr requestingUser = IntPtr.Zero;
        string? promptText = null;
        ulong[] selectFrom = Array.Empty<ulong>();
        ulong[] preSelected = Array.Empty<ulong>();
        uint min = 0;
        uint max = 0;

        if (info != null)
        {
            requestingUser = info->requestingUser;
            promptText = Utf8.ToString(info->promptText);
            selectFrom = CopyPlayers(info->selectFromPlayers, info->selectFromPlayersCount);
            preSelected = CopyPlayers(info->preSelectedPlayers, info->preSelectedPlayersCount);
            min = info->minSelectionCount;
            max = info->maxSelectionCount;
        }

        Post(
            new PlayerPickerUiRequest(
                callbackHandle, requestingUser, promptText, selectFrom, preSelected, min, max),
            Handlers?.PlayerPicker);
    }

    internal static void DispatchSendGameInvite(
        IntPtr callbackHandle,
        IntPtr queue,
        IntPtr requestingUser,
        byte* sessionConfigurationId,
        byte* sessionTemplateName,
        byte* sessionId,
        byte* invitationText,
        byte* customActivationContext)
    {
        Post(
            new SendGameInviteUiRequest(
                callbackHandle,
                requestingUser,
                Utf8.ToString(sessionConfigurationId),
                Utf8.ToString(sessionTemplateName),
                Utf8.ToString(sessionId),
                Utf8.ToString(invitationText),
                Utf8.ToString(customActivationContext)),
            Handlers?.SendGameInvite);
    }

    internal static void DispatchAchievements(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser, uint titleId)
    {
        Post(
            new AchievementsUiRequest(callbackHandle, requestingUser, titleId),
            Handlers?.Achievements);
    }

    internal static void DispatchMultiplayerActivityGameInvite(
        IntPtr callbackHandle, IntPtr queue, IntPtr requestingUser)
    {
        Post(
            new MultiplayerActivityGameInviteUiRequest(callbackHandle, requestingUser),
            Handlers?.MultiplayerActivityGameInvite);
    }

    internal static void DispatchMessageDialog(
        IntPtr callbackHandle,
        IntPtr queue,
        byte* titleText,
        byte* contentText,
        byte* firstButtonText,
        byte* secondButtonText,
        byte* thirdButtonText,
        XGameUiMessageDialogButton defaultButton,
        XGameUiMessageDialogButton cancelButton)
    {
        Post(
            new MessageDialogUiRequest(
                callbackHandle,
                Utf8.ToString(titleText),
                Utf8.ToString(contentText),
                Utf8.ToString(firstButtonText),
                Utf8.ToString(secondButtonText),
                Utf8.ToString(thirdButtonText),
                (MessageDialogButton)defaultButton,
                (MessageDialogButton)cancelButton),
            Handlers?.MessageDialog);
    }

    internal static void DispatchErrorDialog(
        IntPtr callbackHandle, IntPtr queue, int errorCode, byte* serviceContext)
    {
        Post(
            new ErrorDialogUiRequest(callbackHandle, errorCode, Utf8.ToString(serviceContext)),
            Handlers?.ErrorDialog);
    }

    internal static void DispatchTextEntry(
        IntPtr callbackHandle,
        IntPtr queue,
        byte* titleText,
        byte* descriptionText,
        byte* defaultText,
        XGameUiTextEntryInputScope inputScope,
        uint maxTextLength)
    {
        Post(
            new TextEntryUiRequest(
                callbackHandle,
                Utf8.ToString(titleText),
                Utf8.ToString(descriptionText),
                Utf8.ToString(defaultText),
                (TextEntryInputScope)inputScope,
                maxTextLength),
            Handlers?.TextEntry);
    }

    private static ulong[] CopyPlayers(ulong* players, uint count)
    {
        if (players == null || count == 0)
        {
            return Array.Empty<ulong>();
        }

        ulong[] copy = new ulong[count];
        for (uint i = 0; i < count; i++)
        {
            copy[i] = players[i];
        }

        return copy;
    }
}