using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.XboxLive;

/// <summary>
/// Process-global Xbox Live achievements manager. Mirrors <c>achievements_manager_c.h</c>.
/// </summary>
/// <remarks>
/// <para>
/// This is the cached, pumped achievements layer. Unlike <see cref="AchievementsService"/>, which
/// performs explicit asynchronous service queries through an <see cref="XboxLiveContext"/>, this
/// manager warms a local cache when <see cref="AddLocalUser"/> is called and then answers
/// <see cref="GetAchievement"/>, <see cref="GetAchievements"/> and
/// <see cref="GetAchievementsByState"/> synchronously from that cache.
/// </para>
/// <para>
/// The cache and unlock notifications advance only when the title calls <see cref="DoWork"/>,
/// ideally once per frame. After adding a local user, keep pumping until
/// <see cref="AchievementsManagerLocalUserInitialStateSyncedEvent"/> appears and
/// <see cref="IsUserInitialized"/> returns <see langword="true"/>; cached queries before then fail.
/// </para>
/// <para>
/// This manager is process-global, not per user and not per <see cref="XboxLiveContext"/>. Drive
/// <see cref="DoWork"/> and the other achievements-manager calls from one thread; XSAPI does not
/// make <see cref="DoWork"/> thread-safe against concurrent achievements-manager calls.
/// </para>
/// <para>
/// XSAPI differs from the PFMP and Party state-change pattern: <c>XblAchievementsManagerDoWork</c>
/// has no matching <c>Finish</c>. Its returned array is valid only until the next
/// <see cref="DoWork"/> call, so a borrowed C# iterator would dangle silently. This projection
/// deliberately snapshots the batch into managed <see cref="AchievementsManagerEvent"/> records
/// before returning it, while keeping the native event order intact.
/// </para>
/// </remarks>
public sealed unsafe class AchievementsManager
{
    private readonly XboxLiveService _service;

    internal AchievementsManager(XboxLiveService service) =>
        _service = service ?? throw new ArgumentNullException(nameof(service));

    /// <summary>
    /// Adds a local user and starts warming that user's cached achievements
    /// (<c>XblAchievementsManagerAddLocalUser</c>).
    /// </summary>
    /// <param name="user">The local user whose achievements should be cached.</param>
    /// <remarks>
    /// This call starts the cache warm-up but does not complete it synchronously. Call
    /// <see cref="DoWork"/> regularly and wait for
    /// <see cref="AchievementsManagerLocalUserInitialStateSyncedEvent"/> before using cached query
    /// methods for this user.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="user"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Xbox Live Services have not been initialized.</exception>
    public void AddLocalUser(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        ThrowIfNotInitialized();
        Hr.ThrowIfFailed(NativeXbl.XblAchievementsManagerAddLocalUser(user.Handle, IntPtr.Zero));
    }

    /// <summary>
    /// Removes a local user and immediately discards that user's cached achievements
    /// (<c>XblAchievementsManagerRemoveLocalUser</c>).
    /// </summary>
    /// <param name="user">The local user to remove from the achievements manager.</param>
    /// <exception cref="ArgumentNullException"><paramref name="user"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Xbox Live Services have not been initialized.</exception>
    public void RemoveLocalUser(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        ThrowIfNotInitialized();
        Hr.ThrowIfFailed(NativeXbl.XblAchievementsManagerRemoveLocalUser(user.Handle));
    }

    /// <summary>
    /// Returns whether a local user's initial cached state has finished syncing
    /// (<c>XblAchievementsManagerIsUserInitialized</c>).
    /// </summary>
    /// <param name="xboxUserId">The Xbox user id to check.</param>
    /// <returns>
    /// <see langword="true"/> when cached queries can be used for this user; otherwise keep calling
    /// <see cref="DoWork"/> until the local-user-initial-state event arrives.
    /// </returns>
    /// <exception cref="InvalidOperationException">Xbox Live Services have not been initialized.</exception>
    public bool IsUserInitialized(ulong xboxUserId)
    {
        ThrowIfNotInitialized();

        int hr = NativeXbl.XblAchievementsManagerIsUserInitialized(xboxUserId);
        if (hr == HResult.EFail)
        {
            return false;
        }

        Hr.ThrowIfFailed(hr);
        return true;
    }

