'use strict';

const assert = require('node:assert/strict');
const fs = require('node:fs');
const path = require('node:path');
const test = require('node:test');
const watch = require('../gdk_release_watch.cjs');

const SHA = 'a'.repeat(40);
const PIN = Object.freeze({ edition: '260404', windowsVersion: '2604.4.7897', coreVersion: '2604.4.7897' });
const CONTEXT = { repo: { owner: 'microsoft', repo: 'XBOX-GDK-dotnet' } };
const ENV = { GITHUB_REF: 'refs/heads/main', GITHUB_SHA: SHA, GITHUB_RUN_ID: '123', GITHUB_RUN_ATTEMPT: '2', GDK_WATCH_MODE: 'post' };
const BOT = 'github-actions[bot]';

test('watcher source passes repository mode and explicit inputs, with pinned actions and serialized runs', () => {
  const source = fs.readFileSync(path.resolve(__dirname, '..', '..', '..', '.github', 'workflows', 'gdk-release-watch.yml'), 'utf8');
  assert.match(source, /GDK_WATCH_MODE: \$\{\{ vars\.GDK_WATCH_MODE \}\}/);
  assert.match(source, /GDK_WATCH_INPUTS: \$\{\{ toJSON\(inputs\) \}\}/);
  assert.match(source, /persist-credentials: false/);
  assert.match(source, /cancel-in-progress: false/);
  assert.doesNotMatch(source, /drain_backlog/);
  for (const line of source.split('\n').filter((value) => value.includes('uses:'))) assert.match(line, /@[0-9a-f]{40}(?:\s|$)/);
});

test('watcher grants write tokens only to the job gated by trusted main, exact post mode and no preview', async () => {
  const source = fs.readFileSync(path.resolve(__dirname, '..', '..', '..', '.github', 'workflows', 'gdk-release-watch.yml'), 'utf8').replace(/\r\n/g, '\n');
  const preview = source.split('\n  preview:\n')[1].split('\n  post:\n')[0];
  const post = source.split('\n  post:\n')[1];
  assert.match(preview, /permissions:\n      contents: read\n      issues: read/);
  assert.doesNotMatch(preview, /(?:issues|actions): write/);
  assert.match(preview, /env: \{ \.\.\.process\.env, GDK_WATCH_MODE: 'staged' \}/);
  assert.match(post, /needs: preview/);
  assert.match(post, /permissions:\n      contents: read\n      issues: write\n      actions: write/);
  const condition = /    if: >-\n([\s\S]*?)    runs-on:/.exec(post)[1].trim();
  assert.match(condition, /needs\.preview\.outputs\.post_allowed == 'true'/);
  assert.match(condition, /github\.repository == 'microsoft\/XBOX-GDK-dotnet'/);
  assert.match(condition, /github\.ref == 'refs\/heads\/main'/);
  assert.match(condition, /vars\.GDK_WATCH_MODE == 'post'/);
  assert.match(condition, /inputs\.preview != true/);

  // Execute the actual read-only workflow script, with only a fake helper/API.
  const script = /          script: \|\n([\s\S]*)/.exec(preview)[1].split('\n').map((line) => line.slice(12)).join('\n');
  const AsyncFunction = Object.getPrototypeOf(async function () {}).constructor;
  const execute = new AsyncFunction('require', 'process', 'github', 'context', 'core', script);
  const variants = [
    { mode: 'post', ref: 'refs/heads/main', context: CONTEXT, preview: false, expected: true },
    ...['', 'POST', 'Post', 'post ', 'staged'].map((mode) => ({ mode, ref: 'refs/heads/main', context: CONTEXT, expected: false })),
    { mode: 'post', ref: 'refs/heads/main', context: CONTEXT, preview: true, expected: false },
    { mode: 'post', ref: 'refs/heads/topic', context: CONTEXT, expected: false },
    { mode: 'post', ref: 'refs/tags/v260404.1.0', context: CONTEXT, expected: false },
    { mode: 'post', ref: 'refs/heads/main', context: { repo: { owner: 'fork', repo: 'XBOX-GDK-dotnet' } }, expected: false },
  ];
  for (const variant of variants) {
    const outputs = {};
    let helperEnv;
    const helper = { ...watch, runWatch: async ({ env }) => { helperEnv = env; } };
    const env = { GDK_WATCH_MODE: variant.mode, GITHUB_REF: variant.ref, GDK_WATCH_INPUTS: JSON.stringify({ preview: variant.preview || false }) };
    await execute(() => helper, { env }, {}, variant.context, { setOutput: (name, value) => { outputs[name] = value; } });
    assert.equal(helperEnv.GDK_WATCH_MODE, 'staged');
    assert.equal(outputs.post_allowed, String(variant.expected), JSON.stringify(variant));
  }
});

