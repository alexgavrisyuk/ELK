using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using OrderService.Api.Options;

namespace OrderService.Api.Filters
{
    public class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
    {
        public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
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
                return await Task.FromResult(AuthenticateResult.Fail("Token is invalid"));
            }

            var identities = new List<ClaimsIdentity> {new ClaimsIdentity(token.Claims)};
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.AuthScheme);

            return AuthenticateResult.Success(ticket);
        }
    }
}