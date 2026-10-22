<#
.SYNOPSIS
    Validates the one checked-out release pin and resolves verified restored SDK inputs.
.DESCRIPTION
    Dot-source for Read-GdkPin, Resolve-GdkPackages and preparation verification. Upgrades change
    the reviewed pin on main; older maintenance starts from its release tag, not an edition switch.
    Package hashes are SHA512 of the complete downloaded nupkg, not NuGet's contentHash.
#>
[CmdletBinding()]
param([switch] $CheckPrepared)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$script:GdkRepo = Split-Path $PSScriptRoot

function Assert-GdkKeys($Object, [string[]] $Expected, [string] $Name) {
    if ($Object -isnot [System.Collections.IDictionary]) { throw "$Name must be an object." }
    $actual = @($Object.Keys | Sort-Object)
    if (($actual -join ',') -cne (($Expected | Sort-Object) -join ',')) { throw "Unexpected or missing inputs in $Name." }
}

function Assert-GdkPair($Object) {
    if ($Object.edition -isnot [string] -or $Object.edition -cnotmatch '^\d{6}$') { throw 'Edition must be a six-digit string.' }
    foreach ($key in 'windowsVersion', 'coreVersion') {
        if ($Object[$key] -isnot [string] -or $Object[$key] -cnotmatch '^\d{4}\.[1-9]\d?\.\d+$') { throw "Invalid exact $key." }
    }
    if ($Object.windowsVersion -cne $Object.coreVersion) { throw 'Windows/Core versions must match exactly.' }
    $parts = $Object.windowsVersion.Split('.')
    if ($Object.edition -cne ($parts[0] + ([int]$parts[1]).ToString('00'))) { throw 'Version and edition do not match.' }
}