function release(version = '2604.5.7903', id = 55) {
  const parsed = watch.parseVersion(version);
  const tag = `April-2026-Update-${parsed.parts[1]}-v${version}`;
  return { id, tag_name: tag, name: `Microsoft GDK ${version}`, body: '# Changes\nNew public SDK notes.',
    html_url: `https://github.com/microsoft/GDK/releases/tag/${tag}`, published_at: '2026-05-01T00:00:00Z',
    assets: [{ name: `GDK_${version}.zip` }] };
}

function core() {
  const value = { warnings: [], summaryText: '' };
  value.info = () => {};
  value.notice = () => {};
  value.warning = (text) => value.warnings.push(text);
  value.summary = { addHeading: () => value.summary, addRaw: (text) => { value.summaryText += text; return value.summary; }, write: async () => {} };
  return value;
}

function fixture(options = {}) {
  const state = { writes: [], issues: [...(options.issues || [])], comments: [...(options.comments || [])], fetches: [], branchReads: 0 };
  const result = { state, core: core() };
  result.fetchImpl = async (url) => {
    state.fetches.push(url);
    let data;
    if (url === 'https://api.nuget.org/v3/index.json' || url === 'https://feed.example.test/v3/index.json') {
      data = { resources: [{ '@type': 'PackageBaseAddress/3.0.0', '@id': 'https://feed.example.test/flat/' }] };
    } else if (url.endsWith('/microsoft.gdk.windows/index.json')) data = { versions: options.windowsVersions || ['2604.5.7903'] };
    else if (url.endsWith('/microsoft.gdk.core/index.json')) data = { versions: options.coreVersions || ['2604.5.7903'] };
    else throw new Error(`Unexpected metadata or payload URL: ${url}`);
    return { ok: true, json: async () => data };
  };
  const write = (type, args) => state.writes.push({ type, ...args });
  const issueApi = {
    listForRepo: () => {},
    listComments: () => {},
    getLabel: async () => { if (options.missingLabel) throw Object.assign(new Error('missing label'), { status: 404 }); return { data: {} }; },
    createLabel: async (args) => { write('label', args); return { data: {} }; },
    create: async (args) => {
      write('issue', args);
      const issue = { ...args, number: 20 + state.issues.length, state: 'open', user: { login: BOT }, html_url: 'https://github.com/example/issue/20' };
      state.issues.push(issue);
      if (options.ambiguousIssue) throw new Error('lost creation response');
      return { data: issue };
    },
    createComment: async (args) => {
      write('comment', args);
      const comment = { ...args, id: 100 + state.comments.length, user: { login: BOT } };
      state.comments.push(comment);
      return { data: comment };
    },
  };
  result.github = {
    paginate: async (method) => {
      if (options.listError) throw options.listError;
      return method === issueApi.listForRepo ? state.issues : state.comments;
    },
    rest: {
      issues: issueApi,
      repos: {
        getBranch: async () => { state.branchReads += 1; return { data: { commit: { sha: options.mainMoved && state.branchReads > 1 ? 'b'.repeat(40) : SHA } } }; },
        getContent: async () => ({ data: { encoding: 'base64', content: Buffer.from(JSON.stringify(options.pin || PIN)).toString('base64') } }),
        listReleases: async ({ page }) => ({ data: options.releasePages ? options.releasePages[page - 1] || [] : options.releases || [release()] }),
      },
      actions: { createWorkflowDispatch: async (args) => { write('dispatch', args); if (options.dispatchError) throw new Error('dispatch failed'); } },
    },
  };
  result.run = (env = {}, context = CONTEXT) => watch.runWatch({ ...result, context, env: { ...ENV, ...env } });
  return result;
}

function tracking(version = '2604.5.7903', overrides = {}) {
  return { number: 8, state: 'open', user: { login: BOT }, body: `${watch.markerPrefix(`pkg-${watch.parseVersion(version).key}`)}version=${version} -->`, ...overrides };
}

