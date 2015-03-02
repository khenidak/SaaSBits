using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Gateway.Common
{
    public enum SaaSAppType
    {
        WebApp =1,
        WebAPI =2
    }
   public  class SaaSApp 
    {
        public string DisplayName { get; set; }
        public string Name {get;set;}
        public SaaSAppType AppType {get;set;} 
        public bool Anonymous {get;set;}
        public string SignOnUrl {get;set;}
        public string SignOutUrl{get;set;}
        public string WAADAudience { get; set; }
    }
}
