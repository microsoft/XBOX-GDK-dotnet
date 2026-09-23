using System;
using GDK.Net.Interop;

namespace GDK.Net.Users;

/// <summary>
/// The <c>XUserPlatform</c> family: lets the title draw the Gaming Runtime's sign-in prompts itself
/// instead of using the system UI — the remote-connect prompt ("open this URL on another device")
/// and the SPOP prompt ("this account is already signed in somewhere else").
/// </summary>
/// <remarks>
/// <para>
/// This type is static because the underlying <c>XUserPlatform*</c> APIs install <b>process-global</b>
/// handler tables rather than returning a per-registration token like every other event source in
/// this projection. There is consequently no way to uninstall them: the native handlers are set on
/// the first subscription and stay for the lifetime of the process. Removing the last managed
/// subscriber simply means the thunk has nothing to call.
/// </para>
/// <para>
/// A title that opts in takes on a contract: it <b>must</b> resolve every prompt it is shown.
/// Ignoring a <see cref="SpopPrompt"/> event leaves the sign-in hanging forever, because
/// the runtime waits for <see cref="SpopPromptEventArgs.Complete"/>.
/// </para>
/// <para>
/// The events are named for the native handlers they replace:
/// <see cref="RemoteConnectShowPrompt"/> for <c>XUserPlatformRemoteConnectShowPromptEventHandler</c>,
/// <see cref="RemoteConnectClosePrompt"/> for its <c>ClosePrompt</c> sibling, and
/// <see cref="SpopPrompt"/> for <c>XUserPlatformSpopPromptEventHandler</c>. They keep the header's
/// imperative wording rather than the past tense a .NET event usually takes, because that wording is
/// the contract: the title is being told to show or close a prompt, not notified that something
/// happened.
/// </para>
/// </remarks>
public static unsafe class UserPlatform
{
    private static readonly object Gate = new();

    private static EventHandler<RemoteConnectShowPromptEventArgs>? RemoteConnectRequested;
    private static EventHandler<RemoteConnectClosePromptEventArgs>? RemoteConnectClosed;
    private static EventHandler<SpopPromptEventArgs>? SpopRequested;

    private static bool _remoteConnectInstalled;
    private static bool _spopInstalled;

    /// <summary>
    /// Raised when the runtime wants the title to put up a remote-connect prompt.
    /// </summary>
    /// <remarks>
    /// Installing the native handler table also installs the one behind
    /// <see cref="RemoteConnectClosePrompt"/>; the two are a single native registration and cannot
    /// be subscribed independently at the native layer.
    /// </remarks>
    public static event EventHandler<RemoteConnectShowPromptEventArgs>? RemoteConnectShowPrompt
    {
        add
        {
            lock (Gate)
            {
                RemoteConnectRequested += value;
                InstallRemoteConnectHandlers();
            }
        }

        remove
        {
            lock (Gate)
            {
                RemoteConnectRequested -= value;
            }
        }
    }

    /// <summary>
    /// Raised when the runtime is finished with a remote-connect prompt and the title should take it
    /// down. Use <see cref="RemoteConnectClosePromptEventArgs.Matches"/> to pair it with the event
    /// that opened the prompt.
    /// </summary>
    public static event EventHandler<RemoteConnectClosePromptEventArgs>? RemoteConnectClosePrompt
    {
        add
        {
            lock (Gate)
            {
                RemoteConnectClosed += value;
                InstallRemoteConnectHandlers();
            }
        }

        remove
        {
            lock (Gate)
            {
                RemoteConnectClosed -= value;
            }
        }
    }

