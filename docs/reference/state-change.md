# The State-Change Pattern — Shared Projection Contract

> **Purpose.** A handful of GDK subsystems do **not** use the `XAsyncBlock` / `XTaskQueue` async model
> described in [`gdk-surface.md`](./gdk-surface.md) §5–§6. Instead they surface **all** results and
> notifications through a single **polled state-change queue** that the title drains from its game
> loop: **PlayFab Multiplayer** (Lobby + Matchmaking) and **PlayFab Party**. This document is the
> language-neutral **contract** for projecting that pattern — the exact native entry points, the
> memory rules that make it dangerous, and the idiomatic obligation each plan must meet.

Read [`gdk-surface.md`](./gdk-surface.md) first for the universal C-ABI conventions and the
`XAsyncBlock` async model. This document describes the **second** async shape in the GDK. Facts are
from edition `260404`:

- **PlayFab Multiplayer (PFMP)** — `windows\include\playfab\multiplayer\` : `PFMultiplayer.h`,
  `PFLobby.h`, `PFMatchmaking.h`. Flat C linkage (`extern "C"`), C++11 syntax.
- **PlayFab Party** — `windows\include\playfab\party\` : `Party.h` (a C++ class API) over
  **`Party_c.h`**, a genuine flat-C surface. The `_c` header is the projection target (same
  arrangement as GameChat2's `GameChat2.h` / `GameChat2_c.h`).

**Authentication selection is fixed:** whenever a PFMP operation accepts authentication input,
project its `...WithEntityHandle` / `...WithEntityHandles` variant; Party uses
`PartyCreateLocalUser(..., PFEntityHandle, ...)`. Operations authorized by an already-created Lobby,
Ticket, or Party object remain projected as declared. The idiomatic layer never accepts a raw
PlayFab entity-token string and does not expose `PFMultiplayerSetEntityToken`. `PFEntityKey` remains
a normal identity value in state-change payloads; it is not used as authentication input.

---

## 1. The pattern: drain → dispatch → release

Every one of these libraries exposes a matched pair of functions that bracket a batch of
**state changes**. The title calls them on a pump (typically once per game frame):

```c
/* 1. DRAIN: the library hands back an array of pointers to polymorphic state-change structs. */
uint32_t count;
const T_StateChange* const* changes;
XxxStartProcessingXxxStateChanges(handle, &count, &changes);

/* 2. DISPATCH: switch on the discriminant, cast the base pointer to the concrete derived struct. */
for (uint32_t i = 0; i < count; ++i) {
    const T_StateChange* change = changes[i];
    switch (change->stateChangeType) {
        case /* …CompletedStateChangeType */:
            handle((const T_SomethingCompletedStateChange*)change);  /* has result + asyncContext */
            break;
        case /* …notification */:
            handle((const T_SomethingNotificationStateChange*)change);
            break;
        /* … */
    }
}

