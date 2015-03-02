using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>; 

namespace Saas.Gateway.Common
{

   
    public class GWoAuth
    {
        private AppFunc _next = null;
        private GWoAuthOptions _options = null;
        public GWoAuth(AppFunc next, GWoAuthOptions options)
        {
            this._next = next;
            this._options = options;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            // validate GW Auth token here 
            // if validated then execute next 
            // else
            // set response status to 401
            // set a good unauthorized message. 
            // stop executing by return Task.FromResult(0);
            
            // no op just pass it on
            await _next.Invoke(environment);



        }

        


       

    }
}
