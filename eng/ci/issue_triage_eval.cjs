'use strict';

// Offline eval harness for the issue-triage skill (.github/skills/issue-triage/SKILL.md).
// Fixtures and gold expectations live in tests/evals/issue-triage/. The model only
// ever sees the staged source tree, the context file, and the skill; gold data and
// the rubric stay in the repository. See tests/evals/issue-triage/README.md.

const crypto = require('node:crypto');
const fs = require('node:fs');
const path = require('node:path');
const { execFileSync } = require('node:child_process');

const triage = require('./issue_triage.cjs');

const REPO_OWNER = 'microsoft';
const REPO_NAME = 'XBOX-GDK-dotnet';
const REPO_ROOT = path.resolve(__dirname, '..', '..');
const EVAL_ROOT = path.join(REPO_ROOT, 'tests', 'evals', 'issue-triage');
const CASES_ROOT = path.join(EVAL_ROOT, 'cases');
const RUBRIC_PATH = path.join(EVAL_ROOT, 'rubric.md');
const SKILL_PATH = '.github/skills/issue-triage/SKILL.md';
const SHA_PATTERN = /^[0-9a-f]{40}$/;
const CASE_ID_PATTERN = /^[a-z0-9]+(?:-[a-z0-9]+)*$/;
const DIMENSIONS = Object.freeze(['grounding', 'reasoning', 'uncertainty', 'missing_information', 'next_steps']);
const PASS_THRESHOLD = 8;
const STATUS = Object.freeze({
  INFRA_FAILURE: 'infra-or-model-failure',
  INVALID_REPORT: 'invalid-report',
  PENDING_REVIEW: 'pending-human-review',
  QUALITY_FAIL: 'quality-fail',
  QUALITY_PASS: 'quality-pass',
});

class EvalError extends Error {}

function git(args, options = {}) {
  return execFileSync('git', args, {
    cwd: options.cwd || REPO_ROOT,
    encoding: options.encoding === undefined ? 'utf8' : options.encoding,
    maxBuffer: 1 << 28,
    stdio: ['ignore', 'pipe', options.quiet ? 'ignore' : 'pipe'],
  });
}

function readJson(file) {
  try {
    return JSON.parse(fs.readFileSync(file, 'utf8'));
  } catch (error) {
    throw new EvalError(`Cannot read ${file}: ${error.message}`);
  }
}

function writeJson(file, value) {
  fs.mkdirSync(path.dirname(file), { recursive: true });
  fs.writeFileSync(file, `${JSON.stringify(value, null, 2)}\n`, 'utf8');
}

function sha256(text) {
  return crypto.createHash('sha256').update(text).digest('hex');
}

function listCaseIds(casesRoot = CASES_ROOT) {
  return fs
    .readdirSync(casesRoot, { withFileTypes: true })
    .filter((entry) => entry.isDirectory())
    .map((entry) => entry.name)
    .sort();
}

function canonicalJson(value) {
  if (Array.isArray(value)) return `[${value.map(canonicalJson).join(',')}]`;
  if (value && typeof value === 'object') {
    const keys = Object.keys(value).filter((key) => value[key] !== undefined).sort();
    return `{${keys.map((key) => `${JSON.stringify(key)}:${canonicalJson(value[key])}`).join(',')}}`;
  }
  return JSON.stringify(value);
}

// Hashes the whole frozen fixture so any field rendered into model input invalidates the digest.
function fixtureDigest(issue) {
  return crypto.createHash('sha256').update(canonicalJson(issue)).digest('hex');
}

// Scoring criteria: a run is graded against the expectations and rubric it was prepared with.
function expectationsDigest(expectations) {
  return crypto.createHash('sha256').update(canonicalJson(expectations)).digest('hex');
}

function rubricDigest(rubricPath = RUBRIC_PATH) {
  const text = fs.readFileSync(rubricPath, 'utf8').replace(/\r\n/g, '\n');
  return crypto.createHash('sha256').update(text).digest('hex');
}

