using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using BaseAPI.Core.Enums;
using BaseAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI.API
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly IJWTService _jwtService;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger, IJWTService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                //burada db log işlemi eklenecek
                //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                //var userId = _jwtService.ValidateToken(token);
                //var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                //string user = jwt.Claims.First(c => c.Type == CustomClaimTypes.UserId).Value;

                // Kullanıcının IP adresi
                var ipAddress = GetIpAddress(context);

                //request header
                var headers = context.Request.Headers;
                var headersString = string.Join("; ", headers.Select(h => $"{h.Key}: {h.Value}"));

                //diğer bilgiler
                var requestMethod = context.Request.Method;
                var requestPath = context.Request.Path;
                var requestQueryString = context.Request.QueryString;

                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problemDetails = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = "Beklenmeyen hata oluştu"
                };

                string json = JsonSerializer.Serialize(problemDetails);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);

            }
        }

        private string GetIpAddress(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;

            // IPv6 döngüsel adresi (localhost) ise IPv4 döngüsel adresi (127.0.0.1) olarak değiştir
            if (ipAddress != null && ipAddress.Equals(IPAddress.IPv6Loopback))
            {
                ipAddress = IPAddress.Loopback;
            }

            // IPv6 adresini IPv4'e dönüştür
            if (ipAddress != null && ipAddress.IsIPv4MappedToIPv6)
            {
                ipAddress = ipAddress.MapToIPv4();
            }

            return ipAddress?.ToString();
        }
    }
}

