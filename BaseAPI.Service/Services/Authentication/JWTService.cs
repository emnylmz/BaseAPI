using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaseAPI.Core.Interfaces;
using BaseAPI.Core.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BaseAPI.Service.Services
{
	public class JWTService:IJWTService
	{
        private readonly Settings _settings;

        public JWTService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JWTSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim("fullName", user.Firstname + " " + user.Lastname),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Aud, _settings.JWTSettings.Audience),
            new Claim(JwtRegisteredClaimNames.Iss, _settings.JWTSettings.Issuer)
        };

            var token = new JwtSecurityToken(
                issuer: _settings.JWTSettings.Issuer,
                audience: _settings.JWTSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMonths(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.JWTSettings.SecretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}

