using System.Linq.Expressions;
using BaseAPI.Core;
using BaseAPI.Core.DTOs;
using BaseAPI.Core.Interfaces.Repository;
using BaseAPI.Core.Interfaces.Service;
using BaseAPI.Core.Interfaces.UnitOfWork;
using BaseAPI.Core.Model;
using BaseAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BaseAPI.Service.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork,
            IPasswordService passwordService,IUserRepository userRepository
            ) : base(repository, unitOfWork)
        {
            _passwordService = passwordService;
            _userRepository = userRepository;
        }

        public override Task<User> AddAsync(User entity)
        {
            entity.Password = _passwordService.EncryptPassword(entity.Password, out string vectorIV);
            entity.LastIV = vectorIV;
            entity.CreatedOn = DateTime.Now;
            //createdBy eklenecek
            return base.AddAsync(entity);
        }

        public async Task<User> CheckPassAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsername(loginDto.Username);

            if (user == null)
                return user;
            else if (!_passwordService.
                CompareEncrytedAndUnencryptedPassword(user.Password, user.LastIV, loginDto.Password))
                return null;
            return user;
        }
    }
}

