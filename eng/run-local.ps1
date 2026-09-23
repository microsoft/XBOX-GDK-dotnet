<#
.SYNOPSIS
    Publishes the live harness or the sample as a self-contained executable and runs it unpackaged.

.DESCRIPTION
    A GDK title is normally packaged with makepkg, registered with wdapp and launched by AUMID --
    that is what eng/package.ps1 and eng/run-package-tests.ps1 do, and it remains the configuration
    a title actually ships in. But packaging exists here only to give the process an identity, and
    for a PC build MicrosoftGame.config next to the executable supplies the same identity. So for
    day-to-day work the app can just be run:

        eng\run-local.ps1                      # the harness, self-contained, unpackaged
        eng\run-local.ps1 -Project Sample      # the sample
        eng\run-local.ps1 -Project PlayFabSample
        eng\run-local.ps1 -Aot                 # the NativeAOT build a console title ships
        eng\run-local.ps1 -Project Sample -Arguments '--out','C:\temp\demo'

    The output is a directory that can be copied to another dev-unlocked machine and run there with
    no SDK, no runtime install and no registration.

    Still required:
      * a dev-unlocked machine with an account signed in to the Xbox app, and
      * MicrosoftGame.config and xgameruntime.thunks.dll beside the executable. Both are placed
        there by the build (see eng/packaging/GdkRedist.targets), so this is automatic.

    What this does NOT cover, and what eng/run-package-tests.ps1 is still for: the packaged
    execution environment itself -- an install into WindowsApps, the package's own identity rather
    than a borrowed loose-file one, and launch by the shell rather than by the console host.

.PARAMETER Project
    Which app to run.

      Harness  tests/GDK.Net.LiveHarness -- the exhaustive pass/fail run that writes report.json.
      Sample   samples/GDK.Net.UserSample -- the readable demonstration app.
      PlayFabSample
               samples/GDK.Net.PlayFabSample -- the readable PlayFab demonstration app.
      MultiplayerHarness
               tests/GDK.Net.MultiplayerHarness -- the multi-process Lobby and Party run. It
               spawns further copies of itself as participants, so it needs no extra setup.

    Unlike the packaged path, both can be published and run side by side: with no package
    registered there is no shared identity to collide over.

.PARAMETER Aot
    Publish with NativeAOT rather than a self-contained IL publish. This is the configuration an
    Xbox title ships in, so it is worth running at least once before shipping a change. Requires
    the MSVC toolchain, because ILC shells out to link.exe.

.PARAMETER Arguments
    Passed through to the app. The harness takes --out, --scid, --playfab-title-id and --only (a
    comma-separated list of id prefixes, e.g. --only runtime,users,playfab); the samples take
    --out, and the PlayFab sample also takes --title-id and --allow-ui.

.PARAMETER NoRun
    Publish only. Use when the goal is a directory to copy to a test machine.
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [ValidateSet('Harness', 'Sample', 'PlayFabSample', 'MultiplayerHarness')]
    [string] $Project = 'Harness',

    [string] $Configuration = 'Release',
    [ValidateSet('win-x64', 'win-arm64')]
    [string] $RuntimeIdentifier = 'win-x64',
    [switch] $Aot,
    [string[]] $Arguments = @(),
    [switch] $NoRun
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot

# The assembly name doubles as the executable name, so both are derived from one place.
$projectName = switch ($Project) {
    'Sample'             { 'GDK.Net.UserSample' }
    'PlayFabSample'      { 'GDK.Net.PlayFabSample' }
    'MultiplayerHarness' { 'GDK.Net.MultiplayerHarness' }
    default              { 'GDK.Net.LiveHarness' }
}
$projectDir = if ($Project -like '*Sample') { 'samples' } else { 'tests' }
$projectPath = Join-Path $repoRoot "$projectDir\$projectName\$projectName.csproj"

# Deliberately not artifacts/package: that directory is the makepkg layout, and overwriting it here
# would make a later `makepkg pack` silently package whatever this script last produced.
$outputDir = Join-Path $repoRoot "artifacts\local\$projectName"

$arch = if ($RuntimeIdentifier -eq 'win-arm64') { 'arm64' } else { 'x64' }

if (-not $PSCmdlet.ShouldProcess($outputDir, "Publish $projectName self-contained")) {
    Write-Host "Would publish $projectPath to $outputDir." -ForegroundColor Yellow
    return
}

if (Test-Path $outputDir) {
    # A stale publish silently mixes old binaries into a new run.
    Remove-Item $outputDir -Recurse -Force
}

$publishArgs = @(
    'publish', $projectPath,
    '-c', $Configuration,
    '-r', $RuntimeIdentifier,
    '--self-contained', 'true',
    '-o', $outputDir
)

# Both apps multi-target, so publish must name one framework. NativeAOT uses net10.0 because the
# NativeAOT runtime pack is only published from 9.0 onward; the IL publish stays on net8.0, the
# floor GDK.Net supports.
$publishArgs += @('-f', $(if ($Aot) { 'net10.0' } else { 'net8.0' }))

if ($Aot) {
    # ILC locates link.exe by running vswhere.exe from PATH, and the VS Installer directory is not
    # on PATH by default. When the lookup fails the target captures the shell's error text as the
    # linker path, so the failure surfaces as an MSB3073 that never mentions vswhere.
    $vsInstaller = 'C:\Program Files (x86)\Microsoft Visual Studio\Installer'
    if ((Test-Path (Join-Path $vsInstaller 'vswhere.exe')) -and $env:PATH -notlike "*$vsInstaller*") {
        $env:PATH = "$vsInstaller;$env:PATH"
    }

    $publishArgs += '-p:PublishAot=true'
}

Write-Host "==> Publishing $projectName ($RuntimeIdentifier, $(if ($Aot) { 'NativeAOT' } else { 'self-contained' }))" -ForegroundColor Cyan
& dotnet @publishArgs
if ($LASTEXITCODE -ne 0) {
    throw "Publish failed with exit code $LASTEXITCODE."
}

# Without these two the app builds and starts but cannot do anything useful, and the failures it
# produces do not name the missing file: a missing thunks DLL surfaces as 0x89240101 from
# initialization, and a missing MicrosoftGame.config as 0x89245110 ("no package identity") from the
# first XUser call. Check here, where the cause can be stated.
foreach ($required in 'xgameruntime.thunks.dll', 'MicrosoftGame.config') {
    if (-not (Test-Path (Join-Path $outputDir $required))) {
        throw "$required is missing from '$outputDir'. Without it the published app cannot run unpackaged. See eng/packaging/GdkRedist.targets."
    }
}

$exePath = Join-Path $outputDir "$projectName.exe"
Write-Host "Published to $outputDir" -ForegroundColor Green

if ($NoRun) {
    Write-Host "Run it with: $exePath" -ForegroundColor Green
    return
}

Write-Host "==> Running $projectName.exe" -ForegroundColor Cyan

# Run from the output directory: the app resolves xgameruntime.thunks.dll and MicrosoftGame.config
# relative to the executable, but a title reads well-known files relative to the working directory,
# so keep the two the same as they are when the shell launches a packaged title.
Push-Location $outputDir
try {
    & $exePath @Arguments
    $exitCode = $LASTEXITCODE
}
finally {
    Pop-Location
}

if ($exitCode -ne 0) {
    throw "$projectName.exe exited with code $exitCode."
}

Write-Host "$projectName.exe exited with code 0." -ForegroundColor Green
