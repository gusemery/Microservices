using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Microservices.Controllers
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
        public virtual HttpResponseMessage Status()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
