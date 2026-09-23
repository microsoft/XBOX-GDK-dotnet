<#
.SYNOPSIS
    Runs the GDK package test tiers for tests/GDK.Net.LiveHarness.

.DESCRIPTION
    These are MANUAL, LOCAL-ONLY tests. They require an installed Microsoft GDK, a signed-in Xbox
    account, and a dev-unlocked machine; a hosted CI runner can do none of that, which is why the
    repo's ci.yml only builds and unit-tests.

    Tiers, in increasing order of invasiveness:

      Layout    dotnet publish + makepkg genmap + makepkg validate.
                Produces files under artifacts/. Changes nothing on the machine.

      Msixvc    Layout, then makepkg pack /pc.
                Produces a real .msixvc. Still changes nothing on the machine.

      Register  Loose-registers artifacts/package/layout with `wdapp register`, launches the title,
                and reads back the JSON report the harness writes.
                *** THIS MODIFIES THE MACHINE. *** See -Restore below.

      Install   Installs the packed .msixvc with `wdapp install`, launches, reads the report.
                *** THIS MODIFIES THE MACHINE. ***

.PARAMETER Tier
    Which tiers to run. Defaults to the two non-invasive ones.

.PARAMETER Only
    Runs only the checks whose id starts with this prefix, for iterating on one family. Checks
    outside the filter are skipped, and so is anything that depended on them.

.PARAMETER Scid
    Overrides the title's Service Configuration ID. Not normally needed: the harness derives the
    SCID from the title id, which is correct unless Partner Center assigned this title a different
    one. Defaults to %GDKNET_SCID% when that is set.

.PARAMETER Aot
    Build the layout with NativeAOT. Xbox consoles require it, so this is the configuration a
    console title ships in; running the Register or Install tier with -Aot is what proves GDK.Net's
    interop works after ILC has compiled it ahead of time. Requires the MSVC toolchain.

.PARAMETER Restore
    After the invasive tiers, unregister/uninstall this package. Because the harness deliberately
    reuses the identity of an existing title on this box (see tests/GDK.Net.LiveHarness/README.md),
    -Restore will also reinstall that title's .msixvc when -PriorPackage points at it.

.PARAMETER PriorPackage
    Path to the .msixvc whose identity this harness borrows, reinstalled by -Restore.

.EXAMPLE
    pwsh eng/run-package-tests.ps1
    pwsh eng/run-package-tests.ps1 -Tier Layout, Msixvc, Register -Restore
    pwsh eng/run-package-tests.ps1 -Tier Layout, Register -Aot -Restore -PriorPackage ...
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [ValidateSet('Layout', 'Msixvc', 'Register', 'Install')]
    [AllowEmptyCollection()]
    [string[]] $Tier = @('Layout', 'Msixvc'),

    [string] $Only,
    [string] $Scid = $env:GDKNET_SCID,
    [switch] $Aot,
    [switch] $Restore,
    [string] $PriorPackage,
    [int] $LaunchTimeoutSeconds = 120
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
$packageRoot = Join-Path $repoRoot 'artifacts\package'
$layoutDir = Join-Path $packageRoot 'layout'
$outDir = Join-Path $packageRoot 'out'

# Identity comes from tests/GDK.Net.LiveHarness/MicrosoftGame.config; the family name suffix is the
# hash makepkg derives from the publisher, and the app id is the <Executable Id> in the config.
$packageFamilyName = '41336MicrosoftATG.GodotTestApp_zjr0dfhgjwvde'
$aumid = "$packageFamilyName!Game"

$results = [System.Collections.Generic.List[object]]::new()

function Add-Result {
    param(
        [Parameter(Mandatory)] [string] $Name,
        [Parameter(Mandatory)] [ValidateSet('pass', 'fail', 'skip')] [string] $Status,
        [string] $Detail
    )

    $results.Add([pscustomobject]@{ Tier = $Name; Status = $Status; Detail = $Detail })

    $colour = switch ($Status) { 'pass' { 'Green' } 'fail' { 'Red' } default { 'Yellow' } }
    Write-Host ("[{0,-4}] {1} {2}" -f $Status.ToUpperInvariant(), $Name, $Detail) -ForegroundColor $colour
}

