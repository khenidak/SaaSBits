using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Saas.Gateway.HTTPModule
{ 
    public class GatewayModule : IHttpModule
    { 
        public void Init(HttpApplication ctx)
        {
            IGatewayHook AuthHook = new GatewayAuthHook();
            AuthHook.Enagage(ctx);
        }
        public void Dispose()
        {
             
        }       
    }
}
