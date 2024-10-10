using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
public interface ICategoriaGateway
{
    Task<ICollection<Categoria>> GetAllCategoriasAsync();
    Task<Categoria?> GetCategoriaByIdAsync(Guid id);
}
