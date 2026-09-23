# Building GDK.Net

This guide covers building, testing, running and packaging the repository. It is authored
documentation — unlike [`plan.md`](plan.md) and [`reference/`](reference/), which are vendored
copies from the meta repo.

## Prerequisites

| To do this | You need |
|---|---|
| Build the library, run the unit tests | .NET SDK 10.0.x. **No GDK install required.** |
| Run anything against the Gaming Runtime | An installed Microsoft GDK (edition `260404`), a dev-unlocked Windows machine, and an account signed in to the Xbox app |
| Package a title (`makepkg`) | The GDK, for `%GameDK%bin\makepkg.exe` |
| Re-generate interop or PlayFab bindings | The GDK, plus MSVC v143+ / Windows SDK (for `dumpbin.exe`) and Python 3 |
| Publish with NativeAOT (`-Aot`) | The MSVC toolchain — ILC shells out to `link.exe` |

The projection is **Windows-only**. See [`../README.md`](../README.md) for the authoritative
supported-runtime, architecture and toolchain matrix.

### Why no GDK is needed to build

The raw P/Invoke layer under `src/GDK.Net/Interop/` is **checked in**, not generated at build time.
The GDK headers and import libraries are inputs to the *generators* in `eng/`, which are run
deliberately and whose output is committed. Consequently `dotnet restore`, `dotnet build` and
`dotnet test` need no GDK, which is what lets hosted CI (`windows-latest`) build this repo.

The GDK is only needed to **run**, because `xgameruntime.thunks.dll` and the XSAPI/PlayFab redist
DLLs are copied next to the output by `eng/packaging/GdkRedist.targets` from `%GameDKCoreLatest%`.

## Build and test

From the repository root:

```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release --no-build
```

The solution is `GDK.Net.slnx`. `src/GDK.Net/GDK.Net.csproj` multi-targets
`net8.0;net10.0;netstandard2.0`, so a single build compiles three times — see
[`architecture.md`](architecture.md) for what actually differs between them.

### Warnings are errors

`Directory.Build.props` sets `TreatWarningsAsErrors=true` for every project in the repo. This is
deliberate and load-bearing: it is what makes the AOT, trim and single-file analyzers
(`IL2xxx`/`IL3xxx`) block a change that reintroduces reflection or runtime-generated marshalling
stubs, and what keeps the XML documentation complete. Do not suppress it globally.

### Package feeds

`NuGet.config` defaults to the public registry so that outside-contributor clones and
GitHub-hosted CI work. Microsoft-internal developers can uncomment the `msfeedproxy` source.

If restore fails with `NU1100`/`NU1603` naming `Microsoft.NET.ILLink.Tasks`, the feed in use cannot
serve the ILLink package that the AOT analyzers pull in. Build without them:

```powershell
dotnet build -c Release -p:GdkNetEnableAotAnalyzers=false
```

This only disables the *analyzers*. The AOT contract is still enforced for real by
`eng/package.ps1 -Aot`, which runs the actual ILC compilation.

## Running against the Gaming Runtime

A GDK title is normally packaged, registered and launched by AUMID. For day-to-day work that is
unnecessary — on PC, a `MicrosoftGame.config` beside the executable supplies the same identity, so
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

What it does *not* exercise is the packaged execution environment itself — installation into
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
dotnet pack src/GDK.Net/GDK.Net.csproj -c Release
```

`GDK.Net` is the only packable project. Because `GenerateDocumentationFile` is on, the package
carries `GDK.Net.xml` next to the assembly for all three targets, so consumers get the full
IntelliSense text. If you change the packaging, verify the XML survives — the documentation is
only worth writing if it reaches the consumer:

```powershell
Add-Type -AssemblyName System.IO.Compression.FileSystem
$nupkg = Get-Item src/GDK.Net/bin/Release/GDK.Net.*.nupkg | Select-Object -First 1
$zip = [IO.Compression.ZipFile]::OpenRead($nupkg.FullName)
$zip.Entries | Where-Object FullName -like 'lib/*' | Select-Object FullName, Length
$zip.Dispose()
```

Expect six entries: a `GDK.Net.dll` and a `GDK.Net.xml` under each of `lib/net8.0`, `lib/net10.0`
and `lib/netstandard2.0`.

### Packaged test tiers

```powershell
pwsh eng/run-package-tests.ps1                                     # Layout + Msixvc (default)
pwsh eng/run-package-tests.ps1 -Tier Layout,Msixvc,Register -Restore
```

The tiers are ordered by invasiveness. `Layout` and `Msixvc` only write to `artifacts/`.
**`Register` and `Install` modify the machine** — they loose-register or install the package,
launch the title and read back the JSON report the harness writes. Pass `-Restore` to undo this,
and `-PriorPackage` to reinstall the title whose identity the harness borrows.

These are manual, local-only tests. `.github/workflows/ci.yml` only restores, builds and unit-tests.

## Generation and verification tooling

| Script | Purpose |
|---|---|
| `eng/generate-interop.ps1` | ClangSharp generation of the raw interop layer into the git-ignored `eng/Generated/`. See [`../eng/README.md`](../eng/README.md). |
| `eng/generate-playfab.ps1` | Regenerates the PlayFab projection from the installed headers. Use `-SkipExports` to reuse `eng/playfab/exports.json` on a machine without `dumpbin.exe`. |
| `eng/api-coverage.ps1` | Diffs each native DLL's export table against the entry points bound in `src/GDK.Net/Interop`. The cheapest answer to "what is left?" |
| `eng/generate-docs.ps1` | Regenerates the [API reference](api/) with docfx. |

## Regenerating the API reference

The reference under [`docs/api/`](api/) is generated from the XML documentation comments in the
source and **committed**. Regenerate it after any public API change:

```powershell
dotnet tool restore
pwsh eng/generate-docs.ps1
```

`GDK.Net.Tests` contains a drift check that fails when the committed output no longer matches what
the generator produces. The check skips itself when docfx is not restored, so it cannot break a
build on a machine without the tool.

## Related guides

- [Getting started](getting-started.md) — initialising the runtime and signing in a user.
- [Architecture](architecture.md) — layering, target frameworks and the AOT contract.
- [Changing the GDK edition](gdk-edition.md) — the re-pin procedure and minimum version.
- [API reference](api/) — every public type and member.
