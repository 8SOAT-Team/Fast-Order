using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Ports.Repository.Base;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using System.Linq.Expressions;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories.Base;
public class Repository<T> : IRepository<T> where T : class
{
    private FastOrderContext context;

    public Repository(FastOrderContext context)
    {
        this.context = context;
    }

    public virtual async Task AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task<IQueryable<T>> FindAllAsync()
    {
        return context.Set<T>().AsNoTracking();
    }

    public async Task<T> FindByAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().SingleOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> SearchAllPagedAsync(int page, int pageSize)
    {
        return await this.context.Set<T>()
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .AsNoTracking()
           .ToListAsync(); ;
    }

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        context.Set<T>().Update(entity);
    }
}