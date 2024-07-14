using Postech8SOAT.FastOrder.Domain.Entities;
using System.Linq.Expressions;

namespace Postech8SOAT.FastOrder.Domain.Ports.Repository.Base;
public interface IRepository<T> where T : class, IAggregateRoot
{
    Task<List<T>> FindAllAsync();
    Task<T?> FindByAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetById(Guid id);
    Task<IEnumerable<T>> SearchAllPagedAsync(int page, int pageSize);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> FindByIdAsync(Guid id);
}

