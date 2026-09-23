# Glossary — Terms & Acronyms

A key to the acronyms and jargon used across these plans and references. Terse by design; follow the
cross-links for depth.

---

## Platform & runtime

| Term | Meaning |
| --- | --- |
| **GDK** | Microsoft **Game Development Kit** — the unified SDK these plans project. |
| **GRDK** | **Gaming Runtime Dev Kit** — the PC portion of the GDK (what we target). |
| **GXDK** | The Xbox-console portion of the GDK. **Out of scope** (see `roadmap.md` §9). |
| **Gaming Runtime** | The `X*` runtime layer (`XGameRuntimeInitialize`, task queue, users, storage). |
| **Task queue** | `XTaskQueue` — the async dispatch/callback pump underpinning every `XAsyncBlock`. |
| **`XAsyncBlock`** | The async operation control block (start → complete → get-result). Shape **A**. |
| **Two-call buffer** | Size-then-fill idiom: call once for the byte count, again to fill. See `gdk-surface.md` §4.7. |
| **HRESULT** | 32-bit Win32 result code returned/reported by nearly every API. See `gdk-surface.md` §4.8. |
| **MicrosoftGame.config** | The title manifest (identity, executable, capabilities, and packaging configuration) read by the runtime. |
| **Sandbox** | Isolated Xbox Live data/environment partition (e.g. `XDKS.1`) used for dev/test. |
| **XR** | **Xbox Requirements** — the certification bar a title must pass. See `compliance.md`. |
| **TCUI** | **Title-Callable UI** — system-provided UI (account picker, invites, profile cards). |

## Identity & commerce

| Term | Meaning |
| --- | --- |
| **XUser** | A signed-in user handle (`XUserHandle`); root of identity. Pilot slice — see `xuser-pilot.md`. |
| **XUID** | **Xbox User ID** — stable numeric user identifier. |
| **Gamertag** | A user's Xbox display name (has modern unique/suffix forms). |
| **MSA** | **Microsoft Account** — the underlying consumer identity. |
| **XToken / XSTS** | The security token (and signature) minted for a user to call Xbox Live services. |
| **SCID** | **Service Config Id** — identifies a title's Xbox Live service configuration (not a secret). |
| **XStore** | Commerce/licensing surface (entitlements, purchases, license-lost events). |

## Xbox Live services (XSAPI)

| Term | Meaning |
| --- | --- |
| **XSAPI** | **Xbox Services API** (`xsapi-c/*`) — profiles, social, achievements, presence, multiplayer activity, etc. |
| **XblContext** | Per-user XSAPI context (`XblContextHandle`) created from an `XUser`. |
| **RTA** | **Real-Time Activity** — the XSAPI WebSocket push channel backing live events/subscriptions (shape **E**). |
| **MPSD** | **Multiplayer Session Directory** — the Xbox Live session service (`multiplayer_c.h`). **Out of scope** (superseded by PlayFab Lobby + Party). |
| **SmartMatch** | Xbox Live matchmaking built on MPSD (`matchmaking_c.h`). **Out of scope** (superseded by PlayFab Matchmaking). |
| **Presence** | A user's online/rich-presence state. |
| **`*_manager` (XSAPI)** | Stateful managers (`social_manager`, `achievements_manager`) drained by a `DoWork()` pump — shape **S**, next-call reclamation. |
| **Multiplayer Manager (MPM)** | Legacy XSAPI helper over MPSD. **Out of scope** (superseded by PlayFab Lobby/Matchmaking). |

## PlayFab

| Term | Meaning |
| --- | --- |
| **PlayFab / PF** | The LiveOps/back-end service; the GDK C headers (`playfab/*`) include player, title-entity, tooling, and server operations. |
| **Entity** | PlayFab's addressable actor (title, player, character…); target of most calls. |
| **Entity handle** | Reference-counted `PFEntityHandle` for an authenticated entity; contains SDK-managed credentials and is the projection's PlayFab authentication type. |
| **Entity token** | The short-lived credential contained by an entity handle. The idiomatic projection does not expose it directly. |
| **Title id** | Public PlayFab title identifier (not a secret). |
| **Title secret** | Privileged PlayFab credential used only by trusted tooling/service bootstrap to obtain a title entity handle; never embedded or logged. |
| **PFServices** | The PlayFab service APIs (Catalog, Inventory, CloudScript, Leaderboards, Statistics…). Shape **A**. |
| **PFMP** | **PlayFab Multiplayer** (`playfab/multiplayer/*`) — Lobby + Matchmaking. Shape **S**. |
| **Lobby** | PFMP session-membership service. Worked pilot — see `multiplayer-pilot.md`. |
| **Matchmaking** | PFMP ticket-based matchmaking service. |
| **MultiplayerServer** | PFMP dedicated-server allocation (`PFMultiplayerServer`). Shape **A**. |

## Networking & voice

| Term | Meaning |
| --- | --- |
| **Party** | PlayFab **Party** real-time networking + chat (`playfab/party/Party_c.h`). Shape **S**. |
| **PartyXboxLive** | Xbox Live identity/entity integration layer for Party. |
| **GameChat2** | Voice/text chat library (`GameChat2*`). Shape **S**, two channels. |
| **libHttpClient / HC** | The shared HTTP/WebSocket transport (`httpClient/*`) under both XSAPI and PlayFab; owns transport tracing (`HCTrace*`). See `gdk-surface.md` §10. |

## Async-shape tags (see `roadmap.md` §1)

| Tag | Shape |
| --- | --- |
| **A** | **Async** — `XAsyncBlock`: start → completion (poll/callback) → get result. |
| **S** | **State-change loop** — drain → switch → reclaim; two variants: explicit-`Finish` vs. next-call (`DoWork`). See `state-change.md`. |
| **E** | **Events** — `Register`/`Unregister` + token (RTA-backed in XSAPI). |
| **G** | **Getters / sync** — immediate return, often via two-call buffer. |

## Projection tooling

| Term | Meaning |
| --- | --- |
| **FFI** | Foreign Function Interface — calling the C ABI from another language. |
| **P/Invoke** | .NET's FFI mechanism; **Panama / FFM** is Java's (Foreign Function & Memory API). |
| **cgo** | Go's C interop; **N-API** is Node.js's native-addon ABI. |
| **bindgen** | Rust C-header → binding generator; **ClangSharp** is the .NET equivalent. |
| **ANE** | **Adobe Native Extension** — ActionScript 3's only native-interop path. |
| **MSRV** | **Minimum Supported Rust Version**; **LTS** = Long-Term-Support runtime baseline. |
| **abi3** | CPython's stable ABI (version-independent extension modules). |
