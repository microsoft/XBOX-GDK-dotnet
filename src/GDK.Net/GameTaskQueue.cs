using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using GDK.Net.Interop;
using Microsoft.Win32.SafeHandles;

namespace GDK.Net;

/// <summary>
/// How a task queue's callbacks are scheduled. Mirrors <c>XTaskQueueDispatchMode</c>.
/// </summary>
internal enum GameTaskQueueDispatchMode
{
    /// <summary>Callbacks run only when <see cref="GameTaskQueue.DispatchCompletions"/> is called.</summary>
    Manual = 0,

    /// <summary>Callbacks are queued to the system thread pool and may run concurrently.</summary>
    ThreadPool = 1,

    /// <summary>Callbacks are queued to the system thread pool but run one at a time.</summary>
    SerializedThreadPool = 2,

    /// <summary>Callbacks run inline on the thread that submits them.</summary>
    Immediate = 3,
}

/// <summary>
/// Which port of a task queue to target. Mirrors <c>XTaskQueuePort</c> from XTaskQueue.h.
/// </summary>
internal enum GameTaskQueuePortKind
{
    /// <summary>The work port; callbacks submitted here run when the queue dispatches work.</summary>
    Work = 0,

    /// <summary>The completion port; callbacks here signal async-operation completion.</summary>
    Completion = 1,
}

/// <summary>
/// An opaque handle to one port of a <see cref="GameTaskQueue"/>, as returned by
/// <see cref="GameTaskQueue.GetPort"/>.
/// </summary>
/// <remarks>
/// Port handles are <b>owned by their parent queue</b> and must NOT be closed or disposed
/// independently. They remain valid until the queue itself is disposed.
/// </remarks>
internal readonly struct GameTaskQueuePort
{
    internal IntPtr Handle { get; }

    internal GameTaskQueuePort(IntPtr handle) => Handle = handle;

    /// <summary><see langword="true"/> when the underlying handle is non-null.</summary>
    public bool IsValid => Handle != IntPtr.Zero;
}

/// <summary>
/// Payload delivered when any callback is submitted to a monitored task queue.
/// </summary>
internal sealed class GameTaskQueueMonitorEventArgs : EventArgs
{
    internal GameTaskQueueMonitorEventArgs(GameTaskQueuePortKind port) => Port = port;

    /// <summary>The port to which a callback was just submitted.</summary>
    public GameTaskQueuePortKind Port { get; }
}

/// <summary>
/// A GDK task queue: the scheduler behind every async call and event registration in this
/// projection.
/// </summary>
/// <remarks>
/// <para>
/// <b>Deliberately internal.</b> Titles cannot create, name or pump a queue, and every operation
/// leaves <c>XAsyncBlock::queue</c> null so the Gaming Runtime resolves the process default at call
/// time. The projection therefore never creates a queue and never owns one.
/// </para>
/// <para>
/// The reason is that the projection already has to move callbacks off the queue's work port to
/// stay deadlock-free — see <see cref="GameUI.CustomGameUi"/>, where a handler that re-entered the
/// runtime on the same queue would block against itself. Once callbacks are hopped to the thread
/// pool, a manual completion port no longer buys the ordering guarantee it exists for, and multiple
/// queues buy nothing at all. Exposing them would have cost titles real complexity in exchange for
/// a benefit the design had already forfeited.
/// </para>
/// <para>
/// The type is kept rather than deleted because the <c>XTaskQueue</c> bindings underneath it are
/// still part of the projected surface, still covered by tests, and are what a future decision to
/// reintroduce pumping would build on.
/// </para>
/// </remarks>
internal sealed class GameTaskQueue : IDisposable
{
    // Typed as SafeHandleZeroOrMinusOneIsInvalid so we can store either a full-owning
    // TaskQueueHandle (which terminates+closes on release) or a TaskQueueBorrowedHandle
    // (close-only, used for externally-owned handles such as the process-wide default queue).
    private readonly SafeHandleZeroOrMinusOneIsInvalid _handle;
    private bool _disposed;

