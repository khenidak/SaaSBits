setlocal
set logfile=%StartupLogs%\StartIIS.log

net start was  >> %logfile% 2>&1
net start w3svc
EXIT /B 0
