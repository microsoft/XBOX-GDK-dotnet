using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using GDK.Net;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for the fixed startup order and the bounded teardown that guarantees a title's
/// process can always exit.
/// </summary>
[Collection(RuntimeCollection.Name)]
public class RuntimeLifetimeTests
{
    private sealed class Recorder : IDisposable
    {
        private readonly List<string> _log;
        private readonly string _name;

        public Recorder(List<string> log, string name)
        {
            _log = log;
            _name = name;
        }

        public int DisposeCount { get; private set; }

        public void Dispose()
        {
            DisposeCount++;
            _log.Add(_name);
        }
    }

    private sealed class Thrower : IDisposable
    {
        public void Dispose() => throw new InvalidOperationException("teardown failed");
    }

    // ─── The fixed order ────────────────────────────────────────────────────────

    [Fact]
    public void SubsystemOrderRunsGameRuntimeFirstAndPartyLast()
    {
        // The order is a contract the native libraries depend on, not an implementation detail:
        // Party must come down before the Gaming Runtime it was started under.
        Assert.True(SubsystemOrder.GameRuntime < SubsystemOrder.PlayFabCore);
        Assert.True(SubsystemOrder.PlayFabCore < SubsystemOrder.PlayFabMultiplayer);
        Assert.True(SubsystemOrder.PlayFabMultiplayer < SubsystemOrder.PlayFabParty);
    }

    [Fact]
    public void DisposeAllTearsSubsystemsDownInReverseOrder()
    {
        var log = new List<string>();
        var multiplayer = new Recorder(log, "multiplayer");
        var party = new Recorder(log, "party");

        // Registered in startup order; teardown must be the exact reverse regardless.
        RuntimeLifetime.Register(SubsystemOrder.PlayFabMultiplayer, multiplayer);
        RuntimeLifetime.Register(SubsystemOrder.PlayFabParty, party);

        RuntimeLifetime.DisposeAll();

        Assert.Equal(new[] { "party", "multiplayer" }, log);
    }

    [Fact]
    public void DisposeAllIgnoresSubsystemsThatWereAlreadyDisposed()
    {
        var log = new List<string>();
        var subsystem = new Recorder(log, "multiplayer");

        RuntimeLifetime.Register(SubsystemOrder.PlayFabMultiplayer, subsystem);
        RuntimeLifetime.Unregister(subsystem);

        RuntimeLifetime.DisposeAll();

        Assert.Empty(log);
        Assert.Equal(0, subsystem.DisposeCount);
    }

    [Fact]
    public void DisposeAllKeepsGoingWhenOneSubsystemFails()
    {
        // Shutdown is best-effort: a library that fails on the way down must not strand the ones
        // that would otherwise have exited cleanly.
        var log = new List<string>();
        var survivor = new Recorder(log, "multiplayer");

        RuntimeLifetime.Register(SubsystemOrder.PlayFabMultiplayer, survivor);
        RuntimeLifetime.Register(SubsystemOrder.PlayFabParty, new Thrower());

        RuntimeLifetime.DisposeAll();

        Assert.Equal(new[] { "multiplayer" }, log);
    }

    [Fact]
    public void DisposeAllIsIdempotent()
    {
        var log = new List<string>();
        var subsystem = new Recorder(log, "party");

        RuntimeLifetime.Register(SubsystemOrder.PlayFabParty, subsystem);

        RuntimeLifetime.DisposeAll();
        RuntimeLifetime.DisposeAll();

        Assert.Equal(1, subsystem.DisposeCount);
    }

    // ─── The bounded teardown ───────────────────────────────────────────────────

    [Fact]
    public void RunBoundedReportsCompletionOfATeardownThatReturns()
    {
        bool ran = false;

        bool completed = RuntimeLifetime.RunBounded(() => ran = true, TimeSpan.FromSeconds(30));

        Assert.True(completed);
        Assert.True(ran);
    }

    [Fact]
    public void RunBoundedAbandonsATeardownThatNeverReturns()
    {
        // The whole point: PFMultiplayerUninitialize can park forever in a poll loop with no
        // timeout, and a managed Dispose must not be able to wedge the process because of it.
        using var release = new ManualResetEventSlim(false);
        var stopwatch = Stopwatch.StartNew();

        bool completed = RuntimeLifetime.RunBounded(
            () => release.Wait(TimeSpan.FromSeconds(60)), TimeSpan.FromMilliseconds(250));

        stopwatch.Stop();
        release.Set();

        Assert.False(completed);
        Assert.True(
            stopwatch.Elapsed < TimeSpan.FromSeconds(10),
            $"RunBounded waited {stopwatch.Elapsed} for a 250ms budget.");
    }

    [Fact]
    public void RunBoundedRethrowsWhatTheTeardownThrew()
    {
        // A teardown that fails outright is a real error the caller still has to see; only a
        // teardown that never returns is swallowed.
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
            () => RuntimeLifetime.RunBounded(
                () => throw new InvalidOperationException("boom"), TimeSpan.FromSeconds(30)));

        Assert.Equal("boom", ex.Message);
    }

    [Fact]
    public void RunBoundedUsesABackgroundThreadSoAnAbandonedCallCannotHoldTheProcessOpen()
    {
        bool? isBackground = null;
        using var observed = new ManualResetEventSlim(false);

        RuntimeLifetime.RunBounded(
            () =>
            {
                isBackground = Thread.CurrentThread.IsBackground;
                observed.Set();
            },
            TimeSpan.FromSeconds(30));

        Assert.True(observed.Wait(TimeSpan.FromSeconds(30)));
        Assert.True(isBackground);
    }

    [Fact]
    public void TeardownTimeoutIsBoundedAndNotInstant()
    {
        Assert.True(RuntimeLifetime.TeardownTimeout > TimeSpan.Zero);
        Assert.True(RuntimeLifetime.TeardownTimeout <= TimeSpan.FromSeconds(30));
    }
}
