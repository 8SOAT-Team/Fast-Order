using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Service;
public class ClienteService:IClienteService
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

    public async Task<ICollection<Cliente>> GetAllClientesAsync()
    {
        return await repository.FindAllAsync();
    }

    public async Task<Cliente> GetClienteByCpfAsync(string cpf)
    {
        return await repository.GetClienteByCpfAsync(cpf);
    }

    public async Task<Cliente> GetClienteByEmailAsync(string email)
    {
        return await repository.GetClienteByEmailAsync(email);
    }

    public async Task<Cliente> GetClienteByIdAsync(Guid id)
    {
        return await repository.FindByAsync(x => x.Id == id);   
    }

    public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
    {
        await repository.UpdateAsync(cliente);
        return cliente;
    }
}