    /// <summary>
    /// Pumps the achievements manager and returns any events produced since the previous pump
    /// (<c>XblAchievementsManagerDoWork</c>).
    /// </summary>
    /// <returns>
    /// A managed snapshot of the native event batch. Completions and notifications stay interleaved
    /// in native order.
    /// </returns>
    /// <remarks>
    /// Call this regularly, ideally once per frame. Nothing happens in the achievements manager:
    /// cache warming, progress updates or unlock notifications, unless this pump runs. Unlike
    /// PFMP and Party, XSAPI has no <c>Finish</c> call for this batch; the native array is valid
    /// only until the next pump, so this method copies every event before returning.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Xbox Live Services have not been initialized.</exception>
    public IReadOnlyList<AchievementsManagerEvent> DoWork()
    {
        ThrowIfNotInitialized();

        XblAchievementsManagerEvent* nativeEvents;
        nuint count;
        Hr.ThrowIfFailed(NativeXbl.XblAchievementsManagerDoWork(&nativeEvents, &count));

        if (nativeEvents is null || count == 0)
        {
            return Array.Empty<AchievementsManagerEvent>();
        }

        var events = new AchievementsManagerEvent[checked((int)count)];
        for (int i = 0; i < events.Length; i++)
        {
            events[i] = ReadEvent(nativeEvents + i);
        }

        return new ReadOnlyCollection<AchievementsManagerEvent>(events);
    }

    /// <summary>
    /// Gets the cached state of one achievement for a local user
    /// (<c>XblAchievementsManagerGetAchievement</c>).
    /// </summary>
    /// <param name="xboxUserId">The Xbox user id whose cache should be queried.</param>
    /// <param name="achievementId">The achievement id from Partner Center.</param>
    /// <returns>A disposable result whose <see cref="AchievementsManagerResult.Achievements"/> list is snapshotted.</returns>
    /// <remarks>
    /// This is a synchronous cache lookup, not a service call. If the user is not initialized yet,
    /// keep pumping <see cref="DoWork"/> and wait for
    /// <see cref="AchievementsManagerLocalUserInitialStateSyncedEvent"/>, or use
    /// <see cref="AchievementsService.GetAsync"/> when an explicit asynchronous service query is
    /// what you need.
    /// </remarks>
    /// <exception cref="ArgumentException"><paramref name="achievementId"/> is empty.</exception>
    /// <exception cref="InvalidOperationException">Xbox Live Services have not been initialized.</exception>
    public AchievementsManagerResult GetAchievement(ulong xboxUserId, string achievementId)
    {
        if (string.IsNullOrEmpty(achievementId))
        {
            throw new ArgumentException("An achievement id is required.", nameof(achievementId));
        }

        ThrowIfNotInitialized();

        IntPtr id = Utf8.Allocate(achievementId);
        try
        {
            IntPtr raw;
            int hr = NativeXbl.XblAchievementsManagerGetAchievement(xboxUserId, (byte*)id, &raw);
            return ToResult(hr, raw);
        }
        finally
        {
            Utf8.Free(id);
        }
    }

    /// <summary>
    /// Gets all cached achievements for a local user (<c>XblAchievementsManagerGetAchievements</c>).
    /// </summary>
    /// <param name="xboxUserId">The Xbox user id whose cache should be queried.</param>
    /// <param name="sortField">The achievement field to sort by.</param>
    /// <param name="sortOrder">The sort direction.</param>
    /// <returns>A disposable result whose <see cref="AchievementsManagerResult.Achievements"/> list is snapshotted.</returns>
    /// <remarks>
    /// This reads the achievements manager's local cache. Use <see cref="AchievementsService"/> for
    /// explicit asynchronous service queries or when the local cache has not been warmed.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Xbox Live Services have not been initialized.</exception>
    public AchievementsManagerResult GetAchievements(
        ulong xboxUserId,
        AchievementOrderBy sortField = AchievementOrderBy.Default,
        AchievementsManagerSortOrder sortOrder = AchievementsManagerSortOrder.Unsorted)
    {
        ThrowIfNotInitialized();

        IntPtr raw;
        int hr = NativeXbl.XblAchievementsManagerGetAchievements(
            xboxUserId,
            (XblAchievementOrderBy)sortField,
            (XblAchievementsManagerSortOrder)sortOrder,
            &raw);
        return ToResult(hr, raw);
    }

