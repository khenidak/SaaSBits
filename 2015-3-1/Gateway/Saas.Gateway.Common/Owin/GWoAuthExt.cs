using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Gateway.Common
{
    public static class  GWoAuthExt
    {
    public static void UseGWoAuth(
         this IAppBuilder app, GWoAuthOptions options = null)
        {
            options = options ?? new GWoAuthOptions();
            app.Use<GWoAuth>(options);
        }
    }
}
