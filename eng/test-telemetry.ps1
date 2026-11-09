[CmdletBinding()]
param(
    [switch]$Aot,
    [string]$OutputDirectory = (Join-Path $PSScriptRoot '..\artifacts\telemetry')
)

$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $false
$root = Split-Path $PSScriptRoot -Parent
$project = Join-Path $root 'tests\GDK.Net.TelemetryProbe\GDK.Net.TelemetryProbe.csproj'
$output = [IO.Path]::GetFullPath($OutputDirectory)
New-Item -ItemType Directory -Force -Path $output | Out-Null
$provider = '{ab2f0be6-763c-4048-818b-33999458b21c}'
$cases = @('net8.0', 'net10.0')
if ($Aot) { $cases += 'native-aot' }
$results = @()
ConvertTo-Json -InputObject $results | Set-Content (Join-Path $output 'results.json')

foreach ($case in $cases) {
    if ($case -eq 'native-aot') {
        $installer = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer"
        if (Test-Path (Join-Path $installer 'vswhere.exe')) { $env:PATH = "$installer;$env:PATH" }
        $publish = Join-Path $output 'publish'
        & dotnet publish $project -c Release -f net10.0 -r win-x64 --self-contained `
            -p:PublishAot=true -o $publish --verbosity quiet
        $executable = Join-Path $publish 'GDK.Net.TelemetryProbe.exe'
        $arguments = @('--aot')
    }
    else {
        & dotnet build $project -c Release -f $case --verbosity quiet
        $executable = (Get-Command dotnet).Source
        $assembly = Join-Path $root "tests\GDK.Net.TelemetryProbe\bin\Release\$case\GDK.Net.TelemetryProbe.dll"
        $arguments = @("`"$assembly`"", '--jit')
    }
    if ($LASTEXITCODE -ne 0) { throw "$case build/publish failed." }

    $session = 'GdkNetTelemetryProbe-' + [Guid]::NewGuid().ToString('N')
    $etl = Join-Path $output "$case.etl"
    $xmlPath = Join-Path $output "$case.xml"
    $stdout = Join-Path $output "$case.stdout.log"
    $stderr = Join-Path $output "$case.stderr.log"
    & logman create trace $session -p $provider 0x0000800000000000 4 -o $etl -ow -ets
    if ($LASTEXITCODE -ne 0) { throw "Cannot start the $case ETW session. Check local ETW permissions." }
    try {
        $process = Start-Process -FilePath $executable -ArgumentList $arguments -NoNewWindow `
            -RedirectStandardOutput $stdout -RedirectStandardError $stderr -PassThru
        if (-not $process.WaitForExit(60000)) {
            Stop-Process -Id $process.Id
            throw "$case probe timed out."
        }
        if ($process.ExitCode -ne 0) {
            throw "$case probe failed. See $stderr."
        }
        Get-Content $stdout | Write-Host
    }
    finally {
        & logman stop $session -ets
        if ($LASTEXITCODE -ne 0) { throw "Could not stop ETW session $session." }
    }

    & tracerpt $etl -o $xmlPath -of XML -lr -y | Out-Null
    if ($LASTEXITCODE -ne 0) { throw "$case ETW decoding failed." }
    [xml]$trace = Get-Content -Raw $xmlPath
    $namespaces = [Xml.XmlNamespaceManager]::new($trace.NameTable)
    $namespaces.AddNamespace('e', 'http://schemas.microsoft.com/win/2004/08/events/event')
    $events = $trace.SelectNodes(
        "//e:Event[e:System/e:Provider[@Guid='$provider'] and e:System/e:Execution[@ProcessID='$($process.Id)']]",
        $namespaces)
    if ($events.Count -ne 1) { throw "Expected one $case event from the probe; got $($events.Count)." }
    $event = $events[0]
    if ($event.System.Provider.Name -ne 'Microsoft.Gaming.Middleware' -or
        $event.System.Level -ne '4' -or $event.System.Keywords -ne '0x800000000000' -or
        $event.RenderingInfo.Task -ne 'SdkComponentInitialized') {
        throw "$case ETW event identity or classification mismatch."
    }
    $runtimeLine = @(Get-Content $stdout | Where-Object { $_ -like 'RuntimeVersion: *' })
    if ($runtimeLine.Count -ne 1) { throw "$case did not report its runtime version." }
    $expected = [ordered]@{
        PartA_PrivTags = '33554432'; SchemaVersion = '1'; ComponentId = 'GDK.Net'
        ComponentVersion = '1.2.3'; ComponentKind = '2'; InitializationSequence = '1'
        EngineName = ''; EngineVersion = ''; ExecutionRuntimeName = '.NET'
        ExecutionRuntimeVersion = $runtimeLine[0].Substring('RuntimeVersion: '.Length)
        GamingRuntimeVersion = '1.2.3.4'; GdkBuildSdkVersion = ''; DevelopmentContext = '3'
        ConfiguredTitlePackageFamilyName = ''; PackagePublisherId = ''; DeviceType = '4294967295'
        XboxTitleId = '0123ABCD'; PlayFabTitleId = ''
    }
    $fields = @($event.EventData.Data)
    if ($fields.Count -ne $expected.Count) { throw "$case ETW field count mismatch." }
    $index = 0
    $numericFields = @('PartA_PrivTags', 'SchemaVersion', 'ComponentKind',
        'InitializationSequence', 'DevelopmentContext', 'DeviceType')
    foreach ($entry in $expected.GetEnumerator()) {
        $actual = $fields[$index].InnerText
        if ($entry.Key -in $numericFields) { $actual = $actual.Trim() }
        if ($fields[$index].Name -ne $entry.Key -or $actual -cne $entry.Value) {
            throw "$case ETW payload mismatch at field $($entry.Key)."
        }
        $index++
    }
    $results += [pscustomobject]@{ Case = $case; Status = 'Passed'; Fields = $fields.Count }
    ConvertTo-Json -InputObject $results | Set-Content (Join-Path $output 'results.json')
}

$results | Format-Table | Out-Host