/* 3. RELEASE: return the batch so the library can reclaim it. */
XxxFinishProcessingXxxStateChanges(handle, count, changes);
```

Each state-change struct begins with a **`stateChangeType`** discriminant (a tagged union). The base
struct carries nothing else; the concrete type is reached by casting the base pointer to the derived
struct named by the discriminant. Two *kinds* of change come out of the same drain:

- **Operation completions** — the result of a call the title made earlier (create lobby, send invite,
  start matchmaking). These carry an `HRESULT result` and the **`void* asyncContext`** the title
  passed when it started the operation.
- **Unsolicited notifications** — state the library observed on its own (a member joined, a property
  changed, the connection dropped). These carry no `asyncContext`.

A faithful projection surfaces **both kinds in the same loop**, as one closed variant set — completions
and notifications alike — and lets the caller dispatch them with the language's native closed-type
construct (see §6). It does **not** split them onto separate async/event surfaces.

---

## 2. The three drains

| Drain | Start / Finish entry points | Discriminant enum | Base struct |
|---|---|---|---|
| **Party** | `PartyStartProcessingStateChanges` / `PartyFinishProcessingStateChanges` (`Party_c.h:2232/2240`) | `PARTY_STATE_CHANGE_TYPE` (C enum, `Party_c.h:68`) | `struct PARTY_STATE_CHANGE { uint32_t stateChangeType; }` (`Party_c.h:559`) |
| **Lobby** | `PFMultiplayerStartProcessingLobbyStateChanges` / `…FinishProcessingLobbyStateChanges` (`PFLobby.h:3291/3336`) | `enum class PFLobbyStateChangeType : uint32_t` (`PFLobby.h:122`) | `struct PFLobbyStateChange { PFLobbyStateChangeType stateChangeType; }` (`PFLobby.h:1306`) |
| **Matchmaking** | `PFMultiplayerStartProcessingMatchmakingStateChanges` / `…FinishProcessingMatchmakingStateChanges` (`PFMatchmaking.h:408/453`) | `enum class PFMatchmakingStateChangeType : uint32_t` | `struct PFMatchmakingStateChange { … stateChangeType; }` |

Notes:
- **PFMP has two independent drains** off one `PFMultiplayerHandle` (from `PFMultiplayerInitialize`,
  `PFMultiplayer.h`): Lobby and Matchmaking are pumped separately. **Party has one** drain off its
  `PARTY_HANDLE`.
- **Derived-struct shape differs by SDK.** In `Party_c.h` (pure C) the concrete structs use
  **first-member composition** (each begins with a `PARTY_STATE_CHANGE`). In PFMP the concrete structs
  use **C++ single inheritance** (`struct PFLobbyCreateAndJoinLobbyCompletedStateChange :
  PFLobbyStateChange { HRESULT result; void* asyncContext; PFLobbyHandle lobby; };`,
  `PFLobby.h:1321`). Both are standard-layout, so the base discriminant sits first at offset 0 and a
  binding treats a derived struct as `{ base fields; extra fields }`, ABI-identical either way.
- The drain array element type is `const StateChange* const*` — a library-owned array of pointers to
  const structs. The title never allocates or frees it.

---

## 3. Four invariants the projection must honor

These are the load-bearing rules; getting any one wrong corrupts memory or leaks.

1. **Memory is valid only between `Start` and `Finish` — so hold the batch, don't copy eagerly.** The
   array and every resource reachable from a state change are valid **only** between `Start…` and
   `Finish…`; `Finish` (and the next `Start`) invalidates them. The projection keeps that window open
   for the **duration of the loop** and calls `Finish` on **scope exit** (RAII / `Dispose` /
   generator-close / `defer`), so the loop variants expose **borrowed views straight into library
   memory** — zero-copy, but **valid only within the iteration**. The caller must **copy any field it
   retains** past the loop; a view must never escape the loop scope (Rust enforces this at compile time
   via lifetimes; other languages document it and fail-fast where they can detect it). Long-lived
   objects (`PFLobbyHandle`, `PARTY_*_HANDLE`) do **not** live in the batch — they resolve through the
   handle→wrapper identity map (§3.4) and outlive it.
2. **`asyncContext` is the completion-correlation token — surfaced as a typed operation id.** Async
   operations here do not take an `XAsyncBlock`; they take a `void* asyncContext` (e.g.
   `PFMultiplayerCreateAndJoinLobbyWithEntityHandle(…, void* asyncContext, PFLobbyHandle* lobby)`,
   `PFLobby.h:3428`), and the matching `…CompletedStateChange` echoes that pointer back. The
   projection allocates an opaque id at call time, passes it as `asyncContext`, and returns it to the
   caller as a **typed operation id** from the start call. The `…Completed` **variant carries that id
   back through the loop**, and the caller correlates by comparing it — there is **no `await`** and no
   hidden async primitive for these ops. The raw pointer is never exposed. The operation's **handle
   is returned synchronously** but is **not "ready"** until its completion variant drains with
   `S_OK`. These ops have **no cancellation** (there is no `XAsyncCancel`).
3. **Pump frequently, and mind the thread.** `Start…` mutates library state and can invalidate memory
   other calls read (e.g. a member list), so it is **not** safe to call concurrently with other
   library calls on that handle. Drive one pump per subsystem from a single thread, **at least once
   per frame**, and keep the `Start`→`Finish` window short. Each change returned by `Start` must be
   passed to `Finish` **exactly once**; batches may be returned out of order or interleaved.
4. **Object lifetime = valid until its teardown variant drains; then fail-fast.** The native contract
   has a *second*, longer lifetime for objects: a `PFLobbyHandle` / `PARTY_*` object stays valid
   *"until a …DestroyedStateChange has been generated and all state changes referencing it [are]
   returned to Finish"* (`Party.h:7929`, `8071`, `7323`, `7745`; read-model arrays are shorter still —
   *"only valid until the next call to `Start…`"*, `Party.h:6685`, `7202`, which is why getters return
   borrowed views too, per invariant 1). The projection re-expresses this **through the loop**: a
   projected object is valid until its **teardown variant** drains — your own `LobbyLeaveCompleted`, or
   an involuntary `…Disconnected` / self-removal notification — after which the wrapper **invalidates**
   it, drops it from the handle→wrapper identity map, and makes any further use **fail fast** (throw
   `ObjectDisposedException` / return `Err` / reject, per the plan's error idiom). The tick order keeps
   a native handle from being touched after its terminal `Finish`: `Start → run the loop body (reads
   borrowed views; the caller sees the teardown variant while the object is still readable) → Finish →
   invalidate torn-down wrappers + unmap`. Finishing the whole batch before invalidating satisfies the
   *"all state changes referencing it returned"* clause.

---

## 4. Relationship to the `XAsyncBlock` model

The plans already project the `XAsyncBlock` / `XTaskQueue` model (one callback per call, delivered on
a queue the title controls; see `gdk-surface.md` §5–§6, and the pumped `Manual` port that lands
continuations on the game thread). The state-change model is a **different** async shape:

| | `XAsyncBlock` async (core `X*`, XSAPI, `_c` services) | State-change drain (PFMP, Party) |
|---|---|---|
| Delivery | per-call completion callback on an `XTaskQueue` | one polled queue draining **all** ops + notifications |
| Correlation | the `XAsyncBlock*` you allocated | the `void* asyncContext` you passed |
| Notifications | separate `Register/Unregister…Event` callbacks | interleaved in the **same** drain |
| Memory | `XAsyncBlock` alive until `…Result` runs | state changes alive **only** `Start`→`Finish` |
| Cadence | dispatch when signaled (or pump `Manual` per frame) | **must** pump per frame |

They **compose** rather than conflict. A title already pumping a `Manual` `XTaskQueue` once per frame
(`XTaskQueueDispatch`) adds one `Start…`/`Finish…` drain per active state-change subsystem to the same
per-frame tick. The projection should therefore fold both into a single "process everything for this
frame" entry point (see §6.7).

---

## 5. Binding-generation notes

- **Party (`Party_c.h`)** is genuine C — plain C enums and first-member-composition structs — so any
  flat-C binding generator consumes it directly. Prefer it over the C++ `Party.h`; wrapping the C++
  class API cross-language is not a goal (consistent with GameChat2, `gdk-surface.md` §7).
- **PFMP (`PFLobby.h` / `PFMatchmaking.h` / `PFMultiplayer.h`)** declares C linkage but uses C++
  constructs (`enum class`, base-struct inheritance, `noexcept`). A C-only parser will reject it;
  generate with a **C++ front end** (the plans already require C++11 for the core headers,
  `gdk-surface.md` §8) or interpose a thin C facade. The inheritance is standard-layout, so generators
  that flatten a derived struct to `{ base; extras }` (bindgen, ClangSharp, jextract) reproduce the
  correct ABI.
- The discriminant is `uint32_t`-wide in every SDK; the derived struct is reached purely by
  reinterpreting the base pointer — no v-tables, no RTTI.

---

## 6. Projection implications (language-agnostic checklist)

Every plan that covers these subsystems must answer these. The surface stays **faithful to the native
loop** — the caller drains a batch each frame and dispatches it — with a thin idiomatic layer on top:

1. **The loop is the public surface.** Expose the drain as an **idiomatic iterator over a closed
   variant set** (a Rust `enum`, a C# `record` hierarchy, a Kotlin `sealed class`, a TS discriminated
   union, a Python dataclass hierarchy, a Go sealed interface, a Lua tagged table). The caller writes
   the native `for change … switch/match` loop in the language's own idiom. There is **no** demux into
   separate async/event surfaces and **no** hidden poll.
2. **Completions and notifications are both variants.** They arrive interleaved on one drain and stay
   interleaved in the loop — a single, totally-ordered stream, which is the whole reason to keep the
   loop. A completion variant carries the **operation id** (§3.2) plus an `HRESULT`/error; a
   notification variant carries the changed **object**. Neither is turned into an `event` or a `Task`.
3. **Correlate completions by the operation id — no `await`.** The start call returns a typed operation
   id; the caller keeps it and matches it on the corresponding `…Completed` variant in a later drain.
   The handle is returned synchronously but is **not-ready** until then. There is no async primitive
   and **no cancellation** for these ops; a plan documents that rather than inventing one.
4. **`Finish` on scope exit (lazy, zero-copy).** The iterator holds the `Start`→`Finish` window open
   for the loop body and calls `Finish` when the loop scope exits — RAII / `IDisposable` /
   `using`/`Symbol.dispose` / context manager / generator-close / `defer` — and it **must run even if a
   handler throws**. Loop variants are therefore **borrowed views valid only within the iteration**
   (§3.1); the caller copies any field it retains. (ActionScript 3 has no scope-exit hook and no
   pattern match, so it takes the documented exception — an **eager pre-copied `Vector`** with `Finish`
   already called; see its plan.)
5. **Identity, not fresh wrappers.** The object inside a notification variant (`change.lobby`) is
   **reference-equal** to the wrapper the caller created, via a handle→wrapper identity map (§3.4).
6. **Object lifetime + fail-fast (§3.4).** A projected object is valid until its teardown variant
   drains (your `LobbyLeaveCompleted`, or an involuntary disconnect / self-removal), after which the
   wrapper invalidates it — drops it from the identity map — and any further use **fails fast**. Never
   touch a native handle after its terminal `Finish`.
7. **One pump per frame.** The loop **is** the pump: the game loop drains every active subsystem each
   frame, composing with the pumped `Manual` `XTaskQueue` dispatch (§4). A loop-less host (a tool) runs
   the same loop on a wrapper-owned thread and dispatches through its own handlers — there is no
   separate background-async mode because there is no async to drive.

### Idiomatic target by language (orientation; the detail lives in each plan's §4 and §9.2)

The user-facing surface is uniform: **a single loop over a closed variant set — completions and
notifications alike — `Finish` on scope exit, no `await` and no public event.** What differs is the
variant idiom, the dispatch construct, and the scope-exit hook.

| Language | Variant set (public sum type) | Iterate + dispatch | `Finish` on scope exit |
|---|---|---|---|
| **.NET / C#** | `abstract record` hierarchy | `foreach` + `switch` pattern | enumerator `Dispose` (`using`) |
| **Rust** | `enum` | `for` + `match` | `Drop` (lifetime-bound borrows) |
| **JVM (Java/Kotlin)** | Kotlin `sealed class` / Java `sealed interface` | `when` / `switch` patterns (Java 21) | `use {}` / try-with-resources |
| **Go** | sealed interface (unexported method) | `for range` (range-over-func, 1.23+) + type switch | `defer` |
| **Python** | dataclass hierarchy | `for` + `match`/`case` | context manager (`with`) |
| **JavaScript / TypeScript** | discriminated union (`{ type, … }`) | `for…of` + `switch` on `.type` | `using` / `Symbol.dispose` or `try/finally` |
| **Lua** | tagged table (`type` field) | `for` + dispatch table | iterator close (`__close`, 5.4) / explicit |
| **ActionScript** | typed `StateChange` subclasses | `for each` + `switch` on a const + `as` cast | **eager pre-copy** (no scope hook) |

---

The concrete worked slice for this pattern is **PFMP Lobby**, specified in
[`multiplayer-pilot.md`](./multiplayer-pilot.md). As with the `XUser` pilot, the contract is identical
across languages; only the idiomatic mapping differs.
