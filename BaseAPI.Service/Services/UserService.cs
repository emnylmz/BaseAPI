using System.Linq.Expressions;
using BaseAPI.Core;
using BaseAPI.Core.Interfaces.Repository;
using BaseAPI.Core.Interfaces.Service;
using BaseAPI.Core.Interfaces.UnitOfWork;
using BaseAPI.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BaseAPI.Service.Services
{
    public class UserService : BaseService<User>
    {
        private readonly IPasswordService _passwordService;
        public UserService(IGenericRepository<User> userRepository,
            IPasswordService passwordService,
            IUnitOfWork unitOfWork) : base(userRepository, unitOfWork)
        {
            _passwordService = passwordService;
        }

        public override Task<User> AddAsync(User entity)
        {
            entity.Password = _passwordService.EncryptPassword(entity.Password, out string vectorIV);
            entity.LastIV = vectorIV;
            return base.AddAsync(entity);
        }
    }
}

