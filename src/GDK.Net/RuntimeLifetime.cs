using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using GDK.Net.Interop;

namespace GDK.Net;

/// <summary>
/// The fixed order in which the process-wide native libraries are brought up. Teardown is the exact
/// reverse.
/// </summary>
/// <remarks>
/// The order is a fixed property of the projection rather than a consequence of the order a title
/// happens to construct things in, because two of these libraries fault or hang when they are
/// sequenced wrongly. See <see cref="RuntimeLifetime"/>.
/// </remarks>
internal enum SubsystemOrder
{
    /// <summary>The Gaming Runtime itself (<c>XGameRuntimeInitialize</c>). Always first.</summary>
    GameRuntime = 0,

    /// <summary>PlayFab Core and Services (<c>PFInitialize</c>, <c>PFServicesInitialize</c>).</summary>
    PlayFabCore = 1,

    /// <summary>Lobby and matchmaking (<c>PFMultiplayerInitialize</c>).</summary>
    PlayFabMultiplayer = 2,

    /// <summary>Party networking and chat (<c>PartyInitialize</c>). Always last up, first down.</summary>
    PlayFabParty = 3,
}

/// <summary>
/// Owns the initialization order of the GDK's process-wide native libraries and guarantees that a
/// title's process can always exit.
/// </summary>
/// <remarks>
/// <para>
/// <b>Why an explicit order.</b> Three of these libraries have order-dependent failures that are
/// silent at the point of the mistake and only surface much later:
/// </para>
/// <list type="bullet">
/// <item>
/// <description>
/// <c>PFMultiplayerInitialize</c> before the Gaming Runtime is up returns <c>S_OK</c> and a
/// usable-looking handle, but its PubSub worker never runs, so <c>PFMultiplayerUninitialize</c>
/// later parks forever in a poll loop that has no timeout.
/// </description>
/// </item>
/// <item>
/// <description>
/// Leaving Party initialized at process exit terminates the process with
/// <c>STATUS_STACK_BUFFER_OVERRUN</c> (<c>0xC0000409</c>) rather than exiting cleanly.
/// </description>
/// </item>
/// <item>
/// <description>
/// PlayFab's shutdown is asynchronous and runs on the process default task queue, so it has to
/// complete before <c>XGameRuntimeUninitialize</c> tears that queue down.
/// </description>
/// </item>
/// </list>
/// <para>
/// So rather than document an ordering contract and let each title rediscover it, the projection
/// enforces it: every subsystem checks that the Gaming Runtime is up before it starts, registers
/// itself here, and <see cref="GameRuntime.Dispose"/> tears the live ones down in reverse
/// <see cref="SubsystemOrder"/>.
/// </para>
/// <para>
/// <b>Why the teardown is bounded.</b> The ordering rules above are enforceable, but they are not
/// sufficient: a native teardown call can still block indefinitely for reasons outside the title's
/// control. <c>PFMultiplayerUninitialize</c> in particular waits on an unbounded
/// <c>while (pending &gt; 0) Sleep(...)</c> loop with no failure path. A managed <c>Dispose</c>
/// must not be able to wedge a process, so <see cref="RunBounded"/> runs such calls on a
/// background thread and abandons the thread if it overruns. The subsystem is marked disposed
/// either way, and the process is left able to exit.
/// </para>
/// </remarks>
internal static class RuntimeLifetime
{
    /// <summary>
    /// How long a single native teardown call may run before it is abandoned. Generous enough for a
    /// real shutdown that is merely slow, short enough that a hang does not look like a crash.
    /// </summary>
    internal static readonly TimeSpan TeardownTimeout = TimeSpan.FromSeconds(5);

    private static readonly object Gate = new();
    private static readonly List<Entry> Live = new();

    private sealed record Entry(SubsystemOrder Order, IDisposable Subsystem);

