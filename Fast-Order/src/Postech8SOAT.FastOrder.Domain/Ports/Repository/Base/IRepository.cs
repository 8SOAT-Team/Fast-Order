using System.Linq.Expressions;

namespace Postech8SOAT.FastOrder.Domain.Ports.Repository.Base;
public interface IRepository<T>
{
    Task<ICollection<T>> FindAllAsync();
    Task<T> FindByAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> SearchAllPagedAsync(int page, int pageSize);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

