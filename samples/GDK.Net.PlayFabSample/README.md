# GDK.Net.PlayFabSample

A small, readable Microsoft GDK title that demonstrates the **PlayFab** half of the `GDK.Net`
projection. Every file here is meant to be read: plain straight-line calls, one idea per method, no
test scaffolding.

For the non-PlayFab surface — users, game UI, activation, networking — see
[`samples/GDK.Net.UserSample`](../GDK.Net.UserSample). For the exhaustive pass/fail run that grades
every API and writes a machine-readable report, see
[`tests/GDK.Net.LiveHarness`](../../tests/GDK.Net.LiveHarness).

## What it shows

| File | What it demonstrates |
|---|---|
| `Program.cs` | Initializing PlayFab alongside the Gaming Runtime, the error model, and shutdown ordering |
| `Demos/LoginDemo.cs` | Getting an authenticated entity both ways: the signed-in Xbox user, and a developer secret key |
| `Demos/ServicesDemo.cs` | Calling the generated service layer — server time, title data, account info, entity profile |
| `Demos/PartyDemo.cs` | Party's frame-loop model: region latency and a local user round trip through `ProcessStateChanges()` |
| `SampleOptions.cs` | Command line, the log file, and where the secret key is read from |

## What "idiomatic" means here

PlayFab ships with the GDK as six flat C libraries — Core, Services, GameSave, Multiplayer, Party
and Party Xbox Live — totalling about a thousand exports. `GDK.Net` projects all of them, and the
projection's job is that none of their C conventions reach your game code:

| PlayFab (C) | GDK.Net |
|---|---|
| `XAsyncBlock` + `…GetResultSize` + `…GetResult` | `Task<TResult>` / `await` |
| `HRESULT` return, `E_PF_*` codes | Exceptions; `PlayFabException` names the `E_PF_*` symbol |
| Native request structs with `count` + `pointer` field pairs | Request objects with `IReadOnlyList<T>` and `IReadOnlyDictionary<K,V>` |
| `PFEntityHandle`, `PFServiceConfigHandle`, `PFLocalUserHandle` | `IDisposable` classes |
| ISO-8601 `char*` timestamps | `DateTimeOffset` |
| `PartyStateChange` union + tag switch + `FinishProcessingStateChanges` | A state-change class hierarchy over a `foreach` that releases the batch |

`Demos/ServicesDemo.cs` is the shortest path to seeing the first four; `Demos/PartyDemo.cs` covers
the last.

### Party is deliberately not `Task`-based

Party does not use `XAsyncBlock`. Work is started with a call that returns immediately, and
completions arrive on a queue the title drains once per frame. The projection keeps that model
rather than hiding it behind a `Task`, because a game's frame loop is where the pump belongs and
because a state change can arrive with no operation to match it at all — a remote player leaving,
say. What it does remove is the raw `void* asyncIdentifier`, the union of change structs and the
paired start/finish bracket.

## Running it

```powershell
pwsh eng/run-local.ps1 -Project PlayFabSample
```

That publishes a self-contained executable to `artifacts/local/GDK.Net.PlayFabSample` and runs it.
No packaging, no registration — just a console app that prints what each API returned.

What it does need is an installed Microsoft GDK (edition `260404`), a dev-unlocked machine and an
account signed in to the Xbox app. Eight files have to sit next to the executable, all of which the
build puts there for you (see [`eng/packaging/GdkRedist.targets`](../../eng/packaging/GdkRedist.targets)):

| File | Why |
| --- | --- |
| `xgameruntime.thunks.dll` | The module every Gaming Runtime P/Invoke binds to. |
| `MicrosoftGame.config` | Supplies the title identity the Gaming Runtime would otherwise take from package identity. |
| `PlayFabCore.dll`, `PlayFabServices.dll` | Authentication and the 20 generated service classes. |
| `PlayFabGameSave.dll`, `PlayFabMultiplayer.dll` | Cloud saves and lobby/matchmaking, enabled here so the sample matches a real title's layout. |
| `Party.dll`, `PartyXboxLive.dll` | Party chat and networking. |
| `libHttpClient.dll` | What every PlayFab library makes its HTTP calls through. |

They arrive because the project opts in:

