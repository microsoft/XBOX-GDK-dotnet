<#
.SYNOPSIS
    Builds the GDK package layout and .msixvc for the live harness or the sample.

.DESCRIPTION
    Produces the artefacts needed to run a GDK.Net app as a real packaged GDK title:

      artifacts/package/layout/   self-contained publish output plus MicrosoftGame.config
                                  and the shared ShellVisuals from eng/packaging
      artifacts/package/layout.xml  mapping file from `makepkg genmap`
      artifacts/package/out/      `makepkg validate` log and the packed .msixvc

    Requires an installed Microsoft GDK. This cannot run on a hosted CI runner.

.PARAMETER Project
    Which app to package.

      Harness  tests/GDK.Net.LiveHarness -- the exhaustive pass/fail run that writes report.json.
               This is what eng/run-package-tests.ps1 drives, and the default.

      Sample   samples/GDK.Net.UserSample -- the readable demonstration app, which writes a plain
               text log instead of a report.

    Both borrow the same package identity, so only one can be registered at a time.

.PARAMETER Pack
    Also run `makepkg pack /pc` to produce the .msixvc. Skipped by default because packing is slow.

.PARAMETER Aot
    Publish with NativeAOT instead of a self-contained IL publish. Xbox consoles require NativeAOT,
    so this is the configuration a console title actually ships in, and running the packaged app
    this way is what proves GDK.Net works under it. Requires the MSVC toolchain (ILC shells out to
    link.exe), so it cannot run on a hosted CI runner.

.PARAMETER Clean
    Delete artifacts/package before building.
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [ValidateSet('Harness', 'Sample')]
    [string] $Project = 'Harness',

    [string] $Configuration = 'Release',
    [ValidateSet('win-x64', 'win-arm64')]
    [string] $RuntimeIdentifier = 'win-x64',
    [switch] $Aot,
    [switch] $Pack,
    [switch] $Validate,
    [switch] $Clean
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot

# The assembly name doubles as the executable name MicrosoftGame.config declares, so both are
# derived from one place and the layout check below stays honest.
$projectName = if ($Project -eq 'Sample') { 'GDK.Net.UserSample' } else { 'GDK.Net.LiveHarness' }
$projectDir = if ($Project -eq 'Sample') { 'samples' } else { 'tests' }
$projectPath = Join-Path $repoRoot "$projectDir\$projectName\$projectName.csproj"

$packageRoot = Join-Path $repoRoot 'artifacts\package'
$layoutDir = Join-Path $packageRoot 'layout'
$mappingFile = Join-Path $packageRoot 'layout.xml'
$outDir = Join-Path $packageRoot 'out'

function Resolve-MakePkg {
    if (-not $env:GameDK) {
        throw "The Microsoft GDK is not installed: %GameDK% is not set. Package builds require a local GDK install and cannot run on a hosted CI runner."
    }

    $makePkg = Join-Path $env:GameDK 'bin\makepkg.exe'
    if (-not (Test-Path $makePkg)) {
        throw "makepkg.exe was not found at '$makePkg'. Reinstall the Microsoft GDK."
    }

    return $makePkg
}

function Invoke-Tool {
    param(
        [Parameter(Mandatory)] [string] $FilePath,
        [Parameter(Mandatory)] [string[]] $Arguments,
        [string] $Activity
    )

    Write-Host "==> $Activity" -ForegroundColor Cyan
    Write-Verbose "$FilePath $($Arguments -join ' ')"

    & $FilePath @Arguments
    if ($LASTEXITCODE -ne 0) {
        throw "$Activity failed with exit code $LASTEXITCODE."
    }
}

$makePkg = Resolve-MakePkg

if ($Clean -and (Test-Path $packageRoot)) {
    if ($PSCmdlet.ShouldProcess($packageRoot, 'Remove package artifacts')) {
        Remove-Item $packageRoot -Recurse -Force
    }
}

if (-not $PSCmdlet.ShouldProcess($layoutDir, 'Build GDK package layout')) {
    Write-Host "Would publish $projectPath to $layoutDir and run makepkg genmap/validate/pack." -ForegroundColor Yellow
    return
}

New-Item -ItemType Directory -Path $layoutDir -Force | Out-Null
New-Item -ItemType Directory -Path $outDir -Force | Out-Null

# A stale layout silently produces a stale package; publish into a clean directory every time.
Get-ChildItem $layoutDir -Force | Remove-Item -Recurse -Force

$publishArgs = @(
    'publish', $projectPath,
    '-c', $Configuration,
    '-r', $RuntimeIdentifier,
    '--self-contained', 'true',
    '-o', $layoutDir
)

