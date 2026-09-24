using System;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab;

/// <summary>
/// PlayFab Game Save Files (<c>PFGameSaveFiles.h</c>, <c>PFGameSaveFilesUi.h</c>): whole-folder
/// save synchronization between the device and PlayFab, driven by
/// <see cref="PlayFabLocalUser"/>.
/// </summary>
/// <remarks>
/// <para>
/// The library is process-wide, so this is a static class. <see cref="Initialize"/> must run after
/// <see cref="PlayFabRuntime.Initialize"/> and before any other member here.
/// </para>
/// <para>
/// The <c>...UiRequested</c> events replace the native UI callbacks. The sync stays blocked until
/// the handler calls <c>Respond</c> on the event arguments, so a title that subscribes must always
/// respond: cancelling is a valid response. Events raised without a subscriber are answered with
/// the cancel action automatically, which keeps an unhandled prompt from hanging the sync.
/// </para>
/// </remarks>
public static class PlayFabGameSaveFiles
{
    private static readonly object Gate = new();

    private static int _initialized;
    private static EventHandler<GameSaveActiveDeviceChangedEventArgs>? _activeDeviceChanged;
    private static bool _uiCallbacksInstalled;

    private static EventHandler<GameSaveSyncProgressEventArgs>? _syncProgressUiRequested;
    private static EventHandler<GameSaveSyncFailedEventArgs>? _syncFailedUiRequested;
    private static EventHandler<GameSaveActiveDeviceContentionEventArgs>? _deviceContentionUiRequested;
    private static EventHandler<GameSaveConflictEventArgs>? _conflictUiRequested;
    private static EventHandler<GameSaveOutOfStorageEventArgs>? _outOfStorageUiRequested;

    /// <summary>Whether <see cref="Initialize"/> has run without a matching uninitialize.</summary>
    public static bool IsInitialized => Volatile.Read(ref _initialized) != 0;

    /// <summary>
    /// Raised when a user's save moved to another device, which means this title should return to
    /// its main menu (<c>PFGameSaveFilesSetActiveDeviceChangedCallback</c>).
    /// </summary>
    public static unsafe event EventHandler<GameSaveActiveDeviceChangedEventArgs>? ActiveDeviceChanged
    {
        add
        {
            lock (Gate)
            {
                bool wasEmpty = _activeDeviceChanged is null;
                _activeDeviceChanged += value;
                if (wasEmpty && _activeDeviceChanged is not null)
                {
                    Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesSetActiveDeviceChangedCallback(
                        IntPtr.Zero, Trampolines.GameSaveActiveDeviceChangedCallback, null));
                }
            }
        }

