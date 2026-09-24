# GDK Projection Roadmap: the XBOX & PlayFab Surface

The delivery map for projecting the **entire in-scope GDK surface** (XBOX core runtime, XBOX Live /
XSAPI, PlayFab, Party, GameChat2) into each language. It answers **what to build, in what order, and
why** once a language's pilots have proven the approach.

> **Cross-language and language-agnostic.** The product areas, family breakdown, async-shape tags,
> dependencies, and priority tiers below are **shared by every plan**. A language plan does *not*
> restate this roadmap: its §13 "Expansion path" simply points here and notes any language-specific
> sequencing. The two async shapes and the ABI conventions live in
> [`gdk-surface.md`](./gdk-surface.md) and [`state-change.md`](./state-change.md); this document layers
> the **coverage plan** on top of them.

The surface is enumerated at **API-family granularity** (a family = one coherent header set, e.g.
`XStore`, XSAPI Social, PlayFab Economy), grouped by **product area**. Within each area, families are
ordered foundation-first and tagged with a priority tier.

---

## 1. How to read this roadmap

Each product-area table has the columns: **Family · Header(s) · Shape · Purpose / unlocks ·
Depends on · Tier**.

**Async shape**: which of the surface's models the family uses (see the two reference docs):

| Tag | Shape | Mechanism | Reference |
|---|---|---|---|
| **A** | Async (`XAsyncBlock`) | start → completion (poll/callback) → get result (± two-call size) | [`gdk-surface.md`](./gdk-surface.md) §4.4 |
| **S** | Polled change loop | drain a per-frame batch of change/event structs → iterate + dispatch (two reclamation variants, see below) | [`state-change.md`](./state-change.md) |
| **E** | Events | `Register/Unregister…` + token callbacks (often RTA-backed in XSAPI) | [`gdk-surface.md`](./gdk-surface.md) §4.5 |
| **G** | Sync / getters | immediate calls + the two-call buffer pattern; no async | [`gdk-surface.md`](./gdk-surface.md) §4.7 |

A family may carry more than one tag (e.g. **A · E** = mostly async calls plus a change event).

Shape **S** is *one* loop with two **reclamation variants**: how the borrowed per-frame batch is
released differs, but the iterate-and-dispatch treatment is identical:
- **explicit-`Finish`**: `StartProcessing…StateChanges` → iterate → `FinishProcessing…StateChanges`;
  the batch is borrowed only *within* the drain (**PFMP**, **Party**, **GameChat2**).
- **next-call**: the XSAPI `*_manager` `…DoWork()` pump hands nothing back and keeps the batch valid
  until the *next* `DoWork` (**`social_manager`**, **`achievements_manager`**).

Only the borrow window differs (until `Finish` vs. until the next poll); either way the caller copies
out anything it needs to retain past that window.

**Priority tier**: relative build order *within an area* (not a hard schedule):

| Tier | Meaning |
|---|---|
| **P0** | Pilot: already planned; proves a shape end-to-end. |
| **P1** | Foundational or highest-value; unblocks the most downstream families. |
| **P2** | Broadly useful; builds on P1. |
| **P3** | Niche / long-tail / legacy-adjacent. |

**★** marks a family with a dedicated worked pilot ([`xuser-pilot.md`](./xuser-pilot.md),
[`multiplayer-pilot.md`](./multiplayer-pilot.md)).

**Out of scope** (never mapped): XCurl, XAL, GameInput, the GXDK console tree, the legacy XSAPI
multiplayer stack (**Multiplayer Manager**, **MPSD**, **SmartMatch**), and **every API marked
deprecated in the pinned headers**; see §9 and [`gdk-surface.md`](./gdk-surface.md) §7 / §7.1.

**PlayFab authentication rule:** the full player/title/server service surface is in scope because
trusted tooling needs it. Whenever an operation accepts authentication input, its idiomatic form
uses `PFEntityHandle`: prefer `...WithEntityHandle` / `...WithEntityHandles` PFMP variants and the
entity-handle forms used by PlayFab Services and Party. Raw entity-token / `PFEntityKey`
authentication variants are not projected. Authentication bootstrap obtains the handle; operations
authorized by an already-created object handle remain in scope without adding an entity parameter.

