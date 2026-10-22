// Contexts contain digests, never SDK payloads, local paths or live-account information.
const fs = require('node:fs');
const path = require('node:path');
const crypto = require('node:crypto');
const { execFileSync } = require('node:child_process');
const { parseArgs } = require('node:util');

const checks = ['xuser-pilot', 'pfmp-lobby-state-changes'];
const digest = (value, algorithm = 'sha256', encoding = 'hex') =>
  crypto.createHash(algorithm).update(value).digest(encoding);
const canonical = value => {
  if (Array.isArray(value)) return `[${value.map(canonical).join(',')}]`;
  if (value && typeof value === 'object') {
    return `{${Object.keys(value).sort().map(key => `${JSON.stringify(key)}:${canonical(value[key])}`).join(',')}}`;
  }
  return JSON.stringify(value);
};

function keys(value, expected, label) {
  if (!value || typeof value !== 'object' || Array.isArray(value) ||
      Object.keys(value).sort().join(',') !== [...expected].sort().join(',')) {
    throw new Error(`Invalid ${label} fields; do not include identifiers, comments or URLs.`);
  }
}

function trackedFiles(root) {
  const deleted = new Set(execFileSync('git', ['ls-files', '--deleted', '-z'],
    { cwd: root, encoding: 'utf8' }).split('\0').filter(Boolean));
  return execFileSync('git', ['ls-files', '--cached', '--others', '--exclude-standard', '-z', '--', 'src', 'samples', 'tests', 'eng',
    'Directory.Build.props', 'Directory.Build.targets', 'global.json', 'NuGet.Config', '.config',
    '.python-version', '.github/gdk-version.json'],
  { cwd: root, encoding: 'utf8' }).split('\0').filter(file => file && !deleted.has(file));
}

function hashFiles(root, files, label) {
  if (!Array.isArray(files) || !files.length) throw new Error(`Missing ${label} inputs.`);
  const entries = files.map(file => {
    const absolute = path.resolve(root, file);
    const relative = path.relative(root, absolute).replaceAll('\\', '/');
    if (!relative || relative.startsWith('../') || path.isAbsolute(relative)) {
      throw new Error(`${label} inputs must be repository-relative.`);
    }
    if (!fs.statSync(absolute).isFile()) throw new Error(`Invalid ${label} input.`);
    return [relative, digest(fs.readFileSync(absolute))];
  }).sort(([a], [b]) => a.localeCompare(b, 'en'));
  if (new Set(entries.map(([name]) => name.toLowerCase())).size !== entries.length) {
    throw new Error(`Duplicate ${label} input.`);
  }
  return { sha256: digest(canonical(entries)), files: entries.length };
}

/**
 * Generate a context from real candidate archives and rebuild inputs. Promotion/attestation
 * metadata is deliberately not an input: adding an attestation must not change its own digest.
 * dependencyFiles must identify the two original NuGet archives, not sidecar hash assertions.
 */
