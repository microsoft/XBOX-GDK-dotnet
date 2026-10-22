# The pinned GDK version/update

Each source ref targets **one exact Windows/Core NuGet pair**. `main` follows the
latest SDK adopted after validation, not automatically the latest publication.
The [support matrix](../README.md#supported-versions) describes current support;
[Releasing](releasing.md) covers immutable packages/tags and on-demand maintenance.

## Pin and minimum policy

[.github/gdk-version.json](../.github/gdk-version.json) is the build input:
edition, full Windows/Core package versions, verified SDK archive hashes and
release provenance. MSBuild metadata and generator banners derive from it rather
than independent defaults or a machine's installed edition.

The original baseline is the minimum onboarding policy, not a promise that current
wrappers compile or run against every later SDK. A package depends on its exact
pair, including the full build component. A newer build within the same update
still requires validation. Historical source/docs and binaries belong to their
release refs; ordinary main CI does not build them.

## Restored path rules

Resolve actual package roots from restore outputs. Do not assume a particular
global cache, that transitive consumers get direct-reference path properties, or
that an outer multi-targeting build imports inner-TFM properties.

- Windows headers, import libraries and native modules are under the Windows
  package's `native\<edition>\windows\{include,lib,bin}` tree.
- Core packaging tools are in the separate Core package's `native\bin` tree.
  Do not synthesize a combined installed directory.
- Use the Windows `include\playfab` / `bin\<arch>` stack, not the ABI-incompatible
  `GRDK\ExtensionLibraries` Party stack.
- Never use `GRDK\GameKit`, `%GRDKLatest%` or `%GXDKLatest%`. Do not silently
  fall back to `%GameDKCoreLatest%`; an installed edition can disagree with the pin.

Standard NuGet restore acquires files, not runtime installation, title
registration or sandbox configuration. Keep mirror URLs/authentication in
explicit local configuration, not public sources or package metadata.

## Upgrade procedure

1. Verify the last validated release has an immutable source tag and durably
   retained packages/evidence. An expiring Actions artifact or source-only tag
   does not satisfy this prerequisite.
2. Propose the new exact pair and verified archive hashes in an upgrade PR.
   Change the library revision as needed; do not retarget an older maintenance
   branch incidentally.
3. Restore and generate clean production inputs through `eng/build.ps1`. Extract
   exports from the selected binaries; do not reuse committed/test-only export
   fixtures with `-SkipExports` for a different SDK.
4. Inspect generator/model differences and actual compiled native imports,
   signatures, enums, layouts and calling conventions against the selected
   headers and both native architectures. Preserve intentional omission tests.
   Update an expectation only for an evidenced SDK difference, never to hide a
   failed check.
5. Validate all library TFMs, unit/native contracts, package contents and clean
   direct/transitive consumers, exact-version conflicts, native staging, real
   x64 NativeAOT compilation and full API-doc comparison. Cross-compilation is
   not runtime execution. Regenerate current API docs normally.
6. Perform manual live validation using the actual candidate packages. Bind
   sanitized evidence to source/generator/dependency inputs and package digests.
   Missing or stale evidence blocks adoption; rebuilt/different packages require
   fresh evidence, not an edited success claim.
7. Update the canonical support matrix and review compatibility/release notes.
   Merge only after required deterministic and manual evidence is accepted.
   Tag/retain the resulting release through its separate approval checkpoint;
   publishing remains a separate action.

ClangSharp generation remains verification scratch: do not compile its duplicate
declarations beside hand-written core/XSAPI interop. Production PlayFab/Party
changes belong in emitters, not generated C#.

## Export gaps are not text-search counts

`eng/unexported-apis.md` records declarations that are absent from redistributable
thunks or compiled out. Re-derive changes using actual exports and native header
preprocessing with the platform's real defines, including `_GAMING_DESKTOP`.
Naive header searches also count non-GDK and internal-only XSAPI declarations.
Account for export aliases when interpreting `dumpbin` output.

`eng/api-coverage.ps1` is useful coverage reporting, not proof of signatures or
ABI compatibility. Native probes must state which contracts remain unverified.
Do not initialize GDK/PlayFab merely to inspect metadata or layouts.

Historical repros such as `eng/repro/pfmp-uninitialize-hang` intentionally retain
their original pin until explicitly revalidated; do not sweep their version
strings as part of a current-main upgrade.

## Related guides

- [Building](building.md): source acquisition, generation, checks and local execution.
- [Architecture](architecture.md): hand-written interop, generation and AOT constraints.
- [Releasing](releasing.md): baseline retention, immutable tags and older-release fixes.
- [GDK release watch](ci/gdk-release-watch.md): advisory discovery, not automatic adoption.
