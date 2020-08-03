using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TMDb.WebAPI.Controllers
{
    public class TokenController : ApiController
    {
        private const string SecretKey = "my_secret_key_12345";
        public static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        public string GenerateToken(Guid accountID, string role)
        {
            var token = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim("guid", accountID.ToString()),
                    new Claim(ClaimTypes.Role, role)

                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