    /// <summary>
    /// Raised when the runtime wants the title to ask the user how to resolve an account that is
    /// already signed in on another device.
    /// </summary>
    /// <remarks>
    /// The handler must call <see cref="SpopPromptEventArgs.Complete"/> exactly once, on every path.
    /// </remarks>
    public static event EventHandler<SpopPromptEventArgs>? SpopPrompt
    {
        add
        {
            lock (Gate)
            {
                SpopRequested += value;
                if (!_spopInstalled)
                {
                    Hr.ThrowIfFailed(Native.XUserPlatformSpopPromptSetEventHandlers(
                        Queue, Trampolines.SpopPromptCallback, IntPtr.Zero));
                    _spopInstalled = true;
                }
            }
        }

        remove
        {
            lock (Gate)
            {
                SpopRequested -= value;
            }
        }
    }

    /// <summary>
    /// The task queue prompts are delivered on. Always <see langword="null"/> in normal use, so
    /// callbacks resolve the process default queue.
    /// </summary>
    /// <remarks>
    /// The native handler tables capture the queue when they are installed, so changing this after
    /// the first subscription has no effect.
    /// </remarks>
    internal static GameTaskQueue? TaskQueue { get; set; }

    private static IntPtr Queue => TaskQueue.RawHandle();

    internal static void CancelRemoteConnect(IntPtr operation)
    {
        Hr.ThrowIfFailed(Native.XUserPlatformRemoteConnectCancelPrompt(operation));
    }

    internal static void CompleteSpop(IntPtr operation, SpopOperationResult result)
    {
        Hr.ThrowIfFailed(Native.XUserPlatformSpopPromptComplete(
            operation, (XUserPlatformSpopOperationResult)result));
    }

    internal static void DispatchRemoteConnectShow(
        uint userIdentifier,
        IntPtr operation,
        byte* url,
        byte* code,
        nuint qrCodeSize,
        byte* qrCode)
    {
        EventHandler<RemoteConnectShowPromptEventArgs>? handler;
        lock (Gate)
        {
            handler = RemoteConnectRequested;
        }

        if (handler is null)
        {
            return;
        }

        // The QR buffer only lives for the duration of the callback, so copy it out before raising.
        byte[] qr;
        if (qrCode is null || qrCodeSize == 0)
        {
            qr = Array.Empty<byte>();
        }
        else
        {
            qr = new byte[(int)qrCodeSize];
            for (int i = 0; i < qr.Length; i++)
            {
                qr[i] = qrCode[i];
            }
        }

        handler(
            null,
            new RemoteConnectShowPromptEventArgs(
                userIdentifier,
                operation,
                Utf8.ToString(url) ?? string.Empty,
                Utf8.ToString(code) ?? string.Empty,
                qr));
    }

    internal static void DispatchRemoteConnectClose(uint userIdentifier, IntPtr operation)
    {
        EventHandler<RemoteConnectClosePromptEventArgs>? handler;
        lock (Gate)
        {
            handler = RemoteConnectClosed;
        }

        handler?.Invoke(null, new RemoteConnectClosePromptEventArgs(userIdentifier, operation));
    }

    internal static void DispatchSpopPrompt(
        uint userIdentifier,
        IntPtr operation,
        byte* modernGamertag,
        byte* modernGamertagSuffix)
    {
        EventHandler<SpopPromptEventArgs>? handler;
        lock (Gate)
        {
            handler = SpopRequested;
        }

        handler?.Invoke(
            null,
            new SpopPromptEventArgs(
                userIdentifier,
                operation,
                Utf8.ToString(modernGamertag) ?? string.Empty,
                Utf8.ToString(modernGamertagSuffix)));
    }

    private static void InstallRemoteConnectHandlers()
    {
        if (_remoteConnectInstalled)
        {
            return;
        }

        var handlers = new XUserPlatformRemoteConnectEventHandlers
        {
            Show = Trampolines.RemoteConnectShowPromptCallback,
            Close = Trampolines.RemoteConnectClosePromptCallback,
            Context = IntPtr.Zero,
        };

        Hr.ThrowIfFailed(Native.XUserPlatformRemoteConnectSetEventHandlers(Queue, &handlers));
        _remoteConnectInstalled = true;
    }
}
