using Library.Application.Interfaces;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class TokenService : IJwtTokenService
    {
        public ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new List<Claim>
            {
                    new Claim("Username", user.Username),
                    new Claim("Email", user.Email)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "Token");

            return claimsIdentity;
        }
        public string GenerateToken(ClaimsIdentity claimsIdentity, DateTime expiresTime)
        {
            if(expiresTime < DateTime.Now)
            {
                throw new BadHttpRequestException("Not Correct time");
            }
            var jwt = new JwtSecurityToken(
                   issuer: "LibraryServer",
                   audience: "LibraryClient",
                   notBefore: DateTime.UtcNow,
                   claims: claimsIdentity.Claims,
                   expires: expiresTime,
                   signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thHxx1uPmtZYc7LYY1fIbx4t2SPTNf7AeONVQJPNQb0B")), SecurityAlgorithms.HmacSha256)
                   );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
