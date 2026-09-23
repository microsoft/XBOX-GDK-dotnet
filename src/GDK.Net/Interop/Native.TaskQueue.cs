// P/Invoke declarations for extended XTaskQueue APIs and XGameRuntimeInitializeWithOptions.
// Sources: XTaskQueue.h, XGameRuntimeInit.h (GDK edition 260404).
// See Native.cs for the full explanation of why xgameruntime.thunks.dll is used.

using System;
using System.Runtime.InteropServices;

namespace GDK.Net.Interop;

#if NET7_0_OR_GREATER

internal static unsafe partial class Native
{
    // --- XTaskQueue.h: composite queue and port-handle APIs ---

    /// <summary>Creates a queue from two existing port handles.</summary>
    [LibraryImport(LibraryName)]
    internal static partial int XTaskQueueCreateComposite(
        IntPtr workPort,
        IntPtr completionPort,
        IntPtr* queue);

    /// <summary>Returns an <c>XTaskQueuePortHandle</c> for the given port of <paramref name="queue"/>.</summary>
    /// <remarks>Port handles are owned by the queue and must NOT be closed individually.</remarks>
    [LibraryImport(LibraryName)]
    internal static partial int XTaskQueueGetPort(
        IntPtr queue,
        XTaskQueuePort port,
        IntPtr* portHandle);

    // --- XTaskQueue.h: process-wide default queue ---

    /// <summary>
    /// Gets the process-wide default task queue.
    /// Returns 1 (true) if a default queue is set; 0 (false) otherwise.
    /// When 0, <paramref name="queue"/> is set to <see cref="IntPtr.Zero"/>.
    /// </summary>
    [LibraryImport(LibraryName)]
    internal static partial byte XTaskQueueGetCurrentProcessTaskQueue(IntPtr* queue);

    /// <summary>
    /// Sets the process-wide default task queue. Pass <see cref="IntPtr.Zero"/> to clear.
    /// </summary>
    [LibraryImport(LibraryName)]
    internal static partial void XTaskQueueSetCurrentProcessTaskQueue(IntPtr queue);

    // --- XTaskQueue.h: submit callbacks ---

    [LibraryImport(LibraryName)]
    internal static partial int XTaskQueueSubmitCallback(
        IntPtr queue,
        XTaskQueuePort port,
        IntPtr callbackContext,
        IntPtr callback);

    [LibraryImport(LibraryName)]
    internal static partial int XTaskQueueSubmitDelayedCallback(
        IntPtr queue,
        XTaskQueuePort port,
        uint delayMs,
        IntPtr callbackContext,
        IntPtr callback);

    // --- XTaskQueue.h: waiter registration ---

    [LibraryImport(LibraryName)]
    internal static partial int XTaskQueueRegisterWaiter(
        IntPtr queue,
        XTaskQueuePort port,
        IntPtr waitHandle,
        IntPtr callbackContext,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial void XTaskQueueUnregisterWaiter(
        IntPtr queue,
        XTaskQueueRegistrationToken token);

    // --- XTaskQueue.h: monitor registration ---

    [LibraryImport(LibraryName)]
    internal static partial int XTaskQueueRegisterMonitor(
        IntPtr queue,
        IntPtr callbackContext,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [LibraryImport(LibraryName)]
    internal static partial void XTaskQueueUnregisterMonitor(
        IntPtr queue,
        XTaskQueueRegistrationToken token);

    // --- XGameRuntimeInit.h: options-based initialize ---

    [LibraryImport(LibraryName)]
    internal static partial int XGameRuntimeInitializeWithOptions(XGameRuntimeOptions* options);
}

#else

internal static unsafe partial class Native
{
    // --- XTaskQueue.h: composite queue and port-handle APIs ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XTaskQueueCreateComposite(
        IntPtr workPort,
        IntPtr completionPort,
        IntPtr* queue);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XTaskQueueGetPort(
        IntPtr queue,
        XTaskQueuePort port,
        IntPtr* portHandle);

    // --- XTaskQueue.h: process-wide default queue ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern byte XTaskQueueGetCurrentProcessTaskQueue(IntPtr* queue);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XTaskQueueSetCurrentProcessTaskQueue(IntPtr queue);

    // --- XTaskQueue.h: submit callbacks ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XTaskQueueSubmitCallback(
        IntPtr queue,
        XTaskQueuePort port,
        IntPtr callbackContext,
        IntPtr callback);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XTaskQueueSubmitDelayedCallback(
        IntPtr queue,
        XTaskQueuePort port,
        uint delayMs,
        IntPtr callbackContext,
        IntPtr callback);

    // --- XTaskQueue.h: waiter registration ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XTaskQueueRegisterWaiter(
        IntPtr queue,
        XTaskQueuePort port,
        IntPtr waitHandle,
        IntPtr callbackContext,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XTaskQueueUnregisterWaiter(
        IntPtr queue,
        XTaskQueueRegistrationToken token);

    // --- XTaskQueue.h: monitor registration ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XTaskQueueRegisterMonitor(
        IntPtr queue,
        IntPtr callbackContext,
        IntPtr callback,
        XTaskQueueRegistrationToken* token);

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern void XTaskQueueUnregisterMonitor(
        IntPtr queue,
        XTaskQueueRegistrationToken token);

    // --- XGameRuntimeInit.h: options-based initialize ---

    [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    internal static extern int XGameRuntimeInitializeWithOptions(XGameRuntimeOptions* options);
}

#endif
