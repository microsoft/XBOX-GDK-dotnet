# GDK interop generation

This folder contains the checked-in tooling for generating the raw `GDK.Net.Interop` layer from the Microsoft GDK C/C++ headers. It does not hand-author or check in generated bindings.

## Usage

```powershell
.\eng\generate-interop.ps1 -Clean
```

The script resolves `%GameDKCoreLatest%`, discovers the latest installed Windows SDK and MSVC include directories, restores the local `ClangSharpPInvokeGenerator` tool, expands `eng\GDK.Net.rsp` into `eng\obj\GDK.Net.effective.rsp`, and writes generated C# files to `eng\Generated\`.

Use `-WhatIf` for a dry run, or `-NoInstall` to skip local tool restore/install.

## Known-good version

- `ClangSharpPInvokeGenerator` NuGet package: `21.1.8.4`
- Tool output reports: `ClangSharp P/Invoke Binding Generator version 21.1.8`
- Validated against GDK edition `260404` from `%GameDKCoreLatest%\windows\include`

When the GDK edition is re-pinned, update `%GameDKCoreLatest%`, rerun the script, review the diff in `eng\Generated\`, and only then decide whether generated output should be moved into the source tree.

## Relationship to the checked-in interop layer

`src/GDK.Net/Interop/` is currently **hand-authored** and transcribed directly from the headers; its
signatures were cross-checked against this generator's output. The generator is kept here as the
verification and refresh mechanism for that hand-authored layer.

Generated output is written to the git-ignored `eng\Generated\` and is **not compiled into the
project**. Note that it declares the same native type names (`XAsyncBlock`, `XUserAddOptions`, …) in
the same `GDK.Net.Interop` namespace, so it cannot simply be added to `GDK.Net.csproj` alongside the
hand-authored files: adopting it means replacing them, not supplementing them.

## Current validation

Generation succeeds on the reference Windows machine. The latest run produced 43 C# files in `eng\Generated\`; spot checks found `XUserAddAsync`, `XUserGetGamertag`, `XGameRuntimeInitialize`, and `XTaskQueueCreate`. Deprecated `XUserGetMsaTokenSilently*` members are excluded.
