setlocal
set logfile=%StartupLogs%\shutdownIIS.log

net stop was /y >> %logfile% 2>&1

EXIT /B 0
