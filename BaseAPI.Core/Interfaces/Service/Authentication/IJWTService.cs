using System;
using System.Linq.Expressions;
using BaseAPI.Core.Model;

namespace BaseAPI.Core.Interfaces.Service
{
    public interface IJWTService
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
    }
}

