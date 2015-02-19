<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="SaaS.Gateway.Service" generation="1" functional="0" release="0" Id="ce3f6532-4446-42c2-b8f9-368ecedd3cec" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="SaaS.Gateway.ServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="Saas.Gateway.EP:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/LB:Saas.Gateway.EP:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/LB:Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/MapCertificate|Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="Saas.Gateway.EPInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/MapSaas.Gateway.EPInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:Saas.Gateway.EP:Endpoint1">
          <toPorts>
            <inPortMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapSaas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapSaas.Gateway.EPInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EPInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="Saas.Gateway.EP" generation="1" functional="0" release="0" software="c:\code\SaaS\SaaS.Gateway.Service\csx\Release\roles\Saas.Gateway.EP" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/SW:Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Saas.Gateway.EP&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;Saas.Gateway.EP&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="StartupLogs" defaultAmount="[10,10,10]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EPInstances" />
            <sCSPolicyUpdateDomainMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EPUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EPFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="Saas.Gateway.EPUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="Saas.Gateway.EPFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="Saas.Gateway.EPInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="d2218b16-c1d3-478a-8223-00fbc9a2288c" ref="Microsoft.RedDog.Contract\ServiceContract\SaaS.Gateway.ServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="826e507c-5ca4-43df-8994-86e61d996e2e" ref="Microsoft.RedDog.Contract\Interface\Saas.Gateway.EP:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="621bdca7-09eb-476d-94c1-1c2ca6c40798" ref="Microsoft.RedDog.Contract\Interface\Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/SaaS.Gateway.Service/SaaS.Gateway.ServiceGroup/Saas.Gateway.EP:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>