using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.UseCases.Service;
public class ClienteUseCase : IClienteUseCase
{
    private readonly IClienteGateway gateway;
    public ClienteUseCase(IClienteGateway gateway)
    {
        this.gateway = gateway;
    }

    public async Task<Cliente> CreateClienteAsync(Cliente cliente)
    {
        await gateway.CreateClienteAsync(cliente);
        return cliente;
    }

    public async Task<Cliente> DeleteClienteAsync(Cliente cliente)
    {
        await gateway.DeleteClienteAsync(cliente);
        return cliente;
    }

    public Task<List<Cliente>> GetAllClientesAsync()
    {
        return gateway.GetAllClientesAsync();
    }

    public Task<Cliente?> GetClienteByCpfAsync(string cpf)
    {
        return gateway.GetClienteByCpfAsync(cpf);
    }

    public Task<Cliente?> GetClienteByEmailAsync(string email)
    {
        return gateway.GetClienteByEmailAsync(email);
    }

    public Task<Cliente> GetClienteByIdAsync(Guid id)
    {
        return gateway.GetClienteByIdAsync(id);
    }

    public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
    {
        await gateway.UpdateClienteAsync(cliente);
        return cliente;
    }
}
