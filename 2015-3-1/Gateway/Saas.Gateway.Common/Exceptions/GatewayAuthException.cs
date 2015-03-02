using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Gateway.Common
{
    public class GatewayAuthException : Exception
    {
        public GatewayAuthException(string error)
            : base(error)
        { }
    }
}
