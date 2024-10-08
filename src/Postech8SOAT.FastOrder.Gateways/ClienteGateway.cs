using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Context;

namespace Postech8SOAT.FastOrder.Gateways;
public class ClienteGateway : IClienteGateway
{

    private readonly FastOrderContext _context;
    private readonly DbSet<Cliente> _clientes;

    public ClienteGateway( FastOrderContext context)
    {
        _context = context;
        _clientes = _context.Set<Cliente>();
    }

    public Task<Cliente> CreateClienteAsync(Cliente cliente)
    {
        throw new NotImplementedException();
    }

    public Task<Cliente> DeleteClienteAsync(Cliente cliente)
    {
        throw new NotImplementedException();
    }

    public Task<List<Cliente>> GetAllClientesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Cliente?> GetClienteByCpfAsync(Cpf cpf)
    {
        const string query = "SELECT * FROM Clientes WHERE cpf = @cpf";
        return _clientes.FromSqlRaw(query, new SqlParameter("cpf", cpf.GetSanitized()))
            .FirstOrDefaultAsync();
    }

    public Task<Cliente?> GetClienteByCpfAsync(string cpf)
    {
        throw new NotImplementedException();
    }

    public Task<Cliente?> GetClienteByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Cliente> GetClienteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Cliente> InsertCliente(Cliente cliente)
    {
        var insertedCliente = await _clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();

        return insertedCliente.Entity;
    }

    public Task<Cliente> UpdateClienteAsync(Cliente cliente)
    {
        throw new NotImplementedException();
    }
}
