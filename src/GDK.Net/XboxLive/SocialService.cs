using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// One page of social relationships, plus the means to fetch the next. Wraps
/// <c>XblSocialRelationshipResultHandle</c>.
/// </summary>
/// <remarks>
/// The native handle owns the relationship data, so <see cref="Relationships"/> is materialized at
/// construction and every element is a full managed copy. The current page stays readable after it
/// is disposed; only <see cref="GetNextAsync"/> needs the live handle.
/// </remarks>
public sealed class SocialRelationshipsPage : IDisposable
{
    private readonly SocialRelationshipResultHandle _handle;
    private readonly XboxLiveContext _context;
    private readonly GameTaskQueue? _queue;
    private bool _disposed;

    internal unsafe SocialRelationshipsPage(
        SocialRelationshipResultHandle handle,
        XboxLiveContext context,
        GameTaskQueue? queue)
    {
        _handle = handle;
        _context = context;
        _queue = queue;

        XblSocialRelationship* items;
        nuint count;
        Hr.ThrowIfFailed(
            NativeXbl.XblSocialRelationshipResultGetRelationships(handle.DangerousGetHandle(), &items, &count));

        var relationships = new SocialRelationship[(int)count];
        for (int i = 0; i < relationships.Length; i++)
        {
            relationships[i] = SocialRelationship.FromNative(items + i);
        }

        Relationships = new ReadOnlyCollection<SocialRelationship>(relationships);

        byte hasNext;
        Hr.ThrowIfFailed(NativeXbl.XblSocialRelationshipResultHasNext(handle.DangerousGetHandle(), &hasNext));
        HasNext = hasNext != 0;

        nuint totalCount;
        Hr.ThrowIfFailed(NativeXbl.XblSocialRelationshipResultGetTotalCount(handle.DangerousGetHandle(), &totalCount));
        TotalCount = (ulong)totalCount;
    }

    /// <summary>The relationships in this page.</summary>
    public IReadOnlyList<SocialRelationship> Relationships { get; }

    /// <summary>The total number of relationships matching the query.</summary>
    public ulong TotalCount { get; }

    /// <summary>Whether another page is available.</summary>
    public bool HasNext { get; }

    /// <summary>
    /// Fetches the next page (<c>XblSocialRelationshipResultGetNextAsync</c>).
    /// </summary>
    /// <param name="maxItems">Maximum relationships to return. 0 lets the service choose.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <exception cref="InvalidOperationException"><see cref="HasNext"/> is <see langword="false"/>.</exception>
    public unsafe Task<SocialRelationshipsPage> GetNextAsync(
        ulong maxItems = 0,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (!HasNext)
        {
            throw new InvalidOperationException("There are no more social relationship pages to fetch.");
        }

        IntPtr context = _context.Handle;
        IntPtr handle = _handle.DangerousGetHandle();
        GameTaskQueue? queue = _queue;
        nuint nativeMaxItems = SocialService.ToNativeSize(maxItems, nameof(maxItems));

        return AsyncOperation<SocialRelationshipsPage>.RunAsync(
            queue.RawHandle(),
            block => NativeXbl.XblSocialRelationshipResultGetNextAsync(
                context,
                handle,
                nativeMaxItems,
                (XAsyncBlock*)block),
            (IntPtr block, out SocialRelationshipsPage value) =>
            {
                value = null!;

                IntPtr raw;
                int hr = NativeXbl.XblSocialRelationshipResultGetNextResult((XAsyncBlock*)block, &raw);
                if (HResult.Failed(hr))
                {
                    return hr;
                }

                value = new SocialRelationshipsPage(new SocialRelationshipResultHandle(raw), _context, queue);
                return HResult.SOk;
            },
            cancellationToken);
    }

    /// <summary>
    /// Enumerates this page and every page after it, fetching each on demand.
    /// </summary>
    /// <param name="maxItemsPerPage">Maximum relationships per fetched page. 0 lets the service choose.</param>
    /// <param name="cancellationToken">Cancels any page fetch; surfaces as <see cref="OperationCanceledException"/>.</param>
    public async Task<IReadOnlyList<SocialRelationship>> ReadAllAsync(
        ulong maxItemsPerPage = 0,
        CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        var all = new List<SocialRelationship>(Relationships);
        SocialRelationshipsPage current = this;

        while (current.HasNext)
        {
            cancellationToken.ThrowIfCancellationRequested();

            SocialRelationshipsPage next = await current.GetNextAsync(maxItemsPerPage, cancellationToken)
                .ConfigureAwait(false);

            if (!ReferenceEquals(current, this))
            {
                current.Dispose();
            }

            all.AddRange(next.Relationships);
            current = next;
        }

        if (!ReferenceEquals(current, this))
        {
            current.Dispose();
        }

        return new ReadOnlyCollection<SocialRelationship>(all.ToArray());
    }

