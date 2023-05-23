using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseAPI.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BaseAPI.Core.Enums;

namespace BaseAPI.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public readonly int UserId;

        public BaseController()
        {
            if (User != null)
            {
                var text=User.FindFirstValue(CustomClaimTypes.UserId);

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

