# Advisory GDK release monitoring

The [watcher](../../.github/workflows/gdk-release-watch.yml) compares public GDK
release and NuGet metadata with trusted `main`'s exact
[SDK pin](../../.github/gdk-version.json). The
[assessment workflow](../../.github/workflows/gdk-release-assess.md) prepares an
advisory upgrade brief. Neither changes support, modifies source, creates an
upgrade PR, or publishes packages.

## Discovery and queue policy

Compare numeric full package versions, not publication dates or just six-digit
editions. New builds in the same update can be candidates; later servicing
publications from an older release family do not become automatic main upgrades.
Check matching Windows/Core availability and correlate release notes. Missing
packages, lagging notes or ambiguous correlation are evidence gaps, not successful
onboarding.

Tracking records correlate package and release identities so packages discovered
before release notes do not create duplicate issues. Closed tracking issues opt
out. Prefer the newest eligible pending candidate, with at most one automatic
assessment dispatch per watcher run. As main advances, obsolete candidates leave
the automatic queue without automatically closing historical issues.

Explicit `release_tag`, `retry` and `preview` inputs support controlled operation.
Older-release maintenance is deliberate work outside automatic latest-main
discovery. Attempt IDs and snapshot checks prevent stale runs from publishing or
settling a newer attempt; dispatch and terminal failures remain visible.

## Staging and assessment

Only trusted target `main` may write or dispatch. `GDK_WATCH_MODE` and
`GDK_ASSESS_MODE` each require the exact value `post`; absent/malformed values are
staged. Explicit preview wins over posting mode. Forks, branches and tags do not
gain write side effects. A staged assessment can run before a ledger attempt
exists; posting requires the current valid attempt.

The watcher itself performs no model calls. AI assessment, including preview,
also requires the [compiled-budget preflight](issue-triage.md#budget-preflight).
The currently pinned detector runtime does not demonstrably enforce the approved
turn limit, so AI activation is blocked before any model executes. Do not remove
that guard or disable threat detection to enable assessment.

An assessment considers binding/emitter/native changes, exact dependencies,
current API docs, package consumers, AOT, manual evidence and preservation of the
last release. A `tests_only` verdict means no additional functional changes were
identified beyond mandatory onboarding/generation/validation, not that no
repository changes are required. Missing evidence must reduce confidence.

Strict deterministic helper/fixture/compiler checks run without model calls.
Approved billing/policy and human-reviewed staged pilots are required before
posting is enabled. No live sign-in, GDK installation/registration, publishing
credentials or sandbox secrets belong in these workflows.

Rollback is to set either mode away from `post` or disable the affected workflow.
Keep existing tracking records and protections. Record settings readback and run
links when activation actually occurs. See [Releasing](../releasing.md) for the
separate package/tag/retention checkpoint and SDK-upgrade requirements.
