# .NET / C# Projection of the Microsoft GDK — Implementation Plan

> Idiomatic projection of the Microsoft GDK for **.NET**. Reads the native surface described in
> [`reference/gdk-surface.md`](./reference/gdk-surface.md) and implements the shared pilot in
> [`reference/xuser-pilot.md`](./reference/xuser-pilot.md). Goal: an API that feels like **modern
> C#** — `Task`/`await`, `IDisposable`, events, `[Flags]` — not a mechanical P/Invoke wrapper.
>
> This plan is also the **worked example** for the repository: other language plans mirror its
> structure and depth.

## At a glance

| Field | Value |
|---|---|
| **Status** | Authored |
| **Target language** | C# (.NET) |
| **Target runtimes** | `net8.0` (LTS), `net10.0` (LTS), `netstandard2.0` (MonoGame / broad reach) |
| **Architectures / RIDs** | `win-x64` (primary), `win-arm64` |
| **Primary use** | Shipping titles (**MonoGame** and **Stride**) **and** bespoke tooling/services |
| **Binding approach** | ClangSharp-generated raw P/Invoke + hand-authored idiomatic layer |
| **Pinned GDK edition** | `260404` |
| **Root namespace** | `GDK.Net` |
| **In-scope surface** | core `X*` runtime + `_c` services (XSAPI, libHttpClient, GameChat2, PlayFab — incl. **PFMP** Lobby/Matchmaking + **Party** via `_c`, state-change pattern); **not** XCurl/XAL/GameInput, the legacy XSAPI multiplayer stack (MPM/MPSD/SmartMatch), or any deprecated API |

---

## 1. Problem & goal

Create a .NET projection of the GDK flat C API (`X*` family + the `_c` services) that **feels like
idiomatic C#**. The guiding principle: a C# game developer should never see an `HRESULT`, a raw
handle, an `XAsyncBlock`, or a two-call size buffer — they should see `Task<GameUser>`, `using`,
`event`, properties, and `[Flags]` enums. The projection must be consumable from **MonoGame** and
**Stride** titles, from bespoke hosts (including Mono through `netstandard2.0`), and on the current
LTS .NET runtimes for tooling/services.

The native surface is uniform (see `gdk-surface.md` §4), so one well-designed idiomatic layer plus a
documented mapping convention scales from the `XUser` pilot to the whole surface.

## 2. Target runtimes & LTS policy

- **`net8.0`** — current LTS; primary target for tools/services and modern titles.
- **`net10.0`** — the next LTS; adopt its source-generated marshalling / AOT improvements.
- **`netstandard2.0`** — the reach target: **MonoGame** (and Unity-style/older hosts) consume ns2.0.
  This TFM cannot use `LibraryImport`/function pointers, so it gets a distinct interop shim (§3).
- **.NET 9 is intentionally excluded** — it is STS (standard-term support), not LTS.
- **Architectures:** `win-x64` primary; `win-arm64` supported (the GDK ships arm64 import libs).
  A single calling convention applies (x64/arm64 Windows), simplifying the shims.

## 3. Binding / codegen strategy

**Two layers**, so idiomatic code is decoupled from marshalling:

