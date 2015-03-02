using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Saas.Gateway.Common
{
    public class GatewayApp
    {

        
        // in a typical implementation there might be 2 .common libs 
        // one for Gateway and one for backend saas apps 
        //(specially if you out source or aquire the backend applications). 


        // CTOR only for testing
        // SaasApps and SaaS tenants should be driven from shared configuration. 
        public GatewayApp()
        {
            this._SaasApps.Add(new SaaSApp() {
                Name = "hr",
                DisplayName = "HR SaaS App",
                SignOnUrl = "/Account/ExternalLogin",
                WAADAudience = "{// WAAD cliend ID for hr app published multi tenant}",  
                AppType = SaaSAppType.WebApp,
                SignOutUrl = "/Account/LogOfffinal"
            });

            this._SaasTenants.Add(new SaaSTenant()
            {
                TenantID = "60ef658a-1981-42b6-bdae-40f10f3a2f9a", // this microsoft account tenant, use your customer tenants here
                TenantDisplayName = "Microsoft Account"
            });

        }

        //singlton stuff 
        private static GatewayApp _current = new GatewayApp(); 

        public static GatewayApp Current
        {
            get
            {
                return GatewayApp._current;
            }
        }


        // private

        // these will read and updated from a central config
        private List<SaaSApp> _SaasApps = new List<SaaSApp>(); 
        private List<SaaSTenant> _SaasTenants = new List<SaaSTenant>();


        //public 
        public IReadOnlyCollection<SaaSApp> SaaSApps
        {
            get { return this._SaasApps.AsReadOnly(); }
        }

        public IReadOnlyCollection<SaaSTenant> SaasTenants
        {
            get { return this._SaasTenants.AsReadOnly(); }
        }


     




    }
}
