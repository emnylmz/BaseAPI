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

        public UsersController(IService<User> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var users = await _service.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users.ToList());
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, userDtos));
        }
    }


}

