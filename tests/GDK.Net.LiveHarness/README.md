# GDK.Net.LiveHarness

A Microsoft GDK PC title that exercises the `XUser` pilot end to end against a real Gaming Runtime,
a real signed-in Xbox account and a real sandbox. It is the only thing in this repository that
proves the projection actually works — the unit tests deliberately never touch native code.

The harness writes a JSON report (and stdout, which a packaged title has no console for) that
`eng/run-package-tests.ps1` reads back and turns into pass/fail per step.

> **Looking for readable example code?** This project is a test harness: every call is wrapped in
> reporting scaffolding so a failure can be attributed to a named step. For plain, straight-line
> demonstrations of the same APIs, read
> [`samples/GDK.Net.UserSample`](../../samples/GDK.Net.UserSample).

## Why this exists

A P/Invoke that binds the wrong module fails **only** against a real Gaming Runtime, and fails with
a misleading `E_GAMERUNTIME_VERSION_MISMATCH`. Building this harness is what caught exactly that:
the projection originally bound `XGameRuntime.dll`, which exports nothing usable. See the repository
[Projection status](../../docs/status.md) for the full explanation and
`src/GDK.Net/Interop/Native.cs` for the details.

## Requirements

These tests are **manual and local-only**. A hosted CI runner can satisfy none of the following, so
`ci.yml` builds and unit-tests but never runs the harness.

- The Microsoft GDK, edition `260404` (`%GameDK%`, `%GameDKCoreLatest%`).
- A dev-unlocked machine.
- An Xbox account signed in on this machine, in the machine's current sandbox.
- For the packaged tiers only: `makepkg.exe` and `wdapp.exe`, which live in `%GameDK%\bin` —
  **not** under the edition folder.

## Running

The fast loop needs no packaging at all:

```powershell
pwsh eng/run-local.ps1                 # publish self-contained, run, print the report
pwsh eng/run-local.ps1 -Aot            # the same, NativeAOT
```

The identity the Gaming Runtime would normally take from package identity comes from
`MicrosoftGame.config` next to the executable instead, and `xgameruntime.thunks.dll` is copied
beside it by the build (see [`eng/packaging/GdkRedist.targets`](../../eng/packaging/GdkRedist.targets)).
With both present the harness reports the same 27 passes it does packaged. `dotnet run --project
tests/GDK.Net.LiveHarness -f net8.0` works for the same reason.

### Packaged tiers

Unpackaged runs cover the API surface, but not the packaged execution environment: an install into
`WindowsApps`, the package's own identity rather than a borrowed loose-file one, and launch by the
shell rather than by the console host. `eng/run-package-tests.ps1` covers that:

```powershell
# Non-invasive: publish, generate the map file, run the Submission Validator, pack the .msixvc.
pwsh eng/run-package-tests.ps1

# Everything, including the tiers that modify this machine.
pwsh eng/run-package-tests.ps1 -Tier Layout,Msixvc,Register,Install
```

| Tier | What it does | Modifies the machine |
|---|---|---|
| `Layout` | `dotnet publish` self-contained, copy `MicrosoftGame.config` + ShellVisuals + `xgameruntime.thunks.dll`, `makepkg genmap`, `makepkg validate` | No |
| `Msixvc` | `makepkg pack /pc` → a real `.msixvc` (~75 MB) | No |
| `Register` | `wdapp register <layout>` (loose registration), launch, read the report | **Yes** |
| `Install` | `wdapp install <msixvc>`, launch, read the report | **Yes** |

Useful switches: `-AllowUI` (permit `XUserAddAsync` to show account UI), `-SignOut` (also exercise
`XUserSignOutAsync`), `-Restore` (see below). The harness itself takes `--allow-ui`, `--sign-out`
and `--out <dir>`; `eng/run-local.ps1 -Arguments '--sign-out'` passes them through.

### JIT and NativeAOT

The harness runs both ways, and its first reported step, `runtime.compilation`, prints which one
actually executed so the log is self-evidencing rather than assumed:

```powershell
pwsh eng/run-package-tests.ps1 -Tier Layout,Register        # Running JIT-compiled (X64)
pwsh eng/run-package-tests.ps1 -Tier Layout,Register -Aot   # Running NativeAOT-compiled (X64)
```

