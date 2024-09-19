using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Base;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories;
public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    private readonly FastOrderContext _context;
    public ProdutoRepository(FastOrderContext context) : base(context)
    {
        this._context = context;
    }

    public Task<Categoria?> GetCategoriaByIdAsync(Guid categoriaId)
    {
       return _context.Categorias.FirstOrDefaultAsync(c => c.Id == categoriaId);
    }

    public async Task<ICollection<Produto>> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        return await _context.Produtos
            .Where(p => p.CategoriaId == categoriaId)
            .ToListAsync();
    }

    public Task<List<Categoria>> FindAllCategoriasAsync()
    {
        return _context.Categorias.ToListAsync();
    }

    public Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId)
    {
        return _context.Categorias.SingleOrDefaultAsync(c => c.Id == categoriaId);
    }
}