function checkCaseShape(id, caseDef, issue, expectations) {
  const errors = [];
  if (!CASE_ID_PATTERN.test(id)) errors.push('case id must be kebab-case');
  if (caseDef.id !== id) errors.push(`case.json id ${JSON.stringify(caseDef.id)} does not match directory`);
  if (!SHA_PATTERN.test(String(caseDef.target_sha || ''))) errors.push('target_sha must be a full 40-character lowercase SHA');
  if (typeof caseDef.sha_justification !== 'string' || !caseDef.sha_justification.trim()) {
    errors.push('sha_justification is required');
  }
  if (caseDef.synthetic !== true || issue.synthetic !== true) errors.push('case and issue must be explicitly synthetic');
  if (caseDef.repository !== `${REPO_OWNER}/${REPO_NAME}`) errors.push('repository must be the public target repository');
  if (caseDef.source_url !== `https://github.com/${REPO_OWNER}/${REPO_NAME}/commit/${caseDef.target_sha}`) {
    errors.push('source_url must identify the exact public target commit');
  }
  if (!Array.isArray(caseDef.source_files) || !caseDef.source_files.length) errors.push('source_files must pin public evidence hashes');
  for (const entry of caseDef.source_files || []) {
    if (!safeSourcePath(entry.path) || !/^[0-9a-f]{64}$/.test(entry.sha256 || '')) errors.push('source_files needs safe paths and sha256 hashes');
  }
  const expectedDigest = issue ? fixtureDigest(issue) : null;
  if (!/^[0-9a-f]{64}$/.test(String(caseDef.fixture_digest || ''))) {
    errors.push(`fixture_digest must be a sha256 hex digest (issue.json digest is ${expectedDigest})`);
  } else if (caseDef.fixture_digest !== expectedDigest) {
    errors.push(`fixture_digest does not match issue.json (fixture changed without updating the digest; expected ${expectedDigest})`);
  }

  for (const key of ['number', 'url', 'title', 'body', 'state', 'labels', 'comments', 'captured_at', 'discussion_cutoff']) {
    if (issue[key] === undefined) errors.push(`issue.json missing ${key}`);
  }
  if (issue.state !== 'open') errors.push('issue.json must capture an open issue');
  if (triage.issueSkipReason(issue)) errors.push(`issue would be skipped by triage: ${triage.issueSkipReason(issue)}`);
  for (const comment of issue.comments || []) {
    if (comment.created_at && issue.discussion_cutoff && comment.created_at > issue.discussion_cutoff) {
      errors.push(`comment ${comment.id} is after the discussion cutoff`);
    }
  }

  if (expectations.case_id !== id) errors.push('expectations.json case_id does not match directory');
  if (!Array.isArray(expectations.acceptable_kinds) || !expectations.acceptable_kinds.length) {
    errors.push('expectations.acceptable_kinds must be a non-empty array');
  }
  for (const kind of expectations.acceptable_kinds || []) {
    if (!['bug', 'feature', 'question', 'other'].includes(kind)) errors.push(`unknown acceptable kind: ${kind}`);
  }
  if (!Array.isArray(expectations.evidence) || !expectations.evidence.some((item) => item.required)) {
    errors.push('expectations.evidence must include at least one required group');
  }
  for (const [i, group] of (expectations.evidence || []).entries()) {
    if (!group.id || !Array.isArray(group.locations) || !group.locations.length) {
      errors.push(`evidence[${i}] needs an id and at least one location`);
    }
  }
  for (const key of ['key_facts', 'acceptable_uncertainty', 'relevant_questions', 'avoid']) {
    if (!Array.isArray(expectations[key])) errors.push(`expectations.${key} must be an array`);
  }
  return errors;
}

function safeSourcePath(value) {
  return typeof value === 'string' && /^[A-Za-z0-9._\-+@ ()/]+$/.test(value) &&
    !value.split('/').some((part) => !part || part === '.' || part === '..' || part.toLowerCase() === '.git');
}

function readCaseFile(dir, filename) {
  if (typeof filename !== 'string' || !/^[A-Za-z0-9][A-Za-z0-9._-]*\.json$/.test(filename)) {
    throw new EvalError('Fixture filename must be a single JSON filename, not a path.');
  }
  const file = path.join(dir, filename);
  const stat = fs.lstatSync(file);
  if (stat.isSymbolicLink() || !stat.isFile() || stat.size > 1024 * 1024) {
    throw new EvalError('Fixture must be a bounded regular file, not a symbolic link.');
  }
  return readJson(file);
}

function loadCase(id, casesRoot = CASES_ROOT) {
  if (typeof id !== 'string' || !CASE_ID_PATTERN.test(id)) throw new EvalError('Case id must be kebab-case, not a path.');
  const dir = path.join(casesRoot, id);
  const stat = fs.lstatSync(dir);
  if (stat.isSymbolicLink() || !stat.isDirectory()) throw new EvalError('Case directory must not be a symbolic link.');
  const caseDef = readCaseFile(dir, 'case.json');
  const issue = readCaseFile(dir, caseDef.issue_file ?? 'issue.json');
  const expectations = readCaseFile(dir, caseDef.expectations_file ?? 'expectations.json');
  return { id, dir, caseDef, issue, expectations };
}

