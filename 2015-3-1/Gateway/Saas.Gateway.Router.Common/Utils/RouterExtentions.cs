using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Saas.Gateway.Common;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
namespace Saas.Gateway.Router.Common
{
    public static  class RouterExtentions
    {
        

        // the gateway authenticates against itself. NOT using multi tennats
        // so configuration below is using normal tenants 
        // Setup:
        // 1- go to your "core" directory and define a gateway app
        // 2- get the resource id and client ids and use them here 
        // 3- define gateway again as web api (a a client) and get the app key and client ids here

        static string authority = "https://login.windows.net/<tenant goes here>"; // the SaaS core tenant (NOT ANY PARTNER OR CLIENT)
        private static string clientId = ""; // SaaS.Gateway Router (as a client) ID
        private static string appKey = "i"; //SaaS.Gateway Router (as a client) key
        private static string SaaSRouterResourceID = ""; // SaaS.Gateway.Router as  Web API Resource ID
        private static ClientCredential clientCredential = new ClientCredential(clientId, appKey);

        private static AuthenticationContext authContext = new AuthenticationContext(authority);
        
        public static string GetBearerToken (this GatewayApp app)
        {
            AuthenticationResult result = null;
                 try
                {
                    // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
                    result = authContext.AcquireToken(SaaSRouterResourceID, clientCredential);
                }

                 catch (AdalException ex)
                 {
                     throw new GatewayAuthException("Error aquiring gateway deamon oAuth token" + ex.Message);
                 }
              return result.AccessToken;
           
            
        }
    }
}
