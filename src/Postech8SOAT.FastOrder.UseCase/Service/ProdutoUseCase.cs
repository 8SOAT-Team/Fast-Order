using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.UseCases.Service;
public class ProdutoUseCase : IProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Produto> CreateProdutoAsync(Produto produto)
    {
        var categoria = await _produtoRepository.GetCategoriaByIdAsync(produto.CategoriaId);

        DomainExceptionValidation.When(categoria is null, "CategoriaId não existe");

        await _produtoRepository.AddAsync(produto);
        return produto;
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
        return _produtoRepository.FindCategoriaByIdAsync(categoriaId);
    }

    public async Task<Produto> UpdateProdutoAsync(Produto produto)
    {
        await _produtoRepository.UpdateAsync(produto);
        return produto;
    }
}
