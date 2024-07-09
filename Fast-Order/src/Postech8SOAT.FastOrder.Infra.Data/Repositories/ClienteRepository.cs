using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Base;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories;
public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    private readonly FastOrderContext _context;
    public ClienteRepository(FastOrderContext context) : base(context)
    {
        _context = context;
    }

    public Task<Cliente?> GetClienteByCpfAsync(Cpf cpf)
    {
        const string query = "SELECT * FROM Clientes WHERE cpf = @cpf";
        return _context.Clientes.FromSqlRaw(query, new SqlParameter("cpf", cpf.GetSanitized()))
            .FirstOrDefaultAsync();
    }

    public Task<Cliente?> GetClienteByEmailAsync(string email)
    {
        return _context.Clientes.FirstOrDefaultAsync(c => c.Email.ToString() == email);
    }
}
