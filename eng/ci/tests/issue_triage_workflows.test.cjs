'use strict';

const assert = require('node:assert/strict');
const fs = require('node:fs');
const path = require('node:path');
const test = require('node:test');
const { assertCompiledAgentBudgets } = require('../issue_triage.cjs');

const root = path.resolve(process.env.GH_AW_WORKFLOW_ROOT || path.join(__dirname, '..', '..', '..'));
const workflows = [
  ['issue-triage', 'publish_triage_report', 'TRIAGE_MODE'],
  ['issue-triage-eval', null, null],
  ['gdk-release-assess', 'publish_gdk_assessment', 'GDK_ASSESS_MODE'],
];

function jobsIn(text) {
  const jobs = text.slice(text.indexOf('\njobs:\n') + '\njobs:\n'.length);
  return new Map([...jobs.matchAll(/^  ([A-Za-z_][\w-]*):\r?\n([\s\S]*?)(?=^  [A-Za-z_][\w-]*:|$(?![\s\S]))/gm)]
    .map((match) => [match[1], match[2]]));
}

function jobProperty(job, name) {
  const lines = job.split('\n');
  const index = lines.findIndex((line) => line.startsWith(`    ${name}:`));
  if (index < 0) return '';
  const value = [lines[index].slice(`    ${name}:`.length).trim()];
  for (let i = index + 1; i < lines.length && lines[i].startsWith('      '); i++) value.push(lines[i].trim());
  return value.join(' ');
}

function permissionsIn(job) {
  const permissions = jobProperty(job, 'permissions');
  return Object.fromEntries([...permissions.matchAll(/([\w-]+): (read|write|none)\b/g)].map((match) => [match[1], match[2]]));
}

function proxyConfigs(job) {
  return [...job.matchAll(/printf '%s\\n' '(\{"\$schema"[^\n]+})' >[^\n]*awf-config\.json/g)]
    .map((match) => JSON.parse(match[1]).apiProxy);
}

function scriptsIn(job) {
  return [
    ...[...job.matchAll(/^          script: \|\n((?:^            .*\n|^\n)+)/gm)]
      .map((match) => match[1].replace(/^            /gm, '')),
    ...[...job.matchAll(/^\s+script: ("[^\n]*")$/gm)].map((match) => JSON.parse(match[1])),
  ];
}

test('activation budget verifier accepts bounded compiler output and rejects missing or relaxed boundaries', (t) => {
  const directory = fs.mkdtempSync(path.join(process.cwd(), '.triage-budget-test-'));
  t.after(() => fs.rmSync(directory, { recursive: true, force: true }));
  const workflowsDir = path.join(directory, '.github', 'workflows');
  fs.mkdirSync(workflowsDir, { recursive: true });
  const file = path.join(workflowsDir, 'issue-triage.lock.yml');
  function write({ credits = 200, requests = 30, enabled = true, missing = false } = {}) {
    const config = JSON.stringify({ $schema: 'https://example.invalid/schema', apiProxy: { enabled, maxAiCredits: credits, maxRuns: requests } });
    fs.writeFileSync(file, `jobs:\n${['agent', 'detection'].map((phase) =>
      `  ${phase}:\n    steps:\n      - run: |\n${missing ? '' : `          printf '%s\\n' '${config}' > awf-config.json\n`}`).join('')}`);
  }
  write();
  assert.doesNotThrow(() => assertCompiledAgentBudgets({ root: directory, workflow: 'issue-triage' }));
  for (const options of [{ credits: 201 }, { requests: 500 }, { requests: 0 }, { enabled: false }, { missing: true }]) {
    write(options);
    assert.throws(() => assertCompiledAgentBudgets({ root: directory, workflow: 'issue-triage' }), /Activation blocked/);
  }
  assert.throws(() => assertCompiledAgentBudgets({ root: directory, workflow: '../escape' }), /Unknown agent workflow/);
});

