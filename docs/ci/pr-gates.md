# PR gates

[CI](../../.github/workflows/ci.yml) runs for PRs and pushes to `main` and
`release/**`, release tags matching `v*`, and manual dispatch. There are no
workflow-level path filters: the **PR gates** aggregate must always report.
Each run validates its ref's single SDK pin, not historical SDKs.

## Scope

[eng/ci/pr_gates.cjs](../../eng/ci/pr_gates.cjs) combines all matching paths.
PRs compare their head with the merge base. Pushes compare before/after commits;
a missing push baseline selects full validation. Invalid PR comparisons fail
instead of quietly skipping work. Renames consider both paths and deletions count
as changes. Tags and manual runs select every lane.

| Change | Selected work |
| --- | --- |
| Source, tests, projects, dependencies or GDK pin | Build/package, NativeAOT, API docs and automation |
| Generators and native packaging | Build/package, NativeAOT and API docs |
| Generated API reference or documentation-generation inputs | Build prerequisites and full API-doc comparison |
| Triage, evaluation, watcher or assessment tooling | Deterministic automation checks |
| Ordinary authored documentation and contributor templates | Static checks only |
| CI routing, native-evidence policy, shared CI workflow, or unknown paths | All deterministic lanes |

Routing tests and handwritten-workflow/PowerShell syntax checks always run.
The Windows build job shares restored/generated inputs across build, tests,
packaging, selected NativeAOT compilation and selected API reference comparison.
Dependent NativeAOT and API-doc jobs report that shared job's completion; they
do not claim independent runtime execution.

The aggregate requires scope/lint to succeed, every selected lane to succeed,
and every unselected lane to be skipped. Failures, cancellations, missing results
and unexpected skips fail the aggregate.

## What the checks establish

Unit tests run on the test project's target framework; the library still compiles
all frameworks in the [support matrix](../../README.md#supported-versions).
NativeAOT means real x64 compilation, not just analyzer success. No live harness
is executed on a hosted runner. Native redistributable DLLs must not be mistaken
for managed-assembly fallback.

The full API-doc gate runs `eng/generate-docs.ps1 -Check`; the reflection-based
unit tests are complementary, not a replacement for regeneration.
Automation checks run helper tests, synthetic-fixture validation and strict
compilation with the compiler version pinned in CI. They do not call a model.
Generated workflow locks are compiler outputs, never hand-edited.

Actions artifacts are temporary diagnostics/package handoff, not a release
archive; failed-run candidates are retained as unapproved diagnostics. SDK-pin
changes are checked against the verified PR/push base, and release tags require
package-bound manual evidence. An authorized maintainer supplies sanitized
`GDK_MANUAL_EVIDENCE` outside the source tree; missing or mismatched evidence
fails the gate. See [Releasing](../releasing.md#manual-evidence-handoff) before
tagging or adopting a newer SDK.

## Protection rollout

The required context is **PR gates**. Deploy and pilot CI before adding it to
effective branch protections. Preserve existing review, bypass, force-push and
deletion rules. Maintenance protections and immutable release-tag rules require
authorized settings changes and readback; checked-in YAML alone does not enable
them. Unavailable administrative permissions are a rollout blocker, not a reason
to weaken protection or expand credentials.
