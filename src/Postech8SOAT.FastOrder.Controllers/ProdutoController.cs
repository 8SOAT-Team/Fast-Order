using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.Controllers;
public class ProdutoController : IProdutoController
{
    private readonly IProdutoUseCase _produtoUseCase;
    private readonly ICategoriaUseCase _categoriaUseCase;

    public ProdutoController(IProdutoUseCase produtoUseCase, ICategoriaUseCase categoriaUseCase)
    {
        _produtoUseCase = produtoUseCase;
        _categoriaUseCase = categoriaUseCase;
    }

    public async Task<Produto> CreateProdutoAsync(Produto produto)
    {
        var produtoCreated = await _produtoUseCase.CreateProdutoAsync(produto);
        return await Task.FromResult(produtoCreated);
    }

    public async Task DeleteProdutoAsync(Produto produto)
    {
        await _produtoUseCase.DeleteProdutoAsync(produto);
    }


    public async Task<ICollection<Produto>> GetAllProdutosAsync()
    {
        return await _produtoUseCase.GetAllProdutosAsync();
    }

    public Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        return _produtoUseCase.GetProdutoByIdAsync(id);
    }

    public Task<Produto?> GetProdutoByNomeAsync(string nome)
    {
        return _produtoUseCase.GetProdutoByNomeAsync(nome);
    }

    public Task<ICollection<Produto>> GetProdutosByCategoria(Guid categoriaId)
    {
        return _produtoUseCase.GetProdutosByCategoria(categoriaId);
    }

    public Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId)
    {
        return _categoriaUseCase.GetCategoriaByIdAsync(categoriaId);
    }

    public async Task<Produto> UpdateProdutoAsync(Produto produto)
    {
        await _produtoUseCase.UpdateProdutoAsync(produto);
        return produto;
    }

}
