# GDK.Net

GDK.Net is the .NET/C# idiomatic projection of the Microsoft GDK flat C API, intended to expose GDK concepts as modern C# (`Task`, `IDisposable`, events, exceptions, and `[Flags]` enums) rather than raw P/Invoke. The authoritative implementation specification is [`docs/plan.md`](docs/plan.md), with shared language-neutral references in [`docs/reference/`](docs/reference/).

The `docs/` directory holds both authored guides and a vendored, one-way copy of the shared
specification from the `gdk-projections-plans` meta repo. `docs/plan.md` and `docs/reference/` are
vendored — do not edit them here; update the meta repo sources and re-copy them instead.

## Documentation

| Guide | Covers |
|---|---|
| [Getting started](docs/getting-started.md) | The idioms the projection uses, initialising `GameRuntime`, signing in a user |
| [Building](docs/building.md) | Prerequisites, build/test, running unpackaged, packaging, the generation tooling |
| [Architecture](docs/architecture.md) | Layering, the native modules, the three target frameworks, the AOT contract |
| [The pinned GDK edition](docs/gdk-edition.md) | Minimum version and the full re-pin procedure |
| [API reference](docs/api/) | Every public type and member, generated from the XML doc comments |
| [`docs/plan.md`](docs/plan.md) | The authoritative implementation specification (vendored) |
| [`docs/reference/`](docs/reference/) | Shared language-neutral references (vendored) |


## Supported versions

### Table A — Supported runtimes

| Runtime / version | Tier | Architectures (RIDs) | GDK edition | Build toolchain | Status |
|---|---|---|---|---|---|
| `net8.0` | LTS, Primary tier | `win-x64` (primary), `win-arm64` | `260404` | .NET SDK 10.0.x (verified locally: 10.0.302; 9.0.316 also present), MSVC v143+ / Windows SDK, ClangSharp for binding generation | Supported |
| `net10.0` | LTS, Primary tier | `win-x64` (primary), `win-arm64` | `260404` | .NET SDK 10.0.x (verified locally: 10.0.302; 9.0.316 also present), MSVC v143+ / Windows SDK, ClangSharp for binding generation | Supported |
| `netstandard2.0` | Compatibility tier (MonoGame / Mono / broad reach) | `win-x64` (primary), `win-arm64` | `260404` | .NET SDK 10.0.x (verified locally: 10.0.302; 9.0.316 also present), MSVC v143+ / Windows SDK, ClangSharp for binding generation | Supported |

### Table B — Supported engines / frameworks

| Engine / framework | Version(s) | Tier | Status |
|---|---|---|---|
| MonoGame | latest stable Windows project/template (policy: validate current stable release during pilot/live harness work) | Release gate for game workflow integration | Supported |
| Stride | latest stable (policy: scheduled/nightly validation after the pilot locks conventions) | Secondary engine workflow integration | Supported |
| Bespoke hosts and tooling | Microsoft-supported .NET LTS hosts on Windows | Primary tools/services workflow | Supported |

### Support policy

Only Microsoft-supported .NET LTS releases are targeted; the window moves as LTS releases ship and go EOL. `netstandard2.0` exists solely for MonoGame/Mono reach. This projection is Windows-only (no Linux/macOS), and the GDK edition is pinned at `260404`.

## Package feeds

This repository defaults to the public NuGet registry, `https://api.nuget.org/v3/index.json`, via the repo-local [`NuGet.config`](NuGet.config). That keeps outside contributor clones and `windows-latest` GitHub-hosted CI on public registries, because hosted CI cannot reach Microsoft-internal package feeds.

Microsoft-internal developers can opt in to the verified corporate NuGet proxy by uncommenting the `msfeedproxy` source in `NuGet.config`:

```xml
<add key="msfeedproxy" value="https://packagefeedproxy.microsoft.io/nuget/v3/index.json" protocolVersion="3" />
```

The endpoint `https://packagefeedproxy.microsoft.io/nuget/v3/index.json` was verified working from this machine. No explicit authentication was required from this machine.

## Repository layout

- `src/GDK.Net/` — the idiomatic .NET projection.
- `src/GDK.Net/Interop/` — the raw P/Invoke layer bound to `xgameruntime.thunks.dll`, and — for the
  Xbox Live and PlayFab families — `Microsoft.Xbox.Services.C.Thunks.dll` and the six PlayFab
  extension-library DLLs.
