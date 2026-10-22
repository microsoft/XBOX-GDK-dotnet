# Maintainer issue triage

The [triage workflow source](../../.github/workflows/issue-triage.md) adapts issue
reports to the .NET projection. An authorized maintainer requests triage by posting
a new comment containing exactly:

```text
/triage
```

This is static, source-grounded analysis, not a reproduced bug. Include the exact
GDK.Net package/tag or source SHA, resolved SDK dependencies, framework,
architecture, host/AOT mode and a sanitized reproduction. Current-main source is
not proof of behavior in an older release.

## Safeguards and staged operation

Only the trusted target repository's main workflow may publish. `TRIAGE_MODE`
must be exactly `post`; an absent or misspelled value leaves publication staged.
Live issue/comment state and maintainer permission are checked again before
publication. Closed issues, PRs, bots, access requests and security reports are
excluded. Vulnerabilities belong in [SECURITY.md](../../SECURITY.md), not public
triage.

The agent has read-only analysis access; publication is a separate,
issues-write job. Reports are validated against the prepared issue snapshot and
bounded public-source citations, escaped before rendering, and deduplicated using
bot-owned markers. Unexpected API failures remain failures. Do not add broader
tokens or assume a declared tool exclusion removes every built-in tool: inspect
the generated lock's actual authentication, permissions and tool exposure.

Do not paste tokens, private feed URLs, account identifiers or title/sandbox
configuration into issues, prompts, reports or fixtures.

## Budget preflight

Before any model request, activation inspects the compiled agent and detector
proxy limits. The approved limits are 200 AI credits and 30 turns per agent run;
the request cap is also constrained so a missing turn-cap implementation cannot
silently allow a longer run. Credit limits are usage-based and can overshoot on
an already in-flight response.

The compiler currently pinned in [CI](../../.github/workflows/ci.yml) emits a
larger detector request cap, and its detector CLI path does not demonstrably
enforce the configured turn flag. **All three AI workflows therefore fail closed
before model execution, including staged evaluations and assessments.** Posting
variables cannot bypass this guard.

Activation requires a compiler/runtime combination that actually enforces the
limits, followed by deterministic regeneration and an audit of the generated
configuration. Do not disable threat detection, remove the guard, or raise the
approved budget to make a pilot run.

## Evaluations and activation

[Synthetic cases and rubric](../../tests/evals/issue-triage/) cover code behavior,
missing version/reproduction information, and documentation/feature questions at
explicit public source revisions. Gold expectations are withheld from the model.
Fixture validation and helper tests do not invoke models.

A valid report awaiting human review is **unscored**, not a passing evaluation.
Distinguish invalid/model-failed runs from valid-unscored and human-scored runs.
Before enabling posting, verify Actions/Copilot policy and billing, run approved
staged pilots within the workflow's credit/turn limits, and human-score the
results. Missing policy/billing evidence blocks pilots and activation.
The budget preflight above must pass first.

To roll back, set `TRIAGE_MODE` to a value other than `post`, or disable the
affected workflow. Do not remove pre-existing branch protections. Record actual
pilot links and settings readback rather than treating committed sources as a
deployed service.