        remove
        {
            lock (Gate)
            {
                _activeDeviceChanged -= value;
                if (_activeDeviceChanged is null)
                {
                    Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesSetActiveDeviceChangedCallback(
                        IntPtr.Zero, IntPtr.Zero, null));
                }
            }
        }
    }

    /// <summary>Raised while a sync runs so the title can show a progress dialog.</summary>
    public static event EventHandler<GameSaveSyncProgressEventArgs>? SyncProgressUiRequested
    {
        add { lock (Gate) { _syncProgressUiRequested += value; EnsureUiCallbacks(); } }
        remove { lock (Gate) { _syncProgressUiRequested -= value; } }
    }

    /// <summary>Raised when a sync failed and the user must choose how to continue.</summary>
    public static event EventHandler<GameSaveSyncFailedEventArgs>? SyncFailedUiRequested
    {
        add { lock (Gate) { _syncFailedUiRequested += value; EnsureUiCallbacks(); } }
        remove { lock (Gate) { _syncFailedUiRequested -= value; } }
    }

    /// <summary>Raised when another device already owns the user's save.</summary>
    public static event EventHandler<GameSaveActiveDeviceContentionEventArgs>? ActiveDeviceContentionUiRequested
    {
        add { lock (Gate) { _deviceContentionUiRequested += value; EnsureUiCallbacks(); } }
        remove { lock (Gate) { _deviceContentionUiRequested -= value; } }
    }

    /// <summary>Raised when the local and cloud saves diverged.</summary>
    public static event EventHandler<GameSaveConflictEventArgs>? ConflictUiRequested
    {
        add { lock (Gate) { _conflictUiRequested += value; EnsureUiCallbacks(); } }
        remove { lock (Gate) { _conflictUiRequested -= value; } }
    }

    /// <summary>Raised when the user's cloud quota cannot hold the pending upload.</summary>
    public static event EventHandler<GameSaveOutOfStorageEventArgs>? OutOfStorageUiRequested
    {
        add { lock (Gate) { _outOfStorageUiRequested += value; EnsureUiCallbacks(); } }
        remove { lock (Gate) { _outOfStorageUiRequested -= value; } }
    }

    /// <summary>
    /// Initializes the game save library (<c>PFGameSaveFilesInitialize</c>).
    /// </summary>
    /// <param name="saveFolder">
    /// The folder the title's saves live in, or <see langword="null"/> to let the library pick the
    /// platform default.
    /// </param>
    /// <param name="options">Reserved initialization flags.</param>
    public static unsafe void Initialize(
        string? saveFolder = null, GameSaveInitOptions options = GameSaveInitOptions.None)
    {
        IntPtr folder = Utf8.Allocate(saveFolder);
        try
        {
            PFGameSaveInitArgs args = default;
            args.BackgroundQueue = IntPtr.Zero;
            args.Options = (ulong)options;
            args.SaveFolder = (byte*)folder;
            Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesInitialize(&args));
            Volatile.Write(ref _initialized, 1);
        }
        finally
        {
            Utf8.Free(folder);
        }
    }

    /// <summary>
    /// Shuts the game save library down and waits for its background work to drain
    /// (<c>PFGameSaveFilesUninitializeAsync</c>).
    /// </summary>
    public static async Task UninitializeAsync(CancellationToken cancellationToken = default)
    {
        await AsyncOperation.RunAsync(
            IntPtr.Zero, StartUninitialize, GetUninitializeStatus, cancellationToken)
            .ConfigureAwait(false);
        Volatile.Write(ref _initialized, 0);
    }

    /// <summary>
    /// Adds a user to the game save system, showing the platform sync UI when needed
    /// (<c>PFGameSaveFilesAddUserWithUiAsync</c>).
    /// </summary>
    public static unsafe Task AddUserAsync(
        PlayFabLocalUser user,
        GameSaveFilesAddUserOptions options = GameSaveFilesAddUserOptions.None,
        CancellationToken cancellationToken = default)
    {
        IntPtr handle = Require(user);
        return AsyncOperation.RunAsync(
            IntPtr.Zero,
            block => NativePlayFab.PFGameSaveFilesAddUserWithUiAsync(
                handle, (PFGameSaveFilesAddUserOptions)options, (XAsyncBlock*)block),
            static block => NativePlayFab.PFGameSaveFilesAddUserWithUiResult((XAsyncBlock*)block),
            cancellationToken);
    }

    /// <summary>
    /// Uploads the user's save folder to the cloud, showing the platform sync UI when needed
    /// (<c>PFGameSaveFilesUploadWithUiAsync</c>).
    /// </summary>
    /// <param name="user">The user whose save is uploaded.</param>
    /// <param name="option">
    /// Whether this device stays the active device once the upload completes.
    /// </param>
    /// <param name="cancellationToken">Cancels the upload.</param>
    public static unsafe Task UploadAsync(
        PlayFabLocalUser user,
        GameSaveFilesUploadOption option = GameSaveFilesUploadOption.KeepDeviceActive,
        CancellationToken cancellationToken = default)
    {
        IntPtr handle = Require(user);
        return AsyncOperation.RunAsync(
            IntPtr.Zero,
            block => NativePlayFab.PFGameSaveFilesUploadWithUiAsync(
                handle, (PFGameSaveFilesUploadOption)option, (XAsyncBlock*)block),
            static block => NativePlayFab.PFGameSaveFilesUploadWithUiResult((XAsyncBlock*)block),
            cancellationToken);
    }

    /// <summary>
    /// Sets the short description shown next to the user's save
    /// (<c>PFGameSaveFilesSetSaveDescriptionAsync</c>).
    /// </summary>
    public static unsafe Task SetSaveDescriptionAsync(
        PlayFabLocalUser user, string description, CancellationToken cancellationToken = default)
    {
        if (description is null)
        {
            throw new ArgumentNullException(nameof(description));
        }

        IntPtr handle = Require(user);
        var arena = new PlayFabArena();
        try
        {
            IntPtr text = (IntPtr)arena.String(description);
            return PlayFabCall.InvokeAsync(
                arena,
                block => NativePlayFab.PFGameSaveFilesSetSaveDescriptionAsync(
                    handle, (byte*)text, (XAsyncBlock*)block),
                static block =>
                    NativePlayFab.PFGameSaveFilesSetSaveDescriptionResult((XAsyncBlock*)block),
                cancellationToken);
        }
        catch
        {
            arena.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Deletes the user's cloud save and makes the local one authoritative
    /// (<c>PFGameSaveFilesResetCloudAsync</c>).
    /// </summary>
    public static unsafe Task ResetCloudAsync(
        PlayFabLocalUser user, CancellationToken cancellationToken = default)
    {
        IntPtr handle = Require(user);
        return AsyncOperation.RunAsync(
            IntPtr.Zero,
            block => NativePlayFab.PFGameSaveFilesResetCloudAsync(handle, (XAsyncBlock*)block),
            static block => NativePlayFab.PFGameSaveFilesResetCloudResult((XAsyncBlock*)block),
            cancellationToken);
    }

    /// <summary>
    /// The folder the user's saves are stored in
    /// (<c>PFGameSaveFilesGetFolderSize</c>, <c>PFGameSaveFilesGetFolder</c>).
    /// </summary>
    public static unsafe string GetSaveFolder(PlayFabLocalUser user) =>
        PlayFabInterop.GetString(
            Require(user),
            NativePlayFab.PFGameSaveFilesGetFolderSize,
            NativePlayFab.PFGameSaveFilesGetFolder);

    /// <summary>
    /// How many more bytes the user may upload before hitting their quota
    /// (<c>PFGameSaveFilesGetRemainingQuota</c>).
    /// </summary>
    public static unsafe long GetRemainingQuota(PlayFabLocalUser user)
    {
        long quota;
        Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesGetRemainingQuota(Require(user), &quota));
        return quota;
    }

    /// <summary>
    /// Whether the user's saves are currently reaching the cloud
    /// (<c>PFGameSaveFilesIsConnectedToCloud</c>).
    /// </summary>
    public static unsafe bool IsConnectedToCloud(PlayFabLocalUser user)
    {
        byte connected;
        Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesIsConnectedToCloud(Require(user), &connected));
        return connected != 0;
    }

    /// <summary>
    /// Reads how far the user's sync has progressed
    /// (<c>PFGameSaveFilesUiProgressGetProgress</c>).
    /// </summary>
    public static GameSaveSyncProgress GetProgress(PlayFabLocalUser user) =>
        GetProgress(Require(user));

    internal static unsafe GameSaveSyncProgress GetProgress(IntPtr localUser)
    {
        PFGameSaveFilesSyncState state;
        ulong current;
        ulong total;
        Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesUiProgressGetProgress(
            localUser, &state, &current, &total));
        return new GameSaveSyncProgress((GameSaveFilesSyncState)state, current, total);
    }

    internal static unsafe void DispatchActiveDeviceChanged(IntPtr localUser, PFGameSaveDescriptor* device)
    {
        EventHandler<GameSaveActiveDeviceChangedEventArgs>? handler = Volatile.Read(ref _activeDeviceChanged);
        handler?.Invoke(
            null,
            new GameSaveActiveDeviceChangedEventArgs(
                localUser, device is null ? null : GameSaveDescriptor.FromNative(device)));
    }

    internal static unsafe void DispatchProgress(IntPtr localUser, PFGameSaveFilesSyncState state)
    {
        EventHandler<GameSaveSyncProgressEventArgs>? handler = Volatile.Read(ref _syncProgressUiRequested);
        var args = new GameSaveSyncProgressEventArgs(localUser, (GameSaveFilesSyncState)state);
        if (handler is null)
        {
            args.Respond(GameSaveFilesUiProgressUserAction.Cancel);
            return;
        }

        handler.Invoke(null, args);
    }

    internal static unsafe void DispatchSyncFailed(IntPtr localUser, PFGameSaveFilesSyncState state, int hr)
    {
        EventHandler<GameSaveSyncFailedEventArgs>? handler = Volatile.Read(ref _syncFailedUiRequested);
        var args = new GameSaveSyncFailedEventArgs(localUser, (GameSaveFilesSyncState)state, hr);
        if (handler is null)
        {
            args.Respond(GameSaveFilesUiSyncFailedUserAction.Cancel);
            return;
        }

        handler.Invoke(null, args);
    }

    internal static unsafe void DispatchDeviceContention(
        IntPtr localUser, PFGameSaveDescriptor* local, PFGameSaveDescriptor* remote)
    {
        EventHandler<GameSaveActiveDeviceContentionEventArgs>? handler =
            Volatile.Read(ref _deviceContentionUiRequested);
        var args = new GameSaveActiveDeviceContentionEventArgs(
            localUser, Describe(local), Describe(remote));
        if (handler is null)
        {
            args.Respond(GameSaveFilesUiActiveDeviceContentionUserAction.Cancel);
            return;
        }

        handler.Invoke(null, args);
    }

    internal static unsafe void DispatchConflict(
        IntPtr localUser, PFGameSaveDescriptor* local, PFGameSaveDescriptor* remote)
    {
        EventHandler<GameSaveConflictEventArgs>? handler = Volatile.Read(ref _conflictUiRequested);
        var args = new GameSaveConflictEventArgs(localUser, Describe(local), Describe(remote));
        if (handler is null)
        {
            args.Respond(GameSaveFilesUiConflictUserAction.Cancel);
            return;
        }

        handler.Invoke(null, args);
    }

    internal static unsafe void DispatchOutOfStorage(IntPtr localUser, ulong requiredBytes)
    {
        EventHandler<GameSaveOutOfStorageEventArgs>? handler = Volatile.Read(ref _outOfStorageUiRequested);
        var args = new GameSaveOutOfStorageEventArgs(localUser, requiredBytes);
        if (handler is null)
        {
            args.Respond(GameSaveFilesUiOutOfStorageUserAction.Cancel);
            return;
        }

        handler.Invoke(null, args);
    }

    private static unsafe GameSaveDescriptor? Describe(PFGameSaveDescriptor* value) =>
        value is null ? null : GameSaveDescriptor.FromNative(value);

    // Called under Gate. The five UI callbacks are installed as one set, so the first subscription
    // to any of them registers all five; the dispatchers cancel prompts nobody listens for.
    private static unsafe void EnsureUiCallbacks()
    {
        if (_uiCallbacksInstalled)
        {
            return;
        }

        PFGameSaveUICallbacks callbacks = default;
        callbacks.ProgressCallback = Trampolines.GameSaveUiProgressCallback;
        callbacks.SyncFailedCallback = Trampolines.GameSaveUiSyncFailedCallback;
        callbacks.ActiveDeviceContentionCallback = Trampolines.GameSaveUiActiveDeviceContentionCallback;
        callbacks.ConflictCallback = Trampolines.GameSaveUiConflictCallback;
        callbacks.OutOfStorageCallback = Trampolines.GameSaveUiOutOfStorageCallback;
        Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesSetUiCallbacks(&callbacks));
        _uiCallbacksInstalled = true;
    }

    private static IntPtr Require(PlayFabLocalUser user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return user.Handle;
    }

    private static unsafe int StartUninitialize(IntPtr block) =>
        NativePlayFab.PFGameSaveFilesUninitializeAsync((XAsyncBlock*)block);

    private static unsafe int GetUninitializeStatus(IntPtr block) =>
        NativePlayFab.PFGameSaveFilesUninitializeResult((XAsyncBlock*)block);
}