function commitExists(sha) {
  try {
    git(['cat-file', '-e', `${sha}^{commit}`], { quiet: true });
    return true;
  } catch {
    return false;
  }
}

// Makes the pinned commit available locally. Never falls back to another revision.
function ensureCommit(sha, { remote, fetch = true } = {}) {
  if (!SHA_PATTERN.test(String(sha))) throw new EvalError('Pinned commit must be a full lowercase SHA.');
  if (commitExists(sha)) return;
  if (fetch && remote) {
    try {
      const url = git(['remote', 'get-url', remote], { quiet: true }).trim();
      if (url !== `https://github.com/${REPO_OWNER}/${REPO_NAME}.git` && url !== `https://github.com/${REPO_OWNER}/${REPO_NAME}`) {
        throw new EvalError('Eval fetch remote must be the public HTTPS target repository.');
      }
      git(['fetch', '--no-tags', '--depth=1', '--quiet', remote, sha], { quiet: true });
    } catch (error) {
      if (error instanceof EvalError) throw error;
      // Reported below.
    }
  }
  if (!commitExists(sha)) {
    throw new EvalError(`Pinned commit ${sha} is not available${remote ? ` (fetch from ${remote} failed)` : ''}.`);
  }
}

function checkSourceHashesAtCommit(sha, sourceFiles) {
  const errors = [];
  for (const entry of sourceFiles || []) {
    if (!safeSourcePath(entry.path)) {
      errors.push('Source evidence path is unsafe.');
      continue;
    }
    try {
      const content = git(['show', `${sha}:${entry.path}`], { encoding: null, quiet: true });
      if (sha256(content) !== entry.sha256) errors.push(`Source hash mismatch: ${entry.path}`);
    } catch {
      errors.push(`Source evidence unavailable at ${sha}: ${entry.path}`);
    }
  }
  return errors;
}

function fileLineCount(sha, relPath) {
  let text;
  try {
    text = git(['show', `${sha}:${relPath}`], { quiet: true });
  } catch {
    return null;
  }
  return triage.countLines(text);
}

function checkEvidenceAtCommit(sha, expectations, lineCounter = fileLineCount) {
  const errors = [];
  for (const group of expectations.evidence || []) {
    for (const location of group.locations || []) {
      const where = `${group.id}: ${location.path}:${location.start_line}-${location.end_line}`;
      if (!safeSourcePath(location.path)) {
        errors.push(`${where} has an unsafe path`);
        continue;
      }
      const lines = lineCounter(sha, location.path);
      if (lines === null) {
        errors.push(`${where} does not exist at ${sha}`);
        continue;
      }
      if (!Number.isInteger(location.start_line) || !Number.isInteger(location.end_line)) {
        errors.push(`${where} needs integer lines`);
      } else if (location.start_line < 1 || location.end_line < location.start_line) {
        errors.push(`${where} has an invalid range`);
      } else if (location.end_line > lines) {
        errors.push(`${where} is past the end of the file (${lines} lines)`);
      } else if (location.end_line - location.start_line + 1 > triage.LIMITS.maxCitationSpan) {
        errors.push(`${where} exceeds ${triage.LIMITS.maxCitationSpan} lines`);
      }
    }
  }
  return errors;
}

function validateFixtures({ caseIds, remote, fetch = true, casesRoot = CASES_ROOT } = {}) {
  const ids = caseIds && caseIds.length ? caseIds : listCaseIds(casesRoot);
  if (!ids.length) throw new EvalError('No eval cases found.');
  const results = [];
  for (const id of ids) {
    const errors = [];
    let loaded;
    try {
      loaded = loadCase(id, casesRoot);
      errors.push(...checkCaseShape(id, loaded.caseDef, loaded.issue, loaded.expectations));
    } catch (error) {
      errors.push(error.message);
    }
    if (loaded && SHA_PATTERN.test(String(loaded.caseDef.target_sha))) {
      try {
        ensureCommit(loaded.caseDef.target_sha, { remote, fetch });
        errors.push(...checkEvidenceAtCommit(loaded.caseDef.target_sha, loaded.expectations));
        errors.push(...checkSourceHashesAtCommit(loaded.caseDef.target_sha, loaded.caseDef.source_files));
      } catch (error) {
        errors.push(error.message);
      }
    }
    results.push({ id, ok: errors.length === 0, errors });
  }
  return results;
}

