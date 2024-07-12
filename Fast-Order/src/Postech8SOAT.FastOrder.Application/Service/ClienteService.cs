using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository;
using Postech8SOAT.FastOrder.Domain.Ports.Service;
using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.Application.Service;
public class ClienteService : IClienteService
{
    private readonly IClienteRepository repository;
    public ClienteService(IClienteRepository repository)
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

    public Task<ICollection<Cliente>> GetAllClientesAsync()
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
