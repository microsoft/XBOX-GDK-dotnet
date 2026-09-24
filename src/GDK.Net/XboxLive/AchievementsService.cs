using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// One page of achievements, plus the means to fetch the next. Wraps
/// <c>XblAchievementsResultHandle</c>.
/// </summary>
/// <remarks>
/// <para>
/// The native handle owns the achievement data, so <see cref="Achievements"/> is materialized once
/// at construction and every element is a full managed copy. That means a page stays readable after
/// it is disposed, only <see cref="GetNextAsync"/> needs the live handle.
/// </para>
/// <para>
/// This is <b>not</b> the reclaimed-batch pattern the <c>*_manager</c> layers use; each page is an
/// independently owned handle, so pages may be held concurrently.
/// </para>
/// </remarks>
public sealed class AchievementsPage : IDisposable
{
    private readonly AchievementsResultHandle _handle;
    private readonly GameTaskQueue? _queue;
    private bool _disposed;

    internal unsafe AchievementsPage(AchievementsResultHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;

        XblAchievement* items;
        nuint count;
        Hr.ThrowIfFailed(
            NativeXbl.XblAchievementsResultGetAchievements(handle.DangerousGetHandle(), &items, &count));

        var achievements = new Achievement[(int)count];
        for (int i = 0; i < achievements.Length; i++)
        {
            achievements[i] = Achievement.FromNative(items + i);
        }

        Achievements = new ReadOnlyCollection<Achievement>(achievements);

        byte hasNext;
        Hr.ThrowIfFailed(NativeXbl.XblAchievementsResultHasNext(handle.DangerousGetHandle(), &hasNext));
        HasNext = hasNext != 0;
    }

    /// <summary>The achievements in this page (<c>XblAchievementsResultGetAchievements</c>).</summary>
    public IReadOnlyList<Achievement> Achievements { get; }

    /// <summary>
    /// Whether another page is available (<c>XblAchievementsResultHasNext</c>). Cached at
    /// construction; it cannot change for a given page.
    /// </summary>
    public bool HasNext { get; }

    /// <summary>
    /// Fetches the next page (<c>XblAchievementsResultGetNextAsync</c>).
    /// </summary>
    /// <param name="maxItems">
    /// Maximum achievements to return. 0 (the default) lets the service choose.
    /// </param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <exception cref="InvalidOperationException"><see cref="HasNext"/> is <see langword="false"/>.</exception>
    public unsafe Task<AchievementsPage> GetNextAsync(
        uint maxItems = 0,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (!HasNext)
        {
            throw new InvalidOperationException("There are no more achievement pages to fetch.");
        }

        IntPtr handle = _handle.DangerousGetHandle();
        GameTaskQueue? queue = _queue;

        return AsyncOperation<AchievementsPage>.RunAsync(
            queue.RawHandle(),
            block => NativeXbl.XblAchievementsResultGetNextAsync(handle, maxItems, (XAsyncBlock*)block),
            (IntPtr block, out AchievementsPage value) =>
            {
                value = null!;

                IntPtr raw;
                int hr = NativeXbl.XblAchievementsResultGetNextResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new AchievementsPage(new AchievementsResultHandle(raw), queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Enumerates this page and every page after it, fetching each on demand.
    /// </summary>
    /// <remarks>
    /// Each page is disposed once the page after it has been fetched, so the caller only has to
    /// dispose the page it started from. The <see cref="Achievement"/> objects handed out remain
    /// valid because they are managed copies.
    /// </remarks>
    public async Task<IReadOnlyList<Achievement>> ReadAllAsync(
        uint maxItemsPerPage = 0,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        var all = new List<Achievement>(Achievements);
        AchievementsPage current = this;

        while (current.HasNext)
        {
            cancellationToken.ThrowIfCancellationRequested();

            AchievementsPage next = await current.GetNextAsync(maxItemsPerPage, cancellationToken)
                .ConfigureAwait(false);

            if (!ReferenceEquals(current, this))
            {
                current.Dispose();
            }

            all.AddRange(next.Achievements);
            current = next;
        }

        if (!ReferenceEquals(current, this))
        {
            current.Dispose();
        }

        return new ReadOnlyCollection<Achievement>(all.ToArray());
    }

    /// <summary>Releases the native result handle (<c>XblAchievementsResultCloseHandle</c>).</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle.Dispose();
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(AchievementsPage));
        }
    }
}

/// <summary>
/// Achievement queries and progress updates. Reached through
/// <see cref="XboxLiveContext.Achievements"/>. Mirrors <c>achievements_c.h</c>.
/// </summary>
/// <remarks>
/// <para>
/// A title normally only writes progress for the signed-in user, and the service enforces that. The
/// query APIs take an explicit Xbox user id because reading another player's achievements is a
/// legitimate scenario for a leaderboard or profile card.
/// </para>
/// <para>
/// <c>XblAchievementUnlockAddNotificationHandler</c> is declared only for non-GDK platforms and is
/// not exported by the thunks DLL, so unlock notifications are unavailable here.
/// <see cref="ProgressChanged"/>, which is RTA-backed, is exported and works.
/// </para>
/// </remarks>
public sealed unsafe class AchievementsService
{
    private readonly XboxLiveContext _context;