function Resolve-WdApp {
    if (-not $env:GameDK) {
        throw "The Microsoft GDK is not installed: %GameDK% is not set. Package tests are local-only."
    }

    $wdApp = Join-Path $env:GameDK 'bin\wdapp.exe'
    if (-not (Test-Path $wdApp)) {
        throw "wdapp.exe was not found at '$wdApp'. Reinstall the Microsoft GDK."
    }

    return $wdApp
}

function Get-ReportCandidates {
    # A packaged full-trust title may or may not have its AppData redirected into the package
    # container, so look everywhere the harness could plausibly have landed and take the newest.
    @(
        (Join-Path $env:LOCALAPPDATA 'GDK.Net.LiveHarness\report.json'),
        (Join-Path $env:LOCALAPPDATA "Packages\$packageFamilyName\LocalState\GDK.Net.LiveHarness\report.json"),
        (Join-Path $env:LOCALAPPDATA "Packages\$packageFamilyName\LocalCache\Local\GDK.Net.LiveHarness\report.json")
    )
}

function Clear-Reports {
    foreach ($path in Get-ReportCandidates) {
        if (Test-Path $path) {
            Remove-Item $path -Force
        }
    }
}

function Wait-ForReport {
    param(
        [int] $TimeoutSeconds,
        [int] $ProcessId = 0
    )

    # The harness flushes the report after every step, so the file's mere existence says nothing
    # about whether the run finished. Keep polling until the final save sets `completed`, and
    # return the last parsable snapshot if the deadline passes -- that snapshot is the crash
    # evidence the caller reports as a truncation.
    $deadline = (Get-Date).AddSeconds($TimeoutSeconds)
    $latest = $null
    while ((Get-Date) -lt $deadline) {
        $found = Get-ReportCandidates | Where-Object { Test-Path $_ } |
            Get-Item | Sort-Object LastWriteTime -Descending | Select-Object -First 1
        if ($found) {
            try {
                $snapshot = [pscustomobject]@{ Path = $found.FullName; Json = (Get-Content $found.FullName -Raw | ConvertFrom-Json) }
                $latest = $snapshot
                if ($snapshot.Json.completed) {
                    return $snapshot
                }
            }
            catch {
                # A partially written flush; the next poll will pick up the complete text.
            }
        }

        # Waiting out the full timeout after the title has already died wastes minutes per crash,
        # and with the relaunch loop that multiplies. The report is read once more above before
        # this check, so nothing the process wrote on its way out is missed.
        if ($ProcessId -gt 0 -and -not (Get-Process -Id $ProcessId -ErrorAction SilentlyContinue)) {
            return $latest
        }

        Start-Sleep -Seconds 1
    }

    return $latest
}

