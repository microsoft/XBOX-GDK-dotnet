const { test } = require('node:test');
const assert = require('node:assert/strict');
const fs = require('node:fs');
const os = require('node:os');
const path = require('node:path');
const { execFileSync } = require('node:child_process');
const { select, changedPaths, aggregate, eventSelection } = require('./pr_gates.cjs');

test('authored docs and templates do not select Windows builds', () => {
  const result = select(['docs/building.md', 'CONTRIBUTING.md', 'eng/README.md', '.github/ISSUE_TEMPLATE/bug_report.yml']);
  for (const lane of ['build', 'aot', 'docs', 'automation']) assert.equal(result[lane], false);
});

test('mixed paths union lanes, including both sides of a rename', () => {
  const paths = changedPaths('R100\0docs/example.md\0src/GDK.Net/Example.cs\0M\0eng/ci/issue_triage.cjs\0');
  const result = select(paths);
  assert.equal(result.build, true);
  assert.equal(result.aot, true);
  assert.equal(result.docs, true);
  assert.equal(result.automation, true);
});

test('automation-only changes avoid Windows jobs', () => {
  const result = select(['.github/workflows/issue-triage.lock.yml', 'eng/ci/gdk_release_watch.test.cjs',
    'eng/ci/tests/issue_triage_workflows.test.cjs']);
  assert.equal(result.automation, true);
  assert.equal(result.build, false);
  assert.equal(result.aot, false);
});

test('generated docs select prerequisites, while shared build inputs select every gate', () => {
  for (const path of ['docs/api/Core/GDK.Net.GameRuntime.md', 'eng/generate-docs.ps1', 'eng/api-layout.ps1']) {
    const result = select([path]);
    assert.equal(result.docs, true);
    assert.equal(result.build, true);
    assert.equal(result.aot, false);
  }
  for (const path of ['Directory.Build.props', 'GDK.Net.slnx', '.github/gdk-version.json',
    '.github/workflows/ci.yml', 'eng/ci/native-evidence.cjs', 'eng/ci/native-evidence.test.cjs',
    'eng/ci/new-build-policy.cjs', '.github/workflows/unrecognized.lock.yml',
    '.github/workflows/unrecognized.md', 'unknown.file', 'tests/GDK.Net.Tests/UserTests.cs']) {
    const result = select([path]);
    for (const lane of ['build', 'aot', 'docs', 'automation']) assert.equal(result[lane], true, path);
  }
});

test('Git diff parser handles deletion, whitespace and copies without line splitting', () => {
  assert.deepEqual(changedPaths('D\0src/old.cs\0C90\0docs/a b.md\0docs/a\nb.md\0'),
    ['src/old.cs', 'docs/a b.md', 'docs/a\nb.md']);
  assert.deepEqual(changedPaths(''), []);
  for (const input of ['M\0', 'R100\0a\0', 'Q\0file\0', 'M\0file']) {
    assert.throws(() => changedPaths(input));
  }
});

test('manual, tag and new-branch runs select all checks', () => {
  for (const selection of [
    eventSelection('workflow_dispatch', {}, {}),
    eventSelection('push', {}, { GITHUB_REF: 'refs/tags/v260404.1.0' }),
    eventSelection('push', { before: '0'.repeat(40), after: 'a'.repeat(40) }, {}),
  ]) {
    for (const lane of ['build', 'aot', 'docs', 'automation']) assert.equal(selection[lane], true);
  }
  assert.throws(() => eventSelection('pull_request', {}, {}));
  assert.throws(() => eventSelection('push', { after: '--bad' }, {}));
});

test('aggregate accepts only selected successes and unselected skips', () => {
  const selection = select(['eng/ci/issue_triage.cjs']);
  const results = { scope: 'success', lint: 'success', build: 'skipped', aot: 'skipped', docs: 'skipped', automation: 'success' };
  aggregate(selection, results);
  for (const status of ['failure', 'cancelled', 'skipped', undefined]) {
    assert.throws(() => aggregate(selection, { ...results, automation: status }));
  }
  assert.throws(() => aggregate(selection, { ...results, scope: 'failure' }));
  assert.throws(() => aggregate(selection, { ...results, lint: 'skipped' }));
  assert.throws(() => aggregate(selection, { ...results, build: 'success' }));
  assert.throws(() => aggregate({ ...selection, docs: 'false' }, results));
});

test('PRs use the merge base, while unavailable push baselines require full checks', () => {
  const root = fs.mkdtempSync(path.join(os.tmpdir(), 'gdk-pr-gates-'));
  const original = process.cwd();
  const git = (...args) => execFileSync('git', args, { cwd: root, encoding: 'utf8', stdio: ['ignore', 'pipe', 'pipe'] }).trim();
  const write = (relative, content) => {
    const target = path.join(root, relative);
    fs.mkdirSync(path.dirname(target), { recursive: true });
    fs.writeFileSync(target, content);
  };
  try {
    git('init', '--quiet', '--initial-branch=main');
    git('config', 'user.email', 'fixture@example.invalid');
    git('config', 'user.name', 'Scope fixture');
    git('config', 'commit.gpgsign', 'false');
    write('README.md', 'baseline');
    git('add', '.');
    git('commit', '--quiet', '-m', 'Baseline');
    git('checkout', '--quiet', '-b', 'release/260404');
    write('docs/change.md', 'maintenance docs');
    git('add', '.');
    git('commit', '--quiet', '-m', 'Documentation');
    const head = git('rev-parse', 'HEAD');
    git('checkout', '--quiet', 'main');
    write('src/GDK.Net/Unrelated.cs', 'target branch advanced');
    git('add', '.');
    git('commit', '--quiet', '-m', 'Target advanced');
    const base = git('rev-parse', 'HEAD');
    process.chdir(root);
    const pr = eventSelection('pull_request', {
      pull_request: { base: { sha: base }, head: { sha: head } },
    }, {});
    for (const lane of ['build', 'aot', 'docs', 'automation']) assert.equal(pr[lane], false);
    assert.equal(pr.evidenceBase, base);
    const previous = git('rev-parse', 'HEAD~1');
    const ordinaryPush = eventSelection('push', { before: previous, after: base }, {});
    assert.equal(ordinaryPush.evidenceBase, previous);
    assert.throws(() => eventSelection('pull_request', {
      pull_request: { base: { sha: 'b'.repeat(40) }, head: { sha: head } },
    }, {}));
    const push = eventSelection('push', { before: 'b'.repeat(40), after: base }, {});
    for (const lane of ['build', 'aot', 'docs', 'automation']) assert.equal(push[lane], true);
    assert.equal(push.evidenceBase, undefined);
    assert.match(push.reasons[0], /baseline unavailable/);
  } finally {
    process.chdir(original);
    fs.rmSync(root, { recursive: true, force: true });
  }
});
