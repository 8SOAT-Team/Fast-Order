
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.Controllers;
public class ClienteController: IClienteController
{ 
    private readonly IClienteUseCase clienteUseCase;

    public ClienteController(IClienteUseCase useCase)
    {
        
        this.clienteUseCase = useCase;
    }

    public async Task<Cliente> CreateClienteAsync(Cliente cliente)
    {
        var clienteCreated = await clienteUseCase.CreateClienteAsync(cliente);
        return await Task.FromResult(clienteCreated);
    }

    public async Task<Cliente> DeleteClienteAsync(Cliente cliente)
    {
        var clienteDeleted = await clienteUseCase.DeleteClienteAsync(cliente);
        return await Task.FromResult(clienteDeleted);
    }

    public async Task<List<Cliente>> GetAllClientesAsync()
    {
        var clientes = await clienteUseCase.GetAllClientesAsync();
        return await Task.FromResult(clientes);
    }

    public async Task<Cliente?> GetClienteByCpfAsync(string cpf)
    {
        var cliente = await clienteUseCase.GetClienteByCpfAsync(cpf);
        return await Task.FromResult(cliente);
    }

    public async Task<Cliente?> GetClienteByEmailAsync(string email)
    {
        var cliente = await clienteUseCase.GetClienteByEmailAsync(email);
        return await Task.FromResult(cliente);
    }

    public async Task<Cliente> GetClienteByIdAsync(Guid id)
    {
        var cliente = await clienteUseCase.GetClienteByIdAsync(id);
        return await Task.FromResult(cliente);
    }

    public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
    {
        var clienteUpdated = await clienteUseCase.UpdateClienteAsync(cliente);
        return await Task.FromResult(clienteUpdated);
    }

}