function Invoke-LaunchAndCollect {
    param(
        [Parameter(Mandatory)] [string] $WdApp,
        [Parameter(Mandatory)] [string] $TierName
    )

    Clear-Reports

    # An access violation in native code cannot be caught in .NET: the process dies without
    # unwinding, and every check after the offending one goes unmeasured. The harness marks each
    # check as running before it makes the call and can resume from that marker, so relaunching
    # converts a crash from "the rest of the suite is unknown" into one failed check. The loop is
    # bounded and only continues while each attempt gets further than the last.
    $maxAttempts = 12
    $report = $null
    $previousStepCount = -1

    for ($attempt = 1; $attempt -le $maxAttempts; $attempt++) {
        $launchArgs = @('launch', $aumid)
        if ($Only) { $launchArgs += @('--only', $Only) }
        if ($Scid) { $launchArgs += @('--scid', $Scid) }
        if ($attempt -gt 1) { $launchArgs += '--resume' }

        $suffix = if ($attempt -gt 1) { " (resuming, attempt $attempt)" } else { '' }
        Write-Host "==> Launching $aumid$suffix" -ForegroundColor Cyan
        $launchOutput = & $WdApp @launchArgs 2>&1
        $launchOutput | ForEach-Object { Write-Host $_ }
        if ($LASTEXITCODE -ne 0) {
            Add-Result -Name $TierName -Status fail -Detail "wdapp launch exited with $LASTEXITCODE."
            return
        }

        # wdapp reports the pid it started, which lets the wait give up the moment the title dies
        # rather than sitting out the whole launch timeout on every crash.
        $titlePid = 0
        if (($launchOutput -join "`n") -match 'Process Id:\s*(\d+)') { $titlePid = [int] $Matches[1] }

        $report = Wait-ForReport -TimeoutSeconds $LaunchTimeoutSeconds -ProcessId $titlePid
        if (-not $report) {
            Add-Result -Name $TierName -Status fail -Detail "The title launched but wrote no report within ${LaunchTimeoutSeconds}s."
            return
        }

        if ($report.Json.completed) { break }

        $stepCount = @($report.Json.steps).Count
        $running = @($report.Json.steps | Where-Object outcome -eq 'running')
        $crashedIn = if ($running.Count -gt 0) { $running[-1].name } else { '<unknown>' }
        Write-Host "The title terminated in '$crashedIn'; relaunching to carry on past it." -ForegroundColor Yellow

        if ($stepCount -le $previousStepCount) {
            # No forward progress means resuming is not working, and relaunching again would only
            # spin. Fall through and report the truncation.
            break
        }

        $previousStepCount = $stepCount
    }

    Write-Host "Report: $($report.Path)"

    # A stale report from an earlier unpackaged run is indistinguishable from a real one unless the
    # writer is checked: assert the process that produced it was actually the packaged executable.
    $reportedPath = $report.Json.processPath
    if ($reportedPath -notlike '*\WindowsApps\*' -and $reportedPath -notlike "$layoutDir*") {
        Add-Result -Name $TierName -Status fail -Detail "The report was written by '$reportedPath', which is neither the installed package nor the layout. A stale build was activated; clean tests/GDK.Net.LiveHarness/bin and obj and retry."
        return
    }

    foreach ($step in $report.Json.steps) {
        $colour = switch ($step.outcome) { 'passed' { 'Green' } 'failed' { 'Red' } default { 'DarkGray' } }
        Write-Host ("    {0,-7} {1,-28} {2}" -f $step.outcome, $step.name, $step.detail) -ForegroundColor $colour
    }

    # The harness rewrites the report after every step so that a native crash still leaves evidence
    # behind. That makes a truncated report indistinguishable from a complete one by step outcomes
    # alone -- every step it managed to run passed, because the one that killed it never got to
    # record a failure. `completed` is written true only by the final save, so its absence means the
    # process died mid-run and the tier must fail no matter how healthy the steps look.
    if (-not $report.Json.completed) {
        $running = @($report.Json.steps | Where-Object outcome -eq 'running')
        $where = if ($running.Count -gt 0) { $running[-1].name } elseif (@($report.Json.steps).Count -gt 0) { $report.Json.steps[-1].name } else { '<none>' }
        Add-Result -Name $TierName -Status fail -Detail "The title kept terminating in '$where' and resuming stopped making progress. Check the Application event log for a crash."
        return
    }

    if ($report.Json.failed -eq 0) {
        Add-Result -Name $TierName -Status pass -Detail "$($report.Json.passed) steps passed, $($report.Json.skipped) skipped."
    }
    else {
        Add-Result -Name $TierName -Status fail -Detail "$($report.Json.failed) of $($report.Json.steps.Count) steps failed."
    }
}