---

## 2. Sequencing at a glance

Although the roadmap is organized by product area, the natural cross-area order is:

1. **Prove the shapes**: the two pilots: **XUser** (shape **A**) and **PFMP Lobby** (shape **S**).
2. **Finish the XBOX core runtime foundation**: XSystem, XStore, XGameSave (§3, P1).
3. **Stand up XSAPI**: XblContext + Profile + Social + Presence + RTA (§4, P1).
4. **Stand up PlayFab**: Core/Entity + Authentication, then Economy (Catalog + Inventory) (§5, P1–P2).
5. **Broaden multiplayer**: Matchmaking, Party, then GameChat2 (§6–§7).
6. **Fill the long tail**: the remaining P2/P3 families across all areas.

The two pilots are the only P0 work; everything else is P1→P3 and can proceed in parallel per area
once its foundation family (runtime init / XblContext / PlayFab Core) lands.

---

## 3. XBOX: core runtime (`xgameruntime.dll`)

The always-present title runtime. Mostly shape **A** and **G**, with a few change events (**E**).
Every other XBOX family depends on runtime init + feature gating.

| Family | Header(s) | Shape | Purpose / unlocks | Depends on | Tier |
|---|---|---|---|---|---|
| Runtime init & feature gating | `XGameRuntime.h`, `XGameRuntimeInit.h`, `XGameRuntimeFeature.h` | G | init/lifecycle; feature detection; precondition for **everything** | (none) | P0 |
| **XUser** ★ | `XUser.h` | A · E | sign-in, identity, user-change / sign-out-deferral events; gates every user-scoped API | runtime | P0 |
| XSystem | `XSystem.h` | G | console/OS/app info, analytics id, app-state, app-changed event | runtime | P1 |
| XStore | `XStore.h` | A · E | commerce: licenses, DLC, IAP, catalog queries; license-lost event | XUser | P1 |
| XGameSave | `XGameSave.h`, `XGameSaveFiles.h` | A | cloud-synced save containers & blobs | XUser | P1 |
| XPackage | `XPackage.h` | A · E | package/chunk enumeration, mount, install monitor | runtime | P2 |
| XGameUI | `XGameUI.h` | A | system UI: account picker, error/text/gamertag dialogs, web auth | XUser | P2 |
| Game activation & invites | `XGameActivation.h`, `XGameEvent.h`, `XGame.h` | E · A | title activation, protocol/URI launch, invite handling, game events. **`XGameInvite.h` and `XGameProtocol.h` are deprecated** in favour of `XGameActivation*`: bind the latter only | XUser | P2 |
| XNetworking | `XNetworking.h` | A · G | connectivity & security config, connectivity hint | runtime | P2 |
| XGameStreaming | `XGameStreaming.h` | A · E | xCloud client detection & properties | runtime | P3 |
| XPersistentLocalStorage | `XPersistentLocalStorage.h` | A · G | persistent local storage folder | runtime | P3 |
| XSpeechSynthesizer | `XSpeechSynthesizer.h` | A | text-to-speech synthesis | runtime | P3 |
| XAccessibility | `XAccessibility.h` | A · G | accessibility preferences (e.g. captions) | runtime | P3 |
| XAppCapture | `XAppCapture.h` | A | screenshots / game-clip capture | runtime | P3 |
| XDisplay | `XDisplay.h` | G | display mode / HDR info | runtime | P3 |
| XLauncher | `XLauncher.h` | A | launch other apps / URIs | runtime | P3 |

The async engine (`XTaskQueue`, `XAsync*`), error hook (`XError`), and threading (`XThread`) are
runtime headers too, but they are **cross-cutting infrastructure**, not feature families: see
§8.

---

## 4. XBOX Live: XSAPI (`xsapi-c`)

XBOX Live social & game services. Built on **libHttpClient** and the **RTA** websocket channel.
Raw services are shape **A**; live subscriptions add **E**; the `*_manager` convenience layers are the
same polled loop as shape **S**, drained via a `DoWork` pump (the returned batch stays valid until the
next `DoWork` rather than being handed back to an explicit `Finish`). All depend on an `XblContext`
created from a signed-in `XUser`.

