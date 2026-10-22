# Building GDK.Net

This guide covers building, testing, running and packaging the repository.

## Prerequisites

| To do this | You need |
|---|---|
| Build, generate, test and pack the library | Pinned .NET SDK (`global.json`) and Python (`.python-version`), MSVC v143+ / Windows SDK, access to the configured NuGet sources. **No GDK installation/registration required.** |
| Run anything against Gaming Runtime | Matching runtime/native dependencies, a dev-unlocked Windows machine, and an account signed in to the XBOX app |
| Package a title (`makepkg`) | The GDK, for `%GameDK%bin\makepkg.exe` |
| Re-generate interop or PlayFab bindings | The exact restored Windows SDK package; MSVC / Windows SDK for native export/header tooling; pinned Python for PlayFab |
| Publish with NativeAOT (`-Aot`) | The MSVC toolchain: ILC shells out to `link.exe` |

The projection is **Windows-only**. See [`../README.md`](../README.md) for the authoritative
supported-runtime, architecture and toolchain matrix.

### Restore before generation

The ref's [.github/gdk-version.json](../.github/gdk-version.json) selects one exact
Windows/Core pair and verified SDK archive hashes. `eng/build.ps1` restores those inputs
before generating production PlayFab/Party sources into isolated `artifacts/gdk/` output.
Changing refs must not reuse a model/export/source set from a different pin.

Hand-written core/XSAPI interop remains checked in. ClangSharp output is verification
scratch, not a second set of production declarations. A direct repository `dotnet`
build requires prepared inputs; use the orchestrator first. NuGet consumers instead use
prebuilt managed assemblies and ordinary package restore/build, with no repository
checkout, Python or ClangSharp requirement.

Restoring SDK files is not installing/registering Gaming Runtime. Native staging resolves
the actual restored SDK and selected application RID, not a machine's latest installed GDK.

## Build and test

From the repository root:

```powershell
pwsh eng/build.ps1 -Configuration Release -Test -Pack
pwsh eng/build.ps1 -Configuration Release -Test -Pack -Aot -CheckDocs
```

The second command also performs real NativeAOT compilation and the full non-mutating
API reference comparison. Neither command executes a live harness. The solution is
`GDK.Net.slnx`. `src/GDK.Net/GDK.Net.csproj` multi-targets
`net8.0;net10.0;netstandard2.0`, so a single build compiles three times: see
[`architecture.md`](architecture.md) for what actually differs between them.

### Warnings are errors

`Directory.Build.props` sets `TreatWarningsAsErrors=true` for every project in the repo. This is
deliberate and load-bearing: it is what makes the AOT, trim and single-file analyzers
(`IL2xxx`/`IL3xxx`) block a change that reintroduces reflection or runtime-generated marshalling
stubs, and what keeps the XML documentation complete. Do not suppress it globally.

### Package feeds

`NuGet.config` defaults to the public registry for outside contributors and hosted CI.
Use an explicitly selected local NuGet configuration for an approved internal mirror.
Keep private source URLs and credentials out of committed files, logs and packages.
Build tooling must respect the selected sources rather than silently retrying another registry.
Local access does not establish that a hosted runner or a fork PR can use the same feed.

If restore fails with `NU1100`/`NU1603` naming `Microsoft.NET.ILLink.Tasks`, the feed in use cannot
serve the ILLink package that the AOT analyzers pull in. Build without them:

```powershell
dotnet build -c Release -p:GdkNetEnableAotAnalyzers=false
```

This is a local diagnostic option only, not successful CI or release validation. It disables the
*analyzers*, not their underlying requirements. Required gates keep them enabled and also perform
real NativeAOT compilation.

## Running against the Gaming Runtime

A GDK title is normally packaged, registered and launched by AUMID. For day-to-day work that is
unnecessary, on PC, a `MicrosoftGame.config` beside the executable supplies the same identity, so
the app can simply be run:

```powershell
pwsh eng/run-local.ps1                          # the live harness
pwsh eng/run-local.ps1 -Project Sample          # the readable XUser demo
pwsh eng/run-local.ps1 -Project PlayFabSample   # the readable PlayFab demo
pwsh eng/run-local.ps1 -Project MultiplayerHarness
pwsh eng/run-local.ps1 -Aot                     # the NativeAOT build a console title ships
pwsh eng/run-local.ps1 -NoRun                   # publish only, to copy to a test machine
```

`run-local.ps1` produces a self-contained directory that runs on another dev-unlocked machine with
no SDK, no runtime install and no registration.

What it does *not* exercise is the packaged execution environment itself: installation into
`WindowsApps`, the package's own identity, and launch by the shell. That is what the packaged path
is for.

## Packaging

```powershell
pwsh eng/package.ps1                    # layout + genmap + validate
pwsh eng/package.ps1 -Pack              # also produce the .msixvc (slow)
pwsh eng/package.ps1 -Aot -Clean        # the configuration a console title ships in
```

