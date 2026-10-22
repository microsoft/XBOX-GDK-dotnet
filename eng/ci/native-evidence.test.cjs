const test = require('node:test');
const assert = require('node:assert/strict');
const fs = require('node:fs');
const path = require('node:path');
const crypto = require('node:crypto');
const { generateEvidenceContext, validateAttestation, requiresEvidence } = require('./native-evidence.cjs');

test('only SDK-pin changes/releases require attestation, with one baseline onboarding exemption', () => {
  const pin = { edition: '260404', windowsVersion: '2604.4.7897', coreVersion: '2604.4.7897',
    windowsSha512: 'NQ0AHyNt1cCyWp+m/BMoHUietfTigOVfxa2U1hbNCoDLiPGDkFm3bcDjk9jzBJswAqgcC+79wZnViQ68Ix3zQw==',
    coreSha512: 'DtC+96jjBvUFFP2MGAkchDYMrgl8+9wWa9Z1aXeBIGey++/YAt1RCTcWwJJJ7urU8Lb7aY402EOmbnDJsDsMSg==' };
  assert.equal(requiresEvidence({ basePin: null, currentPin: pin }), false);
  assert.equal(requiresEvidence({ basePin: { ...pin }, currentPin: pin }), false);
  assert.equal(requiresEvidence({ basePin: pin, currentPin: { ...pin,
    windowsVersion: '2604.4.7900', coreVersion: '2604.4.7900' } }), true);
  assert.equal(requiresEvidence({ basePin: pin, currentPin: { ...pin, windowsSha512: Buffer.alloc(64, 3).toString('base64') } }), true);
  assert.equal(requiresEvidence({ basePin: pin, currentPin: { ...pin, coreSha512: Buffer.alloc(64, 4).toString('base64') } }), true);
  for (const metadata of [
    { libraryRevision: 2, library: { major: 1, minor: 1 } },
    { notes: 'Documentation-only adjustment' },
    { provenance: { release: 'changed' }, upstream: { notes: 'Metadata correction' } }
  ]) assert.equal(requiresEvidence({ basePin: pin, currentPin: { ...pin, ...metadata } }), false);
  assert.equal(requiresEvidence({ basePin: pin, currentPin: pin, release: true }), true);
  assert.throws(() => requiresEvidence({ currentPin: pin }), /verified base pin/);
  assert.throws(() => requiresEvidence({ basePin: null,
    currentPin: { ...pin, edition: '260405', windowsVersion: '2604.5.7903', coreVersion: '2604.5.7903' } }), /onboarding/);
  assert.throws(() => requiresEvidence({ basePin: pin, currentPin: { ...pin, windowsVersion: 'invalid' } }), /invalid/);
  for (const key of ['windowsSha512', 'coreSha512']) {
    assert.throws(() => requiresEvidence({ basePin: null,
      currentPin: { ...pin, [key]: Buffer.alloc(64, 3).toString('base64') } }), /onboarding/);
    assert.throws(() => requiresEvidence({ basePin: { ...pin, [key]: 'invalid' }, currentPin: pin }), /Base SDK pin is invalid/);
  }
  assert.throws(() => requiresEvidence({ basePin: { ...pin, coreVersion: '2604.4.7900' },
    currentPin: pin }), /Base SDK pin is invalid/);
  assert.throws(() => requiresEvidence({ basePin: { ...pin, edition: '260405' },
    currentPin: pin, release: true }), /Base SDK pin is invalid/);
});

test('manual evidence binds source, generators, dependencies and actual candidate bytes', t => {
  // Scratch stays in the repository, and is removed even when a negative assertion fails.
  const root = path.resolve('artifacts', `native-evidence-test-${process.pid}`);
  fs.mkdirSync(root, { recursive: true });
  t.after(() => fs.rmSync(root, { recursive: true, force: true }));
  for (const [name, bytes] of Object.entries({
    'source.cs': 'source', 'generator.py': 'generator',
    'windows.nupkg': 'windows-dependency', 'core.nupkg': 'core-dependency',
    'candidate.nupkg': Buffer.from([0x50, 0x4b, 0x03, 0x04, 1])
  })) fs.writeFileSync(path.join(root, name), bytes);
  const hash = file => crypto.createHash('sha512').update(fs.readFileSync(path.join(root, file))).digest('base64');
  const options = {
    root, pin: { edition: '260404', windowsVersion: '2604.4.7897', coreVersion: '2604.4.7897',
      windowsSha512: hash('windows.nupkg'), coreSha512: hash('core.nupkg') },
    sourceFiles: ['source.cs'], generatorFiles: ['generator.py'],
    dependencyFiles: { windows: path.join(root, 'windows.nupkg'), core: path.join(root, 'core.nupkg') },
    packages: [{ id: 'GDK.Net', path: path.join(root, 'candidate.nupkg') }]
  };
  const context = generateEvidenceContext(options);
  const attestation = {
    schemaVersion: 1, contextSha256: context.contextSha256, performedAt: '2026-01-01T00:00:00Z',
    architecture: 'x64', checks: [{ id: 'xuser-pilot', result: 'passed' },
      { id: 'pfmp-lobby-state-changes', result: 'passed' }]
  };
  assert.equal(validateAttestation({ context, attestation }).status, 'validated');
  assert.equal(JSON.stringify(context).includes(root), false);
  assert.equal(validateAttestation({ required: false }).status, 'not-required');
  assert.throws(() => validateAttestation({ context }), /required/);
  for (const file of ['source.cs', 'generator.py', 'candidate.nupkg']) {
    const original = fs.readFileSync(path.join(root, file));
    fs.appendFileSync(path.join(root, file), 'changed');
    assert.throws(() => validateAttestation({ context: generateEvidenceContext(options), attestation }), /Stale evidence/);
    fs.writeFileSync(path.join(root, file), original);
  }
  fs.appendFileSync(path.join(root, 'windows.nupkg'), 'changed');
  assert.throws(() => generateEvidenceContext(options), /archive differs/);
  options.pin.windowsSha512 = hash('windows.nupkg');
  assert.throws(() => validateAttestation({ context: generateEvidenceContext(options), attestation }), /Stale evidence/);
  for (const invalid of [
    { ...attestation, titleId: 'not-allowed' },
    { ...attestation, performedAt: '2999-01-01T00:00:00Z' },
    { ...attestation, checks: [attestation.checks[0], attestation.checks[0]] },
    { ...attestation, checks: [{ id: 'xuser-pilot', result: 'failed' }, attestation.checks[1]] }
  ]) assert.throws(() => validateAttestation({ context, attestation: invalid }));
  assert.throws(() => validateAttestation({ context: { ...context, packages: [] }, attestation }), /digest/);
});
