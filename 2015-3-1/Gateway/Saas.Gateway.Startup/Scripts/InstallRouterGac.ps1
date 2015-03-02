
 [System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")
 $publish = New-Object System.EnterpriseServices.Internal.Publish
 
 #dependencies 
$publish.GacInstall("Owin.dll")
$publish.GacInstall("Microsoft.Owin.dll")
$publish.GacInstall("Microsoft.Owin.dll")
$publish.GacInstall("System.IdentityModel.Tokens.Jwt.dll")
$publish.GacInstall("Microsoft.IdentityModel.Protocol.Extensions.dll")
$publish.GacInstall("Microsoft.IdentityModel.Clients.ActiveDirectory.dll")

 
 #HTTP Modules & co.
 $publish.GacInstall("Saas.Gateway.Router.Common.dll")
 $publish.GacInstall("Saas.Gateway.Common.dll")
 $publish.GacInstall("Saas.Gateway.HTTPModule.dll")
 #URL Rewrite provider
 $publish.GacInstall("Saas.Gateway.UrlRouter.dll")
 
