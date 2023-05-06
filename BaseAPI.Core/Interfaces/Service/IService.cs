using System;
using System.Linq.Expressions;

namespace BaseAPI.Core.Interfaces.Service
{
	public interface IService<T> where T :class
	{
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRange(IEnumerable<T> entity);
    }
}

