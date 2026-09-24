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

Imported by both app projects. Copies the two native modules an app needs at runtime into its output
directory:

| File | Source | Required |
| --- | --- | --- |
| `xgameruntime.thunks.dll` | `%GameDKCoreLatest%windows\bin\<arch>\` | Yes: every P/Invoke binds to it, and it is not installed system-wide |

That, plus `MicrosoftGame.config`, is the whole requirement for running unpackaged. The targets file
is a no-op when `%GameDKCoreLatest%` is unset, so hosted CI still compiles both projects.
