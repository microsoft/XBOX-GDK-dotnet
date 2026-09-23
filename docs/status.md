# Projection status

What is projected today, which native module each family binds to, and what is deliberately out of
scope. The coverage numbers here come from [`eng/api-coverage.ps1`](../eng/api-coverage.ps1), which
diffs the bound entry points against each module's export table.


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
[`eng/generate-playfab.ps1`](../eng/generate-playfab.ps1) (`eng/playfab/`); the lifetime types and
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
edition. Run [`eng/api-coverage.ps1`](../eng/api-coverage.ps1) `-Module PlayFab` for the current
per-module tally.

Every family follows [`eng/interop-conventions.md`](../eng/interop-conventions.md): per-TFM shims
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
unreachable API's alternative is, is in [`eng/unexported-apis.md`](../eng/unexported-apis.md).
Run [`eng/api-coverage.ps1`](../eng/api-coverage.ps1) to diff the bound entry points against the
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
[`eng/packaging/GdkRedist.targets`](../eng/packaging/GdkRedist.targets) copies it into the output of
both app projects on every build, and `eng/package.ps1` does the same for the package layout.

**Packaging is not required to run** — a GDK title is normally packaged, but packaging matters here
for one reason only: the Gaming Runtime resolves title identity (TitleId, StoreId, sandbox) from
package identity. Put `MicrosoftGame.config` next to the executable and it reads the identity from
there instead, so on a dev-unlocked PC with an account signed in, a plain `dotnet run` or a
self-contained publish works — see [`eng/run-local.ps1`](../eng/run-local.ps1). Without that file the
runtime still initializes but the first `XUser` call fails with `0x89245110`
("no package identity"). Packaging remains what a title ships as, and
`eng/run-package-tests.ps1` still exercises it; it is just no longer the only way to run.

**Verification** — `dotnet build -c Release` across `net8.0`, `net10.0` and `netstandard2.0` is
warning-clean, and `dotnet test -c Release` passes 1216 tests. Interop correctness was cross-checked
against ClangSharp output generated from the installed headers by `eng/generate-interop.ps1`.

Unit tests deliberately never require the Gaming Runtime, so they run identically on a hosted CI
runner. The pilot has additionally been exercised **live in a packaged GDK title** against a real
signed-in account — 22 of 23 steps pass, the 23rd being an opt-in sign-out. See
[`tests/GDK.Net.LiveHarness/README.md`](../tests/GDK.Net.LiveHarness/README.md). Those package
tests are manual and local-only: they need an installed GDK, which a hosted runner does not have.
