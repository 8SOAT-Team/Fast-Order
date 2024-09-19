using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.UseCases.Service;

public class CategoriaUseCase : ICategoriaUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public CategoriaUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public Task<List<Categoria>> GetAllCategoriasAsync()
    {
        return _produtoRepository.FindAllCategoriasAsync();
    }

    public Task<Categoria?> GetCategoriaByIdAsync(Guid id)
    {
        return _produtoRepository.GetCategoriaByIdAsync(id);
    }
}
