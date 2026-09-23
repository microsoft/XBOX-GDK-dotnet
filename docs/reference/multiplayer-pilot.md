# The PFMP Lobby Pilot — Shared State-Change Projection Contract

> **Purpose.** [`state-change.md`](./state-change.md) describes the polled state-change pattern
> abstractly. This document is the concrete, language-neutral **contract** for the one slice every
> plan implements to prove it: **PlayFab Multiplayer (PFMP) Lobby**. Lobby is the smallest subsystem
> that exercises the whole pattern — a polled drain, a tagged-union dispatch surfaced as a closed
> variant set, an operation completion correlated by an operation id, unsolicited notifications, and
> the hold-during-loop / `Finish`-on-scope-exit memory rule. Proving Lobby end-to-end validates a
> language's answer to Party and Matchmaking too, which are the same shapes again.

Read [`state-change.md`](./state-change.md) first (the pattern) and [`gdk-surface.md`](./gdk-surface.md)
for the universal conventions and the `XUser` identity this slice depends on. Facts are from
`windows\include\playfab\multiplayer\` (`PFMultiplayer.h`, `PFLobby.h`), edition `260404`. Signatures
are abbreviated; see the headers for full SAL.

---

## 1. Why PFMP Lobby is the state-change pilot

| Pattern (state-change.md) | Lobby proof point |
|---|---|
| Library lifecycle handle | `PFMultiplayerInitialize` / `PFMultiplayerUninitialize` → `PFMultiplayerHandle` |
| Identity dependency | `PFEntityHandle` — an authenticated **PlayFab** entity handle obtained from PlayFab Core (not `XUser` directly) |
| Poll drain (`Start`/`Finish`) | `PFMultiplayerStartProcessingLobbyStateChanges` / `…FinishProcessingLobbyStateChanges` |
| Tagged union + dispatch | `PFLobbyStateChangeType` discriminant → cast base `PFLobbyStateChange` to the derived struct |
| Completion variant correlated by op id | `PFMultiplayerCreateAndJoinLobbyWithEntityHandle(…, asyncContext, out lobby)` → `PFLobbyCreateAndJoinLobbyCompletedStateChange { result; asyncContext; lobby }` (the projected op id round-trips as `asyncContext`) |
| Handle ready-after-completion | the `PFLobbyHandle` returns synchronously but is not usable until its completion variant drains with `S_OK` |
| Unsolicited notifications | `PFLobbyMemberAddedStateChange`, `PFLobbyUpdatedStateChange` (no `asyncContext`) |
| Hold during loop; `Finish` on scope exit | loop variants are borrowed views valid only inside the drain window; `PFLobbyGetMembers` / `PFLobbyGetProperties` snapshot for arbitrary-time reads |
| Ordered teardown (loop variant) | `PFLobbyLeaveWithEntityHandle(…, asyncContext)` → `PFLobbyLeaveLobbyCompletedStateChange`, drained **before** `Uninitialize` |

If a projection expresses all of the above idiomatically, Party (`Party_c.h`) and Matchmaking
(`PFMatchmaking.h`) — the other two drains — are the same contract with different variants.

---

## 2. The contract, grouped by pattern

Each group lists the native declarations a plan must project and the **idiomatic obligation** (what
the projected API should feel like).

### 2.1 Library lifecycle & identity
```c
struct MultiplayerInitializationConfiguration { const char* titleId; /* … */ };   // PFMultiplayer.h:50
STDAPI PFMultiplayerInitialize(const MultiplayerInitializationConfiguration* config,
                               PFMultiplayerHandle* handle) noexcept;
STDAPI PFMultiplayerUninitialize(PFMultiplayerHandle handle) noexcept;

typedef struct PFEntity* PFEntityHandle;   // authenticated entity; owns SDK-managed credentials
```
**Obligation:** a scoped runtime object (RAII / `IDisposable` / `AutoCloseable` / context manager /
`Drop`) whose construction initializes and disposal uninitializes. A **PlayFab login** returns a
reference-counted `PFEntityHandle`; on GDK, player login is rooted in the signed-in `XUser` (see the
`XUser` pilot). A plan states the dependency chain explicitly:
`XUser` → PlayFab login → `PFEntityHandle` → PFMP. The wrapper retains/duplicates the entity handle
for the operation lifetime and closes it deterministically. It never extracts or accepts the raw
entity-token string.

### 2.2 The drain (the core of the pattern)
```c
enum class PFLobbyStateChangeType : uint32_t {     // PFLobby.h:122
    CreateAndJoinLobbyCompleted = 0, JoinLobbyCompleted = 1, MemberAdded = 2, /* … */
    Updated = 7, /* … */ Disconnected = 10, LeaveLobbyCompleted = /* … */ };
