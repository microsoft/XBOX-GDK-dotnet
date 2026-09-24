# XBOX GDK.NET API reference

Every public type and member of the projection, generated from the XML documentation comments in
`src/GDK.Net` by [`eng/generate-docs.ps1`](../../eng/generate-docs.ps1). **Do not edit these files by
hand.** Edit the doc comments and regenerate.

| | |
|---|---|
| GDK edition | `260404` |
| Target framework | `net10.0` |
| Namespaces | 18 |
| Pages | 1173 |

## Layout

Each namespace has its own folder, and each folder has a `README.md` describing every API in it
and linking to the matching GDK reference on Microsoft Learn. The pages are split this way because
GitHub stops rendering a directory listing past 1,000 entries, and the projection has more pages
than that; a single flat folder would hide the overflow from anyone browsing the repository.

## Areas

| Area | Namespace | Covers | APIs |
|---|---|---|---|
| [Core runtime](Core/README.md) | `GDK.Net` | Initialising and shutting down the Gaming Runtime, querying which runtime features are available, and the error model every other area reports through. | 8 |
| [Accessibility](Accessibility/README.md) | `GDK.Net.Accessibility` | Player accessibility preferences: closed-caption styling, high-contrast mode, speech synthesis and speech-to-text. | 11 |
| [Activation](Activation/README.md) | `GDK.Net.Activation` | How a title is launched: protocol and URI activation, and the invite and activation arguments the shell passes in. | 3 |
| [Capture](Capture/README.md) | `GDK.Net.Capture` | Screenshots, game clips and broadcast state, including the developer-only diagnostic capture paths. | 14 |
| [Game events](Events/README.md) | `GDK.Net.Events` | Writing title-defined telemetry events declared in the title's game configuration. | 1 |
| [Game save](GameSave/README.md) | `GDK.Net.GameSave` | Cloud-synchronised save containers and blobs, and the file-system view over them. | 8 |
| [Game UI](GameUI/README.md) | `GDK.Net.GameUI` | System-provided dialogs: the account picker, error and message dialogs, text entry, gamertag pickers and the web authentication flow. | 24 |
| [Networking](Networking/README.md) | `GDK.Net.Networking` | Connectivity hints and the network security configuration a title must honour before it opens a socket. | 12 |
| [Package](Package/README.md) | `GDK.Net.Package` | Package and chunk enumeration, mounting, install monitoring and licence checks for downloadable content. | 16 |
| [PlayFab core and services](PlayFab/README.md) | `GDK.Net.PlayFab` | PlayFab initialisation, the entity handle every service call authenticates with, and the generated service layer (economy, data, profiles, statistics and the rest). | 662 |
| [PlayFab Multiplayer](PlayFab/Multiplayer/README.md) | `GDK.Net.PlayFab.Multiplayer` | Lobbies and matchmaking, drained through the PlayFab Multiplayer state-change pump. | 37 |
| [PlayFab Party](PlayFab/Party/README.md) | `GDK.Net.PlayFab.Party` | Realtime networking, voice and text transport, and the XBOX Live identity bridge that authorises Party chat controls. | 146 |
| [Storage](Storage/README.md) | `GDK.Net.Storage` | The persistent local storage folder a title may write to outside its package. | 2 |
| [Store](Store/README.md) | `GDK.Net.Store` | Commerce: licences, durables, add-ons, in-game purchase, catalogue queries and the licence-lost notification. | 20 |
| [Game streaming](Streaming/README.md) | `GDK.Net.Streaming` | Detecting a cloud-streaming client and reading its properties, so a title can adapt to the streamed presentation. | 16 |
| [System information](SystemInfo/README.md) | `GDK.Net.SystemInfo` | Console, operating system and application information, the analytics identifier, display mode and HDR capability. | 17 |
| [Users](Users/README.md) | `GDK.Net.Users` | Sign-in and identity: adding users, privileges, tokens, and the user-change and sign-out deferral events that gate every user-scoped API. | 26 |
| [XBOX Live services](XboxLive/README.md) | `GDK.Net.XboxLive` | XBOX Live through XSAPI: context and initialisation, profile, social, presence, achievements, leaderboards, statistics, title storage, privacy and multiplayer activity. | 132 |

## Scope

Three things are worth knowing about the scope of this reference.

**Internal interop is excluded.** docfx's default API filter emits only public and protected
members, so the `GDK.Net.Interop` layer, which holds the raw P/Invokes and the blittable native
structs, does not appear. That layer is `internal` and is not part of the supported surface. See
[`../architecture.md`](../architecture.md).

**Generated from `net10.0`, and complete for all targets.** The projection multi-targets `net8.0`,
`net10.0` and `netstandard2.0`. The `#if NET7_0_OR_GREATER` guards choose
`[UnmanagedCallersOnly]` function pointers over `Marshal.GetFunctionPointerForDelegate`, but
everything they switch is `internal`: the three assemblies export the same public surface, so
nothing is missing from this reference. Nothing here is target-specific unless the page says so.

## Related

- [Getting started](../getting-started.md)
- [Architecture](../architecture.md)
- [Building](../building.md)
- [The pinned GDK edition](../gdk-edition.md)
