using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository.Base;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using System.Linq.Expressions;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories.Base;

public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
{
    private readonly FastOrderContext _context;

    public Repository(FastOrderContext context)
    {
        this._context = context;
    }

    public virtual async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        _context.SaveChanges();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public Task<List<T>> FindAllAsync()
    {
        return _context.Set<T>().ToListAsync();
    }

    public Task<T?> FindByAsync(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().SingleOrDefaultAsync(predicate);
    }

    public Task<T?> FindByIdAsync(Guid id) => _context.Set<T>().SingleOrDefaultAsync(t => t.Id == id);

    public async Task<IEnumerable<T>> SearchAllPagedAsync(int page, int pageSize)
    {
        return await this._context.Set<T>()
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .AsNoTracking()
           .ToListAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Update(entity);
    }

    public Task<T?> GetById(Guid id)
    {
        return _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }
}