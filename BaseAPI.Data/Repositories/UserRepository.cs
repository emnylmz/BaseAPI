using System;
using System.Linq.Expressions;
using BaseAPI.Core.Interfaces.Repository;
using BaseAPI.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseAPI.Data.Repositories
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        public UserRepository(AppDbContext context) : base(context)
        {
            _dbSet = context.Set<User>();
        }

        public User GetByUsername(string username)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username);
        }
    }
}

