using System.ComponentModel;
using System.IO;
using CorrelationId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Api.Extensions;
using OrderService.Api.Filters;
using Serilog;
using Serilog.Core;

namespace OrderService.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional:true)
                .Build();
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Http(configuration["ElasticSearch:Url"])
                .CreateLogger();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            ConfigureExtensions(services);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                        builder.AllowCredentials();
                    });
            });
            services.AddMvc(options =>
            {
                options.Filters.Add<ApplicationExceptionFilter>();
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            });
            services.AddCorrelationId();

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            StartupExtensions.ConfigureSwagger(app);
        
            app.UseCorrelationId(new CorrelationIdOptions {UseGuidForCorrelationId = true});
            app.UseMvc();
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseAuthentication();
        }
        
        #region Helpers
        

        private static void ConfigureExtensions(IServiceCollection services)
        {
            StartupExtensions.AddServices(services);
            StartupExtensions.ConfigureDb(services);
            StartupExtensions.AddSwagger(services);
            StartupExtensions.ConfigureEventBus(services);
            StartupExtensions.ConfigureMediator(services);
            StartupExtensions.ConfigureAuthentication(services);
        }

        #endregion
    }
}