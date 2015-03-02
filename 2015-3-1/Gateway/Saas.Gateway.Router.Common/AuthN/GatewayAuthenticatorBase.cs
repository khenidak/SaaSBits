using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Saas.Gateway.Common;

namespace Saas.Gateway.Router.Common
{

    public abstract class GatewayAuthenticatorBase
    {
        public Uri TargetUri;
        public string BearerToken; 
        public string HttpVerb;
        public string Path;
        public string[] AcceptHeaders;
        public SaaSApp TargetApp;
        public string ContentType;

        public const string TENANT_ID_CLAIM = "http://schemas.microsoft.com/identity/claims/tenantid";
        public const string UPN_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";


        public GatewayAuthenticatorBase()
        {

        } 

        public abstract GatewayAuthResults Authenticate(out string sUPN, out string sTenantID);

        public void validatJWTToken(out string sTenantID, out string sUPN, string sJWTToken)
        {
            sTenantID = string.Empty;
            sUPN = string.Empty;

            JwtTokenValidatorHandler tokenHandler = new JwtTokenValidatorHandler();

            ClaimsPrincipal claims = null;
            Task tokenValTask = Task.Run(async () =>
                {
                   
                        claims = await tokenHandler.Validate(sJWTToken,
                                                            this.TargetApp.WAADAudience);
                   
                   
                }
                );

        

            try
            {
                tokenValTask.Wait();
            }
            catch (AggregateException ae)
            {
                throw new Exception ("failed to validate token: " + ae.Flatten());
            }

         


            if (null == claims)
                throw new Exception("invalid claims were sent");

            var tid_claim = claims.FindFirst(TENANT_ID_CLAIM);
            var upn_claim = claims.FindFirst(UPN_CLAIM);

            if (null == tid_claim)
                throw new Exception("TenanID was not found in claims");

            if (null == upn_claim)
                throw new Exception("email  was not found in claims");

            //extract
            sTenantID = tid_claim.Value;
            sUPN = upn_claim.Value;

            var _sid = sTenantID; //:-( 

            // is one of our tenants?
            if (null == GatewayApp.Current.SaasTenants.FirstOrDefault(t => t.TenantID == _sid))
                throw new Exception("authentication failed, unregistered tenant");

        }
        public string EncryptAuthCookie(GatewayAuthCookie cookie)
        {
            string json = JsonConvert.SerializeObject(cookie);
            string cipheredJson = json; // encrypt here
            return cipheredJson;
        }

        public GatewayAuthCookie DecryptAuthCookie(string cookie)
        {
            string clearTextJson = cookie; // decrypt here;
            var authCookie = JsonConvert.DeserializeObject<GatewayAuthCookie>(clearTextJson);
            return authCookie;
        }
        
    }
}
