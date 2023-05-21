using System;
using System.Linq.Expressions;
using BaseAPI.Core.Interfaces;
using BaseAPI.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseAPI.Data
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        public UserRepository(AppDbContext context) : base(context)
        {
            _dbSet = context.Set<User>();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}

