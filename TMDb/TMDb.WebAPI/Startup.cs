using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TMDb.WebAPI.Controllers;

[assembly: OwinStartup(typeof(TMDb.WebAPI.Startup))]

namespace TMDb.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false, //true,
                        ValidateAudience = false, //true,
                        ValidateIssuerSigningKey = true,
                        //ValidIssuer = "http://mysite.com", //some string, normally web url,  
                        //ValidAudience = "http://mysite.com",
                        IssuerSigningKey = TokenController.SecurityKey,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(60)
                    }
                });
        }
    }
}
