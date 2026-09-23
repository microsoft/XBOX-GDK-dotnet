using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>
/// Owns the native <c>XblAchievementsAddAchievementProgressChangeHandler</c> registrations that back
/// <see cref="AchievementsService.ProgressChanged"/>.
/// </summary>
/// <remarks>
/// <para>
/// XSAPI's notification handlers are per-<c>XblContextHandle</c>, take a raw context pointer rather
/// than an opaque token, and are unregistered by an <c>XblFunctionContext</c> integer. This type
/// exists to reconcile that with .NET events: at most one native registration per
/// <see cref="XboxLiveContext"/> exists, it is created on the first subscription and removed on the
/// last, and the callback's <c>void*</c> context is a <see cref="GCHandle"/> key rather than a
/// pointer to a managed object, which cannot be pinned.
/// </para>
/// <para>
/// The handler is delivered on an XSAPI-internal thread, not on the projection's task queue: XSAPI
/// notification handlers take no queue parameter, unlike the Gaming Runtime's event registrations.
/// Handlers must therefore be thread-safe or marshal to the game thread themselves.
/// </para>
/// </remarks>
internal sealed class AchievementProgressRegistry : IXboxLiveHandlerRegistry
{
    private static readonly ConcurrentDictionary<IntPtr, AchievementProgressRegistry> Registrations = new();

    private static readonly ConditionalWeakTable<XboxLiveContext, AchievementProgressRegistry> ByContext =
        new();

    private readonly XboxLiveContext _context;
    private readonly object _gate = new();

    private EventHandler<AchievementProgressChangedEventArgs>? _handlers;
    private GCHandle _self;
    private int _functionContext;
    private bool _registered;

    private AchievementProgressRegistry(XboxLiveContext context) => _context = context;

    internal static void Add(XboxLiveContext context, EventHandler<AchievementProgressChangedEventArgs>? handler)
    {
        if (handler is null)
        {
            return;
        }

        AchievementProgressRegistry registry = ByContext.GetValue(
            context,
            static ctx => new AchievementProgressRegistry(ctx));

        lock (registry._gate)
        {
            registry._handlers += handler;
            registry.EnsureRegistered();
        }
    }

    internal static void Remove(XboxLiveContext context, EventHandler<AchievementProgressChangedEventArgs>? handler)
    {
        if (handler is null || !ByContext.TryGetValue(context, out AchievementProgressRegistry? registry))
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

    /// <summary>Entry point used by <see cref="Trampolines"/> when XSAPI reports progress.</summary>
    internal static unsafe void Dispatch(IntPtr eventArgs, IntPtr context)
    {
        if (!Registrations.TryGetValue(context, out AchievementProgressRegistry? registry))
        {
            return;
        }

        EventHandler<AchievementProgressChangedEventArgs>? handlers;
        lock (registry._gate)
        {
            handlers = registry._handlers;
        }

        if (handlers is null)
        {
            return;
        }

        var args = (XblAchievementProgressChangeEventArgs*)eventArgs;
        int count = args is null ? 0 : (int)args->EntryCount;
        var changes = new AchievementProgressChange[count];
        for (int i = 0; i < count; i++)
        {
            XblAchievementProgressChangeEntry* entry = args->UpdatedAchievementEntries + i;
            changes[i] = new AchievementProgressChange(
                Utf8.ToString(entry->AchievementId) ?? string.Empty,
                (AchievementProgressState)entry->ProgressState,
                Achievement.ReadProgression(entry->Progression));
        }

        handlers(
            registry._context,
            new AchievementProgressChangedEventArgs(
                new ReadOnlyCollection<AchievementProgressChange>(changes)));
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

        // Unlike the Gaming Runtime's registration APIs this one returns the XblFunctionContext
        // directly rather than an HRESULT, and reports failure as 0.
        int functionContext = NativeXbl.XblAchievementsAddAchievementProgressChangeHandler(
            _context.Handle,
            Trampolines.AchievementProgressChangeHandler,
            key);

        if (functionContext == 0)
        {
            Registrations.TryRemove(key, out _);
            _self.Free();
            throw new GameRuntimeException(
                HResult.EFail,
                "Xbox Live rejected the achievement progress-change registration.");
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
                NativeXbl.XblAchievementsRemoveAchievementProgressChangeHandler(
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
