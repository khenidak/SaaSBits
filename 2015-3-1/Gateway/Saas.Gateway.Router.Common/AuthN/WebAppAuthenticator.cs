using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Gateway.Router.Common
{
    public class WebAppAuthenticator : GatewayAuthenticatorBase
    {
        public string AuthCookieValue;
        public Func<NameValueCollection> PostDataReader;
        public Action<string> SetAuthCookie;

        public override GatewayAuthResults Authenticate(out string sUPN, out string sTenantID)
        {
            GatewayAuthResults _res = GatewayAuthResults.Deny;
            sUPN = string.Empty;
            sTenantID = string.Empty;
            GatewayAuthCookie cookie = null;
            
            // in web app case we don't really care if it allow anonymous or not. 
            // the user might be calling non secure area (such as login page).

            // Case 1: is pre authenticated? 
            // and is not trying authN again.
            // this to allow users to have multiple uid/pwds
            if ( string.Empty != AuthCookieValue.Trim()  &&
                this.Path.ToLower() != this.TargetApp.SignOnUrl.ToLower())
            {
                // there is a cookie?
                cookie = DecryptAuthCookie(AuthCookieValue);
                
                sUPN = cookie.UPN;   
                sTenantID = cookie.TID;
                System.Diagnostics.Debug.WriteLine(string.Format("cookie was found for user {0} from tenant {1}",sUPN,sTenantID));

                // case 1 w/ a twist
                // if this is the sign out call remove the gw cookie 
                //(i.e subsequent visits - after current - will be routed as anounymous)   
                if (this.Path.ToLower() ==
                    this.TargetApp.SignOutUrl.ToLower())
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Cookie removed: user {0} from tenant {1}", sUPN, sTenantID));
                    SetAuthCookie(string.Empty);
                }

                

                _res = GatewayAuthResults.Allow;
                return _res;
            }

            // Case 2: user has no cookie nor it is not targeting the signon url
            // the request might be to the login page or to the public area
            if (this.Path.ToLower() != this.TargetApp.SignOnUrl.ToLower())
            {
                _res = GatewayAuthResults.Anonymous;
                return _res;
            }

            // another check: if this not a post 
            // it is ours
            if (this.ContentType != "application/x-www-form-urlencoded"
                || this.HttpVerb != "POST")
            {
                // not our post  or form
                _res = GatewayAuthResults.Anonymous;
                return _res;
            }

            // at shis stage we have to read request body (costly operation)
            var PostCollection = PostDataReader();


            if (null == PostCollection["id_token"])
            { 
                // doesn't contain token. leave as is
                _res = GatewayAuthResults.Anonymous;
                return _res;
            }

            // validation method throws exceptions when token is not validated
            validatJWTToken(out sTenantID, out sUPN, PostCollection["id_token"]);

            // set cookie 
            SetAuthCookie(EncryptAuthCookie(new GatewayAuthCookie() 
                        {
                            TID = sTenantID,
                            UPN = sUPN
                        }
                )
              );
            _res = GatewayAuthResults.Allow;
            return _res;
        }
    }
}