function skillProvenance(repoRoot = REPO_ROOT) {
  const skillFile = path.join(repoRoot, SKILL_PATH);
  const content = fs.readFileSync(skillFile, 'utf8');
  let revision = null;
  let skillDirty = null;
  let treeDirty = null;
  try {
    revision = git(['rev-parse', 'HEAD'], { cwd: repoRoot, quiet: true }).trim();
    skillDirty = git(['status', '--porcelain', '--', SKILL_PATH], { cwd: repoRoot, quiet: true }).trim() !== '';
    treeDirty = git(['status', '--porcelain'], { cwd: repoRoot, quiet: true }).trim() !== '';
  } catch {
    // Not a git checkout; leave provenance fields null.
  }
  return { skill_path: SKILL_PATH, skill_digest: sha256(content), skill_dirty: skillDirty, harness_revision: revision, harness_tree_dirty: treeDirty };
}

function buildEvalContext(issue, sha) {
  return '# Synthetic eval fixture — not a real filed issue\n\n' + triage.buildContextMarkdown({
    owner: REPO_OWNER,
    repo: REPO_NAME,
    issue,
    priorComments: (issue.comments || []).filter((comment) => !triage.isBotUser(comment.user) && !triage.isTriageCommand(comment.body)),
    sha,
    snapshotLabel: 'pinned eval commit',
  }).markdown;
}

// Adapter prompt for local model runs; mirrors .github/workflows/issue-triage-eval.md.
function buildLocalPrompt(contextPath) {
  return [
    `Use Actions mode of the issue-triage skill (${SKILL_PATH}). Read that file first and follow it.`,
    `The context file is ${contextPath}.`,
    'The workspace is the current directory: a read-only snapshot of the repository at the commit named in the context file.',
    'Deliver the report by replying with only the report JSON in a single fenced json block. Do not post, edit, or run anything.',
  ].join('\n');
}

function stageSource(sha, sourceDir, repoRoot = REPO_ROOT) {
  fs.mkdirSync(sourceDir, { recursive: true });
  const links = git(['ls-tree', '-r', sha], { quiet: true }).split('\n').filter((line) => /^(120000|160000) /.test(line));
  if (links.length) throw new EvalError('Pinned source contains symbolic links or submodules; refusing to stage it.');
  const tarFile = path.join(path.dirname(sourceDir), `.source-${crypto.randomUUID()}.tar`);
  try {
    git(['archive', '--format=tar', '-o', tarFile, sha]);
    execFileSync('tar', ['-xf', tarFile, '-C', sourceDir], { stdio: ['ignore', 'ignore', 'pipe'] });
  } finally {
    fs.rmSync(tarFile, { force: true });
  }
  // Gold data must never be readable by the model, even if a future pin includes it.
  fs.rmSync(path.join(sourceDir, 'tests', 'evals'), { recursive: true, force: true });
  // Pinned commits may predate the skill; always stage the candidate skill under test.
  const skillTarget = path.join(sourceDir, ...SKILL_PATH.split('/'));
  fs.mkdirSync(path.dirname(skillTarget), { recursive: true });
  fs.copyFileSync(path.join(repoRoot, ...SKILL_PATH.split('/')), skillTarget);
}

