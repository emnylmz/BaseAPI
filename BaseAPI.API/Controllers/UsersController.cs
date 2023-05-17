using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseAPI.Core.DTOs;
using BaseAPI.Core.Interfaces.Service;
using BaseAPI.Core.Model;
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
        private readonly IService<User> _service;
        private readonly IPasswordService _passwordService;

        public UsersController(IService<User> service,
            IMapper mapper,
            IConfiguration configuration,
            IPasswordService passwordService)
        {
            _service = service;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            var users = await _service.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users.ToList());
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, userDtos));
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var entity = await _service.AddAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, userDto));
        }


    }


}