- `tests/GDK.Net.Tests/` — xUnit tests for the error model, header/interop contract, and runtime binding.
- `tests/GDK.Net.LiveHarness/` — GDK title that exercises the `XUser` pilot live and writes the JSON
  report `eng/run-package-tests.ps1` grades. Runs unpackaged (`eng/run-local.ps1`) or packaged.
- `samples/GDK.Net.UserSample/` — readable GDK title demonstrating the same APIs as ordinary
  straight-line code.
- `samples/GDK.Net.PlayFabSample/` — the same, for PlayFab: authentication, the generated service
  layer and Party's frame-loop pump.
- `eng/` — ClangSharp generation tooling (`GDK.Net.rsp`, `generate-interop.ps1`), the PlayFab
  header-driven generator (`playfab/`, driven by `generate-playfab.ps1`), the binding
  rulebook (`interop-conventions.md`), the unreachable-API record (`unexported-apis.md`), the
  coverage check (`api-coverage.ps1`), the API reference generator (`generate-docs.ps1`,
  configured by `docfx.json`), the unpackaged run script (`run-local.ps1`) and the package
  build/test scripts (`package.ps1`, `run-package-tests.ps1`).
- `docs/plan.md` — vendored authoritative .NET projection plan.
- `docs/reference/` — vendored shared reference documents.
- `docs/api/` — generated API reference (`eng/generate-docs.ps1`).
- `docs/building.md`, `docs/getting-started.md`, `docs/architecture.md`, `docs/gdk-edition.md` —
  authored guides.
- `.github/workflows/ci.yml` — Windows CI for restore, build, and tests.

## Building

From this repository root:

```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release --no-build
```

None of that needs a GDK install. To actually *run* against the Gaming Runtime you need an installed
GDK (edition `260404`), a dev-unlocked machine and a signed-in Xbox account — but not a package:

```powershell
pwsh eng/run-local.ps1 -Project Sample    # readable demo, self-contained, unpackaged
pwsh eng/run-local.ps1                    # the live harness, same
pwsh eng/run-package-tests.ps1            # the packaged path, which is what a title ships as
```

See [`docs/building.md`](docs/building.md) for prerequisites, package-feed notes, the
`GdkNetEnableAotAnalyzers` escape hatch and the packaged test tiers.

## Status

The Gaming Runtime surface is projected against the real GDK headers of edition `260404`
(`%GameDKCoreLatest%windows\include`), including the PlayFab headers under
`%GameDKCoreLatest%windows\include\playfab`.

**Implemented families**

