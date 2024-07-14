using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Domain.Ports.Service;
public interface ICategoriaService
{
    Task<Categoria?> GetCategoriaByIdAsync(Guid id);
    Task<List<Categoria>> GetAllCategoriasAsync();
}