    /// <summary>Releases the native result handle.</summary>
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
            throw new ObjectDisposedException(nameof(SocialRelationshipsPage));
        }
    }
}


/// <summary>
/// Social graph queries, reputation feedback and relationship-change notifications. Mirrors
/// <c>social_c.h</c>.
/// </summary>
/// <remarks>
/// Relationship-change events are backed by real-time activity and are delivered on an
/// XSAPI-internal thread, not on the projection's <see cref="GameTaskQueue"/>.
/// </remarks>
public sealed unsafe class SocialService
{
    private readonly XboxLiveContext _context;

    internal SocialService(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Gets a page of people socially connected to a user
    /// (<c>XblSocialGetSocialRelationshipsAsync</c>).
    /// </summary>
    /// <param name="xboxUserId">The Xbox user id whose relationships to read.</param>
    /// <param name="filter">Which relationships to include.</param>
    /// <param name="startIndex">The zero-based starting index.</param>
    /// <param name="maxItems">Maximum relationships to return. 0 lets the service choose.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task<SocialRelationshipsPage> GetRelationshipsAsync(
        ulong xboxUserId,
        SocialRelationshipFilter filter = SocialRelationshipFilter.All,
        ulong startIndex = 0,
        ulong maxItems = 0,
        CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        GameTaskQueue? queue = _context.Queue;
        nuint nativeStartIndex = ToNativeSize(startIndex, nameof(startIndex));
        nuint nativeMaxItems = ToNativeSize(maxItems, nameof(maxItems));

        return AsyncOperation<SocialRelationshipsPage>.RunAsync(
            queue.RawHandle(),
            block => NativeXbl.XblSocialGetSocialRelationshipsAsync(
                context,
                xboxUserId,
                (XblSocialRelationshipFilter)filter,
                nativeStartIndex,
                nativeMaxItems,
                (XAsyncBlock*)block),
            (IntPtr block, out SocialRelationshipsPage value) =>
            {
                IntPtr raw;
                int hr = NativeXbl.XblSocialGetSocialRelationshipsResult((XAsyncBlock*)block, &raw);
                return ToPage(hr, raw, _context, queue, out value);
            },
            cancellationToken);
    }

    /// <summary>
    /// Submits reputation feedback for one user (<c>XblSocialSubmitReputationFeedbackAsync</c>).
    /// </summary>
    /// <param name="xboxUserId">The Xbox user id to submit feedback about.</param>
    /// <param name="feedbackType">The kind of feedback to submit.</param>
    /// <param name="sessionReference">Optional MPSD session the feedback relates to.</param>
    /// <param name="reasonMessage">Optional user-supplied explanation.</param>
    /// <param name="evidenceResourceId">Optional resource id for supporting evidence.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    public Task SubmitReputationFeedbackAsync(
        ulong xboxUserId,
        ReputationFeedbackType feedbackType,
        SocialMultiplayerSessionReference? sessionReference = null,
        string? reasonMessage = null,
        string? evidenceResourceId = null,
        CancellationToken cancellationToken = default)
    {
        IntPtr context = _context.Handle;
        IntPtr reason = Utf8.Allocate(reasonMessage ?? string.Empty);
        IntPtr evidence = Utf8.Allocate(evidenceResourceId);
        IntPtr session = IntPtr.Zero;
        XblMultiplayerSessionReference* sessionPointer = null;

        try
        {
            if (sessionReference is not null)
            {
                session = Marshal.AllocHGlobal(sizeof(XblMultiplayerSessionReference));
                *(XblMultiplayerSessionReference*)session = sessionReference.ToNative();
                sessionPointer = (XblMultiplayerSessionReference*)session;
            }

            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblSocialSubmitReputationFeedbackAsync(
                    context,
                    xboxUserId,
                    (XblReputationFeedbackType)feedbackType,
                    sessionPointer,
                    (byte*)reason,
                    (byte*)evidence,
                    (XAsyncBlock*)block),
                block =>
                {
                    FreeSingleFeedback(reason, evidence, session);
                    return HResult.SOk;
                },
                cancellationToken);
        }
        catch
        {
            FreeSingleFeedback(reason, evidence, session);
            throw;
        }
    }

    /// <summary>
    /// Submits reputation feedback for several users
    /// (<c>XblSocialSubmitBatchReputationFeedbackAsync</c>).
    /// </summary>
    /// <param name="feedbackItems">The feedback items to submit.</param>
    /// <param name="cancellationToken">Cancels the call; surfaces as <see cref="OperationCanceledException"/>.</param>
    /// <exception cref="ArgumentException"><paramref name="feedbackItems"/> is empty.</exception>
    public Task SubmitBatchReputationFeedbackAsync(
        IEnumerable<ReputationFeedbackItem> feedbackItems,
        CancellationToken cancellationToken = default)
    {
        if (feedbackItems is null)
        {
            throw new ArgumentNullException(nameof(feedbackItems));
        }

        IntPtr context = _context.Handle;
        NativeReputationFeedbackBatch batch = NativeReputationFeedbackBatch.Create(feedbackItems);

        try
        {
            return AsyncOperation.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblSocialSubmitBatchReputationFeedbackAsync(
                    context,
                    batch.Items,
                    batch.Count,
                    (XAsyncBlock*)block),
                block =>
                {
                    batch.Dispose();
                    return HResult.SOk;
                },
                cancellationToken);
        }
        catch
        {
            batch.Dispose();
            throw;
        }
    }



    /// <summary>
    /// Raised when XSAPI reports a social relationship change
    /// (<c>XblSocialAddSocialRelationshipChangedHandler</c>).
    /// </summary>
    /// <remarks>
    /// The native registration is created on the first subscription and released on the last.
    /// Callbacks arrive on an XSAPI-internal thread.
    /// </remarks>
    public event EventHandler<SocialRelationshipChangedEventArgs>? RelationshipChanged
    {
        add => SocialRelationshipRegistry.Add(_context, value);
        remove => SocialRelationshipRegistry.Remove(_context, value);
    }

    internal static nuint ToNativeSize(ulong value, string paramName)
    {
        if (UIntPtr.Size == 4 && value > uint.MaxValue)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "The value does not fit in size_t.");
        }

        return (nuint)value;
    }

    private static int ToPage(
        int hr,
        IntPtr raw,
        XboxLiveContext context,
        GameTaskQueue? queue,
        out SocialRelationshipsPage value)
    {
        if (HResult.Failed(hr))
        {
            value = null!;
            return hr;
        }

        value = new SocialRelationshipsPage(new SocialRelationshipResultHandle(raw), context, queue);
        return HResult.SOk;
    }

    private static void FreeSingleFeedback(IntPtr reason, IntPtr evidence, IntPtr session)
    {
        Utf8.Free(reason);
        Utf8.Free(evidence);
        if (session != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(session);
        }
    }
}

