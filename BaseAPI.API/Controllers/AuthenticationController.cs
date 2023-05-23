using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseAPI.Core.DTOs;
using BaseAPI.Core.Interfaces;
using BaseAPI.Core.Model;
using BaseAPI.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace BaseAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthenticationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(
            IMapper mapper,
            IAuthenticationService authenticationService,
            IUserService userService
            )
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userService.CheckPassAsync(loginDto);

            //user gelirse şifre doğru
            if (user == null)
                return CreateActionResult(CustomResponseDto<List<string>>.Fail(200, "Kullanıcı"));

            else
            {
                var token =_authenticationService.Login(user);

                return CreateActionResult(CustomResponseDto<string>.Success(200, token));
            }
        }
    }


}

