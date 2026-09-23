# GDK.Net.UserSample

A small, readable Microsoft GDK title that demonstrates the `GDK.Net` projection. Every file here is
meant to be read: plain straight-line calls, one idea per method, no test scaffolding.

If you want the exhaustive pass/fail run that grades every API and writes a machine-readable report,
that is [`tests/GDK.Net.LiveHarness`](../../tests/GDK.Net.LiveHarness) instead.

## What it shows

| File | What it demonstrates |
|---|---|
| `Program.cs` | Initializing the Gaming Runtime, feature gating, and the error model |
| `Demos/UserDemo.cs` | Signing a user in, reading identity, privileges, gamer picture, cancellation, sign-out |
| `Demos/GameUiDemo.cs` | Rendering a system dialog with the title's own UI instead of the system's |
| `Demos/ActivationDemo.cs` | Being launched from a protocol link, a file association or an invite |
| `Demos/NetworkingDemo.cs` | Reading Gaming Runtime networking configuration |
| `SampleOptions.cs` | Command line and the log file |

## What "idiomatic" means here

The projection's job is that none of the GDK's C conventions reach your game code. Concretely:

| Microsoft GDK (C) | GDK.Net |
|---|---|
| `HRESULT` return, value in an out-parameter | Properties and return values; failures throw `GameRuntimeException` |
| `XAsyncBlock` + completion callback + `…Result` call | `Task` / `await` |
| `XAsyncCancel`, `E_ABORT` | `CancellationToken`, `OperationCanceledException` |
| Two-call size-then-fill buffers (`XUserGetGamertag`) | A `string` |
| `XUserCloseHandle`, `XUserDuplicateHandle` | `IDisposable`, `Duplicate()`, `==` |
| `XUserRegisterForChangeEvent` + a token to unregister | A C# `event`; unsubscribing unregisters |

`Demos/UserDemo.cs` is the shortest path to seeing all six.

## Running it

```powershell
pwsh eng/run-local.ps1 -Project Sample
```

That publishes a self-contained executable to `artifacts/local/GDK.Net.UserSample` and runs it. No
packaging, no registration — just a console app that prints what each API returned.

What it does need is an installed Microsoft GDK (edition `260404`), a dev-unlocked machine and an
account signed in to the Xbox app. And two files have to sit next to the executable, both of which
the build puts there for you (see [`eng/packaging/GdkRedist.targets`](../../eng/packaging/GdkRedist.targets)):

| File | Why |
| --- | --- |
| `xgameruntime.thunks.dll` | The module every P/Invoke binds to. Not installed system-wide, so it is redistributed from the GDK. |
| `MicrosoftGame.config` | Supplies the title identity the Gaming Runtime would otherwise take from package identity. |

Because those are copied by an ordinary build too, `dotnet run` works:

```powershell
dotnet run --project samples/GDK.Net.UserSample -f net8.0
```

Switches: `--allow-ui` (fall back to the account picker when no user is signed in), `--sign-out`
(actually sign the account out at the end), `--out <dir>` (where the log and gamer picture go).
Output is mirrored to `<out>\sample.log`, which defaults to `%LOCALAPPDATA%\GDK.Net.UserSample`.

### Running it packaged

Running unpackaged is the fast loop, but it is not the configuration a title ships in. To exercise
the real one — installed into `WindowsApps`, with the package's own identity, launched by the shell:

```powershell
pwsh eng/package.ps1 -Project Sample -Validate
& "$env:GameDK\bin\wdapp.exe" register artifacts\package\layout
& "$env:GameDK\bin\wdapp.exe" launch 41336MicrosoftATG.GodotTestApp_zjr0dfhgjwvde!Game
```

A packaged title has no attached console, so `sample.log` is the only way to read that run.

## ⚠️ Borrowed package identity

`MicrosoftGame.config` reuses the identity of an existing title
(`41336MicrosoftATG.GodotTestApp`) so the sample has a real, provisioned identity — and
`tests/GDK.Net.LiveHarness` borrows the **same** one. That only matters when packaging: with a
package registered, only one of the two can exist at a time, and registering this sample displaces
both that title and the harness. Run unpackaged and the collision disappears, because nothing is
registered — both apps can run side by side.

See [the harness README](../../tests/GDK.Net.LiveHarness/README.md#-borrowed-package-identity--read-before-running-the-invasive-tiers)
for how to restore the machine, and for how to substitute your own Partner Center identity.

## Building without a GDK

Compiling needs nothing special — the project is part of the solution and builds on any Windows
machine with the .NET SDK. It multi-targets `net8.0;net10.0`, so a manual publish must name one:

```powershell
dotnet build samples/GDK.Net.UserSample
dotnet publish samples/GDK.Net.UserSample -c Release -f net8.0
```

With no GDK installed there is nothing to copy, so the build says so and produces an executable that
cannot run: the first native call fails with `0x89240101`. That is deliberate — hosted CI compiles
this project without a GDK and only needs it to build.
