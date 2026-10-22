'use strict';

const crypto = require('node:crypto');

const TARGET_REPO = 'microsoft/XBOX-GDK-dotnet';
const UPSTREAM_OWNER = 'microsoft';
const UPSTREAM_REPO = 'GDK';
const TRUSTED_REF = 'refs/heads/main';
const TRACKING_LABEL = 'gdk-release';
const DEFAULT_BOT_LOGIN = 'github-actions[bot]';
const ASSESS_WORKFLOW = 'gdk-release-assess.lock.yml';
const MARKER_NAME = 'xbox-gdk-dotnet-release';
const STATE_MARKER_NAME = `${MARKER_NAME}-state`;
const ATTEMPT_PATTERN = /^[0-9]+\.[0-9]+$/;
const STATUSES = new Set(['assessment-dispatched', 'assessment-failed', 'changes-required', 'tests-only', 'needs-review']);
const LIMITS = Object.freeze({ maxReleasePages: 20, releasesPerPage: 100, assessmentTimeoutMs: 6 * 60 * 60 * 1000 });
const MONTHS = ['january', 'february', 'march', 'april', 'may', 'june', 'july', 'august', 'september', 'october', 'november', 'december'];
const TAG_PATTERN = /^([A-Za-z]+)-(20\d{2})(?:-Update-(\d{1,2}))?-v(\d{4}\.\d{1,2}\.\d{1,9}(?:\.\d{1,9})?)$/;

class WatchError extends Error {}

function parseVersion(raw) {
  const match = /^(\d{4})\.(\d{1,2})\.(\d{1,9})(?:\.(\d{1,9}))?$/.exec(String(raw || ''));
  if (!match) return null;
  const parts = match.slice(1).map((part) => Number(part || 0));
  const month = parts[0] % 100;
  if (month < 1 || month > 12) return null;
  return { version: String(raw), parts, key: parts.join('.'), family: parts[0], edition: `${parts[0]}${String(parts[1]).padStart(2, '0')}` };
}

function compareVersions(left, right) {
  const a = parseVersion(typeof left === 'string' ? left : left.version);
  const b = parseVersion(typeof right === 'string' ? right : right.version);
  if (!a || !b) throw new WatchError('Cannot compare malformed GDK versions.');
  for (let i = 0; i < 4; i += 1) if (a.parts[i] !== b.parts[i]) return a.parts[i] - b.parts[i];
  return 0;
}

function parsePin(pin) {
  if (!pin || typeof pin !== 'object' || Array.isArray(pin)) throw new WatchError('The main GDK pin is not an object.');
  const windows = parseVersion(pin.windowsVersion);
  const core = parseVersion(pin.coreVersion);
  if (!windows || !core || windows.key !== core.key || typeof pin.edition !== 'string' || pin.edition !== windows.edition) {
    throw new WatchError('The main GDK pin must name an edition and one exact matching Windows/Core version pair.');
  }
  return { ...pin, version: windows.version, key: windows.key, family: windows.family };
}