function ledger(status, overrides = {}) {
  return { id: 12, user: { login: BOT }, body: watch.renderStateComment({ releaseId: 'pkg-2604.5.7903.0',
    state: { status, attempt: '111.1', at: new Date().toISOString(), ...overrides } }) };
}

test('numeric full SDK ordering includes fourth-component servicing but ignores old families and prereleases', () => {
  const result = watch.selectCandidates({ pin: PIN, releases: [], windowsVersions: [
    '2604.4.7897', '2604.4.7897.1', '2604.10.1', '2604.9.99999', '2510.99.999999', '2604.5.1-preview', '2610.0.1',
  ], coreVersions: ['2604.4.7897.1', '2604.10.1', '2604.9.99999', '2610.0.1'] });
  assert.deepEqual(result.candidates.map((entry) => entry.version), ['2610.0.1', '2604.10.1', '2604.9.99999', '2604.4.7897.1']);
  assert.equal(watch.compareVersions('2604.4.7897', '2604.4.7897.0'), 0);
});

test('pin requires exact matching pair and edition, without a supported-version catalog', () => {
  assert.equal(watch.parsePin(PIN).edition, '260404');
  for (const bad of [{ ...PIN, coreVersion: '2604.5.7903' }, { ...PIN, edition: 260404 }, { ...PIN, windowsVersion: '[2604.4.7897]' }]) {
    assert.throws(() => watch.parsePin(bad), /main GDK pin/);
  }
});

test('missing pair, missing notes, ambiguous notes and release-before-feed are visible evidence', () => {
  const first = release();
  const second = { ...first, id: 56 };
  const missing = watch.selectCandidates({ pin: PIN, releases: [], windowsVersions: ['2604.5.7903'], coreVersions: [] }).candidates[0];
  assert.equal(missing.eligible, false);
  assert.equal(missing.gaps.length, 2);
  const ambiguous = watch.selectCandidates({ pin: PIN, releases: [first, second], windowsVersions: ['2604.5.7903'], coreVersions: ['2604.5.7903'] }).candidates[0];
  assert.equal(ambiguous.tag, null);
  assert.match(ambiguous.gaps.join(), /Multiple/);
  assert.match(watch.selectCandidates({ pin: PIN, releases: [first], windowsVersions: [], coreVersions: [] }).conflicts[0].reason, /no matching Windows/);
});

test('package identity survives notes arriving later and is not a release-id identity', () => {
  const options = { pin: PIN, windowsVersions: ['2604.5.7903'], coreVersions: ['2604.5.7903'] };
  const early = watch.selectCandidates({ ...options, releases: [] }).candidates[0];
  const late = watch.selectCandidates({ ...options, releases: [release()] }).candidates[0];
  assert.equal(early.id, late.id);
  assert.notEqual(late.id, late.releaseId);
  assert.equal(late.gaps.length, 0);
});

test('tag and asset inconsistencies are conservative conflicts', () => {
  assert.equal(watch.classifyRelease({ ...release(), assets: [] }).status, 'conflict');
  assert.equal(watch.classifyRelease({ ...release(), assets: [{ name: 'GDK_2604.4.7897.zip' }] }).status, 'conflict');
  assert.equal(watch.classifyRelease({ ...release(), html_url: 'https://evil.example.test' }).status, 'conflict');
  assert.equal(watch.classifyRelease({ ...release(), draft: true }).status, 'ignored');
  assert.ok(watch.parseGdkTag('May-2026-Update-5-v2604.5.7903').conflict);
});

test('retry is explicit, requires one release tag, and has no bulk-drain input', () => {
  assert.throws(() => watch.readWatchInputs({ GDK_WATCH_INPUTS: '{"retry":true}' }), /requires release_tag/);
  assert.throws(() => watch.readWatchInputs({ GDK_WATCH_INPUTS: '[' }), /valid JSON/);
  assert.deepEqual(watch.readWatchInputs({ GDK_WATCH_INPUTS: '{"drain_backlog":true}' }), { releaseTag: null, retry: false, preview: false });
  assert.equal(watch.assessmentAttemptKey({ runId: '123', runAttempt: '2' }), '123.2');
  assert.throws(() => watch.assessmentAttemptKey({ runId: '123' }), /runId.runAttempt/);
});

