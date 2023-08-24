using AppServer.Core.Services.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddServices();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<ITokenService, TokenService>();            
        }
    }
}
