using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaaS.Services.HrApp.Startup))]
namespace SaaS.Services.HrApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            
            ConfigureAuth(app);
        }
    }
}