test('missing or malformed modes, preview, forks, feature branches and tags perform zero writes', async (t) => {
  const variants = [
    { env: { GDK_WATCH_MODE: undefined } }, { env: { GDK_WATCH_MODE: 'POST' } }, { env: { GDK_WATCH_MODE: 'post ' } },
    { env: { GDK_WATCH_INPUTS: '{"preview":true}' } }, { env: { GITHUB_REF: 'refs/heads/topic' } },
    { env: { GITHUB_REF: 'refs/tags/v260404.1.0' } }, { context: { repo: { owner: 'fork', repo: 'XBOX-GDK-dotnet' } } },
    { env: { GITHUB_SHA: 'b'.repeat(40) } },
  ];
  for (const variant of variants) await t.test(JSON.stringify(variant), async () => {
    const f = fixture();
    assert.equal((await f.run(variant.env, variant.context)).preview, true);
    assert.equal(f.state.writes.length, 0);
  });
});

test('trusted post selects newest pending candidate, provisions label, files just one issue and dispatches at most one', async () => {
  const f = fixture({ windowsVersions: ['2604.5.7903', '2604.7.8000', '2604.6.7999'], coreVersions: ['2604.5.7903', '2604.6.7999', '2604.7.8000'], releases: [], missingLabel: true });
  const result = await f.run();
  assert.equal(result.selected.version, '2604.7.8000');
  assert.equal(f.state.writes.filter((entry) => entry.type === 'issue').length, 1);
  assert.equal(f.state.writes.filter((entry) => entry.type === 'dispatch').length, 1);
  assert.equal(f.state.writes.filter((entry) => entry.type === 'label').length, 1);
  const dispatch = f.state.writes.find((entry) => entry.type === 'dispatch');
  assert.equal(dispatch.inputs.attempt, '123.2');
  assert.equal(dispatch.inputs.release_id, 'pkg-2604.7.8000.0');
  assert.equal(dispatch.ref, 'main');
  assert.match(f.state.writes.find((entry) => entry.type === 'comment').body, /"mainSha"/);
});

test('newest completed candidate does not starve next newest pending candidate', async () => {
  const f = fixture({ windowsVersions: ['2604.5.7903', '2604.6.8000'], coreVersions: ['2604.5.7903', '2604.6.8000'],
    releases: [], issues: [tracking('2604.6.8000')], comments: [{ id: 1, user: { login: BOT }, body: watch.renderStateComment({
      releaseId: 'pkg-2604.6.8000.0', state: { status: 'needs-review' } }) }] });
  assert.equal((await f.run()).selected.version, '2604.5.7903');
});

test('closed issues opt out and in-flight/terminal failures never automatically retry', async (t) => {
  for (const status of ['assessment-dispatched', 'assessment-failed', 'needs-review', 'tests-only', 'changes-required']) {
    await t.test(status, async () => {
      const f = fixture({ issues: [tracking()], comments: [ledger(status)] });
      assert.equal((await f.run()).dispatched, false);
      assert.equal(f.state.writes.filter((entry) => entry.type === 'dispatch').length, 0);
    });
  }
  const closed = fixture({ issues: [tracking(undefined, { state: 'closed' })] });
  assert.equal((await closed.run()).dispatched, false);
  assert.equal(closed.state.writes.length, 0);
});

test('stale attempts become visible terminal failures without another model dispatch; explicit retry works', async () => {
  const f = fixture({ issues: [tracking()], comments: [ledger('assessment-dispatched', { at: '2020-01-01T00:00:00Z' })] });
  assert.equal((await f.run()).dispatched, false);
  assert.ok(f.state.writes.some((entry) => entry.type === 'comment' && entry.body.includes('"status": "assessment-failed"')));
  const retry = fixture({ issues: [tracking()], comments: [ledger('assessment-failed')] });
  assert.equal((await retry.run({ GDK_WATCH_INPUTS: JSON.stringify({ release_tag: release().tag_name, retry: true }) })).dispatched, true);
  const active = fixture({ issues: [tracking()], comments: [ledger('assessment-dispatched')] });
  assert.equal((await active.run({ GDK_WATCH_INPUTS: JSON.stringify({ release_tag: release().tag_name, retry: true }) })).dispatched, false);
});