/// <summary>
/// Owns the native <c>XblSocialAddSocialRelationshipChangedHandler</c> registrations that back
/// <see cref="SocialService.RelationshipChanged"/>.
/// </summary>
internal sealed class SocialRelationshipRegistry : IXboxLiveHandlerRegistry
{
    private static readonly ConcurrentDictionary<IntPtr, SocialRelationshipRegistry> Registrations = new();

    private static readonly ConditionalWeakTable<XboxLiveContext, SocialRelationshipRegistry> ByContext =
        new();

    private readonly XboxLiveContext _context;
    private readonly object _gate = new();

    private EventHandler<SocialRelationshipChangedEventArgs>? _handlers;
    private GCHandle _self;
    private int _functionContext;
    private bool _registered;

    private SocialRelationshipRegistry(XboxLiveContext context) => _context = context;

    internal static void Add(XboxLiveContext context, EventHandler<SocialRelationshipChangedEventArgs>? handler)
    {
        if (handler is null)
        {
            return;
        }

        SocialRelationshipRegistry registry = ByContext.GetValue(
            context,
            static ctx => new SocialRelationshipRegistry(ctx));

        lock (registry._gate)
        {
            registry._handlers += handler;
            registry.EnsureRegistered();
        }
    }

    internal static void Remove(XboxLiveContext context, EventHandler<SocialRelationshipChangedEventArgs>? handler)
    {
        if (handler is null || !ByContext.TryGetValue(context, out SocialRelationshipRegistry? registry))
        {
            return;
        }

        lock (registry._gate)
        {
            registry._handlers -= handler;
            if (registry._handlers is null)
            {
                registry.Unregister();
            }
        }
    }

    /// <summary>Entry point used by <see cref="Trampolines"/> when XSAPI reports a social change.</summary>
    internal static unsafe void Dispatch(IntPtr eventArgs, IntPtr context)
    {
        if (!Registrations.TryGetValue(context, out SocialRelationshipRegistry? registry))
        {
            return;
        }

        EventHandler<SocialRelationshipChangedEventArgs>? handlers;
        lock (registry._gate)
        {
            handlers = registry._handlers;
        }

        if (handlers is null)
        {
            return;
        }

        var args = (XblSocialRelationshipChangeEventArgs*)eventArgs;
        int count = args is null ? 0 : (int)args->XboxUserIdsCount;
        var xuids = new ulong[count];
        for (int i = 0; i < xuids.Length; i++)
        {
            xuids[i] = args->XboxUserIds is null ? 0 : args->XboxUserIds[i];
        }

        handlers(
            registry._context,
            new SocialRelationshipChangedEventArgs(
                args is null ? 0 : args->CallerXboxUserId,
                args is null ? SocialNotificationType.Unknown : (SocialNotificationType)args->SocialNotification,
                new ReadOnlyCollection<ulong>(xuids)));
    }