| Family | Projection |
|---|---|
| `XGameRuntime`, `XTaskQueue`, `XAsync` | `GameRuntime`, `AsyncOperation<T>` (task queues stay internal) |
| `XUser` | `UserManager`, `User` |
| `XGameInvite` / `XGameProtocol`, `XGameEvent` | `GameActivationManager`, `GameEvents` |
| `XSystem`, `XThread`, `XError`, `XGame`, `XLauncher`, `XDisplay` | `GDK.Net.SystemInfo` |
| `XPersistentLocalStorage` | `PersistentLocalStorage` |
| `XPackage` | `GamePackage`, `PackageInstallationMonitor`, `PackageMount` |
| `XGameSave`, `XGameSaveFiles` | `GameSaveProvider`, `GameSaveContainer`, `GameSaveFiles` |
| `XGameUI` | `GameUiManager`, `GameTextEntry`, `CustomGameUi` |
| `XNetworking` | `NetworkingManager` |
| `XAppCapture`, `XAppBroadcast` | `AppCaptureManager` |
| `XStore` | `StoreContext`, `StoreProduct`, `StoreLicense`, `StoreProductQuery` |
| `XGameStreaming` | `StreamingManager` |
| `XClosedCaption`, `XHighContrast`, `XSpeechToText` | `AccessibilityManager` |
| `XSpeechSynthesizer` | `SpeechSynthesizer` |
| `XblContext`, `XblProfile`, `XblAchievements` | `XboxLiveService`, `XboxLiveContext`, `ProfileService`, `AchievementsService` |
| `XblSocial`, `XblPresence`, `XblPrivacy`, `XblLeaderboard`, `XblRealTimeActivity` | `SocialService`, `PresenceService`, `PrivacyService`, `LeaderboardService`, `RealTimeActivityService` |
| `XblUserStatistics`, `XblTitleManagedStats`, `XblTitleStorage` | `UserStatisticsService`, `TitleManagedStatisticsService`, `TitleStorageService` |
| `XblStringVerify`, `XblEvents`, `XblMultiplayerActivity`, `XblGetErrorCondition` | `StringVerificationService`, `EventsService`, `MultiplayerActivityService`, `XboxLiveErrors` |
| `XblSocialManager`, `XblAchievementsManager` | `SocialManager`, `AchievementsManager` |
| `PFCore`, `PFServiceConfig`, `PFEntity`, `PFAuthentication`, `PFEvents`, `PFHttpSettings` | `PlayFabRuntime`, `PlayFabServiceConfig`, `PlayFabEntity`, `PlayFabLocalUser`, `PlayFabHttpSettings`, `PlayFabErrors` |
| `PFAccountManagement*`, `PFCatalog*`, `PFInventory*`, `PFGroups*`, … (the whole `PFServices` surface) | `AccountManagement`, `Authentication`, `Catalog`, `CloudScript`, `Data`, `Events`, `Experimentation`, `Friends`, `Groups`, `Inventory`, `Leaderboards`, `Localization`, `MultiplayerServer`, `PlatformSpecific`, `PlayerDataManagement`, `Profiles`, `PushNotifications`, `Segments`, `Statistics`, `TitleDataManagement` |
| `PFGameSave`, `PFGameSaveFiles` | `PlayFabGameSaveFiles` |
| `PFLobby`, `PFMatchmaking` | `PlayFabMultiplayer`, `Lobby`, `MatchmakingTicket` |
| `Party` | `PartyManager`, `PartyLocalUser`, `PartyNetwork`, `PartyEndpoint`, `PartyDevice`, `PartyChatControl`, `PartyInvitation`, `PartyTextToSpeechProfile` |
| `PartyXboxLive` | `PartyXblManager`, `PartyXblChatUser` |

Xbox Live Services is a **second native module** — `Microsoft.Xbox.Services.C.Thunks.dll`, plus its
hard dependency `libHttpClient.dll`. It is reached through `GameRuntime.XboxLive` and is opt-in at
packaging time (`<GdkNetIncludeXboxLive>true</GdkNetIncludeXboxLive>`), so a title that does not use
Xbox Live does not carry the extra ~2.2 MB. The multiplayer session directory, its manager layer,
matchmaking and the raw HTTP escape hatch are **deliberately out of scope**; see plan §13.1.1 for
why. The two projected manager layers are process-global and hang off `GameRuntime.XboxLive` rather
than a context, and must be pumped once per frame — `SocialManager.DoWork` and
`AchievementsManager.DoWork`.

PlayFab is projected in full — 1,006 of the 1,008 exports across its six native modules. Those
modules are installed beside the Gaming Runtime under `%GameDKCoreLatest%windows\bin\<arch>`, and
are opt-in at packaging time, in three groups:

| Property | Native modules | Managed surface |
|---|---|---|
| `<GdkNetIncludePlayFab>` | `PlayFabCore.dll`, `PlayFabServices.dll`, `PlayFabGameSave.dll`, `libHttpClient.dll` | `GDK.Net.PlayFab` — `PlayFabRuntime`, the 20 generated service classes, `PlayFabGameSaveFiles` |
| `<GdkNetIncludePlayFabMultiplayer>` | `PlayFabMultiplayer.dll` | `GDK.Net.PlayFab.Multiplayer` — lobbies and matchmaking |
| `<GdkNetIncludePlayFabParty>` | `Party.dll`, `PartyXboxLive.dll` | `GDK.Net.PlayFab.Party` — voice, text chat and network transport |

The last two groups import `PlayFabCore.dll`, so asking for either turns the first group on as
well. Note that the GDK also installs a *second*, standalone PlayFab stack under
`%GameDKCoreLatest%GRDK\ExtensionLibraries` (`*.GDK.dll`, plus a `Party.dll` whose
`PartyCreateLocalUser` takes an entity id and token pair rather than a `PFEntityHandle`). The two
stacks are not ABI compatible; this projection binds the `windows\bin` one, the same tree the Xbox
Live group takes `Microsoft.Xbox.Services.C.Thunks.dll` from.

