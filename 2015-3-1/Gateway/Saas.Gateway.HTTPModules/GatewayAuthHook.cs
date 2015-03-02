using System;
using System.Collections.Generic;
//using System.IdentityModel.Metadata;

using System.Linq;
using System.Security.Cryptography.X509Certificates;
//using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;


using System.Threading;
using Saas.Gateway.Common;
using Saas.Gateway.Router.Common;

namespace Saas.Gateway.HTTPModule
{
    public class GatewayAuthHook: IGatewayHook
    {
        

        public void Enagage(System.Web.HttpApplication context)
        {
          
            context.AuthenticateRequest += (new EventHandler(this.OnAuthenticateRequest));

                
        }
       

        void OnAuthenticateRequest(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("AuthN");
            

            var context = ((HttpApplication)sender).Context;
            
          
            string sUPN = string.Empty;
            string sTID = string.Empty;
            string sGWBearerAuth = string.Empty;

            //those should be set in the router. 
            // the less we do here the better (faster)
            context.Request.Headers.Add("Gatewat-UPN", "");
            context.Request.Headers.Add("Gatewat-TID", "");
            context.Request.Headers.Add("Gatewat-Auth", "");




            // we are using IIS based resolver (depnds explicitly on IIS)
            // we are seprating resolution and execution; so if we ever decided to host the code outside IIS we can easily migrate it. 
            // Apps List will be passed into the resolution method always (no matter what method we use to resolve); 

            // We Expect:
            // allow the user to request or not.
            // UPN Value
            // Tenant ID

             try
             {
                  GatewayAuthenticatorBase _Authenticator  = GatewayAuthenticatorFactory.CreateAuthenticator(
                                                 new IISHttpModuleAuthMethodResolver(context)
                                               );

                 GatewayAuthResults AuthRes = _Authenticator.Authenticate(out sUPN,
                                                                          out sTID);

                 if(GatewayAuthResults.Deny == AuthRes)
                     throw new GatewayAuthException("failed to authenticate request");



                 // set server variables used by the URL router provider 
                 if(GatewayAuthResults.Anonymous != AuthRes)
                 {
                     System.Diagnostics.Debug.WriteLine(string.Format("User {0} from tenant {1} Authenticated", sUPN,sTID));
                     context.Request.Headers.Set("Gatewat-UPN", sUPN);
                     context.Request.Headers.Set("Gatewat-TID", sTID);
                 }
                 // get the GW bearer token
                 // set GW bearer header
                 context.Request.Headers.Set("Gatewat-Auth", GatewayApp.Current.GetBearerToken());
             }

        // all error processing
             // set response to 401 
             // terminate the excution of the HTTP pipeline
             catch (GatewayAuthException GwAuthException)
             {
                 context.Response.StatusCode = 401;

                 // log GwAuthException.Message
                 context.Response.StatusDescription = GwAuthException.Message;
                 context.ApplicationInstance.CompleteRequest();
             }
             catch (Exception exception)
             {
                 // log exception
                 //something is awfully wrong
                 context.ApplicationInstance.CompleteRequest();
                 context.Response.StatusCode = 401;
                 context.Response.StatusDescription = 
                     (exception.Message.Length > 500) ? exception.Message.Substring(0, 500) : exception.Message;
             }
           
        }
       

      
         }

}
