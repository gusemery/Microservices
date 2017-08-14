using JetBrains.Annotations;
using System;

namespace Microservices.Models
{
    internal class ServiceResult
    {
        /// <summary>
        /// Constructor for ServiceResult
        /// </summary>
        /// <param name="serviceAddress">Can be Ip (preferred), hostname, or hostname/basepath.  Port will be inserted between these</param>
        /// <param name="port">A valid tcp port</param>
        public ServiceResult(string serviceAddress, int port)
        {
            if (string.IsNullOrWhiteSpace(serviceAddress)) throw new ArgumentException("An empty ip address was specified in the given service result");
            if (port <= 0
                || port > UInt16.MaxValue)
            {
                throw new ArgumentOutOfRangeException("An invalid port was specified in the given service result, given: " + port);
            }

            ServiceAddress = serviceAddress;
            Port = port;
        }

        public string ServiceAddress { get; private set; }
        public int Port { get; private set; }

        /// <summary>
        /// Converts a ServiceResult to a URI based on the result's <see cref="ServiceResult.ServiceAddress"/> and <see cref="ServiceResult.Port"/>
        /// </summary>
        /// <param name="protocol">The protocol for which to use for the resultant URI, default is "http"</param>
        /// <returns>A non-null URI representing the location of the service provided by the <see cref="ServiceResult"/> identifier</returns>
        /// <exception cref="System.ArgumentNullException">serviceResult</exception>
        /// <exception cref="System.ArgumentException">An empty ip address was specified in the given service result</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">An invalid port was specified in the given service result, given:  + serviceResult.Port</exception>
        [NotNull]
        public Uri ToUri(string protocol = "http")
        {
            var parseBasePath = ParseAddressAndBasePath(ServiceAddress);
            var uriBuilder = new UriBuilder(protocol, parseBasePath.Hostname, Port, parseBasePath.BasePath);

            return uriBuilder.Uri;
        }

        private static ParsedAddress ParseAddressAndBasePath(string serviceAddress)
        {
            //Support a base path (vhost for rabbitmq)
            var serviceUri = new Uri("building://" + serviceAddress);

            return new ParsedAddress
            {
                Hostname = serviceUri.Host,
                BasePath = serviceUri.AbsolutePath.Trim('/') + '/'
            };
        }

        private class ParsedAddress
        {
            public string Hostname { get; set; }
            public string BasePath { get; set; }
        }
    }
}