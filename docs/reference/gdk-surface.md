# The Microsoft GDK Public Surface: Language-Agnostic Reference

> **Purpose.** This document describes the Microsoft Game Development Kit (GDK) *as it ships
> today*, in language-neutral terms. It is the **source of truth** that every projection plan in
> this repository cites, so that each plan can focus on *what makes a projection idiomatic for its
> target language* instead of re-describing the native surface. If the GDK changes, update this
> document first; the plans inherit from it.

This is a description of the native API, **not** a projection plan and **not** projection code.

---

## 1. Source of truth

All facts here were read directly from an installed GDK on a Windows host:

- **Header/library root:** `C:\Program Files (x86)\Microsoft GDK\<edition>\windows\include`
  and `...\windows\lib\{x64,arm64}`.
- **Pinned edition:** `260404` (GRDK "April 2026" edition; product build `10.0.26100.7822`).
- **Editions installed on the reference machine:** `250404`, `251001`, `260400`, `260404`. Plans pin
  to `260404` and note that regeneration against a newer edition is a maintenance step.

### Why `windows\include` and not `GRDK\GameKit\Include`

The GDK exposes the same headers in two places. **We standardize on the `windows` tree** (the
PC / `Gaming.Desktop.x64` layout: the header/lib set a Windows GDK title actually links against):

- The **core `X*` runtime headers are byte-identical** between `GRDK\GameKit\Include` and
  `windows\include` (verified: `XUser.h`, `XGameRuntime.h`, `XAsync.h`, `XTaskQueue.h`,
  `XSystem.h`, `XGameRuntimeFeature.h` all match by SHA-256).
- The **`windows` tree is a superset.** It additionally ships the full XBOX Live Services C stack
  and supporting libraries that the GameKit subset does not surface: `GameChat2*.h` and the
  `xsapi-c/`, `httpClient/`, `playfab/` (plus `cpprest/`, `pplx/`, `xsapi-cpp/`) include folders,
  with matching import libs under `windows\lib\x64`.
- **Only the `windows` tree ships `arm64` import libraries.** `windows\lib` contains both `x64`
  and `arm64`; `GRDK\GameKit\Lib` contains only `amd64` (plus loose `xgameruntime.thunks.*`).
  Every projection in this program declares a `win-arm64` tier, so the GameKit tree **cannot**
  satisfy it. This alone settles the choice.

Projecting from the `windows` tree therefore covers the **entire surface a real title consumes**,
not just the base runtime.

### Resolving the paths

