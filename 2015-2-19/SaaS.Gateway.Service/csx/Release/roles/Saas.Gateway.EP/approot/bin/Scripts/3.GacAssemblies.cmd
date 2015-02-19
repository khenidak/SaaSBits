setlocal
set logfile=%StartupLogs%\GacAssemblies.log

gacutil /nologo /i .\Saas.Gateway.UrlRouter.dll >> %logfile% 2>&1
exit /b 0