    private void EnsureRegistered()
    {
        if (_registered)
        {
            return;
        }

        _self = GCHandle.Alloc(this);
        IntPtr key = GCHandle.ToIntPtr(_self);
        Registrations[key] = this;

        int functionContext = NativeXbl.XblSocialAddSocialRelationshipChangedHandler(
            _context.Handle,
            Trampolines.SocialRelationshipChangedHandler,
            key);

        if (functionContext == 0)
        {
            Registrations.TryRemove(key, out _);
            _self.Free();
            throw new GameRuntimeException(
                HResult.EFail,
                "Xbox Live rejected the social relationship-change registration.");
        }

        _functionContext = functionContext;
        _registered = true;
        _context.TrackHandlerRegistry(this);
    }

    /// <inheritdoc />
    public void DetachAll()
    {
        lock (_gate)
        {
            _handlers = null;
            try
            {
                Unregister();
            }
            catch (GameRuntimeException)
            {
            }
        }
    }

    private void Unregister()
    {
        if (!_registered)
        {
            return;
        }

        _registered = false;

        try
        {
            Hr.ThrowIfFailed(
                NativeXbl.XblSocialRemoveSocialRelationshipChangedHandler(
                    _context.Handle,
                    _functionContext));
        }
        finally
        {
            if (_self.IsAllocated)
            {
                Registrations.TryRemove(GCHandle.ToIntPtr(_self), out _);
                _self.Free();
            }
        }
    }
}

internal sealed unsafe class NativeReputationFeedbackBatch : IDisposable
{
    private readonly List<IntPtr> _strings = new();
    private IntPtr _items;
    private IntPtr _sessions;
    private int _count;
    private bool _disposed;

    private NativeReputationFeedbackBatch()
    {
    }

    internal XblReputationFeedbackItem* Items => (XblReputationFeedbackItem*)_items;

    internal nuint Count => (nuint)_count;

    internal static NativeReputationFeedbackBatch Create(IEnumerable<ReputationFeedbackItem> feedbackItems)
    {
        var managed = new List<ReputationFeedbackItem>();
        foreach (ReputationFeedbackItem item in feedbackItems)
        {
            if (item is null)
            {
                throw new ArgumentException("Feedback items cannot contain null.", nameof(feedbackItems));
            }

            managed.Add(item);
        }

        if (managed.Count == 0)
        {
            throw new ArgumentException("At least one feedback item is required.", nameof(feedbackItems));
        }

        var batch = new NativeReputationFeedbackBatch();
        try
        {
            batch._count = managed.Count;
            batch._items = Marshal.AllocHGlobal(sizeof(XblReputationFeedbackItem) * managed.Count);
            batch._sessions = Marshal.AllocHGlobal(sizeof(XblMultiplayerSessionReference) * managed.Count);

            var nativeItems = (XblReputationFeedbackItem*)batch._items;
            var nativeSessions = (XblMultiplayerSessionReference*)batch._sessions;
            for (int i = 0; i < managed.Count; i++)
            {
                ReputationFeedbackItem item = managed[i];
                nativeItems[i] = default;
                nativeSessions[i] = default;

                nativeItems[i].XboxUserId = item.XboxUserId;
                nativeItems[i].FeedbackType = (XblReputationFeedbackType)item.FeedbackType;

                if (item.SessionReference is not null)
                {
                    nativeSessions[i] = item.SessionReference.ToNative();
                    nativeItems[i].SessionReference = nativeSessions + i;
                }

                IntPtr reason = Utf8.Allocate(item.ReasonMessage);
                batch._strings.Add(reason);
                nativeItems[i].ReasonMessage = (byte*)reason;

                IntPtr evidence = Utf8.Allocate(item.EvidenceResourceId);
                if (evidence != IntPtr.Zero)
                {
                    batch._strings.Add(evidence);
                }

                nativeItems[i].EvidenceResourceId = (byte*)evidence;
            }

            return batch;
        }
        catch
        {
            batch.Dispose();
            throw;
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        foreach (IntPtr value in _strings)
        {
            Utf8.Free(value);
        }

        if (_items != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_items);
            _items = IntPtr.Zero;
        }

        if (_sessions != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_sessions);
            _sessions = IntPtr.Zero;
        }
    }
}