The interop layer, the service classes, their request/result models and the `E_PF_*` error
constants are **generated** from the shipped headers by
[`eng/generate-playfab.ps1`](./eng/generate-playfab.ps1) (`eng/playfab/`); the lifetime types and
the state-change pumps are hand-written on top. A failing PlayFab call surfaces as
`PlayFabException` (facility `0x8923`, with the `E_PF_*` symbol in the message); Party reports
`PartyError` rather than an HRESULT and so surfaces as `PartyException`.

Lobbies, matchmaking, Party and PartyXboxLive do not use `XAsyncBlock`, so they are not awaitable:
each start returns an operation id and the title pumps `PlayFabMultiplayer.ProcessStateChanges`,
`PartyManager.ProcessStateChanges` or `PartyXblManager.ProcessStateChanges` once per frame and
matches the completion record by that id. **Deliberately not projected**: the `*GetCustomContext` /
`*SetCustomContext` pairs (a raw `void*` title slot, replaced by the projection's own identity
map), the 18 undocumented `PFGameSave*ForDebug` entry points, and `PFInitializeWithLHC` and
`PartyCreateLocalUserWithEntityType`, which are exported but declared in no shipped header of this
edition. Run [`eng/api-coverage.ps1`](./eng/api-coverage.ps1) `-Module PlayFab` for the current
per-module tally.

Every family follows [`eng/interop-conventions.md`](./eng/interop-conventions.md): per-TFM shims
(`[LibraryImport]` on `net8.0`/`net10.0`, `[DllImport]` on `netstandard2.0`), `SafeHandle`-backed
handle lifetime, `XAsyncBlock` → `Task` with `CancellationToken` → `XAsyncCancel`, and
register/unregister callbacks projected as .NET events that unregister with wait-for-completion.

**Deliberately out of scope** — `XAsyncProvider.h` (for *implementing* async providers rather than
consuming the runtime) and `XCurl.h`.

**APIs the thunks DLL omits** — 14 functions are **not exported** by `xgameruntime.thunks.dll`, so no
projection can reach them: 8 are declared in the public headers anyway (a genuine mismatch) and 6
exist only as symbols in `xgameruntime.lib`. None is behind a preprocessor guard. Edition `260404`
closed most of this gap: `260400` omitted 49, and the projection carried its own re-export shim to
reach them. That shim is gone — the projection now ships **no native code of its own**, and every
P/Invoke binds to the one redistributable module. The 15th unexported name, `XPackageMount`, is an
`E_NOTIMPL` stub in the library itself — DLC mounting was never blocked, and is available through
the exported `XPackageMountWithUiAsync`. XSAPI, by contrast, has **no** gap on GDK; what looks
missing there is gated out as non-GDK or internal surface. Full detail, including what each
unreachable API's alternative is, is in [`eng/unexported-apis.md`](./eng/unexported-apis.md).

### Title-implemented UI

By default the Gaming Runtime draws its own dialogs. `CustomGameUi.SetHandlers` registers the title
as the renderer instead, so `GameUiManager.ShowMessageDialogAsync` and its siblings invoke the
title's handler with a request object rather than showing system UI; the title draws whatever it
likes and calls `Respond` to complete the original operation. Handlers are per-UI and optional, so a
title can take over just the dialogs it cares about. This is how a title keeps a consistent visual
style, and on platforms with no system UI it is the only way these APIs work at all.

```csharp
CustomGameUi.SetHandlers(new CustomGameUiHandlers
{
    MessageDialog = request =>
    {
        // Runs on a thread pool thread, not the runtime's callback thread — blocking and awaiting
        // are both fine here. Responding may also happen later, from any thread.
        myGame.ShowDialog(request.TitleText, request.ContentText, chosen => request.Respond(chosen));
    },
});
```

