using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Context;

namespace Postech8SOAT.FastOrder.Gateways;
public class PedidoGateway(FastOrderContext dbContext) : IPedidoGateway
{
    private readonly FastOrderContext _dbContext = dbContext;

    public Task<Pedido?> GetPedidoCompletoAsync(Guid id)
    {
        return _dbContext.Pedidos.Include(p => p.ItensDoPedido)
             .ThenInclude(i => i.Produto)
             .Include(i => i.Cliente)
             .SingleOrDefaultAsync(i => i.Id == id);
    }

    public Task<List<Pedido>> GetAllPedidosPending()
    {
        const string query = "SELECT * FROM Pedidos WHERE StatusPedido IN (3, 2, 1) ORDER BY StatusPedido DESC, DataPedido ASC";
        return _dbContext.Pedidos.FromSqlRaw(query).ToListAsync();
    }

    public async Task<Pedido> AtualizarPedidoPagamentoIniciadoAsync(Pedido pedido)
    {
        _dbContext.Entry(pedido).State = EntityState.Modified;
        _dbContext.Entry(pedido.Pagamento!).State = EntityState.Added;
        var pedidoAtualizado = await _dbContext.SaveChangesAsync();
        return pedidoAtualizado > 0 ? pedido : throw new Exception("Erro ao atualizar pedido");
    }

    public async Task<Pedido> CreateAsync(Pedido pedido)
    {
        await _dbContext.Set<Pedido>().AddAsync(pedido);
        await _dbContext.SaveChangesAsync();
        return pedido;
    }

    public Task<List<Pedido>> GetAllAsync()
    {
        return _dbContext.Pedidos.ToListAsync();
    }

    public Task<Pedido?> GetByIdAsync(Guid id)
    {
        return _dbContext.Set<Pedido>().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pedido> UpdateAsync(Pedido pedido)
    {
        _dbContext.Set<Pedido>().Update(pedido);
        await _dbContext.SaveChangesAsync();
        return pedido;
    }
}
