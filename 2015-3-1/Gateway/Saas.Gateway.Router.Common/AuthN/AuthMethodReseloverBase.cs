using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Gateway.Router.Common
{
    public abstract class GatewayAuthMethodReseloverBase
    {
     
        public GatewayAuthMethodReseloverBase()
        {
            

        }

        protected string GetBearerToken(string authHeader)
        {
            if (null == authHeader) return string.Empty;

            // The header is of the form "bearer <accesstoken>", so extract to the right of the whitespace to find the access token.
            // check sample: WebAPI-ManuallyValidateJwt-DotNet of Azure A/D Samples on github
            int startIndex = authHeader.LastIndexOf(' ');
            if (startIndex > 0)
                return authHeader.Substring(startIndex).Trim();


            return string.Empty;


        }

        public abstract GatewayAuthenticatorBase Resolve();
    }
}