struct PFLobbyStateChange { PFLobbyStateChangeType stateChangeType; };   // PFLobby.h:1306

STDAPI PFMultiplayerStartProcessingLobbyStateChanges(               // PFLobby.h:3291
    PFMultiplayerHandle handle, uint32_t* stateChangeCount,
    const PFLobbyStateChange* const** stateChanges) noexcept;
STDAPI PFMultiplayerFinishProcessingLobbyStateChanges(              // PFLobby.h:3336
    PFMultiplayerHandle handle, uint32_t stateChangeCount,
    const PFLobbyStateChange* const* stateChanges) noexcept;
```
**Obligation:** the drain **is** the public surface — expressed idiomatically. The projection exposes
an **iterator over a closed variant set** (the language's sum type); user code writes the native
`for change … match/switch` loop in its own idiom. `Start` / `Finish` / the raw `stateChangeType` are
hidden behind the iterator, but **every change is a public variant** — completions and notifications
alike. The iterator holds the batch **live for the loop body** and calls `Finish` on **scope exit**
(RAII / `Dispose` / context manager / `defer`), and it **must** run even if a handler throws; each
change is passed back exactly once. Variants are therefore **borrowed views valid only within the
iteration** — the caller copies any field it keeps past the loop.

### 2.3 Create & join — async correlated by `asyncContext`
```c
STDAPI PFMultiplayerCreateAndJoinLobbyWithEntityHandle(              // PFLobby.h:3428
    PFMultiplayerHandle handle, PFEntityHandle creator,
    const PFLobbyCreateConfiguration* createConfiguration,
    const PFLobbyJoinConfiguration* joinConfiguration,
    _In_opt_ void* asyncContext, _Outptr_opt_ PFLobbyHandle* lobby) noexcept;

struct PFLobbyCreateAndJoinLobbyCompletedStateChange : PFLobbyStateChange {   // PFLobby.h:1321
    HRESULT result; void* asyncContext; PFLobbyHandle lobby; };
```
**Obligation:** project the start call to **return a typed operation id** (the projected
`asyncContext`) together with the **not-yet-ready `Lobby`** (its native handle is retained internally).
There is **no** async primitive and **no `await`**. The wrapper allocates the opaque id, passes it as
`asyncContext`, and later — when the loop drains a `PFLobbyCreateAndJoinLobbyCompletedStateChange` — the
caller matches that id on the completion **variant**, checks `result`, and treats the `Lobby` as ready
(success) or reads the failure (the plan's error idiom). Because of the identity map (§2.4), the
`change.lobby` on the completion **is** the same wrapper the start call returned. There is no pump
thread to block and **no cancellation** (no `XAsyncBlock` / `XAsyncCancel`); a plan documents that
difference rather than inventing cancellation.

### 2.4 Unsolicited notifications
```c
struct PFLobbyMemberAddedStateChange : PFLobbyStateChange {          // PFLobby.h:1417
    PFLobbyHandle lobby; PFEntityKey member; };
struct PFLobbyUpdatedStateChange : PFLobbyStateChange {             // PFLobby.h:1551
    PFLobbyHandle lobby; /* + which-changed flags, member/property update summaries */ };
```
**Obligation:** these are **notification variants** in the same loop, not a separate event surface.
Each carries the changed **object** — `change.lobby`, routed via a **handle→wrapper identity map** so
it is **reference-equal** to the wrapper the caller created. They carry no `asyncContext` (no operation
id). The `PFEntityKey` and any update summaries are **borrowed views valid only within the iteration**
(§2.2); the caller copies anything it retains.

### 2.5 Read model (copy-out getters)
```c
STDAPI PFLobbyGetMembers(PFLobbyHandle lobby, uint32_t* memberCount, const PFEntityKey** members) noexcept;
STDAPI PFLobbyGetProperties(PFLobbyHandle lobby, uint32_t* keyCount, const char* const** keys) noexcept;
```
**Obligation:** these return **library-owned** arrays whose lifetime is tied to the drain window and
which `Start` can reallocate. Unlike the loop variants (borrowed views read inside the loop), these
getters are called at **arbitrary times**, so project them as methods that **snapshot** into owned
collections (`IReadOnlyList` / `Vec` / slice-copy / `list` / array); never hand back a view over native
memory that could outlive the next `Start`.

### 2.6 Leave & teardown
```c
STDAPI PFLobbyLeaveWithEntityHandle(PFLobbyHandle lobby, _In_opt_ PFEntityHandle localUser,
                                    _In_opt_ void* asyncContext) noexcept;      // PFLobby.h:2600
