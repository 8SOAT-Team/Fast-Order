using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Base;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories;
public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    private readonly FastOrderContext _context;
    public ProdutoRepository(FastOrderContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<ICollection<Produto>> GetProdutosByCategoria(Guid categoriaId)
    {
        return await _context.Produtos
            .Where(p => p.CategoriaId == categoriaId)
            .ToListAsync();
    }
}
