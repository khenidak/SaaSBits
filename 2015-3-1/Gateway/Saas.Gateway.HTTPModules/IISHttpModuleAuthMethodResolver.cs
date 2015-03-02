using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Saas.Gateway.Common;
using Saas.Gateway.Router.Common;
using System.Web;
using System.IO;

namespace Saas.Gateway.HTTPModule
{
     
    class IISHttpModuleAuthMethodResolver : GatewayAuthMethodReseloverBase
    {
        protected HttpContext _Ctx = null;
        public IISHttpModuleAuthMethodResolver( HttpContext ctx) : 
            base()
        {
            this._Ctx = ctx;
        }
        public override GatewayAuthenticatorBase Resolve()
        {
            // those are common between all Authenticators types
            Uri uri = new System.Uri("https://" + this._Ctx.Request.Headers["Host"]);
            string sAppName = uri.Host.Split('.')[0]; // app name is always <xxx>.domain.com;
            string sBearerToken = GetBearerToken(HttpContext.Current.Request.Headers["Authorization"]);
            string sHttpVerb = _Ctx.Request.RequestType.ToUpper();
            string sPath = _Ctx.Request.Path;
            string[] aAcceptHeaders = _Ctx.Request.AcceptTypes;
            string sContentType = _Ctx.Request.ContentType;
         


            // Which app is this one?
            SaaSApp targetApp = GatewayApp.Current.SaaSApps.SingleOrDefault
                (a => a.Name == sAppName);

            // error case: no apps found
            if (null == targetApp)
                throw new GatewayAuthException("Invalid SaaS App");
            
            // Case 1: Type is Web App
            // anything that touches on System.Web is indirected as func/action pointers. 
            // this to make SaaS.Gateway.Common truely portalbe (i.e doesn't depend on System.Web)
            if (SaaSAppType.WebApp == targetApp.AppType)
                return new WebAppAuthenticator()
                {
                    ContentType = sContentType,
                    TargetUri = uri,
                    BearerToken = sBearerToken,
                    HttpVerb = sHttpVerb,
                    Path = sPath,
                    AcceptHeaders = aAcceptHeaders,
                    TargetApp = targetApp,
                    
                    PostDataReader = ( ) => {

                        var contentlength  = (int) _Ctx.Request.InputStream.Length; // not cool!
                        var bytes = new byte[contentlength];
                        _Ctx.Request.InputStream.Read(bytes, 0, bytes.Length);
                        _Ctx.Request.InputStream.Position = 0;

                        _Ctx.Request.InsertEntityBody(bytes, 0, contentlength);

                        // this is sad, there must be a way to do that faster. 
                        return HttpUtility.ParseQueryString(Encoding.UTF8.GetString(bytes)); ;

                    },
                    AuthCookieValue = (_Ctx.Request.Cookies["GW-Auth"] == null) ?
                    string.Empty : _Ctx.Request.Cookies["GW-Auth"].Value,
                    SetAuthCookie = (sSecureCookie) =>
                        { 
                             //set Auth cookie logic 
                            System.Diagnostics.Debug.WriteLine("Cookie Set!");
                            _Ctx.Response.Cookies.Add(new HttpCookie("GW-Auth", sSecureCookie));
                        }

                };




            throw new GatewayAuthException("Couldn't resolve authenticator");
            
          


        }


       



    }

}