struct PFLobbyLeaveLobbyCompletedStateChange : PFLobbyStateChange {            // PFLobby.h:1520
    PFLobbyHandle lobby; void* asyncContext; };
```
**Obligation:** `Leave` returns an operation id like §2.3 (no `await`). The projected teardown keeps
**running the loop** until the `PFLobbyLeaveLobbyCompletedStateChange` with that id drains, **before**
disposing the runtime object (which calls `PFMultiplayerUninitialize`) — uninitializing with members
still in a lobby appears to remote clients as a lost connection. Per the object-lifetime invariant
(`state-change.md` §3.4), the `Lobby` stays valid until its **teardown variant** drains — that
`LeaveLobbyCompleted`, or an involuntary `Disconnected` — after which the wrapper **drops it from the
handle→wrapper identity map and fails fast** (throws `ObjectDisposedException` / returns `Err` /
rejects) on any further use, and never touches the native handle after its terminal `Finish`.

---

## 3. Reference end-to-end scenario (what every state-change pilot demonstrates)

The canonical happy path each projection should run against a live PlayFab title on a Gaming Runtime:

1. **Identity.** Sign in an `XUser` (the `XUser` pilot); complete a **PlayFab login** and obtain a
   `PFEntityHandle`. The projection retains that handle and never exposes the underlying token.
2. **Initialize** PFMP with the title id → a scoped runtime handle.
3. **Create & join** a lobby: `CreateAndJoinLobby(creator, createCfg, joinCfg)` returns an **operation
   id** and a **not-ready `Lobby`** (its native handle is retained internally).
4. **Pump** each frame — run the loop (drain → dispatch → `Finish` on scope exit). When it yields a
   `CreateAndJoinLobbyCompleted` variant whose operation id matches and `result == S_OK`, the `Lobby`
   (the same wrapper, via the identity map) is now **ready**.
5. **Read** the member list and properties (snapshotted copies).
6. **Update** a lobby/member property (`PostUpdate`); observe the resulting `Updated` **variant** in
   the loop.
7. **Observe the `MemberAdded` variant** when a second participant joins (a second client / server),
   or document it as the multi-client extension of the slice.
8. **Leave** (`Leave`) returns an operation id; keep running the loop until the `LeaveLobbyCompleted`
   variant with that id drains; then dispose (→ `Uninitialize`) and release the identity.
9. **Error path:** start an operation that fails (e.g. invalid configuration) and confirm its
   `…Completed` **variant** surfaces a failed `HRESULT` in the loop, read through the plan's error
   idiom — demonstrating that completions carry results, unlike `XAsyncBlock`'s `E_ABORT` cancellation.

---

## 4. Live-integration prerequisites

PFMP cannot be meaningfully faked, and the wrapper is too thin to be worth mocking, so **all** tests
are **live** (see [`testing.md`](./testing.md)). Beyond the `XUser` baseline
([`xuser-pilot.md`](./xuser-pilot.md) §4 — installed GDK/Gaming Runtime, packaged app with
`MicrosoftGame.config`, registered title identity, authorized sandbox, signed-in test account) this
slice additionally requires:

- A configured **PlayFab title** (same Title ID as the initialization config) with **Lobby** enabled,
  and the ability to complete a **PlayFab login** from the signed-in `XUser` to get a
  `PFEntityHandle`.
- For `MemberAdded` / cross-client `Updated`: a **second participant** (a second signed-in client or a
  game server). A single client fully covers create → join → self-update → leave.
- Standard hosted CI cannot satisfy these; CI builds/lints only, and the live pilot runs on a
  GDK-capable self-hosted runner with PlayFab connectivity. Each plan repeats this as a risk.

---

*This contract is intentionally identical across languages. Differences live in each plan's §4 mapping
rows and §9.2 sketch — how that language expresses the sum type, the loop/iterator, operation-id
correlation, and `Finish` on scope exit — not in which Lobby surface is covered.*
