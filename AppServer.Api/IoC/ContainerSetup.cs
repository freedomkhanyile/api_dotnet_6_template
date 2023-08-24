using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using AppServer.Core;
using AppServer.Core.Helpers.Constants;
using AppServer.Context;
namespace AppServer.Api.IoC
{
    public static class ContainerSetup
    {
        public static void Setup(
        IServiceCollection services,
        IConfiguration configuration
        )
        {
            AddCors(services);
            services.AddApplication(configuration);
            services.AddPersistence(configuration);
            services.AddConstants(configuration);           
        }

        private static void AddConstants(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("Keys"));

        }
        private static void AddCors(this IServiceCollection services)
        {
            services
                .AddCors(options =>
                {
                    options
                        .AddPolicy("CorsPolicy",
                        builder =>
                           builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());
                });
        }
    }
}
