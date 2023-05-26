using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using BaseAPI.Core.Enums;
using BaseAPI.Core.Interfaces;
using BaseAPI.Core.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace BaseAPI.Service.Services
{
	public class AuthenticationService : IAuthenticationService
    {
        private readonly Settings _settings;

        public AuthenticationService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public string Login(User user)
        {
            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JWTSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(CustomClaimTypes.UserId,user.Id.ToString()),
                //new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(
            issuer: _settings.JWTSettings.Issuer,
                audience: _settings.JWTSettings.Audience,
            claims,
                expires: DateTime.Now.AddMinutes(_settings.JWTSettings.ExpireMinutes),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}

