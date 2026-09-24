// Native-to-managed callback thunks for the runtime core (XAsync.h). Each API family keeps its own
// thunks in a Trampolines.<Family>.cs partial, see Trampolines.User.cs, Trampolines.Xbl.cs and so
// on.

using System;
using System.Runtime.InteropServices;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Native-to-managed callback thunks.
/// </summary>
/// <remarks>
/// On net8.0/net10.0 these are static <c>[UnmanagedCallersOnly]</c> methods taken as function
/// pointers, so no delegate is allocated and nothing has to be kept rooted. On netstandard2.0 the
/// equivalent delegates are held in static readonly fields for the lifetime of the process, which
/// is what keeps the marshalling stubs alive after
/// <see cref="Marshal.GetFunctionPointerForDelegate{TDelegate}(TDelegate)"/> returns.
///
/// Exceptions must never unwind into native code, so every thunk swallows them at the boundary.
/// </remarks>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr AsyncCompletionRoutine { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, void>)&OnAsyncCompleted;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnAsyncCompleted(IntPtr asyncBlock)
    {
        try
        {
            AsyncOperation.Dispatch(asyncBlock);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void AsyncCompletionRoutineDelegate(IntPtr asyncBlock);

    // Rooted for the process lifetime: the native side keeps the function pointer indefinitely.
    private static readonly AsyncCompletionRoutineDelegate AsyncCompletionRoutineKeepAlive = OnAsyncCompleted;

    internal static IntPtr AsyncCompletionRoutine { get; } =
        Marshal.GetFunctionPointerForDelegate(AsyncCompletionRoutineKeepAlive);

    private static void OnAsyncCompleted(IntPtr asyncBlock)
    {
        try
        {
            AsyncOperation.Dispatch(asyncBlock);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

#endif
}
