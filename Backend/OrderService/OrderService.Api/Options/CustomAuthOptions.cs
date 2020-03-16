using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace OrderService.Api.Options
{
    public class CustomAuthOptions : AuthenticationSchemeOptions
    {
        public bool ValidateIssuer { get; set; }

        public bool ValidateAudience { get; set; }

        public bool ValidateLifetime { get; set; }
        
        public bool ValidateIssuerSigningKey { get; set; }
        
        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }
        
        public SymmetricSecurityKey IssuerSigningKey { get; set; }
        
        public string AuthScheme { get; set; }
    }
}


/*
 *
 *  ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))

 * 
 */ 