    /// <summary>
    /// Whether the Gaming Runtime is currently initialized, as reported by the runtime itself.
    /// </summary>
    /// <remarks>
    /// Deliberately a live probe of native state rather than a flag this assembly maintains:
    /// <c>XGameRuntimeIsFeatureAvailable</c> answers <see langword="false"/> before
    /// <c>XGameRuntimeInitialize</c> and after <c>XGameRuntimeUninitialize</c>, so it stays correct
    /// when the runtime was initialized by a native host rather than through
    /// <see cref="GameRuntime.Initialize()"/>.
    /// </remarks>
    internal static bool IsGameRuntimeInitialized
    {
        get
        {
            try
            {
                return Native.XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature.XTaskQueue) != 0;
            }
            catch (DllNotFoundException)
            {
                // No Gaming Runtime at all. Not this check's error to report: let the caller's own
                // native call fail with the message that actually describes the problem.
                return true;
            }
            catch (EntryPointNotFoundException)
            {
                return true;
            }
        }
    }

    /// <summary>
    /// Throws unless the Gaming Runtime is up, so an ordering mistake fails at the call that made
    /// it rather than at an unrelated teardown much later.
    /// </summary>
    /// <param name="api">The native entry point about to be called, for the message.</param>
    internal static void RequireGameRuntime(string api)
    {
        if (IsGameRuntimeInitialized)
        {
            return;
        }

        throw new InvalidOperationException(
            $"{api} requires the Gaming Runtime to be initialized first. Call GameRuntime.Initialize() " +
            "before this, and dispose the GameRuntime last. Starting this library early is not " +
            "reported as an error by the GDK, but leaves it unable to shut down.");
    }

    /// <summary>Records a live subsystem so <see cref="DisposeAll"/> can close it at exit.</summary>
    internal static void Register(SubsystemOrder order, IDisposable subsystem)
    {
        lock (Gate)
        {
            Live.Add(new Entry(order, subsystem));
        }
    }

    /// <summary>Forgets a subsystem the title disposed itself.</summary>
    internal static void Unregister(IDisposable subsystem)
    {
        lock (Gate)
        {
            for (int i = Live.Count - 1; i >= 0; i--)
            {
                if (ReferenceEquals(Live[i].Subsystem, subsystem))
                {
                    Live.RemoveAt(i);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Disposes every subsystem the title left running, in reverse <see cref="SubsystemOrder"/>.
    /// </summary>
    /// <remarks>
    /// Called from <see cref="GameRuntime.Dispose"/>. A title that disposes its own objects finds
    /// nothing here to do; one that does not still exits cleanly instead of faulting in
    /// <c>XGameRuntimeUninitialize</c> or at process teardown.
    /// </remarks>
    internal static void DisposeAll()
    {
        Entry[] pending;
        lock (Gate)
        {
            pending = Live.ToArray();
            Live.Clear();
        }

        Array.Sort(pending, static (x, y) => y.Order.CompareTo(x.Order));

        foreach (Entry entry in pending)
        {
            try
            {
                entry.Subsystem.Dispose();
            }
            catch
            {
                // Shutdown is best-effort: one subsystem's failure must not strand the rest, and
                // the individual Dispose has already bounded itself.
            }
        }
    }

    /// <summary>
    /// Runs a native teardown call that is known to be able to block forever, giving up rather
    /// than wedging the process.
    /// </summary>
    /// <param name="teardown">The native call.</param>
    /// <param name="timeout">How long to allow it.</param>
    /// <returns>
    /// <see langword="true"/> if it completed, <see langword="false"/> if it overran and was
    /// abandoned.
    /// </returns>
    /// <remarks>
    /// The thread is a background thread, so abandoning it does not keep the process alive; the
    /// CLR tears it down at exit. The native library's state is unknowable at that point, which is
    /// exactly why the caller must treat the subsystem as gone and never touch its handle again.
    /// </remarks>
    internal static bool RunBounded(Action teardown, TimeSpan timeout)
    {
        ExceptionDispatchInfo? failure = null;
        using var finished = new ManualResetEventSlim(false);

        var thread = new Thread(() =>
        {
            try
            {
                teardown();
            }
            catch (Exception ex)
            {
                failure = ExceptionDispatchInfo.Capture(ex);
            }
            finally
            {
                // The event may already be disposed if this thread was abandoned and the call
                // returned later anyway; there is nothing left to signal in that case.
                try
                {
                    finished.Set();
                }
                catch (ObjectDisposedException)
                {
                }
            }
        })
        {
            IsBackground = true,
            Name = "GDK.Net native teardown",
        };

        thread.Start();

        if (!finished.Wait(timeout))
        {
            return false;
        }

        failure?.Throw();
        return true;
    }
}
