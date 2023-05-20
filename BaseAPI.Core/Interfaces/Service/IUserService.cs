using System;
using System.Linq.Expressions;
using BaseAPI.Core.DTOs;
using BaseAPI.Core.Model;

namespace BaseAPI.Core.Interfaces.Service
{
    public interface IUserService:IService<User>
    {
        bool CheckPassAsync(LoginDto loginDto);
    }
}

