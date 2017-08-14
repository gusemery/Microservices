using Microservices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Microservices
{
    static class Program
    {
        private static int? _overridePort;
        //private static readonly Lazy<int> LazyPort = new Lazy<int>(PortGenerator.FreeTcpPort);
        //private static readonly ServiceNameBuilder _serviceNameBuilder = new ServiceNameBuilder();
        private static readonly string _uri = "http://*";
        private static string _color = "blue";

        /// <summary>
        /// The port on which the web server will listen
        /// </summary>
        private static int Port
        {
            get { return _overridePort ?? 5000; }    //LazyPort.Value; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            
        }

        private static TopshelfExitCode StartWindowsService()
        {
            return HostFactory.Run(x =>
           {
               x.AfterInstall(ai =>
              {
                  //This runs after the install.  This code is only for restarting a service.
              });
               x.Service<IService>(s =>
              {
                  s.ConstructUsing(CreateHttpServer);
                  s.WhenStarted(service => service.OnStart());

                  s.WhenStopped(service => service.OnStop());

                  s.WhenShutdown(service => service.OnStop());
              });

               var serviceName = "uShip.Ping.Api";  //_serviceNameBuilder.Build("uShip.Ping.Api", shouldIncrement, _color);

               x.EnableShutdown();
               x.RunAsLocalSystem();
               x.StartAutomatically();
               //Restart on crash, unlimited retry (since SetResetPeriod defaults to 0, so doesn't keep a count)... TODO monitor this somehow better
               x.EnableServiceRecovery(s => s.RestartService(0));
               x.SetServiceName(serviceName);
               x.SetDisplayName(serviceName);
               //TODO: PING - change this to reflect the purpose of the microservice web service API
               x.SetDescription("This is the description of the service.");
           });
        }
        
        private static HttpServer CreateHttpServer()
        {
            var format = string.Format("Started at url: {0} port: {1}", _uri, Port);
            var serviceName = "uShip-Ping-Api";
            Console.WriteLine(format);
            return new HttpServer(
                serviceName,
                _uri,
                Port,
                //TODO: PING - Replace ping with your microservice name
                "/ping/status",
                _color,
                string.Format("port{0}", Port));
        }

    }
}
