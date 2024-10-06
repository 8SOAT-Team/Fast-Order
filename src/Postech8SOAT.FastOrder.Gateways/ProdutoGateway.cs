using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Postech8SOAT.FastOrder.Gateways;
public class ProdutoGateway : IProdutoGateway
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ICategoriaGateway _categoriaGateway;
    private readonly FastOrderContext _dbContext;

    public ProdutoGateway(IProdutoRepository produtoRepository, ICategoriaGateway categoriaGateway, FastOrderContext dbContext)
    {
        _produtoRepository = produtoRepository;
        _categoriaGateway = categoriaGateway;
        _dbContext = dbContext;
    }

    public async Task<Produto> CreateProdutoAsync(Produto produto)
    {
        var inserted = await _produtoRepository.AddAsync(produto);
        return inserted;
    }

    public Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        return _produtoRepository.FindByAsync(x => x.Id == id);
    }


    public Task<Produto?> GetProdutoCompletoByIdAsync(Guid id)
    {
        return _dbContext.Set<Produto>().Include(x => x.Categoria)
             .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Produto>> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        return await _dbContext.Set<Produto>().Include(x => x.Categoria)
            .Where(x => x.CategoriaId == categoriaId)
            .ToListAsync();
    }

    public async Task<ICollection<Produto>> ListarProdutosByIdAsync(ICollection<Guid> ids)
    {
        return await _dbContext.Set<Produto>().Include(x => x.Categoria)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<ICollection<Produto>> ListarTodosProdutosAsync()
    {
        return await _dbContext.Set<Produto>().Include(x => x.Categoria).ToListAsync();
    }
}