test('dispatch failure is recorded terminally and propagates rather than hiding API errors', async () => {
  const f = fixture({ dispatchError: true });
  await assert.rejects(f.run(), /dispatch failed/);
  assert.ok(f.state.writes.some((entry) => entry.type === 'comment' && entry.body.includes('"status": "assessment-failed"')));
  const bad = fixture({ listError: Object.assign(new Error('API denied'), { status: 403 }) });
  await assert.rejects(bad.run(), /API denied/);
});

test('ambiguous issue creation recovers durable bot identity instead of double creating', async () => {
  const f = fixture({ ambiguousIssue: true });
  assert.equal((await f.run()).dispatched, true);
  assert.equal(f.state.writes.filter((entry) => entry.type === 'issue').length, 1);
  assert.match(f.core.warnings.join(), /ambiguous creation/);
});

test('later notes are attached once without duplicate issues or automatic reassessment', async () => {
  const f = fixture({ issues: [tracking()], comments: [ledger('needs-review')] });
  await f.run();
  await f.run();
  const attachments = f.state.writes.filter((entry) => entry.type === 'comment' && entry.body.includes('-notes id='));
  assert.equal(attachments.length, 1);
  assert.equal(f.state.writes.filter((entry) => ['dispatch', 'issue'].includes(entry.type)).length, 0);
});

test('advancing main drops older pending candidates without closing or rewriting issues', async () => {
  const f = fixture({ pin: { edition: '260405', windowsVersion: '2604.5.8000', coreVersion: '2604.5.8000' }, issues: [tracking()], comments: [] });
  assert.equal((await f.run()).discovery.candidates.length, 0);
  assert.equal(f.state.writes.length, 0);
});

test('explicit older maintenance is outside automatic queue and still compares to exact main', async () => {
  const f = fixture({ pin: { edition: '260406', windowsVersion: '2604.6.8000', coreVersion: '2604.6.8000' } });
  const result = await f.run({ GDK_WATCH_INPUTS: JSON.stringify({ release_tag: release().tag_name }) });
  assert.equal(result.dispatched, true);
  const issue = f.state.writes.find((entry) => entry.type === 'issue');
  assert.match(issue.body, /Explicit older-release maintenance/);
  assert.match(issue.body, /2604.6.8000/);
});

test('main moving during discovery fails closed before writes', async () => {
  const f = fixture({ mainMoved: true });
  await assert.rejects(f.run(), /Main moved/);
  assert.equal(f.state.writes.length, 0);
});

test('all release pages are inspected even when unsorted; a truncated listing is a visible error', async () => {
  const filler = Array.from({ length: 100 }, (_, id) => ({ id, tag_name: `unrelated-${id}` }));
  const f = fixture({ releasePages: [filler, [release()]] });
  assert.equal((await watch.listUpstreamReleases(f.github)).length, 101);
  const truncated = fixture({ releasePages: Array.from({ length: 20 }, () => filler) });
  await assert.rejects(watch.listUpstreamReleases(truncated.github), /pagination limit/);
});

test('configured metadata source is used explicitly with no fallback or package archive downloads', async () => {
  const f = fixture();
  await f.run({ GDK_NUGET_INDEX_URL: 'https://feed.example.test/v3/index.json' });
  assert.equal(f.state.fetches.length, 3);
  assert.equal(f.state.fetches[0], 'https://feed.example.test/v3/index.json');
  assert.ok(f.state.fetches.every((url) => url.endsWith('index.json')));
  let calls = 0;
  await assert.rejects(watch.listPackageVersions({ env: {}, fetchImpl: async () => { calls += 1; throw new Error('TLS failure'); } }), /TLS failure/);
  assert.equal(calls, 1);
  await assert.rejects(watch.listPackageVersions({ env: { GDK_NUGET_INDEX_URL: 'http://feed.example.test/index.json' }, fetchImpl: f.fetchImpl }), /HTTPS/);
});

test('only bot-owned valid ledgers and issues count; duplicate bot identities fail visibly', async () => {
  const human = { ...ledger('needs-review'), user: { login: 'maintainer' } };
  assert.equal(watch.latestState([human], 'pkg-2604.5.7903.0'), null);
  const f = fixture({ issues: [tracking(), tracking(undefined, { number: 9 })] });
  await assert.rejects(f.run(), /Multiple bot-owned/);
  assert.throws(() => watch.latestState([{ ...ledger('needs-review'), body: `${watch.stateMarkerPrefix('pkg-2604.5.7903.0')}status=needs-review -->` }], 'pkg-2604.5.7903.0'), /malformed/);
});
