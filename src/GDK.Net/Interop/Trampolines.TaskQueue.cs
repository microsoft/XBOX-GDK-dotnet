using System;
using System.Runtime.InteropServices;
using GDK.Net;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Native-to-managed thunks for the XTaskQueue callback APIs.
/// </summary>
/// <remarks>
/// Three trampolines are provided:
/// <list type="bullet">
/// <item><description>
/// <see cref="TaskQueueOneShotCallback"/>, for <c>XTaskQueueSubmitCallback</c> and
/// <c>XTaskQueueSubmitDelayedCallback</c>. The context is a <see cref="GCHandle"/> pointing
/// directly to the managed <see cref="System.Action"/> delegate. The handle is freed on every
/// invocation (both the normal and the <c>canceled=true</c> termination paths).
/// </description></item>
/// <item><description>
/// <see cref="TaskQueueWaiterCallback"/>, for <c>XTaskQueueRegisterWaiter</c>. The context is
/// a <see cref="GCHandle"/> pointing to a <see cref="GameTaskQueueWaiterRegistration"/> held in
/// a static dictionary. Freeing the handle is coordinated between the trampoline
/// (<c>canceled=true</c> path) and <see cref="GameTaskQueueWaiterRegistration.Dispose"/>.
/// </description></item>
/// <item><description>
/// <see cref="TaskQueueMonitorCallback"/>, for <c>XTaskQueueRegisterMonitor</c>. Same ownership
/// model as the waiter trampoline, but carries the additional <c>queue</c> and <c>port</c> params.
/// </description></item>
/// </list>
/// Exceptions must never cross back into the Gaming Runtime, so every thunk swallows them.
/// </remarks>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr TaskQueueOneShotCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, byte, void>)&OnTaskQueueOneShot;

    internal static IntPtr TaskQueueWaiterCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, byte, void>)&OnTaskQueueWaiter;

    internal static IntPtr TaskQueueMonitorCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, XTaskQueuePort, void>)&OnTaskQueueMonitor;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnTaskQueueOneShot(IntPtr context, byte canceled)
    {
        GCHandle handle = GCHandle.FromIntPtr(context);
        try
        {
            if (canceled == 0 && handle.Target is Action action)
            {
                action();
            }
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
        finally
        {
            if (handle.IsAllocated)
            {
                handle.Free();
            }
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnTaskQueueWaiter(IntPtr context, byte canceled)
    {
        try
        {
            GameTaskQueueWaiterRegistration.Dispatch(context, canceled);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnTaskQueueMonitor(IntPtr context, IntPtr queue, XTaskQueuePort port)
    {
        try
        {
            GameTaskQueueMonitorRegistration.Dispatch(context, queue, port);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void OneShotCallbackDelegate(IntPtr context, byte canceled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void WaiterCallbackDelegate(IntPtr context, byte canceled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void MonitorCallbackDelegate(IntPtr context, IntPtr queue, XTaskQueuePort port);

    // Rooted for the process lifetime: the native side keeps the function pointer indefinitely.
    private static readonly OneShotCallbackDelegate OneShotCallbackKeepAlive = OnTaskQueueOneShot;
    private static readonly WaiterCallbackDelegate WaiterCallbackKeepAlive = OnTaskQueueWaiter;
    private static readonly MonitorCallbackDelegate MonitorCallbackKeepAlive = OnTaskQueueMonitor;

    internal static IntPtr TaskQueueOneShotCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(OneShotCallbackKeepAlive);

    internal static IntPtr TaskQueueWaiterCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(WaiterCallbackKeepAlive);

    internal static IntPtr TaskQueueMonitorCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(MonitorCallbackKeepAlive);

    private static void OnTaskQueueOneShot(IntPtr context, byte canceled)
    {
        GCHandle handle = GCHandle.FromIntPtr(context);
        try
        {
            if (canceled == 0 && handle.Target is Action action)
            {
                action();
            }
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
        finally
        {
            if (handle.IsAllocated)
            {
                handle.Free();
            }
        }
    }

    private static void OnTaskQueueWaiter(IntPtr context, byte canceled)
    {
        try
        {
            GameTaskQueueWaiterRegistration.Dispatch(context, canceled);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void OnTaskQueueMonitor(IntPtr context, IntPtr queue, XTaskQueuePort port)
    {
        try
        {
            GameTaskQueueMonitorRegistration.Dispatch(context, queue, port);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