```xml
<GdkNetIncludePlayFab>true</GdkNetIncludePlayFab>
<GdkNetIncludePlayFabParty>true</GdkNetIncludePlayFabParty>
<GdkNetIncludePlayFabMultiplayer>true</GdkNetIncludePlayFabMultiplayer>
```

> **These come from `windows\bin\<arch>`, not from `GRDK\ExtensionLibraries`.** The GDK ships two
> PlayFab stacks that are *not* ABI compatible — `PartyCreateLocalUser`, for one, takes a
> `PFEntityHandle` in the first and two strings in the second. Mixing them is an access violation at
> run time, not a build error. `GDK.Net` is generated from `windows\include\playfab` and so binds
> and redistributes the matching `windows\bin` binaries.

Because those are copied by an ordinary build too, `dotnet run` works:

```powershell
dotnet run --project samples/GDK.Net.PlayFabSample -f net8.0
```

### Switches

| Switch | Meaning |
| --- | --- |
| `--title-id <id>` | The PlayFab title to talk to. Defaults to `10D176`, or `%GDKNET_PLAYFAB_TITLE_ID%`. |
| `--allow-ui` | Fall back to the account picker when no user is signed in. |
| `--out <dir>` | Where the log goes. Defaults to `%LOCALAPPDATA%\GDK.Net.PlayFabSample`. |

Output is mirrored to `<out>\playfab-sample.log`.

Note that the PlayFab title id has nothing to do with the Xbox title id in `MicrosoftGame.config`:
PlayFab keeps its own title registry, so the projection has to be told which one to use.

### The developer secret key

The title-entity path and the server APIs need a PlayFab developer secret key. It is read **only**
from the environment — `GDKNET_PLAYFAB_SECRET_KEY`, `PLAYFAB_DEVELOPER_SECRET_KEY` or
`PLAYFAB_SECRET_KEY` — and never from a command-line flag, because a flag would leave a server
credential in shell history and CI logs. With none set, the sample skips those two sections and runs
everything else.

A shipping game never holds a secret key at all. It authenticates as the player through
`PlayFabLocalUser`, which is what `Demos/LoginDemo.cs` shows second.

## Shutdown ordering

`GameRuntime.Dispose()` handles this, so a title does not have to sequence the two libraries by
hand — but it is worth knowing what it is doing:

1. Dispose every `PartyManager` and `PFMultiplayer` handle.
2. Dispose every `PlayFabEntity`, `PlayFabLocalUser` and `PlayFabServiceConfig`.
   `PFUninitializeAsync` fails while any of them is outstanding.
3. `GameRuntime.Dispose()` drains PlayFab — `PFServicesUninitializeAsync` then
   `PFUninitializeAsync` — *before* `XGameRuntimeUninitialize`, because PlayFab's shutdown runs on
   the process default task queue and faults if that queue is already gone.

So steps 1 and 2 are the title's job and step 3 is automatic. That is why every PlayFab handle in
`Program.cs` is scoped with `using` inside the runtime's own `using`: the inner scopes close first.

A title that wants to observe the shutdown, or bound it itself, can still `await
PlayFabRuntime.UninitializeAsync()` explicitly; the automatic pass then finds nothing to do.

## ⚠️ Borrowed package identity

`MicrosoftGame.config` reuses the identity of an existing title (`41336MicrosoftATG.GodotTestApp`),
the same one `GDK.Net.UserSample` and `tests/GDK.Net.LiveHarness` borrow. That only matters when
packaging: with a package registered, only one of the three can exist at a time. Run unpackaged and
the collision disappears, because nothing is registered.

See [the harness README](../../tests/GDK.Net.LiveHarness/README.md#-borrowed-package-identity--read-before-running-the-invasive-tiers)
for how to restore the machine, and for how to substitute your own Partner Center identity.

## Building without a GDK

Compiling needs nothing special — the project is part of the solution and builds on any Windows
machine with the .NET SDK. It multi-targets `net8.0;net10.0`, so a manual publish must name one:

```powershell
dotnet build samples/GDK.Net.PlayFabSample
dotnet publish samples/GDK.Net.PlayFabSample -c Release -f net8.0
```

With no GDK installed there is nothing to copy, so the build says so and produces an executable that
cannot run: the first native call fails with `0x89240101`. That is deliberate — hosted CI compiles
this project without a GDK and only needs it to build.
