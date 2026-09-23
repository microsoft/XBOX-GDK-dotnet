@echo off
setlocal

set GDK=C:\Program Files (x86)\Microsoft GDK\260400\windows
if not exist "%GDK%" (
    echo Microsoft GDK 260400 not found at "%GDK%".
    exit /b 1
)

set "VCVARS="
for %%e in (Enterprise Professional Community BuildTools) do (
    for %%v in (18 2022 2019) do (
        if not defined VCVARS if exist "%ProgramFiles%\Microsoft Visual Studio\%%v\%%e\VC\Auxiliary\Build\vcvars64.bat" set "VCVARS=%ProgramFiles%\Microsoft Visual Studio\%%v\%%e\VC\Auxiliary\Build\vcvars64.bat"
        if not defined VCVARS if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\%%v\%%e\VC\Auxiliary\Build\vcvars64.bat" set "VCVARS=%ProgramFiles(x86)%\Microsoft Visual Studio\%%v\%%e\VC\Auxiliary\Build\vcvars64.bat"
    )
)

if not defined VCVARS (
    echo No Visual Studio C++ toolset found. Set VCVARS to your vcvars64.bat and rerun.
    exit /b 1
)

call "%VCVARS%" >nul || exit /b 1

cd /d "%~dp0"
cl /nologo /EHsc /Zi /W3 /Fe:pfmp-repro.exe main.cpp ^
   /I "%GDK%\include" ^
   /link /SUBSYSTEM:CONSOLE ^
   "%GDK%\lib\x64\PlayFabMultiplayer.lib" ^
   "%GDK%\lib\x64\Party.lib" ^
   "%GDK%\lib\x64\xgameruntime.lib" ^
   advapi32.lib || exit /b 1

rem The native libraries are not on PATH, so stage them next to the exe.
copy /y "%GDK%\bin\x64\*.dll" . >nul

echo Built %~dp0pfmp-repro.exe
