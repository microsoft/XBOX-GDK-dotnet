# Releases and GDK updates

`main` targets one validated GDK version/update, not a matrix of historical SDKs.
The checked-out ref's [.github/gdk-version.json](../.github/gdk-version.json) records
its exact SDK inputs. The [supported versions](../README.md#supported-versions)
table describes the current projection.

## Package identity

The package ID is `GDK.Net`. Release versions use
`<edition>.<library-major>.<library-minor>`; each version identifies its own
prebuilt managed binaries and exact `Microsoft.GDK.Windows` **and**
`Microsoft.GDK.Core` dependencies. NuGet constraints use `[version]`, not a bare
minimum version. Do not overwrite a released package or use `+build` metadata to
distinguish packages: NuGet does not treat that metadata as a distinct identity.

Assembly versions use library-version components, not the six-digit GDK edition.
The latter exceeds the size of an assembly-version component. SDK identity belongs
in assembly metadata and the informational version.

The managed assembly supports the target frameworks in the canonical matrix.
Native architecture selection is an application RID concern; it does not by
itself require separate managed package IDs. Restoring the dependencies does not
install or register Gaming Runtime, configure a title, or grant sandbox access.
See [Building](building.md) for the distinction between the library NuGet package
and a packaged game.

## Release checkpoint

Publishing automation and feed uploads are deliberately deferred. Preparing a
package is not evidence that it is available on NuGet, nor does it reserve the
package name. Before publication, an authorized publisher must verify package-ID
or prefix ownership and the destination feed's organizational policy. A public
gallery lookup does not establish those rights.

Before creating a release, review and approve the exact source and artifacts:

1. Build and validate the checked-out pin, including generated bindings, unit and
   native contracts, package consumers, NativeAOT compilation and API reference.
   Record any manual live validation separately; compilation is not execution.
2. Record source revision, SDK archive hashes, dependency locks, toolchain and
   generator inputs, candidate package digests, and sanitized validation evidence.
   Evidence must identify the actual binaries; avoid self-referential hashes.
3. Verify package version, exact dependencies, source pin and artifact hashes agree.
   Verify license, README, XML documentation and public repository metadata, with
   no SDK payloads or private resources embedded in the package.
4. Obtain release approval, create an annotated `v<package-version>` tag, and
   durably retain the exact packages and evidence in the approved release archive.
   Ordinary Actions artifacts expire and are not durable release retention.
5. Verify the retained artifacts and tag readback. Never move or delete existing
   release tags, including tags created before this versioning convention.

Source tags preserve code and documentation, not downloaded dependencies or
hosted runner images. Pinning tools improves reproducibility but cannot freeze
every external input. Retained release binaries are authoritative; a later fix
gets a new version even if rebuilding old source produces different bytes.

## Adopting a newer GDK

Preserve the last validated release's binaries and evidence **before** advancing
`main`. Change the pin, generation inputs, necessary bindings/contracts, library
revision and current API documentation together in a reviewed upgrade PR.
Validate the proposed pin, including manual live evidence for the actual candidate
packages. Missing or stale live evidence blocks the upgrade, not unrelated
automation changes.

An upstream release notification or advisory assessment never changes support,
opens an upgrade PR, authorizes publication, or replaces validation. It is
acceptable to skip upstream releases; there is no requirement to package every
update.

## Manual evidence handoff

PR builds compare their SDK identity with the verified base commit; pushes use
their available before commit. SDK version or archive-hash changes require manual
evidence. Library-revision and release-note changes alone do not. Release-tag
builds require evidence even when the SDK is unchanged. Baseline onboarding is
not an SDK upgrade.

First run all deterministic checks and retain the candidate package and the
`provenance.json` containing `contextSha256`. Failed-run artifacts are unapproved
diagnostics, not releases. Test that exact package in a real packaged title,
including the XUser pilot and PFMP Lobby state-change slice. No CI job or advisory
assessment performs or approves those live checks.

After both checks actually pass, an authorized maintainer supplies this sanitized
attestation through the repository variable `GDK_MANUAL_EVIDENCE`. This is a
**format example, not evidence**:

```json
{
  "schemaVersion": 1,
  "contextSha256": "<candidate provenance contextSha256>",
  "performedAt": "<actual UTC time: YYYY-MM-DDTHH:mm:ssZ>",
  "architecture": "x64",
  "checks": [
    { "id": "xuser-pilot", "result": "passed" },
    { "id": "pfmp-lobby-state-changes", "result": "passed" }
  ]
}
```

The architecture must be `x64` or `arm64`; it records what was actually exercised,
not proof of execution on both. Both check IDs are required exactly once. No
extra fields, identities, comments, URLs, account/title/sandbox identifiers or
secrets are accepted. The timestamp must be a real, non-future UTC time.

CI writes the controlled variable to a temporary file outside the checkout and
removes it after validation. Re-run the same commit. Source, generated inputs,
dependency archives and the actual candidate-package digests must still match;
otherwise test the new candidate and provide new evidence. Clear the variable
after the checkpoint. Never commit an attestation into source or make evidence a
rebuild input.

For local release validation, keep the attestation external or under `artifacts`
and use the orchestrator's `-ManualEvidencePath` with `-BaseCommit <full-SHA>`, or
with `-ReleaseEvidence` for a release. To validate an existing candidate without
rebuilding it:

```powershell
node eng\ci\native-evidence.cjs gate --root . --release `
  --prepared $preparedPath --package $candidatePath --attestation $attestationPath
```

These variables name the actual prepared manifest, candidate package and external
attestation. Validation checks schema and input binding, not signer authenticity.
Trust comes from the separately controlled maintainer handoff. A missing push
baseline or an ordinary manual CI run is not manual release approval.

## Fixing an older release

Create a maintenance branch only when an older release needs a fix:

```powershell
$packageVersion = Read-Host 'Released GDK.Net package version'
$edition = ($packageVersion -split '\.')[0]
git fetch origin --tags
git switch -c "release/$edition" "v$packageVersion"
```

Keep that tag's GDK pin, apply only the required fix, bump the library revision
and run the checks on that ref. Use the normal reviewed PR process and confirm
maintenance-branch protection before merging. Issue a new immutable tag/package;
do not overwrite the old one. Retargeting that branch's SDK is a separate,
deliberate compatibility change.

Historical API documentation lives in each release tag. Older packages can remain
available without receiving ongoing CI, routine rebuilds or automatic backports.
Fixes on `main` do not automatically reach older releases.