> **Before binding any of this, read
> [`gdk-surface.md`](./gdk-surface.md) → "Which module to bind: XSAPI".** XSAPI is a *separate*
> library from the Gaming Runtime with its own redistributable. Bind
> **`Microsoft.Xbox.Services.C.Thunks.dll`** (425 exports) and ship **`libHttpClient.dll`**
> alongside it; the `Microsoft.Xbox.Services.{142,143}.C.lib` static libs are for C++ titles and
> cannot be bound by a projection. `XAsync*`/`XTaskQueue*` are **not** in that DLL: they still come
> from `xgameruntime.thunks.dll`, so XSAPI work requires both modules. **Every `Xbl*` function that
> survives the GDK preprocessor is exported except the internal `XblSetApiType`**: names that look
> absent are gated out by `HC_PLATFORM` (non-GDK surface) or `XSAPI_INTERNAL_EVENTS_SERVICE`
> (internal-only). Still verify every entry point against `dumpbin /exports` for your GDK edition
> before declaring it, comparing against *preprocessed* headers rather than a raw grep.
>
> Teardown is `XblCleanupAsync`: asynchronous, and there is no synchronous form. Init needs the
> title's **SCID**.

| Family | Header(s) | Shape | Purpose / unlocks | Depends on | Tier |
|---|---|---|---|---|---|
| XblContext & init | `xbox_live_global_c.h`, `xbox_live_context_c.h`, `xbox_live_context_settings_c.h` | G | `XblInitialize(XblInitArgs{scid, queue})` → `XblCleanupAsync`; build `XblContext` from `XUser`; retry/telemetry settings; **foundation for all XSAPI** | XUser | P1 |
| Real-Time Activity (RTA) | `real_time_activity_c.h` | E | the websocket channel underpinning presence / stat / social subscriptions | XblContext | P1 |
| Profile | `profile_c.h` | A | gamertag, gamerpic, gamerscore | XblContext | P1 |
| Social | `social_c.h` (· `social_manager_c.h`) | A · S | social graph, people, relationships (manager = `DoWork` poll loop, next-call reclamation) | XblContext | P1 |
| Presence | `presence_c.h` | A · E | rich presence set/query; RTA-backed subscriptions | XblContext, RTA | P1 |
| Achievements | `achievements_c.h` (· `achievements_manager_c.h`) | A · S | achievement read / unlock / progress (manager = `DoWork` poll loop) | XblContext | P1 |
| Leaderboards | `leaderboard_c.h` | A | leaderboard queries | XblContext | P2 |
| User statistics | `user_statistics_c.h` | A · E | stat read/write + subscriptions | XblContext, RTA | P2 |
| Title-managed statistics | `title_managed_statistics_c.h` | A | service-managed title stats | XblContext | P2 |
| Title storage | `title_storage_c.h` | A | title / user / global blob storage | XblContext | P2 |
| Privacy | `privacy_c.h` | A | comms / view permission checks | XblContext | P2 |
| Multiplayer activity | `multiplayer_activity_c.h` | A | activity, recent players, invites: **the supported way to advertise a session on XBOX** | XblContext | P2 |
| Game invite | `game_invite_c.h` | E | invite-received events | XblContext | P2 |
| Notification | `notification_c.h` | E | game notifications (SPOP, achievement, etc.) | XblContext | P3 |
| Events (telemetry) | `events_c.h` | A | XBOX Live telemetry events | XblContext | P3 |
| String verify | `string_verify_c.h` | A | profanity / string moderation | XblContext | P3 |
| HTTP call | `http_call_c.h` | A | generic authenticated XBOX Live REST call (low-level) | XblContext | P3 |

> **Out: the whole legacy multiplayer stack.** XSAPI **Multiplayer Manager**
> (`multiplayer_manager_c.h`), **MPSD** (`multiplayer_c.h`) and **SmartMatch**
> (`matchmaking_c.h`) are **not** projected. Sessions, rosters, and matchmaking are served by
> **PlayFab Lobby / Matchmaking** (§6) plus **Party**; advertising a session and handling invites on
> XBOX goes through **Multiplayer activity** (`multiplayer_activity_c.h`) above. See
> [`gdk-surface.md`](./gdk-surface.md) §7.

