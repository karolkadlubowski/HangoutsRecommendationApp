using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.API.Application.Abstractions;
using Identity.API.Application.Providers;
using Identity.API.Domain.Entities;
using Library.Shared.AppConfigs;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Application.Services
{
    public class JwtAuthTokenService : IAuthTokenService
    {
        private readonly JwtConfig _jwtConfig;

        public JwtAuthTokenService(IConfigurationProvider configurationProvider)
        {
            _jwtConfig = configurationProvider.GetConfiguration().JwtConfig;
        }

        public string GenerateAuthenticationToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Email, user.Email),
            };

            return CreateJwtToken(claims);
        }

        private string CreateJwtToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriber = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.TokenExpirationInMinutes),
                SigningCredentials = credentials
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var token = jwtTokenHandler.CreateToken(tokenDescriber);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}