setlocal
set logfile=%StartupLogs%\InstallArr.log


:: //use the below for local testing
::set logfile=c:\temp\InstallArr.log

:: what is the current folder
echo current folder: %cd% >> %logfile% 2>&1

PowerShell.exe -ExecutionPolicy Unrestricted -Command "& %cd%\scripts\InstallArr.ps1" >> %logfile% 2>&1

exit /b 0