    internal AchievementsService(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Gets a single achievement by id (<c>XblAchievementsGetAchievementAsync</c>).
    /// </summary>
    /// <param name="xboxUserId">The user whose progress to read.</param>
    /// <param name="serviceConfigurationId">The SCID the achievement is defined in.</param>
    /// <param name="achievementId">The achievement's id.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <returns>
    /// A single-entry page. The caller owns it and should dispose it, though the
    /// <see cref="Achievement"/> it yields outlives disposal.
    /// </returns>
    public Task<AchievementsPage> GetAsync(
        ulong xboxUserId,
        string serviceConfigurationId,
        string achievementId,
        CancellationToken cancellationToken = default)
    {
        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }

        if (string.IsNullOrEmpty(achievementId))
        {
            throw new ArgumentException("An achievement id is required.", nameof(achievementId));
        }

        IntPtr context = _context.Handle;
        GameTaskQueue? queue = _context.Queue;
        IntPtr scid = Utf8.Allocate(serviceConfigurationId);
        IntPtr id = IntPtr.Zero;

        try
        {
            id = Utf8.Allocate(achievementId);

            return AsyncOperation<AchievementsPage>.RunAsync(
                queue.RawHandle(),
                block => NativeXbl.XblAchievementsGetAchievementAsync(
                    context,
                    xboxUserId,
                    (byte*)scid,
                    (byte*)id,
                    (XAsyncBlock*)block),
                (IntPtr block, out AchievementsPage value) =>
                {
                    Utf8.Free(scid);
                    Utf8.Free(id);

                    IntPtr raw;
                    int hr = NativeXbl.XblAchievementsGetAchievementResult((XAsyncBlock*)block, &raw);
                    return ToPage(hr, raw, queue, out value);
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(scid);
            Utf8.Free(id);
            throw;
        }
    }

    /// <summary>
    /// Gets a page of achievements for a title
    /// (<c>XblAchievementsGetAchievementsForTitleIdAsync</c>).
    /// </summary>
    /// <param name="xboxUserId">The user whose progress to read.</param>
    /// <param name="titleId">The title to read achievements for.</param>
    /// <param name="type">Which kinds of achievement to include.</param>
    /// <param name="unlockedOnly">When <see langword="true"/>, only achievements the user has earned.</param>
    /// <param name="orderBy">Sort order.</param>
    /// <param name="skipItems">How many achievements to skip: the paging offset.</param>
    /// <param name="maxItems">Maximum achievements per page; 0 lets the service choose.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<AchievementsPage> GetForTitleAsync(
        ulong xboxUserId,
        uint titleId,
        AchievementType type = AchievementType.All,
        bool unlockedOnly = false,
        AchievementOrderBy orderBy = AchievementOrderBy.Default,
        uint skipItems = 0,
        uint maxItems = 0,
        CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        GameTaskQueue? queue = _context.Queue;

        return AsyncOperation<AchievementsPage>.RunAsync(
            queue.RawHandle(),
            block => NativeXbl.XblAchievementsGetAchievementsForTitleIdAsync(
                context,
                xboxUserId,
                titleId,
                (XblAchievementType)type,
                unlockedOnly ? (byte)1 : (byte)0,
                (XblAchievementOrderBy)orderBy,
                skipItems,
                maxItems,
                (XAsyncBlock*)block),
            (IntPtr block, out AchievementsPage value) =>
            {
                IntPtr raw;
                int hr = NativeXbl.XblAchievementsGetAchievementsForTitleIdResult((XAsyncBlock*)block, &raw);
                return ToPage(hr, raw, queue, out value);
            },
            cancellationToken);
    }

    /// <summary>
    /// Reports progress towards an achievement (<c>XblAchievementsUpdateAchievementAsync</c>).
    /// </summary>
    /// <param name="xboxUserId">The user to record progress for.</param>
    /// <param name="achievementId">The achievement's id.</param>
    /// <param name="percentComplete">Progress from 0 to 100. 100 unlocks the achievement.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <remarks>
    /// The service treats a repeated or lower value as a no-op, so a title may call this freely
    /// without tracking what it last sent.
    /// </remarks>
    public Task UpdateAsync(
        ulong xboxUserId,
        string achievementId,
        uint percentComplete,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(achievementId))
        {
            throw new ArgumentException("An achievement id is required.", nameof(achievementId));
        }

        if (percentComplete > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(percentComplete),
                percentComplete,
                "Progress is a percentage from 0 to 100.");
        }

