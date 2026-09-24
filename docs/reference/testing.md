# Testing a GDK Projection: Shared Strategy & Harness Contract

> **Purpose.** Every projection wraps the *same* GDK, which imposes the *same* testing constraints, so
> the testing strategy is shared. **Testing is live.** This document explains why, defines the
> **requirements of the live-integration harness**, names the **test runner** each language uses to
> drive it, requires representative **engine/framework workflow integrations**, and sets out the
> **CI vs self-hosted** split. Each plan's **§10** specializes it.

Read [`xuser-pilot.md`](./xuser-pilot.md) first: its **§3** canonical scenario is the script the
live harness runs, and its **§4** lists the minimum live prerequisites. This document is the fuller
model around that slice.

The core constraint: `XUser` (and most of the GDK) **cannot be meaningfully faked**. A real
signed-in user, an authorized sandbox, and the Gaming Runtime are required to prove behavior. And the
projection itself is a **thin idiomatic layer** over the raw bindings: there is little glue worth
mocking, and a mock of the native seam mostly asserts that our own fake behaves like our own fake.
**So every test is live**: no mocks, no fakes, no substituted native seams. The language test
frameworks named below are used only as **runners** that drive the live harness.

The cost is real: live tests need a GDK-capable, sandbox-authorized machine and cannot run on hosted
CI. That is an accepted trade: a thin wrapper's correctness is *only* meaningful against the real
runtime, and every plan flags it as a risk (§12).

---

## 1. The live-integration harness: requirements

The harness runs [`xuser-pilot.md`](./xuser-pilot.md) §3 end-to-end. Beyond the §4 prerequisites, a
usable harness must satisfy:

- **Packaged GDK identity.** The test process is a **packaged app** with a `MicrosoftGame.config` and a
  **registered title identity**; the GDK resolves identity from the running package, so tests cannot
  run as a bare console binary. For every non-C# language this means **hosting the language runtime
  (CLR/Mono, JVM, Node/Deno/Bun, CPython, the Lua host, or the AIR runtime) inside that package**: the
  single hardest harness requirement.
- **Runtime & sandbox state.** An installed **Gaming Runtime** compatible with edition `260404`, an
  **authorized sandbox**, and a **signed-in test account**. The account's state (age group, privileges,
  guest-ness) is part of the fixture and must be documented per suite.
- **Process-global lifecycle → no in-process parallelism.** `XGameRuntimeInitialize`/`Uninitialize`
  and user state are **process-wide singletons**. Tests must **sequence** `initialize → scenario →
  uninitialize` and isolate cases in **separate processes** (or a single serial fixture); parallel
  test runners must be constrained to one live case per process.
- **Deterministic async.** Prefer a **`Manual` completion queue that the test pumps explicitly**
  (dispatch, then assert) over a thread-pool queue: pumping makes completions and event delivery
  reproducible and removes flakiness from live runs.
- **Teardown assertions.** Verify balanced `Duplicate`/`Close`, that subscriptions unregister honoring
  `wait`, and that no callback fires after dispose.
- **Diagnostics capture.** Wire the projection's error callback (the `XError`-style hook) and any trace
  output to the test log, and capture it as a CI artifact: live failures are often only diagnosable
  from that stream.
- **PlayFab native deployment.** The PFMP pilot requires the architecture-matched
  `PlayFabCore.dll` and `PlayFabMultiplayer.dll` (and their build-time import libraries where the
  binding links them). Every host artifact must deploy/resolve these from the pinned GDK redist set;
  `XGameRuntime.dll` alone is insufficient. Run a native-load smoke check before PlayFab login/Lobby.
- **Self-hosted runner.** A GDK-capable, sandbox-authorized machine (labeled runner or a manual lab
  box). Hosted cloud CI cannot satisfy identity/runtime/sandbox and must not run the live suite.
- **Real host workflow.** Do not validate only through a test-only executable. Each language also
  runs the same pilots through its normal bespoke-host workflow and representative engines/frameworks
  (§2), using their ordinary dependency, update-loop, packaging, and shutdown conventions.

### 1.1 What the first live run of `gdk-dotnet` taught

The .NET harness was written, reviewed and compiled long before it was ever executed, and the first
real run invalidated several assumptions that looked safe on paper. They are recorded here because
they follow from the GDK and the packaged-app model, not from anything specific to .NET, so every
projection will meet them.

