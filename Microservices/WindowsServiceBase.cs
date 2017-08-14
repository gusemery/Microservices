using Microservices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices
{
    public abstract class WindowsServiceBase : IService
    {
        private readonly ServiceId _serviceId;
        private readonly int _port;
        private readonly string _statusRoute;
        private readonly string[] _tags;
        private IServiceRegistration _registration;

        /// <summary>
        /// This class attaches a Windows Service to Consul.  You muse either override or do 
        /// base.OnStart()/OnStop() to activate the Consul integration!
        /// </summary>
        /// <param name="serviceName">Name of the service starting</param>
        /// <param name="port">TCP Port the service talks upon.</param>
        /// <param name="statusRoute"></param>
        /// <param name="tags">Tags to register in consul</param>
        protected WindowsServiceBase(String serviceName, int port, string statusRoute, string[] tags)
        {
            _serviceId = new ServiceId(serviceName, Guid.NewGuid().ToString());
            _port = port;
            _statusRoute = statusRoute;
            _tags = tags.Where(x => !string.IsNullOrEmpty(x)).ToArray();

        }

        //This function adds the Windows Service from Consul.
        public virtual void OnStart()
        {

        }

        //This function removes the Windows Service from Consul.
        public virtual void OnStop()
        {
            // Purposefully letting Dispose take care of this
            //DeregisterWithConsul();
        }
        public void Dispose()
        {
            Deregister();
        }

        public void Deregister()
        {

        }
    }
}