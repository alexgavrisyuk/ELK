using System.IO;
using AuthService.Api.Extensions;
using AuthService.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Api
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
                    });
            });
            services.AddMvc(options =>
            {
                options.Filters.Add<ApplicationExceptionFilter>();
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            StartupExtensions.ConfigureSwagger(app);

            app.UseMvc();
            app.UseStaticFiles();
            app.UseCors("AllowAll");
        }
        
        #region Helpers

        private static void ConfigureExtensions(IServiceCollection services)
        {
            StartupExtensions.AddServices(services);
            StartupExtensions.ConfigureDb(services);
            StartupExtensions.AddSwagger(services);
        }

        #endregion
    }
}