- **The report file is the only channel out.** A packaged title has no attached console, so stdout is
  not observable and a debugger is not always attachable. Everything the harness learns must be
  written to a file, and written **as it is learned**, a report produced only on the way out is
  precisely the report you never get.
- **A failing check must not end the run.** Model checks as **values**: an id, the ids of the checks
  it depends on, and a body, and execute each in its own isolated attempt. A harness that calls its
  families as one straight-line sequence lets the first failure hide the state of everything after
  it, which is backwards for something whose job is to report coverage.
- **Declare prerequisites, and distinguish "prerequisite failed" from "prerequisite was filtered
  out".** Isolation alone turns one broken sign-in into a hundred identical "no user handle" errors.
  With prerequisites, exactly one line describes the actual problem.
- **Make the pass condition explicit.** If "the body returned without error" means pass, a check that
  *returns a string describing a failure* reports green. This is not hypothetical: it is the single
  most severe defect found reviewing the .NET harness, and it would have hidden a broken API behind a
  passing suite. Prefer an explicit outcome value, or make the failure path throw.
- **Skips can make a hollow run look clean.** The .NET harness originally skipped all of XSAPI and
  GameSave when no service configuration id was supplied, and skips do not fail a run, so it
  reported success while testing almost nothing. Treat a family-wide skip as a result that needs
  review, and derive configuration where it can be derived (the SCID follows from the title id) so
  the skip never arises.
- **Plan for the process dying mid-run.** An access violation in native code **cannot be caught** in
  a managed or hosted runtime, not in .NET, and equally not in the JVM, CPython, Node or a Lua host.
  The process is gone without unwinding, and every check after the offending one goes unmeasured.
  The harness must therefore be **resumable**: record each check as *running* before making the call,
  and on relaunch treat a check still marked running as the one that crashed, record it as a failure
  and carry on. The launcher relaunches while each attempt gets further than the last. Native state
  does not survive the process, so the checks that establish a runtime, a user handle or a service
  context must re-run on resume; list them in one place, because forgetting one produces a cascade a
  long way from the omission.
- **Validate packaging in tiers, not once at the end.** Separate **layout** (publish + generate the
  map + run the submission validator), **package** (produce the `.msixvc`), **register** (loose-register
  the layout and launch it) and **install** (install the package and launch it). The first two are
  non-invasive, need no elevation, and catch packaging defects before anything touches the machine.
- **Declare the framework dependencies the projection's native payload carries.** `libHttpClient` and
  the XSAPI thunks depend on `Microsoft.VCLibs.140.00.UWPDesktop`, which the submission validator
  rejects unless `MicrosoftGame.config` declares it via
  `DesktopRegistration` → `DependencyList` → `KnownDependency Name="VC14"`. Any projection shipping
  those binaries needs this, whatever the host language.
- **Loose registration is not full title identity.** Without elevation, `wdapp register` registers the
  app as an *Application* rather than a *Game*, and `XPackageIsPackagedProcess` then reports false, so
  the whole `XPackage` family is untestable at that tier. Decide per family which tier is authoritative
  and record it, rather than reading the resulting failures as projection defects.

---

## 2. Engine/framework workflow integrations

Every language plan names and tests **three host profiles**:

1. **Bespoke host**: a minimal packaged engine/tool host that directly owns the language runtime,
   task queue, frame/update loop, and shutdown. This proves the projection without a framework
   adapter.
2. **Primary engine/framework**: the ecosystem's most representative target; a release-gating live
   integration.
3. **Secondary engine/framework**: a materially different workflow; a scheduled/nightly live
   integration and a compatibility gate before declaring the projection mature.

Each profile must use the host's normal workflow rather than a parallel test-only path:

1. Scaffold/build with the framework's standard project tooling.
2. Consume the projection through the ecosystem's normal package/module mechanism.
3. Initialize from the normal startup hook.
4. Pump Manual `XTaskQueue` completions **and** active state-change loops from the normal per-frame
   update hook. A non-game framework without a frame loop uses its normal main-thread scheduler;
   renderer IPC must not carry native handles or substitute for the native pump.
5. Deliver callbacks/events on the host's expected thread.
6. Run the complete live **XUser** pilot and **PFMP Lobby** state-change pilot.
7. Shut down from the host's normal disposal hook, including queue terminate → wait → close.
8. Produce and launch the framework's normal Windows artifact as a packaged GDK title with
   `MicrosoftGame.config` and the required PlayFab runtime DLLs.
