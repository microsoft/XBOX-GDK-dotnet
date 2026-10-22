# Shared packaging assets

`ShellVisuals/` holds the tile, logo and splash images that a GDK `MicrosoftGame.config` references
by file name. They must sit at the root of the package layout, next to the executable.

Both packaged apps in this repository use them:

- `tests/GDK.Net.LiveHarness`: the live pass/fail harness
- `samples/GDK.Net.UserSample`: the readable demonstration app

They live here rather than in either project so neither owns the only copy; each project links them
in with `CopyToOutputDirectory` so a publish already places them at the layout root.

`MicrosoftGame.config` is **not** shared: it names the executable, so each project keeps its own.
It is also what lets both apps run *unpackaged*: the Gaming Runtime reads title identity from it
when the process has no package identity, so it must reach the output directory of an ordinary
build, not just the package layout.

## `GdkRedist.targets`

Imported by repository apps and shipped under the package's `buildTransitive/` directory. Reads
the application's actual NuGet restore graph with the SDK's `NuGet.ProjectModel` lockfile reader,
validates both exact SDK dependencies, and copies selected DLLs for build **and** publish.
It works through an intermediate carrier package, with all three supported target frameworks.
No PowerShell, Python, source generation or repository imports run in package consumers.

| File | Source | Required |
| --- | --- | --- |
| `xgameruntime.thunks.dll` | Restored Windows package's `native\<edition>\windows\bin\<arch>\` | Yes: every P/Invoke binds to it, and it is not installed system-wide |

`win-x64` and `win-arm64` select native architecture; absent a RID, `GdkRedistArchitecture` or the
ARM64 platform selects it, otherwise x64 is the default. Conflicting RID/architecture choices fail.
`GdkNetIncludeXboxLive`, `GdkNetIncludePlayFab`, `GdkNetIncludePlayFabMultiplayer` and
`GdkNetIncludePlayFabParty` retain their false defaults. Multiplayer and Party imply PlayFab even
when the core flag is explicitly false. Shared DLLs are copied once. Every selected DLL is required.

Installed `GameDK` / `GameDKCoreLatest` values are ignored; there is no installed-SDK fallback or
override. `Resolve-GdkTool.ps1` similarly resolves `makepkg`/`wdapp` from the separately restored,
hash-verified Core package. Packaging a title still requires its own `MicrosoftGame.config`, and
live execution/registration remains manual.
The local title scripts (`package.ps1`, `run-local.ps1`, `run-package-tests.ps1`) accept
`-NuGetConfig` and `-PackagesDirectory` to keep their publish restore on the explicitly chosen
sources/cache used for preparation; they do not infer or change a user's source configuration.

## Package verification

After `eng\build.ps1 -Pack`, run:

```powershell
pwsh eng\packaging\verify-package.ps1 `
  -Package artifacts\gdk\260404\2604.4.7897\packages\GDK.Net.260404.1.0.nupkg `
  -WorkRoot <isolated-directory-outside-checkout> -Aot
```

`-NuGetConfig` explicitly chooses an existing source configuration; no source fallback is automatic.
`-PackagesDirectory` optionally chooses an isolated package cache. The default lives under WorkRoot.
`-ConflictingSdkVersion` supplies a different real SDK version for direct-dependency-wins negatives;
the current baseline's negative fixture uses `2604.5.7903`, never as a production input.
For a focused negative-input/AOT rerun, `-CheckSet Native` omits the full family/TFM/RID matrix;
provenance records this narrower scope. CI uses the default `-CheckSet All`.

The verifier checks metadata/DLL/XML pairs and exact dependencies, direct and carrier consumption,
all TFMs and both RIDs, all native opt-ins, matching/conflicting direct SDK references, each missing
native DLL, normal publish and optional real packaged-library `net10.0`/`win-x64` NativeAOT publish.
It never executes the consumer, contacts live GDK services, installs/registers anything or publishes
a feed package. `verification.json` and diagnostic logs remain in WorkRoot; they are not public
provenance because command logs can include local paths. Only the produced `GDK.Net.*.nupkg`
belongs in library-package uploads.

Packing fixes NuGet's `DeterministicTimestamp` to the ZIP epoch (`1980-01-01T00:00:00Z`) as
well as enabling the SDK's compiler determinism. A boolean `true` is insufficient: NuGet uses the
current time in that mode.
Unchanged prepared/built inputs therefore produce identical package archive bytes on repeated
`dotnet pack --no-build --no-restore` invocations. This is not a claim that arbitrary fresh builds
on different toolchains or operating systems reproduce a retained binary.
