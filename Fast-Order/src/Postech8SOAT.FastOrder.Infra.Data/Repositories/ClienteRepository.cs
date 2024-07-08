using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Base;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories;
public class ClienteRepository : Repository<Cliente>,IClienteRepository
{
    private readonly FastOrderContext _context;
    public ClienteRepository(FastOrderContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Cliente> GetClienteByCpfAsync(string cpf)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
    }

    public async Task<Cliente> GetClienteByEmailAsync(string email)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Email == email);
    }

}