9. Record the exact scaffold/build/package/run commands and the tested framework version.

Planned targets:

| Language | Bespoke host baseline | Primary integration | Secondary integration | Normal frame/lifecycle hook |
|---|---|---|---|---|
| **.NET / C#** | packaged .NET/Mono host with its own loop | **MonoGame** | **Stride** | `Game.Update` / engine game-system update; dispose on host shutdown |
| **Rust** | minimal packaged host loop | **Bevy** | **macroquad** | Bevy `Update` system / macroquad loop before `next_frame()` |
| **JVM** | packaged JVM host with an explicit tick | **libGDX** | **jMonkeyEngine** | `ApplicationListener.render` / `BaseAppState.update`; framework disposal |
| **Go** | packaged Win32-style host loop | **Ebitengine** | **raylib-go** | `Game.Update` / explicit `WindowShouldClose` loop |
| **Python** | packaged embedded-CPython host | **Panda3D** | **pygame-ce** | Panda3D `taskMgr` / pygame loop; explicit cleanup |
| **JS / TS** | packaged Node host | **Electron** | **Phaser 3 in Electron** | Electron main-process scheduler; Phaser consumes a typed bridge, not native handles |
| **Lua** | custom PUC-Lua/LuaJIT embedded host | **LÖVE** | **Defold** | `love.update` / Defold extension `Update`; matching application-finalization hook |
| **ActionScript** | plain HARMAN AIR packaged app | **Starling** | **Away3D** | native-stage `ENTER_FRAME`; dispose ANE resources on `NativeApplication.EXITING` |

The bespoke profile is **in addition to**, not a substitute for, the two framework integrations.
If an ecosystem cannot support the secondary target, the plan must document the concrete blocker
and name the next candidate rather than silently dropping the profile.

---

## 3. Test runners by language

Concrete choices live in each plan's **§10**; this is the map. These frameworks **drive the live
harness**: they are not used to host mocks.

| Language | Test runner (drives the live harness) | Coverage & static checks | Live host / matrix |
|---|---|---|---|
| **.NET / C#** | xUnit or NUnit (+ FluentAssertions) | coverlet; Roslyn analyzers | bespoke + MonoGame + Stride; **.NET LTS and Mono** |
| **Rust** | built-in `#[test]` / `cargo test`, gated `--features live` + `#[ignore]` | `cargo llvm-cov`, clippy | bespoke + Bevy + macroquad |
| **JVM** | **JUnit 5** (+ AssertJ); Kotlin **kotest** | JaCoCo; ErrorProne / detekt | bespoke + libGDX + jMonkeyEngine |
| **Go** | built-in `testing` (+ **testify**), `//go:build live`, **`-race`** | `go test -cover`, `go vet` | bespoke + Ebitengine + raylib-go |
| **Python** | **pytest** (+ **pytest-asyncio** for the `await` path), `@pytest.mark.live` | `coverage.py`; ruff + mypy on `.pyi` | bespoke + Panda3D + pygame-ce |
| **JS / TS** | **vitest** / jest / `node:test`; `Deno.test`; `bun test` | c8/istanbul; ESLint; `tsc --noEmit` | bespoke Node + Electron + Phaser |
| **Lua** | **busted** (+ **luassert**) | **luacov**; **luacheck** | bespoke embed + LÖVE + Defold; **Lua 5.4 + LuaJIT** matrix |
| **ActionScript** | **FlexUnit 4** (AsUnit = legacy) | asdoc/lint | bespoke AIR + Starling + Away3D |

---

## 4. What runs where

- **Hosted CI (every push):** build the raw + idiomatic layers, then **lint / type-check / package**.
  No behavioral tests run here: there is no Gaming Runtime, and (by design) no mocks to stand in for
  it.
- **Self-hosted GDK runner (gated / scheduled):** the **entire live test suite**. The `XUser`
  scenario plus any extended **soak / leak / race-under-load** runs. This is the only authoritative
  signal; it runs on merge, nightly, or on-demand depending on runner availability.

Every plan repeats: as a **risk** (§12): that the live suite cannot run on hosted CI, so a
GDK-capable runner is a hard dependency for validation.

---

*This strategy is intentionally shared. What differs per language is the **host/framework matrix**
(§2), the **test runner** (§3), and the mechanics of hosting the runtime in a package (§1):
captured in each plan's §10.*
