<#
.SYNOPSIS
    Generates production PlayFab/Party sources from explicit restored Windows SDK inputs.
.DESCRIPTION
    No installed SDK or checked-in model/export fixture is used. All six shipping modules are
    inspected afresh. A failed subprocess removes both partial and previously published output.
    OutputRoot is an isolated generation directory, never the repository's source directory.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $IncludeRoot,
    [Parameter(Mandatory)][string] $BinRoot,
    [Parameter(Mandatory)][ValidatePattern('^\d{6}$')][string] $Edition,
    [Parameter(Mandatory)][string] $OutputRoot,
    [string] $Python = 'python',
    [string] $Dumpbin
)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$repo = Split-Path $PSScriptRoot
$OutputRoot = [IO.Path]::GetFullPath($OutputRoot)
$artifactRoot = [IO.Path]::GetFullPath((Join-Path $repo 'artifacts')) + '\'
if (-not $OutputRoot.StartsWith($artifactRoot, [StringComparison]::OrdinalIgnoreCase)) {
    throw 'OutputRoot must be an isolated directory under artifacts, never production source.'
}
$stage = "$OutputRoot.pending-$([guid]::NewGuid().ToString('N'))"
try {
    if (Test-Path $OutputRoot) { Remove-Item $OutputRoot -Recurse -Force }
    New-Item $stage -ItemType Directory -Force | Out-Null
    if (-not (Test-Path (Join-Path $IncludeRoot 'playfab\core\PFErrors.h'))) {
        throw "Missing Windows PlayFab headers at '$IncludeRoot'. Restore the pinned SDK first."
    }
    if (-not $Dumpbin) {
        $Dumpbin = Get-Command dumpbin.exe -ErrorAction SilentlyContinue | Select-Object -First 1 -ExpandProperty Source
        if (-not $Dumpbin) {
            $Dumpbin = Get-ChildItem -Path "${env:ProgramFiles}\Microsoft Visual Studio", "${env:ProgramFiles(x86)}\Microsoft Visual Studio" `
                -Filter dumpbin.exe -Recurse -ErrorAction SilentlyContinue |
                Where-Object FullName -Like '*HostX64\x64*' | Select-Object -First 1 -ExpandProperty FullName
        }
    }
    if (-not $Dumpbin) { throw 'dumpbin.exe is required; install the Visual Studio C++ build tools.' }
    $exports = @{}
    foreach ($name in 'PlayFabCore.dll', 'PlayFabServices.dll', 'PlayFabGameSave.dll',
        'PlayFabMultiplayer.dll', 'Party.dll', 'PartyXboxLive.dll') {
        $module = Join-Path $BinRoot $name
        if (-not (Test-Path $module -PathType Leaf)) { throw "Missing required export input '$module'." }
        $lines = & $Dumpbin /nologo /exports $module
        if ($LASTEXITCODE -ne 0) { throw "dumpbin failed for $name (exit $LASTEXITCODE)." }
        $found = 0
        foreach ($line in $lines) {
            if ($line -match '^\s+\d+\s+[0-9A-F]+\s+[0-9A-F]{8}\s+(\S+)') {
                $exports[$Matches[1]] = $name
                $found++
            }
        }
        if (-not $found) { throw "No exports found in '$module'." }
    }
    $sorted = [ordered]@{}
    foreach ($name in ($exports.Keys | Sort-Object)) { $sorted[$name] = $exports[$name] }
    $exportsPath = Join-Path $stage 'exports.json'
    $modelPath = Join-Path $stage 'model.json'
    $sourceRoot = Join-Path $stage 'src'
    $sorted | ConvertTo-Json | Set-Content $exportsPath -Encoding utf8
    & $Python (Join-Path $PSScriptRoot 'playfab\parse_headers.py') --include-root $IncludeRoot --edition $Edition --out $modelPath
    if ($LASTEXITCODE -ne 0) { throw "Header parsing failed (exit $LASTEXITCODE)." }
    foreach ($emitter in 'emit_native.py', 'emit_services.py', 'emit_errors.py', 'emit_party.py') {
        & $Python (Join-Path $PSScriptRoot "playfab\$emitter") --include-root $IncludeRoot `
            --edition $Edition --model $modelPath --exports $exportsPath --output-root $sourceRoot
        if ($LASTEXITCODE -ne 0) { throw "$emitter failed (exit $LASTEXITCODE)." }
    }
    if (-not (Get-ChildItem $sourceRoot -Filter '*.cs' -Recurse -File)) { throw 'Generation produced no C# sources.' }
    Move-Item $stage $OutputRoot
    Write-Host "Generated Windows SDK $Edition sources: $OutputRoot"
}
catch {
    if (Test-Path $OutputRoot) { Remove-Item $OutputRoot -Recurse -Force }
    throw
}
finally {
    if (Test-Path $stage) { Remove-Item $stage -Recurse -Force }
}
