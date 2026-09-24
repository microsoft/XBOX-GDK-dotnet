<#
.SYNOPSIS
    Reports which native thunk exports this projection binds, and which it does not.

.DESCRIPTION
    The projection binds two native modules:

      xgameruntime.thunks.dll          : the Gaming Runtime (X* APIs)
      Microsoft.Xbox.Services.C.Thunks : Xbox Live Services (Xbl* APIs)

    Both re-export flat __stdcall C entry points. See eng/interop-conventions.md for why
    XGameRuntime.dll itself cannot be bound, and eng/unexported-apis.md for the APIs that are in
    xgameruntime.lib but missing from the thunks DLL.

    It also binds the six PlayFab extension libraries, which export their flat C API directly:

      PlayFabCore.dll / PlayFabServices.dll / PlayFabGameSave.dll
      PlayFabMultiplayer.dll (PFMP lobbies and matchmaking)
      Party.dll / PartyXboxLive.dll

    This script diffs each DLL's export table against the entry-point names that appear anywhere in
    src/GDK.Net/Interop, so a family that has not been wrapped yet shows up as unbound. It is the
    cheapest way to answer "what is left?" and to catch a family that silently regressed.

.PARAMETER Architecture
    x64 (default) or arm64. Both architectures export the same set.

.PARAMETER Module
    Restrict the report to one module. 'PlayFab' selects all six PlayFab libraries; 'All' (default)
    reports everything.

.PARAMETER FailOnUnbound
    Exit non-zero when anything is unbound. Off by default because unbound APIs are expected while
    the surface is still being wrapped.

.EXAMPLE
    pwsh -NoProfile -File eng/api-coverage.ps1

.EXAMPLE
    pwsh -NoProfile -File eng/api-coverage.ps1 -Module XboxLive
#>
[CmdletBinding()]
param(
    [ValidateSet('x64', 'arm64')]
    [string] $Architecture = 'x64',

    [ValidateSet('All', 'GameRuntime', 'XboxLive', 'PlayFab', 'PlayFabCore', 'PlayFabServices',
                 'PlayFabGameSave', 'PlayFabMultiplayer', 'PlayFabParty', 'PlayFabPartyXboxLive')]
    [string] $Module = 'All',

    [switch] $FailOnUnbound
)

$ErrorActionPreference = 'Stop'

$gdkRoot = $env:GameDKCoreLatest
if (-not $gdkRoot) {
    throw 'GameDKCoreLatest is not set. Install the Microsoft GDK, or set it to the edition root (for example "C:\Program Files (x86)\Microsoft GDK\260404\").'
}

$dumpbin = Get-ChildItem -Path 'C:\Program Files\Microsoft Visual Studio' -Filter 'dumpbin.exe' -Recurse -ErrorAction SilentlyContinue |
    Where-Object FullName -Like '*Hostx64\x64*' |
    Select-Object -First 1 -ExpandProperty FullName
if (-not $dumpbin) {
    throw 'Could not locate dumpbin.exe. Install the Visual Studio C++ build tools.'
}

# Always the windows tree: the same stack the P/Invokes bind and eng/packaging redistributes. The
# GRDK\GameKit tree ships no arm64 libraries and a reduced header set, and the standalone PlayFab
# SDKs under GRDK\ExtensionLibraries (*.GDK.dll plus their own Party.dll) are a different, ABI
# incompatible stack.
$modules = [ordered] @{
    GameRuntime          = 'xgameruntime.thunks.dll'
    XboxLive             = 'Microsoft.Xbox.Services.C.Thunks.dll'
    PlayFabCore          = 'PlayFabCore.dll'
    PlayFabServices      = 'PlayFabServices.dll'
    PlayFabGameSave      = 'PlayFabGameSave.dll'
    PlayFabMultiplayer   = 'PlayFabMultiplayer.dll'
    PlayFabParty         = 'Party.dll'
    PlayFabPartyXboxLive = 'PartyXboxLive.dll'
}

$playFabModules = @(
    'PlayFabCore', 'PlayFabServices', 'PlayFabGameSave',
    'PlayFabMultiplayer', 'PlayFabParty', 'PlayFabPartyXboxLive')

# Exports the projection deliberately does not bind, reported separately so they do not read as
# coverage gaps.
#
#   *CustomContext  : a void* title context slot on a native handle. A managed caller has no use
#                     for it (the projection already keys its own identity maps off the handle) and
#                     storing a managed pointer there would outlive any GC guarantee the runtime can
#                     make. .NET callers use an ordinary Dictionary instead.
#   *ForDebug       : internal entry points not declared in any shipped header.
#   PFInitializeWithLHC, PartyCreateLocalUserWithEntityType
#                   : exported but declared in no shipped header of edition 260404, so there is no
#                     signature to bind against. Re-check when a future edition publishes them.
$intentionallyUnbound = @(
    '(Get|Set)CustomContext$',
    'ForDebug$',
    '^PFInitializeWithLHC$',
    '^PartyCreateLocalUserWithEntityType$'
)

$interop = Join-Path $PSScriptRoot '..\src\GDK.Net\Interop'

# Strip comments before looking for bindings. These files name unbound and deliberately-skipped
# symbols in their header comments to explain why they are absent, so matching raw text reports
# those symbols as bound -- the exact opposite of the truth.
$sources = Get-ChildItem -Path $interop -Filter *.cs | ForEach-Object {
    $text = Get-Content -Raw $_.FullName
    $text = [regex]::Replace($text, '/\*.*?\*/', '', 'Singleline')
    $text = [regex]::Replace($text, '(?m)//.*$', '')
    $text
}

