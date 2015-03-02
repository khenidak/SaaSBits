using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaaS.WebAPI.CTRLS
{

    // this is X controls nothing but a simple get 
    public class xController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(
            HttpStatusCode.OK,
            new { Message = "Hello World" }); // later we will return pod info from here 


        }

    }
}