    /// <summary>
    /// Gets cached achievements in a specific progress state
    /// (<c>XblAchievementsManagerGetAchievementsByState</c>).
    /// </summary>
    /// <param name="xboxUserId">The Xbox user id whose cache should be queried.</param>
    /// <param name="achievementState">The progress state to include.</param>
    /// <param name="sortField">The achievement field to sort by.</param>
    /// <param name="sortOrder">The sort direction.</param>
    /// <returns>A disposable result whose <see cref="AchievementsManagerResult.Achievements"/> list is snapshotted.</returns>
    /// <remarks>
    /// This reads the achievements manager's local cache and fails until the user is initialized.
    /// Pump <see cref="DoWork"/> and wait for
    /// <see cref="AchievementsManagerLocalUserInitialStateSyncedEvent"/> first.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Xbox Live Services have not been initialized.</exception>
    public AchievementsManagerResult GetAchievementsByState(
        ulong xboxUserId,
        AchievementProgressState achievementState,
        AchievementOrderBy sortField = AchievementOrderBy.Default,
        AchievementsManagerSortOrder sortOrder = AchievementsManagerSortOrder.Unsorted)
    {
        ThrowIfNotInitialized();

        IntPtr raw;
        int hr = NativeXbl.XblAchievementsManagerGetAchievementsByState(
            xboxUserId,
            (XblAchievementOrderBy)sortField,
            (XblAchievementsManagerSortOrder)sortOrder,
            (XblAchievementProgressState)achievementState,
            &raw);
        return ToResult(hr, raw);
    }

    /// <summary>
    /// Updates cached achievement progress and unlocks at 100 percent
    /// (<c>XblAchievementsManagerUpdateAchievement</c>).
    /// </summary>
    /// <param name="xboxUserId">The Xbox user id to update.</param>
    /// <param name="achievementId">The achievement id from Partner Center.</param>
    /// <param name="currentProgress">Progress from 1 through 100. 100 unlocks the achievement.</param>
    /// <remarks>
    /// The update is not reflected locally immediately. Keep pumping <see cref="DoWork"/>; a later
    /// batch may contain <see cref="AchievementsManagerAchievementProgressUpdatedEvent"/> and, when
    /// the update unlocks the achievement, <see cref="AchievementsManagerAchievementUnlockedEvent"/>.
    /// This API can work offline after the user has previously been added while online. If that
    /// prerequisite is not met, use <see cref="AchievementsService.UpdateAsync"/> as a fallback.
    /// </remarks>
    /// <exception cref="ArgumentException"><paramref name="achievementId"/> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="currentProgress"/> is outside 1 through 100.</exception>
    /// <exception cref="InvalidOperationException">Xbox Live Services have not been initialized.</exception>
    public void UpdateAchievement(ulong xboxUserId, string achievementId, uint currentProgress)
    {
        if (string.IsNullOrEmpty(achievementId))
        {
            throw new ArgumentException("An achievement id is required.", nameof(achievementId));
        }

        if (currentProgress == 0 || currentProgress > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(currentProgress),
                currentProgress,
                "Progress is a percentage from 1 to 100.");
        }

        ThrowIfNotInitialized();

        IntPtr id = Utf8.Allocate(achievementId);
        try
        {
            Hr.ThrowIfFailed(NativeXbl.XblAchievementsManagerUpdateAchievement(
                xboxUserId,
                (byte*)id,
                (byte)currentProgress));
        }
        finally
        {
            Utf8.Free(id);
        }
    }

    private static AchievementsManagerResult ToResult(int hr, IntPtr raw)
    {
        Hr.ThrowIfFailed(hr);
        return raw == IntPtr.Zero
            ? new AchievementsManagerResult()
            : new AchievementsManagerResult(new AchievementsManagerResultHandle(raw));
    }

    private static AchievementsManagerEvent ReadEvent(XblAchievementsManagerEvent* native)
    {
        return native->EventType switch
        {
            XblAchievementsManagerEventType.LocalUserInitialStateSynced =>
                new AchievementsManagerLocalUserInitialStateSyncedEvent(native->XboxUserId),
            XblAchievementsManagerEventType.AchievementUnlocked =>
                new AchievementsManagerAchievementUnlockedEvent(
                    native->XboxUserId,
                    ReadProgressChange(&native->ProgressInfo)),
            XblAchievementsManagerEventType.AchievementProgressUpdated =>
                new AchievementsManagerAchievementProgressUpdatedEvent(
                    native->XboxUserId,
                    ReadProgressChange(&native->ProgressInfo)),
            _ => throw new GameRuntimeException(
                HResult.EFail,
                $"XSAPI returned an unknown achievements-manager event type {(uint)native->EventType}."),
        };
    }

    private static AchievementProgressChange ReadProgressChange(XblAchievementProgressChangeEntry* native)
    {
        return new AchievementProgressChange(
            Utf8.ToString(native->AchievementId) ?? string.Empty,
            (AchievementProgressState)native->ProgressState,
            Achievement.ReadProgression(native->Progression));
    }

    private void ThrowIfNotInitialized()
    {
        if (!_service.IsInitialized)
        {
            throw new InvalidOperationException(
                "Xbox Live Services are not initialized. Call GameRuntime.XboxLive.Initialize first.");
        }
    }
}
