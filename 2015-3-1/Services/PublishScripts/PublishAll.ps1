Write-Host "Publishing SaasWebAPI.."
Invoke-Expression ".\PublishScripts\WebSites.Pod.Replicator.ps1 -Configuration .\publishscripts\Configurations\SaaSWebAPI1-WAWS-dev.json -TotalNumberOfPods 2"