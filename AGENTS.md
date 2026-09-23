# AGENTS.md — `gdk-dotnet`

## What this repository is

The **dotnet** implementation of the Microsoft GDK idiomatic projection. It is one of eight
sibling language repositories split out of the `gdk-projections-plans` meta repo.

## The spec lives in `docs/`

- **`docs/plan.md`** is the authoritative specification for this repository. Implement against it.
  It follows a shared 15-section template; §4 (the mapping table) is the heart, §9 is the acceptance
  slice (the `XUser` pilot, plus the PFMP Lobby state-change slice).
- **`docs/reference/*.md`** are the nine shared, language-agnostic reference docs
  (`gdk-surface.md`, `xuser-pilot.md`, `state-change.md`, `multiplayer-pilot.md`,
  `roadmap.md`, `security-privacy.md`, `compliance.md`, `glossary.md`, `testing.md`).

## Vendored docs are read-only copies

`docs/` holds **two kinds** of file. Know which you are editing before you touch anything.

**Vendored — do not edit here.** `docs/plan.md` and `docs/reference/*.md` are manually vendored
copies. The canonical sources are `plans/dotnet.md` and `reference/*.md` in the
`gdk-projections-plans` meta repo. Fix the meta repo, then re-copy. The only local delta is that
`docs/plan.md` has its `../reference/` links rewritten to `./reference/`.

**Authored here — this repo is canonical.** These have no meta-repo counterpart:

| Path | Notes |
|---|---|
| `docs/building.md` | Build, test, run, package. |
| `docs/getting-started.md` | Idioms and a first sign-in. |
| `docs/architecture.md` | Layering, target frameworks, the AOT contract. |
| `docs/gdk-edition.md` | The re-pin procedure and minimum version. |
| `docs/api/` | **Generated.** Do not hand-edit; edit the XML doc comments and run `eng/generate-docs.ps1`. |

The re-vendor step is a blind copy of the meta repo's `reference/` and `plans/dotnet.md` onto
`docs/reference/` and `docs/plan.md`. Keep it scoped to exactly those paths — a recursive copy over
all of `docs/` would destroy the authored guides and the generated reference.

## Public API changes must stay documented

`src/GDK.Net/GDK.Net.csproj` sets `GenerateDocumentationFile`, and `TreatWarningsAsErrors` is on
repo-wide, so a new public type or member without an XML doc comment **fails the build** (CS1591).
This is deliberate: it is what keeps `docs/api/` complete.

- Hand-written code: write the doc comment.
- Generated PlayFab/Party code: fix the emitter under `eng/playfab/`, never the output. The enum
  member summaries come from `emit_native.emit_enum(..., document: true)`.
- After any public API change, re-run `pwsh eng/generate-docs.ps1` and commit `docs/api/`.
  `ApiReferenceDriftTests` fails when a type has no page or a page outlives its type.


## Ground rules

1. **Idiomatic, not mechanical.** Callers must never see an `HRESULT`, a raw handle, an
   `XAsyncBlock`, a registration token, or a two-call size buffer. See `docs/plan.md` §4.
2. **Scope.** In: the core `X*` runtime + the `_c` services (XSAPI, libHttpClient, GameChat2,
   PlayFab incl. PFMP and Party). Out: XCurl, XAL, GameInput, the GXDK console tree.
3. **Pinned GDK edition `260404`**. Headers come from the edition's `windows\include` tree and
   import libraries from `windows\lib\{x64,arm64}`. **Never** use the `GRDK\GameKit\*` paths —
   they omit the Xbox Live / PlayFab stack and ship no `arm64` libraries. Derive paths from
   `%GameDKCoreLatest%` (the edition root), never `%GRDKLatest%` or `%GXDKLatest%`.
4. **Cancellation** (`E_ABORT`) routes to the language's cancellation idiom, never a generic error.
   The numeric HRESULT is always preserved for diagnostics.
5. **Testing is live.** The pilot is validated in a packaged GDK app against a real sandbox; CI only
   builds and lints. See `docs/reference/testing.md`.
6. **Supported versions are declared in `README.md`.** Any change to the supported runtime,
   architecture, engine, or toolchain matrix must update that table in the same change.
