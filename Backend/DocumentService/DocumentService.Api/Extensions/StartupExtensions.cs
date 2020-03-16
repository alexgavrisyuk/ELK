using DocumentService.Api.Filters;
using DocumentService.Infrastructure;
using DocumentService.Infrastructure.Interfaces;
using DocumentService.Infrastructure.Repositories;
using DocumentService.Supervisor.CommandsHandlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DocumentService.Api.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Settings>(
                options =>
                {
                    options.ConnectionString = configuration.GetSection("ConnectionString").Value;
                    options.Database = configuration.GetSection("Database").Value;
                });
        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddTransient<DocumentContext>();
            services.AddScoped<ApplicationExceptionFilter>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
        }
        
        public static void ConfigureMediator(IServiceCollection services)
        {
            services.AddMediatR(typeof(DocumentCommandsHandler).Assembly);
            services.AddMediatR(typeof(Startup).Assembly);
        }
        
        public static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        public static void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}