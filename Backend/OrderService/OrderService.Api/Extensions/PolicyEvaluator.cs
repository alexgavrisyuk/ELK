using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using OrderService.Api.Filters;
using OrderService.Api.Helpers;

namespace OrderService.Api.Extensions
{
    public class PolicyEvaluator : IPolicyEvaluator
    {
        private readonly CustomAuthHandler _handler;

        public PolicyEvaluator(CustomAuthHandler handler)
        {
            _handler = handler;
        }
        
        public async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
            {
                return await Task.FromResult(AuthenticateResult.Fail("Cannot read authorization header."));
            }

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token;
            try
            {
                token = handler.ReadJwtToken(authorization);
            }
            catch (Exception e)
            {
                return AuthenticateResult.Fail("Incorrect token");
                throw;
            }

            var identities = new List<ClaimsIdentity> {new ClaimsIdentity(token.Claims)};
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Constants.AuthScheme.Auth);

            return AuthenticateResult.Success(ticket);
        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context,
            object resource)
        {
            if (!authenticationResult.Succeeded)
            {
                return Task.FromResult(PolicyAuthorizationResult.Forbid());                
            }
            
            return Task.FromResult(PolicyAuthorizationResult.Success());
        }
    }
}