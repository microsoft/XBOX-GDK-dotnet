<#
.SYNOPSIS
    Inspects compiled imports and compiles selected header ABI contracts for both architectures.
.DESCRIPTION
    Requires the explicit restored Windows edition root, never the installed environment. No DLL
    is loaded for execution, no probe is linked/run, and no Gaming Runtime/PlayFab API is called.
    This is selected ABI verification, NOT exhaustive ABI or live packaged-title validation.
.PARAMETER WindowsRoot
    Restored Microsoft.GDK.Windows native\<edition> root (containing windows\include and bin).
.PARAMETER AssemblyPath
    Actual compiled GDK.Net.dll to inspect, normally the prepared net10.0 Release output.
.PARAMETER OutputDirectory
    Repository-local diagnostic directory. Do not publish probe sources/objects or SDK payloads.
    Only native-verification.json is a sanitized shareable report.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $WindowsRoot,
    [Parameter(Mandatory)][string] $AssemblyPath,
    [Parameter(Mandatory)][string] $OutputDirectory
)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
. (Join-Path $PSScriptRoot 'gdk.ps1')
$pin = Read-GdkPin
$repo = Split-Path $PSScriptRoot
$WindowsRoot = (Resolve-Path $WindowsRoot).Path
$AssemblyPath = (Resolve-Path $AssemblyPath).Path
$OutputDirectory = [IO.Path]::GetFullPath($OutputDirectory)
if (-not $OutputDirectory.StartsWith("$repo\", [StringComparison]::OrdinalIgnoreCase)) {
    throw 'Native diagnostics must stay inside the repository.'
}
[void](New-Item $OutputDirectory -ItemType Directory -Force)
$reportPath = Join-Path $OutputDirectory 'native-verification.json'
Remove-Item $reportPath -ErrorAction SilentlyContinue
if ((Split-Path $WindowsRoot -Leaf) -cne $pin.edition) { throw 'Windows root does not match the selected edition.' }
$packageRoot = Split-Path (Split-Path $WindowsRoot)
$archive = Join-Path $packageRoot "microsoft.gdk.windows.$($pin.windowsVersion).nupkg"
$stream = [IO.File]::OpenRead($archive)
try { $hash = [Convert]::ToBase64String([Security.Cryptography.SHA512]::HashData($stream)) }
finally { $stream.Dispose() }
if ($hash -cne $pin.windowsSha512) { throw 'Native input archive differs from the exact checked-out pin.' }
$include = Join-Path $WindowsRoot 'windows\include'
$headers = @('XAsync.h', 'XTaskQueue.h', 'XUser.h', 'playfab\core\PFEntity.h',
    'playfab\multiplayer\PFMultiplayer.h', 'playfab\multiplayer\PFLobby.h', 'playfab\party\Party_c.h')
foreach ($header in $headers) {
    if (-not (Test-Path (Join-Path $include $header) -PathType Leaf)) { throw "Missing selected header: $header" }
}
$vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
if (-not (Test-Path $vswhere)) { throw 'MSVC discovery requires installed Visual Studio vswhere.exe.' }
function Find-Vs([switch] $Arm64) {
    $arguments = @('-latest', '-products', '*', '-requires', 'Microsoft.VisualStudio.Component.VC.Tools.x86.x64')
    if ($Arm64) { $arguments += 'Microsoft.VisualStudio.Component.VC.Tools.ARM64' }
    $installation = & $vswhere @arguments -property installationPath
    if ($LASTEXITCODE -ne 0) { throw "vswhere failed ($LASTEXITCODE)." }
    if (-not $installation) { throw "Installed MSVC toolchain missing (ARM64=$Arm64); no install is attempted." }
    return $installation
}
$vs = Find-Vs
$dumpbin = Get-ChildItem (Join-Path $vs 'VC\Tools\MSVC') -Filter dumpbin.exe -Recurse |
    Where-Object FullName -Like '*\Hostx64\x64\dumpbin.exe' |
    Sort-Object FullName -Descending | Select-Object -First 1 -ExpandProperty FullName
if (-not $dumpbin) { throw 'Installed x64-host dumpbin.exe not found.' }

# Reflection inspects metadata/layout only. It does not run initializers or invoke native methods.
$assembly = [Reflection.Assembly]::LoadFrom($AssemblyPath)
Add-Type @'
using System;
using System.Runtime.InteropServices;
public static class NativeContractLayout
{
    public static int Size(Type type) => Marshal.SizeOf(type);
    public static long Offset(Type type, string field) => Marshal.OffsetOf(type, field).ToInt64();
}
'@
$methods = @($assembly.GetTypes() | ForEach-Object {
    $_.GetMethods([Reflection.BindingFlags]'Public,NonPublic,Static,DeclaredOnly')
})
$imports = @($methods | ForEach-Object {
    $attributes = $_.GetCustomAttributes([Runtime.InteropServices.DllImportAttribute], $false)
    if ($attributes.Count) {
        $attribute = $attributes[0]
        [pscustomobject]@{ module = $attribute.Value; entryPoint = $attribute.EntryPoint
            convention = $attribute.CallingConvention.ToString(); method = $_ }
    }
})
if (-not $imports.Count) { throw 'The compiled assembly contains no native imports.' }
$modules = @('xgameruntime.thunks.dll', 'Microsoft.Xbox.Services.C.Thunks.dll',
    'PlayFabCore.dll', 'PlayFabServices.dll', 'PlayFabGameSave.dll',
    'PlayFabMultiplayer.dll', 'Party.dll', 'PartyXboxLive.dll')
$selectedInputs = @($headers | ForEach-Object { "windows\include\$_" })
foreach ($architecture in @('x64', 'arm64')) {
    $selectedInputs += @($modules | ForEach-Object { "windows\bin\$architecture\$_" })
}
$zip = [IO.Compression.ZipFile]::OpenRead($archive)
try {
    foreach ($relative in $selectedInputs) {
        $entry = $zip.GetEntry("native/$($pin.edition)/$($relative.Replace('\', '/'))")
        if (-not $entry) { throw "Selected input is absent from the verified archive: $relative" }
        $content = $entry.Open()
        try { $expectedHash = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($content)) }
        finally { $content.Dispose() }
        if ((Get-FileHash (Join-Path $WindowsRoot $relative) -Algorithm SHA256).Hash -cne $expectedHash) {
            throw "Extracted native input differs from the verified archive: $relative"
        }
    }
} finally { $zip.Dispose() }
foreach ($import in $imports) {
    if ($import.module -cnotin $modules) { throw "Unexpected compiled native module: $($import.module)" }
    if ($import.convention -notin @('Winapi', 'StdCall')) { throw "Unexpected calling convention: $($import.entryPoint)" }
}
$omissions = @('XErrorReport', 'XGameInviteRegisterForPendingEvent', 'XGameInviteUnregisterForPendingEvent',
    'XGameInviteAcceptPendingInvite', 'XNetworkingSetConfigurationSetting', 'XThreadVerifyNotTimeSensitive',
    'XGameStreamingGetAssociatedFrame', 'XGameStreamingSendDebugMessageToClient', 'XGameUiShowManageSpaceAsync',
    'XGameUiShowManageSpaceResult', 'XPackageGetIdentifier', 'XUserGetMsaTokenSilentlyAsync',
    'XUserGetMsaTokenSilentlyResult', 'XUserGetMsaTokenSilentlyResultSize')
