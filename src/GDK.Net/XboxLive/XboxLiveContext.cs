using System;
using System.Collections.Generic;
using GDK.Net.Interop;
using GDK.Net.Users;

namespace GDK.Net.XboxLive;

/// <summary>
/// An Xbox Live context: the per-user, per-sign-in handle every Xbox Live service call is made
/// through. Wraps <c>XblContextHandle</c>.
/// </summary>
/// <remarks>
/// <para>
/// A context is built from a signed-in <see cref="User"/> and stops being valid the moment that
/// user signs out or is replaced, so it is <b>not</b> a process-lifetime object. The compliance
/// obligation is to dispose and rebuild it on user change; holding a stale context makes calls fail
/// against the previous identity.
/// </para>
/// <para>
/// The native handle is owned by a <see cref="System.Runtime.InteropServices.SafeHandle"/>, so a
/// missed <see cref="Dispose"/> still releases it at finalization. <see cref="Duplicate"/> produces
/// an independent instance for a component with its own lifespan, which is preferable to creating a
/// second context for the same user.
/// </para>
/// </remarks>
public sealed unsafe class XboxLiveContext : IDisposable
{
    private readonly XboxLiveContextHandle _handle;
    private readonly GameTaskQueue? _queue;
    private readonly List<IXboxLiveHandlerRegistry> _registries = new();
    private readonly object _registryGate = new();
    private bool _disposed;

    internal XboxLiveContext(XboxLiveContextHandle handle, GameTaskQueue? queue)
    {
        _handle = handle;
        _queue = queue;

        ulong xuid;
        Hr.ThrowIfFailed(NativeXbl.XblContextGetXboxUserId(handle.DangerousGetHandle(), &xuid));
        XboxUserId = xuid;

        Profiles = new ProfileService(this);
        Achievements = new AchievementsService(this);
        Social = new SocialService(this);
        Presence = new PresenceService(this);
        Privacy = new PrivacyService(this);
        Leaderboards = new LeaderboardService(this);
        RealTimeActivity = new RealTimeActivityService(this);
        UserStatistics = new UserStatisticsService(this);
        TitleStorage = new TitleStorageService(this);
        TitleManagedStatistics = new TitleManagedStatisticsService(this);
        StringVerification = new StringVerificationService(this);
        Events = new EventsService(this);
        MultiplayerActivity = new MultiplayerActivityService(this);
        Settings = new XboxLiveContextSettings(this);
    }

    /// <summary>
    /// The Xbox user id of the user this context was created for
    /// (<c>XblContextGetXboxUserId</c>). Cached; constant for the context's lifetime.
    /// </summary>
    public ulong XboxUserId { get; }

    /// <summary>Xbox Live profile lookups for this user.</summary>
    public ProfileService Profiles { get; }

    /// <summary>Achievement queries and progress updates for this user.</summary>
    public AchievementsService Achievements { get; }

    /// <summary>Friends, followers and reputation feedback for this user.</summary>
    public SocialService Social { get; }

    /// <summary>Presence reporting and lookups for this user and the people they know.</summary>
    public PresenceService Presence { get; }

    /// <summary>
    /// Privacy permission checks, and the avoid and mute lists. Consult this before enabling
    /// communications or user-generated content; the checks fail closed.
    /// </summary>
    public PrivacyService Privacy { get; }

    /// <summary>Leaderboard queries for this user's title.</summary>
    public LeaderboardService Leaderboards { get; }

    /// <summary>
    /// The Real-Time Activity connection. Activate it before subscribing to presence, social or
    /// statistic change notifications; those subscriptions are delivered over it.
    /// </summary>
    public RealTimeActivityService RealTimeActivity { get; }

    /// <summary>
    /// Statistic queries for this user, and the change subscriptions that keep them fresh over the
    /// Real-Time Activity connection.
    /// </summary>
    public UserStatisticsService UserStatistics { get; }

    /// <summary>
    /// Cloud-backed title storage: per-user, per-title and global blobs, transferred in chunks.
    /// </summary>
    public TitleStorageService TitleStorage { get; }

    /// <summary>
    /// Writes to title-managed statistics, the leaderboard-backing stats the service owns on the
    /// title's behalf.
    /// </summary>
    public TitleManagedStatisticsService TitleManagedStatistics { get; }

    /// <summary>
    /// String verification. Run every player-authored string through this before displaying it; the
    /// checks fail closed.
    /// </summary>
    public StringVerificationService StringVerification { get; }

    /// <summary>In-game events written to the title's event stream.</summary>
    public EventsService Events { get; }

    /// <summary>
    /// The activity this user is advertising, the invites sent from it, and the recent-players
    /// list. Keeping the advertised activity accurate is a certification obligation.
    /// </summary>
    public MultiplayerActivityService MultiplayerActivity { get; }

    /// <summary>HTTP and websocket tuning for calls made through this context.</summary>
    public XboxLiveContextSettings Settings { get; }

    internal IntPtr Handle
    {
        get
        {
            ThrowIfDisposed();
            return _handle.DangerousGetHandle();
        }
    }

    internal GameTaskQueue? Queue => _queue;

    /// <summary>
    /// Records a registry that has just taken out its first native notification-handler
    /// registration against this context, so <see cref="Dispose"/> can detach it again.
    /// </summary>
    internal void TrackHandlerRegistry(IXboxLiveHandlerRegistry registry)
    {
        lock (_registryGate)
        {
            if (!_registries.Contains(registry))
            {
                _registries.Add(registry);
            }
        }
    }

