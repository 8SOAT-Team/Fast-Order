using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;

namespace Postech8SOAT.FastOrder.Gateways;
public class ClienteGateway : IClienteGateway
{
    private readonly IClienteRepository repository;

    private readonly FastOrderContext _context;
    private readonly DbSet<Cliente> _clientes;

    public ClienteGateway(IClienteRepository repository, FastOrderContext context)
    {
        this.repository = repository;
        _context = context;
        _clientes = _context.Set<Cliente>();
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

    public Task<Cliente?> GetClienteByCpfAsync(Cpf cpf)
    {
        const string query = "SELECT * FROM Clientes WHERE cpf = @cpf";
        return _clientes.FromSqlRaw(query, new SqlParameter("cpf", cpf.GetSanitized()))
            .FirstOrDefaultAsync();
    }

    public async Task<Cliente> InsertCliente(Cliente cliente)
    {
        var insertedCliente = await _clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();

        return insertedCliente.Entity;
    }
}