for (const [name, publisher, mode] of workflows) {
  test(`${name}: generated jobs cannot issue-write outside the exact-post trusted publisher`, () => {
    const text = fs.readFileSync(path.join(root, '.github', 'workflows', `${name}.lock.yml`), 'utf8').replace(/\r\n/g, '\n');
    const jobs = jobsIn(text);
    assert.ok(jobs.has('agent') && jobs.has('conclusion') && jobs.has('detection'), 'compiled security-boundary jobs must exist');
    assert.equal(permissionsIn(jobs.get('conclusion')).issues, 'read');
    for (const [jobName, job] of jobs) {
      const permissions = permissionsIn(job);
      if (jobName !== publisher) {
        assert.notEqual(permissions.issues, 'write', `${jobName} must not write issues`);
        assert.notEqual(permissions['pull-requests'], 'write', `${jobName} must not write pull-request comments`);
        assert.notEqual(permissions.contents, 'write', `${jobName} must not write repository contents`);
      } else {
        assert.equal(permissions.issues, 'write');
        const condition = jobProperty(job, 'if');
        assert.ok(condition.includes("github.repository == 'microsoft/XBOX-GDK-dotnet'"), 'publisher must reject forks');
        assert.ok(condition.includes("github.ref == 'refs/heads/main'"), 'publisher must reject branches/tags');
        assert.ok(condition.includes(`vars.${mode} == 'post'`), 'publisher requires exact post mode');
        assert.ok(condition.includes("needs.authorize_post.outputs.post_allowed == 'true'"), 'writer requires case-sensitive read-only authorization');
        assert.ok(jobProperty(job, 'needs').includes('authorize_post'), 'authorization must be an actual job dependency');
        assert.ok(condition.includes("needs.agent.result == 'success'"));
        assert.ok(condition.includes("needs.detection.outputs.detection_success == 'true'"));
        if (name === 'gdk-release-assess') assert.ok(condition.includes('inputs.preview != true'), 'preview must not receive a writer');
      }
    }
    if (publisher) assert.ok(jobs.has(publisher), 'expected controlled publisher must exist');
    const conclusion = jobs.get('conclusion');
    assert.doesNotMatch(conclusion, /handle_detection_runs\.cjs|report_failed_jobs\.cjs/, 'generic issue reporters must not be compiled');
    assert.match(conclusion, /GH_AW_FAILURE_REPORT_AS_ISSUE: "false"/);
    if (name === 'issue-triage') assert.match(jobs.get('agent'), /env: \{ \.\.\.process\.env, TRIAGE_MODE: 'staged' \}/);
  });

  test(`${name}: generated model calls are bounded or activation fails closed on the compiler limitation`, async () => {
    const text = fs.readFileSync(path.join(root, '.github', 'workflows', `${name}.lock.yml`), 'utf8').replace(/\r\n/g, '\n');
    const jobs = jobsIn(text);
    for (const phase of ['agent', 'detection']) {
      const job = jobs.get(phase);
      assert.ok(job, `${phase} must not be disabled`);
      const configs = proxyConfigs(job);
      assert.equal(configs.length, 1, `${phase} must have a concrete compiled proxy configuration`);
      assert.equal(configs[0].enabled, true);
      assert.equal(configs[0].maxAiCredits, 200);
      if (phase === 'agent') assert.equal(configs[0].maxRuns, 30);
      assert.match(job, /GH_AW_MAX_TURNS: 30(?:\n|$)/);
      assert.doesNotMatch(job, /GH_AW_DEFAULT_DETECTION_MAX_AI_CREDITS|GH_AW_DEFAULT_MAX_TURNS/);
    }
    const detection = jobs.get('detection');
    assert.match(detection, /\bthreat-detect --engine copilot --max-turns 30 --retries 0\b/);
    assert.match(detection, /GH_AW_DETECTION_CONTINUE_ON_ERROR: "false"/);
    const preActivation = jobs.get('pre_activation');
    const guard = scriptsIn(preActivation).find((script) => script.includes('assertCompiledAgentBudgets'));
    assert.ok(guard, 'actual compiled pre-activation must check concrete proxy budgets');
    assert.ok(jobProperty(jobs.get('activation'), 'needs').includes('pre_activation'));
    assert.doesNotMatch(jobProperty(jobs.get('activation'), 'if'), /(?:always|failure|cancelled)\(/);
    assert.ok(jobProperty(jobs.get('agent'), 'needs').includes('activation'));
    assert.doesNotMatch(jobProperty(jobs.get('agent'), 'if'), /(?:always|failure|cancelled)\(/);
    assert.ok(jobProperty(detection, 'if').includes("needs.agent.result != 'skipped'"));
    if (proxyConfigs(detection)[0].maxRuns > 30) {
      let output = false;
      const execute = new (Object.getPrototypeOf(async function () {}).constructor)('require', 'process', 'context', 'core', 'github', guard);
      await assert.rejects(() => execute(() => ({
        assertCompiledAgentBudgets,
        runGate: async () => { throw new Error('live gate must not run after unsupported budgets'); },
      }), { env: { HAS_ALTERNATE_GITHUB_AUTH: 'false', GITHUB_WORKSPACE: root } }, {},
      { setOutput: () => { output = true; } }, {}), /Activation blocked: compiled detection proxy/);
      assert.equal(output, false, 'failed budget verification must not mark activation eligible/ready');
    } else {
      assert.doesNotThrow(() => assertCompiledAgentBudgets({ root, workflow: name }));
    }
  });

  if (publisher) {
    test(`${name}: compiled read-only authorization rejects malformed modes, forks, refs and previews before granting a writer`, async () => {
      const text = fs.readFileSync(path.join(root, '.github', 'workflows', `${name}.lock.yml`), 'utf8').replace(/\r\n/g, '\n');
      const authorization = jobsIn(text).get('authorize_post');
      assert.ok(authorization, 'separate permissionless authorization job must exist');
      assert.deepEqual(permissionsIn(authorization), {});
      assert.ok(authorization.includes('post_allowed: ${{ steps.authorize.outputs.post_allowed }}'));
      const scripts = scriptsIn(authorization);
      const script = scripts.find((value) => value.includes("core.setOutput('post_allowed'"));
      assert.ok(script, 'authorization must execute in a read-only agent step');
      const execute = new (Object.getPrototypeOf(async function () {}).constructor)('require', 'process', 'context', 'core', 'github', script);
      const variants = [
        { allowed: true, mode: 'post' },
        ...[undefined, '', 'POST', 'Post', 'post ', 'staged'].map((value) => ({ mode: value })),
        { mode: 'post', repo: { owner: 'fork', repo: 'XBOX-GDK-dotnet' } },
        { mode: 'post', repo: { owner: 'microsoft', repo: 'another-repo' } },
        ...['refs/heads/topic', 'refs/heads/Main', 'refs/tags/v260404.1.0'].map((ref) => ({ mode: 'post', ref })),
      ];
      if (name === 'gdk-release-assess') {
        variants.push(...[true, 'false', undefined].map((preview) => ({ mode: 'post', preview })));
      } else {
        variants.push({ mode: 'post', eventName: 'workflow_dispatch' });
      }
      for (const variant of variants) {
        const outputs = {};
        const env = {
          GITHUB_WORKSPACE: root,
          GITHUB_SHA: 'a'.repeat(40),
          [mode]: variant.mode,
          GDK_ASSESS_INPUTS: JSON.stringify({ preview: Object.hasOwn(variant, 'preview') ? variant.preview : false }),
        };
        const context = {
          repo: variant.repo || { owner: 'microsoft', repo: 'XBOX-GDK-dotnet' },
          ref: variant.ref || 'refs/heads/main',
          eventName: variant.eventName || 'issue_comment',
        };
        await execute(() => ({ prepareContext: async () => {}, prepareAssessmentContext: async () => {} }),
          { env }, context, { setOutput: (key, value) => { outputs[key] = value; } }, {});
        assert.equal(outputs.post_allowed, variant.allowed ? 'true' : 'false', JSON.stringify(variant));
      }
    });
  }
}
