const { execFileSync } = require('node:child_process');
const fs = require('node:fs');

const lanes = ['build', 'aot', 'docs', 'automation'];
const all = () => Object.fromEntries(lanes.map(lane => [lane, true]));

function select(paths, full = false) {
  if (!Array.isArray(paths) || paths.some(p => typeof p !== 'string' || !p)) {
    throw new Error('Changed paths must be a list of nonempty strings.');
  }
  const selected = Object.fromEntries(lanes.map(lane => [lane, false]));
  const reasons = [];
  if (full) return { ...all(), reasons: ['Full validation requested.'] };
  for (const original of paths) {
    const path = original.replaceAll('\\', '/');
    let required;
    if (path === '.github/workflows/ci.yml' || /^eng\/ci\/(?:pr_gates|native[-_])/.test(path)) {
      required = lanes;
    } else if (/^(src|tests\/GDK\.Net|samples)\//.test(path) ||
               /^tests\/GDK\.Net[^/]*\//.test(path) ||
               /(?:^|\/)(?:Directory\..*\.(?:props|targets)|global\.json|[Nn]u[Gg]et\.[Cc]onfig|.*\.(?:csproj|slnx|sln))$/.test(path) ||
               path === '.config/dotnet-tools.json') {
      required = lanes;
    } else if (path === '.github/gdk-version.json') {
      required = lanes;
    } else if (/^docs\/api\//.test(path) ||
               /^eng\/(?:generate-docs\.ps1|api-layout\.ps1|api-areas\.json|docfx\.json|gdk-learn-index\.json|update-learn-index\.ps1)$/.test(path)) {
      required = ['build', 'docs'];
    } else if (/^eng\/(?:generate-|playfab\/|packaging\/|package\.ps1|run-|build\.ps1|resolve-|get-gdk)/.test(path)) {
      required = ['build', 'aot', 'docs'];
    } else if (/^eng\/ci\/(?:tests\/)?(?:issue_triage(?:_(?:eval|workflows))?|gdk_release_(?:watch|assess))(?:\.test)?\.cjs$/.test(path) ||
               /^tests\/evals\/issue-triage\//.test(path) ||
               /^\.github\/workflows\/(?:issue-triage(?:-eval)?|gdk-release-assess)\.(?:md|lock\.yml)$/.test(path) ||
               path === '.github/workflows/gdk-release-watch.yml' ||
               path === '.github/aw/actions-lock.json' ||
               /^\.github\/skills\/issue-triage\//.test(path)) {
      required = ['automation'];
    } else if (/^(?:eng\/ci\/|tests\/evals\/|\.github\/(?:workflows\/|skills\/|aw\/))/.test(path)) {
      required = lanes;
    } else if (/^(?:docs\/|\.github\/ISSUE_TEMPLATE\/)/.test(path) ||
               path === '.github/PULL_REQUEST_TEMPLATE.md' ||
               /\.md$/i.test(path)) {
      required = [];
    } else {
      required = lanes;
    }
    for (const lane of required) selected[lane] = true;
    reasons.push(`${path}: ${required.length ? required.join(', ') : 'static checks only'}`);
  }
  return { ...selected, reasons };
}

function changedPaths(text) {
  if (typeof text !== 'string') throw new Error('Invalid Git diff.');
  if (!text) return [];
  const fields = text.split('\0');
  if (fields.pop() !== '') throw new Error('Truncated Git diff.');
  const paths = [];
  while (fields.length) {
    const status = fields.shift();
    if (!/^(?:[ACDMTUXB]|[RC]\d+)$/.test(status)) throw new Error(`Invalid Git diff status: ${status}`);
    const count = /^[RC]/.test(status) ? 2 : 1;
    for (let i = 0; i < count; i++) {
      const path = fields.shift();
      if (!path) throw new Error('Missing path in Git diff.');
      paths.push(path);
    }
  }
  return paths;
}

function aggregate(selection, results) {
  for (const lane of lanes) {
    if (typeof selection[lane] !== 'boolean') throw new Error(`Invalid selection: ${lane}`);
  }
  for (const required of ['scope', 'lint']) {
    if (results[required] !== 'success') throw new Error(`${required} did not succeed.`);
  }
  for (const lane of lanes) {
    const expected = selection[lane] ? 'success' : 'skipped';
    if (results[lane] !== expected) {
      throw new Error(`${lane}: expected ${expected}, received ${results[lane] ?? 'missing'}.`);
    }
  }
}

function git(...args) {
  return execFileSync('git', args, {
    encoding: 'utf8', maxBuffer: 16 * 1024 * 1024, stdio: ['ignore', 'pipe', 'pipe'],
  });
}

function sha(value) {
  if (!/^[0-9a-f]{40}$/i.test(value ?? '') || /^0+$/.test(value)) throw new Error('Invalid commit SHA.');
  return value;
}

function eventSelection(eventName, event, env = process.env) {
  if (eventName === 'workflow_dispatch' || env.GITHUB_REF?.startsWith('refs/tags/')) return select([], true);
  if (eventName === 'pull_request') {
    const base = sha(event.pull_request?.base?.sha);
    const head = sha(event.pull_request?.head?.sha);
    const mergeBase = git('merge-base', base, head).trim();
    return { ...select(changedPaths(git('diff', '--name-status', '-z', mergeBase, head))), evidenceBase: base };
  }
  if (eventName === 'push') {
    const after = sha(event.after);
    if (!event.before || /^0+$/.test(event.before)) return select([], true);
    const before = sha(event.before);
    try {
      git('cat-file', '-e', `${before}^{commit}`);
    } catch (error) {
      if (![1, 128].includes(error.status)) throw error;
      return { ...select([], true), reasons: ['Push baseline unavailable; full validation required.'] };
    }
    return { ...select(changedPaths(git('diff', '--name-status', '-z', before, after))), evidenceBase: before };
  }
  throw new Error(`Unsupported event: ${eventName}`);
}

function main() {
  const command = process.argv[2];
  if (command === 'scope') {
    const event = JSON.parse(fs.readFileSync(process.env.GITHUB_EVENT_PATH, 'utf8'));
    const selection = eventSelection(process.env.GITHUB_EVENT_NAME, event);
    const output = lanes.map(lane => `${lane}=${selection[lane]}`).join('\n');
    fs.appendFileSync(process.env.GITHUB_OUTPUT, `${output}\nevidence_base=${selection.evidenceBase ?? ''}\n`);
    fs.appendFileSync(process.env.GITHUB_STEP_SUMMARY,
      `## Selected PR gates\n\n${selection.reasons.map(reason => `- ${reason.replaceAll('`', "'").replaceAll('<', '&lt;').replaceAll('\n', ' ')}`).join('\n')}\n`);
  } else if (command === 'aggregate') {
    const needs = JSON.parse(process.env.NEEDS_JSON);
    const selection = Object.fromEntries(lanes.map(lane => {
      const value = needs.scope?.outputs?.[lane];
      if (!['true', 'false'].includes(value)) throw new Error(`Missing or malformed scope output: ${lane}`);
      return [lane, value === 'true'];
    }));
    const results = Object.fromEntries(Object.entries(needs).map(([name, job]) => [name, job.result]));
    aggregate(selection, results);
  } else {
    throw new Error('Usage: node eng/ci/pr_gates.cjs scope|aggregate');
  }
}

module.exports = { select, changedPaths, aggregate, eventSelection };
if (require.main === module) main();
