using System;
using System.Collections.Generic;
using Microservices.Models;
using JetBrains.Annotations;

namespace Microservices
{
    /// <summary>
    /// Provides a wrapper implementation for exposing a means of service discovery
    /// </summary>
    internal interface IExternalServiceDiscoveryClient
    {
        /// <summary>
        /// Registers the specified service identifier by calling the client's API; and also creates a health check request expecting the endpoint
        /// /Status to exist upon your web PI
        /// </summary>
        /// <param name="serviceId">The service identifier used as a unique ID for querying this service</param>
        /// <param name="port">The port at which to discover this service</param>
        /// <param name="statusCheckRoute">a route to the HTTP status check</param>
        /// <param name="statusCheckInterval"></param>
        /// <param name="statusCheckTimeout"></param>
        /// <param name="tags">An optional list of tags to assign to the service.</param>
        void RegisterWithStatusCheck(ServiceId serviceId, int port, string statusCheckRoute, TimeSpan statusCheckInterval = new TimeSpan(), TimeSpan statusCheckTimeout = new TimeSpan(), params string[] tags);

        /// <summary>
        /// Deregisters the specified service identifier to no longer be discoverable
        /// </summary>
        /// <param name="serviceId">The service identifier for the service to remove</param>
        void Deregister(ServiceId serviceId);

        /// <summary>
        /// Discovers another service by a unique identifier.
        /// </summary>
        /// <param name="serviceName">The service identifier used to determine to discover the external service's API</param>
        /// <returns>A result from the external service extracted to a representation of data we might need</returns>
        [NotNull]
        ServiceResult DiscoverOne([NotNull]string serviceName);

        /// <summary>
        /// Get all the services with the requested <see cref="serviceName"/>
        /// </summary>
        [NotNull]
        IEnumerable<ServiceResult> DiscoverAll(string serviceName);
    }
}