> **Not blocked by an export gap: a correction.** Earlier revisions of this roadmap said six
> families could be projected for their query surface but not their change-notification surface,
> because the registration functions were "missing from the thunks DLL". That was wrong: the list
> came from grepping the headers without running the preprocessor. Verified on `260404` by
> preprocessing with `cl /EP /D_GAMING_DESKTOP` and diffing against `dumpbin /exports`, **every
> `Xbl*` function a GDK build can see is exported except the internal `XblSetApiType`**.
>
> **Social** (`XblSocial{Add,Remove}FriendRequestCountChangedHandler`) and **Privacy**
> (block/mute list changed handlers) are exported as of `260404`: they were genuinely absent on
> `260400`, so re-verify per edition. **Achievements** unlock handlers, **Game invite**
> (`XblGameInviteRegisterForEvent*`), **Multiplayer activity** invite handlers and **Notification**
> (`XblNotification{Subscribe,Unsubscribe}*`) are **non-GDK surface**: `#if HC_PLATFORM ==
> HC_PLATFORM_WIN32 || HC_PLATFORM_IS_EXTERNAL` (or the iOS/Android/UWP equivalent) compiles them
> out of a GDK build entirely. On GDK that functionality comes from the **core runtime** instead:
> `XGameInvite*` and `XUser*` in `xgameruntime.thunks.dll`.
>
> Project all six families **in full**. The **RTA** channel remains the more important subscription
> mechanism and is fully exported: connection-state and resync handlers,
> `XblPresenceTrack{Users,AdditionalTitles}`, `XblSocialAddSocialRelationshipChangedHandler` and
> `XblUserStatisticsTrackStatistics` are all present.
>
> Do **not** reach for the older `XblRealTimeActivity{Activate,Deactivate}`, the subscription-error
> handler, or the per-family `Xbl*SubscribeTo*Change` calls to fill any of this in: every one of
> them is marked deprecated in the headers. See [`gdk-surface.md`](./gdk-surface.md) §7.1.

---

## 5. PlayFab: Core & Services (`playfab/core`, `playfab/services`, `playfab/gamesave`)

The PlayFab backend. **Every call is shape A** (`XAsyncBlock`): the same mapping as the XBOX core
runtime. The headers include player/client operations plus privileged title-entity and
`Server`-prefixed operations; all remain in scope for trusted tooling. Group privileged operations
under an explicit tooling/server namespace so they cannot be mistaken for shipping-title calls.

`PFCore` init + authentication produce a `PFEntityHandle`, the only PlayFab authentication type used
by the idiomatic API. Player login yields a player entity handle; trusted tooling may use the narrow
title-secret bootstrap to obtain a title entity handle. Every subsequent service call uses that
handle, keeping token management inside PlayFab Core.

**Foundation (P1):**

| Family | Header(s) | Shape | Purpose / unlocks | Depends on | Tier |
|---|---|---|---|---|---|
| PlayFab Core / Entity | `core/PFCore.h`, `PFServices.h`, `PFServiceConfig.h`, `PFEntity.h`, `PFEntityKey.h` | A | init, service config, reference-counted entity handle; **foundation for all PlayFab** | (none) | P1 |
| Authentication / LocalUser | `core/PFAuthentication.h` (`+_Xbox`), `PFLocalUser.h` (`+_Xbox`, `+_Steam`) | A | player login or trusted title-secret bootstrap; yields the entity handle that gates every service | PF Core | P1 |

**Services (all A, all depend on Authentication):**

