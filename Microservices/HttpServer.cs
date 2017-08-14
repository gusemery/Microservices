using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices
{
    public class HttpServer : WindowsServiceBase
    {
        private readonly string _baseUrl;
        private IDisposable _webApp = null;

        /// <summary>
        /// Creates a new <see cref="HttpServer"/>
        /// </summary>
        /// <param name="serviceName">Name with which this server will be registered with Consul</param>
        /// <param name="hostAddress">Host that this server will listen on</param>
        /// <param name="port">Port that this server will listen on</param>
        /// <param name="statusRoute">Route segment to reach the status endpoint for this service</param>
        /// <param name="tags">Tags with which to register this service with Consul</param>
        public HttpServer(string serviceName, string hostAddress, int port, string statusRoute, params string[] tags)
            : base(serviceName, port, statusRoute, tags)
        {
            _baseUrl = string.Format("{0}:{1}", hostAddress, port);
        }

        /// <summary>
        /// Called when the service is starting up
        /// </summary>
        public override void OnStart()
        {
            //_webApp = WebApp.Start<StartOwin>(url: _baseUrl);
            base.OnStart();
        }

        /// <summary>
        /// Called when the service is shutting down
        /// </summary>
        public override void OnStop()
        {
            if (_webApp != null) _webApp.Dispose();
            base.OnStop();
        }
    }
}