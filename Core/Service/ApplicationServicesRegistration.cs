using Microsoft.Extensions.DependencyInjection;
using ServicesAbstracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            Services.AddScoped<IServiceManager, ServiceManager>();

            return Services;
        }
    }
}
