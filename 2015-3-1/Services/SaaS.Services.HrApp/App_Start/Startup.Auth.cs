using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

using Microsoft.Owin.Security.OAuth;
using Owin;
using SaaS.Services.HrApp.Models;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Configuration;
using System.Globalization;
using System.Web.Helpers;
using System.IdentityModel.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using Saas.Gateway.Common;
using System.Linq;

 
namespace SaaS.Services.HrApp
{
    public partial class Startup
    {
       
      

        public void ConfigureAuth(IAppBuilder app)
        {
          
            //fixed address for multitenant apps in the public cloud
            string Authority = "https://login.windows.net/common/";

            // before anything just make sure that GW is the one calling us
            app.UseGWoAuth(new GWoAuthOptions());


            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions { });

            // the redirect back is not to this app but to the gateway itself 
            // the URLs is 2 pieces
            // 1 domain name: (whatever your gateway is listening to)
            // 2 Path: this is the path in as defined SaaSApps collection in GatewayApp.Current field (saas.gateway.common dll)
            
            string sGatewaySignInURL = "http://hr.hnidk.com/account/externallogin";
            string sGatewaySignOutUrl = "http://hr.hnidk.com/account/LoggOffFinal";
            string ClientId = "XXX"; // as configured for the HR app in Azure A/D core tenant
            
            // code is based on WaaD Multi tenanat sample (strongly reommend to check). 
                        app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = ClientId,
                    Authority = Authority,
                    TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                    {
                        // instead of using the default validation (validating against a single issuer value, as we do in line of business apps), 
                        // we inject our own multitenant validation logic
                        ValidateIssuer = false,
                       
                    },
                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        RedirectToIdentityProvider = (context) =>
                        {

                            context.ProtocolMessage.RedirectUri = sGatewaySignInURL;
                            context.ProtocolMessage.PostLogoutRedirectUri = sGatewaySignOutUrl; 
                            return Task.FromResult(0);
                        },
                        // we use this notification for injecting our custom logic
                        SecurityTokenValidated = (context) =>
                        {
                            // retriever caller data from the incoming principal
                            string issuer = context.AuthenticationTicket.Identity.FindFirst("iss").Value;
                            string UPN = context.AuthenticationTicket.Identity.FindFirst(ClaimTypes.Name).Value;
                            string tenantID = context.AuthenticationTicket.Identity.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;

                            if (   null == GatewayApp.Current.SaasTenants.FirstOrDefault(t => t.TenantID == tenantID))
                                // the caller was neither from a trusted issuer or a registered user - throw to block the authentication flow
                                throw new SecurityTokenValidationException("unknown tenant");

                            return Task.FromResult(0);
                        },
                        AuthenticationFailed = (context) =>
                        {
                            context.OwinContext.Response.Redirect("/Home/Error");
                            context.HandleResponse(); // Suppress the exception
                            return Task.FromResult(0);
                        }
                    } 
                });
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

        }
        }


    }