| Family | Header(s) | Purpose / unlocks | Tier |
|---|---|---|---|
| Catalog | `services/PFCatalog.h` | Economy v2 catalog: items, search | P2 |
| Inventory | `services/PFInventory.h` | Economy v2 inventory, currencies, purchase (→ Catalog) | P2 |
| CloudScript | `services/PFCloudScript.h` | execute cloud functions / `ExecuteFunction` | P2 |
| Title data | `services/PFTitleDataManagement.h` | title data & news | P2 |
| Account management | `services/PFAccountManagement.h` | account link / unlink, profile | P2 |
| Profiles | `services/PFProfiles.h` | entity profiles | P2 |
| Data | `services/PFData.h` | entity objects & files | P2 |
| Leaderboards | `services/PFLeaderboards.h` | PlayFab leaderboards | P2 |
| Statistics | `services/PFStatistics.h` | PlayFab statistics | P2 |
| PlayFab GameSave | `gamesave/PFGameSaveFiles.h` (`+Ui`), `PFXGameSave.h` | PlayFab-backed cloud save (counterpart to XBOX `XGameSave`, §3) | P2 |
| Player data | `services/PFPlayerDataManagement.h` | player / user key-value data (legacy) | P3 |
| Groups | `services/PFGroups.h` | entity groups & roles | P3 |
| Friends | `services/PFFriends.h` | PlayFab friends list | P3 |
| Segments | `services/PFSegments.h` | player segment membership | P3 |
| Experimentation | `services/PFExperimentation.h` | treatment variants | P3 |
| Localization | `services/PFLocalization.h` | localized content | P3 |
| Push notifications | `services/PFPushNotifications.h` | device push registration | P3 |
| Events / pipeline | `core/PFEvents.h`, `PFEventPipeline.h` | PlayFab telemetry pipeline | P3 |
| Multiplayer Server | `services/PFMultiplayerServer.h` | request / list hosted server allocations (shape **A**, not state-change: cf. §6) | P3 |

---

## 6. PlayFab: Multiplayer & Party (`playfab/multiplayer`, `playfab/party`)

The **state-change** heart of the surface (shape **S**); see [`state-change.md`](./state-change.md)
and the worked [`multiplayer-pilot.md`](./multiplayer-pilot.md). Each library owns its own
`StartProcessing…StateChanges` / `FinishProcessing…StateChanges` drain, pumped per frame.

For entry points that authenticate a user or server, project only PFMP's
`...WithEntityHandle` / `...WithEntityHandles` variants and Party's
`PartyCreateLocalUser(..., PFEntityHandle, ...)`. Do not expose PFMP's raw token/key variants or
`PFMultiplayerSetEntityToken`. Tooling generation defines `PFMULTIPLAYER_INCLUDE_SERVER_APIS` so the
server Lobby/Matchmaking operations are present; creation/claim/join entry points use their
entity-handle variants, while subsequent methods authorized by the resulting lobby/ticket handle
remain projected as declared.

| Family | Header(s) | Shape | Purpose / unlocks | Depends on | Tier |
|---|---|---|---|---|---|
| PFMultiplayer (init + pump) | `multiplayer/PFMultiplayer.h` | S | shared init + `ProcessStateChanges` pump for Lobby & Matchmaking | PF Core (or standalone) | P0 |
| **PFLobby** ★ | `multiplayer/PFLobby.h` | S | lobby create / join / update, members, search | PFMultiplayer | P0 |
| PFMatchmaking | `multiplayer/PFMatchmaking.h` | S | matchmaking tickets | PFMultiplayer | P1 |
| Party | `party/Party_c.h` (`Party.h` C++) | S | realtime networking + voice/text transport | (none) | P1 |
| PartyXboxLive | `party/PartyXboxLive_c.h` (`PartyXboxLive.h`) | S | XBOX Live identity/auth for Party chat controls | Party, XUser | P2 |

> **Note:** PlayFab **Multiplayer Server** (server allocation) is shape **A** and lives with the
> PlayFab services in §5,
> not here. Despite the "multiplayer" name it is not part of the state-change surface.

---

## 7. Realtime voice & text: GameChat2

