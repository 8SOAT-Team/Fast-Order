using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Domain.Ports.Service;
public interface IClienteService
{
    Task<Cliente> GetClienteByIdAsync(int id);
    Task<Cliente> GetClienteByCpfAsync(string cpf);
    Task<Cliente> GetClienteByEmailAsync(string email);
    Task<Cliente> CreateClienteAsync(Cliente cliente);
    Task<Cliente> UpdateClienteAsync(Cliente cliente);
    Task<Cliente> DeleteClienteAsync(Cliente cliente);
    Task<IEnumerable<Cliente>> GetAllClientesAsync();
}
