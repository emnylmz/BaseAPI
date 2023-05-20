using System;
using System.Linq.Expressions;
using BaseAPI.Core.Model;

namespace BaseAPI.Core.Interfaces.Repository
{
	public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetByUsername(string username);
    }
}

