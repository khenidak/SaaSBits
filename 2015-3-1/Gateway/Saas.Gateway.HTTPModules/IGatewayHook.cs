using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Saas.Gateway.HTTPModule
{
    //as elegantly done by James Baker
    // interfaces and methods renamed for clarity 
    
        public interface IGatewayHook
        {
            void Enagage(HttpApplication context);
        }
    
}
