using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Gateways.Interfaces;
public interface ICategoriaGateway
{
    Task<List<Categoria>> GetAllCategoriasAsync();
    Task<Categoria?> GetCategoriaByIdAsync(Guid id);
}
