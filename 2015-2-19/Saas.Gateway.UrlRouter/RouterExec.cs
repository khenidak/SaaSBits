using Microsoft.Web.Iis.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Gateway.UrlRouter
{
    public class RouterExec : IRewriteProvider, IProviderDescriptor
    {


        string sFirstUrl = @"{your backend server 01 URL}";
        string sSecondUrl = @"your backend server 02 URL";
         
        public void Initialize(IDictionary<string, string> settings, IRewriteContext rewriteContext)
        {
            // no op
        }

        public string getServerForUser(string sUserTag)
        {
            if ("t1" == sUserTag)
                return sFirstUrl;
            else
                return sSecondUrl;
        }
        public string Rewrite(string value)
        {
            System.Diagnostics.Trace.WriteLine("****Begin Rewrite*****");
            string[] arInput = value.Split(new string[] {"[***]"}, StringSplitOptions.None);
             

            string sUserTag = arInput[0];
            string sPath = arInput[1];

            System.Diagnostics.Trace.WriteLine("Got- Path:" + sPath);
            System.Diagnostics.Trace.WriteLine("Got- Tag:" + sUserTag);

            string sBaseUrl = getServerForUser(sUserTag);

            System.Diagnostics.Trace.WriteLine("Write:" + sBaseUrl + sPath);

            System.Diagnostics.Trace.WriteLine("****End Rewrite*****");
                return  sBaseUrl + sPath;
             
        }


        public IEnumerable<SettingDescriptor> GetSettings()
        {
            return null;
        }

    }
}
