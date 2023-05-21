using System;
using System.Linq.Expressions;
using BaseAPI.Core.Model;

namespace BaseAPI.Core.Interfaces
{
    public interface IJWTService
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
    }
}

