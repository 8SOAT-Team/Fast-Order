using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.Controllers;
public class CategoriaController:ICategoriaController
{
    private readonly ICategoriaUseCase categoriaUseCase;

    public CategoriaController(ICategoriaUseCase categoriaUseCase)
    {
        this.categoriaUseCase = categoriaUseCase;
    }

    public Task<List<Categoria>> GetAllCategoriasAsync()
    {
        return categoriaUseCase.GetAllCategoriasAsync();
    }

    public Task<Categoria?> GetCategoriaByIdAsync(Guid id)
    {
        return categoriaUseCase.GetCategoriaByIdAsync(id);
    }
}
