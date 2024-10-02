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

    public async Task DeleteProdutoAsync(Produto produto)
    {
        await _produtoRepository.DeleteAsync(produto);
    }

    public async Task<ICollection<Produto>> GetAllProdutosAsync()
    {
        return await _produtoRepository.FindAllAsync();
    }

    public Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        return _produtoRepository.FindByAsync(x => x.Id == id);
    }

    public Task<Produto?> GetProdutoByNomeAsync(string nome)
    {
        return _produtoRepository.FindByAsync(x => x.Nome!.Equals(nome));
    }

    public Task<ICollection<Produto>> GetProdutosByCategoria(Guid categoriaId)
    {
        return _produtoRepository.GetProdutosByCategoriaAsync(categoriaId);
    }

    public Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId)
    {
        return _categoriaGateway.GetCategoriaByIdAsync(categoriaId);
    }

    public async Task<Produto> UpdateProdutoAsync(Produto produto)
    {
        await _produtoRepository.UpdateAsync(produto);
        return produto;
    }

    public Task<Produto?> GetProdutoCompletoByIdAsync(Guid id)
    {
        return _dbContext.Set<Produto>().Include(x => x.Categoria)
             .FirstOrDefaultAsync(x => x.Id == id);
    }
}