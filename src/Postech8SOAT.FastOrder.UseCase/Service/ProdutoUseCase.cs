using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.UseCases.Service;
public class ProdutoUseCase : IProdutoUseCase
{
    private readonly IProdutoGateway _produtoGateway;

    public ProdutoUseCase(IProdutoGateway produtoGateway,ICategoriaGateway categoriaGateway)
    {
        _produtoGateway = produtoGateway;
    }

    public async Task<Produto> CreateProdutoAsync(Produto produto)
    {
        await _produtoGateway.CreateProdutoAsync(produto);
        return produto;
    }

    public async Task DeleteProdutoAsync(Produto produto)
    {
        await _produtoGateway.DeleteProdutoAsync(produto);
    }


    public async Task<ICollection<Produto>> GetAllProdutosAsync()
    {
        return await _produtoGateway.GetAllProdutosAsync();
    }

    public Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        return _produtoGateway.GetProdutoByIdAsync(id);
    }

    public Task<Produto?> GetProdutoByNomeAsync(string nome)
    {
        return _produtoGateway.GetProdutoByNomeAsync(nome);
    }

    public Task<ICollection<Produto>> GetProdutosByCategoria(Guid categoriaId)
    {
        return _produtoGateway.GetProdutosByCategoria(categoriaId);
    }

    public Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId)
    {
        return _produtoGateway.FindCategoriaByIdAsync(categoriaId);
    }

    public async Task<Produto> UpdateProdutoAsync(Produto produto)
    {
        await _produtoGateway.UpdateProdutoAsync(produto);
        return produto;
    }
}