foreach ($name in $omissions) {
    if ($imports.entryPoint -ccontains $name) { throw "Intentionally unexported baseline API was bound: $name" }
}

$probe = [Collections.Generic.List[string]]::new()
$probe.Add(@'
#include <windows.h>
#include <cstdint>
#include <cstddef>
#include <type_traits>
#define PFMULTIPLAYER_INCLUDE_SERVER_APIS 1
#include <XAsync.h>
#include <XUser.h>
#include <playfab/multiplayer/PFMultiplayer.h>
#include <playfab/multiplayer/PFLobby.h>
#include <playfab/party/Party_c.h>
static_assert(sizeof(void*) == 8, "Only supported 64-bit architectures");
'@)
$layouts = @('XAsyncBlock', 'XUserLocalId', 'PFLobbyStateChange',
    'PFLobbyJoinLobbyCompletedStateChange', 'PARTY_STATE_CHANGE', 'PARTY_CREATE_NEW_NETWORK_COMPLETED_STATE_CHANGE')
foreach ($name in $layouts) {
    $type = $assembly.GetType("GDK.Net.Interop.$name", $true)
    $size = [NativeContractLayout]::Size($type)
    $probe.Add("static_assert(sizeof($name) == $size, `"$name size`");")
    foreach ($field in $type.GetFields([Reflection.BindingFlags]'Public,NonPublic,Instance')) {
        # Managed XAsyncBlock splits the native opaque internal byte array into four pointers.
        $nativeField = if ($name -eq 'XAsyncBlock' -and $field.Name -like 'Internal*') { 'internal' }
            else { $field.Name.Substring(0, 1).ToLowerInvariant() + $field.Name.Substring(1) }
        $offset = [NativeContractLayout]::Offset($type, $field.Name)
        if ($name -eq 'XAsyncBlock' -and $field.Name -like 'Internal*') {
            $index = [int]$field.Name.Substring(8)
            $probe.Add("static_assert(offsetof($name, $nativeField) + sizeof(void*) * $index == $offset, `"$name.$($field.Name) offset`");")
        } else {
            $probe.Add("static_assert(offsetof($name, $nativeField) == $offset, `"$name.$($field.Name) offset`");")
        }
    }
}
$enums = @('XUserAddOptions', 'XUserPrivilege', 'XUserGamertagComponent',
    'PFLobbyStateChangeType', 'PARTY_STATE_CHANGE_TYPE')
foreach ($name in $enums) {
    $type = $assembly.GetType("GDK.Net.Interop.$name", $true)
    $width = [NativeContractLayout]::Size([Enum]::GetUnderlyingType($type))
    $probe.Add("static_assert(sizeof($name) == $width, `"$name width`");")
    foreach ($member in [Enum]::GetNames($type)) {
        $value = [Convert]::ToUInt64([Enum]::Parse($type, $member))
        $nativeMember = if ($name -eq 'PARTY_STATE_CHANGE_TYPE') {
            $snake = $member -creplace '([A-Z]+)([A-Z][a-z])', '$1_$2' -creplace '([a-z0-9])([A-Z])', '$1_$2'
            "${name}_$($snake.ToUpperInvariant())"
        } else { "$name`::$member" }
        $probe.Add("static_assert(static_cast<uint64_t>($nativeMember) == ${value}ULL, `"$name.$member value`");")
    }
}

# Each mapping ties a checked compiled managed signature to a native header function type. The
# HRESULT/opaque-handle/bool mappings are explicit, not inferred from export coverage or names.
$signatures = @(
    @('XAsyncGetStatus', 'Int32', 'XAsyncBlock*,Byte', 'HRESULT (__stdcall *)(XAsyncBlock*, bool) noexcept'),
    @('XUserAddAsync', 'Int32', 'XUserAddOptions,XAsyncBlock*', 'HRESULT (__stdcall *)(XUserAddOptions, XAsyncBlock*) noexcept'),
    @('XUserGetId', 'Int32', 'IntPtr,UInt64*', 'HRESULT (__stdcall *)(XUserHandle, uint64_t*) noexcept'),
    @('XUserGetLocalId', 'Int32', 'IntPtr,XUserLocalId*', 'HRESULT (__stdcall *)(XUserHandle, XUserLocalId*) noexcept'),
    @('PFMultiplayerStartProcessingLobbyStateChanges', 'Int32', 'IntPtr,UInt32*,PFLobbyStateChange***',
        'HRESULT (__stdcall *)(PFMultiplayerHandle, uint32_t*, const PFLobbyStateChange* const**) noexcept'),
    @('PFMultiplayerFinishProcessingLobbyStateChanges', 'Int32', 'IntPtr,UInt32,PFLobbyStateChange**',
        'HRESULT (__stdcall *)(PFMultiplayerHandle, uint32_t, const PFLobbyStateChange* const*) noexcept'),
    @('PartyStartProcessingStateChanges', 'UInt32', 'IntPtr,UInt32*,PARTY_STATE_CHANGE***',
        'PartyError (__stdcall *)(PARTY_HANDLE, uint32_t*, const PARTY_STATE_CHANGE* const**)'),
    @('PartyFinishProcessingStateChanges', 'UInt32', 'IntPtr,UInt32,PARTY_STATE_CHANGE**',
        'PartyError (__stdcall *)(PARTY_HANDLE, uint32_t, const PARTY_STATE_CHANGE* const*)')
)
foreach ($signature in $signatures) {
    $found = @($imports | Where-Object entryPoint -CEQ $signature[0])
    if ($found.Count -ne 1) { throw "Expected exactly one compiled import: $($signature[0])" }
    $method = $found[0].method
    $parameters = @($method.GetParameters() | ForEach-Object { $_.ParameterType.Name }) -join ','
    if ($method.ReturnType.Name -cne $signature[1] -or $parameters -cne $signature[2]) {
        throw "Compiled signature changed: $($signature[0]) -> $($method.ReturnType.Name) ($parameters)"
    }
    $probe.Add("static_assert(std::is_same_v<decltype(&$($signature[0])), $($signature[3])>, `"$($signature[0]) signature/calling convention`");")
}
$probePath = Join-Path $OutputDirectory 'selected-abi.cpp'
[IO.File]::WriteAllLines($probePath, $probe)
$architectures = @()
foreach ($architecture in @('x64', 'arm64')) {
    $moduleReports = @()
    foreach ($module in $modules) {
        $dll = Join-Path $WindowsRoot "windows\bin\$architecture\$module"
        $bytes = [IO.File]::ReadAllBytes($dll)
        $pe = [BitConverter]::ToInt32($bytes, 0x3c)
        if ([BitConverter]::ToUInt32($bytes, $pe) -ne 0x00004550) { throw "Invalid PE: $module" }
        $machine = [BitConverter]::ToUInt16($bytes, $pe + 4)
        $expected = if ($architecture -eq 'x64') { 0x8664 } else { 0xaa64 }
        if ($machine -ne $expected) { throw "Wrong PE architecture: $architecture/$module" }
        $output = & $dumpbin /nologo /exports $dll 2>&1
        if ($LASTEXITCODE -ne 0) { throw "dumpbin exports failed ($LASTEXITCODE): $architecture/$module`n$($output -join "`n")" }
        $exports = @($output | ForEach-Object {
            if ($_ -match '^\s+\d+\s+[0-9a-fA-F]+\s+[0-9a-fA-F]+\s+(\S+)') { $Matches[1] }
        } | Sort-Object -Unique)
        if (-not $exports.Count) { throw "No exports read: $architecture/$module" }
        $bound = @($imports | Where-Object module -CEQ $module | Select-Object -ExpandProperty entryPoint -Unique)
        $missing = @($bound | Where-Object { $_ -cnotin $exports })
        if ($missing.Count) { throw "Missing compiled imports in $architecture/${module}: $($missing -join ', ')" }
        if ($module -ceq 'xgameruntime.thunks.dll') {
            $changedOmissions = @($omissions | Where-Object { $_ -cin $exports })
            if ($changedOmissions.Count) { throw "Baseline unexported contract changed; investigate: $($changedOmissions -join ', ')" }
        }
        $intentional = @()
        $unverified = @()
        foreach ($name in @($exports | Where-Object { $_ -cnotin $bound })) {
            $reason = if ($name -cmatch '(Get|Set)CustomContext$') { 'Native title context; managed identity maps are used instead.' }
                elseif ($name -cmatch 'ForDebug$' -or $name -cin @('PFInitializeWithLHC', 'PartyCreateLocalUserWithEntityType')) {
                    'No signature in the selected shipped headers; deliberately not bound.'
                } elseif ($name -cin @('XAsyncRun', 'XAsyncGetResultSize')) {
                    'Native async-provider surface deliberately omitted by the managed async abstraction.'
                } else { $null }
            if ($reason) { $intentional += [ordered]@{ entryPoint = $name; reason = $reason } }
            else { $unverified += $name }
        }
        $moduleReports += [ordered]@{ module = $module; sha256 = (Get-FileHash $dll -Algorithm SHA256).Hash.ToLowerInvariant()
            imports = @($bound | Sort-Object); intentionalUnboundExports = $intentional; unverifiedUnboundExports = $unverified }
    }
    $installation = if ($architecture -eq 'arm64') { Find-Vs -Arm64 } else { $vs }
    $vcvars = Join-Path $installation 'VC\Auxiliary\Build\vcvarsall.bat'
    $target = if ($architecture -eq 'arm64') { 'x64_arm64' } else { 'x64' }
    $object = Join-Path $OutputDirectory "selected-abi-$architecture.obj"
    Remove-Item $object -ErrorAction SilentlyContinue
    $command = "set `"PATH=$(Split-Path $vswhere);%PATH%`" && call `"$vcvars`" $target >nul && cl /nologo /std:c++17 /EHsc /W3 /WX /c /I`"$include`" `"$probePath`" /Fo`"$object`""
    $output = & $env:ComSpec /d /c $command 2>&1
    if ($LASTEXITCODE -ne 0) { throw "Native $architecture ABI probe failed ($LASTEXITCODE):`n$($output -join "`n")" }
    if (-not (Test-Path $object)) { throw "MSVC did not produce the $architecture probe object." }
    if ([BitConverter]::ToUInt16([IO.File]::ReadAllBytes($object), 0) -ne $expected) {
        throw "MSVC produced the wrong target machine for the $architecture probe."
    }
    $architectures += [ordered]@{ architecture = $architecture; exports = $moduleReports; selectedAbi = 'compiled-not-executed' }
}
$report = [ordered]@{
    schemaVersion = 1; edition = $pin.edition; windowsVersion = $pin.windowsVersion
    windowsArchiveSha512 = $hash; assemblySha256 = (Get-FileHash $AssemblyPath -Algorithm SHA256).Hash.ToLowerInvariant()
    headers = @($headers | ForEach-Object { [ordered]@{
        header = $_; sha256 = (Get-FileHash (Join-Path $include $_) -Algorithm SHA256).Hash.ToLowerInvariant()
    } })
    selectedLayouts = $layouts; selectedEnums = $enums; selectedSignatures = @($signatures | ForEach-Object { $_[0] })
    preservedUnexported = $omissions; architectures = $architectures
    unverified = @('All signatures/layouts/enums outside the explicit selected lists',
        'Callback lifetime, string/buffer ownership and full transitive field semantics',
        'Exported-but-not-imported functions are not ABI-verified or claimed as projected',
        'Packaged-title runtime behavior, sign-in, sandbox services and live PFMP Lobby behavior',
        'ARM64 managed runtime layout/execution: native ARM64 compilation compares selected 64-bit managed host layouts only')
}
$report | ConvertTo-Json -Depth 15 | Set-Content $reportPath -Encoding utf8
Write-Host "Native import/export verification passed for x64 + arm64; selected ABI probes compiled (never executed). Report: $reportPath"
