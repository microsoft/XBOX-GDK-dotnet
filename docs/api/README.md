# API reference

Generated from the XML documentation comments in `src/GDK.Net` by
[`eng/generate-docs.ps1`](../../eng/generate-docs.ps1). **Do not edit these files by hand** --
edit the doc comments and regenerate.

| | |
|---|---|
| GDK edition | `260404` |
| Target framework | `net10.0` |
| Namespaces | 18 |
| Pages | 1173 |

## Scope

Two things are worth knowing about the scope of this reference.

**Internal interop is excluded.** docfx's default API filter emits only public and protected
members, so the `GDK.Net.Interop` layer -- the raw P/Invokes and the blittable native structs --
does not appear. That layer is `internal` and is not part of the supported surface. See
[`../architecture.md`](../architecture.md).

**Generated from `net10.0`, and complete for all targets.** The projection multi-targets
`net8.0`, `net10.0` and `netstandard2.0`. The `#if NET7_0_OR_GREATER` guards choose
`[UnmanagedCallersOnly]` function pointers over `Marshal.GetFunctionPointerForDelegate`, but
everything they switch is `internal`: the three assemblies export the same public surface, so
nothing is missing from this reference. Nothing here is target-specific unless the page says so.

## Namespaces

- [`GDK.Net`](GDK.Net.md)
- [`GDK.Net.Accessibility`](GDK.Net.Accessibility.md)
- [`GDK.Net.Activation`](GDK.Net.Activation.md)
- [`GDK.Net.Capture`](GDK.Net.Capture.md)
- [`GDK.Net.Events`](GDK.Net.Events.md)
- [`GDK.Net.GameSave`](GDK.Net.GameSave.md)
- [`GDK.Net.GameUI`](GDK.Net.GameUI.md)
- [`GDK.Net.Networking`](GDK.Net.Networking.md)
- [`GDK.Net.Package`](GDK.Net.Package.md)
- [`GDK.Net.PlayFab`](GDK.Net.PlayFab.md)
- [`GDK.Net.PlayFab.Multiplayer`](GDK.Net.PlayFab.Multiplayer.md)
- [`GDK.Net.PlayFab.Party`](GDK.Net.PlayFab.Party.md)
- [`GDK.Net.Storage`](GDK.Net.Storage.md)
- [`GDK.Net.Store`](GDK.Net.Store.md)
- [`GDK.Net.Streaming`](GDK.Net.Streaming.md)
- [`GDK.Net.SystemInfo`](GDK.Net.SystemInfo.md)
- [`GDK.Net.Users`](GDK.Net.Users.md)
- [`GDK.Net.XboxLive`](GDK.Net.XboxLive.md)

## Related

- [Getting started](../getting-started.md)
- [Architecture](../architecture.md)
- [Building](../building.md)
- [The pinned GDK edition](../gdk-edition.md)