function prepare({ caseId, outDir, remote, fetch = true, model = null, runId = null, now = new Date() }) {
  if (!caseId) throw new EvalError('--case is required.');
  if (!outDir) throw new EvalError('--out is required.');
  const loaded = loadCase(caseId);
  const shapeErrors = checkCaseShape(caseId, loaded.caseDef, loaded.issue, loaded.expectations);
  if (shapeErrors.length) throw new EvalError(`Case ${caseId} is invalid: ${shapeErrors.join('; ')}`);
  const sha = loaded.caseDef.target_sha;
  ensureCommit(sha, { remote, fetch });
  const evidenceErrors = [...checkEvidenceAtCommit(sha, loaded.expectations), ...checkSourceHashesAtCommit(sha, loaded.caseDef.source_files)];
  if (evidenceErrors.length) throw new EvalError(`Pinned evidence is invalid: ${evidenceErrors.join('; ')}`);

  const out = path.resolve(outDir);
  const relativeGold = path.relative(EVAL_ROOT, out);
  if (relativeGold === '' || (!relativeGold.startsWith(`..${path.sep}`) && relativeGold !== '..' && !path.isAbsolute(relativeGold))) {
    throw new EvalError('Run output must not be inside the gold fixture directory.');
  }
  if (fs.existsSync(out) && fs.readdirSync(out).length) throw new EvalError(`Output directory ${out} is not empty.`);
  const sourceDir = path.join(out, 'source');
  const contextDir = path.join(out, 'context');
  stageSource(sha, sourceDir);
  fs.mkdirSync(contextDir, { recursive: true });
  const contextPath = path.join(contextDir, 'context.md');
  fs.writeFileSync(contextPath, buildEvalContext(loaded.issue, sha), 'utf8');
  fs.writeFileSync(path.join(out, 'prompt.txt'), `${buildLocalPrompt(contextPath)}\n`, 'utf8');

  const run = {
    case_id: caseId,
    issue_url: loaded.issue.url,
    fixture_digest: loaded.caseDef.fixture_digest,
    expectations_digest: expectationsDigest(loaded.expectations),
    rubric_digest: rubricDigest(),
    target_sha: sha,
    source_files: loaded.caseDef.source_files,
    synthetic: true,
    ...skillProvenance(),
    model,
    run_id: runId,
    prepared_at: now.toISOString(),
    outcome: 'prepared',
  };
  writeJson(path.join(out, 'run.json'), run);
  return { out, sourceDir, contextPath, run };
}

// Accepts raw JSON, a fenced json block, or a transcript that ends with one.
function extractReport(text) {
  const trimmed = String(text || '').trim();
  if (!trimmed) throw new EvalError('Report file is empty.');
  try {
    return triage.parseReportValue(trimmed);
  } catch {
    const blocks = [...trimmed.matchAll(/```json\s*\n([\s\S]*?)\n```/g)];
    if (!blocks.length) throw new EvalError('No report JSON found (expected raw JSON or a fenced json block).');
    try {
      return JSON.parse(blocks[blocks.length - 1][1]);
    } catch (error) {
      throw new EvalError(`Report JSON block is invalid: ${error.message}`);
    }
  }
}

function validateReportAt({ reportText, sourceDir }) {
  let report;
  try {
    report = extractReport(reportText);
    triage.validateReport(report);
  } catch (error) {
    return { schema_valid: false, citations_valid: false, errors: [error.message], report: report || null };
  }
  const errors = [];
  for (const finding of report.findings) {
    try {
      triage.validateCitation(sourceDir, finding);
    } catch (error) {
      errors.push(error.message);
    }
  }
  return { schema_valid: true, citations_valid: errors.length === 0, errors, report };
}

function overlaps(finding, location) {
  return finding.path === location.path && finding.start_line <= location.end_line && finding.end_line >= location.start_line;
}

// Automated signals for the reviewer; they inform but never replace human scores.
function automatedSignals(report, expectations) {
  const findings = (report && report.findings) || [];
  const evidence = (expectations.evidence || []).map((group) => ({
    id: group.id,
    required: Boolean(group.required),
    covered: findings.some((finding) => group.locations.some((location) => overlaps(finding, location))),
  }));
  return {
    kind: report ? report.kind : null,
    kind_acceptable: Boolean(report && (expectations.acceptable_kinds || []).includes(report.kind)),
    security_sensitive_flag: report ? report.security_sensitive === true : null,
    evidence,
    required_evidence_covered: evidence.filter((item) => item.required).every((item) => item.covered),
  };
}

function blankScorecard(caseId) {
  return {
    case_id: caseId,
    reviewer: null,
    reviewed_at: null,
    scores: Object.fromEntries(DIMENSIONS.map((name) => [name, null])),
    critical_failure: null,
    alternative_rationale: '',
    notes: '',
  };
}

function scorecardErrors(scorecard) {
  if (!scorecard) return ['scorecard missing'];
  const errors = [];
  if (typeof scorecard.reviewer !== 'string' || !scorecard.reviewer.trim()) errors.push('reviewer not recorded');
  if (typeof scorecard.reviewed_at !== 'string' || !/^\d{4}-\d\d-\d\dT/.test(scorecard.reviewed_at) || !Number.isFinite(Date.parse(scorecard.reviewed_at))) {
    errors.push('reviewed_at timestamp not recorded');
  }
  for (const name of DIMENSIONS) {
    const value = scorecard.scores && scorecard.scores[name];
    if (![0, 1, 2].includes(value)) errors.push(`${name} score must be 0, 1, or 2`);
  }
  const critical = scorecard.critical_failure;
  if (critical !== null && (typeof critical !== 'object' || !critical || !critical.reason)) {
    errors.push('critical_failure must be null or { "reason": "..." }');
  }
  return errors;
}

