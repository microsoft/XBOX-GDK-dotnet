using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Sort direction for cached achievements-manager queries.</summary>
public enum AchievementsManagerSortOrder : uint
{
    /// <summary>Do not sort the cached results.</summary>
    Unsorted = 0,

    /// <summary>Sort in ascending order by the selected achievement field.</summary>
    Ascending = 1,

    /// <summary>Sort in descending order by the selected achievement field.</summary>
    Descending = 2,
}

/// <summary>Kind of event returned by <see cref="AchievementsManager.DoWork"/>.</summary>
public enum AchievementsManagerEventType : uint
{
    /// <summary>A local user's initial cached achievement state has finished syncing.</summary>
    LocalUserInitialStateSynced = 0,

    /// <summary>An achievement was unlocked.</summary>
    AchievementUnlocked = 1,

    /// <summary>An achievement's progress changed.</summary>
    AchievementProgressUpdated = 2,
}

/// <summary>
/// One event returned by <see cref="AchievementsManager.DoWork"/>.
/// </summary>
/// <remarks>
/// Derived records model the native <c>XblAchievementsManagerEventType</c> variants. The list
/// returned from <see cref="AchievementsManager.DoWork"/> keeps completions and notifications
/// interleaved in the exact order XSAPI returned them.
/// </remarks>
public abstract record AchievementsManagerEvent
{
    private protected AchievementsManagerEvent(AchievementsManagerEventType type, ulong xboxUserId)
    {
        Type = type;
        XboxUserId = xboxUserId;
    }

    /// <summary>The native event variant.</summary>
    public AchievementsManagerEventType Type { get; }

    /// <summary>The Xbox user id the event belongs to.</summary>
    public ulong XboxUserId { get; }
}

/// <summary>
/// A local user's initial achievement cache has finished syncing.
/// </summary>
/// <remarks>
/// Cached queries for this user are safe after this event has been returned by
/// <see cref="AchievementsManager.DoWork"/> and <see cref="AchievementsManager.IsUserInitialized"/>
/// returns <see langword="true"/>.
/// </remarks>
public sealed record AchievementsManagerLocalUserInitialStateSyncedEvent : AchievementsManagerEvent
{
    internal AchievementsManagerLocalUserInitialStateSyncedEvent(ulong xboxUserId)
        : base(AchievementsManagerEventType.LocalUserInitialStateSynced, xboxUserId)
    {
    }
}

/// <summary>An achievement was unlocked for a local user.</summary>
public sealed record AchievementsManagerAchievementUnlockedEvent : AchievementsManagerEvent
{
    internal AchievementsManagerAchievementUnlockedEvent(ulong xboxUserId, AchievementProgressChange progress)
        : base(AchievementsManagerEventType.AchievementUnlocked, xboxUserId)
    {
        Progress = progress;
    }

    /// <summary>The unlocked achievement and its final progress state.</summary>
    public AchievementProgressChange Progress { get; }
}

/// <summary>An achievement's progress changed for a local user.</summary>
public sealed record AchievementsManagerAchievementProgressUpdatedEvent : AchievementsManagerEvent
{
    internal AchievementsManagerAchievementProgressUpdatedEvent(ulong xboxUserId, AchievementProgressChange progress)
        : base(AchievementsManagerEventType.AchievementProgressUpdated, xboxUserId)
    {
        Progress = progress;
    }

    /// <summary>The achievement whose progress changed.</summary>
    public AchievementProgressChange Progress { get; }
}

/// <summary>
/// The result of a synchronous achievements-manager cache query.
/// </summary>
/// <remarks>
/// <para>
/// Native <c>XblAchievementsManagerResultHandle</c> instances are reference-counted and must be
/// closed. This wrapper owns one such handle and closes it from <see cref="Dispose"/>.
/// </para>
/// <para>
/// The handle owns a native <c>XblAchievement</c> array. <see cref="Achievements"/> is fully
/// snapshotted during construction, reusing the same <see cref="Achievement"/> type returned by
/// <see cref="AchievementsService"/>, so achievements remain readable after this result is disposed.
/// </para>
/// </remarks>
public sealed class AchievementsManagerResult : IDisposable
{
    private readonly AchievementsManagerResultHandle? _handle;
    private bool _disposed;

    internal AchievementsManagerResult()
    {
        Achievements = Array.Empty<Achievement>();
    }

    internal unsafe AchievementsManagerResult(AchievementsManagerResultHandle handle)
    {
        _handle = handle;

        XblAchievement* items;
        ulong count;
        Hr.ThrowIfFailed(
            NativeXbl.XblAchievementsManagerResultGetAchievements(handle.DangerousGetHandle(), &items, &count));

        var achievements = new Achievement[checked((int)count)];
        for (int i = 0; i < achievements.Length; i++)
        {
            achievements[i] = Achievement.FromNative(items + i);
        }

        Achievements = new ReadOnlyCollection<Achievement>(achievements);
    }

    /// <summary>The achievements in this cached-query result.</summary>
    public IReadOnlyList<Achievement> Achievements { get; }

    /// <summary>
    /// Creates another managed owner for the same native result handle
    /// (<c>XblAchievementsManagerResultDuplicateHandle</c>).
    /// </summary>
    /// <remarks>
    /// The duplicate snapshots its own <see cref="Achievements"/> list during construction. Use this
    /// only when two components need independent disposable result lifetimes; ordinary callers can
    /// keep the <see cref="Achievement"/> objects instead.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">This result has been disposed.</exception>
    public AchievementsManagerResult Duplicate()
    {
        ThrowIfDisposed();
        return _handle is null
            ? new AchievementsManagerResult()
            : new AchievementsManagerResult(_handle.Duplicate());
    }

    /// <summary>Closes the native result handle.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle?.Dispose();
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(AchievementsManagerResult));
        }
    }
}
