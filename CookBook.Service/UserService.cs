using CookBook.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CookBook.Service
{
    public class UserService : IUserService
    {
        private readonly AuthenticationServiceConfig _authenticationServiceConfig;
        public UserService(AuthenticationServiceConfig authenticationServiceConfig) {
            _authenticationServiceConfig = authenticationServiceConfig;
        }
        public string GenerateJwtForUser(UserViewModel user) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationServiceConfig.JwtSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim("id", user.Id.ToString())
                };

            var token = new JwtSecurityToken(
                issuer: _authenticationServiceConfig.JwtIssuer,
                audience: _authenticationServiceConfig.JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_authenticationServiceConfig.JwtValidForMinutes),
                signingCredentials: creds);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);

            return jwt;
        }
    }
}