    private GameTaskQueue(
        SafeHandleZeroOrMinusOneIsInvalid handle,
        GameTaskQueueDispatchMode workDispatchMode,
        GameTaskQueueDispatchMode completionDispatchMode,
        bool isComposite = false)
    {
        _handle = handle;
        WorkDispatchMode = workDispatchMode;
        CompletionDispatchMode = completionDispatchMode;
        IsComposite = isComposite;
    }

    /// <summary>How work callbacks are scheduled.</summary>
    /// <remarks>
    /// Not meaningful when <see cref="IsComposite"/> is <see langword="true"/>; the actual dispatch
    /// mode is inherited from the source port but cannot be queried through this API.
    /// </remarks>
    public GameTaskQueueDispatchMode WorkDispatchMode { get; }

    /// <summary>How completion callbacks are scheduled.</summary>
    /// <remarks>
    /// Not meaningful when <see cref="IsComposite"/> is <see langword="true"/>; the actual dispatch
    /// mode is inherited from the source port but cannot be queried through this API.
    /// </remarks>
    public GameTaskQueueDispatchMode CompletionDispatchMode { get; }

    /// <summary>
    /// <see langword="true"/> when completions must be drained manually by
    /// <see cref="DispatchCompletions"/>.
    /// </summary>
    /// <remarks>Not meaningful when <see cref="IsComposite"/> is <see langword="true"/>.</remarks>
    public bool IsPumped => CompletionDispatchMode == GameTaskQueueDispatchMode.Manual;

    /// <summary>
    /// <see langword="true"/> when this queue was built from existing port handles via
    /// <see cref="CreateComposite"/>, or was obtained from an external source such as
    /// <see cref="ProcessDefault"/>. When <see langword="true"/>, <see cref="WorkDispatchMode"/>
    /// and <see cref="CompletionDispatchMode"/> reflect the <see cref="GameTaskQueueDispatchMode.Manual"/>
    /// default rather than the actual dispatch modes, which cannot be determined through this API.
    /// </summary>
    public bool IsComposite { get; }

    internal IntPtr Handle
    {
        get
        {
            ThrowIfDisposed();
            return _handle.DangerousGetHandle();
        }
    }

    // ─── Factory methods ──────────────────────────────────────────────────────────

    /// <summary>Creates a queue using <paramref name="dispatchMode"/> for both ports.</summary>
    public static GameTaskQueue Create(
        GameTaskQueueDispatchMode dispatchMode = GameTaskQueueDispatchMode.ThreadPool)
        => Create(dispatchMode, dispatchMode);

    /// <summary>Creates a queue with independent work and completion dispatch modes.</summary>
    public static unsafe GameTaskQueue Create(
        GameTaskQueueDispatchMode workDispatchMode,
        GameTaskQueueDispatchMode completionDispatchMode)
    {
        IntPtr raw;
        int hr = Native.XTaskQueueCreate(
            (XTaskQueueDispatchMode)workDispatchMode,
            (XTaskQueueDispatchMode)completionDispatchMode,
            &raw);
        Hr.ThrowIfFailed(hr);

        return new GameTaskQueue(new TaskQueueHandle(raw), workDispatchMode, completionDispatchMode);
    }

    /// <summary>
    /// Creates a composite queue whose work and completion ports are taken from two existing port
    /// handles (<c>XTaskQueueCreateComposite</c>).
    /// </summary>
    /// <param name="workPort">The port to use for work callbacks.</param>
    /// <param name="completionPort">The port to use for completion callbacks.</param>
    /// <returns>
    /// A new <see cref="GameTaskQueue"/> with <see cref="IsComposite"/> set to
    /// <see langword="true"/>. Terminating the composite queue does NOT terminate the source queues
    /// that supplied the ports.
    /// </returns>
    public static unsafe GameTaskQueue CreateComposite(
        GameTaskQueuePort workPort,
        GameTaskQueuePort completionPort)
    {
        IntPtr raw;
        int hr = Native.XTaskQueueCreateComposite(workPort.Handle, completionPort.Handle, &raw);
        Hr.ThrowIfFailed(hr);

        return new GameTaskQueue(new TaskQueueHandle(raw), default, default, isComposite: true);
    }

