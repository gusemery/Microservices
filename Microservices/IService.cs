using System;

namespace Microservices.Common
{
    /// <summary>
    /// A service with hooks for running code when it starts and stops
    /// </summary>
    public interface IService : IDisposable
    {
        /// <summary>
        /// Method to run after the service starts
        /// </summary>
        void OnStart();

        /// <summary>
        /// Method to run before the service stops
        /// </summary>
        void OnStop();
    }
}
