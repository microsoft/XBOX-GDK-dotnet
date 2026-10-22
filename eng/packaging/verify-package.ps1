<#
.SYNOPSIS
    Checks a packed library and ordinary out-of-checkout consumers without executing them.
.DESCRIPTION
    Uses configured NuGet sources, optionally an explicitly selected config, plus a local package
    feed. WorkRoot must be an isolated directory outside the checkout. No shared cache is changed.
    Aot compiles a net10.0 win-x64 consumer; netstandard2.0 is build-only.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $Package,
    [Parameter(Mandatory)][string] $WorkRoot,
    [string] $NuGetConfig,
    [string] $PackagesDirectory,
    [string] $ConflictingSdkVersion = '2604.5.7903',
    [ValidateSet('All', 'Native')][string] $CheckSet = 'All',
    [switch] $Aot
)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
. (Join-Path $PSScriptRoot '..\gdk.ps1')
$repo = Split-Path (Split-Path $PSScriptRoot)
$pin = Read-GdkPin
$Package = (Resolve-Path $Package).Path
$provenancePath = Join-Path (Split-Path $Package) 'provenance.json'
if (Test-Path $provenancePath) { Remove-Item $provenancePath -Force }
$WorkRoot = [IO.Path]::GetFullPath($WorkRoot)
if ($WorkRoot.StartsWith("$repo\", [StringComparison]::OrdinalIgnoreCase) -or $WorkRoot -eq $repo) {
    throw 'Consumer WorkRoot must be outside the repository, so no repository build imports can participate.'
}
if ($ConflictingSdkVersion -eq $pin.windowsVersion) { throw 'The negative SDK version must differ from the release pin.' }
if (-not $NuGetConfig -and (Test-Path (Join-Path $repo 'NuGet.Config'))) {
    $NuGetConfig = Join-Path $repo 'NuGet.Config'
}
if ($NuGetConfig) { $NuGetConfig = (Resolve-Path $NuGetConfig).Path }
if ($PackagesDirectory) { $PackagesDirectory = [IO.Path]::GetFullPath($PackagesDirectory) }
else { $PackagesDirectory = Join-Path $WorkRoot 'packages' }
New-Item $WorkRoot -ItemType Directory -Force | Out-Null
$feed = Join-Path $WorkRoot 'feed'
New-Item $feed -ItemType Directory -Force | Out-Null
$logRoot = Join-Path $WorkRoot 'logs'
New-Item $logRoot -ItemType Directory -Force | Out-Null
# Stop directory traversal even if this work directory is inside another source checkout.
'<Project />' | Set-Content (Join-Path $WorkRoot 'Directory.Build.props')
'<Project />' | Set-Content (Join-Path $WorkRoot 'Directory.Build.targets')
$checks = [Collections.Generic.List[object]]::new()
$savedEnvironment = @{}
foreach ($name in 'GameDK', 'GameDKCoreLatest') {
    $savedEnvironment[$name] = [Environment]::GetEnvironmentVariable($name, 'Process')
    [Environment]::SetEnvironmentVariable($name, $null, 'Process')
}

function Assert-Check([bool] $Condition, [string] $Message) {
    if (-not $Condition) { throw $Message }
}
function Invoke-Check([string] $Name, [string[]] $Arguments, [switch] $ExpectNativeError) {
    $log = Join-Path $logRoot "$Name.log"
    $output = & dotnet @Arguments 2>&1
    $exit = $LASTEXITCODE
    $output | Set-Content $log
    $text = $output -join "`n"
    if ($ExpectNativeError) {
        Assert-Check ($exit -ne 0 -and $text.Contains('GDKNET001')) "$Name did not fail with the expected native-input validation. See $log."
    }
    else { Assert-Check ($exit -eq 0) "$Name failed (exit $exit). See $log.`n$text" }
    $checks.Add([ordered]@{ name = $Name; result = 'passed'; command = "dotnet $($Arguments -join ' ')"; exitCode = $exit })
    Write-Host "PASS $Name"
}
function Restore-Consumer([string] $Name, [string] $Project, [string[]] $Properties) {
    $arguments = @('restore', $Project, '--packages', $PackagesDirectory,
        "-p:RestoreAdditionalProjectSources=$feed", '--nologo', '-v', 'minimal') + $Properties
    if ($NuGetConfig) { $arguments += @('--configfile', $NuGetConfig) }
    Invoke-Check "$Name-restore" $arguments
    $assets = Get-Content (Join-Path (Split-Path $Project) 'obj\project.assets.json') -Raw | ConvertFrom-Json -AsHashtable
    $library = $assets.libraries["GDK.Net/$($pin.packageVersion)"]
    Assert-Check ($null -ne $library) 'The candidate package is absent from consumer restore.'
    $candidate = @($assets.packageFolders.Keys | ForEach-Object {
        Join-Path $_ "$($library.path.Replace('/', '\'))\gdk.net.$($pin.packageVersion).nupkg"
    } | Where-Object { Test-Path $_ })
    Assert-Check ($candidate.Count -ge 1 -and (Get-FileHash $candidate[0]).Hash -ceq (Get-FileHash $Package).Hash) `
        'Restore selected different cached GDK.Net bytes. Select a fresh isolated PackagesDirectory; existing caches are never modified.'
}
function New-Consumer([string] $Name) {
    $directory = Join-Path $WorkRoot $Name
    New-Item $directory -ItemType Directory -Force | Out-Null
    Copy-Item (Join-Path $PSScriptRoot 'fixtures\*') $directory -Force
    return (Join-Path $directory 'Consumer.csproj')
}
function Expected-Native([string] $Family) {
    $names = @('xgameruntime.thunks.dll')
    if ($Family -in 'XboxLive', 'All') { $names += 'Microsoft.Xbox.Services.C.Thunks.dll', 'libHttpClient.dll' }
    if ($Family -in 'PlayFab', 'Multiplayer', 'Party', 'All') {
        $names += 'PlayFabCore.dll', 'PlayFabServices.dll', 'PlayFabGameSave.dll', 'libHttpClient.dll'
    }
    if ($Family -in 'Multiplayer', 'All') { $names += 'PlayFabMultiplayer.dll' }
    if ($Family -in 'Party', 'All') { $names += 'Party.dll', 'PartyXboxLive.dll' }
    return @($names | Sort-Object -Unique)
}
function Assert-NativeOutput([string] $Directory, [string] $Family, [string] $SourceDirectory) {
    $expected = @(Expected-Native $Family)
    $all = @(Expected-Native 'All')
    $actual = @(Get-ChildItem $Directory -Filter '*.dll' -File | Where-Object Name -in $all | Select-Object -ExpandProperty Name | Sort-Object)
    Assert-Check (($actual -join ',') -ceq ($expected -join ',')) "Incorrect or duplicate native family output in $Directory."
    foreach ($name in $expected) {
        Assert-Check ((Get-FileHash (Join-Path $Directory $name)).Hash -ceq
            (Get-FileHash (Join-Path $SourceDirectory $name)).Hash) "Wrong-architecture native binary: $name."
    }
}

try {
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [IO.Compression.ZipFile]::OpenRead($Package)
    try {
        $entries = @($archive.Entries | Select-Object -ExpandProperty FullName)
        $reader = [IO.StreamReader]::new($archive.GetEntry('GDK.Net.nuspec').Open())
        try { [xml]$nuspec = $reader.ReadToEnd() } finally { $reader.Dispose() }
        $metadata = $nuspec.package.metadata
        Assert-Check ($metadata.id -ceq 'GDK.Net' -and $metadata.version -ceq $pin.packageVersion) 'Package identity does not match the checked-out pin.'
        Assert-Check ($metadata.projectUrl -ceq 'https://github.com/microsoft/XBOX-GDK-dotnet' -and
            $metadata.repository.url -ceq 'https://github.com/microsoft/XBOX-GDK-dotnet' -and
            $metadata.repository.commit -match '^[0-9a-f]{40}$') 'Missing public repository URL or source SHA.'
        Assert-Check ($metadata.license.InnerText -ceq 'MIT' -and $metadata.readme -ceq 'README.md') 'Missing license/readme metadata.'
        foreach ($required in 'README.md', 'LICENSE', 'buildTransitive/GDK.Net.props', 'buildTransitive/GDK.Net.targets',
            'buildTransitive/GdkRedist.targets', 'buildTransitive/GdkNet.ResolveNativeAssets.cs') {
            Assert-Check ($entries -ccontains $required) "Package is missing $required."
        }
        Assert-Check (@($metadata.dependencies.group).Count -eq 3) 'Expected exactly three dependency groups.'
        foreach ($framework in 'net8.0', 'net10.0', '.NETStandard2.0') {
            $group = @($metadata.dependencies.group | Where-Object targetFramework -CEQ $framework)
            Assert-Check ($group.Count -eq 1) "Missing dependency group $framework."
            Assert-Check (@($group[0].dependency).Count -eq 2) "Unexpected dependency leakage in $framework."
            foreach ($kind in 'Windows', 'Core') {
                $dependency = @($group[0].dependency | Where-Object id -CEQ "Microsoft.GDK.$kind")
                $version = $pin["$($kind.ToLowerInvariant())Version"]
                Assert-Check ($dependency.Count -eq 1 -and $dependency[0].version -ceq "[$version]") "Non-exact $kind dependency in $framework."
            }
        }
        $libraries = @($entries | Where-Object { $_ -like 'lib/*' })
        $expectedLibraries = @('net8.0', 'net10.0', 'netstandard2.0' | ForEach-Object { "lib/$_/GDK.Net.dll"; "lib/$_/GDK.Net.xml" })
        Assert-Check ((($libraries | Sort-Object) -join ',') -ceq (($expectedLibraries | Sort-Object) -join ',')) 'Package must contain DLL/XML pairs for every TFM and no additional lib assets.'
        Assert-Check (@($entries | Where-Object { $_ -match '(?i)\.(h|hpp|lib|exe)$' -or ($_ -match '(?i)\.dll$' -and $_ -notlike 'lib/*/GDK.Net.dll') }).Count -eq 0) 'Native SDK payload must not be bundled.'
        $inspection = Join-Path $WorkRoot 'assembly-inspection'
        New-Item $inspection -ItemType Directory -Force | Out-Null
        foreach ($framework in 'net8.0', 'net10.0', 'netstandard2.0') {
            $path = Join-Path $inspection "$framework.dll"
            [IO.Compression.ZipFileExtensions]::ExtractToFile($archive.GetEntry("lib/$framework/GDK.Net.dll"), $path, $true)
            $identity = [Reflection.AssemblyName]::GetAssemblyName($path)
            Assert-Check ($identity.Name -ceq 'GDK.Net' -and $identity.Version.ToString() -ceq "$($pin.library.major).$($pin.library.minor).0.0") "Invalid library-based assembly identity for $framework."
            $fileVersion = [Diagnostics.FileVersionInfo]::GetVersionInfo($path).FileVersion
            Assert-Check ($fileVersion -ceq "$($pin.library.major).$($pin.library.minor).0.0") "Invalid library-based file version for $framework."
            Assert-Check ($archive.GetEntry("lib/$framework/GDK.Net.xml").Length -gt 0) "Empty XML documentation for $framework."
        }
    }
    finally { $archive.Dispose() }
    $checks.Add([ordered]@{ name = 'package-structure'; result = 'passed' })
    Copy-Item $Package $feed -Force
    $common = @("-p:ConsumerPackageVersion=$($pin.packageVersion)", '-p:ConsumerPackageId=GDK.Net')
    $carrierVersion = "1.0.0-check.$([Guid]::NewGuid().ToString('N'))"
    $carrier = New-Consumer 'carrier'
    $carrierProperties = $common + @('-p:ConsumerFramework=netstandard2.0', '-p:OutputType=Library',
        '-p:PackageId=GdkNet.Verification.Carrier', "-p:PackageVersion=$carrierVersion")
    Restore-Consumer 'carrier' $carrier $carrierProperties
    Invoke-Check 'carrier-pack' (@('pack', $carrier, '--no-restore', '-o', $feed, '--nologo', '-v', 'minimal') + $carrierProperties)
    $lastProject = $null
    $routes = if ($CheckSet -eq 'All') { @('direct', 'transitive') } else { @() }
    foreach ($route in $routes) {
        foreach ($framework in 'net8.0', 'net10.0', 'netstandard2.0') {
            foreach ($rid in 'win-x64', 'win-arm64') {
                $name = "$route-$framework-$rid"
                $project = New-Consumer $name
                $properties = @("-p:ConsumerFramework=$framework", "-p:RuntimeIdentifier=$rid")
                if ($route -eq 'direct') { $properties += $common }
                else { $properties += '-p:ConsumerPackageId=GdkNet.Verification.Carrier', "-p:ConsumerPackageVersion=$carrierVersion" }
                Restore-Consumer $name $project $properties
                $assets = Join-Path (Split-Path $project) 'obj\project.assets.json'
                $resolved = Resolve-GdkPackages $pin $assets
                $arch = $rid.Substring(4)
                $source = Join-Path $resolved.windowsRoot "windows\bin\$arch"
                foreach ($family in 'Default', 'XboxLive', 'PlayFab', 'Multiplayer', 'Party', 'All') {
                    $output = Join-Path (Split-Path $project) "output\$family"
                    $familyProperties = @("-p:GdkNetIncludeXboxLive=$($family -in 'XboxLive', 'All')",
                        "-p:GdkNetIncludePlayFab=$($family -in 'PlayFab', 'All')",
                        "-p:GdkNetIncludePlayFabMultiplayer=$($family -in 'Multiplayer', 'All')",
                        "-p:GdkNetIncludePlayFabParty=$($family -in 'Party', 'All')")
                    Invoke-Check "$name-$family" (@('build', $project, '--no-restore', '-o', $output, '--nologo', '-v', 'minimal') + $properties + $familyProperties)
                    Assert-NativeOutput $output $family $source
                }
                if ($route -eq 'direct' -and $framework -eq 'net10.0' -and $rid -eq 'win-x64') {
                    $stale = Join-Path $output 'xgameruntime.thunks.dll'
                    'not-the-pinned-runtime' | Set-Content $stale
                    (Get-Item $stale).LastWriteTimeUtc = [DateTime]::UtcNow.AddDays(1)
                    Invoke-Check 'replace-newer-stale-output' (@('build', $project, '--no-restore', '-o', $output,
                        '--nologo', '-v', 'minimal') + $properties + $familyProperties)
                    Assert-NativeOutput $output 'All' $source
                }
                if ($framework -ne 'netstandard2.0') {
                    $output = Join-Path (Split-Path $project) 'publish'
                    Invoke-Check "$name-publish" (@('publish', $project, '--no-restore', '--self-contained', 'false',
                        '-o', $output, '--nologo', '-v', 'minimal') + $properties + $familyProperties)
                    Assert-NativeOutput $output 'All' $source
                }
                $lastProject = $project
                $lastProperties = $properties
            }
        }
    }
    if ($CheckSet -eq 'Native') {
        $lastProject = New-Consumer 'native-inputs'
        $lastProperties = $common + @('-p:ConsumerFramework=netstandard2.0', '-p:RuntimeIdentifier=win-arm64')
        Restore-Consumer 'native-inputs' $lastProject $lastProperties
    }

    $matching = New-Consumer 'direct-sdk-matching'
    $properties = $common + @('-p:ConsumerFramework=net10.0', "-p:DirectWindowsVersion=$($pin.windowsVersion)", "-p:DirectCoreVersion=$($pin.coreVersion)")
    Restore-Consumer 'direct-sdk-matching' $matching $properties
    Invoke-Check 'direct-sdk-matching-build' (@('build', $matching, '--no-restore', '--nologo', '-v', 'minimal') + $properties)
    foreach ($kind in 'Windows', 'Core') {
        $conflict = New-Consumer "direct-sdk-conflict-$kind"
        $properties = $common + @('-p:ConsumerFramework=net10.0', "-p:Direct${kind}Version=$ConflictingSdkVersion")
        # A newer Windows package has a newer Core minimum. Pin both directly in that case so
        # restore actually succeeds with NU1608 warnings, exercising our independent build error.
        if ($kind -eq 'Windows') { $properties += "-p:DirectCoreVersion=$ConflictingSdkVersion" }
        # NU1608 is deliberately not promoted: our target must reject direct-dependency-wins itself.
        Restore-Consumer "direct-sdk-conflict-$kind" $conflict $properties
        Invoke-Check "direct-sdk-conflict-$kind-build" (@('build', $conflict, '--no-restore', '--nologo', '-v', 'minimal') + $properties) -ExpectNativeError
    }

    # Sparse private fixtures use real restored DLLs. Only these copies are removed, never a cache.
    $fixture = Join-Path $WorkRoot 'missing-native-fixture'
    New-Item $fixture -ItemType Directory -Force | Out-Null
    $assets = Get-Content (Join-Path (Split-Path $lastProject) 'obj\project.assets.json') -Raw | ConvertFrom-Json -AsHashtable
    $resolved = Resolve-GdkPackages $pin (Join-Path (Split-Path $lastProject) 'obj\project.assets.json')
    $windowsRelative = $assets.libraries["Microsoft.GDK.Windows/$($pin.windowsVersion)"].path.Replace('/', '\')
    $coreRelative = $assets.libraries["Microsoft.GDK.Core/$($pin.coreVersion)"].path.Replace('/', '\')
    $fixtureBin = Join-Path $fixture "$windowsRelative\native\$($pin.edition)\windows\bin\arm64"
    New-Item $fixtureBin -ItemType Directory -Force | Out-Null
    New-Item (Join-Path $fixture "$coreRelative\native") -ItemType Directory -Force | Out-Null
    $allNames = @(Expected-Native 'All')
    foreach ($name in $allNames) { Copy-Item (Join-Path $resolved.windowsRoot "windows\bin\arm64\$name") $fixtureBin -Force }
    $folders = [ordered]@{ "$fixture\" = @{} }
    foreach ($folder in $assets.packageFolders.Keys) { $folders[$folder] = $assets.packageFolders[$folder] }
    $assets.packageFolders = $folders
    $fixtureAssets = Join-Path $fixture 'project.assets.json'
    $assets | ConvertTo-Json -Depth 100 | Set-Content $fixtureAssets
    foreach ($name in $allNames) {
        $path = Join-Path $fixtureBin $name
        Remove-Item $path -Force
        try {
            Invoke-Check "missing-$name" (@('msbuild', $lastProject, '-t:GdkNetResolveRedistributables',
                "-p:ProjectAssetsFile=$fixtureAssets", '-p:GdkNetIncludeXboxLive=true',
                '-p:GdkNetIncludePlayFab=true', '-p:GdkNetIncludePlayFabMultiplayer=true',
                '-p:GdkNetIncludePlayFabParty=true', '-nologo', '-v:minimal') + $lastProperties) -ExpectNativeError
        }
        finally { Copy-Item (Join-Path $resolved.windowsRoot "windows\bin\arm64\$name") $path }
    }
    if ($Aot) {
        $project = New-Consumer 'native-aot'
        $properties = $common + @('-p:ConsumerFramework=net10.0', '-p:RuntimeIdentifier=win-x64',
            '-p:PublishAot=true', '-p:GdkNetIncludeXboxLive=true', '-p:GdkNetIncludePlayFabParty=true')
        Restore-Consumer 'native-aot' $project $properties
        $installer = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer'
        if (Test-Path (Join-Path $installer 'vswhere.exe')) { $env:PATH = "$installer;$env:PATH" }
        $output = Join-Path (Split-Path $project) 'publish'
        Invoke-Check 'native-aot-publish' (@('publish', $project, '--no-restore', '--self-contained', 'true',
            '-c', 'Release', '-o', $output, '--nologo', '-v', 'minimal') + $properties)
        $executable = Join-Path $output 'GdkNet.PackageConsumer.exe'
        Assert-Check (Test-Path $executable) 'NativeAOT consumer executable is missing.'
        foreach ($binary in @((Get-Item $executable)) + @(Get-ChildItem $output -Filter '*.dll')) {
            $stream = [IO.File]::OpenRead($binary.FullName)
            $pe = [Reflection.PortableExecutable.PEReader]::new($stream)
            try { Assert-Check (-not $pe.HasMetadata -and -not $pe.PEHeaders.CorHeader) "Managed fallback in NativeAOT output: $($binary.Name)." }
            finally { $pe.Dispose(); $stream.Dispose() }
        }
        $assets = Join-Path (Split-Path $project) 'obj\project.assets.json'
        $resolved = Resolve-GdkPackages $pin $assets
        $expected = @('xgameruntime.thunks.dll', 'Microsoft.Xbox.Services.C.Thunks.dll', 'libHttpClient.dll',
            'PlayFabCore.dll', 'PlayFabServices.dll', 'PlayFabGameSave.dll', 'Party.dll', 'PartyXboxLive.dll')
        foreach ($name in $expected) {
            Assert-Check ((Get-FileHash (Join-Path $output $name)).Hash -ceq
                (Get-FileHash (Join-Path $resolved.windowsRoot "windows\bin\x64\$name")).Hash) "Missing or wrong AOT native asset $name."
        }
    }
}
finally {
    foreach ($name in $savedEnvironment.Keys) { [Environment]::SetEnvironmentVariable($name, $savedEnvironment[$name], 'Process') }
    [ordered]@{ packageVersion = $pin.packageVersion; scope = $CheckSet; checks = $checks; nativeAotRequested = [bool]$Aot;
        execution = 'No consumer or native service was executed.' } |
        ConvertTo-Json -Depth 6 | Set-Content (Join-Path $WorkRoot 'verification.json')
}
if (-not $checks.Count) { throw 'No package checks were performed.' }
[ordered]@{
    schemaVersion = 1
    package = [ordered]@{ id = 'GDK.Net'; version = $pin.packageVersion; sha256 = (Get-FileHash $Package).Hash.ToLowerInvariant()
        sourceCommit = [string]$metadata.repository.commit }
    sdk = [ordered]@{ edition = $pin.edition; windowsVersion = $pin.windowsVersion; coreVersion = $pin.coreVersion
        windowsSha512 = $pin.windowsSha512; coreSha512 = $pin.coreSha512 }
    validation = [ordered]@{ scope = $CheckSet; checks = @($checks | ForEach-Object { [ordered]@{ name = $_.name; result = $_.result } })
        nativeAot = $(if ($Aot) { 'compiled-win-x64-not-executed' } else { 'not-requested' })
        liveExecution = 'not-performed'; publication = 'not-performed' }
} | ConvertTo-Json -Depth 8 | Set-Content $provenancePath
Write-Host "Package and consumer checks passed ($($checks.Count)); no publication or live execution."
