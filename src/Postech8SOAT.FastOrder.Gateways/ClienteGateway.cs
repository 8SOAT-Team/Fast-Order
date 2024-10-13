using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

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