// Pure pass/fail decision. Unscored or partially scored runs are never a pass.
function decide({ outcome, validation, scorecard }) {
  if (outcome && outcome !== 'prepared' && outcome !== 'completed') {
    return { status: STATUS.INFRA_FAILURE, reasons: [`run outcome: ${outcome}`] };
  }
  if (!validation) return { status: STATUS.INFRA_FAILURE, reasons: ['no report produced'] };
  if (!validation.schema_valid || !validation.citations_valid) {
    return { status: STATUS.INVALID_REPORT, reasons: validation.errors };
  }
  const missing = scorecardErrors(scorecard);
  if (missing.length) return { status: STATUS.PENDING_REVIEW, reasons: missing };

  const total = DIMENSIONS.reduce((sum, name) => sum + scorecard.scores[name], 0);
  const reasons = [];
  if (scorecard.critical_failure) reasons.push(`critical failure: ${scorecard.critical_failure.reason}`);
  if (total < PASS_THRESHOLD) reasons.push(`total ${total}/10 is below ${PASS_THRESHOLD}`);
  if (scorecard.scores.grounding !== 2) reasons.push(`grounding is ${scorecard.scores.grounding}, must be 2`);
  return { status: reasons.length ? STATUS.QUALITY_FAIL : STATUS.QUALITY_PASS, total, reasons };
}

function findReportFile(runDir) {
  for (const name of ['report.json', 'report.md', 'report.txt']) {
    const file = path.join(runDir, name);
    if (fs.existsSync(file)) return file;
  }
  return null;
}

// Actions artifacts omit the snapshot; rebuild it from the pinned SHA when needed.
function ensureSource(dir, run, { remote, fetch = true } = {}) {
  if (!SHA_PATTERN.test(String(run.target_sha))) throw new EvalError('Run target_sha is not a full lowercase SHA.');
  const sourceDir = path.join(dir, 'source');
  if (!fs.existsSync(sourceDir)) {
    ensureCommit(run.target_sha, { remote, fetch });
    stageSource(run.target_sha, sourceDir);
  }
  for (const entry of run.source_files || []) {
    if (!safeSourcePath(entry.path)) throw new EvalError('Source manifest contains an unsafe path.');
    triage.validateCitation(sourceDir, { path: entry.path, start_line: 1, end_line: 1 });
    if (sha256(fs.readFileSync(path.join(sourceDir, ...entry.path.split('/')))) !== entry.sha256) {
      throw new EvalError(`Staged source hash mismatch: ${entry.path}`);
    }
  }
  return sourceDir;
}

// A run is only comparable to the case it was prepared from; reject artifacts whose
// pinned snapshot, issue fixture, gold expectations, or rubric no longer match.
function staleRunErrors(run, caseDef, issue, expectations, rubricPath = RUBRIC_PATH) {
  const errors = [];
  if (run.target_sha !== caseDef.target_sha) {
    errors.push(`target_sha ${run.target_sha} does not match case ${caseDef.target_sha}`);
  }
  if (canonicalJson(run.source_files) !== canonicalJson(caseDef.source_files)) errors.push('source_files do not match current public provenance');
  const currentDigest = fixtureDigest(issue);
  if (run.fixture_digest !== caseDef.fixture_digest || run.fixture_digest !== currentDigest) {
    errors.push(`fixture_digest ${run.fixture_digest} does not match current fixture ${currentDigest}`);
  }
  const currentExpectations = expectationsDigest(expectations);
  if (run.expectations_digest !== currentExpectations) {
    errors.push(`expectations_digest ${run.expectations_digest} does not match current expectations ${currentExpectations}`);
  }
  const currentRubric = rubricDigest(rubricPath);
  if (run.rubric_digest !== currentRubric) {
    errors.push(`rubric_digest ${run.rubric_digest} does not match current rubric ${currentRubric}`);
  }
  return errors;
}

