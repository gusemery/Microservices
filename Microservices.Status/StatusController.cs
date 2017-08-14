using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Configuration;

namespace Microservices.Status
{
    

    namespace uShip.Ping.Api.Controllers
    {
        /// <summary>
        /// Used by health checks, such as Consul, to verify that the web application is running
        /// </summary>
        public class StatusController : ApiController
        {
          
            /// <summary>
            /// Returns an HTTP 200 status code to show API pipeline is working
            /// </summary>
            [HttpGet]
            [StatusRoute()]
            
            public virtual HttpResponseMessage Status()
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
        }
    }

}