    // ─── Port access ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the <see cref="GameTaskQueuePort"/> handle for <paramref name="port"/>
    /// (<c>XTaskQueueGetPort</c>).
    /// </summary>
    /// <remarks>
    /// The returned port handle is owned by this queue. Do NOT close or dispose it independently.
    /// It remains valid until the queue itself is disposed.
    /// </remarks>
    public unsafe GameTaskQueuePort GetPort(GameTaskQueuePortKind port)
    {
        IntPtr portHandle;
        int hr = Native.XTaskQueueGetPort(Handle, (XTaskQueuePort)port, &portHandle);
        Hr.ThrowIfFailed(hr);
        return new GameTaskQueuePort(portHandle);
    }

    // ─── Process-wide default queue ───────────────────────────────────────────────

    /// <summary>
    /// Gets or sets the process-wide default task queue.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Get:</b> returns the current process default, or <see langword="null"/> if none is set
    /// (<c>XTaskQueueGetCurrentProcessTaskQueue</c>). The caller is responsible for disposing the
    /// returned instance, which closes the duplicated handle without terminating the underlying
    /// queue. <see cref="IsComposite"/> is <see langword="true"/> on the returned instance because
    /// the dispatch modes are not known to the projection.
    /// </para>
    /// <para>
    /// <b>Set:</b> updates the process-wide default to the supplied queue, or clears it when
    /// <see langword="null"/> is assigned (<c>XTaskQueueSetCurrentProcessTaskQueue</c>).
    /// </para>
    /// </remarks>
    public static unsafe GameTaskQueue? ProcessDefault
    {
        get
        {
            IntPtr raw;
            byte hasDefault = Native.XTaskQueueGetCurrentProcessTaskQueue(&raw);
            if (hasDefault == 0 || raw == IntPtr.Zero)
            {
                return null;
            }

            return new GameTaskQueue(new TaskQueueBorrowedHandle(raw), default, default, isComposite: true);
        }

        set => Native.XTaskQueueSetCurrentProcessTaskQueue(
            value is null ? IntPtr.Zero : value.Handle);
    }

    // ─── Submit callbacks ─────────────────────────────────────────────────────────