function scoreRun(runDir, options = {}) {
  const dir = path.resolve(runDir);
  const run = readJson(path.join(dir, 'run.json'));
  const { caseDef, issue, expectations } = loadCase(run.case_id, options.casesRoot);
  const stale = staleRunErrors(run, caseDef, issue, expectations, options.rubricPath);
  if (stale.length) {
    throw new EvalError(`Run ${dir} is stale for case ${run.case_id}: ${stale.join('; ')}. Re-run prepare for this case.`);
  }
  const reportFile = findReportFile(dir);
  const validation = reportFile
    ? validateReportAt({ reportText: fs.readFileSync(reportFile, 'utf8'), sourceDir: ensureSource(dir, run, options) })
    : null;
  const scorecardFile = path.join(dir, 'scorecard.json');
  const scorecard = fs.existsSync(scorecardFile) ? readJson(scorecardFile) : null;
  if (scorecard && scorecard.case_id !== run.case_id) throw new EvalError(`scorecard.json is for ${scorecard.case_id}, not ${run.case_id}`);
  const decision = decide({ outcome: run.outcome, validation, scorecard });
  const result = {
    case_id: run.case_id,
    target_sha: run.target_sha,
    skill_digest: run.skill_digest,
    model: run.model,
    run_id: run.run_id,
    report_file: reportFile ? path.basename(reportFile) : null,
    validation: validation && { schema_valid: validation.schema_valid, citations_valid: validation.citations_valid, errors: validation.errors },
    signals: validation && validation.report ? automatedSignals(validation.report, expectations) : null,
    ...decision,
  };
  writeJson(path.join(dir, 'result.json'), result);
  return result;
}

// Copies the single report item from a gh-aw agent_output.json into the run directory
// without validating it, so an invalid report is still recorded for review.
function collectAgentOutput({ runDir, agentOutputPath, itemType }) {
  const dir = path.resolve(runDir);
  const runFile = path.join(dir, 'run.json');
  const run = readJson(runFile);
  let reports = [];
  if (agentOutputPath && fs.existsSync(agentOutputPath)) {
    let output;
    try {
      output = JSON.parse(fs.readFileSync(agentOutputPath, 'utf8'));
    } catch {
      output = null;
    }
    const items = Array.isArray(output && output.items) ? output.items : [];
    reports = items.filter((item) => item && item.type === itemType);
  }
  if (reports.length === 1) {
    const raw = reports[0].report;
    const text = typeof raw === 'string' ? raw : JSON.stringify(raw === undefined ? null : raw, null, 2);
    fs.writeFileSync(path.join(dir, 'report.json'), `${text.trim()}\n`);
    run.outcome = 'completed';
  } else {
    run.outcome = reports.length ? 'multiple-reports' : 'no-report';
  }
  writeJson(runFile, run);
  return run.outcome;
}

function suiteVerdict(results, caseIds = listCaseIds()) {
  const passed = new Set(results.filter((result) => result.status === STATUS.QUALITY_PASS).map((result) => result.case_id));
  const missing = caseIds.filter((id) => !passed.has(id));
  return { pass: missing.length === 0, failing_or_missing: missing };
}

function parseArgs(argv) {
  const args = { _: [] };
  for (let i = 0; i < argv.length; i += 1) {
    const token = argv[i];
    if (!token.startsWith('--')) {
      args._.push(token);
      continue;
    }
    const key = token.slice(2);
    if (key === 'no-fetch') {
      args.fetch = false;
      continue;
    }
    const value = argv[i + 1];
    if (value === undefined || value.startsWith('--')) throw new EvalError(`--${key} needs a value`);
    i += 1;
    if (key === 'case' || key === 'run') (args[key] = args[key] || []).push(value);
    else args[key] = value;
  }
  return args;
}

const USAGE = `Usage: node eng/ci/issue_triage_eval.cjs <command> [options]

  validate-fixtures [--case ID]... [--remote NAME] [--no-fetch]
  prepare --case ID --out DIR [--remote NAME] [--no-fetch] [--model NAME] [--run-id ID]
  collect --run DIR --agent-output FILE --item-type TYPE   (Actions: copy the agent's report)
  validate-report --run DIR
  scorecard --run DIR
  score --run DIR [--run DIR]...   (exits 0 only when every case has a quality-pass run)
`;

