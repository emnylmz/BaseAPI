using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaseAPI.Core.Interfaces.Authentication;
using BaseAPI.Core.Interfaces.Service;
using BaseAPI.Core.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BaseAPI.Service.Services
{
	public class AuthenticationService : IAuthenticationService
    {
        private readonly UserService _userService;

        public AuthenticationService()
        {

        }

        public string Login()
        {
            throw new NotImplementedException();
        }
    }
}

