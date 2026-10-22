[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [Parameter(Mandatory = $true)][string]$IncludeRoot,
    [Parameter(Mandatory = $true)][ValidatePattern('^\d{6}$')][string]$Edition,
    [Parameter(Mandatory = $true)][string]$OutputRoot,
    [switch]$NoInstall,
    [switch]$Clean
)

$ErrorActionPreference = 'Stop'

Set-StrictMode -Version 3.0

$ToolPackageId = 'ClangSharpPInvokeGenerator'
$ToolVersion = '21.1.8.4'
$RepoRoot = Resolve-Path (Join-Path $PSScriptRoot '..')
$ToolRoot = Join-Path $RepoRoot '.config\interop'
$ResponseTemplate = Join-Path $PSScriptRoot 'GDK.Net.rsp'
$OutputRoot = [IO.Path]::GetFullPath($OutputRoot)
if (-not $OutputRoot.StartsWith(([IO.Path]::GetFullPath((Join-Path $RepoRoot 'artifacts')) + '\'), [StringComparison]::OrdinalIgnoreCase)) {
    throw 'ClangSharp verification OutputRoot must be under artifacts; it is never a production Compile input.'
}
if ($OutputRoot -match '(?i)\\generated(?:\\|$)') {
    throw 'ClangSharp verification must not write anywhere inside the production generated input tree.'
}
$ObjDir = Join-Path $OutputRoot 'response'
$GeneratedOutput = Join-Path $OutputRoot ("verification.pending-" + [guid]::NewGuid().ToString('N'))
$EffectiveResponse = Join-Path $ObjDir 'GDK.Net.effective.rsp'

function Resolve-RequiredDirectory {
    param(
        [Parameter(Mandatory = $true)][string]$Path,
        [Parameter(Mandatory = $true)][string]$Name
    )

    if (-not (Test-Path -LiteralPath $Path -PathType Container)) {
        throw "$Name was not found at '$Path'."
    }

    return (Resolve-Path -LiteralPath $Path).Path
}

function Resolve-LatestChildDirectory {
    param(
        [Parameter(Mandatory = $true)][string]$Root,
        [Parameter(Mandatory = $true)][string]$Name,
        [string[]]$RequiredChildren = @()
    )

    $rootPath = Resolve-RequiredDirectory -Path $Root -Name $Name
    $candidates = Get-ChildItem -LiteralPath $rootPath -Directory |
        Sort-Object -Property Name -Descending

    foreach ($candidate in $candidates) {
        $missing = $false
        foreach ($child in $RequiredChildren) {
            if (-not (Test-Path -LiteralPath (Join-Path $candidate.FullName $child))) {
                $missing = $true
                break
            }
        }

        if (-not $missing) {
            return $candidate.FullName
        }
    }

    throw "Could not find a usable $Name under '$rootPath'."
}

function Resolve-MsvcIncludeDirectory {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
    if (Test-Path -LiteralPath $vswhere) {
        $installPath = & $vswhere -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath
        if ($LASTEXITCODE -ne 0) { throw "vswhere failed (exit $LASTEXITCODE)." }
        if ($LASTEXITCODE -eq 0 -and -not [string]::IsNullOrWhiteSpace($installPath)) {
            $toolsRoot = Join-Path $installPath.Trim() 'VC\Tools\MSVC'
            $toolsVersion = Resolve-LatestChildDirectory -Root $toolsRoot -Name 'MSVC tools' -RequiredChildren @('include\type_traits')
            return Resolve-RequiredDirectory -Path (Join-Path $toolsVersion 'include') -Name 'MSVC include directory'
        }
    }

    $fallbackRoot = Join-Path $env:ProgramFiles 'Microsoft Visual Studio\18\Enterprise\VC\Tools\MSVC'
    $fallbackVersion = Resolve-LatestChildDirectory -Root $fallbackRoot -Name 'MSVC tools' -RequiredChildren @('include\type_traits')
    return Resolve-RequiredDirectory -Path (Join-Path $fallbackVersion 'include') -Name 'MSVC include directory'
}

$gdkInclude = Resolve-RequiredDirectory -Path $IncludeRoot -Name "GDK $Edition Windows include directory"

$windowsSdkRoot = Resolve-RequiredDirectory -Path (Join-Path ${env:ProgramFiles(x86)} 'Windows Kits\10\Include') -Name 'Windows SDK Include root'
$windowsSdkVersion = Resolve-LatestChildDirectory -Root $windowsSdkRoot -Name 'Windows SDK version' -RequiredChildren @('shared\winerror.h', 'um\Windows.h', 'ucrt\corecrt.h')
$msvcInclude = Resolve-MsvcIncludeDirectory

$tokens = @{
    '{{GDK_INCLUDE}}' = $gdkInclude
    '{{MSVC_INCLUDE}}' = $msvcInclude
    '{{WINDOWS_SDK_SHARED}}' = Resolve-RequiredDirectory -Path (Join-Path $windowsSdkVersion 'shared') -Name 'Windows SDK shared include directory'
    '{{WINDOWS_SDK_UM}}' = Resolve-RequiredDirectory -Path (Join-Path $windowsSdkVersion 'um') -Name 'Windows SDK um include directory'
    '{{WINDOWS_SDK_UCRT}}' = Resolve-RequiredDirectory -Path (Join-Path $windowsSdkVersion 'ucrt') -Name 'Windows SDK ucrt include directory'
    '{{WINDOWS_SDK_WINRT}}' = Resolve-RequiredDirectory -Path (Join-Path $windowsSdkVersion 'winrt') -Name 'Windows SDK winrt include directory'
    '{{WINDOWS_SDK_CPPWINRT}}' = Resolve-RequiredDirectory -Path (Join-Path $windowsSdkVersion 'cppwinrt') -Name 'Windows SDK cppwinrt include directory'
    '{{GENERATED_OUTPUT}}' = $GeneratedOutput
}

Write-Host "GDK include: $gdkInclude"
Write-Host "Windows SDK: $windowsSdkVersion"
Write-Host "MSVC include: $msvcInclude"
Write-Host "Output: $GeneratedOutput"

if (-not $NoInstall) {
    Push-Location $ToolRoot
    try {
        if ($PSCmdlet.ShouldProcess($RepoRoot, "restore/install local .NET tool $ToolPackageId $ToolVersion")) {
            dotnet tool restore
            if ($LASTEXITCODE -ne 0) { throw "dotnet tool restore failed (exit $LASTEXITCODE)." }
            $toolList = dotnet tool list --local
            if ($LASTEXITCODE -ne 0) { throw "dotnet tool list failed (exit $LASTEXITCODE)." }
            if ($toolList -notmatch '(?im)^clangsharppinvokegenerator\s+') {
                dotnet tool install $ToolPackageId --version $ToolVersion
                if ($LASTEXITCODE -ne 0) { throw "ClangSharp tool installation failed (exit $LASTEXITCODE)." }
            }
        }
    }
    finally {
        Pop-Location
    }
}
else {
    Write-Host 'Skipping dotnet tool restore/install because -NoInstall was specified.'
}

$responseText = Get-Content -LiteralPath $ResponseTemplate -Raw
foreach ($entry in $tokens.GetEnumerator()) {
    $responseText = $responseText.Replace($entry.Key, $entry.Value)
}

$commandPreview = "dotnet tool run ClangSharpPInvokeGenerator -- '@$EffectiveResponse'"
Write-Host "Command: $commandPreview"

if ($PSCmdlet.ShouldProcess($GeneratedOutput, 'generate GDK raw interop bindings')) {
    $published = Join-Path $OutputRoot 'verification'
    if (Test-Path $published) { Remove-Item $published -Recurse -Force }
    New-Item -ItemType Directory -Force -Path $ObjDir | Out-Null
    if ($Clean -and (Test-Path -LiteralPath $GeneratedOutput)) {
        Remove-Item -LiteralPath $GeneratedOutput -Recurse -Force
    }

    Set-Content -LiteralPath $EffectiveResponse -Value $responseText -Encoding UTF8

    Push-Location $ToolRoot
    try {
        dotnet tool run ClangSharpPInvokeGenerator -- "@$EffectiveResponse"
        if ($LASTEXITCODE -ne 0) { throw "ClangSharp verification failed (exit $LASTEXITCODE)." }
        if (-not (Test-Path $GeneratedOutput)) { throw 'ClangSharp produced no verification output.' }
        Move-Item $GeneratedOutput $published
    }
    finally {
        Pop-Location
        if (Test-Path $GeneratedOutput) { Remove-Item $GeneratedOutput -Recurse -Force }
    }
}
