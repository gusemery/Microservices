using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Configuration;

namespace Microservices.Status
{
    public class StatusRouteAttribute : Attribute, IDirectRouteFactory, IHttpRouteInfoProvider
    {
        public StatusRouteAttribute()
        {
            var route = ConfigurationManager.AppSettings["StatusApiRoute"];
            Template = route ?? "api/Status";

        }
        public StatusRouteAttribute(string template)
        {
            Template = template;
        }

        public StatusRouteAttribute(string name, string template)
        {
            Name = name;
            Template = template;        
        }

        public string Name
        {
            get; set;
        }

        public string Template
        {
            get; set;
        }

        private int order = 0;
        public int Order
        {
            get { return order; } set { order = value; }
        }

        public RouteEntry CreateRoute(DirectRouteFactoryContext context)
        {
            var builder = context.CreateBuilder(Template);
            builder.Name = Name;
            builder.Order = Order;
            var result = builder.Build();
            return result;
            //return new RouteEntry(Template, new HttpRoute(Template));

        }
    }
}