function sanitizeReleaseTitle(raw) {
  return String(raw || '').replace(/[\u0000-\u001f\u007f-\u009f\u2028\u2029`|<>]/g, ' ').replace(/\s+/g, ' ').trim().slice(0, 200);
}

function parseGdkTag(tag) {
  const match = TAG_PATTERN.exec(String(tag || ''));
  if (!match) return null;
  const [, month, year, update, version] = match;
  const parsed = parseVersion(version);
  if (!parsed || MONTHS.indexOf(month.toLowerCase()) + 1 !== parsed.family % 100 ||
      Number(year) % 100 !== Math.floor(parsed.family / 100) || Number(update || 0) !== parsed.parts[1]) {
    return { conflict: 'release tag date/update disagrees with its SDK version' };
  }
  return { ...parsed, releaseLabel: `${month} ${year}${update ? ` Update ${Number(update)}` : ''}` };
}

function classifyRelease(release) {
  if (!release || release.draft || release.prerelease) return { status: 'ignored', reason: 'draft or prerelease' };
  const parsed = parseGdkTag(release.tag_name);
  if (!parsed) return { status: 'ignored', reason: 'not a versioned GDK release' };
  if (parsed.conflict) return { status: 'conflict', reason: parsed.conflict };
  if (!/^\d+$/.test(String(release.id || ''))) return { status: 'conflict', reason: 'upstream release id is not numeric' };
  const assets = (release.assets || []).filter((asset) => /^GDK_.*\.zip$/i.test(asset.name || ''));
  if (assets.length !== 1 || !parseVersion(assets[0].name.replace(/^GDK_/i, '').replace(/\.zip$/i, '')) ||
      compareVersions(assets[0].name.replace(/^GDK_/i, '').replace(/\.zip$/i, ''), parsed.version) !== 0) {
    return { status: 'conflict', reason: 'SDK archive identity is missing or disagrees with the tag' };
  }
  const expectedUrl = `https://github.com/${UPSTREAM_OWNER}/${UPSTREAM_REPO}/releases/tag/${release.tag_name}`;
  if (release.html_url !== expectedUrl) return { status: 'conflict', reason: 'release URL does not match the upstream tag' };
  return { status: 'eligible', release: { ...parsed, id: String(release.id), tag: release.tag_name,
    name: sanitizeReleaseTitle(release.name), url: expectedUrl, body: String(release.body || ''),
    publishedAt: release.published_at || null, asset: assets[0].name } };
}

function selectCandidates({ releases, windowsVersions, coreVersions, pin }) {
  const state = parsePin(pin);
  const conflicts = [];
  const notes = new Map();
  for (const entry of releases) {
    const result = classifyRelease(entry);
    if (result.status === 'conflict') conflicts.push({ tag: sanitizeReleaseTitle(entry.tag_name), reason: result.reason });
    if (result.status !== 'eligible') continue;
    const list = notes.get(result.release.key) || [];
    list.push(result.release);
    notes.set(result.release.key, list);
  }
  const core = new Map();
  for (const version of coreVersions) {
    const parsed = parseVersion(version);
    if (parsed) core.set(parsed.key, version);
  }
  const candidates = new Map();
  for (const version of windowsVersions) {
    const parsed = parseVersion(version);
    // Do not let a later publication date resurrect servicing of an older family.
    if (!parsed || parsed.family < state.family || compareVersions(version, state.version) <= 0) continue;
    const correlated = notes.get(parsed.key) || [];
    const gaps = [];
    if (!core.has(parsed.key)) gaps.push('Matching Microsoft.GDK.Core package is missing (feed lag or incomplete pair).');
    if (correlated.length === 0) gaps.push('Public GDK release notes are not yet available for this exact SDK build.');
    if (correlated.length > 1) gaps.push('Multiple public releases correlate with this exact SDK build.');
    const note = correlated.length === 1 ? correlated[0] : null;
    if (note && !note.body.trim()) gaps.push('The correlated public release has no release notes.');
    const candidate = { ...parsed, id: `pkg-${parsed.key}`, windowsVersion: version, coreVersion: core.get(parsed.key) || null,
      tag: note ? note.tag : null, releaseId: note ? note.id : null, name: note ? note.name : `GDK ${version}`,
      url: note ? note.url : null, asset: note ? note.asset : null, body: note ? note.body : '',
      releaseLabel: note ? note.releaseLabel : parsed.edition, gaps, eligible: Boolean(core.has(parsed.key)) };
    candidates.set(parsed.key, candidate);
  }
  // Notes can precede both packages. Make that feed lag visible without inventing an assessable package.
  for (const [key, entries] of notes) {
    if (!candidates.has(key) && entries[0].family >= state.family && compareVersions(entries[0].version, state.version) > 0) {
      conflicts.push({ tag: entries[0].tag, reason: 'Public release has no matching Windows package in the configured feed.' });
    }
  }
  return { candidates: [...candidates.values()].sort((a, b) => compareVersions(b, a)), conflicts, pin: state };
}

function safeJson(text, name) {
  try { return JSON.parse(text); } catch { throw new WatchError(`${name} is not valid JSON.`); }
}

function readWatchInputs(env) {
  const raw = safeJson(env.GDK_WATCH_INPUTS || '{}', 'GDK_WATCH_INPUTS');
  if (!raw || typeof raw !== 'object' || Array.isArray(raw)) throw new WatchError('Watcher inputs must be an object.');
  const tag = String(raw.release_tag || '').trim();
  if (tag && !TAG_PATTERN.test(tag)) throw new WatchError('release_tag is not a versioned GDK release tag.');
  const retry = raw.retry === true || raw.retry === 'true';
  if (retry && !tag) throw new WatchError('retry requires release_tag.');
  return { releaseTag: tag || null, retry, preview: raw.preview === true || raw.preview === 'true' };
}

function evaluateTrustedContext({ context, env }) {
  const repository = `${context.repo.owner}/${context.repo.repo}`;
  if (repository !== TARGET_REPO) return { trusted: false, reason: 'not the target repository' };
  if (env.GITHUB_REF !== TRUSTED_REF) return { trusted: false, reason: 'not trusted main' };
  return { trusted: true };
}

async function readMainPin(github) {
  const [owner, repo] = TARGET_REPO.split('/');
  const { data: branch } = await github.rest.repos.getBranch({ owner, repo, branch: 'main' });
  const sha = branch && branch.commit && branch.commit.sha;
  if (!/^[a-f0-9]{40}$/.test(sha || '')) throw new WatchError('Trusted main returned no exact commit.');
  const { data } = await github.rest.repos.getContent({ owner, repo, path: '.github/gdk-version.json', ref: sha });
  if (!data || data.encoding !== 'base64' || typeof data.content !== 'string') throw new WatchError('Trusted main pin is unavailable.');
  return { pin: parsePin(safeJson(Buffer.from(data.content, 'base64').toString('utf8'), 'Trusted main pin')), sha };
}

function metadataUrl(raw) {
  let url;
  try { url = new URL(raw); } catch { throw new WatchError('Configured NuGet metadata URL is invalid.'); }
  if (url.protocol !== 'https:' || url.username || url.password || url.hash) throw new WatchError('NuGet metadata requires credential-free HTTPS URLs.');
  return url;
}

async function fetchMetadata(url, fetchImpl) {
  const response = await fetchImpl(metadataUrl(url).href, { redirect: 'error', signal: AbortSignal.timeout(30000) });
  if (!response.ok) throw new WatchError(`Configured NuGet metadata request failed (HTTP ${response.status}); no fallback was attempted.`);
  return response.json();
}

async function listPackageVersions({ env, fetchImpl = globalThis.fetch }) {
  const index = await fetchMetadata(env.GDK_NUGET_INDEX_URL || 'https://api.nuget.org/v3/index.json', fetchImpl);
  const bases = [...new Set((index.resources || []).filter((resource) => resource['@type'] === 'PackageBaseAddress/3.0.0')
    .map((resource) => resource['@id']))];
  if (bases.length !== 1) throw new WatchError('Configured NuGet index must expose one unambiguous PackageBaseAddress/3.0.0.');
  const base = metadataUrl(bases[0]).href.replace(/\/?$/, '/');
  const result = {};
  for (const [name, id] of [['windowsVersions', 'microsoft.gdk.windows'], ['coreVersions', 'microsoft.gdk.core']]) {
    const payload = await fetchMetadata(`${base}${id}/index.json`, fetchImpl);
    if (!Array.isArray(payload.versions) || !payload.versions.every((item) => typeof item === 'string')) {
      throw new WatchError('Configured NuGet package metadata has no version list.');
    }
    result[name] = payload.versions;
  }
  return result;
}

async function listUpstreamReleases(github) {
  const releases = [];
  for (let page = 1; page <= LIMITS.maxReleasePages; page += 1) {
    const { data } = await github.rest.repos.listReleases({ owner: UPSTREAM_OWNER, repo: UPSTREAM_REPO, per_page: LIMITS.releasesPerPage, page });
    if (!Array.isArray(data)) throw new WatchError('Upstream releases payload is invalid.');
    releases.push(...data);
    if (data.length < LIMITS.releasesPerPage) return releases;
  }
  throw new WatchError('Upstream release pagination limit reached; discovery is incomplete.');
}

const markerPrefix = (id) => `<!-- ${MARKER_NAME} id=${id} `;
const stateMarkerPrefix = (id) => `<!-- ${STATE_MARKER_NAME} id=${id} `;
const isBotAuthored = (entity, login) => Boolean(entity && entity.user && entity.user.login === login);

function renderStateComment({ releaseId, state }) {
  if (!STATUSES.has(state.status)) throw new WatchError('Unknown assessment status.');
  return `${stateMarkerPrefix(releaseId)}status=${state.status} -->\n**Watcher state:** \`${state.status}\`\n\n\`\`\`json\n${JSON.stringify({ ...state, release: releaseId }, null, 2)}\n\`\`\`\n`;
}

function latestState(comments, releaseId, botLogin = DEFAULT_BOT_LOGIN) {
  for (const comment of [...comments].sort((a, b) => Number(b.id) - Number(a.id))) {
    if (!isBotAuthored(comment, botLogin) || !String(comment.body || '').startsWith(stateMarkerPrefix(releaseId))) continue;
    const match = /```json\n([\s\S]*?)\n```/.exec(comment.body);
    if (!match) throw new WatchError('Bot-owned ledger state is malformed.');
    const state = safeJson(match[1], 'Watcher ledger');
    if (!state || state.release !== releaseId || !STATUSES.has(state.status)) throw new WatchError('Bot-owned ledger state has an invalid identity/status.');
    return { state, comment };
  }
  return null;
}

async function listTrackingIssues({ github, owner, repo, botLogin = DEFAULT_BOT_LOGIN }) {
  const issues = await github.paginate(github.rest.issues.listForRepo, { owner, repo, state: 'all', labels: TRACKING_LABEL, per_page: 100 });
  const result = new Map();
  for (const issue of issues) {
    if (issue.pull_request || !isBotAuthored(issue, botLogin)) continue;
    const match = /^<!-- xbox-gdk-dotnet-release id=(pkg-[0-9.]+) /.exec(issue.body || '');
    if (!match) continue;
    if (result.has(match[1])) throw new WatchError(`Multiple bot-owned tracking issues for ${match[1]}; resolve ambiguity manually.`);
    result.set(match[1], issue);
  }
  return result;
}

function snapshotId({ candidate, pin, sha }) {
  return crypto.createHash('sha256').update(JSON.stringify({ candidate: candidate.id, windows: candidate.windowsVersion,
    core: candidate.coreVersion, tag: candidate.tag, body: candidate.body, pin, sha })).digest('hex');
}

function staleAttemptStatus(state, now = Date.now()) {
  if (!state || state.status !== 'assessment-dispatched') return null;
  const date = Date.parse(state.at || '');
  return !Number.isFinite(date) || now - date > LIMITS.assessmentTimeoutMs ? 'assessment timed out or has no start time' : null;
}

function assessmentDecision({ record, retry = false }) {
  if (!record) return { dispatch: true, reason: 'new candidate' };
  if (record.issue.state === 'closed') return { dispatch: false, reason: 'closed issue opts out' };
  if (!record.state) return { dispatch: true, reason: 'no prior attempt' };
  if (record.state.status === 'assessment-dispatched' && !record.staleAttempt) return { dispatch: false, reason: 'assessment in flight' };
  if (retry) return { dispatch: true, reason: 'explicit retry' };
  if (record.staleAttempt) return { dispatch: false, reason: record.staleAttempt, stalled: true };
  return { dispatch: false, reason: `terminal ${record.state.status}; explicit retry required` };
}

function assessmentAttemptKey({ runId, runAttempt }) {
  const key = `${runId}.${runAttempt}`;
  if (!ATTEMPT_PATTERN.test(key)) throw new WatchError('Assessment attempt requires numeric runId.runAttempt.');
  return key;
}

function renderTrackingIssueBody({ release, pin, mainSha, runUrl, maintenance }) {
  return [
    `${markerPrefix(release.id)}version=${release.version} -->`,
    `## GDK ${release.version} — advisory .NET assessment`, '',
    `Windows/Core exact pair: \`${release.windowsVersion}\` / \`${release.coreVersion}\`.`,
    `Comparison: trusted main \`${mainSha}\`, edition \`${pin.edition}\`, exact pair \`${pin.windowsVersion}\` / \`${pin.coreVersion}\`.`,
    release.url ? `Public release: ${release.url}` : 'Public release notes are not yet correlated; this package identity stays unchanged when notes arrive.',
    maintenance ? '**Explicit older-release maintenance investigation, outside the automatic latest-only queue.**' : 'Latest-only main investigation; this does not claim SDK support.',
    ...release.gaps.map((gap) => `- Evidence gap: ${gap}`), '',
    'No upgrade, pull request, publication or runtime validation is authorized. A reviewed pin change must include mandatory dependency/onboarding, generation and validation work.',
    'Preserve immutable old packages/tags. Older fixes start from the exact release tag only when needed.',
    'Closing this bot-owned issue opts out. Reports are immutable snapshots; retries require release_tag and retry.',
    `Watcher run: ${runUrl}`, '',
  ].join('\n');
}

async function ensureTrackingIssue({ github, core, owner, repo, release, pin, mainSha, runUrl, maintenance, existing }) {
  if (existing) return existing;
  try {
    await github.rest.issues.getLabel({ owner, repo, name: TRACKING_LABEL });
  } catch (error) {
    if (error.status !== 404) throw error;
    try {
      await github.rest.issues.createLabel({ owner, repo, name: TRACKING_LABEL, color: '1d76db', description: 'Advisory upstream GDK assessment' });
    } catch (createError) {
      if (createError.status !== 422) throw createError;
      await github.rest.issues.getLabel({ owner, repo, name: TRACKING_LABEL });
    }
  }
  try {
    const { data } = await github.rest.issues.create({ owner, repo, title: `GDK ${release.version}: assess .NET impact`,
      body: renderTrackingIssueBody({ release, pin, mainSha, runUrl, maintenance }), labels: [TRACKING_LABEL] });
    return data;
  } catch (error) {
    // A failed response may follow a successful create. Recover by durable identity; never blindly repeat.
    const recovered = (await listTrackingIssues({ github, owner, repo })).get(release.id);
    if (!recovered) throw error;
    core.warning('Recovered tracking issue after an ambiguous creation response.');
    return recovered;
  }
}

async function runWatch({ github, context, core, env, fetchImpl = globalThis.fetch }) {
  const inputs = readWatchInputs(env);
  const trust = evaluateTrustedContext({ context, env });
  const { pin, sha } = await readMainPin(github);
  const preview = inputs.preview || env.GDK_WATCH_MODE !== 'post' || !trust.trusted || env.GITHUB_SHA !== sha;
  const releases = await listUpstreamReleases(github);
  const packages = await listPackageVersions({ env, fetchImpl });
  let discovery = selectCandidates({ releases, ...packages, pin });
  let maintenance = false;
  if (inputs.releaseTag) {
    const upstream = releases.find((entry) => entry.tag_name === inputs.releaseTag);
    const classified = classifyRelease(upstream);
    if (classified.status !== 'eligible') throw new WatchError('Explicit release tag is missing or ambiguous.');
    // Explicit older investigation still compares with main, never an inferred supported baseline.
    if (compareVersions(classified.release.version, pin.version) <= 0 || classified.release.family < pin.family) {
      maintenance = true;
      const floor = { edition: '200100', windowsVersion: '2001.0.0', coreVersion: '2001.0.0' };
      discovery = selectCandidates({ releases, ...packages, pin: floor });
    }
    discovery.candidates = discovery.candidates.filter((entry) => entry.key === classified.release.key);
    if (discovery.candidates.length !== 1) throw new WatchError('Explicit release has no Windows package in the configured feed.');
  }
  const { owner, repo } = context.repo;
  const issues = await listTrackingIssues({ github, owner, repo });
  const decisions = new Map();
  const pending = [];
  if (!preview && (await readMainPin(github)).sha !== sha) throw new WatchError('Main moved during discovery; rerun against its new exact pin.');
  for (const candidate of discovery.candidates) {
    const issue = issues.get(candidate.id);
    let record = null;
    if (issue) {
      const comments = await github.paginate(github.rest.issues.listComments, { owner, repo, issue_number: issue.number, per_page: 100 });
      const found = latestState(comments, candidate.id);
      record = { issue, state: found && found.state };
      record.staleAttempt = staleAttemptStatus(record.state);
      // Package publication can precede public notes. Attach later provenance without
      // changing the issue identity, rewriting an earlier report, or buying another run.
      const notesMarker = `<!-- ${MARKER_NAME}-notes id=${candidate.id} release=${candidate.releaseId} -->`;
      if (!preview && issue.state !== 'closed' && candidate.tag && !String(issue.body || '').includes(candidate.url) &&
          !comments.some((comment) => isBotAuthored(comment, DEFAULT_BOT_LOGIN) && String(comment.body || '').startsWith(notesMarker))) {
        await github.rest.issues.createComment({ owner, repo, issue_number: issue.number,
          body: `${notesMarker}\nPublic notes are now correlated with the same exact package: ${candidate.url}\nExisting reports remain historical snapshots. A fresh assessment requires explicit release_tag + retry.\n` });
      }
    }
    const decision = candidate.eligible ? assessmentDecision({ record, retry: inputs.retry }) :
      { dispatch: false, reason: 'missing exact Windows/Core pair' };
    decisions.set(candidate.id, decision);
    if (decision.stalled && !preview && issue.state !== 'closed') {
      await github.rest.issues.createComment({ owner, repo, issue_number: issue.number,
        body: renderStateComment({ releaseId: candidate.id, state: { ...record.state, status: 'assessment-failed', note: decision.reason, at: new Date().toISOString() } }) });
    }
    if (decision.dispatch) pending.push({ candidate, issue });
  }
  const selected = pending[0] || null;
  const runUrl = `https://github.com/${owner}/${repo}/actions/runs/${env.GITHUB_RUN_ID}`;
  if (selected && !preview) {
    // Recheck main immediately before any candidate/dispatch write.
    if ((await readMainPin(github)).sha !== sha) throw new WatchError('Main moved during discovery; rerun against its new exact pin.');
    const issue = await ensureTrackingIssue({ github, core, owner, repo, release: selected.candidate, pin, mainSha: sha, runUrl, maintenance, existing: selected.issue });
    const attempt = assessmentAttemptKey({ runId: env.GITHUB_RUN_ID, runAttempt: env.GITHUB_RUN_ATTEMPT });
    const state = { status: 'assessment-dispatched', attempt, snapshot: snapshotId({ candidate: selected.candidate, pin, sha }),
      mainSha: sha, runId: env.GITHUB_RUN_ID, runUrl, maintenance, at: new Date().toISOString() };
    await github.rest.issues.createComment({ owner, repo, issue_number: issue.number, body: renderStateComment({ releaseId: selected.candidate.id, state }) });
    try {
      await github.rest.actions.createWorkflowDispatch({ owner, repo, workflow_id: ASSESS_WORKFLOW, ref: 'main',
        inputs: { release_id: selected.candidate.id, release_tag: selected.candidate.tag || '', issue_number: String(issue.number), attempt } });
    } catch (error) {
      await github.rest.issues.createComment({ owner, repo, issue_number: issue.number,
        body: renderStateComment({ releaseId: selected.candidate.id, state: { ...state, status: 'assessment-failed', note: 'Dispatch response failed or was ambiguous; explicit retry only.', at: new Date().toISOString() } }) });
      throw error;
    }
  }
  const lines = [`Publishing: ${preview ? 'staged (no writes)' : 'post'}. Main exact baseline: ${pin.windowsVersion} / ${pin.coreVersion} (${sha}).`,
    maintenance ? 'Explicit maintenance investigation; outside the automatic queue.' : 'Newest pending candidate only; maximum one assessment dispatch.',
    ...discovery.candidates.map((candidate) => `- ${candidate.version}: ${decisions.get(candidate.id).reason}${selected && selected.candidate.id === candidate.id ? ' (selected)' : ''}${candidate.gaps.length ? `; ${candidate.gaps.join(' ')}` : ''}`),
    ...discovery.conflicts.map((conflict) => `- Metadata conflict: ${conflict.tag}: ${conflict.reason}`)];
  await core.summary.addHeading('GDK release watch', 2).addRaw(`\n${lines.join('\n')}\n`).write();
  for (const conflict of discovery.conflicts) core.warning(`Metadata conflict: ${conflict.tag}: ${conflict.reason}`);
  return { preview, selected: selected && selected.candidate, dispatched: Boolean(selected && !preview), discovery, decisions };
}

module.exports = { TARGET_REPO, UPSTREAM_OWNER, UPSTREAM_REPO, TRUSTED_REF, TRACKING_LABEL, DEFAULT_BOT_LOGIN,
  ASSESS_WORKFLOW, ATTEMPT_PATTERN, LIMITS, WatchError, parseVersion, compareVersions, parsePin, sanitizeReleaseTitle,
  parseGdkTag, classifyRelease, selectCandidates, readWatchInputs, evaluateTrustedContext, readMainPin,
  listPackageVersions, listUpstreamReleases, markerPrefix, stateMarkerPrefix, isBotAuthored, renderStateComment,
  latestState, listTrackingIssues, snapshotId, staleAttemptStatus, assessmentDecision, assessmentAttemptKey,
  renderTrackingIssueBody, ensureTrackingIssue, runWatch };