Accessible voice/text chat. Also shape **S**: it drains **two** state-change channels
(`ChatManagerStartProcessingStateChanges` for chat events and
`ChatManagerStartProcessingStreamStateChanges` for audio/text streams), so it reuses the
[`state-change.md`](./state-change.md) machinery. Typically layered over a transport (Party or the
title's own networking).

| Family | Header(s) | Shape | Purpose / unlocks | Depends on | Tier |
|---|---|---|---|---|---|
| GameChat2 | `GameChat2_c.h` (`GameChat2.h` C++) | S | per-user voice/text chat, mute/relationship, transcription/TTS; two drain channels | a transport (Party / own) | P2 |

---

## 8. Cross-cutting foundation

Not a product area: the infrastructure every area sits on. It is fully specified in
[`gdk-surface.md`](./gdk-surface.md) §4 and **proven by the XUser pilot**, so it carries **no separate
milestone**; it simply must exist before area work begins.

- **Async engine**: `XTaskQueue.h`, `XAsync.h`, `XAsyncProvider.h`: the `XAsyncBlock` / task-queue
  model and the `Manual`-port per-frame **pump** that keeps continuations on the game loop's thread.
- **Error**: `XError.h`, `XGameErr.h`: the `HRESULT` facility and error hook.
- **Threading**: `XThread.h`: thread affinity / naming.
- **libHttpClient**: `httpClient/*.h`: the origin of the async primitives, plus a direct HTTP /
  WebSocket surface. That direct surface is **low-priority / mostly internal** (titles reach services
  through the layers above), so it is not a scheduled family.

---

## 9. Out of scope

Excluded from all projections (rationale in [`gdk-surface.md`](./gdk-surface.md) §7):

| Excluded | Header(s) | Why |
|---|---|---|
| XCurl | `XCurl.h` | raw libcurl-compatible HTTP; wrapping curl is not a goal. |
| XAL | `Xal/*.h` | auth is reached through `XUser`, already covered. |
| GameInput | `GameInput.h` | COM-style (`AddRef`/`Release`/`QueryInterface`), not the flat-C model; project separately if needed. |
| XSAPI Multiplayer Manager (MPM) | `xsapi-c/multiplayer_manager_c.h` | legacy convenience layer over MPSD; use PlayFab Lobby/Matchmaking (§6). |
| XSAPI Multiplayer / MPSD | `xsapi-c/multiplayer_c.h` | XBOX-only legacy session model superseded by PlayFab Lobby + Party; also the most heavily deprecated header in XSAPI on `260404` (17 deprecated entry points). Advertise sessions via Multiplayer activity (§4). |
| XSAPI Matchmaking / SmartMatch | `xsapi-c/matchmaking_c.h` | XBOX-only matchmaking built on MPSD; use PlayFab Matchmaking (§6). |
| Deprecated APIs (all families) | anywhere in the pinned headers | never projected, in **any** family, including otherwise in-scope ones. Detection markers, the empty-macro trap, and the doc-comment-only cases are in [`gdk-surface.md`](./gdk-surface.md) §7.1. |
| GXDK console tree | (separate licensed tree) | XBOX hardware APIs, not in the PC `windows\include` layout. |

> **Deprecation is per-declaration, not per-family.** In-scope families still contain deprecated
> members that must be filtered: e.g. `XUser.h` (`XUserGetMsaTokenSilently*`),
> `real_time_activity_c.h`, `presence_c.h`, `social_c.h`, `user_statistics_c.h`, `XGameStreaming.h`,
> `XPackage.h`. `XGameInvite.h` and `XGameProtocol.h`'s activation entry points are deprecated in
> favour of `XGameActivation*`; bind the latter.

---

## 10. Relationship to the plans

- The **two shapes** are proven once per language by the pilots: **XUser** (shape **A**,
  [`xuser-pilot.md`](./xuser-pilot.md)) and **PFMP Lobby** (shape **S**,
  [`multiplayer-pilot.md`](./multiplayer-pilot.md)). Proving them validates the binding, error,
  handle, async, event, and state-change mappings for **every** family that shares those shapes.
- After the pilots, coverage is **breadth over the tables above**, in the §2
  order. Because families in an area share one shape and one foundation, adding a family is mostly
  new surface area, not new *mechanism*.
- Each plan's **§13 "Expansion path"** points here rather than re-listing the surface, and records only
  that language's deviations (e.g. a runtime that reaches a family sooner, or a family a given host
  cannot exercise). The shared order, shapes, dependencies, and tiers stay in this document.
