# NativeAOT

How this repository guarantees the projection is AOT-safe, and how to AOT-publish a title of your
own. For the design rationale behind the contract, see
[`architecture.md`](architecture.md#the-aot-contract).


**Xbox consoles do not permit JIT compilation, so a .NET title that ships to console must be
NativeAOT-compiled.** `GDK.Net` is AOT-safe on `net8.0` and `net10.0`, and the repository enforces
that at three levels rather than asserting it once.

**AOT is opt-in, not required.** Nothing here forces it on you: `dotnet build`, `dotnet test`,
`dotnet pack` and an ordinary `dotnet publish` are unchanged and JIT-compiled, and the packaged
harness defaults to a self-contained `net8.0` JIT layout. `eng/package.ps1 -Aot` and the CI publish
job are the only AOT paths. Both modes are verified against a real signed-in account in a real
package — the harness's `runtime.compilation` step reports `Running JIT-compiled (X64)` or
`Running NativeAOT-compiled (X64)`, and both give the same 23 passed / 1 skipped. Consume this
library however suits your title; AOT is what console requires, not what this repo imposes.

**1. Analyzers, on every build.** `GDK.Net.csproj`, the harness and the sample set `EnableAotAnalyzer`,
`EnableTrimAnalyzer` and `EnableSingleFileAnalyzer`, so any construct requiring dynamic code or
unreferenced metadata is a build error under the repo-wide `TreatWarningsAsErrors`. The assembly
also carries `[AssemblyMetadata("IsTrimmable", "True")]`, which lets the trimmer prune it inside a
consuming title instead of rooting it wholesale.

The design that makes this pass is not incidental: every native entry point is a
`[LibraryImport]` — source-generated marshalling, so no marshalling stubs are produced at run time
— and every callback the runtime invokes is a `static` method with
`[UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]` taken as a
`delegate* unmanaged[Stdcall]<...>`. Nothing calls `Marshal.GetFunctionPointerForDelegate` on
`net8.0`+; the delegate-based path exists only inside `netstandard2.0` `#if` branches, which no AOT
publish ever compiles. `tests/GDK.Net.Tests/AotCompatibilityTests.cs` locks all of this in place, so
a regression fails the ordinary test run rather than surfacing on console months later.

**2. A real ILC compilation, in CI.** Analyzers only see what is annotated; ILC sees the whole
closure. The `Verify NativeAOT compilation` job AOT-publishes the harness — which transitively
compiles `GDK.Net` — and asserts the output is a native executable with no managed assemblies
beside it, since a silent fallback to an IL publish would still produce an `.exe`.

**3. A live packaged run.** Compiling is not running. `eng/run-package-tests.ps1 -Aot` builds the
AOT harness into a real GDK package and executes it; its `runtime.compilation` step reports
`RuntimeFeature.IsDynamicCodeSupported` so the log proves which mode actually ran:

```powershell
pwsh -Command "& .\eng\run-package-tests.ps1 -Tier Layout,Register -Aot"
```

This is the check that matters — it exercises the reverse-P/Invoke completion callbacks, the
`XAsyncBlock` → `Task` engine and cancellation under a runtime with no JIT at all. It needs an
installed GDK and a dev-unlocked machine, so it is manual.

To AOT-publish a title of your own:

```powershell
dotnet publish -c Release -f net10.0 -r win-x64 --self-contained -p:PublishAot=true
```

Two environment notes:

- **`net10.0` (or `net9.0`), not `net8.0`.** The NativeAOT runtime pack
  (`Microsoft.NETCore.App.Runtime.NativeAOT.win-x64`) was first published for 9.0, so a `net8.0` AOT
  publish cannot be restored with a 10.x SDK. `GDK.Net` still *targets* `net8.0` for
  framework-dependent consumers; only the AOT publish needs the newer TFM.
- **`vswhere.exe` must be on `PATH`.** ILC resolves `link.exe` by shelling out to `vswhere` and
  treating the command's combined output as the path, so when it is missing the *error text*
  becomes the linker and you get an opaque `MSB3073`. Prepend
  `%ProgramFiles(x86)%\Microsoft Visual Studio\Installer`; a `vcvars64` shell is **not** sufficient.
  `eng/package.ps1 -Aot` and CI both do this for you.

If your build machine cannot reach nuget.org, enabling the analyzers surfaces as `NU1100`/`NU1603`
for `Microsoft.NET.ILLink.Tasks` rather than as a network error. Build with
`-p:GdkNetEnableAotAnalyzers=false` to opt out; the library source is unchanged either way, you
simply lose the compile-time enforcement.


