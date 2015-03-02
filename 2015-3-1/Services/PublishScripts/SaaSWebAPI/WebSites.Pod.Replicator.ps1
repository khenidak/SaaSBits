#Requires -Version 3.0
# make sure to install Azure Powershell before running the scrip
# make sure you have setup your accounts follow instructions here http://azure.microsoft.com/en-us/documentation/articles/install-configure-powershell/


<#


#>
[CmdletBinding(HelpUri = 'http://go.microsoft.com/fwlink/?LinkID=391696')]
param
(
    [Parameter(Mandatory = $true)]
    [ValidateScript({Test-Path $_ -PathType Leaf})]
    [String]
    $Configuration,


    [Parameter(Mandatory = $false)] 
    [int]
    $TotalNumberOfPods =1,
  

    [Parameter(Mandatory = $false)]
    [Switch]
    $SendHostMessagesToOutput = $false
)



#shortcut to ensure Azure powershell cmdlets are loaded
$execDirectory = Split-Path -Parent $PSScriptRoot #should always execute in the solution folder

#the following sets the command paramter to C:\ 
& "${env:ProgramFiles(x86)}\Microsoft SDKs\Azure\PowerShell\ServiceManagement\Azure\Services\ShortcutStartup.ps1" | Out-Null

cd $execDirectory #restore path

$CurrentWebSiteCount = 1 
$SubscriptionName = "Khaled (Kal) Hnidk Subscription"
$execDirectory = Split-Path -Parent $PSScriptRoot
$WebDeployPackage = ""




 
function New-WebDeployPackage
{
    
    $BuildLog = "$execDirectory\BuildLog.log"

    Write-Host "Build started.. log will be saved in $BuildLog"

    MSBuild ".\SaaS.WebAPI\Saas.WebAPI.csproj" /T:Package  /p:PackageLocation="$execDirectory\WebPackages\Saas.WebAPI.zip" >> $execDirectory\BuildLog.log
    $CurrentWebDeployPackage =  Join-Path -path $execDirectory -ChildPath "WebPackages\Saas.WebAPI.zip"


    Write-host "Build Completed - Package Location: $CurrentWebDeployPackage"
    $CurrentWebDeployPackage

}

function Test-WebApplication
{
#no op
    #Edit this function to run unit tests on your web application

    #Write a function to run unit tests on your web application, use VSTest.Console.exe. For help, see VSTest.Console Command-Line Reference at http://go.microsoft.com/fwlink/?LinkId=391340
}





# Script main routine
Set-StrictMode -Version 3

Remove-Module AzureWebSitePublishModule -ErrorAction SilentlyContinue

Import-Module ($execDirectory + '\PublishScripts\AzureWebSitePublishModule.psm1') -Scope Local -Verbose:$false

New-Variable -Name VMWebDeployWaitTime -Value 30 -Option Constant -Scope Script 
New-Variable -Name AzureWebAppPublishOutput -Value @() -Scope Global -Force
New-Variable -Name SendHostMessagesToOutput -Value $SendHostMessagesToOutput -Scope Global -Force

try
{
    $originalErrorActionPreference = $Global:ErrorActionPreference
    $originalVerbosePreference = $Global:VerbosePreference
    
    if ($PSBoundParameters['Verbose'])
    {
        $Global:VerbosePreference = 'Continue'
    }
    
    $scriptName = $MyInvocation.MyCommand.Name + ':'
    
    Write-VerboseWithTime ($scriptName + ' Start')
    
    $Global:ErrorActionPreference = 'Stop'
    Write-VerboseWithTime ('{0} $ErrorActionPreference is set to {1}' -f $scriptName, $ErrorActionPreference)
    
    Write-Debug ('{0}: $PSCmdlet.ParameterSetName = {1}' -f $scriptName, $PSCmdlet.ParameterSetName)

    # Save the current subscription. It will be restored to Current status later in the script
    Backup-Subscription -UserSpecifiedSubscription $SubscriptionName
    

    
    
    # Verify that you have the Azure module, Version 0.7.4 or later.
    if (-not (Test-AzureModule))
    {
         throw 'You have an outdated version of Microsoft Azure PowerShell. To install the latest version, go to http://go.microsoft.com/fwlink/?LinkID=320552 .'
    }
    
    if ($SubscriptionName)
    {

        # If you provided a subscription name, verify that the subscription exists in your account.
        if (!(Get-AzureSubscription -SubscriptionName $SubscriptionName))
        {
            throw ("{0}: Cannot find the subscription name $SubscriptionName" -f $scriptName)

        }

        # Set the specified subscription to current.
        Select-AzureSubscription -SubscriptionName $SubscriptionName | Out-Null

        Write-VerboseWithTime ('{0}: Subscription is set to {1}' -f $scriptName, $SubscriptionName)
    }

    

    $Config = Read-ConfigFile $Configuration 




    #Build and package your web application
    $WebDeployPackage = New-WebDeployPackage

    #Run unit tests on your web application
    Test-WebApplication

   Write-Host "Package $WebDeployPackage to $TotalNumberOfPods (Web Sites)"
    do {
    # for the sake of testing we want clean envrionments  
           $currentWebSite = $Config.name + $CurrentWebSiteCount
           Write-Host "Provisioning Web Site: $currentWebSite pod # $CurrentWebSiteCount"
           if (Test-AzureName -Website -Name $currentWebSite )
            {
                Write-Host  "Web Site: $currentWebSite Exist .. deleting" -ForegroundColor Yellow
                Remove-AzureWebsite  $currentWebSite -force -Confirm:$false
                Write-Host  "Web Site: $currentWebSite Removed" -ForegroundColor Yellow
            }

            Write-Host  "Web Site: $currentWebSite Creating.."
            New-AzureWebsite -Name $currentWebSite -Location $Config.location  `
            Write-Host  "Web Site: $currentWebSite created at $($Config.location)"
            


             Publish-AzureWebsiteProject `
            -Name $currentWebSite `
            -Package $WebDeployPackage

            Write-Host  "Web Site: $currentWebSite Package Deployed"

            Write-Host " "


            $CurrentWebSiteCount  = $CurrentWebSiteCount  + 1
    }
    while ($CurrentWebSiteCount -le  $TotalNumberOfPods)


    Exit
  
}
finally
{
    $Global:ErrorActionPreference = $originalErrorActionPreference
    $Global:VerbosePreference = $originalVerbosePreference

    # Restore the original current subscription to Current status
    Restore-Subscription

    Write-Output $Global:AzureWebAppPublishOutput    
    $Global:AzureWebAppPublishOutput = @()
}
