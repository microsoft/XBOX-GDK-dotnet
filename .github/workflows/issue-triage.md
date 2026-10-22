---
name: Issue Triage
description: >-
  First-pass AI evaluation of an issue against the default branch, requested by a
  maintainer commenting exactly `/triage`. Publication is staged unless TRIAGE_MODE
  is exactly post on the trusted repository's main branch.
on:
  issue_comment:
    types: [created]
  roles: [admin, maintainer, write]
  reaction: none
  status-comment: false
  permissions:
    contents: read
    issues: read
  steps:
    - name: Check out triage helper
      uses: actions/checkout@93cb6efe18208431cddfb8368fd83d5badbf9bfd # v5.0.1
      with:
        persist-credentials: false
        ref: ${{ github.sha }}
        sparse-checkout: |
          eng/ci
          .github/workflows
        sparse-checkout-cone-mode: true
    - name: Check triage eligibility
      id: gate
      uses: actions/github-script@3a2844b7e9c422d3c10d287c895573f7108da1b3 # v9.0.0
      env:
        HAS_ALTERNATE_GITHUB_AUTH: ${{ secrets.GH_AW_GITHUB_MCP_SERVER_TOKEN != '' || secrets.GH_AW_GITHUB_TOKEN != '' }}
      with:
        script: |
          if (process.env.HAS_ALTERNATE_GITHUB_AUTH !== 'false') {
            throw new Error('Alternate gh-aw GitHub credentials are not permitted for read-only triage.');
          }
          const triage = require(`${process.env.GITHUB_WORKSPACE}/eng/ci/issue_triage.cjs`);
          triage.assertCompiledAgentBudgets({ root: process.env.GITHUB_WORKSPACE, workflow: 'issue-triage' });
          await triage.runGate({ github, context, core });
jobs:
  pre-activation:
    outputs:
      eligible: ${{ steps.gate.outputs.eligible }}
  # GitHub expression comparisons ignore case; authorize before granting a writer.
  authorize_post:
    runs-on: ubuntu-slim
    permissions: {}
    outputs:
      post_allowed: ${{ steps.authorize.outputs.post_allowed }}
    steps:
      - name: Authorize exact-post triage
        id: authorize
        uses: actions/github-script@3a2844b7e9c422d3c10d287c895573f7108da1b3 # v9.0.0
        env:
          TRIAGE_MODE: ${{ vars.TRIAGE_MODE }}
        with:
          script: |
            const postAllowed = process.env.TRIAGE_MODE === 'post' &&
              context.repo.owner === 'microsoft' && context.repo.repo === 'XBOX-GDK-dotnet' &&
              context.ref === 'refs/heads/main' && context.eventName === 'issue_comment';
            core.setOutput('post_allowed', postAllowed ? 'true' : 'false');
  conclusion:
    permissions:
      actions: read
      issues: read
  publish_triage_report:
    needs: [agent, detection, authorize_post, post_triage_report]
    if: (!cancelled()) && needs.authorize_post.outputs.post_allowed == 'true' && needs.agent.result == 'success' && needs.detection.outputs.detection_success == 'true' && needs.post_triage_report.result == 'success' && github.repository == 'microsoft/XBOX-GDK-dotnet' && github.ref == 'refs/heads/main' && vars.TRIAGE_MODE == 'post'
    runs-on: ubuntu-latest
    permissions:
      contents: read
      issues: write
    steps:
      - name: Check out analyzed commit
        uses: actions/checkout@93cb6efe18208431cddfb8368fd83d5badbf9bfd # v5.0.1
        with:
          ref: ${{ github.sha }}
          persist-credentials: false
      - name: Download triage context metadata
        uses: actions/download-artifact@3e5f45b2cfb9172054b4087a40e8e0b5a5461e7c # v8.0.1
        with:
          name: issue-triage-context
          path: .triage-publish/context
      - name: Download submitted report
        uses: actions/download-artifact@3e5f45b2cfb9172054b4087a40e8e0b5a5461e7c # v8.0.1
        with:
          pattern: "{agent,agent-output-fallback}"
          merge-multiple: true
          path: .triage-publish/agent
      - name: Publish triage report
        uses: actions/github-script@3a2844b7e9c422d3c10d287c895573f7108da1b3 # v9.0.0
        env:
          TRIAGE_MODE: ${{ vars.TRIAGE_MODE }}
        with:
          script: |
            const triage = require(`${process.env.GITHUB_WORKSPACE}/eng/ci/issue_triage.cjs`);
            await triage.publish({
              github, context, core, env: process.env,
              root: process.env.GITHUB_WORKSPACE,
              agentOutputPath: `${process.env.GITHUB_WORKSPACE}/.triage-publish/agent/agent_output.json`,
              contextPath: `${process.env.GITHUB_WORKSPACE}/.triage-publish/context/context.json`,
            });