    /// <summary>
    /// Returns the user this context was created for (<c>XblContextGetUser</c>).
    /// </summary>
    /// <remarks>
    /// XSAPI duplicates the underlying user handle before returning it, so the returned
    /// <see cref="User"/> owns its own handle and must be disposed independently of this
    /// context.
    /// </remarks>
    public User GetUser()
    {
        IntPtr raw;
        Hr.ThrowIfFailed(NativeXbl.XblContextGetUser(Handle, &raw));
        return new User(new UserHandle(raw), _queue);
    }

    /// <summary>
    /// Returns an independent context backed by its own native handle
    /// (<c>XblContextDuplicateHandle</c>).
    /// </summary>
    public XboxLiveContext Duplicate()
    {
        IntPtr raw;
        Hr.ThrowIfFailed(NativeXbl.XblContextDuplicateHandle(Handle, &raw));
        return new XboxLiveContext(new XboxLiveContextHandle(raw), _queue);
    }

    /// <summary>Releases the native context handle (<c>XblContextCloseHandle</c>).</summary>
    /// <remarks>
    /// Any notification handler still registered against this context (presence, social,
    /// real-time activity, achievement progress) is removed first, while the handle is still
    /// valid. Leaving one attached would let XSAPI invoke it against a closed handle.
    /// </remarks>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        IXboxLiveHandlerRegistry[] registries;
        lock (_registryGate)
        {
            registries = _registries.ToArray();
            _registries.Clear();
        }

        foreach (IXboxLiveHandlerRegistry registry in registries)
        {
            registry.DetachAll();
        }

        _disposed = true;
        _handle.Dispose();
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(XboxLiveContext));
        }
    }
}

/// <summary>
/// HTTP and websocket tuning for one <see cref="XboxLiveContext"/>. Mirrors
/// <c>xbox_live_context_settings_c.h</c>.
/// </summary>
/// <remarks>
/// The defaults are what the service expects; changing them is a deliberate act, usually to cope
/// with a slow network. Every value is expressed as a <see cref="TimeSpan"/> here even though the
/// native surface takes whole seconds, so a sub-second value is rounded down.
/// </remarks>
public sealed unsafe class XboxLiveContextSettings
{
    private readonly XboxLiveContext _context;

    internal XboxLiveContextSettings(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Timeout applied to calls the service marks as long-running
    /// (<c>XblContextSettingsGet/SetLongHttpTimeout</c>).
    /// </summary>
    public TimeSpan LongHttpTimeout
    {
        get
        {
            uint seconds;
            Hr.ThrowIfFailed(NativeXbl.XblContextSettingsGetLongHttpTimeout(_context.Handle, &seconds));
            return TimeSpan.FromSeconds(seconds);
        }

        set => Hr.ThrowIfFailed(
            NativeXbl.XblContextSettingsSetLongHttpTimeout(_context.Handle, ToSeconds(value)));
    }

    /// <summary>
    /// Delay before a failed call is retried
    /// (<c>XblContextSettingsGet/SetHttpRetryDelay</c>).
    /// </summary>
    public TimeSpan HttpRetryDelay
    {
        get
        {
            uint seconds;
            Hr.ThrowIfFailed(NativeXbl.XblContextSettingsGetHttpRetryDelay(_context.Handle, &seconds));
            return TimeSpan.FromSeconds(seconds);
        }

        set => Hr.ThrowIfFailed(
            NativeXbl.XblContextSettingsSetHttpRetryDelay(_context.Handle, ToSeconds(value)));
    }

    /// <summary>
    /// Total window an HTTP call, including retries, may occupy
    /// (<c>XblContextSettingsGet/SetHttpTimeoutWindow</c>).
    /// </summary>
    public TimeSpan HttpTimeoutWindow
    {
        get
        {
            uint seconds;
            Hr.ThrowIfFailed(NativeXbl.XblContextSettingsGetHttpTimeoutWindow(_context.Handle, &seconds));
            return TimeSpan.FromSeconds(seconds);
        }

        set => Hr.ThrowIfFailed(
            NativeXbl.XblContextSettingsSetHttpTimeoutWindow(_context.Handle, ToSeconds(value)));
    }

    /// <summary>
    /// Total window a websocket connection attempt may occupy
    /// (<c>XblContextSettingsGet/SetWebsocketTimeoutWindow</c>).
    /// </summary>
    public TimeSpan WebsocketTimeoutWindow
    {
        get
        {
            uint seconds;
            Hr.ThrowIfFailed(NativeXbl.XblContextSettingsGetWebsocketTimeoutWindow(_context.Handle, &seconds));
            return TimeSpan.FromSeconds(seconds);
        }

        set => Hr.ThrowIfFailed(
            NativeXbl.XblContextSettingsSetWebsocketTimeoutWindow(_context.Handle, ToSeconds(value)));
    }

    /// <summary>
    /// Whether quality-of-service probes use the cross-platform servers
    /// (<c>XblContextSettingsGet/SetUseCrossPlatformQosServers</c>).
    /// </summary>
    public bool UseCrossPlatformQosServers
    {
        get
        {
            byte value;
            Hr.ThrowIfFailed(
                NativeXbl.XblContextSettingsGetUseCrossPlatformQosServers(_context.Handle, &value));
            return value != 0;
        }

        set => Hr.ThrowIfFailed(
            NativeXbl.XblContextSettingsSetUseCrossPlatformQosServers(
                _context.Handle,
                value ? (byte)1 : (byte)0));
    }

    private static uint ToSeconds(TimeSpan value)
    {
        if (value < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "The value cannot be negative.");
        }

        double seconds = value.TotalSeconds;
        return seconds >= uint.MaxValue ? uint.MaxValue : (uint)seconds;
    }
}
