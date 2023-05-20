using System;
using BaseAPI.Core.Interfaces.Service;

namespace BaseAPI.API.Startup
{
	public class JwtMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;
        private readonly IJWTService _jwtService;

        public JwtMiddleware(RequestDelegate next, IUserService userService, IJWTService jwtService)
        {
            _next = next;
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtService.ValidateToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = await _userService.GetByIdAsync(userId.Value);
            }

            await _next(context);
        }
    }
}

