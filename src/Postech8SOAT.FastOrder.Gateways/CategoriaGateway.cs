using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;

namespace Postech8SOAT.FastOrder.Gateways;
public class CategoriaGateway : ICategoriaGateway
{
    private readonly IProdutoRepository _produtoRepository;

    public CategoriaGateway(IProdutoRepository produtoRepository)
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
