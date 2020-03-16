using System;
using Microsoft.AspNetCore.Authentication;
using OrderService.Api.Filters;
using OrderService.Api.Helpers;
using OrderService.Api.Options;

namespace OrderService.Api.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder,
            Action<CustomAuthOptions> configureOptions, string scheme)
        {
            return builder.AddScheme<CustomAuthOptions, CustomAuthHandler>(scheme,
                configureOptions);
        }
    }
}