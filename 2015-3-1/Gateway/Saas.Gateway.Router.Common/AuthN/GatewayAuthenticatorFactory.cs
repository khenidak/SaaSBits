using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Gateway.Router.Common
{
    public class GatewayAuthenticatorFactory
    {
        public static GatewayAuthenticatorBase CreateAuthenticator(GatewayAuthMethodReseloverBase resolver)
        {
            return resolver.Resolve();
        }
    }
}
