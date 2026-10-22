# Repository layout

Where things live, and which directories are generated rather than authored by hand.


- `src/GDK.Net/`: the idiomatic .NET projection.
- `src/GDK.Net/Interop/`: the raw P/Invoke layer bound to `xgameruntime.thunks.dll`, and (for the
  XBOX Live and PlayFab families) `Microsoft.Xbox.Services.C.Thunks.dll` and the six PlayFab
  extension-library DLLs.
- `tests/GDK.Net.Tests/`: xUnit tests for the error model, header/interop contract, and runtime binding.
- `tests/GDK.Net.LiveHarness/`: GDK title that exercises the `XUser` pilot live and writes the JSON
  report `eng/run-package-tests.ps1` grades. Runs unpackaged (`eng/run-local.ps1`) or packaged.
- `samples/GDK.Net.UserSample/`: readable GDK title demonstrating the same APIs as ordinary
  straight-line code.
- `samples/GDK.Net.PlayFabSample/`: the same, for PlayFab: authentication, the generated service
  layer and Party's frame-loop pump.
- `eng/`: ClangSharp generation tooling (`GDK.Net.rsp`, `generate-interop.ps1`), the PlayFab
  header-driven generator (`playfab/`, driven by `generate-playfab.ps1`), the binding
  rulebook (`interop-conventions.md`), the unreachable-API record (`unexported-apis.md`), the
  coverage check (`api-coverage.ps1`), the API reference generator (`generate-docs.ps1`,
  configured by `docfx.json`) and its per-area layout pass (`api-layout.ps1`, configured by
  `api-areas.json`), the unpackaged run script (`run-local.ps1`) and the package
  build/test scripts (`package.ps1`, `run-package-tests.ps1`).
- `docs/api/`: generated API reference (`eng/generate-docs.ps1`), one folder per area, each with a
  generated `README.md` index.
- `docs/building.md`, `docs/getting-started.md`, `docs/architecture.md`, `docs/gdk-edition.md`:
  authored guides.
- `docs/releasing.md`: package/tag identity, durable retention, upgrades and on-demand maintenance.
- `docs/ci/`: PR routing/gates and staged triage/evaluation/GDK advisory runbooks.
- `.github/gdk-version.json`: one exact SDK pin for the checked-out ref, not a support catalog.
- `artifacts/gdk/`: isolated restored-input/generation/build/package diagnostics for the current
  pin. Generated production PlayFab/Party C# is not a committed per-edition snapshot.
- `eng/ci/`: deterministic scope, aggregate, triage/evaluation, release-watch and assessment helpers.
- `.github/workflows/ci.yml`: scoped static, build/package, NativeAOT, docs and automation checks.
- `.github/workflows/*.md`: agentic workflow sources; their `.lock.yml` files and
  `.github/aw/actions-lock.json` are compiler outputs.
