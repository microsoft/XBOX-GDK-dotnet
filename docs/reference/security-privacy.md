# Security & Privacy — Language-Agnostic Reference

Cross-cutting rules every projection must honor when wrapping the Xbox Live and PlayFab backend
surface. These are not ABI mechanics (see [`gdk-surface.md`](./gdk-surface.md)) — they define how
trust, credentials, and user data appear in the idiomatic layer.

---

## 1. Trust contexts: title runtime vs. trusted tooling

The GDK PlayFab headers contain the complete service surface: player/client operations,
title-entity operations, secret-key authentication, and `Server`-prefixed operations. The
projection keeps that coverage because trusted tools and services need it.

The API must make the caller's trust context obvious:

- **Title-runtime APIs** run as a signed-in Xbox user or PlayFab player entity.
- **Trusted-tooling APIs** may run as a PlayFab title entity and call privileged/server operations.
  Group these APIs in an explicit tooling/server namespace or package so they are not mistaken for
  title-safe calls.
- **Raw header presence is not permission.** Documentation must identify which entity type and
  credential level each operation requires.

The PlayFab **title id**, Xbox Live **service config id (SCID)**, and sandbox id are identifiers, not
secrets. The PlayFab **title secret**, signing keys, XSTS tokens, and entity tokens are credentials.

> **Projection rule #1 — expose the full surface without obscuring trust.** Privileged PlayFab APIs
> remain available for tooling, but their namespace, documentation, and authentication path must
> make it clear that they are not for an untrusted shipping-title process.

---

## 2. Entity handles are the PlayFab authentication surface

`PFEntityHandle` is a reference-counted handle to an authenticated PlayFab entity. It contains the
credentials needed for service calls and must be duplicated/closed with the native handle rules.
It is **not** the entity-token string itself.

After an authentication bootstrap returns a `PFEntityHandle`, the idiomatic projection uses that
handle whenever a PlayFab operation requires authentication input:

- Whenever an operation accepts authentication identity/credentials, prefer the variant that takes
  `PFEntityHandle`.
- For PFMP Lobby and Matchmaking, project only the `...WithEntityHandle` /
  `...WithEntityHandles` variants.
- For Party, use `PartyCreateLocalUser(..., PFEntityHandle, ...)`.
- Do not project the raw-token / `PFEntityKey` authentication variants or
  `PFMultiplayerSetEntityToken` into the idiomatic API.

This keeps token acquisition and refresh inside PlayFab Core. If an operation accepts a raw
entity-token / `PFEntityKey` authentication input and has no entity-handle variant, defer it.
Authentication methods are the expected exception: player login and the narrow **trusted
title-secret bootstrap** return the entity handle used afterward. Operations authorized by an
already-created object handle (for example, server-lobby update/leave methods) remain in scope even
when they take no entity handle themselves.

The title-secret bootstrap is therefore a special tooling-only boundary: accept the secret from a
runtime secret provider, obtain a title entity handle, then discard the caller-visible secret and
use that handle for all subsequent operations.

---

## 3. Credential handling

- Never compile a title secret, signing key, token, or test credential into source or package
  metadata. Trusted tooling receives secrets at runtime from its normal secret provider.
- Never persist or log title secrets, entity tokens, XSTS tokens/signatures, authorization headers,
  or web-auth results.
- The idiomatic PlayFab API does not expose raw entity-token bytes. Xbox
  `XUserGetTokenAndSignature*` remains available for callers authenticating their own service, but
  its result must be an opaque, non-loggable value.
- Use the scrubbed trace-callback path for logs. Direct native debugger/file trace sinks can bypass
  projection filtering and are developer-only (see [`gdk-surface.md`](./gdk-surface.md) §10).

---

## 4. PII & user data

- Gamertag, **XUID**, gamerpic, real name, PlayFab entity/master-player ids, presence, and social
  graph are **user data**. Fetch the minimum needed, cache the least, and never route it to a third
  party from the projection itself.
- **Data residency** is title-configured on the service side; a projection must not re-route,
  mirror, or durably cache PII in a different location "for performance."
- Prefer the service's own privacy gates (see [`compliance.md`](./compliance.md) §3) over ad-hoc
  filtering: check permission (`xsapi-c/privacy_c.h`) before showing or sharing another user's data,
  enabling comms, or crossing networks.

---

## 5. Sandbox & environment isolation

- Xbox Live data is partitioned by **sandbox**. The sandbox id is configuration, not a secret; select
  it through deployment/test configuration rather than baking one environment into reusable code.
- Test accounts, credentials, and sandbox membership are sensitive — keep them out of the repo,
  logs, and CI artifacts.

---

## 6. Projection duties (checklist)

1. **Full surface, explicit trust** — expose privileged PlayFab operations for tooling under a clear
   tooling/server boundary.
2. **Entity handles for authentication** — whenever a post-login operation takes authentication
   input, its idiomatic form takes `PFEntityHandle`; raw-token variants are unsupported.
3. **Secrets only at bootstrap** — title secrets are runtime-provided to trusted tooling solely to
   obtain a title entity handle.
4. **No credential persistence/logging** — scrub callback-routed traces; direct sinks are opt-in and
   potentially unredacted.
5. **Least data** — minimize PII fetched and cached; honor data residency.
6. **Privacy gates first-class** — expose permission checks and fail closed on errors.

See also: [`compliance.md`](./compliance.md), [`gdk-surface.md`](./gdk-surface.md) §10
(diagnostics), and [`testing.md`](./testing.md) (live-credential handling).
