setlocal
set logfile=%StartupLogs%\shutdownIIS.log

net start was  >> %logfile% 2>&1
IISreset /start 
EXIT /B 0
