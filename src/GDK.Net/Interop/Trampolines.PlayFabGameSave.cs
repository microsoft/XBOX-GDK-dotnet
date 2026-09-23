// Callback thunks for PFGameSaveFiles.h and PFGameSaveFilesUi.h.
//
// The PlayFab game save callbacks are process-wide singletons rather than per-registration tokens,
// so no context value is needed: every thunk forwards straight to PlayFabGameSaveFiles, which owns
// the managed event handlers.
//
// See Trampolines.cs for the shim rules these declarations follow.

using System;
using System.Runtime.InteropServices;
using GDK.Net.PlayFab;

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace GDK.Net.Interop;

/// <summary>
/// Native-to-managed thunks for the PlayFab game save callbacks.
/// </summary>
internal static unsafe partial class Trampolines
{
#if NET5_0_OR_GREATER

    internal static IntPtr GameSaveActiveDeviceChangedCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, PFGameSaveDescriptor*, IntPtr, void>)
        &OnGameSaveActiveDeviceChanged;

    internal static IntPtr GameSaveUiProgressCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, PFGameSaveFilesSyncState, IntPtr, void>)
        &OnGameSaveUiProgress;

    internal static IntPtr GameSaveUiSyncFailedCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, PFGameSaveFilesSyncState, int, IntPtr, void>)
        &OnGameSaveUiSyncFailed;

    internal static IntPtr GameSaveUiActiveDeviceContentionCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<
            IntPtr, PFGameSaveDescriptor*, PFGameSaveDescriptor*, IntPtr, void>)
        &OnGameSaveUiActiveDeviceContention;

    internal static IntPtr GameSaveUiConflictCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<
            IntPtr, PFGameSaveDescriptor*, PFGameSaveDescriptor*, IntPtr, void>)
        &OnGameSaveUiConflict;

    internal static IntPtr GameSaveUiOutOfStorageCallback { get; } =
        (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, ulong, IntPtr, void>)&OnGameSaveUiOutOfStorage;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnGameSaveActiveDeviceChanged(
        IntPtr localUser, PFGameSaveDescriptor* activeDevice, IntPtr context) =>
        GameSaveActiveDeviceChanged(localUser, activeDevice);

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnGameSaveUiProgress(
        IntPtr localUser, PFGameSaveFilesSyncState state, IntPtr context) =>
        GameSaveUiProgress(localUser, state);

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnGameSaveUiSyncFailed(
        IntPtr localUser, PFGameSaveFilesSyncState state, int error, IntPtr context) =>
        GameSaveUiSyncFailed(localUser, state, error);

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnGameSaveUiActiveDeviceContention(
        IntPtr localUser,
        PFGameSaveDescriptor* local,
        PFGameSaveDescriptor* remote,
        IntPtr context) =>
        GameSaveUiActiveDeviceContention(localUser, local, remote);

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnGameSaveUiConflict(
        IntPtr localUser,
        PFGameSaveDescriptor* local,
        PFGameSaveDescriptor* remote,
        IntPtr context) =>
        GameSaveUiConflict(localUser, local, remote);

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnGameSaveUiOutOfStorage(
        IntPtr localUser, ulong requiredBytes, IntPtr context) =>
        GameSaveUiOutOfStorage(localUser, requiredBytes);

#else

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void GameSaveActiveDeviceChangedDelegate(
        IntPtr localUser, PFGameSaveDescriptor* activeDevice, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void GameSaveUiProgressDelegate(
        IntPtr localUser, PFGameSaveFilesSyncState state, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void GameSaveUiSyncFailedDelegate(
        IntPtr localUser, PFGameSaveFilesSyncState state, int error, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void GameSaveUiDescriptorPairDelegate(
        IntPtr localUser, PFGameSaveDescriptor* local, PFGameSaveDescriptor* remote, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void GameSaveUiOutOfStorageDelegate(
        IntPtr localUser, ulong requiredBytes, IntPtr context);

    // Rooted for the process lifetime: the native side keeps the function pointers indefinitely.
    private static readonly GameSaveActiveDeviceChangedDelegate GameSaveActiveDeviceChangedKeepAlive =
        (localUser, activeDevice, context) => GameSaveActiveDeviceChanged(localUser, activeDevice);

    private static readonly GameSaveUiProgressDelegate GameSaveUiProgressKeepAlive =
        (localUser, state, context) => GameSaveUiProgress(localUser, state);

    private static readonly GameSaveUiSyncFailedDelegate GameSaveUiSyncFailedKeepAlive =
        (localUser, state, error, context) => GameSaveUiSyncFailed(localUser, state, error);

    private static readonly GameSaveUiDescriptorPairDelegate GameSaveUiActiveDeviceContentionKeepAlive =
        (localUser, local, remote, context) =>
            GameSaveUiActiveDeviceContention(localUser, local, remote);

    private static readonly GameSaveUiDescriptorPairDelegate GameSaveUiConflictKeepAlive =
        (localUser, local, remote, context) => GameSaveUiConflict(localUser, local, remote);

    private static readonly GameSaveUiOutOfStorageDelegate GameSaveUiOutOfStorageKeepAlive =
        (localUser, requiredBytes, context) => GameSaveUiOutOfStorage(localUser, requiredBytes);

    internal static IntPtr GameSaveActiveDeviceChangedCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(GameSaveActiveDeviceChangedKeepAlive);

    internal static IntPtr GameSaveUiProgressCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(GameSaveUiProgressKeepAlive);

    internal static IntPtr GameSaveUiSyncFailedCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(GameSaveUiSyncFailedKeepAlive);

    internal static IntPtr GameSaveUiActiveDeviceContentionCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(GameSaveUiActiveDeviceContentionKeepAlive);

    internal static IntPtr GameSaveUiConflictCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(GameSaveUiConflictKeepAlive);

    internal static IntPtr GameSaveUiOutOfStorageCallback { get; } =
        Marshal.GetFunctionPointerForDelegate(GameSaveUiOutOfStorageKeepAlive);

#endif

    private static void GameSaveActiveDeviceChanged(IntPtr localUser, PFGameSaveDescriptor* activeDevice)
    {
        try
        {
            PlayFabGameSaveFiles.DispatchActiveDeviceChanged(localUser, activeDevice);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void GameSaveUiProgress(IntPtr localUser, PFGameSaveFilesSyncState state)
    {
        try
        {
            PlayFabGameSaveFiles.DispatchProgress(localUser, state);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void GameSaveUiSyncFailed(
        IntPtr localUser, PFGameSaveFilesSyncState state, int error)
    {
        try
        {
            PlayFabGameSaveFiles.DispatchSyncFailed(localUser, state, error);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void GameSaveUiActiveDeviceContention(
        IntPtr localUser, PFGameSaveDescriptor* local, PFGameSaveDescriptor* remote)
    {
        try
        {
            PlayFabGameSaveFiles.DispatchDeviceContention(localUser, local, remote);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void GameSaveUiConflict(
        IntPtr localUser, PFGameSaveDescriptor* local, PFGameSaveDescriptor* remote)
    {
        try
        {
            PlayFabGameSaveFiles.DispatchConflict(localUser, local, remote);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }

    private static void GameSaveUiOutOfStorage(IntPtr localUser, ulong requiredBytes)
    {
        try
        {
            PlayFabGameSaveFiles.DispatchOutOfStorage(localUser, requiredBytes);
        }
        catch
        {
            // Never let a managed exception cross back into the Gaming Runtime.
        }
    }
}
