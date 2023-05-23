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
    
    public class UsersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;

        public UsersController(
            IMapper mapper,
            IConfiguration configuration,
            IPasswordService passwordService,
            IUserService userService
            )
        {
            _userService = userService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        //[HttpGet("GetAll"),Authorize(Roles ="Test")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users.ToList());
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, userDtos));
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var entity = await _userService.AddAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, userDto));
        }


    }


}