    /// <summary>
    /// Submits <paramref name="callback"/> to the specified port
    /// (<c>XTaskQueueSubmitCallback</c>).
    /// </summary>
    /// <param name="callback">The action to invoke. Must not be <see langword="null"/>.</param>
    /// <param name="port">
    /// The port to submit to. Defaults to <see cref="GameTaskQueuePortKind.Completion"/>.
    /// </param>
    /// <remarks>
    /// The callback is invoked exactly once. If the queue is terminated before the callback runs,
    /// it is invoked with a <em>canceled</em> signal and the action is discarded (not called).
    /// Either way, the internal resources for this submission are always released.
    /// </remarks>
    public unsafe void SubmitCallback(
        Action callback,
        GameTaskQueuePortKind port = GameTaskQueuePortKind.Completion)
    {
        if (callback is null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        // Resolve the handle first so ObjectDisposedException fires before the GCHandle is allocated.
        IntPtr queue = Handle;

        GCHandle handle = GCHandle.Alloc(callback);
        int hr = Native.XTaskQueueSubmitCallback(
            queue,
            (XTaskQueuePort)port,
            GCHandle.ToIntPtr(handle),
            Trampolines.TaskQueueOneShotCallback);

        if (HResult.Failed(hr))
        {
            handle.Free();
            Hr.ThrowIfFailed(hr);
        }
    }

    /// <summary>
    /// Submits <paramref name="callback"/> to the specified port after at least
    /// <paramref name="delay"/> has elapsed (<c>XTaskQueueSubmitDelayedCallback</c>).
    /// </summary>
    /// <param name="callback">The action to invoke. Must not be <see langword="null"/>.</param>
    /// <param name="delay">
    /// Minimum delay before the callback is eligible to run. Clamped to the native 32-bit
    /// millisecond range (0 – ~49.7 days).
    /// </param>
    /// <param name="port">
    /// The port to submit to. Defaults to <see cref="GameTaskQueuePortKind.Completion"/>.
    /// </param>
    public unsafe void SubmitDelayedCallback(
        Action callback,
        TimeSpan delay,
        GameTaskQueuePortKind port = GameTaskQueuePortKind.Completion)
    {
        if (callback is null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        IntPtr queue = Handle; // ObjectDisposedException before GCHandle alloc
        uint delayMs = ToTimeoutMilliseconds(delay);

        GCHandle handle = GCHandle.Alloc(callback);
        int hr = Native.XTaskQueueSubmitDelayedCallback(
            queue,
            (XTaskQueuePort)port,
            delayMs,
            GCHandle.ToIntPtr(handle),
            Trampolines.TaskQueueOneShotCallback);

        if (HResult.Failed(hr))
        {
            handle.Free();
            Hr.ThrowIfFailed(hr);
        }
    }

    // ─── Register waiter ──────────────────────────────────────────────────────────

    /// <summary>
    /// Registers a Win32 waitable handle so that <paramref name="callback"/> is called on
    /// <paramref name="port"/> whenever <paramref name="waitHandle"/> is signaled
    /// (<c>XTaskQueueRegisterWaiter</c>).
    /// </summary>
    /// <param name="waitHandle">
    /// A <see cref="WaitHandle"/> whose <c>SafeWaitHandle</c> is used. Must not be
    /// <see langword="null"/> or disposed.
    /// </param>
    /// <param name="callback">The action to invoke on each signal. Must not be <see langword="null"/>.</param>
    /// <param name="port">
    /// The port to deliver callbacks to. Defaults to <see cref="GameTaskQueuePortKind.Completion"/>.
    /// </param>
    /// <returns>
    /// An <see cref="IDisposable"/> registration. Dispose it to stop future callbacks.
    /// If the queue is terminated before disposal, the registration cleans up automatically.
    /// </returns>
    public GameTaskQueueWaiterRegistration RegisterWaiter(
        WaitHandle waitHandle,
        Action callback,
        GameTaskQueuePortKind port = GameTaskQueuePortKind.Completion)
    {
        if (waitHandle is null)
        {
            throw new ArgumentNullException(nameof(waitHandle));
        }

        if (callback is null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        return GameTaskQueueWaiterRegistration.Create(Handle, (XTaskQueuePort)port, waitHandle, callback);
    }

    // ─── Register monitor ─────────────────────────────────────────────────────────

    /// <summary>
    /// Registers a monitor callback that fires whenever any callback is submitted to this queue
    /// (<c>XTaskQueueRegisterMonitor</c>).
    /// </summary>
    /// <param name="callback">
    /// Invoked on the thread that submits a callback to the queue, with a
    /// <see cref="GameTaskQueueMonitorEventArgs"/> identifying the port.
    /// Must not be <see langword="null"/>.
    /// </param>
    /// <returns>
    /// An <see cref="IDisposable"/> registration. Dispose it to deregister.
    /// </returns>
    public GameTaskQueueMonitorRegistration RegisterMonitor(
        Action<GameTaskQueueMonitorEventArgs> callback)
    {
        if (callback is null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        return GameTaskQueueMonitorRegistration.Create(Handle, callback);
    }

    // ─── Existing public methods ──────────────────────────────────────────────────

    /// <summary>
    /// Returns an independent instance backed by its own native handle
    /// (<c>XTaskQueueDuplicateHandle</c>); disposing it does not affect this one.
    /// </summary>
    public unsafe GameTaskQueue Duplicate()
    {
        IntPtr raw;
        int hr = Native.XTaskQueueDuplicateHandle(Handle, &raw);
        Hr.ThrowIfFailed(hr);

        return new GameTaskQueue(new TaskQueueHandle(raw), WorkDispatchMode, CompletionDispatchMode, IsComposite);
    }

    /// <summary>
    /// Drains ready completion callbacks and returns how many ran. Call this once per frame under
    /// the pumped model.
    /// </summary>
    /// <param name="timeout">
    /// How long the first dispatch may wait for a callback to become ready. Defaults to
    /// <see cref="TimeSpan.Zero"/>, which never blocks. Later callbacks in the same drain are always
    /// taken without waiting.
    /// </param>
    public int DispatchCompletions(TimeSpan? timeout = null)
    {
        IntPtr queue = Handle;
        uint timeoutMs = ToTimeoutMilliseconds(timeout);

        int dispatched = 0;
        while (Native.XTaskQueueDispatch(queue, XTaskQueuePort.Completion, timeoutMs) != 0)
        {
            dispatched++;
            timeoutMs = 0;
        }

        return dispatched;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle.Dispose();
    }

    internal static uint ToTimeoutMilliseconds(TimeSpan? timeout)
    {
        if (timeout is not { } value)
        {
            return 0;
        }

        if (value == Timeout.InfiniteTimeSpan)
        {
            return uint.MaxValue;
        }

        double milliseconds = value.TotalMilliseconds;
        if (milliseconds <= 0)
        {
            return 0;
        }

        return milliseconds >= uint.MaxValue ? uint.MaxValue - 1 : (uint)milliseconds;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(GameTaskQueue));
        }
    }

    // ─── Private handle type ──────────────────────────────────────────────────────

    /// <summary>
    /// Wraps an <c>XTaskQueueHandle</c> obtained externally (e.g.
    /// <c>XTaskQueueGetCurrentProcessTaskQueue</c>). Calls <c>XTaskQueueCloseHandle</c> on release
    /// but does <b>not</b> terminate the queue, because the caller does not own it exclusively.
    /// </summary>
    private sealed class TaskQueueBorrowedHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal TaskQueueBorrowedHandle(IntPtr existingHandle)
            : base(ownsHandle: true)
        {
            SetHandle(existingHandle);
        }

        protected override bool ReleaseHandle()
        {
            Native.XTaskQueueCloseHandle(handle);
            return true;
        }
    }
}

// ─── Registration types ───────────────────────────────────────────────────────────────────────────

/// <summary>
/// Represents an active <c>XTaskQueueRegisterWaiter</c> registration. Dispose to unregister.
/// </summary>
/// <remarks>
/// If the owning queue is terminated before this registration is disposed, the registration
/// cleans up its own resources automatically via the <c>canceled=true</c> callback path.
/// </remarks>
internal sealed unsafe class GameTaskQueueWaiterRegistration : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, GameTaskQueueWaiterRegistration> s_registrations = new();

