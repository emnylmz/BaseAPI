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
	public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTService _jwtService;

        public AuthenticationService(IJWTService jwtService)
        {
            _jwtService = jwtService;
        }

        public string Login(User user)
        {
            return _jwtService.GenerateToken(user);
        }
    }
}

