using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.UseCases.Service.Interfaces;
public interface ICategoriaUseCase
{
    Task<Categoria?> GetCategoriaByIdAsync(Guid id);
    Task<List<Categoria>> GetAllCategoriasAsync();
}
