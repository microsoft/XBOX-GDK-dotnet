# GDK interop generation

This folder contains the checked-in tooling for generating the raw `GDK.Net.Interop` layer from the Microsoft GDK C/C++ headers. It does not hand-author or check in generated bindings.

## Usage

For ordinary source builds, use the current-pin orchestrator:

```powershell
pwsh eng/build.ps1 -Configuration Release -Test -Pack
```

It restores exact SDK inputs and prepares production PlayFab/Party output before
building. See [Building](../docs/building.md) and [Releasing](../docs/releasing.md).
The script below is separate core-interop verification, not a replacement
production build.

```powershell
.\eng\generate-interop.ps1 -Clean
```

Use explicit selected SDK inputs and isolated scratch paths. Native compiler includes still
come from the installed MSVC/Windows SDK toolchain. The local tool manifest pins ClangSharp;
verification output must not be compiled alongside the hand-written interop.

Use `-WhatIf` for a dry run, or `-NoInstall` to skip local tool restore/install.

## Pinned inputs

The current SDK pin is [.github/gdk-version.json](../.github/gdk-version.json);
tool versions live in [the tool manifest](../.config/dotnet-tools.json) and the
root toolchain version files. See the [canonical support matrix](../README.md#supported-versions)
and [upgrade procedure](../docs/gdk-edition.md), rather than maintaining a second version list here.

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