Output lands in `artifacts/package/`: `layout/` (publish output plus `MicrosoftGame.config` and
shared `ShellVisuals`), `layout.xml` (the `makepkg genmap` mapping file) and `out/` (the
`makepkg validate` log and the packed `.msixvc`).

`eng/package.ps1` and `eng/run-package-tests.ps1` both require an installed GDK and therefore
cannot run on a hosted CI runner.

### The NuGet package

`eng/package.ps1` builds the *game* package. The library itself is packed by the SDK:

```powershell
pwsh eng/build.ps1 -Configuration Release -Pack
```

`GDK.Net` is the library package. Exact Windows/Core dependencies must survive packing for
every target framework. Package preparation does not publish to a feed; see
[Releasing](releasing.md) for retention, approval and maintenance.

Because `GenerateDocumentationFile` is on, the package
carries `GDK.Net.xml` next to the assembly for all three targets, so consumers get the full
IntelliSense text. Package checks must inspect the actual output, not an older package
left in another output directory, and verify both metadata and clean external consumers.

Expect six entries: a `GDK.Net.dll` and a `GDK.Net.xml` under each of `lib/net8.0`, `lib/net10.0`
and `lib/netstandard2.0`.

### Packaged test tiers

```powershell
pwsh eng/run-package-tests.ps1                                     # Layout + Msixvc (default)
pwsh eng/run-package-tests.ps1 -Tier Layout,Msixvc,Register -Restore
```

The tiers are ordered by invasiveness. `Layout` and `Msixvc` only write to `artifacts/`.
**`Register` and `Install` modify the machine**: they loose-register or install the package,
launch the title and read back the JSON report the harness writes. Pass `-Restore` to undo this,
and `-PriorPackage` to reinstall the title whose identity the harness borrows.

These are manual, local-only tests. [PR gates](ci/pr-gates.md) perform deterministic
build/package, unit/native-contract, AOT and documentation checks without running a title.

## Generation and verification tooling

| Script | Purpose |
|---|---|
| `eng/generate-interop.ps1` | ClangSharp generation of the raw interop layer into the git-ignored `eng/Generated/`. See [`../eng/README.md`](../eng/README.md). |
| `eng/build.ps1` | Resolves/restores the current pin, generates production inputs, and runs selected build/test/package/AOT/docs checks. |
| `eng/generate-playfab.ps1` | Generates the PlayFab projection from explicit selected inputs and clean model/export/output paths. Never reuse old exports for a new SDK. |
| `eng/api-coverage.ps1` | Reports export coverage; identifier counts alone do not establish ABI correctness. |
| `eng/generate-docs.ps1` | Regenerates the [API reference](api/) with docfx, then lays it out by area. |
| `eng/api-layout.ps1` | The layout pass on its own: moves docfx's flat output into one folder per namespace and writes every index. Safe to re-run on an already-laid-out tree. |
| `eng/update-learn-index.ps1` | Refreshes `eng/gdk-learn-index.json` from the published GDK table of contents, which is what the area indexes link out to. |

## Regenerating the API reference

The reference under [`docs/api/`](api/) is generated from the XML documentation comments in the
source and **committed**. Regenerate it after any public API change:

```powershell
dotnet tool restore
pwsh eng/generate-docs.ps1
```

`GDK.Net.Tests` checks that public types and members have reference pages and that pages do not
outlive their types. It does not invoke docfx. To compare the complete committed reference with
fresh generator output without modifying it, run:

```powershell
pwsh eng/generate-docs.ps1 -Check
```

This check restores the pinned documentation tools and fails on generated-output drift or missing
tooling.

### How the reference is laid out

docfx writes one flat folder of `GDK.Net.<Namespace>.<Type>.md` pages. GitHub truncates any folder
listing at 1,000 entries, and the projection is past that, so `generate-docs.ps1` finishes by
calling `eng/api-layout.ps1`, which:

1. moves each page into a folder named for its namespace, from `eng/api-areas.json`
   (`GDK.Net.PlayFab.Party` becomes `docs/api/PlayFab/Party/`, and a namespace with no entry gets a
   folder derived from its name);
2. rewrites every inter-page link and the `toc.yml` hrefs to the new relative paths;
3. writes a `README.md` in each folder listing every type with its kind, its summary, and a link to
   the matching GDK reference page on Microsoft Learn, resolved through `eng/gdk-learn-index.json`;
4. writes the root [`docs/api/README.md`](api/README.md) index of areas.

The pass is idempotent: it re-derives namespaces from the page headings and normalises link targets
before recomputing them, so running it twice changes nothing. To re-run the layout alone, without
docfx:

```powershell
pwsh eng/api-layout.ps1
```

Editing `eng/api-areas.json` is how you change a folder name, an area title, an area summary, or
the GDK families an area cross-links to.

## Related guides

- [Getting started](getting-started.md): initialising the runtime and signing in a user.
- [Architecture](architecture.md): layering, target frameworks and the AOT contract.
- [Changing the GDK edition](gdk-edition.md): the re-pin procedure and minimum version.
- [API reference](api/), every public type and member.
