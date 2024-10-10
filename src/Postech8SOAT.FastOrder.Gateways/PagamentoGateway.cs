using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Context;

namespace Postech8SOAT.FastOrder.Gateways;
public class PagamentoGateway(FastOrderContext dbContext) : IPagamentoGateway
{

    private readonly FastOrderContext _dbContext = dbContext;

    public Task<List<Pagamento>> FindPagamentoByPedidoId(Guid pedidoId)
    {
        return _dbContext.Pagamentos.Where(p => p.PedidoId == pedidoId).ToListAsync();
    }

    public Task<Pagamento?> GetById(Guid id)
    {
        return _dbContext.Pagamentos.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pagamento> UpdatePagamentoAsync(Pagamento pagamento)
    {
        _dbContext.Pagamentos.Update(pagamento);
        await _dbContext.SaveChangesAsync();
        return pagamento;
    }
}
