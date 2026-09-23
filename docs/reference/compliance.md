# Certification & Compliance — Language-Agnostic Reference

Shipping an Xbox/GDK title requires passing **Xbox Requirements (XR)** certification. Many XRs are
**behavioral** — they dictate *how* you must call the APIs, not just which ones. A projection can't
pass cert on a title's behalf, but it **can make the compliant path the default path** and make the
non-compliant path hard to reach. This doc lists the obligations that shape the wrapper design; treat
them as design constraints, not optional polish.

> This is guidance for projection authors, not a substitute for the official XR list. Requirement
> numbers change per edition; the *behaviors* below are stable.

---

## 1. User & identity duties

- **Sign-out must be honored promptly.** Subscribe to user-change (`XUserRegisterForChangeEvent`) and
  react to sign-out/sign-in and to the *signing-out* (deferral) phase: stop using that user's
  handles, flush their state, and release resources quickly so the OS can complete sign-out. **Never
  block** the transition.
- **Attribute every operation to the right user.** Xbox Live and PlayFab calls are per-user/entity;
  on user switch, tear down the old `XblContext`/entity and rebuild. Don't leak one user's handles
  into another's flow.
- **Handle "no default user"** and multi-user gracefully — resolve the acting user explicitly rather
  than assuming a single global one.

## 2. Commerce & licensing duties

- **License loss must revoke access immediately.** Watch the `XStore` license-change signal; when a
  license is lost (trial expiry, family sharing revoked, refund), stop access to the licensed content
  right away.
- **Respect entitlements/trials** — gate content on the live license/entitlement check, not on a
  cached "was owned" flag.

## 3. Privacy & social duties

- **Gate before you expose or communicate.** Use the privacy permission checks (`xsapi-c/privacy_c.h`)
  before enabling voice/text chat, showing another user's data, sharing user-generated content, or
  crossing to another network. **Fail closed** — treat an errored/undecided check as *denied*.
- **Communication & UGC restrictions** (e.g. child accounts) are enforced by these checks; do not
  bypass or cache-around them.
- See [`security-privacy.md`](./security-privacy.md) §3 for the data-handling side of this.

## 4. Display duties (gamertag & gamerpic)

- Display `XUserGamertagComponent::UniqueModern` directly — it already contains the suffix when one
  is required. If the UI styles the parts separately, use `Modern` plus `ModernSuffix`; never append
  `ModernSuffix` to `UniqueModern`.
- Fetch gamerpics through the profile APIs; don't hotlink or durably cache beyond need.

## 5. Multiplayer & session duties

- Keep the advertised session accurate. The session layer a projected title uses is PlayFab
  **Lobby** / **Matchmaking** plus XSAPI **Multiplayer Activity** (`multiplayer_activity_c.h`) — the
  legacy MPSD/SmartMatch stack is out of scope ([`gdk-surface.md`](./gdk-surface.md) §7). The
  projection must let a title publish an **activity/joinable session** for invites and "join game,"
  update it on membership change, and **clean it up on leave** (voluntary `…LeaveCompleted`) or on
  involuntary disconnect. See [`multiplayer-pilot.md`](./multiplayer-pilot.md).
- Invites/joins route through the platform; surface them as first-class events, not polled state a
  title might miss.

## 6. System UI (TCUI) duties

- Prefer **Title-Callable UI** for account picker, invite/party UI, and profile cards where an XR
  requires the system experience. Expose these as ordinary idiomatic calls so a title reaches for the
  compliant UI first rather than rolling its own.

## 7. Projection duties (checklist)

1. **Mandatory events are first-class & unmissable** — sign-out/user-change and license-lost surface
   as prominent, documented hooks a title can't quietly ignore.
2. **Compliant path is the default** — privacy checks, session cleanup, and TCUI are the easy calls;
   the non-compliant shortcut is absent or clearly marked.
3. **Prompt, non-blocking sign-out** — use the sign-out deferral only for bounded user-state cleanup
   and release it promptly so the OS can complete the transition.
4. **Document the obligation** next to the API — each wrapped surface notes the XR behavior it must
   uphold so title authors inherit the rule.
5. **Fail closed on privacy/license** — deny/revoke on error rather than defaulting open.

See also: [`security-privacy.md`](./security-privacy.md) and [`roadmap.md`](./roadmap.md) (which
families carry these duties).
