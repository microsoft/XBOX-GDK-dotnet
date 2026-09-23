param([int]$Timeout = 12)

$cases = [ordered]@{
  'A pfmp alone, no gameruntime'        = 'pfmp-init,pfmp-uninit'
  'B pfmp alone, with gameruntime'      = 'gr-init,net-wait,pfmp-init,pfmp-uninit'
  'C pfmp alone, gr-init only'          = 'gr-init,pfmp-init,pfmp-uninit'
  'D party then pfmp uninit'            = 'gr-init,net-wait,pfmp-init,party-init,pfmp-uninit,party-cleanup'
  'E party cleanup before pfmp uninit'  = 'gr-init,net-wait,pfmp-init,party-init,party-cleanup,pfmp-uninit'
  'F party first, then pfmp'            = 'gr-init,net-wait,party-init,pfmp-init,pfmp-uninit,party-cleanup'
  'G gr-uninit BEFORE pfmp-uninit'      = 'gr-init,net-wait,pfmp-init,gr-uninit,pfmp-uninit'
  'H idle pfmp 20s then uninit'         = 'gr-init,net-wait,pfmp-init,sleep:20000,pfmp-uninit'
  'I full clean shutdown'               = 'gr-init,net-wait,pfmp-init,party-init,pfmp-uninit,party-cleanup,gr-uninit'
}

Set-Location $PSScriptRoot
foreach ($name in $cases.Keys) {
    $steps = $cases[$name]
    $log = "log-$($name.Substring(0,1)).txt"
    $p = Start-Process -FilePath .\pfmp-repro.exe `
        -ArgumentList '--steps', $steps, '--timeout', $Timeout `
        -NoNewWindow -Wait -PassThru -RedirectStandardOutput $log -RedirectStandardError "err-$($name.Substring(0,1)).txt"

    $code = $p.ExitCode
    $verdict = switch ($code) {
        0          { 'clean' }
        2          { 'HANG' }
        1          { 'step failed' }
        default    { 'crash 0x{0:X8}' -f $code }
    }
    "{0,-38} {1}" -f $name, $verdict
}