function generateEvidenceContext({ root, pin, packages, dependencyFiles, sourceFiles, generatorFiles }) {
  if (!pin || !/^\d{6}$/.test(pin.edition) ||
      !/^\d{4}\.\d+\.\d+$/.test(pin.windowsVersion) || pin.windowsVersion !== pin.coreVersion ||
      pin.edition !== `${pin.windowsVersion.split('.')[0]}${pin.windowsVersion.split('.')[1].padStart(2, '0')}`) {
    throw new Error('Invalid exact Windows/Core pin.');
  }
  keys(dependencyFiles, ['windows', 'core'], 'dependency archive');
  const dependencies = {};
  for (const kind of ['windows', 'core']) {
    const bytes = fs.readFileSync(dependencyFiles[kind]);
    const sha512 = digest(bytes, 'sha512', 'base64');
    if (sha512 !== pin[`${kind}Sha512`]) throw new Error(`${kind} archive differs from the selected pin.`);
    dependencies[kind] = { version: pin[`${kind}Version`], sha512 };
  }
  if (!Array.isArray(packages) || !packages.length) throw new Error('Candidate packages are required.');
  const candidates = packages.map(candidate => {
    keys(candidate, ['id', 'path'], 'candidate package');
    if (candidate.id !== 'GDK.Net' ||
        !candidate.path.toLowerCase().endsWith('.nupkg')) throw new Error('Invalid candidate package.');
    const bytes = fs.readFileSync(candidate.path);
    if (bytes.length < 4 || bytes.readUInt32LE(0) !== 0x04034b50) throw new Error('Candidate is not a NuGet ZIP archive.');
    return { id: candidate.id, sha256: digest(bytes), bytes: bytes.length };
  }).sort((a, b) => a.id.localeCompare(b.id, 'en'));
  if (new Set(candidates.map(candidate => candidate.id.toLowerCase())).size !== candidates.length) {
    throw new Error('Duplicate candidate package.');
  }
  const tracked = sourceFiles && generatorFiles ? [] : trackedFiles(root);
  const context = {
    schemaVersion: 1,
    edition: pin.edition,
    dependencies,
    source: hashFiles(root, sourceFiles ?? tracked.filter(file => /^(src|samples|tests)\//.test(file)), 'source'),
    generators: hashFiles(root, generatorFiles ?? tracked.filter(file =>
      !/^(src|samples|tests)\//.test(file)), 'generator/dependency manifest'),
    packages: candidates
  };
  return { ...context, contextSha256: digest(canonical(context)) };
}

/**
 * A sanitized maintainer assertion, not proof that this tool ran a live test or authenticated a
 * signer. Callers set required only for an SDK-pin upgrade or publication/release.
 */
function validateAttestation({ context, attestation, required = true, now = new Date() }) {
  if (!required) return { status: 'not-required' };
  if (!attestation) throw new Error('Manual packaged-title evidence is required for SDK upgrades/releases.');
  const { contextSha256, ...inputs } = context ?? {};
  if (!/^[a-f0-9]{64}$/.test(contextSha256 ?? '') || digest(canonical(inputs)) !== contextSha256) {
    throw new Error('Invalid evidence context digest.');
  }
  keys(attestation, ['schemaVersion', 'contextSha256', 'performedAt', 'architecture', 'checks'], 'attestation');
  if (attestation.schemaVersion !== 1 || attestation.contextSha256 !== contextSha256) {
    throw new Error('Stale evidence: source, generator, dependencies or candidate package changed.');
  }
  if (!['x64', 'arm64'].includes(attestation.architecture) ||
      !/^\d{4}-\d\d-\d\dT\d\d:\d\d:\d\dZ$/.test(attestation.performedAt) ||
      !Number.isFinite(Date.parse(attestation.performedAt)) ||
      new Date(attestation.performedAt).toISOString().replace('.000', '') !== attestation.performedAt ||
      Date.parse(attestation.performedAt) > new Date(now).getTime()) throw new Error('Invalid manual test time/architecture.');
  if (!Array.isArray(attestation.checks) || attestation.checks.length !== checks.length) {
    throw new Error('Both packaged XUser and PFMP Lobby checks are required.');
  }
  for (const check of attestation.checks) {
    keys(check, ['id', 'result'], 'manual check');
    if (!checks.includes(check.id) || check.result !== 'passed') throw new Error('Manual check did not pass.');
  }
  if (new Set(attestation.checks.map(check => check.id)).size !== checks.length) throw new Error('Duplicate manual check.');
  return { status: 'validated', contextSha256, architecture: attestation.architecture };
}

function sdkIdentity(pin, label) {
  const fields = ['edition', 'windowsVersion', 'coreVersion', 'windowsSha512', 'coreSha512'];
  if (!pin || typeof pin !== 'object' || Array.isArray(pin) ||
      fields.some(key => typeof pin[key] !== 'string') ||
      !/^\d{6}$/.test(pin.edition) ||
      !/^\d{4}\.[1-9]\d?\.\d+$/.test(pin.windowsVersion) ||
      pin.windowsVersion !== pin.coreVersion ||
      pin.edition !== `${pin.windowsVersion.split('.')[0]}${pin.windowsVersion.split('.')[1].padStart(2, '0')}` ||
      ['windowsSha512', 'coreSha512'].some(key => !/^[A-Za-z0-9+/]{86}==$/.test(pin[key]) ||
        Buffer.from(pin[key], 'base64').toString('base64') !== pin[key])) {
    throw new Error(`${label} SDK pin is invalid.`);
  }
  return Object.fromEntries(fields.map(key => [key, pin[key]]));
}

function requiresEvidence({ basePin, currentPin, release = false }) {
  const current = sdkIdentity(currentPin, 'Current');
  const base = basePin === undefined || basePin === null ? basePin : sdkIdentity(basePin, 'Base');
  if (release) return true;
  if (base === null) {
    // Only these independently verified original NuGet archive bytes get the onboarding waiver.
    const approvedBaseline = {
      edition: '260404', windowsVersion: '2604.4.7897', coreVersion: '2604.4.7897',
      windowsSha512: 'NQ0AHyNt1cCyWp+m/BMoHUietfTigOVfxa2U1hbNCoDLiPGDkFm3bcDjk9jzBJswAqgcC+79wZnViQ68Ix3zQw==',
      coreSha512: 'DtC+96jjBvUFFP2MGAkchDYMrgl8+9wWa9Z1aXeBIGey++/YAt1RCTcWwJJJ7urU8Lb7aY402EOmbnDJsDsMSg=='
    };
    if (canonical(current) === canonical(approvedBaseline)) return false;
    throw new Error('Missing base pin is only valid for approved 260404 baseline onboarding.');
  }
  if (base === undefined) throw new Error('A verified base pin is required.');
  return canonical(base) !== canonical(current);
}

function readBasePin(root, base) {
  if (!/^[a-f0-9]{40}$/i.test(base ?? '')) throw new Error('An actual full base commit SHA is required.');
  const git = args => execFileSync('git', args, { cwd: root, encoding: 'utf8', stdio: ['ignore', 'pipe', 'pipe'] });
  git(['cat-file', '-e', `${base}^{commit}`]);
  const name = '.github/gdk-version.json';
  if (!git(['ls-tree', '--name-only', base, '--', name]).trim()) return null;
  return JSON.parse(git(['show', `${base}:${name}`]));
}

function cli(args) {
  const { values, positionals } = parseArgs({
    args, allowPositionals: true, strict: true,
    options: { root: { type: 'string' }, prepared: { type: 'string' }, package: { type: 'string', multiple: true },
      output: { type: 'string' }, base: { type: 'string' }, attestation: { type: 'string' }, release: { type: 'boolean' } }
  });
  if (positionals.length !== 1 || !['context', 'gate'].includes(positionals[0])) {
    throw new Error('Use context|gate --root <repo> --prepared <prepared.json> --package <candidate.nupkg> [--output <context.json>] [--base <full SHA>] [--attestation <external.json>] [--release].');
  }
  const root = path.resolve(values.root ?? '.');
  const pin = JSON.parse(fs.readFileSync(path.join(root, '.github', 'gdk-version.json'), 'utf8'));
  if (positionals[0] === 'gate') {
    const required = requiresEvidence({ currentPin: pin, release: values.release,
      basePin: values.release ? undefined : readBasePin(root, values.base) });
    if (!required) return { status: 'not-required', reason: 'SDK pin unchanged or approved baseline onboarding.' };
  }
  if (!values.prepared || !values.package?.length) throw new Error('Prepared inputs and actual candidate packages are required.');
  const prepared = JSON.parse(fs.readFileSync(values.prepared, 'utf8'));
  if (!prepared.files || typeof prepared.files !== 'object' || Array.isArray(prepared.files)) {
    throw new Error('Prepared source/generator fingerprints are required.');
  }
  const generatedSources = [];
  const generatedModels = [];
  for (const [file, expected] of Object.entries(prepared.files)) {
    const relative = path.relative(root, path.resolve(file)).replaceAll('\\', '/');
    if (!relative || relative.startsWith('../') || path.isAbsolute(relative) ||
        typeof expected !== 'string' || !/^[a-f0-9]{64}$/i.test(expected) ||
        digest(fs.readFileSync(file)) !== expected.toLowerCase()) {
      throw new Error('Prepared source/generator inputs are stale; prepare and rebuild the candidate.');
    }
    if (/^artifacts\/gdk\/.+\/generated\/src\/.+\.cs$/.test(relative)) generatedSources.push(relative);
    else if (/^artifacts\/gdk\/.+\/generated\/(?:model|exports)\.json$/.test(relative)) generatedModels.push(relative);
  }
  if (!generatedSources.length || !generatedModels.length) throw new Error('Prepared generated source/model fingerprints are missing.');
  const dependencies = {};
  for (const kind of ['windows', 'core']) {
    const directory = prepared.resolved?.[`${kind}PackageRoot`];
    if (typeof directory !== 'string') throw new Error(`Missing verified ${kind} package root.`);
    dependencies[kind] = path.join(directory, `microsoft.gdk.${kind}.${pin[`${kind}Version`]}.nupkg`);
  }
  const tracked = trackedFiles(root);
  const context = generateEvidenceContext({ root, pin, dependencyFiles: dependencies,
    sourceFiles: [...tracked.filter(file => /^(src|samples|tests)\//.test(file)), ...generatedSources],
    generatorFiles: [...tracked.filter(file => !/^(src|samples|tests)\//.test(file)), ...generatedModels],
    packages: values.package.map(file => ({ id: 'GDK.Net', path: file })) });
  if (positionals[0] === 'context') {
    if (!values.output) throw new Error('Context generation requires an explicit --output path.');
    const output = path.resolve(root, values.output);
    if (!path.relative(root, output).replaceAll('\\', '/').startsWith('artifacts/')) {
      throw new Error('Write contexts under repository artifacts/, outside rebuild inputs.');
    }
    fs.mkdirSync(path.dirname(output), { recursive: true });
    fs.writeFileSync(output, `${JSON.stringify(context, null, 2)}\n`);
    return { status: 'context-generated', contextSha256: context.contextSha256 };
  }
  if (values.attestation) {
    const relative = path.relative(root, path.resolve(values.attestation)).replaceAll('\\', '/');
    if (!relative.startsWith('../') && !path.isAbsolute(relative) && !relative.startsWith('artifacts/')) {
      throw new Error('Supply an external/artifacts attestation, never a self-referential rebuild input.');
    }
  }
  const attestation = values.attestation ? JSON.parse(fs.readFileSync(values.attestation, 'utf8')) : undefined;
  return validateAttestation({ context, attestation });
}

module.exports = { generateEvidenceContext, validateAttestation, requiresEvidence };
if (require.main === module) {
  // Failures deliberately propagate as nonzero exits; there is no fallback or synthetic evidence.
  console.log(JSON.stringify(cli(process.argv.slice(2))));
}