**Threading.** The runtime raises these callbacks on the work port of the task queue driving the
originating operation, and it expects the title to get off that port rather than work on it. So the
projection copies the request payload out of runtime-owned memory, hands the request to the thread
pool, and returns from the native callback immediately. A handler may therefore block, `await`, and
in particular await further Gaming Runtime operations on that same queue without deadlocking against
itself. The cost is that handlers arrive with no synchronization context, so a title that must touch
its renderer marshals to its own thread as it would for any other background callback. The borrowed
`XTaskQueueHandle` is deliberately not exposed on the request: it would dangle the moment the
callback returns, and it is the one queue a handler must not schedule onto. A handler that throws
cannot take the process down, and if it throws before responding the projection posts the neutral
response on its behalf so the caller's operation is never left pending.

**Unregistering.** `CustomGameUi.ClearHandlers` passes a table whose function pointers are all null,
not a null pointer. `XGameUiSetUiCallbacks` dereferences its argument unconditionally, so passing
`nullptr` access-violates inside the Gaming Runtime and takes the process down — worth knowing for
the other language projections, since nothing in the header says so.

Run [`eng/api-coverage.ps1`](./eng/api-coverage.ps1) to diff the bound entry points against the
thunk export table; it currently reports **349 of 355** exports bound, the remaining six being the
out-of-scope `XAsync*` provider entry points.

**Which module to bind** — the entry points are **not** in `XGameRuntime.dll`. That module (in
`System32`) exports only four private version-negotiation ordinals; every public `X*` API is a
statically linked stub inside `xgameruntime.lib`, so a C++ title reaches them through the static
library and a P/Invoke against `XGameRuntime.dll` can only ever raise
`EntryPointNotFoundException`. The GDK also ships **`xgameruntime.thunks.dll`**
(`%GameDKCoreLatest%windows\bin\{x64,arm64}`), which re-exports the full surface as 390 flat
`__stdcall` C entry points. That is the module this projection binds to, and it must be
redistributed next to the game executable — it is not installed system-wide.
[`eng/packaging/GdkRedist.targets`](./eng/packaging/GdkRedist.targets) copies it into the output of
both app projects on every build, and `eng/package.ps1` does the same for the package layout.

**Packaging is not required to run** — a GDK title is normally packaged, but packaging matters here
for one reason only: the Gaming Runtime resolves title identity (TitleId, StoreId, sandbox) from
package identity. Put `MicrosoftGame.config` next to the executable and it reads the identity from
there instead, so on a dev-unlocked PC with an account signed in, a plain `dotnet run` or a
self-contained publish works — see [`eng/run-local.ps1`](./eng/run-local.ps1). Without that file the
runtime still initializes but the first `XUser` call fails with `0x89245110`
("no package identity"). Packaging remains what a title ships as, and
`eng/run-package-tests.ps1` still exercises it; it is just no longer the only way to run.

**Verification** — `dotnet build -c Release` across `net8.0`, `net10.0` and `netstandard2.0` is
warning-clean, and `dotnet test -c Release` passes 1216 tests. Interop correctness was cross-checked
against ClangSharp output generated from the installed headers by `eng/generate-interop.ps1`.

Unit tests deliberately never require the Gaming Runtime, so they run identically on a hosted CI
runner. The pilot has additionally been exercised **live in a packaged GDK title** against a real
signed-in account — 22 of 23 steps pass, the 23rd being an opt-in sign-out. See
[`tests/GDK.Net.LiveHarness/README.md`](./tests/GDK.Net.LiveHarness/README.md). Those package
tests are manual and local-only: they need an installed GDK, which a hosted runner does not have.

## NativeAOT

**Xbox consoles do not permit JIT compilation, so a .NET title that ships to console must be
NativeAOT-compiled.** `GDK.Net` is AOT-safe on `net8.0` and `net10.0`, and the repository enforces
that at three levels rather than asserting it once.

**AOT is opt-in, not required.** Nothing here forces it on you: `dotnet build`, `dotnet test`,
`dotnet pack` and an ordinary `dotnet publish` are unchanged and JIT-compiled, and the packaged
harness defaults to a self-contained `net8.0` JIT layout. `eng/package.ps1 -Aot` and the CI publish
job are the only AOT paths. Both modes are verified against a real signed-in account in a real
package — the harness's `runtime.compilation` step reports `Running JIT-compiled (X64)` or
`Running NativeAOT-compiled (X64)`, and both give the same 23 passed / 1 skipped. Consume this
library however suits your title; AOT is what console requires, not what this repo imposes.

