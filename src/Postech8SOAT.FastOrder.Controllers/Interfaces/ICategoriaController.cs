using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Controllers.Interfaces;
public interface ICategoriaController
{
    Task<Categoria?> GetCategoriaByIdAsync(Guid id);
    Task<List<Categoria>> GetAllCategoriasAsync();
}
