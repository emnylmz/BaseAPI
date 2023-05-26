using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using BaseAPI.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using BaseAPI.Core.Enums;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace BaseAPI.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public readonly int UserId;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userId = identity.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserId)?.Value;
                if (userId != null)
                    UserId = Convert.ToInt32(userId);
            }
        }

        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == (int)HttpStatusCode.NoContent)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}

