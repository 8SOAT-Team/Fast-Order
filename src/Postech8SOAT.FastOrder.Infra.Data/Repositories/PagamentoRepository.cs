using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Base;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories;

public class PagamentoRepository : Repository<Pagamento>, IPagamentoRepository
{
    private readonly FastOrderContext _dbContext;

    public PagamentoRepository(FastOrderContext context) : base(context)
    {
        _dbContext = context;
    }

    public Task<Pagamento?> GetByPedidoId(Guid pedidoId)
    {
        return _dbContext.Set<Pagamento>().FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
    }

    public Task<List<Pagamento>> ListByPedidoId(Guid pedidoId)
    {
        return _dbContext.Set<Pagamento>().Where(p => p.PedidoId == pedidoId).ToListAsync();
    }
}