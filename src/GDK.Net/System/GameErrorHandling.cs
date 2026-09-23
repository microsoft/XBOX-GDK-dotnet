using System;
using System.Runtime.InteropServices;
using System.Text;
using GDK.Net.Interop;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.SystemInfo;

/// <summary>
/// GDK error-reporting hooks. Wrap <c>XErrorSetCallback</c> and <c>XErrorSetOptions</c> from
/// XError.h.
/// </summary>
/// <remarks>
/// <para>
/// Use <see cref="SetErrorCallback"/> to receive a notification whenever the Gaming Runtime calls
/// <c>XErrorReport</c> internally. Return <see langword="true"/> from the callback to suppress the
/// error; return <see langword="false"/> to let the runtime apply its configured
/// <see cref="ErrorOptions"/> behaviour.
/// </para>
/// <para>
/// Only one callback can be active at a time (the native API is a last-writer-wins register).
/// Passing <see langword="null"/> clears the registration.
/// </para>
/// </remarks>
public static unsafe class GameErrorHandling
{
    private static GdkErrorCallback? _errorCallback;
    private static readonly object Gate = new object();

    /// <summary>
    /// Registers or clears the error callback (<c>XErrorSetCallback</c>).
    /// </summary>
    /// <param name="callback">
    /// The managed callback to invoke on each error, or <see langword="null"/> to clear.
    /// The callback must not throw; any exception is silently swallowed at the native boundary.
    /// </param>
    public static void SetErrorCallback(GdkErrorCallback? callback)
    {
        lock (Gate)
        {
            _errorCallback = callback;
            Native.XErrorSetCallback(
                callback != null ? ErrorCallbackFunctionPointer : IntPtr.Zero,
                IntPtr.Zero);
        }
    }

    /// <summary>
    /// Configures error-reporting behaviour (<c>XErrorSetOptions</c>).
    /// </summary>
    /// <param name="optionsDebuggerPresent">Options applied when a debugger is attached.</param>
    /// <param name="optionsDebuggerNotPresent">Options applied when no debugger is present.</param>
    public static void SetOptions(ErrorOptions optionsDebuggerPresent, ErrorOptions optionsDebuggerNotPresent)
    {
        Native.XErrorSetOptions((XErrorOptions)optionsDebuggerPresent, (XErrorOptions)optionsDebuggerNotPresent);
    }

    // ─── Trampoline ──────────────────────────────────────────────────────────────
#if NET5_0_OR_GREATER
    internal static IntPtr ErrorCallbackFunctionPointer { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<int, byte*, IntPtr, byte>)&OnNativeError;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static byte OnNativeError(int hr, byte* msg, IntPtr context)
    {
        try
        {
            GdkErrorCallback? cb = _errorCallback;
            if (cb == null)
            {
                return 0;
            }

            string message = PtrToString(msg);
            return cb(hr, message) ? (byte)1 : (byte)0;
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
            return 0;
        }
    }
#else
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate byte NativeErrorCallbackDelegate(int hr, byte* msg, IntPtr context);

    private static readonly NativeErrorCallbackDelegate ErrorCallbackDelegateKeepAlive = OnNativeError;

    internal static IntPtr ErrorCallbackFunctionPointer { get; } =
        Marshal.GetFunctionPointerForDelegate(ErrorCallbackDelegateKeepAlive);

    private static byte OnNativeError(int hr, byte* msg, IntPtr context)
    {
        try
        {
            GdkErrorCallback? cb = _errorCallback;
            if (cb == null)
            {
                return 0;
            }

            string message = PtrToString(msg);
            return cb(hr, message) ? (byte)1 : (byte)0;
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
            return 0;
        }
    }
#endif

    private static unsafe string PtrToString(byte* ptr)
    {
        if (ptr == null)
        {
            return string.Empty;
        }

        int length = 0;
        while (ptr[length] != 0)
        {
            length++;
        }

        return length > 0 ? Encoding.UTF8.GetString(ptr, length) : string.Empty;
    }
}
