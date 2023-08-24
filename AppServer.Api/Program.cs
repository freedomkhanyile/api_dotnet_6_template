
using Serilog.Formatting.Compact;
using Serilog;
using AppServer.Api.IoC;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Information()
       .WriteTo.Debug(new RenderedCompactJsonFormatter())
       .CreateLogger();

        try
        {
            Log.Information("Start: tkt.wave.core.Api App");
            StartProgramme(args);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static void StartProgramme(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        ContainerSetup.Setup(builder.Services, builder.Configuration);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppServer.Api", Version = "v1.0.0" });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                    },
                    new string[] {}
                }
             });
        });     

        builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.File("app_logs/logs.txt", rollingInterval: RollingInterval.Hour));

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppServer.Api v1.0.0"));
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseStaticFiles();
        // requires using Microsoft.AspNetCore.HttpOverrides;
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseStaticFiles();

        app.MapControllers();

        app.Run();

        app.UseSerilogRequestLogging();
    }
}
 