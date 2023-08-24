using AppServer.Context.Context;
using AppServer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Context
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Uncomment to use Postgres SQL
            //services.AddDbContext<ApplicationDbContext>(o =>
            //    o.UseNpgsql(
            //                configuration.GetConnectionString("DefaultConnection"),
            //                          b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseSqlServer(
                            configuration.GetConnectionString("sqlDefaultConnection"),
                                      b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        }
    }
}
