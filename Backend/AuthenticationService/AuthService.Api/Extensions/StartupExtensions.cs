using AuthService.Api.Filters;
using AuthService.Infrastructure;
using AuthService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AuthService.Api.Extensions
{
    public static class StartupExtensions
    {
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
        
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<Services.AuthService>();
            services.AddScoped<ProfileService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<ApplicationExceptionFilter>();
        }

        public static void ConfigureDb(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration.GetValue("ConnectionString", string.Empty);

            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly("AuthService.Api")));

            var context = services.BuildServiceProvider().GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}