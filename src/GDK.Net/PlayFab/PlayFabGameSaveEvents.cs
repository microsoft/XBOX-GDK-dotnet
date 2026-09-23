using System;
using GDK.Net.Interop;

namespace GDK.Net.PlayFab;

/// <summary>
/// How far a PlayFab game save sync has progressed
/// (<c>PFGameSaveFilesUiProgressGetProgress</c>).
/// </summary>
public readonly struct GameSaveSyncProgress : IEquatable<GameSaveSyncProgress>
{
    internal GameSaveSyncProgress(GameSaveFilesSyncState state, ulong current, ulong total)
    {
        State = state;
        CurrentBytes = current;
        TotalBytes = total;
    }

    /// <summary>The stage the sync is currently in.</summary>
    public GameSaveFilesSyncState State { get; }

    /// <summary>Bytes transferred so far.</summary>
    public ulong CurrentBytes { get; }

    /// <summary>Bytes the sync will transfer in total, or zero when it is not yet known.</summary>
    public ulong TotalBytes { get; }

    /// <summary>The completed fraction in the range 0 to 1, or zero when the total is unknown.</summary>
    public double Fraction => TotalBytes == 0 ? 0d : (double)CurrentBytes / TotalBytes;

    /// <inheritdoc/>
    public bool Equals(GameSaveSyncProgress other) =>
        State == other.State && CurrentBytes == other.CurrentBytes && TotalBytes == other.TotalBytes;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is GameSaveSyncProgress other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() =>
        ((int)State * 397) ^ (CurrentBytes.GetHashCode() * 31) ^ TotalBytes.GetHashCode();

    /// <summary>Compares two progress snapshots for equality.</summary>
    public static bool operator ==(GameSaveSyncProgress left, GameSaveSyncProgress right) =>
        left.Equals(right);

    /// <summary>Compares two progress snapshots for inequality.</summary>
    public static bool operator !=(GameSaveSyncProgress left, GameSaveSyncProgress right) =>
        !left.Equals(right);
}

/// <summary>
/// Base class for the PlayFab game save notifications, which all identify the local user the
/// notification is about.
/// </summary>
/// <remarks>
/// The native callbacks hand back a borrowed <c>PFLocalUserHandle</c>. Rather than surface a
/// handle, or duplicate one the title would have to dispose, these arguments only let the title
/// ask which of its own <see cref="PlayFabLocalUser"/> instances the notification belongs to.
/// </remarks>
public abstract class GameSaveEventArgs : EventArgs
{
    private readonly IntPtr _localUser;

    private protected GameSaveEventArgs(IntPtr localUser)
    {
        _localUser = localUser;
    }

    private protected IntPtr LocalUserHandle => _localUser;

    /// <summary>
    /// Whether this notification is about <paramref name="user"/>
    /// (<c>PFLocalUserHandleCompare</c>).
    /// </summary>
    public bool IsFor(PlayFabLocalUser user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return NativePlayFab.PFLocalUserHandleCompare(_localUser, user.Handle) == 0;
    }
}

/// <summary>
/// Raised when another device took over as the active device for a user's save, which means the
/// title should return to its main menu (<c>PFGameSaveFilesActiveDeviceChangedCallback</c>).
/// </summary>
public sealed class GameSaveActiveDeviceChangedEventArgs : GameSaveEventArgs
{
    internal GameSaveActiveDeviceChangedEventArgs(IntPtr localUser, GameSaveDescriptor? activeDevice)
        : base(localUser)
    {
        ActiveDevice = activeDevice;
    }

    /// <summary>The save now owned by the new active device, when the runtime supplied one.</summary>
    public GameSaveDescriptor? ActiveDevice { get; }
}

/// <summary>
/// Base class for the notifications that ask the title to show a game save dialog and report the
/// user's choice back with <c>Respond</c>.
/// </summary>
/// <remarks>
/// The sync stays blocked until a response is given, so every handler must respond exactly once,
/// even if only with the cancel action.
/// </remarks>
public abstract class GameSaveUserActionEventArgs : GameSaveEventArgs
{
    private int _responded;

    private protected GameSaveUserActionEventArgs(IntPtr localUser)
        : base(localUser)
    {
    }

    /// <summary>Whether a response has already been given.</summary>
    public bool HasResponded => System.Threading.Volatile.Read(ref _responded) != 0;

    private protected void MarkResponded()
    {
        if (System.Threading.Interlocked.Exchange(ref _responded, 1) != 0)
        {
            throw new InvalidOperationException(
                "This game save prompt has already been responded to.");
        }
    }
}

/// <summary>
/// Raised while a save is syncing so the title can show progress
/// (<c>PFGameSaveFilesUiProgressCallback</c>).
/// </summary>
public sealed class GameSaveSyncProgressEventArgs : GameSaveUserActionEventArgs
{
    internal GameSaveSyncProgressEventArgs(IntPtr localUser, GameSaveFilesSyncState state)
        : base(localUser)
    {
        State = state;
    }

    /// <summary>The stage the sync has reached.</summary>
    public GameSaveFilesSyncState State { get; }