# The sample multi-targets, so publish must name one framework. NativeAOT uses net10.0 because the
# NativeAOT runtime pack is only published from 9.0 onward; the IL publish stays on net8.0, the
# floor GDK.Net supports, so the default layout proves the oldest supported target still works.
$publishFramework = if ($Aot) { 'net10.0' } else { 'net8.0' }
$publishArgs += @('-f', $publishFramework)
if ($Aot) {
    # ILC shells out to link.exe and locates it by running vswhere.exe from PATH. On a machine
    # where the VS Installer directory is not on PATH -- which is the default -- that lookup fails
    # and, because the target captures the shell's combined output as the linker path, the failure
    # surfaces as the baffling
    #     MSB3073: The command "'vswhere.exe' is not recognized ...;...\link.exe" exited with code 3
    # Prepending the installer directory is enough; it must be done for this process because
    # `dotnet publish` inherits it.
    $vsInstaller = 'C:\Program Files (x86)\Microsoft Visual Studio\Installer'
    if ((Test-Path (Join-Path $vsInstaller 'vswhere.exe')) -and $env:PATH -notlike "*$vsInstaller*") {
        $env:PATH = "$vsInstaller;$env:PATH"
    }

    $publishArgs += '-p:PublishAot=true'
}

Invoke-Tool -FilePath 'dotnet' -Activity "Publishing $projectName ($RuntimeIdentifier, $(if ($Aot) { 'NativeAOT' } else { 'self-contained' }))" -Arguments $publishArgs

if ($Aot) {
    # A NativeAOT publish must leave a single native executable behind. If any managed assembly or
    # the runtime host is still there, something silently fell back to an IL publish and the
    # packaged run would not be testing what it claims to.
    $strays = Get-ChildItem $layoutDir -Filter '*.dll' -File |
        Where-Object { $_.Name -notlike 'xgameruntime.*' }
    if ($strays) {
        throw "The NativeAOT publish left managed assemblies in the layout ($($strays.Name -join ', ')), so it fell back to an IL publish."
    }
}

# xgameruntime.thunks.dll is the module every P/Invoke binds to (see src/GDK.Net/Interop/Native.cs).
# It is NOT installed system-wide -- only XGameRuntime.dll is in System32, and that exports nothing
# useful -- so it must be redistributed next to the game executable or the title fails to launch.
$thunksArch = if ($RuntimeIdentifier -eq 'win-arm64') { 'arm64' } else { 'x64' }
$thunks = Join-Path $env:GameDKCoreLatest "windows\bin\$thunksArch\xgameruntime.thunks.dll"
if (-not (Test-Path $thunks)) {
    throw "xgameruntime.thunks.dll was not found at '$thunks'. The package would build but could not launch."
}

Copy-Item $thunks $layoutDir -Force

# MicrosoftGame.config and the ShellVisuals must sit at the layout root next to the executable.
# The publish already copies them there -- the config from the project directory, the PNGs linked
# in from eng/packaging/ShellVisuals -- so this only asserts the result.
$required = @(
    "$projectName.exe",
    'xgameruntime.thunks.dll',
    'MicrosoftGame.config',
    'StoreLogo.png',
    'Square44x44Logo.png',
    'Square150x150Logo.png',
    'Square480x480Logo.png'
)

$missing = $required | Where-Object { -not (Test-Path (Join-Path $layoutDir $_)) }
if ($missing) {
    throw "The package layout is incomplete; missing: $($missing -join ', ')"
}

# The executable name in MicrosoftGame.config must match what was actually published, or the
# package registers but cannot launch.
[xml] $gameConfig = Get-Content (Join-Path $layoutDir 'MicrosoftGame.config')
$declaredExe = $gameConfig.Game.ExecutableList.Executable.Name
if (-not (Test-Path (Join-Path $layoutDir $declaredExe))) {
    throw "MicrosoftGame.config declares executable '$declaredExe', which is not present in the layout."
}

Write-Host "Layout ready: $layoutDir" -ForegroundColor Green
Write-Host "  Project  : $projectName ($Project)"
Write-Host "  Identity : $($gameConfig.Game.Identity.Name) $($gameConfig.Game.Identity.Version)"
Write-Host "  TitleId  : $($gameConfig.Game.TitleId)   StoreId: $($gameConfig.Game.StoreId)"
Write-Host "  Files    : $((Get-ChildItem $layoutDir -Recurse -File).Count)"

Invoke-Tool -FilePath $makePkg -Activity 'Generating the package mapping file' -Arguments @(
    'genmap', '/f', $mappingFile, '/d', $layoutDir
)

if ($Validate -or $Pack) {
    Invoke-Tool -FilePath $makePkg -Activity 'Validating the package' -Arguments @(
        'validate', '/pc', '/f', $mappingFile, '/d', $layoutDir, '/pd', $outDir
    )
}

if ($Pack) {
    Invoke-Tool -FilePath $makePkg -Activity 'Packing the .msixvc' -Arguments @(
        'pack', '/pc', '/f', $mappingFile, '/d', $layoutDir, '/pd', $outDir
    )

    $package = Get-ChildItem $outDir -Filter '*.msixvc' | Sort-Object LastWriteTime -Descending | Select-Object -First 1
    if (-not $package) {
        throw "makepkg pack reported success but produced no .msixvc in '$outDir'."
    }

    Write-Host "Package: $($package.FullName) ($([math]::Round($package.Length / 1MB, 1)) MB)" -ForegroundColor Green
}
