using System;
using CustomerService.Api.Filters;
using CustomerService.Infrastructure;
using CustomerService.Infrastructure.Repositories;
using CustomerService.Logic.Interfaces;
using CustomerService.MessageQueue;
using CustomerService.MessageQueue.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;

namespace CustomerService.Api.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureEventBus(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            
            var host = configuration.GetValue("RabbitMq:Host", string.Empty);
            if (!int.TryParse(configuration.GetValue("RabbitMq:Port", string.Empty), out var port))
            {
                port = 5672;
            }
            
            services.AddSingleton<MessageListener>(s =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = host,
                    Port = port,
                    UserName = "guest",
                    Password = "guest",
                    VirtualHost = "/",
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(15)
                };
             
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
   
                return new MessageListener(connection, channel);
            });
            
            var listener = services.BuildServiceProvider().GetService<MessageListener>();

            try
            {
                listener.Subscribe(Constants.MessageQueue.BookingExchange);
                listener.Subscribe(Constants.MessageQueue.OrderExchange);    
            }
            catch (Exception e)
            {
                Policy
                    .Handle<Exception>()
                    .RetryForever(ex =>
                    {
                        listener.Subscribe(Constants.MessageQueue.BookingExchange);
                        listener.Subscribe(Constants.MessageQueue.OrderExchange);    
                    });
            }
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
        
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, Logic.Services.CustomerService>();
            services.AddScoped<CustomerRepository>();
            services.AddScoped<ApplicationExceptionFilter>();
        }

        public static void ConfigureDb(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration.GetValue("ConnectionString", string.Empty);
            
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly("CustomerService.Api")));

            var context = services.BuildServiceProvider().GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}