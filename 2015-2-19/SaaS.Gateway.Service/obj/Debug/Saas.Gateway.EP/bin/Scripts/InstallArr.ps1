
$dwPath = "C:\Resources\Download"
$logfile = "C:\Resources\Download\log"

mkdir "C:\Resources\Download"


$wclient =  new-object net.webclient
$wclient.DownloadFile('http://download.microsoft.com//download/D/4/4/D446D154-2232-49A1-9D64-F5A9429913A4/WebDeploy_amd64_en-US.msi',"$dwPath\WebDeploy3.msi")
$wclient.DownloadFile('http://download.microsoft.com/download/B/0/0/B00FEF21-79DE-48B0-8731-F9CFE70CE613/WebPlatformInstaller_3_10_amd64_en-US.msi',"$dwPath\WebPI3.msi")
$wclient.DownloadFile('http://download.microsoft.com/download/8/9/B/89B754A5-56F7-45BD-B074-8974FD2039AF/WebDeploy_2_10_amd64_en-US.msi',"$dwPath\WebDeploy_21.msi")
$wclient.DownloadFile('http://download.microsoft.com/download/F/E/2/FE2E2E07-22B5-4875-9A36-8B778D157F91/WebFarm2_x64.msi',"$dwPath\WebFarm22.msi")
$wclient.DownloadFile('http://download.microsoft.com/download/C/F/F/CFF3A0B8-99D4-41A2-AE1A-496C08BEB904/WebPlatformInstaller_amd64_en-US.msi',"$dwPath\WebPI5.msi")
$wclient.DownloadFile('http://download.microsoft.com/download/3/4/1/3415F3F9-5698-44FE-A072-D4AF09728390/ExternalDiskCache_amd64_en-US.msi',"$dwPath\ExternalDiskCache.msi")
$wclient.DownloadFile('http://download.microsoft.com/download/D/E/9/DE90D9BD-B61C-43F5-8B80-90FDC0B06144/ExternalDiskCachePatch_amd64.msp',"$dwPath\ExternalDiskCachePatch.msp")
$wclient.DownloadFile('http://download.microsoft.com/download/6/7/D/67D80164-7DD0-48AF-86E3-DE7A182D6815/rewrite_2.0_rtw_x64.msi',"$dwPath\rewrite_2.0_rtw_x64.msi")
$wclient.DownloadFile('http://download.microsoft.com/download/6/3/D/63D67918-483E-4507-939D-7F8C077F889E/requestRouter_x64.msi',"$dwPath\requestRouter.msi")



Start-Process -FilePath "msiexec" -ArgumentList "/i $dwPath\WebPI3.msi /qn /log $logfile-WebPI3.log" -Wait -Passthru 
Start-Process -FilePath "msiexec" -ArgumentList "/i $dwPath\WebDeploy_21.msi /qn /log $logfile-WebDeploy21.log" -Wait -Passthru 
Start-Process -FilePath "msiexec" -ArgumentList "/i $dwPath\WebFarm22.msi /qn /log $logfile-WebFarm22.log" -Wait -Passthru
Start-Process -FilePath "msiexec" -ArgumentList "/i $dwPath\WebDeploy3.msi /qn /log $logfile-WebDeploy3.log" -Wait -Passthru
Start-Process -FilePath "msiexec" -ArgumentList "/i $dwPath\WebPI5.msi /qn /log $logfile-WebPI5.log" -Wait -Passthru
Start-Process -FilePath "msiexec" -ArgumentList "/i $dwPath\ExternalDiskCache.msi /qn /log $logfile-ExternalDiskCache.log" -Wait -Passthru
Start-Process -FilePath "msiexec" -ArgumentList "/i $dwPath\ExternalDiskCachePatch.msp /qn /log $logfile-ExternalDiskCachePatch.log" -Wait -Passthru
Start-Process -FilePath "msiexec" -ArgumentList "/i $dwPath\rewrite_2.0_rtw_x64.msi /qn /log $logfile-rewrite.log" -Wait -Passthru
Start-Process -FilePath "msiexec" -ArgumentList "/i $dwPath\requestRouter.msi /qn /log $logfile-requestRouter.log" -Wait -Passthru

