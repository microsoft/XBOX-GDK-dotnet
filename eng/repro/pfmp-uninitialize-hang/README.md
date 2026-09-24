# `PFMultiplayerUninitialize` hangs when the library is initialized too early

A self-contained native repro, with no .NET involved, for a defect in the PlayFab Multiplayer
library shipped with Microsoft GDK edition **260400**.

## Summary

`PFMultiplayerInitialize` returns `S_OK` when the Game Core runtime has not been initialized, but
the handle it produces cannot be shut down: `PFMultiplayerUninitialize` never returns.

No PlayFab account, authentication, network traffic or lobby is needed. Two calls are enough.

```
PFMultiplayerInitialize(&config, &handle);   // -> S_OK
PFMultiplayerUninitialize(handle);           // never returns
```

Adding `XGameRuntimeInitialize()` before the first call makes the same sequence complete in about
100ms.

## Why it matters

The dependency is documented on `PFMultiplayerInitialize` but not enforced, and the failure is
silent and deferred: initialization reports success, the handle looks usable, and the consequence
only appears at shutdown, in a different call, as a hang rather than an error. In a real title the
runtime is usually up a few hundred milliseconds into process start, so this presents as an
intermittent shutdown hang that reproduces roughly one run in three under load or on a cold boot.

Suggested fix: fail `PFMultiplayerInitialize` with a distinct error when the Game Core runtime is
not initialized, and give the shutdown poll loop a timeout and a failure path.

## Building

Requires Microsoft GDK 260400 and a Visual Studio C++ toolset.

```
build.cmd
```

## Reproducing

```
pfmp-repro.exe --steps pfmp-init,pfmp-uninit
```

Exit code `2` means a step hung; `0` means every step returned. The process stays alive for the
timeout window after reporting a hang so a debugger can be attached.

`matrix.ps1` runs the full bisection:

```powershell
.\matrix.ps1
```

Observed on GDK 260400, Windows 11, x64: the hang is specific to the missing runtime
initialization, and Party is not involved in any way:

| Case                                 | Result |
| ------------------------------------ | ------ |
| A  pfmp alone, no `XGameRuntimeInitialize` | **HANG** (5/5) |
| B  pfmp alone, with game runtime     | clean  |
| C  pfmp alone, `gr-init` only        | clean  |
| D  Party initialized, then pfmp uninit | clean |
| E  `PartyCleanup` before pfmp uninit | clean  |
| F  Party first, then pfmp            | clean  |
| G  `XGameRuntimeUninitialize` before pfmp uninit | clean |
| H  pfmp idle 20s, then uninit        | clean  |
| I  full clean shutdown               | clean  |

The `probe` step reports whether the Gaming Runtime is observable from outside, which is what makes
the ordering guard in `GDK.Net` possible:

```
pfmp-repro.exe --steps probe,gr-init,probe,gr-uninit,probe
```

`XGameRuntimeIsFeatureAvailable` answers 0 before `XGameRuntimeInitialize`, 1 while it is up, and 0
again after `XGameRuntimeUninitialize`, so it is a reliable live probe of the real runtime state
regardless of who initialized it.

## Call stack

Captured with `cdb -p <pid> -pv -c "~*kv"` against the hung process. The thread is parked in a
sleep-poll loop inside the PubSub subscription manager: it is not blocked on a lock, and it
consumes almost no CPU (0.14s over seven minutes), so it is an unbounded retry rather than a
deadlock.

```
ntdll!NtDelayExecution
ntdll!RtlDelayExecution
KERNELBASE!SleepEx
MSVCP140!_Thrd_sleep
PlayFabMultiplayer!MuThreadImpl::Sleep
PlayFabMultiplayer!PubSubSubscriptionManager::Shutdown
PlayFabMultiplayer!PubSubApiImpl::Destroy
PlayFabMultiplayer!PubSubManagerCleanup
PlayFabMultiplayer!MultiplayerApiImpl::Destroy
PlayFabMultiplayer!PFMultiplayerUninitialize
```

An identical stack was captured from a hung .NET process using `GDK.Net`, which is how the two were
confirmed to be the same defect.

## Related: fastfail at process exit

Separately, leaving Party initialized at process exit (no `PartyCleanup`) terminates the process
with `0xC0000409` (`STATUS_STACK_BUFFER_OVERRUN` / fastfail). Reproduce with:

```
pfmp-repro.exe --steps gr-init,party-init
```

versus the clean:

```
pfmp-repro.exe --steps gr-init,party-init,party-cleanup
```

## Root cause in the SDK source

The loop is in `Xbox.Bumblelion` at `/src/pubsub/api/src/PubSubSubscriptionManager.cpp`:

```cpp
while (m_processingCallback.load() > 0)
{
    MuThread::Sleep(c_pubSubXTaskCallbackCompletionWaitTimeInMilliseconds);
}
XTaskQueueTerminate(m_workerQueue, false, nullptr, nullptr);
```

`m_processingCallback` is incremented at init and again on each self-resubmit in `DoWorkInternal`,
and decremented at the end of each `XTaskQueue` callback. If the queue never dispatches, the
counter never drains and the loop spins forever: there is no timeout and no failure path.

It was introduced by commit `b104ee46` (PR 13872395, 2025-10-01, "Ensure safe termination of
XTaskQueue by waiting for submitted tasks and callback completion"), itself compensating for
`a344f079` (PR 13767188), which had switched `XTaskQueueTerminate` to the no-wait flag to avoid a
Game Core suspend/resume freeze. So it is a bounded-wait-shaped problem that was solved with an
unbounded wait.

The branch `bmoraescobar/fix_plm_suspend_hang` no longer exists on the remote: presumably merged
or renamed, but the loop above is still present on `main`.

## How `GDK.Net` works around this

Two independent defences, because the ordering rule is enforceable but not sufficient:

1. **A fixed startup order, enforced.** `SubsystemOrder` in `src/GDK.Net/RuntimeLifetime.cs` fixes
   the order as Gaming Runtime → PlayFab Core → multiplayer → Party, and teardown as the exact
   reverse. `PFInitialize`, `PFMultiplayerInitialize` and `PartyInitialize` each check that the
   Gaming Runtime is up first, via `XGameRuntimeIsFeatureAvailable`, which is a live probe of the
   real runtime state rather than a flag the library keeps, so it stays correct when a native host
   did the initialization. An ordering mistake now throws at the call that made it instead of
   surfacing as a hang at an unrelated teardown much later. `PlayFabMultiplayer.Initialize` also
   polls `XNetworkingGetConnectivityHint` for `networkInitialized` (bounded to 10s); removing that
   reproduces the hang in the managed live harness in about one run in three.

2. **A bounded teardown.** `PFMultiplayerUninitialize` and `PartyCleanup` run on a background
   thread and are abandoned after five seconds, so a `Dispose` can never wedge a title's process
   however the native library behaves. `GameRuntime.Dispose` also tears down any subsystem the
   title left running, in reverse order, which additionally prevents the `0xC0000409` exit
   fastfail described above.

Note that the missing-runtime case only hangs reliably in a *native* process. In a .NET process the
same steps complete, and the managed hang instead needs real in-flight PubSub work, which is what
makes it a race in practice, and why the bound in (2) matters even with (1) in place.
