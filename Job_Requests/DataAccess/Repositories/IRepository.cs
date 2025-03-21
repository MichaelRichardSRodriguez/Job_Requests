using Job_Requests.Models;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
                                         bool tracked = false,
                                         params string[]? includeProperties); // Expression<Func<T, object>>[]? includeProperties = null
        Task<T> GetRecordAsync(Expression<Func<T, bool>>? filter = null,
                                 bool tracked = false,
                                 params string[]? includeProperties);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T,bool>> expression);
    }
}
