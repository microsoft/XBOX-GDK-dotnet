<#
.SYNOPSIS
    Runs focused pin, restore-resolution and generator failure tests without GDK runtime calls.
.DESCRIPTION
    Synthetic SDK directories below artifacts are test fixtures, never production inputs.
#>
[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
. (Join-Path $PSScriptRoot 'gdk.ps1')
$repo = Split-Path $PSScriptRoot
$testRoot = Join-Path $repo ("artifacts\gdk-tests-" + [guid]::NewGuid().ToString('N'))
New-Item $testRoot -ItemType Directory -Force | Out-Null
$script:Checks = 0
function Assert-Rejected([scriptblock] $Action, [string] $Pattern) {
    try { & $Action | Out-Null } catch {
        if ($_.Exception.Message -notmatch $Pattern) { throw "Wrong rejection: $($_.Exception.Message); expected $Pattern" }
        $script:Checks++
        return
    }
    throw "Expected rejection matching $Pattern."
}
try {
    $pinPath = Join-Path $repo '.github\gdk-version.json'
    $original = Get-Content $pinPath -Raw
    $pin = Read-GdkPin
    $script:Checks++
    $cases = @(
        @{ mutate = { param($p) $p.edition = '260405' }; reason = 'Version and edition' },
        @{ mutate = { param($p) $p.edition = 260404 }; reason = 'six-digit string' },
        @{ mutate = { param($p) $p.coreVersion = '2604.5.7903' }; reason = 'match exactly' },
        @{ mutate = { param($p) $p.windowsVersion = '[2604.4.7897]' }; reason = 'Invalid exact' },
        @{ mutate = { param($p) $p.windowsSha512 = 'bad' }; reason = 'SHA512' },
        @{ mutate = { param($p) $p.library.major = 260404 }; reason = 'assembly components' },
        @{ mutate = { param($p) $p.library.minor = '0' }; reason = 'integer' },
        @{ mutate = { param($p) $p.supportedEditions = @('260404') }; reason = 'Unexpected' },
        @{ mutate = { param($p) $p.library.extra = 1 }; reason = 'Unexpected' },
        @{ mutate = { param($p) $p.provenance.windows = 'https://example.invalid/package' }; reason = 'provenance' },
        @{ mutate = { param($p) $p.upgradePolicy = 'automatic' }; reason = 'reviewed pin' },
        @{ mutate = { param($p) $p.maintenancePolicy = 'historical-switch' }; reason = 'release tag' },
        @{ mutate = { param($p) $p.minimum.edition = '260405'; $p.minimum.windowsVersion = $p.minimum.coreVersion = '2604.5.7903' }; reason = 'below the declared minimum' }
    )
    foreach ($case in $cases) {
        $fixture = $original | ConvertFrom-Json -AsHashtable
        & $case.mutate $fixture
        $path = Join-Path $testRoot 'pin.json'
        $fixture | ConvertTo-Json -Depth 8 | Set-Content $path -Encoding utf8
        Assert-Rejected { Read-GdkPin $path } $case.reason
    }
    $duplicate = $original.Replace('"edition": "260404",', '"edition": "260404", "edition": "260404",')
    $duplicate | Set-Content (Join-Path $testRoot 'pin.json')
    Assert-Rejected { Read-GdkPin (Join-Path $testRoot 'pin.json') } 'Duplicate'
    $original.Replace('"edition":', '"\u0065dition":') | Set-Content (Join-Path $testRoot 'pin.json')
    Assert-Rejected { Read-GdkPin (Join-Path $testRoot 'pin.json') } 'Escaped manifest keys'
    $propsPath = [Security.SecurityElement]::Escape((Join-Path $repo 'Directory.Build.props'))
    $msbuildFixture = @'
<Project>
  <Import Project="PROPS_PATH" />
  <PropertyGroup>
    <_FixturePin>{"minimum":{"edition":"260404","windowsVersion":"2604.4.7897","coreVersion":"2604.4.7897"},"edition":"990102","windowsVersion":"9901.2.3","coreVersion":"9901.2.3","library":{"major":1,"minor":0}}</_FixturePin>
  </PropertyGroup>
  <Target Name="Test">
    <MSBuild Projects="$(MSBuildProjectFullPath)" Targets="Check" Properties="_GdkPinJson=$([MSBuild]::Escape('$(_FixturePin)'))" />
  </Target>
  <Target Name="Check">
    <Error Condition="'$(_GdkPinnedEdition)' != '990102' Or '$(GdkWindowsVersion)' != '9901.2.3' Or '$(GdkCoreVersion)' != '9901.2.3'" Text="Minimum policy shadowed the selected top-level pin." />
  </Target>
</Project>
'@
    $fixtureProject = Join-Path $testRoot 'minimum-first-test-only.proj'
    $msbuildFixture.Replace('PROPS_PATH', $propsPath) | Set-Content $fixtureProject
    & dotnet msbuild $fixtureProject -target:Test -verbosity:quiet
    if ($LASTEXITCODE -ne 0) { throw 'MSBuild selected the minimum instead of the top-level pin.' }
    $script:Checks++

    # Tiny synthetic archives exercise hash and packageFolders resolution, not SDK acquisition.
    $cache = Join-Path $testRoot 'cache'
    $libraries = @{}
    foreach ($kind in 'windows', 'core') {
        $id = "microsoft.gdk.$kind"
        $relative = "$id\$($pin.windowsVersion)"
        $root = Join-Path $cache $relative
        New-Item $root -ItemType Directory -Force | Out-Null
        $archive = Join-Path $root "$id.$($pin.windowsVersion).nupkg"
        [IO.File]::WriteAllText($archive, "synthetic-test-only-$kind")
        $hash = [Convert]::ToBase64String([Security.Cryptography.SHA512]::HashData([IO.File]::ReadAllBytes($archive)))
        $pin["${kind}Sha512"] = $hash
        $hash | Set-Content "$archive.sha512"
        $libraries["Microsoft.GDK.$kind/$($pin.windowsVersion)"] = @{ path = $relative }
    }
    $windows = Join-Path $cache "microsoft.gdk.windows\$($pin.windowsVersion)\native\$($pin.edition)"
    $core = Join-Path $cache "microsoft.gdk.core\$($pin.coreVersion)\native"
    foreach ($path in @('windows\include\XUser.h', 'windows\include\playfab\core\PFErrors.h',
        'windows\lib\x64\xgameruntime.lib', 'windows\lib\ARM64\xgameruntime.lib',
        'windows\bin\x64\xgameruntime.thunks.dll', 'windows\bin\arm64\xgameruntime.thunks.dll')) {
        $target = Join-Path $windows $path
        New-Item (Split-Path $target) -ItemType Directory -Force | Out-Null
        [IO.File]::WriteAllText($target, 'test-only')
    }
    New-Item (Join-Path $core 'bin') -ItemType Directory -Force | Out-Null
    [IO.File]::WriteAllText((Join-Path $core 'bin\makepkg.exe'), 'test-only')
    $assetsPath = Join-Path $testRoot 'project.assets.json'
    $assets = @{ libraries = $libraries; packageFolders = @{ $cache = @{} } }
    $assets | ConvertTo-Json -Depth 5 | Set-Content $assetsPath
    $resolved = Resolve-GdkPackages $pin $assetsPath
    if ($resolved.windowsRoot -cne $windows) { throw 'Resolution ignored packageFolders.' }
    $script:Checks++
    $pin.windowsSha512 = [Convert]::ToBase64String([byte[]]::new(64))
    Assert-Rejected { Resolve-GdkPackages $pin $assetsPath } 'archive SHA512'

    # A failed native subprocess invalidates the previous output and removes staging directories.
    $output = Join-Path $testRoot 'failed-generation'
    New-Item $output -ItemType Directory | Out-Null
    [IO.File]::WriteAllText((Join-Path $output 'stale.cs'), 'stale')
    [IO.File]::WriteAllText((Join-Path $resolved.binRoot 'PlayFabCore.dll'), 'test-only')
    $fakeDumpbin = Join-Path $testRoot 'fail-dumpbin.cmd'
    [IO.File]::WriteAllText($fakeDumpbin, "@exit /b 17`r`n")
    Assert-Rejected {
        & (Join-Path $PSScriptRoot 'generate-playfab.ps1') -IncludeRoot $resolved.includeRoot `
            -BinRoot $resolved.binRoot -Edition $pin.edition -OutputRoot $output -Dumpbin $fakeDumpbin
    } 'dumpbin failed.*17'
    if ((Test-Path $output) -or @(Get-ChildItem $testRoot -Filter 'failed-generation.pending-*').Count) {
        throw 'Failed generation retained stale or partial output.'
    }
    $script:Checks++
    foreach ($name in 'PlayFabServices.dll', 'PlayFabGameSave.dll', 'PlayFabMultiplayer.dll', 'Party.dll', 'PartyXboxLive.dll') {
        [IO.File]::WriteAllText((Join-Path $resolved.binRoot $name), 'test-only')
    }
    [IO.File]::WriteAllText($fakeDumpbin, "@echo     1    0 00001000 PFTest`r`n@exit /b 0`r`n")
    $fakePython = Join-Path $testRoot 'fail-python.cmd'
    [IO.File]::WriteAllText($fakePython, "@exit /b 23`r`n")
    Assert-Rejected {
        & (Join-Path $PSScriptRoot 'generate-playfab.ps1') -IncludeRoot $resolved.includeRoot `
            -BinRoot $resolved.binRoot -Edition $pin.edition -OutputRoot $output -Dumpbin $fakeDumpbin -Python $fakePython
    } 'Header parsing failed.*23'
    if ((Test-Path $output) -or @(Get-ChildItem $testRoot -Filter 'failed-generation.pending-*').Count) {
        throw 'Failed Python parsing retained partial output.'
    }
    $script:Checks++
    $pin.artifactRoot = Join-Path $testRoot 'prepared-fixture'
    $fixtureSources = Join-Path $pin.artifactRoot 'generated\src'
    New-Item $fixtureSources -ItemType Directory -Force | Out-Null
    $fixtureSource = Join-Path $fixtureSources 'TestOnly.cs'
    [IO.File]::WriteAllText($fixtureSource, 'test-only-original')
    Write-GdkPrepared $pin $resolved
    Test-GdkPrepared $pin
    $script:Checks++
    [IO.File]::WriteAllText($fixtureSource, 'test-only-changed')
    Assert-Rejected { Test-GdkPrepared $pin } 'input/output changed'
    Remove-Item (Join-Path $pin.artifactRoot 'prepared.json')
    Assert-Rejected { Test-GdkPrepared $pin } 'not prepared.*eng\\build.ps1'
    Write-Host "$script:Checks focused GDK checks passed."
}
finally { Remove-Item $testRoot -Recurse -Force }
