# GDK.Net

[![License: MIT][badge-license]][link-license]
[![.NET 8 and 10][badge-dotnet]][link-dotnet]
[![netstandard2.0][badge-netstandard]][link-netstandard]
[![Microsoft GDK 260404][badge-gdk]][link-gdk]
[![XBOX Services supported][badge-xbl]][link-xbl]
[![PlayFab supported][badge-playfab]][link-playfab]
[![NativeAOT ready][badge-aot]][link-aot]
[![Windows only][badge-windows]][link-windows]
[![Visit our Blog][badge-blog]][link-blog]
[![Join us on Discord][badge-discord]][link-discord]
[![PRs welcome][badge-prs]][link-prs]

**Write your XBOX game in C#, not in P/Invoke.** GDK.Net is the .NET projection of the Microsoft **GDK** flat C API, exposing the Gaming Runtime, **XBOX Services** and **PlayFab** as ordinary modern C# - `Task`, `IDisposable`, events, exceptions and `[Flags]` enums instead of `HRESULT`s, raw handles, `XAsyncBlock`s and two-call size buffers.

> [!IMPORTANT]
> **This is source, not a product.** The projection is MIT-licensed, but the Microsoft GDK and the PlayFab extension libraries it binds to are installed and licensed separately, consistent with other XBOX samples. There is no specified update cadence. We will watch the repo, monitor issues, and iterate where it makes sense. We are keen to hear your feedback and to see community PRs.
>
> **Windows-only, and pinned to one GDK edition.** Everything here is built and verified against Microsoft GDK edition `260404`. Building needs no GDK install at all; *running* against the Gaming Runtime needs the GDK, a dev-unlocked machine and a signed-in account. CI can only build, test and verify NativeAOT compilation - behaviour is validated by hand in a packaged title against a real sandbox.

## Quick start

| I want to... | Start here |
|---|---|
| Add the projection to an existing .NET game | [**Getting started**](docs/getting-started.md) |
| Clone, build and run the samples from source | [**Building**](docs/building.md) |
| See exactly what is projected, and what is not | [**Projection status**](docs/status.md) |
| Ship to console, where JIT is not permitted | [**NativeAOT**](docs/native-aot.md) |
| Draw the runtime's dialogs myself | [**Title-implemented UI**](docs/custom-game-ui.md) |
| Move to a newer GDK edition | [**The pinned GDK edition**](docs/gdk-edition.md) |
| Browse the full documentation tree | [**Documentation index**](docs/README.md) |

Building needs nothing but the .NET SDK:

```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release --no-build
```

To run against the Gaming Runtime you need an installed GDK, a dev-unlocked machine and a signed-in XBOX account - but **not** a package:

```powershell
pwsh eng/run-local.ps1 -Project Sample    # readable demo, self-contained, unpackaged
pwsh eng/run-local.ps1                    # the live harness, same
pwsh eng/run-package-tests.ps1            # the packaged path, which is what a title ships as
```

## Overview

GDK.Net projects the GDK surface a .NET title actually needs, generated and verified against the real headers of edition `260404` rather than transcribed by hand:

- **Gaming Runtime** - users and sign-in, game invites and protocol activation, storage, packages and DLC, game saves, game UI, networking, capture and broadcast, the Store, game streaming, accessibility, speech, and system metadata
- **XBOX Services** - profile, achievements, social, presence, privacy, leaderboards, real-time activity, user and title-managed statistics, title storage, string verification, events, multiplayer activity, plus the social and achievements manager layers
- **PlayFab** - Core and the full generated Services surface, Game Saves, Multiplayer (Lobby and Matchmaking), and Party including PartyXboxLive
- **NativeAOT** - AOT-safe on `net8.0` and `net10.0`, enforced by analyzers, warnings-as-errors and a real ILC compilation in CI, because consoles do not permit JIT

Coverage is measured, not asserted: **349 of 355** Gaming Runtime exports and **1,006 of 1,008** PlayFab exports are bound, the remainder being deliberately out of scope. Run [`eng/api-coverage.ps1`](eng/api-coverage.ps1) for the current tally, and see [**Projection status**](docs/status.md) for what is excluded and why.

The projection ships **no native code of its own**. Every P/Invoke binds to redistributable modules the GDK already installs.

## Modules

The core projection is one assembly. XBOX Services and PlayFab are separate native modules, so they are opt-in at packaging time - a title that does not use them does not carry them.

| Opt-in property | Native modules | Managed surface |
|---|---|---|
| *(always)* | `xgameruntime.thunks.dll` | `GDK.Net` - the Gaming Runtime: `GameRuntime`, `UserManager`, `GamePackage`, `GameSaveProvider`, `StoreContext`, `NetworkingManager`, and the rest |
| `<GdkNetIncludeXboxLive>` | `Microsoft.Xbox.Services.C.Thunks.dll`, `libHttpClient.dll` | `GameRuntime.XboxLive` - profile, achievements, social, presence, privacy, leaderboards, stats, title storage, and the manager layers |
| `<GdkNetIncludePlayFab>` | `PlayFabCore.dll`, `PlayFabServices.dll`, `PlayFabGameSave.dll`, `libHttpClient.dll` | `GDK.Net.PlayFab` - `PlayFabRuntime`, the 20 generated service classes, `PlayFabGameSaveFiles` |
| `<GdkNetIncludePlayFabMultiplayer>` | `PlayFabMultiplayer.dll` | `GDK.Net.PlayFab.Multiplayer` - lobbies and matchmaking |
| `<GdkNetIncludePlayFabParty>` | `Party.dll`, `PartyXboxLive.dll` | `GDK.Net.PlayFab.Party` - voice, text chat and network transport |

