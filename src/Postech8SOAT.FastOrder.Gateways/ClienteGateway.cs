using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;

namespace Postech8SOAT.FastOrder.Gateways;
public class ClienteGateway : IClienteGateway
{
    private readonly IClienteRepository repository;
    public ClienteGateway(IClienteRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Cliente> CreateClienteAsync(Cliente cliente)
    {
        await repository.AddAsync(cliente);
        return cliente;
    }

    public async Task<Cliente> DeleteClienteAsync(Cliente cliente)
    {
        await repository.DeleteAsync(cliente);
        return cliente;
    }

    public Task<List<Cliente>> GetAllClientesAsync()
    {
        return repository.FindAllAsync();
    }

    public Task<Cliente?> GetClienteByCpfAsync(string cpf)
    {
        return repository.GetClienteByCpfAsync(new Cpf(cpf));
    }

    public Task<Cliente?> GetClienteByEmailAsync(string email)
    {
        return repository.GetClienteByEmailAsync(email);
    }

    public Task<Cliente> GetClienteByIdAsync(Guid id)
    {
        return repository.GetById(id);
    }

    public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
    {
        await repository.UpdateAsync(cliente);
        return cliente;
    }

}
