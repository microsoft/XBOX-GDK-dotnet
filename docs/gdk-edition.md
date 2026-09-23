# The pinned GDK edition

GDK.Net is pinned to **one** Microsoft GDK edition at a time. This page explains what "pinned"
means, what the minimum version is, and the exact procedure for moving to a new edition.

## Minimum and supported versions

The supported runtime, architecture, engine and toolchain matrix is declared in
[`../README.md`](../README.md#supported-versions), which is the single canonical source. Do not
restate it elsewhere — per `AGENTS.md` rule 6, any change to that matrix must update the README
table in the same change.

In summary: the GDK edition is both the minimum and the pinned version. There is no "minimum
supported edition" range — the projection is generated from one edition's headers and validated
against that edition's redistributables. An older GDK will be missing exports the projection binds;
a newer one is untested until it is re-pinned by the procedure below.

## Where the edition actually comes from

Three things pin the edition **functionally**:

| Location | Role |
|---|---|
| `Directory.Build.props` → `<GdkEdition>` | The single build-level source of truth. Emitted into the assembly as an `AssemblyMetadataAttribute` named `GdkEdition`, so a built binary reports the edition it was generated against. |
| `eng/playfab/pfmodel.py` → `banner(tool, family, edition=...)` | The default edition stamped into every generated PlayFab/interop file banner. |
| `%GameDKCoreLatest%` (machine environment) | The **edition root** the headers, import libraries and redistributable DLLs are read from. Set by the GDK installer; not stored in the repo. |

Everything else — roughly 200 files — carries the edition only as banner or prose text
(`// GDK edition 260404`). Those are documentation, but they must still be swept so the repo does
not claim two editions at once.

### Path rules

`AGENTS.md` rule 3 is strict about this, and getting it wrong produces a projection that silently
omits half the surface:

- Headers come from `%GameDKCoreLatest%windows\include`, import libraries from
  `%GameDKCoreLatest%windows\lib\{x64,arm64}`.
- **Never** use the `GRDK\GameKit\*` paths. They omit the Xbox Live and PlayFab stack entirely and
  ship no `arm64` libraries.
- Derive every path from `%GameDKCoreLatest%`, never `%GRDKLatest%` or `%GXDKLatest%`.
- The GDK ships two mutually ABI-incompatible PlayFab stacks. The projection binds the
  `windows\bin\x64` one (the stack `windows\include\playfab` describes), **not**
  `GRDK\ExtensionLibraries`. See the comment block at the top of `eng/generate-playfab.ps1`.

## Re-pinning to a new edition

> Your shell's `%GameDKCoreLatest%` is inherited at process start. After installing a new GDK,
> **open a new shell** — an existing one still points at the old edition and every generator will
> silently reproduce the old surface.

### 1. Install the edition and confirm the root

```powershell
$env:GameDKCoreLatest
Get-ChildItem "$env:GameDKCoreLatest\windows\include" | Select-Object -First 5
```

### 2. Update the contract test *first*

`tests/GDK.Net.Tests/ThunksBindingContractTests.cs` pins which entry points the thunks DLL exports
and, deliberately, which ones remain unreachable — "so a future edition that exports them is
noticed rather than assumed". Run the suite before changing anything else: the failures tell you
what the new edition changed.

### 3. Re-generate

```powershell
pwsh eng/generate-interop.ps1 -Clean     # ClangSharp -> eng/Generated/ (review, not compiled)
pwsh eng/generate-playfab.ps1            # regenerates the committed PlayFab projection
```

`generate-playfab.ps1` runs `dumpbin` over the redist DLLs, re-parses the headers into
`eng/playfab/model.json`, and re-emits the C#. Review `model.json`'s diff: SAL annotation changes
there are normal between editions and often produce no C# change at all.

Use `-SkipExports` to reuse the committed `exports.json` on a machine without the MSVC tools.

### 4. Re-verify the export gap — by preprocessing, not grep

`eng/unexported-apis.md` records the APIs that are declared in the headers but not exported by the
redistributable thunks DLLs. **Regenerate this by actually preprocessing the headers**, not by
text-scanning them.

This matters. A naive `grep` over the XSAPI headers counts declarations that are compiled out on
GDK: 11 are gated by `HC_PLATFORM` (they exist only on non-GDK platforms) and 6 more by
`#ifdef XSAPI_INTERNAL_EVENTS_SERVICE` (internal-only). A text scan reports an 18-API gap where the
real GDK-visible gap is **one** (`XblSetApiType`). Preprocess with the platform defines the GDK
actually compiles under:

```powershell
cl /EP /D_GAMING_DESKTOP /I"$env:GameDKCoreLatest\windows\include" <header>
```

`eng/api-coverage.ps1` diffs each DLL's export table against the entry points bound in
`src/GDK.Net/Interop`, and is the cheapest way to see what moved:

```powershell
pwsh -NoProfile -File eng/api-coverage.ps1
pwsh -NoProfile -File eng/api-coverage.ps1 -Module XboxLive
```

When reading `dumpbin` output directly, note that the export regex must tolerate the ` = alias`
suffix.

### 5. Sweep the edition string

```powershell
git grep -l '<old edition>'
```

Update `Directory.Build.props`, `eng/playfab/pfmodel.py`, then every banner and prose reference.
This is mechanical but large — the 260400 → 260404 move touched 196 files.

### 6. Update the documentation that states the edition

- `README.md` — the supported-versions tables and the status section (canonical).
- `AGENTS.md` rule 3.
- `eng/README.md` — the "Known-good version" block.
- `eng/unexported-apis.md` — both gap tables.
- This page, if the procedure changed.

`docs/plan.md` and `docs/reference/*.md` also mention the edition, but they are **vendored** —
fix them in the `gdk-projections-plans` meta repo and re-copy. See `AGENTS.md`.

### 7. Validate

```powershell
dotnet build -c Release
dotnet test -c Release --no-build
pwsh eng/generate-docs.ps1               # the API reference banner carries the edition
```

Then run the live harness on a dev-unlocked machine — CI cannot do this:

```powershell
pwsh eng/run-local.ps1
```

## Known exception

`eng/repro/pfmp-uninitialize-hang` is deliberately left at edition `260400`. The hang it reproduces
has never been re-tested on a newer edition, so pinning it forward would destroy the reproduction.
Leave it alone during a sweep.

## Related guides

- [Building](building.md) — prerequisites and the build/test/package commands.
- [Architecture](architecture.md) — why the interop layer is checked in rather than generated at build time.
