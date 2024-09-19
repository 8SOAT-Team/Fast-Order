using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Gateways.Interfaces;
public interface IClienteGateway
{
    Task<Cliente> CreateClienteAsync(Cliente cliente);

    Task<Cliente> DeleteClienteAsync(Cliente cliente);

    Task<List<Cliente>> GetAllClientesAsync();

    Task<Cliente?> GetClienteByCpfAsync(string cpf);

    Task<Cliente?> GetClienteByEmailAsync(string email);

    Task<Cliente> GetClienteByIdAsync(Guid id);

    Task<Cliente> UpdateClienteAsync(Cliente cliente);

}
