using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OrderService.Api.Filters;
using OrderService.Api.Helpers;
using OrderService.Api.Options;
using OrderService.CustomerServiceApi.Interfaces;
using OrderService.CustomerServiceApi.Services;
using OrderService.Domain.AggregatesModels.DishAggregate;
using OrderService.Domain.AggregatesModels.OrderAggregate;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Repositories;
using OrderService.MessageQueue;
using OrderService.Ordering.CommandHandlers;
using OrderService.Ordering.Models;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;

namespace OrderService.Api.Extensions
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
            
            services.AddSingleton<RabbitMqClient>(s =>
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

                var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
                
                return new RabbitMqClient(connection, channel, loggerFactory);
            });
        }

        public static void ConfigureMediator(IServiceCollection services)
        {
            services.AddMediatR(typeof(OrderCommandHandler).Assembly);
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
        
        public static void AddServices(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var url = configuration.GetValue<string>("CustomerServiceApiUrl");
            
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<ApplicationExceptionFilter>();
            services.AddSingleton<ICustomerServiceApiClient, CustomerServiceApiClient>(opt =>
                new CustomerServiceApiClient(url));

            services.AddTransient<IPolicyEvaluator, PolicyEvaluator>();
        }

        public static void ConfigureDb(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration.GetValue("ConnectionString", string.Empty);

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly("OrderService.Api")));

            var context = services.BuildServiceProvider().GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }

        public static void ConfigureMapster(IServiceCollection services)
        {
            
        }
        
        public static void ConfigureAuthentication(IServiceCollection services)
        {
            const string audience = "auth.audience";
            const string issuer = "auth.issuer";
            const string key = "keycvfbghdfhfghfghgfhfghfghgfh";
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = Constants.AuthScheme.Auth;
                    options.DefaultChallengeScheme = Constants.AuthScheme.Auth;
                })
                .AddCustomAuth(options =>
                {
                    options.ValidateIssuer = true;
                    options.ValidateAudience = true;
                    options.ValidateLifetime = true;
                    options.ValidateIssuerSigningKey = true;
                    options.ValidIssuer = issuer;
                    options.ValidAudience = audience;
                    options.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    options.AuthScheme = Constants.AuthScheme.Auth;
                }, Constants.AuthScheme.Auth);
        }
    }
}