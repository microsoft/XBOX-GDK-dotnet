---
name: Issue Triage Eval
description: >-
  Runs the issue-triage skill against one pinned eval case and uploads the report
  as an artifact for human scoring. Never writes to issues. See
  tests/evals/issue-triage/README.md.
on:
  workflow_dispatch:
    inputs:
      case:
        description: Eval case to run (tests/evals/issue-triage/cases).
        required: true
        type: choice
        options:
          - cancellation-eabort
          - historical-package-mismatch
          - native-aot-question
  reaction: none
  status-comment: false
  permissions:
    contents: read
  steps:
    - name: Check out budget verifier
      uses: actions/checkout@93cb6efe18208431cddfb8368fd83d5badbf9bfd # v5.0.1
      with:
        ref: ${{ github.sha }}
        persist-credentials: false
        sparse-checkout: |
          eng/ci
          .github/workflows
        sparse-checkout-cone-mode: true
    - name: Refuse alternate GitHub credentials
      id: auth
      uses: actions/github-script@3a2844b7e9c422d3c10d287c895573f7108da1b3 # v9.0.0
      env:
        HAS_ALTERNATE_GITHUB_AUTH: ${{ secrets.GH_AW_GITHUB_MCP_SERVER_TOKEN != '' || secrets.GH_AW_GITHUB_TOKEN != '' }}
      with:
        script: |
          if (process.env.HAS_ALTERNATE_GITHUB_AUTH !== 'false') {
            throw new Error('Alternate gh-aw GitHub credentials are not permitted for read-only evals.');
          }
          const triage = require(`${process.env.GITHUB_WORKSPACE}/eng/ci/issue_triage.cjs`);
          triage.assertCompiledAgentBudgets({ root: process.env.GITHUB_WORKSPACE, workflow: 'issue-triage-eval' });
jobs:
  conclusion:
    permissions:
      actions: read
      issues: read
if: github.repository == 'microsoft/XBOX-GDK-dotnet' && github.ref == 'refs/heads/main'
permissions:
  contents: read
  copilot-requests: write
engine:
  id: copilot
# Keep in sync with DOC_HOSTS in eng/ci/issue_triage.cjs.
network:
  allowed:
    - docs.github.com
    - learn.microsoft.com
strict: true
concurrency:
  group: issue-triage-eval-${{ github.ref }}-${{ inputs.case }}
  cancel-in-progress: false
  job-discriminator: ${{ github.run_id }}
tools:
  github: false
  bash: false
  cli-proxy: false
  edit: false
  # Mirrors issue-triage.md: web-fetch makes gh-aw drop --disable-builtin-mcps,
  # leaving built-in github-mcp-server reachable. Accepted for the same reason
  # (read-only job token plus the gh-aw firewall). Review compiled authentication
  # before activation; never add or inherit a broader token.
  web-fetch: {}
max-turns: 30
max-ai-credits: 200
timeout-minutes: 20
steps:
  - name: Stage eval snapshot
    env:
      EVAL_CASE: ${{ inputs.case }}
      # Mirror the agent job's model selector so run.json records the model, not the engine.
      EVAL_MODEL: ${{ vars.GH_AW_MODEL_AGENT_COPILOT || vars.GH_AW_DEFAULT_MODEL_COPILOT || 'auto' }}
    run: |
      node eng/ci/issue_triage_eval.cjs prepare \
        --case "$EVAL_CASE" \
        --out .triage-eval \
        --model "$EVAL_MODEL" \
        --run-id "$GITHUB_RUN_ID"
      # Gold expectations must never be readable by the agent.
      rm -rf tests/evals .git
      # gh-aw configures Git remotes after custom steps. Keep empty metadata,
      # without the original objects/index that could reveal the hidden gold.
      git init --quiet --initial-branch=main
      git remote add origin https://github.com/microsoft/XBOX-GDK-dotnet.git
post-steps:
  - name: Collect and validate eval report
    if: always()
    run: |
      result=0
      node eng/ci/issue_triage_eval.cjs collect \
        --run .triage-eval \
        --agent-output /tmp/gh-aw/agent_output.json \
        --item-type submit_triage_eval_report || result=1
      node eng/ci/issue_triage_eval.cjs validate-report --run .triage-eval --no-fetch || result=1
      exit "$result"
  - name: Upload eval run
    if: always()
    uses: actions/upload-artifact@043fb46d1a93c77aae656e7c1c64a875d1fc6a0a # v7.0.1
    with:
      name: issue-triage-eval-${{ inputs.case }}
      path: |
        .triage-eval/run.json
        .triage-eval/report.json
        .triage-eval/validation.json
        .triage-eval/context/
      if-no-files-found: warn
      retention-days: 30
safe-outputs:
  github-token: ${{ secrets.GITHUB_TOKEN }}
  report-failure-as-issue: false
  report-failed-jobs: false
  threat-detection:
    enabled: true
    engine:
      id: copilot
      max-turns: 30
    report-as-issue: false
    continue-on-error: false
    max-ai-credits: 200
    max-turns: 30
    retries: 0
  noop: false
  missing-tool: false
  missing-data: false
  report-incomplete: false
  activation-comments: false
  jobs:
    submit-triage-eval-report:
      description: >-
        Submit the single triage report for this eval case. Call exactly once. The
        `report` input is a JSON string matching the schema in the instructions.
      permissions:
        contents: read
      inputs:
        report:
          description: The triage report as a JSON string.
          required: true
          type: string
      steps:
        - name: Record submission
          run: |
            echo "Eval report submitted. Score it from the issue-triage-eval-* artifact of the agent job; see tests/evals/issue-triage/README.md." >> "$GITHUB_STEP_SUMMARY"
---

# Issue triage eval

Use Actions mode of the issue-triage skill below.

- Context file: `.triage-eval/context/context.md`. It holds the synthetic issue
  labels, title, body, and earlier comments. Read it first.
- Code: the read-only snapshot at `.triage-eval/source`, extracted from
  the pinned commit named in the context file. Treat that directory as the
  repository root: search and read only there, and give every citation path
  relative to it. Ignore the job workspace; it is not the snapshot under
  evaluation.
- Output: call the `submit_triage_eval_report` tool exactly once. Its `report`
  input is the report JSON object from the skill's report contract, serialized
  as a JSON string.

{{#runtime-import .github/skills/issue-triage/SKILL.md}}
