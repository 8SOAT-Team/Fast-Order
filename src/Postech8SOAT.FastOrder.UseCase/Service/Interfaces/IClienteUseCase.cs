using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.UseCases.Service.Interfaces;
public interface IClienteUseCase
{
    Task<Cliente> GetClienteByIdAsync(Guid id);
    Task<Cliente?> GetClienteByCpfAsync(string cpf);
    Task<Cliente?> GetClienteByEmailAsync(string email);
    Task<Cliente> CreateClienteAsync(Cliente cliente);
    Task<Cliente> UpdateClienteAsync(Cliente cliente);
    Task<Cliente> DeleteClienteAsync(Cliente cliente);
    Task<List<Cliente>> GetAllClientesAsync();
}
