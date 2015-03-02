:: Importing IIS Config (to application host file)
:: all config sections extarcted from ApplicationHost.config as a pre-build event


setlocal
:: use the following for cloud
set logfile=%StartupLogs%\ConfigIIS.log


:: use this for local
::set logfile=c:\temp\ConfigIIS.log

:: the below files extract from the local machine by a pre build event
:: we are importing them into the web role IIS server 
%windir%\system32\inetsrv\appcmd set config -in < scripts\AppHost.Handlers.xml >> %logfile% 2>&1
%windir%\system32\inetsrv\appcmd set config -in < scripts\AppHost.Modules.xml >> %logfile% 2>&1
%windir%\system32\inetsrv\appcmd set config -in < scripts\AppHost.Rewrite.Providers.xml >> %logfile% 2>&1
%windir%\system32\inetsrv\appcmd set config -in < scripts\AppHost.Rewrite.inboundRules.xml >> %logfile% 2>&1
%windir%\system32\inetsrv\appcmd set config -in < scripts\AppHost.Rewrite.outboundRules.xml >> %logfile% 2>&1
%windir%\system32\inetsrv\appcmd set config -in < scripts\AppHost.proxy.xml >> %logfile% 2>&1