function main(argv = process.argv.slice(2), log = console.log, options = {}) {
  const args = parseArgs(argv);
  const command = args._[0];
  const remote = args.remote || process.env.ISSUE_TRIAGE_EVAL_REMOTE || 'origin';
  const fetch = args.fetch !== false;
  switch (command) {
    case 'validate-fixtures': {
      const results = validateFixtures({ caseIds: args.case, remote, fetch });
      for (const result of results) {
        log(`${result.ok ? 'ok  ' : 'FAIL'} ${result.id}`);
        for (const error of result.errors) log(`       ${error}`);
      }
      return results.every((result) => result.ok) ? 0 : 1;
    }
    case 'prepare': {
      if (!args.case || args.case.length !== 1) throw new EvalError('prepare needs exactly one --case.');
      const { out, contextPath } = prepare({ caseId: args.case[0], outDir: args.out, remote, fetch, model: args.model || null, runId: args['run-id'] || null });
      log(`Prepared ${out}`);
      log(`  source:  ${path.join(out, 'source')}`);
      log(`  context: ${contextPath}`);
      log(`  prompt:  ${path.join(out, 'prompt.txt')}`);
      log('Save the model reply as report.md (or report.json) in the run directory, then run scorecard/score.');
      return 0;
    }
    case 'collect': {
      if (!args.run || args.run.length !== 1 || !args['agent-output'] || !args['item-type']) {
        throw new EvalError('collect needs --run, --agent-output, and --item-type.');
      }
      const outcome = collectAgentOutput({ runDir: args.run[0], agentOutputPath: args['agent-output'], itemType: args['item-type'] });
      log(`outcome=${outcome}`);
      return outcome === 'completed' ? 0 : 1;
    }
    case 'validate-report': {
      if (!args.run || args.run.length !== 1) throw new EvalError('validate-report needs exactly one --run.');
      const dir = path.resolve(args.run[0]);
      const reportFile = findReportFile(dir);
      const run = readJson(path.join(dir, 'run.json'));
      const validation = reportFile
        ? validateReportAt({ reportText: fs.readFileSync(reportFile, 'utf8'), sourceDir: ensureSource(dir, run, { remote, fetch }) })
        : null;
      const decision = decide({ outcome: run.outcome, validation, scorecard: null });
      writeJson(path.join(dir, 'validation.json'), { ...validation, ...decision });
      if (!validation) {
        log(`${decision.status}: no report produced`);
        return 1;
      }
      log(`schema_valid=${validation.schema_valid} citations_valid=${validation.citations_valid} status=${decision.status}`);
      for (const error of validation.errors) log(`  ${error}`);
      return validation.schema_valid && validation.citations_valid && decision.status === STATUS.PENDING_REVIEW ? 0 : 1;
    }
    case 'scorecard': {
      if (!args.run || args.run.length !== 1) throw new EvalError('scorecard needs exactly one --run.');
      const dir = path.resolve(args.run[0]);
      const run = readJson(path.join(dir, 'run.json'));
      const file = path.join(dir, 'scorecard.json');
      if (fs.existsSync(file)) throw new EvalError(`${file} already exists.`);
      writeJson(file, blankScorecard(run.case_id));
      log(`Wrote ${file}. Fill it in using tests/evals/issue-triage/rubric.md.`);
      return 0;
    }
    case 'score': {
      if (!args.run || !args.run.length) throw new EvalError('score needs at least one --run.');
      const results = args.run.map((run) => scoreRun(run, { ...options, remote, fetch }));
      for (const result of results) {
        log(`${result.status.padEnd(22)} ${result.case_id}${result.total !== undefined ? ` (${result.total}/10)` : ''}`);
        for (const reason of result.reasons || []) log(`       ${reason}`);
      }
      const suite = suiteVerdict(results, listCaseIds(options.casesRoot));
      log(suite.pass ? 'Suite: pass' : `Suite: not passing (failing or missing: ${suite.failing_or_missing.join(', ')})`);
      return suite.pass ? 0 : 1;
    }
    default:
      log(USAGE);
      return command ? 2 : 0;
  }
}

module.exports = {
  DIMENSIONS,
  PASS_THRESHOLD,
  STATUS,
  EvalError,
  automatedSignals,
  blankScorecard,
  buildEvalContext,
  buildLocalPrompt,
  checkCaseShape,
  checkEvidenceAtCommit,
  collectAgentOutput,
  decide,
  extractReport,
  expectationsDigest,
  fixtureDigest,
  listCaseIds,
  loadCase,
  main,
  parseArgs,
  prepare,
  rubricDigest,
  scoreRun,
  staleRunErrors,
  suiteVerdict,
  validateFixtures,
  validateReportAt,
};

if (require.main === module) {
  try {
    process.exitCode = main();
  } catch (error) {
    if (!(error instanceof EvalError || error instanceof triage.TriageError)) throw error;
    console.error(`error: ${error.message}`);
    process.exitCode = 2;
  }
}
