<#
.SYNOPSIS
    Restores the checked-out pin, verifies SDK archives, generates, and builds the solution.
.DESCRIPTION
    -PrepareOnly restores and generates without compiling. prepared.json records verified native
    roots for subsequent native checks and packaging; package roots come from project.assets.json.
    NuGet's configured sources are preserved unless -NuGetConfig is explicitly supplied.
    -Aot publishes (but never executes) the net10.0 win-x64 live harness with real NativeAOT.
#>
[CmdletBinding()]
param(
    [ValidateSet('Debug', 'Release')][string] $Configuration = 'Release',
    [switch] $PrepareOnly,
    [switch] $Test,
    [switch] $Pack,
    [switch] $CheckDocs,
    [switch] $Aot,
    [string] $BaseCommit,
    [string] $ManualEvidencePath,
    [switch] $ReleaseEvidence,
    [string] $ConsumerWorkRoot,
    [string] $ConsumerPackagesDirectory,
    [Alias('ConfigFile')][string] $NuGetConfig,
    [string] $PackagesDirectory
)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
. (Join-Path $PSScriptRoot 'gdk.ps1')
$repo = Split-Path $PSScriptRoot
$pin = Read-GdkPin
$library = Join-Path $repo 'src\GDK.Net\GDK.Net.csproj'
$solution = Join-Path $repo 'GDK.Net.slnx'
$summary = [ordered]@{
    edition = $pin.edition
    sdkVersion = $pin.windowsVersion
    packageVersion = $pin.packageVersion
    restore = 'not run'
    generation = 'not run'
    build = $(if ($PrepareOnly) { 'not selected' } else { 'not run' })
    contracts = $(if ($Test) { 'not run' } else { 'not selected' })
    tests = $(if ($Test) { 'not run' } else { 'not selected' })
    pack = $(if ($Pack) { 'not run' } else { 'not selected' })
    consumers = $(if ($Pack) { 'not run' } else { 'not selected' })
    provenance = $(if ($Pack) { 'not run' } else { 'not selected' })
    aot = $(if ($Aot) { 'not run' } else { 'not selected' })
    docs = $(if ($CheckDocs) { 'not run' } else { 'not selected' })
    native = $(if ($Test) { 'not run' } else { 'not selected' })
    evidence = $(if ($BaseCommit -or $ReleaseEvidence) { 'not run' } else { 'not selected' })
}
$summary.preparedManifest = Join-Path $pin.artifactRoot 'prepared.json'
if ($PrepareOnly -and ($Test -or $Pack -or $CheckDocs -or $Aot)) { throw '-PrepareOnly cannot be combined with build/validation selections.' }
if (($BaseCommit -or $ReleaseEvidence -or $ManualEvidencePath) -and -not $Pack) {
    throw 'Evidence validation requires -Pack to bind the actual candidate package.'
}
if ($ManualEvidencePath -and -not $BaseCommit -and -not $ReleaseEvidence) {
    throw '-ManualEvidencePath requires -BaseCommit or -ReleaseEvidence.'
}
function Invoke-Dotnet([string[]] $Arguments) {
    & dotnet @Arguments
    if ($LASTEXITCODE -ne 0) { throw "dotnet $($Arguments[0]) failed (exit $LASTEXITCODE)." }
}
$restoreOptions = @()
if ($NuGetConfig) { $restoreOptions += @('--configfile', (Resolve-Path $NuGetConfig).Path) }
if ($PackagesDirectory) { $restoreOptions += @('--packages', [IO.Path]::GetFullPath($PackagesDirectory)) }
$marker = Join-Path $pin.artifactRoot 'prepared.json'
New-Item $pin.artifactRoot -ItemType Directory -Force | Out-Null
if (Test-Path $marker) { Remove-Item $marker -Force }
try {
    $phase = 'restore'
    $restoreProject = if ($PrepareOnly) { $library } else { $solution }
    Invoke-Dotnet (@('restore', $restoreProject) + $restoreOptions)
    $summary.restore = 'passed'
    $phase = 'generation'
    $assets = Join-Path $pin.artifactRoot 'obj\GDK.Net\project.assets.json'
    $resolved = Resolve-GdkPackages $pin $assets
    $pythonVersion = & python --version
    if ($LASTEXITCODE -ne 0) { throw "python --version failed (exit $LASTEXITCODE)." }
    $expectedPython = (Get-Content (Join-Path $repo '.python-version') -Raw).Trim()
    if ($pythonVersion.Trim() -cne "Python $expectedPython") { throw "Python $expectedPython is required by .python-version." }
    $resolved.toolchain = @{ python = $expectedPython; dotnet = (Get-Content (Join-Path $repo 'global.json') -Raw | ConvertFrom-Json).sdk.version }
    & (Join-Path $PSScriptRoot 'generate-playfab.ps1') -IncludeRoot $resolved.includeRoot `
        -BinRoot $resolved.binRoot -Edition $pin.edition -OutputRoot (Join-Path $pin.artifactRoot 'generated')
    Write-GdkPrepared $pin $resolved
    Test-GdkPrepared $pin
    $summary.generation = 'passed'
    if (-not $PrepareOnly) {
        $phase = 'build'
        Invoke-Dotnet @('build', $solution, '-c', $Configuration, '--no-restore')
        $summary.build = 'passed'
        if ($Test) {
            $phase = 'contracts'
            & pwsh -NoProfile -File (Join-Path $PSScriptRoot 'test-gdk.ps1')
            if ($LASTEXITCODE -ne 0) { throw "Focused GDK contract checks failed (exit $LASTEXITCODE)." }
            $summary.contracts = 'passed'
            $phase = 'native'
            foreach ($framework in 'net8.0', 'net10.0', 'netstandard2.0') {
                & pwsh -NoProfile -File (Join-Path $PSScriptRoot 'verify-native.ps1') `
                    -WindowsRoot $resolved.windowsRoot `
                    -AssemblyPath (Join-Path $pin.artifactRoot "bin\GDK.Net\$Configuration\$framework\GDK.Net.dll") `
                    -OutputDirectory (Join-Path $pin.artifactRoot "native-verification\$framework")
                if ($LASTEXITCODE -ne 0) { throw "Native compatibility verification failed for $framework (exit $LASTEXITCODE)." }
            }
            $summary.native = 'passed (all three TFMs; compiled/export checks only; no runtime calls)'
            $phase = 'tests'
            Invoke-Dotnet @('test', (Join-Path $repo 'tests\GDK.Net.Tests\GDK.Net.Tests.csproj'),
                '-c', $Configuration, '--no-build', '--no-restore')
            $summary.tests = 'passed'
        }
        if ($Pack) {
            $phase = 'pack'
            $candidate = Join-Path $pin.artifactRoot "packages\GDK.Net.$($pin.packageVersion).nupkg"
            foreach ($stale in @($candidate, (Join-Path $pin.artifactRoot 'provenance.json'),
                (Join-Path $pin.artifactRoot 'packages\provenance.json'))) {
                if (Test-Path $stale) { Remove-Item $stale -Force }
            }
            Invoke-Dotnet @('pack', $library, '-c', $Configuration, '--no-build', '--no-restore',
                '-o', (Join-Path $pin.artifactRoot 'packages'))
            $summary.pack = 'passed'
            $phase = 'consumers'
            $ownedConsumerRoot = -not $ConsumerWorkRoot
            if (-not $ConsumerWorkRoot) {
                $ConsumerWorkRoot = Join-Path (Split-Path $repo) ('.gdk-dotnet-consumer-' + [guid]::NewGuid().ToString('N'))
            }
            $consumerArguments = @('-NoProfile', '-File', (Join-Path $PSScriptRoot 'packaging\verify-package.ps1'),
                '-Package', $candidate, '-WorkRoot', $ConsumerWorkRoot)
            if ($NuGetConfig) { $consumerArguments += @('-NuGetConfig', (Resolve-Path $NuGetConfig).Path) }
            if ($ConsumerPackagesDirectory) { $consumerArguments += @('-PackagesDirectory', [IO.Path]::GetFullPath($ConsumerPackagesDirectory)) }
            if ($Aot) { $consumerArguments += '-Aot' }
            & pwsh @consumerArguments
            if ($LASTEXITCODE -ne 0) { throw "Packaged-consumer verification failed (exit $LASTEXITCODE)." }
            $summary.consumers = 'passed'
            if ($ownedConsumerRoot -and (Test-Path $ConsumerWorkRoot)) { Remove-Item $ConsumerWorkRoot -Recurse -Force }
            $phase = 'provenance'
            & node (Join-Path $PSScriptRoot 'ci\native-evidence.cjs') context --root $repo --prepared $marker `
                --package $candidate --output (Join-Path $pin.artifactRoot 'provenance.json')
            if ($LASTEXITCODE -ne 0) { throw "Native evidence context generation failed (exit $LASTEXITCODE)." }
            $summary.provenance = 'passed'
        }
        if ($Aot) {
            $phase = 'aot'
            $installer = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer'
            if (Test-Path (Join-Path $installer 'vswhere.exe')) { $env:PATH = "$installer;$env:PATH" }
            $aotOutput = Join-Path $pin.artifactRoot 'aot\win-x64'
            if (Test-Path $aotOutput) { Remove-Item $aotOutput -Recurse -Force }
            $publishOptions = @()
            if ($NuGetConfig) { $publishOptions += "-p:RestoreConfigFile=$((Resolve-Path $NuGetConfig).Path)" }
            if ($PackagesDirectory) { $publishOptions += "-p:RestorePackagesPath=$([IO.Path]::GetFullPath($PackagesDirectory))" }
            Invoke-Dotnet (@('publish', (Join-Path $repo 'tests\GDK.Net.LiveHarness\GDK.Net.LiveHarness.csproj'),
                '-c', $Configuration, '-f', 'net10.0', '-r', 'win-x64', '--self-contained', 'true',
                '-p:PublishAot=true', '-o', $aotOutput) + $publishOptions)
            $executable = Join-Path $aotOutput 'GDK.Net.LiveHarness.exe'
            if (-not (Test-Path $executable)) { throw 'NativeAOT produced no executable.' }
            # Native redistributable DLLs are expected; a CLR header/metadata signals IL fallback.
            $binaries = @((Get-Item $executable)) + @(Get-ChildItem $aotOutput -Filter '*.dll' -File)
            foreach ($binary in $binaries) {
                $stream = [IO.File]::OpenRead($binary.FullName)
                $reader = [System.Reflection.PortableExecutable.PEReader]::new($stream)
                try {
                    if ($reader.HasMetadata -or $reader.PEHeaders.CorHeader) {
                        throw "NativeAOT output contains a managed binary: $($binary.Name)."
                    }
                }
                finally { $reader.Dispose(); $stream.Dispose() }
            }
            $summary.aot = 'passed (compiled only; not executed)'
        }
        if ($CheckDocs) {
            $phase = 'docs'
            $docsOptions = @{}
            if ($NuGetConfig) { $docsOptions.NuGetConfig = $NuGetConfig }
            & (Join-Path $PSScriptRoot 'generate-docs.ps1') -Check @docsOptions
            $summary.docs = 'passed'
        }
        if ($BaseCommit -or $ReleaseEvidence) {
            $phase = 'evidence'
            $evidenceArguments = @('gate', '--root', $repo, '--prepared', $marker,
                '--package', $candidate)
            if ($BaseCommit) { $evidenceArguments += @('--base', $BaseCommit) }
            if ($ManualEvidencePath) { $evidenceArguments += @('--attestation', (Resolve-Path $ManualEvidencePath).Path) }
            if ($ReleaseEvidence) { $evidenceArguments += '--release' }
            & node (Join-Path $PSScriptRoot 'ci\native-evidence.cjs') @evidenceArguments
            if ($LASTEXITCODE -ne 0) { throw "Native evidence validation failed (exit $LASTEXITCODE)." }
            $summary.evidence = 'passed'
        }
    }
}
catch {
    $summary[$phase] = 'failed'
    $summary.failure = $_.Exception.Message
    throw
}
finally {
    $summary | ConvertTo-Json | Set-Content (Join-Path $pin.artifactRoot 'build-summary.json') -Encoding utf8
    $summary | ConvertTo-Json | Write-Host
}