    /// <summary>
    /// Reads the current byte counts for this sync (<c>PFGameSaveFilesUiProgressGetProgress</c>).
    /// </summary>
    public GameSaveSyncProgress GetProgress() =>
        PlayFabGameSaveFiles.GetProgress(LocalUserHandle);

    /// <summary>
    /// Dismisses the progress dialog with the user's choice
    /// (<c>PFGameSaveFilesSetUiProgressResponse</c>).
    /// </summary>
    public void Respond(GameSaveFilesUiProgressUserAction action)
    {
        MarkResponded();
        Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesSetUiProgressResponse(
            LocalUserHandle, (PFGameSaveFilesUiProgressUserAction)action));
    }
}

/// <summary>
/// Raised when a sync failed and the title must ask the user how to continue
/// (<c>PFGameSaveFilesUiSyncFailedCallback</c>).
/// </summary>
public sealed class GameSaveSyncFailedEventArgs : GameSaveUserActionEventArgs
{
    private readonly int _hr;

    internal GameSaveSyncFailedEventArgs(IntPtr localUser, GameSaveFilesSyncState state, int hr)
        : base(localUser)
    {
        State = state;
        _hr = hr;
    }

    /// <summary>The stage the sync failed in.</summary>
    public GameSaveFilesSyncState State { get; }

    /// <summary>Why the sync failed.</summary>
    public Exception Error => Hr.ToException(_hr);

    /// <summary>
    /// Dismisses the dialog with the user's choice
    /// (<c>PFGameSaveFilesSetUiSyncFailedResponse</c>).
    /// </summary>
    public void Respond(GameSaveFilesUiSyncFailedUserAction action)
    {
        MarkResponded();
        Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesSetUiSyncFailedResponse(
            LocalUserHandle, (PFGameSaveFilesUiSyncFailedUserAction)action));
    }
}

/// <summary>
/// Raised when another device is already the active device for this save
/// (<c>PFGameSaveFilesUiActiveDeviceContentionCallback</c>).
/// </summary>
public sealed class GameSaveActiveDeviceContentionEventArgs : GameSaveUserActionEventArgs
{
    internal GameSaveActiveDeviceContentionEventArgs(
        IntPtr localUser, GameSaveDescriptor? local, GameSaveDescriptor? remote)
        : base(localUser)
    {
        LocalGameSave = local;
        RemoteGameSave = remote;
    }

    /// <summary>The save on this device.</summary>
    public GameSaveDescriptor? LocalGameSave { get; }

    /// <summary>The save on the device that currently owns the slot.</summary>
    public GameSaveDescriptor? RemoteGameSave { get; }

    /// <summary>
    /// Dismisses the dialog with the user's choice
    /// (<c>PFGameSaveFilesSetUiActiveDeviceContentionResponse</c>).
    /// </summary>
    public void Respond(GameSaveFilesUiActiveDeviceContentionUserAction action)
    {
        MarkResponded();
        Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesSetUiActiveDeviceContentionResponse(
            LocalUserHandle, (PFGameSaveFilesUiActiveDeviceContentionUserAction)action));
    }
}

/// <summary>
/// Raised when the local and cloud saves diverged and the user must pick one
/// (<c>PFGameSaveFilesUiConflictCallback</c>).
/// </summary>
public sealed class GameSaveConflictEventArgs : GameSaveUserActionEventArgs
{
    internal GameSaveConflictEventArgs(
        IntPtr localUser, GameSaveDescriptor? local, GameSaveDescriptor? remote)
        : base(localUser)
    {
        LocalGameSave = local;
        RemoteGameSave = remote;
    }

    /// <summary>The save on this device.</summary>
    public GameSaveDescriptor? LocalGameSave { get; }

    /// <summary>The save in the cloud.</summary>
    public GameSaveDescriptor? RemoteGameSave { get; }

    /// <summary>
    /// Dismisses the dialog with the user's choice
    /// (<c>PFGameSaveFilesSetUiConflictResponse</c>).
    /// </summary>
    public void Respond(GameSaveFilesUiConflictUserAction action)
    {
        MarkResponded();
        Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesSetUiConflictResponse(
            LocalUserHandle, (PFGameSaveFilesUiConflictUserAction)action));
    }
}

/// <summary>
/// Raised when the user's cloud save quota cannot hold the pending upload
/// (<c>PFGameSaveFilesUiOutOfStorageCallback</c>).
/// </summary>
public sealed class GameSaveOutOfStorageEventArgs : GameSaveUserActionEventArgs
{
    internal GameSaveOutOfStorageEventArgs(IntPtr localUser, ulong requiredBytes)
        : base(localUser)
    {
        RequiredBytes = requiredBytes;
    }

    /// <summary>How many additional bytes of quota the upload needs.</summary>
    public ulong RequiredBytes { get; }

    /// <summary>
    /// Dismisses the dialog with the user's choice
    /// (<c>PFGameSaveFilesSetUiOutOfStorageResponse</c>).
    /// </summary>
    public void Respond(GameSaveFilesUiOutOfStorageUserAction action)
    {
        MarkResponded();
        Hr.ThrowIfFailed(NativePlayFab.PFGameSaveFilesSetUiOutOfStorageResponse(
            LocalUserHandle, (PFGameSaveFilesUiOutOfStorageUserAction)action));
    }
}