    private readonly IntPtr _queueHandle;
    private readonly Action _callback;
    private GCHandle _gcHandle;
    private XTaskQueueRegistrationToken _token; // set after successful native registration
    private int _freed; // 0 = alive, 1 = freed — guards TryRelease against double-free

    private GameTaskQueueWaiterRegistration(IntPtr queueHandle, Action callback)
    {
        _queueHandle = queueHandle;
        _callback = callback;
    }

    internal static GameTaskQueueWaiterRegistration Create(
        IntPtr queueHandle,
        XTaskQueuePort port,
        WaitHandle waitHandle,
        Action callback)
    {
        var registration = new GameTaskQueueWaiterRegistration(queueHandle, callback);
        GCHandle gcHandle = GCHandle.Alloc(registration);
        IntPtr context = GCHandle.ToIntPtr(gcHandle);
        s_registrations[context] = registration;
        registration._gcHandle = gcHandle;

        XTaskQueueRegistrationToken token;
        int hr = Native.XTaskQueueRegisterWaiter(
            queueHandle,
            port,
            waitHandle.SafeWaitHandle.DangerousGetHandle(),
            context,
            Trampolines.TaskQueueWaiterCallback,
            &token);

        if (HResult.Failed(hr))
        {
            s_registrations.TryRemove(context, out _);
            gcHandle.Free();
            Hr.ThrowIfFailed(hr); // throws; registration is abandoned
        }

        // Set the real token.  The callback cannot need _token (it only needs _callback), and
        // Dispose cannot be called before Create returns, so this assignment is race-free.
        registration._token = token;
        return registration;
    }