The last two PlayFab groups import `PlayFabCore.dll`, so asking for either turns `<GdkNetIncludePlayFab>` on as well. [`eng/packaging/GdkRedist.targets`](eng/packaging/GdkRedist.targets) copies the modules you asked for next to the executable on every build.

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


## Documentation

Full documentation lives in [`docs/`](docs/README.md).

Start here:

- [**Documentation index**](docs/README.md) - the full doc tree
- [**Getting started**](docs/getting-started.md) - the idioms the projection uses, initialising `GameRuntime`, signing in a user
- [**Building**](docs/building.md) - prerequisites, build and test, running unpackaged, packaging, the generation tooling
- [**Architecture**](docs/architecture.md) - the two layers, the native modules, the three target frameworks, the AOT contract, threading
- [**Projection status**](docs/status.md) - what is projected, what is out of scope, and the current coverage numbers

Reference:

- [**API reference**](docs/api/) - every public type and member, generated from the XML doc comments
- [**Repository layout**](docs/repository-layout.md) - where things live, and what is generated or vendored
- [**The pinned GDK edition**](docs/gdk-edition.md) - minimum version and the full re-pin procedure
- [**`docs/plan.md`**](docs/plan.md) - the authoritative implementation specification (vendored)
- [**`docs/reference/`**](docs/reference/) - shared language-neutral references (vendored)

The `docs/` directory holds both authored guides and a vendored, one-way copy of the shared specification from the `gdk-projections-plans` meta repo. `docs/plan.md` and `docs/reference/` are vendored — do not edit them here; update the meta repo sources and re-copy them instead.

## Additional Documentation

- [**Microsoft GDK**](https://github.com/microsoft/GDK) - Microsoft GDK product details
- [**PlayFab Unified SDK**](https://learn.microsoft.com/en-us/gaming/playfab/sdks/unified-sdk/overview) - PlayFab Unified SDK product details
- [**XBOX Godot Sample**](https://github.com/microsoft/XBOX-Godot-Sample) - the same GDK surface bound into Godot 4, usable from GDScript and C#
- [**Godot C# Essentials**](https://github.com/microsoft/godot-csharp-essentials) - learning content provided by Microsoft on using Godot with C#

## Support and contributing

- [**Open an issue**](https://github.com/microsoft/XBOX-GDK-dotnet/issues/new) - bugs and feature requests
- [`SUPPORT.md`](SUPPORT.md) - how to file issues, and what to include
- [`CONTRIBUTING.md`](CONTRIBUTING.md) - the CLA, and what a change here has to satisfy
- [`SECURITY.md`](SECURITY.md) - security vulnerability reporting (MSRC; please do **not** file security issues via GitHub)
- [`CODE_OF_CONDUCT.md`](CODE_OF_CONDUCT.md) - Microsoft Open Source Code of Conduct

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of
Microsoft trademarks or logos is subject to and must follow
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion
or imply Microsoft sponsorship. Any use of third-party trademarks or logos are subject to those
third-party's policies.

<!-- Badge image and link definitions -->
[badge-license]: https://img.shields.io/badge/License-MIT-107C10
[link-license]: LICENSE
[badge-dotnet]: https://img.shields.io/badge/.NET-8%20%7C%2010-512BD4?logo=dotnet&logoColor=white
[link-dotnet]: docs/architecture.md#target-frameworks
[badge-netstandard]: https://img.shields.io/badge/netstandard-2.0-512BD4
[link-netstandard]: docs/architecture.md#target-frameworks
[badge-gdk]: https://img.shields.io/badge/Microsoft%20GDK-260404-107C10
[link-gdk]: docs/gdk-edition.md
[badge-xbl]: https://img.shields.io/badge/XBOX%20Services-%E2%9C%93-107C10
[link-xbl]: docs/status.md
[badge-playfab]: https://img.shields.io/badge/PlayFab-%E2%9C%93-107C10
[link-playfab]: docs/status.md
[badge-aot]: https://img.shields.io/badge/NativeAOT-ready-107C10
[link-aot]: docs/native-aot.md
[badge-windows]: https://img.shields.io/badge/Windows-only-0078D4?logo=windows&logoColor=white
[link-windows]: #supported-versions
[badge-blog]: https://img.shields.io/badge/Visit%20our-Blog-FFA500?logo=rss&logoColor=white
[link-blog]: https://developer.microsoft.com/en-us/games/articles/
[badge-discord]: https://img.shields.io/badge/Join%20us%20on-Discord-7289DA?logo=discord&logoColor=white
[link-discord]: https://aka.ms/msftgamedevdiscord
[badge-prs]: https://img.shields.io/badge/PRs-welcome-d6336c
[link-prs]: CONTRIBUTING.md