        IntPtr context = _context.Handle;
        IntPtr id = Utf8.Allocate(achievementId);

        try
        {
            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblAchievementsUpdateAchievementAsync(
                    context,
                    xboxUserId,
                    (byte*)id,
                    percentComplete,
                    (XAsyncBlock*)block),
                block =>
                {
                    Utf8.Free(id);
                    return HResult.SOk;
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(id);
            throw;
        }
    }

    /// <summary>
    /// Reports progress towards an achievement defined by another title
    /// (<c>XblAchievementsUpdateAchievementForTitleIdAsync</c>).
    /// </summary>
    /// <remarks>
    /// Only needed when the achievement lives in a different title or service configuration than
    /// the running one; otherwise use
    /// <see cref="UpdateAsync(ulong, string, uint, CancellationToken)"/>.
    /// </remarks>
    public Task UpdateForTitleAsync(
        ulong xboxUserId,
        uint titleId,
        string serviceConfigurationId,
        string achievementId,
        uint percentComplete,
        CancellationToken cancellationToken = default)
    {
        if (serviceConfigurationId is null)
        {
            throw new ArgumentNullException(nameof(serviceConfigurationId));
        }

        if (string.IsNullOrEmpty(achievementId))
        {
            throw new ArgumentException("An achievement id is required.", nameof(achievementId));
        }

        if (percentComplete > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(percentComplete),
                percentComplete,
                "Progress is a percentage from 0 to 100.");
        }

        IntPtr context = _context.Handle;
        IntPtr scid = Utf8.Allocate(serviceConfigurationId);
        IntPtr id = IntPtr.Zero;

        try
        {
            id = Utf8.Allocate(achievementId);

            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblAchievementsUpdateAchievementForTitleIdAsync(
                    context,
                    xboxUserId,
                    titleId,
                    (byte*)scid,
                    (byte*)id,
                    percentComplete,
                    (XAsyncBlock*)block),
                block =>
                {
                    Utf8.Free(scid);
                    Utf8.Free(id);
                    return HResult.SOk;
                },
                cancellationToken);
        }
        catch
        {
            Utf8.Free(scid);
            Utf8.Free(id);
            throw;
        }
    }

    /// <summary>
    /// Raised when the service reports progress on an achievement
    /// (<c>XblAchievementsAddAchievementProgressChangeHandler</c>).
    /// </summary>
    /// <remarks>
    /// Backed by real-time activity, so it only fires while an RTA connection is live. The native
    /// registration is created on the first subscription and released on the last, so a title that
    /// never subscribes pays nothing.
    /// </remarks>
    public event EventHandler<AchievementProgressChangedEventArgs>? ProgressChanged
    {
        add => AchievementProgressRegistry.Add(_context, value);
        remove => AchievementProgressRegistry.Remove(_context, value);
    }

    private static int ToPage(int hr, IntPtr raw, GameTaskQueue? queue, out AchievementsPage value)
    {
        if (HResult.Failed(hr))
        {
            value = null!;
            return hr;
        }

        value = new AchievementsPage(new AchievementsResultHandle(raw), queue);
        return HResult.SOk;
    }
}

/// <summary>
/// One achievement whose progress changed. Mirrors <c>XblAchievementProgressChangeEntry</c>.
/// </summary>
public sealed class AchievementProgressChange
{
    internal AchievementProgressChange(
        string achievementId,
        AchievementProgressState progressState,
        AchievementProgression progression)
    {
        AchievementId = achievementId;
        ProgressState = progressState;
        Progression = progression;
    }

    /// <summary>The achievement's id.</summary>
    public string AchievementId { get; }

    /// <summary>The new progress state.</summary>
    public AchievementProgressState ProgressState { get; }

    /// <summary>The new progress detail.</summary>
    public AchievementProgression Progression { get; }
}

/// <summary>
/// Payload for <see cref="AchievementsService.ProgressChanged"/>. Mirrors
/// <c>XblAchievementProgressChangeEventArgs</c>.
/// </summary>
public sealed class AchievementProgressChangedEventArgs : EventArgs
{
    internal AchievementProgressChangedEventArgs(IReadOnlyList<AchievementProgressChange> changes) =>
        Changes = changes;

    /// <summary>The achievements whose progress changed.</summary>
    public IReadOnlyList<AchievementProgressChange> Changes { get; }
}
