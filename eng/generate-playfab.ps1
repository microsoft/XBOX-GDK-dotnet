<#
.SYNOPSIS
    Regenerates the PlayFab projection from the installed Microsoft GDK headers.

.DESCRIPTION
    Three steps, all driven from %GameDKCoreLatest%:

      1. dumpbin over the PlayFab redist DLLs -> eng/playfab/exports.json, which records the owning
         module for every native export so the generated P/Invokes name the right library.
      2. parse_headers.py -> eng/playfab/model.json, a JSON model of the PlayFab enums, structs,
         handles and functions.
      3. emit_native.py / emit_services.py / emit_errors.py / emit_party.py -> the generated C#
         under src/GDK.Net/Interop and src/GDK.Net/PlayFab.

    The generated files are checked in, so this script only needs to run when the GDK edition
    changes or a generator is edited.

.PARAMETER SkipExports
    Reuse the existing exports.json instead of running dumpbin. Useful on a machine without the
    Visual Studio C++ tools.
#>
[CmdletBinding()]
param(
    [switch] $SkipExports
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$engPlayFab = Join-Path $PSScriptRoot 'playfab'
$exportsPath = Join-Path $engPlayFab 'exports.json'

if (-not $env:GameDKCoreLatest) {
    throw 'GameDKCoreLatest is not set. Install the Microsoft GDK and open a GDK command prompt.'
}

$gdkRoot = $env:GameDKCoreLatest.TrimEnd('\')

# The GDK ships two PlayFab stacks that are *not* ABI compatible with each other:
#
#   * windows\bin\x64          - the runtime the GDK installs beside XSAPI and xgameruntime. Its
#                                Party.dll links against PlayFabCore.dll and takes a PFEntityHandle,
#                                and it is the stack the windows\include\playfab headers describe.
#   * GRDK\ExtensionLibraries  - the standalone PlayFab SDKs (*.GDK.dll plus a Party.dll that takes
#                                an entity id and token string pair) with their own header copies.
#
# The projection is generated from windows\include\playfab, so it binds - and eng/packaging
# redistributes - the windows\bin stack, matching how the Xbox Live projection binds
# Microsoft.Xbox.Services.C.Thunks.dll from the same folder.
$playFabBin = Join-Path $gdkRoot "windows\bin\x64"

if (-not $SkipExports) {
    $dumpbin = Get-ChildItem -Path "${env:ProgramFiles}\Microsoft Visual Studio", "${env:ProgramFiles(x86)}\Microsoft Visual Studio" `
        -Filter 'dumpbin.exe' -Recurse -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -like '*HostX64\x64*' } |
        Select-Object -First 1

    if (-not $dumpbin) {
        throw 'dumpbin.exe was not found. Re-run with -SkipExports to reuse the checked-in exports.json.'
    }

    # These DLLs are the shipping surface: an export missing here is an API a title could not
    # call even if the header declares it.
    $modules = Get-ChildItem -Path $playFabBin -Include @(
        'PlayFabCore.dll',
        'PlayFabServices.dll',
        'PlayFabGameSave.dll',
        'PlayFabMultiplayer.dll',
        'Party.dll',
        'PartyXboxLive.dll'
    ) -File -Recurse

    $exports = [ordered]@{}
    foreach ($module in $modules) {
        Write-Host "dumpbin $($module.Name)"
        & $dumpbin.FullName /nologo /exports $module.FullName |
            Select-String -Pattern '^\s+\d+\s+[0-9A-F]+\s+[0-9A-F]{8}\s+(\S+)' |
            ForEach-Object { $exports[$_.Matches[0].Groups[1].Value] = $module.Name }
    }

    # Sort by export name so a regeneration produces a byte-identical file: Get-ChildItem's order
    # is filesystem-dependent, which would otherwise churn the diff for no reason.
    $sorted = [ordered]@{}
    foreach ($name in ($exports.Keys | Sort-Object)) {
        $sorted[$name] = $exports[$name]
    }

    $sorted | ConvertTo-Json -Depth 2 | Set-Content -Path $exportsPath -Encoding utf8
    Write-Host "wrote $($sorted.Count) exports to $exportsPath"
}

python (Join-Path $engPlayFab 'parse_headers.py')
python (Join-Path $engPlayFab 'emit_native.py')
python (Join-Path $engPlayFab 'emit_services.py')
python (Join-Path $engPlayFab 'emit_errors.py')
python (Join-Path $engPlayFab 'emit_party.py')
