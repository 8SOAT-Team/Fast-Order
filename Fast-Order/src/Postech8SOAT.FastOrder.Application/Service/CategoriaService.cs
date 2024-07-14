using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Service;

public class CategoriaService : ICategoriaService
{
    private readonly IProdutoRepository _produtoRepository;

    public CategoriaService(IProdutoRepository produtoRepository)
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
