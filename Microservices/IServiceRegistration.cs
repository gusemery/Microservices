using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices
{
    public interface IServiceRegistration
    {
        bool RegisterService();
        bool DeregisterService();
    }
}
