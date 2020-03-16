using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services.Helper
{
    public static class JwtTokenUtil
    {
        private const string ClaimName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        private const string AuthIssuer = "auth.issuer";
        private const string AuthAudience = "auth.audience";

        private const string LoginClaimType = "login.claim";

        private const string Key = "keycvfbghdfhfghfghgfhfghfghgfh";
        
        public static string Generate(string loginClaimValue)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(AuthIssuer, AuthAudience,
                new List<Claim>
                {
                    new Claim(LoginClaimType, loginClaimValue)
                }, 
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddHours(4));

            var handler = new JwtSecurityTokenHandler();

            var strToken = handler.WriteToken(token);

            return strToken;
        }
    }
}