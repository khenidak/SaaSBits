setlocal
set logfile=%StartupLogs%\installARR.log

@powershell "((new-object net.webclient).DownloadFile('http://download.microsoft.com/download/6/3/D/63D67918-483E-4507-939D-7F8C077F889E/requestRouter_x64.msi','startup\requestRouter_x64.msi'))" >> %logfile% 2>&1
msiexec /i startup\requestRouter_x64.msi /qn /log %logfile%

EXIT /B 0