function Remove-HarnessPackage {
    # `wdapp unregister` fails with 0x80070490 against a loose registration, and `wdapp install`
    # refuses to overwrite an identity that is still registered, so tear it down with the appx
    # cmdlets first. This is what makes the borrowed identity recoverable.
    $installed = Get-AppxPackage -Name '41336MicrosoftATG.GodotTestApp' -ErrorAction SilentlyContinue
    foreach ($package in $installed) {
        Remove-AppxPackage -Package $package.PackageFullName -ErrorAction Stop
    }

    Start-Sleep -Seconds 2
}

$wdApp = $null
if ($Tier -contains 'Register' -or $Tier -contains 'Install' -or $Restore) {
    $wdApp = Resolve-WdApp
}

$packageArgs = @{ Aot = [bool]$Aot }

if ($Tier -contains 'Layout') {
    & (Join-Path $PSScriptRoot 'package.ps1') -Validate @packageArgs
    if ($LASTEXITCODE -ne 0 -and $null -ne $LASTEXITCODE) { throw 'Layout tier failed.' }
    Add-Result -Name 'Layout' -Status pass -Detail "Published, mapped and validated in $layoutDir."
}

if ($Tier -contains 'Msixvc') {
    & (Join-Path $PSScriptRoot 'package.ps1') -Pack @packageArgs
    $package = Get-ChildItem $outDir -Filter '*.msixvc' -ErrorAction SilentlyContinue |
        Sort-Object LastWriteTime -Descending | Select-Object -First 1
    if ($package) {
        Add-Result -Name 'Msixvc' -Status pass -Detail "$($package.Name), $([math]::Round($package.Length / 1MB, 1)) MB."
    }
    else {
        Add-Result -Name 'Msixvc' -Status fail -Detail "No .msixvc was produced in $outDir."
    }
}

if ($Tier -contains 'Register') {
    if ($PSCmdlet.ShouldProcess($layoutDir, 'wdapp register (loose registration)')) {
        & $wdApp register $layoutDir
        if ($LASTEXITCODE -ne 0) {
            Add-Result -Name 'Register' -Status fail -Detail "wdapp register exited with $LASTEXITCODE."
        }
        else {
            Invoke-LaunchAndCollect -WdApp $wdApp -TierName 'Register'
        }
    }
}

if ($Tier -contains 'Install') {
    $package = Get-ChildItem $outDir -Filter '*.msixvc' -ErrorAction SilentlyContinue |
        Sort-Object LastWriteTime -Descending | Select-Object -First 1
    if (-not $package) {
        Add-Result -Name 'Install' -Status skip -Detail 'No .msixvc available; run the Msixvc tier first.'
    }
    elseif ($PSCmdlet.ShouldProcess($package.FullName, 'wdapp install')) {
        Remove-HarnessPackage
        & $wdApp install $package.FullName
        if ($LASTEXITCODE -ne 0) {
            Add-Result -Name 'Install' -Status fail -Detail "wdapp install exited with $LASTEXITCODE."
        }
        else {
            Invoke-LaunchAndCollect -WdApp $wdApp -TierName 'Install'
        }
    }
}

if ($Restore) {
    Write-Host '==> Restoring the machine' -ForegroundColor Cyan
    & $wdApp terminate $aumid 2>&1 | Out-Null
    Remove-HarnessPackage

    if ($PriorPackage) {
        if (-not (Test-Path $PriorPackage)) {
            throw "-PriorPackage '$PriorPackage' does not exist; the borrowed identity is left uninstalled."
        }

        & $wdApp install $PriorPackage
        if ($LASTEXITCODE -ne 0) {
            throw "Reinstalling '$PriorPackage' failed with exit code $LASTEXITCODE. The borrowed identity is left uninstalled."
        }

        Add-Result -Name 'Restore' -Status pass -Detail "Reinstalled $(Split-Path -Leaf $PriorPackage)."
    }
    else {
        Add-Result -Name 'Restore' -Status pass -Detail 'Harness package removed. Pass -PriorPackage to reinstall the borrowed title.'
    }
}

Write-Host ''
$results | Format-Table -AutoSize | Out-String | Write-Host

if ($results.Where({ $_.Status -eq 'fail' }).Count -gt 0) {
    exit 1
}