Do **not** hardcode an absolute path, and do **not** use `%GRDKLatest%`: that variable points at
the `GRDK\` subtree. Derive both directories from the edition root:

| Variable | Value on the reference machine | Use |
|---|---|---|
| `%GameDKCoreLatest%` | `C:\Program Files (x86)\Microsoft GDK\260404\` | ✅ Edition root. Build the paths below from this |
| `%GRDKLatest%` | `C:\Program Files (x86)\Microsoft GDK\260404\GRDK\` | ❌ Wrong subtree for this program |
| `%GXDKLatest%` | `C:\Program Files (x86)\Microsoft GDK\251001\GXDK\` | ❌ Console tree. Out of scope (§7) |

```
%GameDKCoreLatest%windows\include          # all headers
%GameDKCoreLatest%windows\lib\x64          # import libs, x64
%GameDKCoreLatest%windows\lib\arm64        # import libs, arm64
%GameDKCoreLatest%windows\bin\{x64,arm64}  # redistributable DLLs
```

Binding generators (`bindgen`, `ClangSharp`, `jextract`, `cffi`, `node-gyp`, …) must be pointed at
`windows\include`, and linkers at the architecture-matched `windows\lib` directory.

---

## 2. The surface at a glance (layered stack)

The GDK is a **layered set of flat C libraries** that all share one calling model. From the bottom
up:

```
+--------------------------------------------------------------+
|  Game / engine (the projection's consumer)                   |
+--------------------------------------------------------------+
|  PlayFab (Core / Services / Multiplayer / GameSave)          |  backend services
|  Party / PartyXboxLive        GameChat2                       |  networking + voice/text
+--------------------------------------------------------------+
|  XSAPI  (Microsoft.Xbox.Services.*.C)                        |  XBOX Live: social, presence,
|         achievements, multiplayer, leaderboards, title stg    |  stats, privacy, RTA ...
+--------------------------------------------------------------+
|  libHttpClient  (async + HTTP + WebSocket)                   |  transport + async engine
+--------------------------------------------------------------+
|  Core runtime  (xgameruntime.dll):                           |  XGameRuntime, XUser, XSystem,
|  XUser XSystem XPackage XStore XGameSave XGameUI XNetworking  |  XPackage, XStore, XGameSave,
|  XTaskQueue XAsync XError XThread XGameStreaming ...          |  XTaskQueue/XAsync, XError ...
+--------------------------------------------------------------+
|  GameInput (COM-style)                                       |  input: DIFFERENT pattern, see §7
+--------------------------------------------------------------+
```

| Layer | Headers | Import lib(s) | What it provides |
|---|---|---|---|
| **Core runtime** | `XGameRuntime*.h`, `XUser.h`, `XSystem.h`, `XPackage.h`, `XStore.h`, `XGameSave*.h`, `XGameUI.h`, `XNetworking.h`, `XGameStreaming.h`, `XThread.h`, `XError.h`, `XTaskQueue.h`, `XAsync*.h` | `xgameruntime.lib` (+ `xgameruntime.thunks.lib`) | Runtime init/feature-gating, users & identity, system/console info, packages & installs, store/commerce, game save, title UI, networking, streaming, the task-queue/async engine, error hook. |
| **libHttpClient** | `httpClient/*.h` (`async.h`, `httpClient.h`, `mock.h`, `trace.h`, …) | `libHttpClient.lib` | The async primitives (`XAsyncBlock`, task queues) plus HTTP/WebSocket. The async model below **originates here** and is shared by every layer. |
| **XSAPI** | `xsapi-c/*.h` (`xbox_live_global_c.h`, `achievements_c.h`, `social_c.h`, `presence_c.h`, `multiplayer_activity_c.h`, `leaderboard_c.h`, `title_storage_c.h`, `privacy_c.h`, `profile_c.h`, `real_time_activity_c.h`, …) | **bind `Microsoft.Xbox.Services.C.Thunks.dll`** (425 exports; the `Microsoft.Xbox.Services.{142,143}.C.lib` static libs are for C++ titles, see §below) | XBOX Live services. Same flat-C idioms; handle type `XblContextHandle`; on GDK `XblUserHandle == XUserHandle`. Requires `libHttpClient.dll` alongside it. The legacy multiplayer headers (`multiplayer_manager_c.h`, `multiplayer_c.h`, `matchmaking_c.h`) are **out of scope**; see §7. |
| **GameChat2** | `GameChat2.h`, `GameChat2_c.h` | `GameChat2.lib` | Real-time voice/text chat (a C++ API with a `_c` flat-C surface). |
| **PlayFab** | `playfab/*.h` | `PlayFabCore.lib`, `PlayFabServices.lib`, `PlayFabMultiplayer.lib`, `PlayFabGameSave.lib` | PlayFab backend (login, economy, data), game save, and **Multiplayer** (Lobby + Matchmaking). Multiplayer uses the **state-change pattern**, not `XAsyncBlock`; see [`state-change.md`](./state-change.md). |
| **Party** | `party\Party.h` over `party\Party_c.h`; `PartyXboxLive.h` over `PartyXboxLive_c.h` | `Party.lib`, `PartyXboxLive.lib` | Low-level multiplayer networking + voice/text transport. C++ class API with a **`_c` flat-C surface** (the projection target, like GameChat2); uses the **state-change pattern** ([`state-change.md`](./state-change.md)). |
| **GameInput** | `GameInput.h` | `GameInput.lib` | Input. **COM-style** (interfaces/`Release`), not the flat-C model; see scope §7. |

> Everything from **Core runtime through XSAPI / GameChat2 / PlayFab (the `_c` surfaces)** obeys the
> *single* set of conventions in §4. That uniformity is the entire reason a projection can scale:
> map the shapes once, apply everywhere.

---

## 3. Environment the headers assume

- **C++11 is required to even parse the headers**: every header begins with
  `#if !defined(__cplusplus) #error C++11 required`. They use `enum class`, `noexcept`, and
  namespaced constants. A projection's binding generator must invoke the C++ front end, not a
  plain C parser.
- **`extern "C"` linkage**: despite requiring the C++ compiler, all entry points have C linkage
  (no name mangling, no overloads), so FFI/`dlopen`-style binding is straightforward once parsed.
- **Windows SDK dependency**: headers pull in Windows types (`HRESULT`, `HANDLE`, `CALLBACK`,
  SAL annotations like `_In_`/`_Out_`, and `APP_LOCAL_DEVICE_ID` used by `XUser` device
  association). Binding generation must supply Windows SDK include paths and the usual `WIN32`
  defines.
- **Architectures**: `windows\lib` ships both **`x64`** and **`arm64`** import libs. `win-x64` is
  the primary target; `win-arm64` is available. (No 32-bit.)

---

## 4. Universal C-ABI conventions (the shapes every projection maps)

These are the recurring shapes. Each projection plan's "idiomatic mapping table" is essentially a
per-language answer to this section.

### 4.1 Function declaration form
- `STDAPI Foo(...)` → returns **`HRESULT`** (the overwhelmingly common form).
- `STDAPI_(T) Foo(...)` → returns a **non-HRESULT** value `T` (e.g. `void`, `bool`, `int32_t`, or a
  small struct like `XSystemGetAnalyticsInfo`).
- Nearly all entry points are `noexcept`.
- Parameters carry **SAL annotations** (`_In_`, `_Out_`, `_Inout_`, `_Out_writes_bytes_to_`,
  `_In_opt_`, `_Field_z_`, …) that document ownership, direction, nullability, and buffer sizing:
  invaluable metadata for generating a safe idiomatic layer.

### 4.2 Initialization & lifecycle
Each layer has an explicit init/teardown pair; a projection typically wraps these in a scope/guard:
- **Runtime:** `XGameRuntimeInitialize()` / `XGameRuntimeInitializeWithOptions(const XGameRuntimeOptions*)`
  and `XGameRuntimeUninitialize()`. Options select the game-config source
  (`XGameRuntimeGameConfigSource::{Default,Inline,File}`).
- **XSAPI:** `XblInitialize(const XblInitArgs*)` / `XblCleanupAsync(XAsyncBlock*)`. Note the
  asymmetry: **init is synchronous but teardown is async**, because XSAPI must drain RTA
  subscriptions and in-flight HTTP before it can unload; a projection must await it rather than
  fire-and-forget, and must not tear the task queue down first. There is no synchronous
  `XblCleanup`. `XblInitArgs` carries the title's **SCID** (`const char* scid`, required) and an
  optional `XTaskQueueHandle queue` for XSAPI's internal work (telemetry, RTA); leave the queue null
  to get a threadpool-backed default. Optional `XblMemSetFunctions(...)` (see §4.9) must be called
  *before* init. XSAPI init comes **after** `XGameRuntimeInitialize`, and an `XblContextHandle` is
  built per signed-in user from that user's `XUserHandle`.

### 4.3 Handles & resource ownership
Objects are opaque pointers behind a `typedef`, e.g. `typedef struct XUser* XUserHandle;`,
`typedef struct XTaskQueueObject* XTaskQueueHandle;`, `typedef struct XblContext* XblContextHandle;`.
The recurring operations:
- **Close**: `X*CloseHandle(handle)` (e.g. `XUserCloseHandle`, `XTaskQueueCloseHandle`). Handles are
  **reference-counted**; close releases one reference.
- **Duplicate**: `X*DuplicateHandle(handle, out dup)` adds a reference / yields an independent handle.
- **Compare / identity**: some types offer `X*Compare` (e.g. `XUserCompare` returns an ordering
  `int32_t`) for equality/sorting.
- **Validity**: `XSystemIsHandleValid`, plus `XSystemHandleTrack` for a global create/destroy hook
  across all handle types (`XSystemHandleType`).

**Projection duty:** a resource type with deterministic release (RAII / `IDisposable` /
`AutoCloseable` / context manager / `Drop`), copy-via-Duplicate semantics, and value equality where
`Compare` exists.

### 4.4 Asynchronous model (the heart of the surface)
Defined in `XAsync.h` + `XTaskQueue.h` (from libHttpClient) and reused everywhere.

- **`XAsyncBlock`** is a caller-owned struct that must **outlive the call**:
  ```c
  struct XAsyncBlock {
      XTaskQueueHandle          queue;     // where callbacks are dispatched
      void*                     context;   // caller context passed to callback
      XAsyncCompletionRoutine*  callback;  // optional completion callback
      unsigned char internal[sizeof(void*) * 4];  // reserved (32 bytes on 64-bit)
  };
  ```
- **Start / result pairing.** An async API is a *pair*: a `...Async(args..., XAsyncBlock*)` starter
  and a `...Result(XAsyncBlock*, out...)` retriever (e.g. `XUserAddAsync` → `XUserAddResult`).
  When the operation completes, the completion callback fires and the caller calls the `Result`
  function to obtain the payload.
- **Result sizing.** For variable-size payloads there is a three-call form:
  `...Async` → `...ResultSize(XAsyncBlock*, out size)` → `...Result(XAsyncBlock*, size, buffer, ...)`
  (e.g. `XUserGetGamerPictureAsync` / `...ResultSize` / `...Result`). Generic accessors
  `XAsyncGetStatus` and `XAsyncGetResultSize` also exist.
- **Cancellation.** `XAsyncCancel(XAsyncBlock*)` cancels; the operation then completes with
  **`E_ABORT`** from its `Result`/status call.
- **Task queues** (`XTaskQueue.h`) decide *where/when* callbacks run. A queue has two **ports**
(`Work` and `Completion`) each with a **dispatch mode**:
  - `Manual`: callbacks run only when the app calls `XTaskQueueDispatch(queue, port, timeoutMs)`
    (the game-loop / "pump" mode).
  - `ThreadPool`: system thread pool, possibly concurrent.
  - `SerializedThreadPool`: thread pool, one at a time.
  - `Immediate`: run inline on the submitting thread.
  - A process-wide default queue exists (`XTaskQueueGet/SetCurrentProcessTaskQueue`).
- **Queue shutdown is terminate, then close.** `XTaskQueueTerminate` prevents new submissions and
  completes termination after queued callbacks have completed or been delivered as canceled. Wait
  for that completion (`wait=true` or the termination callback), then release the handle with
  `XTaskQueueCloseHandle`; termination does **not** close the handle. A duplicated queue handle still
  references the same queue and follows the same terminate → wait → close sequence.

**Projection duty:** map the start/result pair to the language's async idiom (Task/Future/Promise/
coroutine/blocking-with-context), surface **cancellation → the language's cancel type**, translate
**`E_ABORT` → "operation canceled"**, and offer a **game-loop pump** so completions can resume on the
title's update thread (`Manual` port + `XTaskQueueDispatch` per frame).

### 4.5 Events & callbacks
Two related patterns:
- **Runtime events**: `X*RegisterForXxxEvent(queue, context, callback, out XTaskQueueRegistrationToken)`
  paired with `X*UnregisterForXxxEvent(token, wait)`. The callback is dispatched on the supplied task
  queue; `wait` on unregister optionally blocks for in-flight callbacks (e.g.
  `XUserRegisterForChangeEvent` / `XUserUnregisterForChangeEvent`). `XTaskQueueRegistrationToken`
  wraps a `uint64_t`.
- **XSAPI handlers**: use an `int32_t` **`XblFunctionContext`** token returned at registration and
  passed back to unregister.

**Projection duty:** expose these as the language's first-class event/subscription idiom
(`event`/delegate, listener + closeable registration, `EventEmitter`, channel, callback list),
manage the token + keep any callback trampoline alive for the subscription's lifetime, and
unsubscribe on dispose.

### 4.6 Enumerations & flags
- Enums are `enum class E : uint32_t` (fixed underlying type).
- **Flag** enums are additionally marked with `DEFINE_ENUM_FLAG_OPERATORS(E)` (e.g.
  `XUserAddOptions`, `XErrorOptions`, `XUserPrivilegeOptions`): these are bit sets.

**Projection duty:** distinguish plain enums from flag/bit-set enums and use each language's native
form (`[Flags]`, `bitflags!`, `EnumSet`, `IntFlag`, typed-int + bitwise).

### 4.7 Variable-length output (two-call buffer pattern)
Synchronous variable-length reads use a **size-then-fill** convention: call once to learn the byte
count, allocate, call again to fill: e.g.
`XUserGetGamertag(user, component, size, buffer, out used)`,
`XSystemGetConsoleId(size, buffer, out used)`. Sizes are frequently exposed as named constants
(`XUserGamertagComponent*MaxBytes`, `XSystemConsoleIdBytes`, …).

Text comes in **UTF-8 (`char*`)** and, for several APIs, parallel **UTF-16 (`wchar_t*`)** variants
(e.g. `XUserGetTokenAndSignature` vs `...Utf16`). Projections should hide the two-call dance behind a
single call returning the language's natural `string` / byte array, choosing the encoding that fits
the platform.

### 4.8 Error model
- The universal result type is **`HRESULT`** (`S_OK` == success; `FAILED(hr)` for errors).
- **Facilities/ranges** seen in the headers: `FACILITY_GAME` (2340) for GDK codes, plus XBOX Live.
  Representative named codes (from `XGameErr.h`, `xsapi-c/errors`):
  - Runtime: `E_GAMERUNTIME_NOT_INITIALIZED` (`0x89240100`), `E_GAMERUNTIME_DLL_NOT_FOUND`,
    `E_GAMERUNTIME_VERSION_MISMATCH`, `E_GAMERUNTIME_UNINITIALIZE_ACTIVEOBJECTS`, …
  - User: `E_GAMEUSER_NO_DEFAULT_USER` (`0x89245106`), `E_GAMEUSER_MAX_USERS_ADDED`,
    `E_GAMEUSER_SIGNED_OUT`, `E_GAMEUSER_RESOLVE_USER_ISSUE_REQUIRED`, `E_GAMEUSER_NO_TOKEN_REQUIRED`,
    `E_GAMEUSER_USER_NOT_IN_SANDBOX` (`0x8015DC12`), …
  - XBOX Live: `E_XBL_RUNTIME_ERROR` (`0x89235200`), `E_XBL_RTA_SUBSCRIPTION_LIMIT_REACHED`,
    `E_XBL_AUTH_NO_TOKEN`, `E_XBL_NOT_INITIALIZED`, …
  - Canceled async → **`E_ABORT`**.
- **Diagnostic hook.** `XErrorSetCallback(callback, context)` registers a global error-report
  callback; `XErrorSetOptions(...)` chooses debug behavior (`OutputDebugStringOnError`,
  `DebugBreakOnError`, `FailFastOnError`) separately for the debugger-present / not-present cases.

**Projection duty:** translate failing `HRESULT`s into the language's error idiom (typed exception
hierarchy / `Result` / `(value,error)`), preserve the numeric HRESULT for diagnostics, map the
well-known codes to friendly types, and route `E_ABORT` to the cancellation idiom rather than a
generic error.

### 4.9 Memory hooks
Several layers let the title own allocation. XSAPI exposes
`XblMemSetFunctions(XblMemAllocFunction, XblMemFreeFunction)` (with an `HCMemoryType` category),
callable only before `XblInitialize` and not again until `XblCleanupAsync` completes. libHttpClient and
others have
equivalents. A projection may leave these at default, but should expose them for engines with custom
allocators.

### 4.10 Feature detection
`XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature)` reports whether a subsystem is present after
`XGameRuntimeInitialize`. `XGameRuntimeFeature` enumerates the runtime subsystems
(`XUser = 18`, `XGameSave = 7`, `XStore = 14`, `XSystem = 15`, `XTaskQueue = 16`, …). Projections
should surface this as a capability query so callers can gate features gracefully.

### 4.11 Scalar conversions & sentinel values
Native scalars carry sentinels the language's own type may not be able to represent, and a
projection that converts them naively turns service data into an exception.

- **Timestamps.** Date fields are `time_t`: seconds since the Unix epoch, and the services use
  out-of-band values for "unset" and "never". Zero means unset; a licence or offer that never expires
  comes back as a value far outside the range most date types accept. Convert by **saturating** to
  the language's minimum and maximum instants, never by passing the raw value to a checked
  constructor. `gdk-dotnet` shipped the naive form and it threw `ArgumentOutOfRangeException` from
  inside a native enumeration callback the first time it saw a real game licence, where the failure
  is both fatal and hard to attribute.
- **Sizes and counts.** `size_t`/`uint32_t` counts are unsigned; a projection whose natural integer
  is signed must decide explicitly what an out-of-range value means rather than wrapping.
- **`XUserLocalId` and friends.** Opaque scalar identifiers are values, not handles: copy them, do
  not close them, and do not assume they are stable across sign-out.

> **Projection duty.** Every native scalar that reaches the public surface needs a stated conversion
> rule, and any conversion that can fail must saturate or return the language's "absent" value.
> Conversions run inside native callbacks where an exception has nowhere to go.

---

## 5. Native modules, linkage & architectures

### Which module to bind: `xgameruntime.thunks.dll`, not `XGameRuntime.dll`

> **This corrects earlier guidance in this document and in the per-language plans.** It was proven
> on a real packaged title during the `gdk-dotnet` pilot; it applies to **every** non-C++ language
> in this programme.

`C:\Windows\System32\XGameRuntime.dll` exports **seven private ordinals only**
(`InitializeApiImpl`, `InitializeApiImplEx`, `InitializeApiImplEx2`, `UninitializeApiImpl` and
three more). It exports **no `X*` API whatsoever**. Every public `X*` entry point is *static code*
inside the import library `xgameruntime.lib`; those stubs negotiate a **compile-time version
cookie** through the private ordinals. A C++ title reaches the API only because it statically links
that lib. **A projection that binds `XGameRuntime.dll` by module name can never work in any
language**: every call raises the platform's missing-entry-point error.

The GDK ships the correct module:

```
%GameDKCoreLatest%windows\bin\{x64,arm64}\xgameruntime.thunks.dll   (~145 KB)
```

It re-exports **390** flat `__stdcall` C entry points, including `XGameRuntimeInitialize`,
`XUserAddAsync` and `XTaskQueueCreate`. Bind this module. It is **not** installed system-wide, so
it must be **redistributed into the package layout next to the game executable**.

Consequences every projection must honour:

- Bind `xgameruntime.thunks.dll` by module name; never `XGameRuntime.dll`.
- Copy the thunks DLL into the package layout as a build step, and **hard-fail the packaging step
  if it is absent**, a missing thunks DLL otherwise surfaces at runtime as an initialization
  failure that looks like an environment problem.
- **14 APIs are absent from the thunks DLL** in edition `260404`, so they are not reachable by
  binding that DLL alone. Declare a binding for one and you get a missing-entry-point error that
  projections commonly translate into `E_GAMERUNTIME_VERSION_MISMATCH`, a misleading diagnosis
  that hides a binding bug. **Verify every entry point against the DLL's export table before
  declaring it.** The authoritative list lives in `gdk-dotnet/eng/unexported-apis.md`.
- **Unlike XSAPI, this is a real gap: none of the 14 is behind a preprocessor guard.** Verified by
  preprocessing `XGameRuntime.h` with `cl /EP /D_GAMING_DESKTOP`. They split two ways:
  - **8 are declared in the public headers but unexported**: a genuine header/DLL mismatch:
    `XGameInvite{Register,Unregister}ForPendingEvent`, `XGameInviteAcceptPendingInvite`,
    `XNetworkingSetConfigurationSetting`, `XGameStreamingGetAssociatedFrame`,
    `XUserGetMsaTokenSilently{Async,Result,ResultSize}`. The headers promise what the DLL will not
    deliver, which is exactly the trap above.
  - **6 have no public declaration at all** and exist only as symbols inside `xgameruntime.lib`:
    `XErrorReport`, `XThreadVerifyNotTimeSensitive`, `XGameStreamingSendDebugMessageToClient`,
    `XGameUiShowManageSpace{Async,Result}`, `XPackageGetIdentifier`. That is precisely why a
    static-linking shim could reach them and why binding the DLL never can.
- **The gap is edition-specific, so re-verify it on every edition bump.** Edition `260400` omitted
  **49**; `260404` omits **14**. `gdk-dotnet` previously reached the missing 48 (the 49th,
  `XPackageMount`, is a dead `E_NOTIMPL` stub *in the library itself*) through a **custom native
  shim DLL** that statically linked `xgameruntime.lib` and re-exported them by name through a
  `.def` file. That shim was **retired** once `260404` exported everything the projection surfaced;
  the technique is recorded here because it remains the answer if a future edition regresses, but a
  projection should not build one speculatively: it adds an MSVC + GDK build dependency that
  hosted CI cannot satisfy. DLC mounting never needed it: the exported `XPackageMountWithUiAsync`
  covers it. See `gdk-dotnet/eng/unexported-apis.md`. `gdk-dotnet` previously reached the missing 48 (the 49th,
  `XPackageMount`, is a dead `E_NOTIMPL` stub *in the library itself*) through a **custom native
  shim DLL** that statically linked `xgameruntime.lib` and re-exported them by name through a
  `.def` file. That shim was **retired** once `260404` exported everything the projection surfaced;
  the technique is recorded here because it remains the answer if a future edition regresses, but a
  projection should not build one speculatively: it adds an MSVC + GDK build dependency that
  hosted CI cannot satisfy. DLC mounting never needed it: the exported `XPackageMountWithUiAsync`
  covers it. See `gdk-dotnet/eng/unexported-apis.md`.

### Which module to bind: XSAPI is the same story, with its own thunks DLL

XBOX Live Services (XSAPI) is **not** part of the Gaming Runtime and is **not** reachable through
`xgameruntime.thunks.dll`. It is a separate library with its own redistributable, and it repeats the
static-lib trap exactly: `Microsoft.Xbox.Services.{142,143}.C.lib` are **toolset-versioned static
import libs for C++ titles** (142 = VS2019, 143 = VS2022) and are useless to a projection. The
module to bind is:

```
%GameDKCoreLatest%windows\bin\{x64,arm64}\Microsoft.Xbox.Services.C.Thunks.dll   (~1.9 MB)
```

Verified on edition `260404` (x64): it exports **425** flat C entry points: 423 plain `Xbl*`
(`XblInitialize`, `XblContextCreateHandle`, `XblProfileGetUserProfileAsync`, …) plus two
`XblWrapper_`-prefixed aliases (`XblWrapper_XblInitialize`, `XblWrapper_XblCleanupAsync`). Note it
exports **no `XAsync*`/`XTaskQueue*` symbols**: the async engine XSAPI shares with the core runtime
still comes from `xgameruntime.thunks.dll`, so a projection that uses XSAPI needs **both** modules
bound and both DLLs shipped.

**It has a hard dependency on `libHttpClient.dll`,** which is a separate redistributable in the same
directory. Shipping the thunks DLL without it produces a load failure at first XSAPI call, not at
process start, an error that reads like a service outage rather than a packaging bug. Both DLLs
also import `MSVCP140.dll` / `VCRUNTIME140{,_1}.dll`, so the VC++ runtime must be present in the
package or on the machine.

**XSAPI has essentially no export gap on GDK: exactly one function.** This corrects earlier
guidance in this document, which claimed 23. That number came from text-scanning the headers
without running the preprocessor, so it counted declarations that a GDK build never sees.

Verified by preprocessing `xsapi-c/services_c.h` with `cl /EP /D_GAMING_DESKTOP` against
`%GameDKCoreLatest%windows\include` and diffing against `dumpbin /exports` (260404, x64): **437
`Xbl*` names survive the preprocessor, and every one of them is exported except `XblSetApiType`**,
an internal API (its `XblApiType` enum is `{ XblCApi, XblCPPApi }`) that no projection should
surface. Everything a GDK title can legally call is callable.

Discount the header-side residue before comparing: 10 of the remaining names are callback
`typedef`s (`XblRealTimeActivityResyncHandler`, `XblStatisticChangedHandler`, …) and 3 are enum
operator casts (`XblMultiplayerSessionChangeTypes`, `XblMutableRoleSettings`,
`XblSocialManagerExtraDetailLevel`). They match a naive `Name(` regex and are not entry points.

**The APIs that look missing are gated out of the GDK build by design.** They fall into two
categories, neither of which is a defect to work around:

| Gate | On GDK | Count | APIs |
|---|---|---|---|
| `#if HC_PLATFORM == HC_PLATFORM_WIN32 \|\| HC_PLATFORM_IS_EXTERNAL` | **false** | 9 | `XblGameInviteRegisterForEventAsync`/`Result`, `XblGameInviteUnregisterForEventAsync`, `XblGameInvite{Add,Remove}NotificationHandler`, `XblAchievementUnlock{Add,Remove}NotificationHandler`, `XblMultiplayerActivity{Add,Remove}InviteHandler` |
| `#if HC_PLATFORM == HC_PLATFORM_IOS \|\| ANDROID [\|\| UWP]` | **false** | 2 | `XblNotification{Subscribe,Unsubscribe}{To,From}NotificationsAsync` |
| `#ifdef XSAPI_INTERNAL_EVENTS_SERVICE` | **undefined** | 6 | `XblLocalStorageSetHandlers`, `XblLocalStorage{Read,Write,Clear}Complete`, `XblEventsSetMaxFileSize`, `XblEventsSetStorageAllotment` |

The first two rows are **non-GDK surface**: `config.h` sets `HC_PLATFORM = HC_PLATFORM_GDK` whenever
`_GAMING_DESKTOP`/`_GAMING_XBOX` is defined, and `HC_PLATFORM_IS_EXTERNAL` covers only Switch/PS4/
PS5/Generic. On GDK the equivalent functionality comes from the **core runtime** instead:
`XGameInvite*` and `XUser*` in `xgameruntime.thunks.dll`, which is why XSAPI does not duplicate it.
The third row is **internal-only Microsoft build surface**, absent on every platform in a shipping
build, not just GDK.

A projection therefore must **not** treat Social, Privacy, Presence, Achievements, Notification or
Multiplayer-activity as degraded. Their handler-registration entry points
(`XblSocial{Add,Remove}FriendRequestCountChangedHandler`,
`XblPrivacy{Add,Remove}{BlockList,MuteList}ChangedHandler`, `XblPresenceAdd*ChangedHandler`, …) are
unguarded and exported; project them in full. On `260400` the Privacy and Social six were genuinely
unexported: that edition had **7** declared-but-unexported functions, so this *is* edition-
specific and worth re-verifying, but the correct current answer is 1, not 23.

**Verify with the preprocessor, not a text scan.** Compile a translation unit that includes the
headers with the GDK platform macros defined, then diff the surviving declarations against `dumpbin
/exports`. A raw `grep` over the header tree overstates the gap by ~20x, and the GDK's XSAPI headers
cannot even be preprocessed as a non-GDK build: `Xal/xal_types.h` includes `Xal/xal_win32.h`, which
the GDK does not ship.

Consequences every projection must honour:

- Bind `Microsoft.Xbox.Services.C.Thunks.dll` by module name; never a `Microsoft.Xbox.Services.*.C.lib`.
- Redistribute **`Microsoft.Xbox.Services.C.Thunks.dll` *and* `libHttpClient.dll`** into the package
  layout, and hard-fail packaging if either is missing: exactly as for the core thunks DLL.
- Take XSAPI headers from `%GameDKCoreLatest%windows\include\xsapi-c`, **not** from
  `GRDK\ExtensionLibraries\Xbox.Services.API.C\Include`. The two are parallel copies; the `windows`
  tree is the one this programme standardizes on, and it is the only one with `arm64` libs.
- Do not bind the `.Debug` variants (`Microsoft.Xbox.Services.C.Thunks.Debug.dll`) in shipping
  configurations.

### Modules and architectures

- **Core runtime module:** `xgameruntime.thunks.dll`, redistributed from
  `%GameDKCoreLatest%windows\bin\{x64,arm64}` (see above). `xgameruntime.lib` under
  `windows\lib\{x64,arm64}` remains the linkage path for C++ titles and for any shim.
- **XSAPI module:** `Microsoft.Xbox.Services.C.Thunks.dll` **plus its hard dependency
  `libHttpClient.dll`**, redistributed from the same directory (see above).
- **Redistributables a projection ships in the package layout.** These are *not* installed
  system-wide; everything the title binds must travel with it:

  | DLL | Needed when | Size (x64, 260404) |
  |---|---|---|
  | `xgameruntime.thunks.dll` | always | ~142 KB |
  | `Microsoft.Xbox.Services.C.Thunks.dll` | any XSAPI use | ~1.9 MB |
  | `libHttpClient.dll` | with XSAPI (hard dependency) | ~329 KB |
  | `GameChat2.dll` | GameChat2 | ~1.3 MB |
  | `PlayFabCore.dll`, `PlayFabServices.dll`, `PlayFabMultiplayer.dll`, `PlayFabGameSave.dll` | corresponding PlayFab surface | 1.4–5.4 MB |
  | `Party.dll`, `PartyXboxLive.dll` | Party voice/text | 3.4 MB / 1.1 MB |

- **Service *link* libraries** (for C++ titles and shims, not for projections) ship under
  `windows\lib\{x64,arm64}`: `libHttpClient.lib`, `Microsoft.Xbox.Services.{142,143}.C.lib`
  (VS toolset-versioned) + `Microsoft.Xbox.Services.C.Thunks.lib`, `GameChat2.lib`,
  `PlayFabCore/Services/Multiplayer/GameSave.lib`, `Party.lib`, `PartyXboxLive.lib`,
  `GameInput.lib`.
- **Architectures:** `x64` (primary) and `arm64`. RIDs: `win-x64`, `win-arm64`.
- **Packaging reality:** a projection generally ships *its own* managed/idiomatic layer and binds
  every native surface through the redistributed thunks DLLs listed above, which it copies into the
  package layout at build time. The title must be a **packaged GDK app** (has
  `MicrosoftGame.config`) with a registered identity to exercise most services, and XSAPI
  additionally needs the title's **SCID** and a signed-in user.

---

## 6. Threading & reentrancy (summary)

- Completion callbacks and events run **wherever their task queue's port dispatches them**: a
  thread-pool thread, or the pumping thread under `Manual`. Callbacks from native worker threads
  cross into the projection's runtime, which matters for languages with a GIL (Python), a managed
  callback trampoline (.NET, JVM upcalls), or goroutine scheduling (Go).
- Handles are reference-counted; duplication/closing is safe to hand across threads as long as
  lifetime rules are respected.
- The **pump model** (`Manual` port + `XTaskQueueDispatch` per frame) is how a single-threaded game
  loop keeps all GDK continuations/events on its own update thread, a first-class concern for every
  game-facing projection.

---

## 7. Scope for projections (in / out)

**In scope (two async shapes over one flat-C ABI):** the core runtime (`XUser`, `XSystem`,
`XPackage`, `XStore`, `XGameSave`, `XGameUI`, `XNetworking`, `XGameStreaming`, `XError`,
`XTaskQueue`/`XAsync`), and the `_c` service APIs (XSAPI, libHttpClient, GameChat2's `_c` surface,
PlayFab's C SDKs including **PlayFab Multiplayer**, and **Party / PartyXboxLive** via their `_c`
surfaces). Most of this is the **`XAsyncBlock`-shaped** surface and one mapping covers it. A subset:
**PFMP Lobby/Matchmaking** and **Party**: uses a **second async shape**, the polled **state-change
pattern** (poll → dispatch → release, pumped from the game loop); it is specified separately in
[`state-change.md`](./state-change.md) with a worked Lobby slice in
[`multiplayer-pilot.md`](./multiplayer-pilot.md).

**Out of scope / special-cased (different shapes):**
- **GameInput** (`GameInput.h`) is a **COM-style** interface API (`IGameInput*`, `AddRef`/`Release`,
  `QueryInterface`), not the flat-C/`XAsyncBlock` model. Project it separately if needed.
- **XCurl** (`XCurl.h` / `XCurl.lib`, a libcurl-compatible HTTP surface) is **intentionally
  excluded**. Titles use it for raw HTTP, but wrapping curl is not a goal of these projections.
- **XAL** (`Xal/*.h`, the XBOX Authentication Library) is **intentionally excluded**. On GDK, user
  authentication is reached through `XUser`, which the projections already cover.
- **GXDK console APIs** (XBOX hardware) live under a separate licensed `GXDK` tree (not present in
  this `windows`/PC layout) and are **out of scope** here.
- **The legacy XSAPI multiplayer stack**: **Multiplayer Manager (MPM)**
  (`xsapi-c/multiplayer_manager_c.h`), **Multiplayer Session Directory (MPSD)**
  (`xsapi-c/multiplayer_c.h`), and **SmartMatch matchmaking** (`xsapi-c/matchmaking_c.h`): is
  **intentionally excluded**. Session, roster, and matchmaking needs are served by **PlayFab Lobby /
  Matchmaking** plus **Party**, which are cross-platform, actively invested in, and already the
  state-change pilot (see [`multiplayer-pilot.md`](./multiplayer-pilot.md)). Projecting a second,
  XBOX-only session model alongside them would double the surface, split the guidance, and steer
  titles onto the legacy path. The exclusion is also self-reinforcing: on `260404` these headers
  carry the largest concentration of deprecated entry points in XSAPI (`multiplayer_c.h` 17,
  `multiplayer_manager_c.h` 4). **Advertised-session and invite obligations are met through PlayFab
  Lobby plus XSAPI Multiplayer Activity (`multiplayer_activity_c.h`), not MPSD.**
- **Anything marked deprecated in the pinned headers**: see §7.1.

### 7.1 Deprecated APIs are never projected

A projection targets one pinned edition, so "deprecated" is a fact you can read off the headers. **No
deprecated entry point, struct, enum, or member is projected**: not hidden behind a feature flag,
not exposed "for completeness." Callers who need a deprecated API are on the wrong version of the
surface, and carrying it forward locks the projection to something the platform is removing.

The GDK does not remove these symbols, it only marks them, so they stay exported: they bind cleanly,
pass a smoke test, and look like ordinary coverage. That is why the rule has to be mechanical rather
than left to reviewer judgement. The reasons compound:

- **They no longer do what their name says.** The header notes are explicit:
  `XblRealTimeActivitySubscriptionGetState` "will always return `Unknown`",
  `XblRealTimeActivityAddSubscriptionErrorHandler`'s "callback will no longer be invoked",
  `XblRealTimeActivityActivate` is "no longer required". Projecting one hands the caller an API that
  compiles, runs, returns success, and silently does nothing.
- **They are undocumented.** Deprecated APIs are dropped from the public documentation site, so a
  caller who finds one on your surface has nowhere to look it up. Conversely, a projected member with
  no upstream doc page is a signal to go back to the header and check for a marker.
- **They distort the design.** The deprecated form is usually a per-kind variant of a newer unified
  call. Projecting both makes the newer one look like one option among several, forces an enum or a
  switch to carry the distinction, and creates double-notification hazards for a caller who
  subscribes to both. Dropping them frequently collapses a multi-branch design to a single path:
  removing the `XGameInvite`/`XGameProtocol` registrations in favour of `XGameActivationRegisterForEvent`
  eliminated a registration-source enum and a second trampoline shape outright.
- **They are the first thing a future edition removes**, turning a source-compatible upgrade into a
  load-time failure.

The replacement is normally named in the same header note. In practice it is either a "track"-style
API that manages the underlying subscription for you (`XblPresenceTrackUsers`,
`XblUserStatisticsTrackStatistics` in place of the per-user/per-statistic subscribe pairs) or a
single unified registration. Project the replacement and let the deprecated one go.

Do not soften this into an "obsolete"/"deprecated" annotation on your own surface. A native API that
returns success and does nothing is not a deprecated projected API: it is one that should never have
been exposed. Annotating it still ships it, still autocompletes, and still has to be supported.

Deprecation is marked several ways in edition `260404`; a generator must recognize **all** of them:

| Marker | Defined in | Used by |
|---|---|---|
| `__declspec(deprecated("…"))` directly on the declaration | (none) | core runtime: `XUser.h` (3), `XGameInvite.h` (5), `XGameStreaming.h` (3), `XGameProtocol.h` (2), `XPackage.h` (1) |
| `XBL_DEPRECATED`, `STDAPI_XBL_DEPRECATED`, `STDAPI_XBL_DEPRECATED_(type)` | `pal.h` | XSAPI: `multiplayer_c.h` (17), `real_time_activity_c.h` (6), `presence_c.h` (4), `multiplayer_manager_c.h` (4), `game_invite_c.h` (3), `social_c.h` (2), `user_statistics_c.h` (2) |
| `PF_DEPRECATED`, `PF_API_DEPRECATED`, `PF_API_DEPRECATED_(type)` | `PFPal.h` | PlayFab C SDK |
| `_XSAPICPP_DEPRECATED` | `types.h` | the C++ XSAPI surface |
| `CASABLANCA_DEPRECATED(x)` | `cpprest_compat.h` | cpprest (out of scope anyway) |

Two traps:

- **The macros expand to nothing under some configurations.** `pal.h` and `PFPal.h` both define their
  deprecation macro as empty on non-MSVC front ends, so a generator that keys off the *expanded*
  declaration will silently see nothing. Detect deprecation from the **unexpanded** header text, or
  keep an explicit denylist that is re-derived whenever the edition is re-pinned.
- **Some deprecations are documentation-only.** A number of declarations carry no attribute at all,
  only a `/// DEPRECATED. …will be removed in a future release` doc comment (this is how several
  RTA subscription-management and service-call-logging APIs are marked). These cannot be caught by
  codegen and must be caught by review when the edition is pinned or re-pinned.

Practically: **filter deprecated members out of the generated raw layer where the tooling can, and
maintain an explicit denylist for the rest.** Record the denylist in the plan, and re-verify it as
part of any edition re-pin, an API that was merely deprecated in one edition is often absent from
the next. Related: the exclusion of `XGameInvite.h` and `XGameProtocol.h`'s activation entry points
is *not* a scope judgement: those are deprecated in favour of `XGameActivation*`, which is what a
projection binds instead.

> **Projection duty.** Put the enforcement in the API-coverage tool, not in unit tests: it already
> reads the headers and the shipped exports, so it stays correct across editions without a
> hardcoded list. Have it recognize every marker above, report deprecated exports as their **own**
> category: distinct from unbound ones, so they neither read as coverage gaps nor get "filled in"
> by a later contributor, and fail the run if any is bound. Strip comments before matching
> bindings: interop files that name an absent symbol in a comment to explain its absence will
> otherwise be reported as binding it.

## 8. Projection implications (language-agnostic checklist)

Every projection plan, whatever the language, must answer these: driven entirely by §4:

1. **Binding generation**: run the C++ front end over `windows\include` with Windows SDK
   includes/defines; emit raw FFI for functions, structs, enums, and callback typedefs.
2. **Error mapping**: `HRESULT` → the language's error idiom; `E_ABORT` → cancellation; keep the
   numeric code; map well-known `E_GAMERUNTIME_*` / `E_GAMEUSER_*` / `E_XBL_*`.
3. **Resource model**: opaque handles → deterministic-release resource types; Duplicate = copy;
   Compare = equality.
4. **Async model**: start/result (± size) pairs → Task/Future/Promise/coroutine/blocking-with-cancel;
   a task-queue wrapper; a **game-loop pump**.
5. **Events**: register/unregister + token → first-class subscriptions with lifetime-managed
   trampolines.
6. **Enums & flags**: plain vs `DEFINE_ENUM_FLAG_OPERATORS` bit sets.
7. **Buffers & strings**: hide the two-call size pattern; pick UTF-8 vs UTF-16; return native
   strings / byte arrays.
8. **Lifecycle & features**: init/uninit guards; `XGameRuntimeIsFeatureAvailable` as a capability
   query; optional memory hooks.
9. **Pilot**: prove all of the above on **`XUser`** first (see `xuser-pilot.md`), then scale by the
   same rules.
10. **State-change subsystems**: PFMP (Lobby/Matchmaking) and Party use a **second async shape**: a
   polled state-change queue (poll → dispatch → release) pumped from the game loop, with a tagged
   union of change types, completions correlated by `asyncContext`, and a copy-out-before-`Finish`
   memory rule. Project it per [`state-change.md`](./state-change.md); prove it on the **PFMP Lobby**
   slice ([`multiplayer-pilot.md`](./multiplayer-pilot.md)).
11. **PlayFab trust & identity**: include active player/title/server APIs for tooling (including
    PFMP server declarations enabled by `PFMULTIPLAYER_INCLUDE_SERVER_APIS`), clearly separate
    privileged operations, and use `PFEntityHandle` whenever an operation accepts authentication
    input. Keep object-handle-authorized server methods and the authentication bootstraps.

---

## 9. Versioning & pinning

- Plans pin binding generation to a single edition (**`260404`** here) for reproducibility and record
  which editions were available (`250404` / `251001` / `260400` / `260404`).
- Because the surface is uniform and additive across editions, re-pinning to a newer edition is a
  mechanical regenerate-and-diff step, not a redesign.
- The core `X*` runtime is identical between the `GameKit` and `windows` header trees; the `windows`
  tree is the authoritative superset this repository targets (see §1).

---

## 10. Diagnostics, logging & tracing

Both XSAPI and PlayFab ride the shared **libHttpClient** transport, so transport-level tracing is
one shared mechanism:

| Sink | API (header) | Notes |
| --- | --- | --- |
| Transport trace level | `HCSettingsSetTraceLevel(HCTraceLevel)` (`httpClient/trace.h`) | Shared by XSAPI + PlayFab HTTP/WebSocket. |
| Trace callback | `HCTraceSetClientCallback(HCTraceCallback*)` (`trace.h`) | Route through the projection so levels can be mapped and sensitive fields scrubbed. |
| Trace to debugger | `HCTraceSetTraceToDebugger(bool)` (`trace.h`) | Direct native sink; bypasses projection filtering. Developer-only and off by default. |
| PlayFab file trace | `PFTraceEnableTraceToFile(...)` (`playfab/core/PFTrace.h`) | Direct native sink; bypasses projection filtering. Developer-only and explicit opt-in. |
| Error hook | `XErrorSetCallback` / `XErrorSetOptions` | Runtime error reporting: see §4.8. |
| Resilience knobs | `XblContextSettingsSet*` (`xsapi-c/xbox_live_context_settings_c.h`) | HTTP/WebSocket timeouts, retry delay, timeout window, QoS: tune, then observe via trace. |

The distinction is the data path. With `HCTraceSetClientCallback`, a line flows
`native library → projection callback → sanitizer → host logger`, so the projection can redact it.
With debugger/file tracing, the native library writes directly to that sink; the projection never
sees the line before it is emitted. Registering the callback as well does not make the direct copy
safe: it only creates a separate callback-routed copy that can be scrubbed.

> **Projection duty.** Bridge the trace callback into the language's logging framework with mapped
> levels, make the trace level configurable at runtime, prefer structured logs, and scrub tokens,
> signatures, and PII before callback-routed lines reach the host logger. Direct debugger/file sinks
> cannot be intercepted or scrubbed by the projection; keep them off by default and document that
> their output may contain sensitive service diagnostics (see
> [`security-privacy.md`](./security-privacy.md) §3).

---

## 11. Idiomaticity first; optimize measured hot paths

Outside the state-change APIs, the projection's primary goal is to feel native to the target
language. Hide native buffers, pointer lifetimes, and start/result pairs behind the language's normal
strings, collections, resources, errors, and async primitives. Normal language-level allocation is
acceptable when it produces the idiomatic API.

The state-change loop is the exception because its native ordering and borrow window are part of the
contract (see [`state-change.md`](./state-change.md)). Preserve `Start`→iterate→`Finish`, expose the
language's natural closed-variant representation, and avoid **unbounded or accidental** per-frame
churn. Do not require an allocation-free public surface when records, dataclasses, tables, or owned
copies are the language's idiom.

Callbacks run wherever their configured task-queue port dispatches them: the pumping thread for
`Manual`, a pool thread for `ThreadPool`, serialized on `SerializedThreadPool`, or inline for
`Immediate`. Marshal only when the chosen language/runtime requires it.

> **Projection duty.** Choose the most idiomatic safe API first. Optimize only measured hot paths,
> keeping lower-allocation overloads optional rather than forcing native buffer mechanics into the
> default surface.

---

## 12. Naming discipline

Naming is the part of a projection that is cheapest to get wrong and most expensive to change: it is
the whole public surface, it is what every sample and doc page repeats, and it cannot be revised
after the first release. `gdk-dotnet` re-derived its names twice before settling on a rule strict
enough to apply mechanically, and the rule is portable.

**Transform the native name, then stop.**

1. **Drop the subsystem prefix.** `XUserGetGamertag` → `GetGamertag`, `XblAchievementsUpdateAchievementAsync`
   → the achievements type's update operation. The prefix exists because C has no namespaces; every
   target language does, so keeping it stutters (`XboxLive.XblContext`).
2. **Drop the redundant family qualifier** once the member already sits on a type that names the
   family: `XblAchievementsGetAchievement` on an achievements service becomes `GetAchievement`, not
   `GetAchievementAchievement`.
3. **Preserve every remaining header term verbatim.** Do not "improve" `Gamertag` to `GamerTag`,
   `Scid` to `ServiceConfigurationId`, or `Xuid` to `UserId`. A developer reading GDK documentation,
   a header, or a Stack Overflow answer must be able to find the projected member by searching for
   the native term.
4. **Never add a prefix to resolve a collision.** If two members would collide, the containing types
   are wrong: split them. Adding `XboxLive`- or `Live`-style disambiguators re-introduces exactly
   the stutter step 1 removed, and it spreads: one disambiguated name licenses the next.

The rule is deliberately strict and mechanical, because a rule with judgement in it produces a
surface that is inconsistent in ways no reviewer can defend. The native surface does not collide
with itself, so a faithful transform does not either.

> **Projection duty.** Write the rule down in the implementation repo before naming the second
> family, and apply it as a review gate. Every name that deviates needs a recorded reason, and
> "it read better" is not one.

---

*See also: [`xuser-pilot.md`](./xuser-pilot.md): the concrete `XUser` slice every plan implements
first, expressed against the exact functions, enums, and structs in `XUser.h`.
[`state-change.md`](./state-change.md): the polled state-change pattern used by PFMP and Party, with
its worked slice in [`multiplayer-pilot.md`](./multiplayer-pilot.md).
[`security-privacy.md`](./security-privacy.md), [`compliance.md`](./compliance.md), and
[`glossary.md`](./glossary.md): cross-cutting obligations and terminology.*
