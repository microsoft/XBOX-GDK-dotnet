function Resolve-GdkTool {
    param([Parameter(Mandatory)][ValidateSet('makepkg', 'wdapp')][string] $Name)
    . (Join-Path $PSScriptRoot '..\gdk.ps1')
    $pin = Read-GdkPin
    $assets = Join-Path $pin.artifactRoot 'obj\GDK.Net\project.assets.json'
    if (-not (Test-Path $assets)) {
        throw 'Restore and prepare the pinned SDK first: pwsh eng\build.ps1 -PrepareOnly.'
    }
    $resolved = Resolve-GdkPackages $pin $assets
    $path = Join-Path $resolved.coreRoot "bin\$Name.exe"
    if (-not (Test-Path $path)) { throw "The exact restored Microsoft.GDK.Core package is missing $Name.exe." }
    return $path
}
