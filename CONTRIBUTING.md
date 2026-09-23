# Contributing to GDK.Net

This project welcomes contributions and suggestions. Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the
instructions provided by the bot. You will only need to do this once across all repos using our CLA.

## Code of Conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/)
or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or
comments.

## Reporting security issues

Please do not report security vulnerabilities through public GitHub issues. See [SECURITY.md](SECURITY.md).

## Building and testing

No GDK installation is required to build or to run the unit tests:

```powershell
dotnet restore
dotnet build -c Release
dotnet test -c Release --no-build
```

[`docs/building.md`](docs/building.md) covers the prerequisites, running against the Gaming Runtime,
packaging, and the generation tooling. [`docs/architecture.md`](docs/architecture.md) explains the
layering and the NativeAOT contract.

## What a change has to satisfy

**Warnings are errors.** `TreatWarningsAsErrors` is on repo-wide.

**The projection is idiomatic, not mechanical.** Callers must never see an `HRESULT`, a raw handle,
an `XAsyncBlock`, a registration token, or a two-call size buffer. Cancellation (`E_ABORT`) routes
to `OperationCanceledException`, never a generic error, while the numeric HRESULT is preserved for
diagnostics. See [`docs/plan.md`](docs/plan.md) §4.

**Every public API carries an XML doc comment.** `src/GDK.Net/GDK.Net.csproj` sets
`GenerateDocumentationFile`, so a new public type or member without one fails the build (CS1591).
This is deliberate — it is what keeps the API reference complete. After any public API change,
regenerate and commit the reference:

```powershell
dotnet tool restore
pwsh eng/generate-docs.ps1
```

`ApiReferenceDriftTests` fails when a type has no page or a page outlives its type.

**Do not hand-edit generated output.** [`docs/api/`](docs/api/) is generated from the doc comments.
The PlayFab and Party projections are generated too — fix the emitter under `eng/playfab/`, never
its output.

**Do not edit the vendored documentation here.** `docs/plan.md` and `docs/reference/` are one-way
copies of the shared specification from the `gdk-projections-plans` meta repo. Fix the source there
and re-copy. Everything else under `docs/` is authored in this repository and is canonical here.

**Keep the support matrix honest.** Any change to the supported runtime, architecture, engine, or
toolchain matrix must update the tables in [`README.md`](README.md) in the same change.

**The GDK edition is pinned** to `260404`. See [`docs/gdk-edition.md`](docs/gdk-edition.md) for the
re-pin procedure.

## Testing is live

CI builds, runs the unit tests, and verifies that NativeAOT compilation succeeds. It cannot validate
behaviour: hosted runners have no GDK and no signed-in account. The pilot is validated in a packaged
GDK app against a real sandbox on a dev-unlocked machine:

```powershell
pwsh eng/run-package-tests.ps1 -Tier Layout,Register -Aot
```

If your change affects runtime behaviour, please say in the pull request whether you were able to
run this, and on what. See [`docs/reference/testing.md`](docs/reference/testing.md).
