using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.UseCases.Service;

public class CategoriaUseCase : ICategoriaUseCase
{
    private readonly ICategoriaGateway _categoriaGateway;

    public CategoriaUseCase(ICategoriaGateway _categoriaGateway)
    {
        this._categoriaGateway = _categoriaGateway;
    }

    public Task<List<Categoria>> GetAllCategoriasAsync()
    {
        return _categoriaGateway.GetAllCategoriasAsync();
    }

    public Task<Categoria?> GetCategoriaByIdAsync(Guid id)
    {
        return _categoriaGateway.GetCategoriaByIdAsync(id);
    }
}