**JIT is the default** — `-Aot` is strictly opt-in, and every tier works without it. The JIT layout
is a self-contained `net8.0` publish (~185 assemblies including `coreclr.dll`); the AOT layout is a
single ~2.5 MB `net10.0` native executable with no managed assemblies at all. AOT additionally needs
the MSVC linker, which is why it is not the default.

The project multi-targets `net8.0;net10.0` (the NativeAOT runtime pack only exists from 9.0 onward),
so publishing it **by hand** needs an explicit framework or the SDK reports `NETSDK1129`:

```powershell
dotnet publish tests/GDK.Net.LiveHarness -c Release -f net8.0
```

`eng/package.ps1` and `eng/run-local.ps1` always pass `-f` for you, so this only affects manual
publishes.

## ⚠️ Borrowed package identity — read before running the invasive tiers

`MicrosoftGame.config` deliberately reuses the identity of an existing title
(`41336MicrosoftATG.GodotTestApp`, StoreId `9MT216TML1T2`, TitleId `6184102E`) so the harness has a
real, provisioned identity to test with. Only the executable name, display name and description were
changed.

`samples/GDK.Net.UserSample` borrows the **same** identity, so only one of the two can be registered
at a time; registering either displaces the other. This applies to the packaged tiers only — an
unpackaged run registers nothing, so both apps can be run side by side.

**Registering or installing this harness therefore displaces that title.** It is fully reversible:

```powershell
pwsh eng/run-package-tests.ps1 -Tier @() -Restore -PriorPackage <path-to-that-title>.msixvc
```

`-Restore` uses `Remove-AppxPackage` rather than `wdapp unregister`, which fails with `0x80070490`
against a loose registration, and it removes the package before reinstalling, because `wdapp install`
refuses an identity that is still registered.

If you have your own Partner Center identity, replace the `<Identity>`, `<StoreId>`, `<MSAAppId>` and
`<TitleId>` in `MicrosoftGame.config` and the `$packageFamilyName` in `eng/run-package-tests.ps1`,
and none of this applies.

## Layout

| File | What it holds |
|---|---|
| `Program.cs` | The ordered check list — read this first to see what runs and in what order |
| `Checks/RuntimeChecks.cs` | Compilation mode, `XGameRuntimeInitialize`, the `XUser` feature gate |
| `Checks/UserChecks.cs` | The `XUser` pilot: add, read, privileges, handle equality, picture, cancellation, sign-out |
| `Checks/GameUiChecks.cs` | Title-rendered UI: registration and display are separate entry points |
| `Checks/ActivationChecks.cs` | Activation and pending-invite events |
| `Checks/NetworkingChecks.cs` | `XNetworkingQueryConfigurationSetting` |
| `Report.cs` | The JSON report machinery and its schema |
| `HarnessOptions.cs` | Command-line parsing |

## What the harness covers

`runtime.compilation`, `runtime.initialize`, `runtime.feature-gate`,
`users.subscribe-changed`, `users.add`, `user.id`, `user.local-id`, `user.state`, `user.age-group`,
`user.is-guest`, all four `user.gamertag.*` components, `user.privilege.*`, `user.equality`
(`XUserDuplicateHandle` + `XUserCompare` + hashing), `user.gamer-picture`, `user.cancellation`
(`XAsyncCancel`), `user.is-sign-out-present`, `user.sign-out` (opt-in), `users.changed-events`,
`gameui.custom-ui-roundtrip`, `activation.unified-event`, `activation.pending-invite` and
`networking.configuration-setting`.

Three further groups are recorded as explicit skips rather than omitted — `store.gifting-ui`,
`capture.user-record` and `userplatform.prompts` — so the report states that those APIs are
reachable and says why they were not called.

## Troubleshooting

- **`E_GAMERUNTIME_VERSION_MISMATCH` (`0x89240102`) on every step** — the binding resolved no entry
  point. Check that `xgameruntime.thunks.dll` is present in the layout.
- **The report says it was written from `bin\Release\...` instead of the package** — a stale
  unpackaged build was activated. The runner now fails loudly on this; delete `bin` and `obj` under
  this directory and re-run.
- **No report appears** — a packaged title has no console, so a startup crash is silent. Check the
  `Microsoft-Windows-AppModel-Runtime` and `AppXDeployment-Server` event logs.