# Index every identifier once rather than running one regex per export per file. With ~1,600
# exports across eight modules the pairwise scan takes minutes; the set lookup is instant and
# matches on the same whole-word boundary.
$identifiers = [System.Collections.Generic.HashSet[string]]::new()
foreach ($text in $sources) {
    foreach ($token in [regex]::Matches($text, '[A-Za-z_][A-Za-z0-9_]*')) {
        [void] $identifiers.Add($token.Value)
    }
}

# Symbols the GDK headers mark as deprecated. The projection never binds these: the headers
# document them as no longer required, never invoked, or returning placeholder data, and they have
# no page on the public documentation site. They are therefore expected to be unbound, and reported
# separately so they do not read as coverage gaps. Scanning the headers rather than hardcoding a
# list means a symbol deprecated by a future GDK edition is picked up automatically.
#
# The two header families spell deprecation differently: XSAPI uses the STDAPI_XBL_DEPRECATED macro
# on the declaration itself, while the Gaming Runtime puts __declspec(deprecated("...")) on its own
# line above the declaration. Both are matched below; missing either silently reports that family's
# deprecated symbols as ordinary coverage gaps.
$deprecated = [System.Collections.Generic.HashSet[string]]::new()

$xsapiHeaders = Join-Path $gdkRoot 'windows\include'
if (Test-Path $xsapiHeaders) {
    Get-ChildItem -Path $xsapiHeaders -Recurse -Filter '*.h' |
        Select-String -Pattern '^\s*STDAPI\w*_DEPRECATED\w*\s*(?:\(\s*\w+\s*\)\s*)?(\w+)\s*\(' |
        ForEach-Object { [void] $deprecated.Add($_.Matches[0].Groups[1].Value) }
}

$grdkHeaders = Join-Path $gdkRoot 'GRDK\gameKit\Include'
if (-not (Test-Path $grdkHeaders)) {
    $grdkHeaders = Join-Path $gdkRoot '..\GRDK\gameKit\Include'
}
if (Test-Path $grdkHeaders) {
    foreach ($header in Get-ChildItem -Path $grdkHeaders -Recurse -Filter '*.h') {
        $lines = Get-Content -Path $header.FullName
        for ($i = 0; $i -lt $lines.Count; $i++) {
            if ($lines[$i] -notmatch '__declspec\(deprecated') { continue }

            # The declaration can be several lines below the attribute, past EXTERN_C, the calling
            # convention and the return type.
            for ($j = $i; $j -lt [Math]::Min($i + 6, $lines.Count); $j++) {
                if ($lines[$j] -match '\b(X[A-Za-z0-9]{3,})\s*\(') {
                    [void] $deprecated.Add($Matches[1])
                    break
                }
            }
        }
    }
}

$anyUnbound = $false
$anyDeprecatedBound = $false

foreach ($name in $modules.Keys) {
    if ($Module -eq 'PlayFab') {
        if ($playFabModules -notcontains $name) { continue }
    }
    elseif ($Module -ne 'All' -and $Module -ne $name) { continue }

    $dll = Join-Path $gdkRoot "windows\bin\$Architecture\$($modules[$name])"
    if (-not (Test-Path $dll)) {
        throw "Could not find $dll."
    }

    # No trailing anchor: the Gaming Runtime thunks print "  1 0 00001000 Name" while the XSAPI
    # thunks print "  1 0 00001000 Name = Name". Anchoring to end-of-line silently matches nothing
    # for the latter and reports every export as unbound.
    $exports = & $dumpbin /exports $dll | ForEach-Object {
        if ($_ -match '^\s+\d+\s+[0-9A-F]+\s+[0-9A-F]{8}\s+(\S+)') { $Matches[1] }
    }

    $bound = [System.Collections.Generic.HashSet[string]]::new()
    foreach ($export in $exports) {
        if ($identifiers.Contains($export)) { [void] $bound.Add($export) }
    }

    $skipped = $exports | Where-Object { $deprecated.Contains($_) -and -not $bound.Contains($_) } | Sort-Object
    $violations = $exports | Where-Object { $deprecated.Contains($_) -and $bound.Contains($_) } | Sort-Object
    $remaining = $exports | Where-Object { -not $bound.Contains($_) -and -not $deprecated.Contains($_) }

    $intentional = $remaining | Where-Object { $export = $_; $intentionallyUnbound | Where-Object { $export -match $_ } } | Sort-Object
    $unbound = $remaining | Where-Object { $export = $_; -not ($intentionallyUnbound | Where-Object { $export -match $_ }) } | Sort-Object

    Write-Host ''
    Write-Host "$name ($($modules[$name]), $Architecture)"
    Write-Host "  Exports       : $($exports.Count)"
    Write-Host "  Bound         : $($bound.Count)"
    Write-Host "  Deprecated    : $($skipped.Count) (intentionally unbound)"
    Write-Host "  Not projected : $($intentional.Count) (intentionally unbound)"
    Write-Host "  Unbound       : $($unbound.Count)"

    if ($violations) {
        $anyDeprecatedBound = $true
        Write-Host ''
        Write-Host '  Deprecated exports that are bound and must be removed:'
        $violations | ForEach-Object { Write-Host "    $_" }
    }

    if ($unbound) {
        $anyUnbound = $true
        Write-Host ''
        Write-Host '  Unbound exports:'
        $unbound | ForEach-Object { Write-Host "    $_" }
    }
}

# Binding a deprecated symbol is always an error, not a coverage gap, so it fails the run
# regardless of -FailOnUnbound.
if ($anyDeprecatedBound) {
    exit 1
}

if ($FailOnUnbound -and $anyUnbound) {
    exit 1
}