function Read-GdkPin([string] $Path = (Join-Path $script:GdkRepo '.github\gdk-version.json')) {
    $text = [IO.File]::ReadAllText($Path)
    if ($text -match '"[^"\r\n]*\\[^"\r\n]*"\s*:') { throw 'Escaped manifest keys are unsupported.' }
    # ConvertFrom-Json collapses duplicate names; reject them before converting.
    $document = [System.Text.Json.JsonDocument]::Parse($text)
    function Test-UniqueJsonKeys($Element) {
        if ($Element.ValueKind -eq [System.Text.Json.JsonValueKind]::Object) {
            $seen = [Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
            foreach ($property in $Element.EnumerateObject()) {
                if (-not $seen.Add($property.Name)) { throw "Duplicate JSON key '$($property.Name)'." }
                Test-UniqueJsonKeys $property.Value
            }
        }
    }
    try { Test-UniqueJsonKeys $document.RootElement } finally { $document.Dispose() }
    $pin = $text | ConvertFrom-Json -AsHashtable
    Assert-GdkKeys $pin @('edition', 'windowsVersion', 'coreVersion', 'windowsSha512', 'coreSha512',
        'provenance', 'library', 'minimum', 'upgradePolicy', 'maintenancePolicy') 'pin'
    Assert-GdkPair $pin
    Assert-GdkKeys $pin.library @('major', 'minor') 'library'
    foreach ($key in 'major', 'minor') {
        if ($pin.library[$key] -isnot [long] -and $pin.library[$key] -isnot [int]) { throw "Library $key must be an integer." }
        if ($pin.library[$key] -lt 0 -or $pin.library[$key] -gt 65534) { throw "Library $key exceeds valid assembly components." }
    }
    Assert-GdkKeys $pin.minimum @('edition', 'windowsVersion', 'coreVersion') 'minimum'
    Assert-GdkPair $pin.minimum
    if ([int]$pin.edition -lt [int]$pin.minimum.edition -or [version]$pin.windowsVersion -lt [version]$pin.minimum.windowsVersion) {
        throw 'Pin is below the declared minimum.'
    }
    if ($pin.upgradePolicy -cne 'reviewed-pin-pr' -or $pin.maintenancePolicy -cne 'branch-from-release-tag') {
        throw 'Upgrades require reviewed pin PRs; older maintenance starts from a release tag.'
    }
    Assert-GdkKeys $pin.provenance @('windows', 'core', 'release') 'provenance'
    foreach ($kind in 'windows', 'core') {
        $hash = $pin["${kind}Sha512"]
        if ($hash -isnot [string] -or $hash -cnotmatch '^[A-Za-z0-9+/]{86}==$' -or
            [Convert]::FromBase64String($hash).Length -ne 64) { throw "Invalid $kind SHA512." }
        $id = if ($kind -eq 'windows') { 'Microsoft.GDK.Windows' } else { 'Microsoft.GDK.Core' }
        if ($pin.provenance[$kind] -cne "https://www.nuget.org/packages/$id/$($pin["${kind}Version"])") {
            throw "Invalid public $kind provenance."
        }
    }
    if ($pin.provenance.release -cne 'https://github.com/microsoft/GDK') { throw 'Invalid public release provenance.' }
    $pin.packageVersion = "$($pin.edition).$($pin.library.major).$($pin.library.minor)"
    $pin.artifactRoot = Join-Path $script:GdkRepo "artifacts\gdk\$($pin.edition)\$($pin.windowsVersion)"
    $pin.pinHash = (Get-FileHash $Path -Algorithm SHA256).Hash
    return $pin
}

function Resolve-GdkPackages($Pin, [string] $AssetsFile) {
    $assets = Get-Content $AssetsFile -Raw | ConvertFrom-Json -AsHashtable
    $result = [ordered]@{}
    foreach ($kind in 'windows', 'core') {
        $id = "Microsoft.GDK.$kind"
        $version = $Pin["${kind}Version"]
        $libraries = @($assets.libraries.Keys | Where-Object { $_ -like "$id/*" })
        if ($libraries.Count -ne 1 -or $libraries[0] -ine "$id/$version") { throw "Restore must resolve exactly $id/$version." }
        $relative = $assets.libraries[$libraries[0]].path.Replace('/', '\')
        $roots = @($assets.packageFolders.Keys | ForEach-Object { Join-Path $_ $relative } | Where-Object { Test-Path $_ })
        if ($roots.Count -ne 1) { throw "Cannot uniquely resolve $id from restore packageFolders." }
        $root = [IO.Path]::GetFullPath($roots[0])
        $archive = Join-Path $root "$($id.ToLowerInvariant()).$version.nupkg"
        if (-not (Test-Path $archive)) { throw "Missing original archive for hash verification: $archive" }
        $stream = [IO.File]::OpenRead($archive)
        try { $hash = [Convert]::ToBase64String([Security.Cryptography.SHA512]::HashData($stream)) }
        finally { $stream.Dispose() }
        if ($hash -cne $Pin["${kind}Sha512"]) { throw "$id archive SHA512 does not match the checked-out pin." }
        $sidecar = (Get-Content "$archive.sha512" -Raw).Trim()
        if ($sidecar -cne $hash) { throw "$id archive SHA512 sidecar mismatch." }
        $result["${kind}PackageRoot"] = $root
    }
    $result.edition = $Pin.edition
    $result.windowsVersion = $Pin.windowsVersion
    $result.coreVersion = $Pin.coreVersion
    $result.windowsRoot = Join-Path $result.windowsPackageRoot "native\$($Pin.edition)"
    $result.includeRoot = Join-Path $result.windowsRoot 'windows\include'
    $result.binRoot = Join-Path $result.windowsRoot 'windows\bin\x64'
    $result.coreRoot = Join-Path $result.corePackageRoot 'native'
    foreach ($path in @(
        (Join-Path $result.includeRoot 'XUser.h'), (Join-Path $result.includeRoot 'playfab\core\PFErrors.h'),
        (Join-Path $result.windowsRoot 'windows\lib\x64\xgameruntime.lib'),
        (Join-Path $result.windowsRoot 'windows\lib\ARM64\xgameruntime.lib'),
        (Join-Path $result.binRoot 'xgameruntime.thunks.dll'),
        (Join-Path $result.windowsRoot 'windows\bin\arm64\xgameruntime.thunks.dll'),
        (Join-Path $result.coreRoot 'bin\makepkg.exe'))) {
        if (-not (Test-Path $path -PathType Leaf)) { throw "Required restored SDK input missing: $path" }
    }
    return $result
}

function Get-GdkGenerationInputs {
    return @((Join-Path $script:GdkRepo '.github\gdk-version.json'),
        (Join-Path $script:GdkRepo '.python-version'),
        (Join-Path $script:GdkRepo 'global.json'),
        (Join-Path $PSScriptRoot 'generate-playfab.ps1'),
        (Join-Path $PSScriptRoot 'gdk.ps1')) +
        @(Get-ChildItem (Join-Path $PSScriptRoot 'playfab') -Filter '*.py' -File | Sort-Object Name | ForEach-Object FullName)
}

function Write-GdkPrepared($Pin, $Resolved) {
    $root = Join-Path $Pin.artifactRoot 'generated'
    $files = @((Get-GdkGenerationInputs)) + @(Get-ChildItem $root -Recurse -File | ForEach-Object FullName)
    $hashes = [ordered]@{}
    foreach ($file in $files) { $hashes[$file] = (Get-FileHash $file -Algorithm SHA256).Hash }
    [ordered]@{ pinHash = $Pin.pinHash; resolved = $Resolved; files = $hashes } |
        ConvertTo-Json -Depth 6 | Set-Content (Join-Path $Pin.artifactRoot 'prepared.json') -Encoding utf8
}

function Test-GdkPrepared($Pin) {
    $marker = Join-Path $Pin.artifactRoot 'prepared.json'
    if (-not (Test-Path $marker)) { throw 'SDK inputs are not prepared. Run pwsh eng\build.ps1 -PrepareOnly (or the complete build).' }
    $prepared = Get-Content $marker -Raw | ConvertFrom-Json -AsHashtable
    if ($prepared.pinHash -cne $Pin.pinHash) { throw 'Pin changed. Run pwsh eng\build.ps1 to restore and regenerate.' }
    $sourceFiles = @($prepared.files.Keys | Where-Object { $_.StartsWith((Join-Path $Pin.artifactRoot 'generated\src\'), [StringComparison]::OrdinalIgnoreCase) -and $_.EndsWith('.cs') })
    if (-not $sourceFiles.Count) { throw 'Prepared inputs contain no production C# sources. Run pwsh eng\build.ps1.' }
    foreach ($file in $prepared.files.Keys) {
        if (-not (Test-Path $file) -or (Get-FileHash $file -Algorithm SHA256).Hash -cne $prepared.files[$file]) {
            throw "Generation input/output changed: $file. Run pwsh eng\build.ps1."
        }
    }
    foreach ($file in (Get-GdkGenerationInputs)) {
        if (-not $prepared.files.Contains($file)) { throw "Unprepared generator input: $file. Run pwsh eng\build.ps1." }
    }
    $generated = @(Get-ChildItem (Join-Path $Pin.artifactRoot 'generated') -Recurse -File)
    foreach ($file in $generated) {
        if (-not $prepared.files.Contains($file.FullName)) { throw "Unexpected generated output: $($file.FullName)." }
    }
}

if ($CheckPrepared) { Test-GdkPrepared (Read-GdkPin) }
