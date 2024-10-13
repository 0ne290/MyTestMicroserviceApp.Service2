using Application.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Web.gRpcServices;
using Web.Middlewares;

namespace Web;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            //.MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                .WithDefaultDestructurers()
                .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }))
            
            .WriteTo.Async(c => c.Console())
            
            .CreateLogger();
        
        try
        {
            Log.Information("Starting host build");
            
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices(builder.Configuration.GetConnectionString("Sqlite") ?? throw new Exception("Connection string not found in config file."));
            builder.Services.AddSerilog();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews().AddNewtonsoftJson();
            builder.Services.AddGrpc();
        
            var app = builder.Build();
            
            app.UseSerilogRequestLogging();
            app.UseMiddleware<ExceptionLoggingMiddleware>();
            
            app.UseHsts();
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseRouting();
            //app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}");
            
            app.MapGrpcService<Service2>();
            
            Log.Information("Success to build host. Starting web application");
            
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Failed to build host");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}