if: needs.pre_activation.outputs.eligible == 'true' && github.repository == 'microsoft/XBOX-GDK-dotnet' && github.ref == 'refs/heads/main'
permissions:
  contents: read
  issues: read
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
  group: issue-triage-${{ github.repository_id }}-${{ github.event.issue.number }}-${{ github.event.comment.id }}
  cancel-in-progress: false
tools:
  github: false
  bash: false
  cli-proxy: false
  edit: false
  # Enabling web-fetch makes gh-aw drop --disable-builtin-mcps from the compiled
  # harness, because Copilot CLI serves web_fetch from its built-in tool schema.
  # That also leaves built-in github-mcp-server reachable despite `github: false`.
  # Accepted here: the job token is read-only (contents/issues: read) and the
  # firewall still constrains egress. Do not configure inherited broader tokens:
  # the compiled authentication and permissions must be checked before activation.
  web-fetch: {}
max-turns: 30
max-ai-credits: 200
timeout-minutes: 20
steps:
  - name: Prepare triage context
    uses: actions/github-script@3a2844b7e9c422d3c10d287c895573f7108da1b3 # v9.0.0
    with:
      script: |
        const triage = require(`${process.env.GITHUB_WORKSPACE}/eng/ci/issue_triage.cjs`);
        await triage.prepareContext({
          github,
          context,
          core,
          outDir: `${process.env.GITHUB_WORKSPACE}/.triage-agent`,
          sha: process.env.GITHUB_SHA,
        });
post-steps:
  - name: Upload triage context metadata
    if: always()
    uses: actions/upload-artifact@043fb46d1a93c77aae656e7c1c64a875d1fc6a0a # v7.0.1
    with:
      name: issue-triage-context
      path: .triage-agent/context.json
      if-no-files-found: ignore
      retention-days: 7
  - name: Validate and stage triage report
    uses: actions/github-script@3a2844b7e9c422d3c10d287c895573f7108da1b3 # v9.0.0
    with:
      script: |
        const triage = require(`${process.env.GITHUB_WORKSPACE}/eng/ci/issue_triage.cjs`);
        await triage.publish({
          github,
          context,
          core,
          env: { ...process.env, TRIAGE_MODE: 'staged' },
          agentOutputPath: '/tmp/gh-aw/agent_output.json',
          root: process.env.GITHUB_WORKSPACE,
          contextPath: `${process.env.GITHUB_WORKSPACE}/.triage-agent/context.json`,
        });
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
    post-triage-report:
      description: >-
        Submit the single triage report for this issue. Call exactly once. The
        `report` input is a JSON string matching the schema in the instructions.
      permissions:
        contents: read
      inputs:
        report:
          description: The triage report as a JSON string.
          required: true
          type: string
      steps:
        - name: Record validated submission
          run: echo "Report validated and staged in the read-only agent summary." >> "$GITHUB_STEP_SUMMARY"
---

# First-pass issue triage

Use Actions mode of the issue-triage skill below.

- Context file: `.triage-agent/context.md`. It holds the issue title,
  body, labels, and earlier comments. Read it first.
- Code: the current workspace, checked out at the default-branch commit named in
  the context file.
- This is current-main static analysis only. For older package reports, explain
  the mismatch and ask for the exact release tag/commit and both exact Windows
  and Core NuGet identities. Do not assume main reproduces historical behavior.
- Output: call the `post_triage_report` tool exactly once. Its `report` input is
  the report JSON object from the skill's report contract, serialized as a JSON
  string.

{{#runtime-import .github/skills/issue-triage/SKILL.md}}
