using System;
using System.Linq.Expressions;
using BaseAPI.Core.Interfaces.Repository;
using BaseAPI.Core.Interfaces.Service;
using BaseAPI.Core.Interfaces.UnitOfWork;
using BaseAPI.Core.Model;

namespace BaseAPI.Service.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}