    /// <summary>Unregisters the waiter (<c>XTaskQueueUnregisterWaiter</c>). Safe to call multiple times.</summary>
    public void Dispose()
    {
        if (Interlocked.Exchange(ref _freed, 1) == 0)
        {
            Native.XTaskQueueUnregisterWaiter(_queueHandle, _token);
            IntPtr context = GCHandle.ToIntPtr(_gcHandle);
            s_registrations.TryRemove(context, out _);
            _gcHandle.Free();
        }
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> for the waiter callback trampoline.</summary>
    internal static void Dispatch(IntPtr context, byte canceled)
    {
        if (!s_registrations.TryGetValue(context, out GameTaskQueueWaiterRegistration? reg))
        {
            return;
        }

        if (canceled != 0)
        {
            // Queue is terminating: clean up so GCHandle is released even if Dispose is never called.
            if (Interlocked.Exchange(ref reg._freed, 1) == 0)
            {
                s_registrations.TryRemove(context, out _);
                reg._gcHandle.Free();
            }

            return;
        }

        try
        {
            reg._callback();
        }
        catch
        {
            // Never let a managed exception escape to the trampoline.
        }
    }
}

/// <summary>
/// Represents an active <c>XTaskQueueRegisterMonitor</c> registration. Dispose to unregister.
/// </summary>
/// <remarks>
/// The monitor fires whenever any callback is submitted to the monitored queue.
/// If the owning queue is terminated before this registration is disposed, the registration
/// cleans up its own resources automatically.
/// </remarks>
internal sealed unsafe class GameTaskQueueMonitorRegistration : IDisposable
{
    private static readonly ConcurrentDictionary<IntPtr, GameTaskQueueMonitorRegistration> s_registrations = new();

    private readonly IntPtr _queueHandle;
    private readonly Action<GameTaskQueueMonitorEventArgs> _callback;
    private GCHandle _gcHandle;
    private XTaskQueueRegistrationToken _token; // set after successful native registration
    private int _freed; // 0 = alive, 1 = freed

    private GameTaskQueueMonitorRegistration(IntPtr queueHandle, Action<GameTaskQueueMonitorEventArgs> callback)
    {
        _queueHandle = queueHandle;
        _callback = callback;
    }

    internal static GameTaskQueueMonitorRegistration Create(
        IntPtr queueHandle,
        Action<GameTaskQueueMonitorEventArgs> callback)
    {
        var registration = new GameTaskQueueMonitorRegistration(queueHandle, callback);
        GCHandle gcHandle = GCHandle.Alloc(registration);
        IntPtr context = GCHandle.ToIntPtr(gcHandle);
        s_registrations[context] = registration;
        registration._gcHandle = gcHandle;

        XTaskQueueRegistrationToken token;
        int hr = Native.XTaskQueueRegisterMonitor(
            queueHandle,
            context,
            Trampolines.TaskQueueMonitorCallback,
            &token);

        if (HResult.Failed(hr))
        {
            s_registrations.TryRemove(context, out _);
            gcHandle.Free();
            Hr.ThrowIfFailed(hr);
        }

        registration._token = token;
        return registration;
    }

    /// <summary>Unregisters the monitor (<c>XTaskQueueUnregisterMonitor</c>). Safe to call multiple times.</summary>
    public void Dispose()
    {
        if (Interlocked.Exchange(ref _freed, 1) == 0)
        {
            Native.XTaskQueueUnregisterMonitor(_queueHandle, _token);
            IntPtr context = GCHandle.ToIntPtr(_gcHandle);
            s_registrations.TryRemove(context, out _);
            _gcHandle.Free();
        }
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> for the monitor callback trampoline.</summary>
    internal static void Dispatch(IntPtr context, IntPtr queue, XTaskQueuePort port)
    {
        if (!s_registrations.TryGetValue(context, out GameTaskQueueMonitorRegistration? reg))
        {
            return;
        }

        try
        {
            reg._callback(new GameTaskQueueMonitorEventArgs((GameTaskQueuePortKind)port));
        }
        catch
        {
            // Never let a managed exception escape to the trampoline.
        }
    }
}

/// <summary>
/// Resolves a possibly-absent queue to the handle the native layer expects.
/// </summary>
internal static class GameTaskQueueExtensions
{
    /// <summary>
    /// Returns the queue's native handle, or <see cref="IntPtr.Zero"/> when no queue was supplied.
    /// </summary>
    /// <remarks>
    /// A null <c>XAsyncBlock::queue</c> is not an error or a missing value -- it tells the Gaming
    /// Runtime to resolve the process default task queue at call time, which by default is a
    /// thread-pool queue for both ports. Leaving the field null is therefore how the projection
    /// says "the caller expressed no preference", rather than manufacturing a queue and imposing
    /// one. The same applies to the event-registration APIs, which take the handle directly.
    /// </remarks>
    internal static IntPtr RawHandle(this GameTaskQueue? queue) => queue?.Handle ?? IntPtr.Zero;
}
