using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Context;

namespace Postech8SOAT.FastOrder.Gateways;
public class CategoriaGateway : ICategoriaGateway
{
    private readonly FastOrderContext _dbContext;

    public CategoriaGateway(FastOrderContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Categoria>> GetAllCategoriasAsync()
    {
        return await _dbContext.Set<Categoria>().ToListAsync();
    }

    public Task<Categoria?> GetCategoriaByIdAsync(Guid id)
    {
        return _dbContext.Set<Categoria>().FirstOrDefaultAsync(c => c.Id == id);
    }
}
