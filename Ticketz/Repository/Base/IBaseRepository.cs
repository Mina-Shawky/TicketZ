using System.Linq.Expressions;

namespace Ticketz.Repository.Base;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>> expression);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