1. **Raw interop (generated, checked in).** [ClangSharp](https://github.com/dotnet/ClangSharp)
   (libClang) parses `windows\include` and emits raw signatures, structs, enums, and callback
   delegates into `GDK.Net.Interop`. ClangSharp must be configured with **Windows SDK include paths
   and defines** so the headers parse (they need `HRESULT`, `CALLBACK`, SAL annotations, and
   `APP_LOCAL_DEVICE_ID` from `appmodel.h`); the headers also `#error` without **C++11**, which
   libClang satisfies. A response file (`GDK.Net.rsp`) pins the include set and the traversal to the
   in-scope headers only.
2. **Idiomatic layer (hand-authored).** Wraps the raw layer per the mapping table (§4).

**Per-TFM interop shims** (the only place that forks by TFM):
- **Modern (`net8.0` / `net10.0`)** — `[LibraryImport]` source-generated marshalling; native
  callbacks via `[UnmanagedCallersOnly]` static function pointers → trim/AOT friendly, no delegate
  lifetime problems. **NativeAOT is mandatory for Xbox console**, so this is the required shape, not
  a preference.
- **`netstandard2.0`** — classic `[DllImport]` + `Marshal.GetFunctionPointerForDelegate` with
  **GC-rooted** delegate instances (kept alive for the registration's lifetime).

**Pinning & hygiene:** generation targets edition **`260404`**; regenerating against a newer edition
is a mechanical re-run + diff. The generator **filters** deprecated members (e.g.
`XUserGetMsaTokenSilently*`) and the **out-of-scope** headers (XCurl, XAL, GameInput, and the legacy
multiplayer trio `multiplayer_manager_c.h` / `multiplayer_c.h` / `matchmaking_c.h` — see
`gdk-surface.md` §7). Deprecated declarations are excluded from **every** family, following the
detection rules and denylist discipline in `gdk-surface.md` §7.1.

**Native linkage — bind the thunks DLLs, never the static libs.** See
[`reference/gdk-surface.md`](./reference/gdk-surface.md) → "Which module to bind". Each native
surface has a *redistributable* module that must be copied into the package layout; none of them is
installed system-wide, and packaging must hard-fail if one is missing.

| Surface | Module to bind | Notes |
|---|---|---|
| Core Gaming Runtime | `xgameruntime.thunks.dll` | 390 exports. **Never `XGameRuntime.dll`** — it exports only private version-negotiation ordinals, so a binding against it can never work. |
| Core, unexported APIs | — | 14 APIs are declared or present in `xgameruntime.lib` but unexported in `260404`, so they are unreachable. None is required: each has an exported alternative recorded in `eng/unexported-apis.md`. |
| **XSAPI (Xbox Live)** | **`Microsoft.Xbox.Services.C.Thunks.dll`** | 425 exports. **Never `Microsoft.Xbox.Services.{142,143}.C.lib`** — those are toolset-versioned static libs for C++ titles. Requires `libHttpClient.dll` shipped beside it. |

XSAPI exports **no** `XAsync*`/`XTaskQueue*`, so `AsyncOperation<T>` and `GameTaskQueue` keep
binding `xgameruntime.thunks.dll`; an XSAPI-using title loads **both** modules. Unlike the core
runtime, XSAPI has **no meaningful export gap on GDK**: every `Xbl*` function that survives the GDK
preprocessor is exported except the internal `XblSetApiType`. Header names that look missing are
gated out of the GDK build — 11 by `HC_PLATFORM` (Win32/iOS/Android/UWP surface) and 6 by
`#ifdef XSAPI_INTERNAL_EVENTS_SERVICE` — so no family needs degrading. Still verify each
`[LibraryImport]` against the export table (`eng/api-coverage.ps1` does this), but compare against
the *preprocessed* headers, not a raw grep.

## 4. Idiomatic mapping table

| GDK C shape | C# projection |
|---|---|
| `STDAPI XFoo(...)` → `HRESULT` | call, then `Hr.ThrowIfFailed(hr)`; success returns the value/void |
| `STDAPI_(T) XFoo(...)` (non-HRESULT) | direct return of `T` (`bool`/`int`/struct), no throw |
| `E_ABORT` from a canceled async | throw `OperationCanceledException` (via the `CancellationToken`) |
| known `E_GAMERUNTIME_*` / `E_GAMEUSER_*` / `E_XBL_*` | `GameRuntimeException` subclass with typed `HResult` + message |
| `XFooHandle` + `Close` / `Duplicate` | `Foo : IDisposable` backed by a `SafeHandle`; `Foo Duplicate()` |
| `XFooCompare` | `IEquatable<Foo>`, `==` / `!=` via `XFooCompare`; `GetHashCode()` from the type's stable identity value (e.g. `XUserLocalId`) so it stays consistent with `XFooCompare` equality |
| cheap pure `XFooGetBar(h, out b)` | property `Bar { get; }` (throws on failure) |
| costly/side-effecting `XFooDoThing(h, out b)` | method `DoThing()` |
| `XFooBarAsync(...)` + `XFooBarResult(...)` | `Task<T> Foo.BarAsync(..., CancellationToken)` (TAP) |
| `...Async` + `...ResultSize` + `...Result` | `Task<byte[]>` / `Task<T[]>` (size handled internally) |
| task queue / dispatch | `GameTaskQueue` (internal) + `GameTaskQueue.DispatchCompletions()`; every instance, including duplicates, terminates, waits, then closes |
| `enum class E : uint32_t` | `enum E : uint` |
| flag enum (`DEFINE_ENUM_FLAG_OPERATORS`) | `[Flags] enum E : uint` |
| two-call size buffer (sync) | one method returning `string` / `byte[]` / `T[]` |
| UTF-8 (`char*`) vs UTF-16 (`wchar_t*`) | surface **one** `string` API (prefer the UTF-16 variant where offered; else decode UTF-8) |
| `XFooRegisterForXEvent` / `Unregister` + token | C# `event`; token + rooted delegate held internally, unregistered on dispose |
| `struct S {...}` | `readonly struct` / `record struct`, blittable where possible |
| polymorphic state-change struct (tagged union via `stateChangeType`) | a **public** `abstract record` hierarchy (`LobbyStateChange` → `CreateAndJoinLobbyCompleted` / `MemberAdded` / `LobbyUpdated` / …), dispatched with a `switch` expression on the record type — the loop is the surface |
| `Start`/`Finish…StateChanges` drain | `foreach (var change in mp.ProcessStateChanges())` — the loop **is** public; the enumerator's `Dispose` calls `Finish` (lazy: batch held live during the loop, zero-copy); composes with `DispatchCompletions()` |
| completion change + `void* asyncContext` | start call returns a typed `OperationId` (round-tripped as `asyncContext`); the `…Completed` record carries it back in the loop and the caller matches by id — **no `await`**, **no** cancellation (no `XAsyncCancel`) |
| notification change (non-completion) | a notification record in the same loop carrying `change.Lobby` (reference-equal via a handle→wrapper identity map); the object is valid until its teardown variant drains, then fails fast (`ObjectDisposedException`) |
| naming | **drop the `X` prefix**, PascalCase, domain namespaces (`GDK.Net.Users.GameUser`), keep enum member names, XML-doc the GDK origin |

> Naming is proposed, not locked; the pilot review finalizes it.

## 5. Error model

- `HResult` static class holds the well-known codes; `Hr.ThrowIfFailed(int hr)` is called after every
  HRESULT-returning P/Invoke.
- **`GameRuntimeException : Exception`** carries the numeric `HResult` and a message; targeted
  subclasses (e.g. `GameUserException`) map the common `E_GAMEUSER_*` / `E_GAMERUNTIME_*` codes to
  friendly types so callers can `catch` specifically.
- **Cancellation** is special-cased: `E_ABORT` from a canceled async surfaces as
  `OperationCanceledException` (linked to the caller's `CancellationToken`), never as a
  `GameRuntimeException`.
- Optional: bridge the native diagnostic hook `XErrorSetCallback` to a .NET event
  (`GameRuntime.ErrorReported`) for logging/telemetry.

## 6. Resource / handle model

- Each ordinary handle type gets a `SafeHandle` subclass whose `ReleaseHandle()` calls the matching
  `X*CloseHandle`. The public type is `IDisposable` and owns the `SafeHandle`.
- `GameTaskQueue` is the shutdown special case: every instance, including one returned by
  `Duplicate()`, terminates and waits for termination before closing the native handle.
- `Duplicate()` calls `X*DuplicateHandle` and returns an independent instance (own `SafeHandle`).
- Equality uses the GDK compare function (`GameUser` implements `IEquatable<GameUser>`, `==`/`!=` via
  `XUserCompare`). Because a comparison function cannot itself yield a hash, `GetHashCode` hashes the
  user's stable **`XUserLocalId`** (from `XUserGetLocalId`): the same-user handles that `XUserCompare`
  treats as equal share one `XUserLocalId`, so equal users always hash equally.
- `SafeHandle` gives thread-safe, finalizer-backed release, so a missed `Dispose()` still frees the
  native handle (with a debug warning).

## 7. Async & threading model  *(MonoGame-aware)*

**The async engine** — `AsyncOperation<T>`:
- Allocates a **pinned** `XAsyncBlock` (its `internal[sizeof(void*)*4]` layout is opaque; the struct
  is kept alive for the call's duration), stores a `GCHandle` context, and installs a **static**
  completion routine (`[UnmanagedCallersOnly]` on modern; rooted delegate on ns2.0).
- On completion the routine calls the `*Result` (or `*ResultSize`+`*Result`) function and completes a
  `TaskCompletionSource<T>`.
- The caller's `CancellationToken` registers a callback that calls `XAsyncCancel`; the resulting
  `E_ABORT` completes the task as canceled.

**Two completion strategies** (both back the same `Task` surface):
- **Default (process queue) — shipped.** The projection creates no queue: operations name no queue,
  so the Gaming Runtime resolves the process default — thread pool on both ports unless the host
  replaced it. `await` resumes on the pool. This is the only strategy currently reachable by a
  title.
- **Pumped (game loop) — designed, not yet exposed.** A `Manual` completion port dispatched once per
  frame, so continuations and events land on the thread that pumps, with a
  `GameSynchronizationContext` installed there so plain `await` resumes on the game/update thread —
  the key "native feel" for MonoGame. `GameTaskQueue` is presently **internal** and no public
  signature names a queue (enforced by `TaskQueueAdvancedTests.NoPublicApiMentionsATaskQueue`), so a
  title cannot yet create, name or pump one. Exposing this is outstanding work, and it is the
  deliberate reason the surface stays queue-free until the shape is settled.

  Whatever shape it takes, it will not be a queue hung off `GameRuntime`: `XGameRuntimeInitialize`
  takes no arguments, so a runtime-wide queue is a projection invention layered over the runtime's
  own defaulting. The queue belongs on the calls that use it.

## 8. Events model

- Native `X*RegisterForXEvent` → a C# `event` (e.g. `Users.UserChanged`,
  `Users.DeviceAssociationChanged`).
- The registration token and the callback trampoline (rooted delegate / function pointer + context
  `GCHandle`) are managed **internally** and released via `X*UnregisterForXEvent(token, wait: true)`
  on dispose.
- Events are delivered on the subscription's task queue, so under the pumped model they raise on the
  game thread (consistent with `await`).

## 9. Pilot slices

### 9.1 `XUser` (primary pilot)

Implements [`reference/xuser-pilot.md`](./reference/xuser-pilot.md). Intended public shape:

```csharp
using var runtime = GameRuntime.Initialize();               // XGameRuntimeInitialize / Uninitialize
if (!runtime.IsFeatureAvailable(GameFeature.User)) return;  // XGameRuntimeIsFeatureAvailable

using GameUser user = await runtime.Users.AddAsync(         // XUserAddAsync / XUserAddResult
        GameUserAddOptions.AddDefaultUserSilently, ct);      // [Flags]

ulong id            = user.Id;                              // XUserGetId            -> property
GameUserState state = user.State;                          // XUserGetState         -> property
GameUserAgeGroup ag = user.AgeGroup;                       // XUserGetAgeGroup      -> property
bool isGuest        = user.IsGuest;                        // XUserGetIsGuest       -> property
string gamertag     = user.GetGamertag(GamertagComponent.UniqueModern); // two-call buffer -> string

byte[] picture      = await user.GetGamerPictureAsync(GamerPictureSize.Medium, ct); // sized async
bool canChat        = user.CheckPrivilege(GameUserPrivilege.Communications, out var denyReason);

runtime.Users.UserChanged += (s, e) => { /* e.User, e.Change */ };  // Register/Unregister + token
await user.SignOutAsync(ct);                                       // XUserSignOutAsync / Result
```

Covers: lifecycle + feature gate, `SafeHandle`/`IDisposable`/equality, async→`Task` (incl. sized),
sync getters→properties, two-call buffer→`string`, `[Flags]` + plain enums, `out` deny-reason,
`event`, and cancellation (`ct` → `XAsyncCancel` → `OperationCanceledException`).

### 9.2 PFMP Lobby & PF Party — the state-change slice

Implements [`reference/multiplayer-pilot.md`](./reference/multiplayer-pilot.md) with the
state-change rows from §4. The native poll-drain is kept **faithful**: the loop is the public surface —
a `foreach` over a closed `record` hierarchy — with `Start` / `Finish` / `stateChangeType` hidden
behind the enumerator, which calls `Finish` on scope exit.

```csharp
using var mp = await Multiplayer.InitializeAsync(titleId, user, ct);   // XUser → SDK-owned PlayFab Entity handle

// Start calls are synchronous: a typed OperationId + a not-ready handle (no await for these ops):
(OperationId join, Lobby lobby) = mp.CreateAndJoinLobby(createCfg, joinCfg);

// The loop IS the public surface and the per-frame pump (once per frame, update thread).
// The enumerator's Dispose() calls Finish → lazy, zero-copy; change records must not escape it.
foreach (var change in mp.ProcessStateChanges())
{
    switch (change)
    {
        case CreateAndJoinLobbyCompleted c when c.Operation == join:
            if (c.Failed) throw c.Error;                  // completions carry an HRESULT
            break;                                        // c.Lobby == `lobby`, now ready (identity map)
        case MemberAdded m:  Show(m.Member);        break;  // m.Member is a borrowed view — copy to keep
        case LobbyUpdated u: Refresh(u.ChangedKeys); break;
        case LobbyLeaveCompleted or Disconnected:         // teardown: `lobby` now invalidated → fails fast
            break;
    }
}   // ← Finish runs here (scope exit)

// Reads + further ops issued anytime; their completions surface on a later pump:
IReadOnlyList<LobbyMember> members = lobby.Members;   // getters snapshot (called outside the loop)
OperationId post  = lobby.PostUpdate(props);          // → LobbyUpdated on a later pump
OperationId leave = lobby.Leave();                    // pump until LobbyLeaveCompleted, then dispose → Uninitialize
```

Covers (revised decisions): the loop **is** the public surface and the per-frame pump, composing with
`DispatchCompletions()`; a **public `abstract record` hierarchy** is the closed variant set, dispatched
with a `switch` expression; completions correlate by a typed `OperationId` (round-tripped as
`asyncContext`) with **no `await`** and **no** cancellation; notifications are loop records carrying
`change.Lobby` (reference-equal via a handle→wrapper identity map); `Finish` runs on **scope exit** via
the enumerator's `Dispose` (lazy, zero-copy), so change records are **borrowed views valid only within
the loop** — the caller copies (`Members` snapshots) anything it retains; a projected object is valid
until its teardown variant (`LobbyLeaveCompleted` or an involuntary `Disconnected`), then throws
`ObjectDisposedException`. A loop-less host runs the same loop on a wrapper-owned thread. The same
shape covers **Party** (`Party_c.h`) and **Matchmaking** (`PFMatchmaking.h`) off the same pump.

## 10. Testing

Testing is **live** — see [`../reference/testing.md`](./reference/testing.md) for the shared harness
contract. The idiomatic layer is a thin wrapper over P/Invoke, so there is no glue worth mocking; only
the real Gaming Runtime proves behavior.

- **Bespoke live harness.** `GDK.Net.LiveHarness` is a **packaged GDK app** (ships a
  `MicrosoftGame.config`, runs under the Gaming Runtime). It runs XUser end-to-end: Initialize →
  feature check → `AddAsync(AddDefaultUserSilently)` (fallback to `AddDefaultUserAllowingUI` on
  `E_GAMEUSER_NO_DEFAULT_USER`) → read `Id`/`Gamertag`/`State` → subscribe `UserChanged` → `SignOut`,
  plus a cancellation test (`E_ABORT` → `OperationCanceledException`), and extends the same structure
  across Store, GameSave, Package, Capture and the XSAPI services and managers. Run the **same suite
  on a .NET LTS runtime *and* on Mono** so marshalling differences surface.
- **Harness structure.** Checks are **values** — an id, the ids it requires, and a body — held in one
  ordered registry and executed one at a time in isolation, so a failure never ends the run and a
  failed prerequisite produces one explanatory skip rather than a screen of identical errors. The
  registry is an explicit list, not an assembly scan, because the harness publishes with NativeAOT.
  Results go to a JSON report flushed after every check: a packaged title has no console, and the
  file is the only channel out of the process. Each check is recorded as *running* before it makes
  its call, so a relaunch can attribute a native crash and carry on past it — an access violation in
  the Gaming Runtime **cannot be caught** in .NET 8, by `catch`, attribute or runtimeconfig switch.
  See [`../reference/testing.md`](./reference/testing.md) §1.1.
- **Contract tests.** `GDK.Net.Tests` runs on hosted CI with **xUnit** (+ **FluentAssertions**) and
  coverlet. It never loads the GDK: it asserts shape — naming, enum values against the headers,
  handle disposal semantics, marshalling attributes. It is a guard against drift, **not** evidence
  that any API works.
- **Packaging tiers.** `eng/run-package-tests.ps1` runs **Layout** (publish, generate the map, run the
  submission validator), **Msixvc** (pack), **Register** (loose-register and launch) and **Install**
  (install and launch). The first two need no elevation and catch packaging defects before anything
  touches the machine. Without elevation `wdapp register` registers the title as an *Application*, so
  `XPackageIsPackagedProcess` reports false and the `XPackage` family is only authoritative from the
  Install tier.
**Engine/framework workflow integrations**

| Profile | Standard workflow under test |
|---|---|
| **Bespoke** | Host-owned update loop and `GameTaskQueue`; consume `GDK.Net` as an ordinary NuGet dependency; package the executable directly as a GDK title. |
| **MonoGame** | Start from the normal Windows project/template; initialize in `Game.Initialize`, dispatch completions and state-change loops from `Game.Update`, and dispose from the game's shutdown path. |
| **Stride** | Add `GDK.Net` to a normal Stride game; register a game-system/service that owns initialization, pumps from the engine update, and tears down with the game. |

Each profile runs the live XUser and PFMP Lobby pilots through its ordinary build/package/run
workflow; MonoGame is the release gate and Stride runs scheduled/nightly.
Each harness pins its framework/runtime version and checks in the scaffold, restore, build, GDK
package, and launch commands. The packaged output must resolve the pinned-architecture
`PlayFabCore.dll` and `PlayFabMultiplayer.dll` before starting the Lobby pilot.
- **Prerequisites:** installed GDK/Gaming Runtime, a registered title identity, an authorized
  **sandbox**, and a signed-in **test account**.
- **CI reality:** hosted CI only **builds + packs + lints** (no runtime, no mocks); the live suite runs
  on a GDK-capable self-hosted runner. Flagged as a risk (§12).

## 11. Packaging & distribution

- NuGet package **`GDK.Net`** ships **managed assemblies only** (multi-targeted
  `net8.0;net10.0;netstandard2.0`). Effective RIDs **`win-x64`** / **`win-arm64`**.
- **The native modules are redistributables the *title* ships, not system components.** Nothing the
  projection binds is installed machine-wide, so `eng/package.ps1` copies each one into the layout
  and **hard-fails if it is absent** — a missing DLL otherwise surfaces at runtime as an
  initialization failure that reads like a broken dev box:

  | DLL | Ships when | Source |
  |---|---|---|
  | `xgameruntime.thunks.dll` | always | `%GameDKCoreLatest%windows\bin\{x64,arm64}` |
  | `Microsoft.Xbox.Services.C.Thunks.dll` | any XSAPI use | `%GameDKCoreLatest%windows\bin\{x64,arm64}` |
  | `libHttpClient.dll` | with XSAPI — **hard dependency** | same |

  The XSAPI pair adds ~2.2 MB to the layout, so gate it on actual use rather than copying
  unconditionally. `libHttpClient.dll` is the easy one to forget: omitting it fails at the first
  XSAPI call, not at startup.
- **Declare the VC runtime dependency when XSAPI ships.** `libHttpClient.dll` and
  `Microsoft.Xbox.Services.C.Thunks.dll` depend on `Microsoft.VCLibs.140.00.UWPDesktop`, and the
  submission validator fails the layout unless `MicrosoftGame.config` declares it:
  `DesktopRegistration` → `DependencyList` → `KnownDependency Name="VC14"`.
- PFMP-enabled applications deploy architecture-matched `PlayFabCore.dll` and
  `PlayFabMultiplayer.dll` from the pinned GDK redist during application packaging; the managed
  NuGet package does not silently embed them.
- **NativeAOT** is required for Xbox console. The library is AOT-clean and enforces it (analyzers +
  a real ILC publish in CI + structural tests); JIT remains the default for PC. See the repo README.
- `Directory.Build.props` centralizes `LangVersion`, `Nullable=enable`, TFMs, analyzers.
- `getting-started.md` documents the `MicrosoftGame.config` + Gaming Runtime requirement plus
  MonoGame `Game.Update` and Stride game-system integration snippets.

## 12. Risks & open decisions

- **Naming convention** is locked to the rule in
  [`../reference/gdk-surface.md`](./reference/gdk-surface.md) §12 and recorded in the implementation
  repo's `eng/interop-conventions.md`: drop the `X`/`Xbl` prefix and the redundant family qualifier,
  preserve every remaining header term verbatim, and never add a prefix to resolve a collision.
- **Task queues are not part of the public surface, and the projection never creates one.** No public
  signature names a queue and `GameTaskQueue` is internal, so titles cannot create, name or pump one
  (`TaskQueueAdvancedTests.NoPublicApiMentionsATaskQueue` enforces this). Internally the projection
  creates no queue either: operations leave `XAsyncBlock::queue` null and the Gaming Runtime resolves
  the process default, so nothing is owned or disposed on anyone's behalf. Trade-off: a host that
  replaces or removes the process default via `XTaskQueueSetCurrentProcessTaskQueue` changes where
  callbacks run, and the projection currently offers no way to opt out of that — which is the same
  gap as the unexposed pumped model in §7.
- **ClangSharp parsing** of GDK headers (C++11 + Windows SDK includes/defines) is the first real
  hurdle; fallback is hand-authored raw P/Invoke for the pilot headers while generation stabilizes.
- **Live-only testing** limits automated CI; mitigated by keeping interop thin and the idiomatic
  layer deterministic.
- **`netstandard2.0` shim** (rooted delegates, no source-gen marshalling) is the most error-prone
  path; covered by the same live pilot.
- **Edition pinning:** generate against `260404`; note `250404`/`251001`/`260404` installed.

## 13. Expansion path

> **Canonical surface & order:** the full Xbox + PlayFab coverage plan — every API family with its
> async shape, dependencies, and priority tier — lives in
> [`reference/roadmap.md`](./reference/roadmap.md). The order below is the .NET reading of it.

The Gaming Runtime (`X*`) is now projected in full — every family in
[`reference/roadmap.md`](./reference/roadmap.md) §3, bar the deliberately excluded
`XAsyncProvider.h` and `XCurl.h`. **Xbox Live (XSAPI) is the next major surface**, and it is an
integral part of the GDK rather than an optional extra: achievements, presence, the social graph,
leaderboards and title storage are certification-relevant for most shipping titles.

### 13.1 Xbox Live Services (XSAPI)

XSAPI reuses everything the runtime pilot established — `AsyncOperation<T>`, `Hr.ThrowIfFailed`,
`SafeHandle`, the event pattern, the `[LibraryImport]` + `[UnmanagedCallersOnly]` AOT shape — so the
work is surface area, not new mechanics. What is genuinely new:

- **A second native module.** `Microsoft.Xbox.Services.C.Thunks.dll` plus `libHttpClient.dll` join
  the layout (§3, §11). `eng/api-coverage.ps1` must grow to diff this DLL's export table too.
- **`XblContextHandle` is per-user and per-sign-in.** It is built from an `XUserHandle`, so it must
  be torn down and rebuilt on user change or sign-out — a new `SafeHandle` type whose lifetime is
  coupled to `GameUser`, and a compliance obligation (see
  [`reference/compliance.md`](./reference/compliance.md)).
- **Async teardown.** `XblCleanupAsync` has no synchronous form and must be awaited before the task
  queue is torn down; `GameRuntime`'s disposal order has to account for it. Init is synchronous and
  needs the title's **SCID**, which is a new piece of required configuration.
- **RTA is a long-lived connection, not a call.** XSAPI opens and closes the websocket on demand as
  subscriptions come and go; its connection-state and resync handlers must surface as .NET events,
  and the resync semantics ("refetch everything") have no analogue in the runtime surface. The
  connection lifetime itself is *not* projected: `XblRealTimeActivity{Activate,Deactivate}`, the
  subscription-error handler and the subscription state/id getters are all deprecated in the header
  and are therefore excluded (see [`gdk-surface.md`](./reference/gdk-surface.md) §7.1).
- **A `DoWork` pump.** The `*_manager` layers (`social_manager_c.h`, `achievements_manager_c.h`) use
  the polled batch loop of [`reference/state-change.md`](./reference/state-change.md) with
  *next-call* reclamation — the batch stays valid only until the next `DoWork`. That is the same
  shape PFMP needs, so projecting one manager de-risks the other.
- **No XSAPI export gap.** Earlier revisions of this plan claimed 23 header-declared functions were
  absent and that notification-handler registration for Social, Achievements, Privacy, Game invite,
  Multiplayer activity and Notification was unreachable. That was wrong: it came from grepping
  headers without the preprocessor. On GDK every `Xbl*` function that survives preprocessing is
  exported except the internal `XblSetApiType`. The names that looked missing are **non-GDK
  surface** gated by `HC_PLATFORM` (Win32/iOS/Android/UWP — on GDK the core runtime's
  `XGameInvite*` / `XUser*` cover it) or **internal-only** surface gated by
  `XSAPI_INTERNAL_EVENTS_SERVICE`. Project every family in full.

Suggested order, following the roadmap's tiers: **XblContext + init** → **Profile** → **Social** →
**Presence** + **RTA** → **Achievements** → then P2 (Leaderboards, statistics, title storage,
privacy).

### 13.2 Beyond XSAPI

PlayFab (Core, Services, Economy), PFMP Lobby/Matchmaking and Party via the `_c` surfaces, and
GameChat2 — all shape **A** or the same `DoWork` loop, so they inherit the XSAPI groundwork.

## 14. Cross-cutting obligations

Beyond the per-API mechanics above, every projection inherits a set of **cross-cutting duties**
specified once — language-agnostically — in the shared references. This plan adopts them wholesale:

- **Security & privacy** — tooling generation enables `PFMULTIPLAYER_INCLUDE_SERVER_APIS` and keeps
  the full PlayFab player/title/server surface; whenever a post-login call takes authentication
  input, use `PFEntityHandle`. Title secrets exist only at trusted tooling bootstrap and are never
  embedded or logged.
  See [`reference/security-privacy.md`](./reference/security-privacy.md).
- **Certification & compliance** — honor sign-out / user-change and license-loss promptly and without
  blocking; gate comms/UGC behind privacy checks (**fail closed**); keep the advertised session
  accurate; prefer TCUI. See [`reference/compliance.md`](./reference/compliance.md).
- **Diagnostics & tracing** — bridge the libHttpClient / PlayFab trace hooks into
  `Microsoft.Extensions.Logging` / `EventSource` with mapped levels and callback-routed logs
  scrubbed; direct debugger/file sinks are developer-only and may be unredacted.
  See [`reference/gdk-surface.md`](./reference/gdk-surface.md) §10.
- **Idiomaticity first** — outside the state-change loop, prefer normal .NET strings, collections,
  `SafeHandle`s, exceptions, and `Task`s over allocation rules or exposed native buffers; optimize
  measured hot paths only. See [`reference/gdk-surface.md`](./reference/gdk-surface.md) §11.

Terminology used across this plan is defined in [`reference/glossary.md`](./reference/glossary.md).

## 15. Todos

1. Scaffold repo/solution (TFMs, RIDs, `Directory.Build.props`, CI skeleton).
2. ClangSharp generation tooling; get in-scope GDK headers to parse (`GDK.Net.rsp`).
3. Raw interop layer (generated + per-TFM callback shims).
4. Error model (`Hr.ThrowIfFailed`, `GameRuntimeException`, code mapping, `E_ABORT`→cancel).
5. Runtime lifecycle (Initialize/Uninitialize + feature gating).
6. `GameTaskQueue` + `XAsyncBlock`→`Task` engine (+ cancellation).
7. MonoGame pumped `SynchronizationContext` / `DispatchCompletions`.
8. `GameUser` handle wrapper (SafeHandle, IDisposable, equality).
9. User sync members (Id/State/AgeGroup/IsGuest/Gamertag).
10. User async members (AddAsync/SignOutAsync/GamerPicture/CheckPrivilege).
11. User events (UserChanged / DeviceAssociationChanged).
12. Bespoke packaged integration harness running the live XUser + PFMP Lobby pilots.
13. MonoGame Windows integration harness (`Initialize` / `Update` / shutdown).
14. Stride game-system integration harness (`Initialize` / `Update` / `Destroy`).
15. Packaging, PlayFab DLL deployment, and version-pinned scaffold/build/package/run recipes.
16. Pilot review (validate all three workflows; lock naming + expansion).
