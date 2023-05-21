using System;
using System.Linq.Expressions;
using BaseAPI.Core.DTOs;
using BaseAPI.Core.Model;

namespace BaseAPI.Core.Interfaces
{
    public interface IUserService:IService<User>
    {
        Task<User> CheckPassAsync(LoginDto loginDto);
    }
}