**1. Analyzers, on every build.** `GDK.Net.csproj`, the harness and the sample set `EnableAotAnalyzer`,
`EnableTrimAnalyzer` and `EnableSingleFileAnalyzer`, so any construct requiring dynamic code or
unreferenced metadata is a build error under the repo-wide `TreatWarningsAsErrors`. The assembly
also carries `[AssemblyMetadata("IsTrimmable", "True")]`, which lets the trimmer prune it inside a
consuming title instead of rooting it wholesale.

The design that makes this pass is not incidental: every native entry point is a
`[LibraryImport]` — source-generated marshalling, so no marshalling stubs are produced at run time
— and every callback the runtime invokes is a `static` method with
`[UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]` taken as a
`delegate* unmanaged[Stdcall]<...>`. Nothing calls `Marshal.GetFunctionPointerForDelegate` on
`net8.0`+; the delegate-based path exists only inside `netstandard2.0` `#if` branches, which no AOT
publish ever compiles. `tests/GDK.Net.Tests/AotCompatibilityTests.cs` locks all of this in place, so
a regression fails the ordinary test run rather than surfacing on console months later.

**2. A real ILC compilation, in CI.** Analyzers only see what is annotated; ILC sees the whole
closure. The `Verify NativeAOT compilation` job AOT-publishes the harness — which transitively
compiles `GDK.Net` — and asserts the output is a native executable with no managed assemblies
beside it, since a silent fallback to an IL publish would still produce an `.exe`.

**3. A live packaged run.** Compiling is not running. `eng/run-package-tests.ps1 -Aot` builds the
AOT harness into a real GDK package and executes it; its `runtime.compilation` step reports
`RuntimeFeature.IsDynamicCodeSupported` so the log proves which mode actually ran:

```powershell
pwsh -Command "& .\eng\run-package-tests.ps1 -Tier Layout,Register -Aot"
```

This is the check that matters — it exercises the reverse-P/Invoke completion callbacks, the
`XAsyncBlock` → `Task` engine and cancellation under a runtime with no JIT at all. It needs an
installed GDK and a dev-unlocked machine, so it is manual.

To AOT-publish a title of your own:

```powershell
dotnet publish -c Release -f net10.0 -r win-x64 --self-contained -p:PublishAot=true
```

Two environment notes:

- **`net10.0` (or `net9.0`), not `net8.0`.** The NativeAOT runtime pack
  (`Microsoft.NETCore.App.Runtime.NativeAOT.win-x64`) was first published for 9.0, so a `net8.0` AOT
  publish cannot be restored with a 10.x SDK. `GDK.Net` still *targets* `net8.0` for
  framework-dependent consumers; only the AOT publish needs the newer TFM.
- **`vswhere.exe` must be on `PATH`.** ILC resolves `link.exe` by shelling out to `vswhere` and
  treating the command's combined output as the path, so when it is missing the *error text*
  becomes the linker and you get an opaque `MSB3073`. Prepend
  `%ProgramFiles(x86)%\Microsoft Visual Studio\Installer`; a `vcvars64` shell is **not** sufficient.
  `eng/package.ps1 -Aot` and CI both do this for you.

If your build machine cannot reach nuget.org, enabling the analyzers surfaces as `NU1100`/`NU1603`
for `Microsoft.NET.ILLink.Tasks` rather than as a network error. Build with
`-p:GdkNetEnableAotAnalyzers=false` to opt out; the library source is unchanged either way, you
simply lose the compile-time enforcement.


## Contributing

This project welcomes contributions and suggestions. Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the
instructions provided by the bot. You will only need to do this once across all repos using our CLA.

See [CONTRIBUTING.md](CONTRIBUTING.md) for what a change has to satisfy — the documentation
requirement on public APIs, the generated files you must not hand-edit, and how the pilot is
validated live. Security issues go to MSRC, not to a public issue; see [SECURITY.md](SECURITY.md).

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/)
or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or
comments.


## License

GDK.Net is licensed under the [MIT License](LICENSE). Use of the Microsoft GDK itself is governed by
the license that accompanies the GDK, not by this repository's license.


## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of
Microsoft trademarks or logos is subject to and must follow
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion
or imply Microsoft sponsorship. Any use of third-party trademarks or logos are subject to those
third-party's policies.
