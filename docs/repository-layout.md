# Repository layout

Where things live, and which directories are generated or vendored rather than authored by hand.


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

