using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;

namespace Postech8SOAT.FastOrder.Gateways;
public class PedidoGateway : IPedidoGateway
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly FastOrderContext _dbContext;

    public PedidoGateway(IPedidoRepository pedidoRepository, FastOrderContext dbContext)
    {
        _pedidoRepository = pedidoRepository;
        _dbContext = dbContext;
    }

    private async Task<Pedido> DeveEncontrarPedido(Guid id)
    {
        var pedido = await _pedidoRepository.GetById(id);

        DomainExceptionValidation.When(pedido is null, "Pedido não encontrado");

        return pedido!;
    }

    private Task SalvarPedido(Pedido pedido) => _pedidoRepository.UpdateAsync(pedido);

    public async Task<Pedido> CreatePedidoAsync(Pedido pedido)
    {
        await _pedidoRepository.AddAsync(pedido);
        var pedidoCriado = await _pedidoRepository.GetById(pedido.Id);
        return pedidoCriado!;
    }

    public Task<List<Pedido>> GetAllPedidosAsync()
    {
        return _pedidoRepository.FindAllAsync();
    }

    public async Task<Pedido> GetPedidoByIdAsync(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);

        return pedido;
    }

    public async Task<Pedido> IniciarPreparo(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);
        pedido.IniciarPreparo();
        await SalvarPedido(pedido);
        return pedido;
    }

    public async Task<Pedido> FinalizarPreparo(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);
        pedido.FinalizarPreparo();
        await SalvarPedido(pedido);
        return pedido;
    }

    public async Task<Pedido> Entregar(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);
        pedido.Entregar();
        await SalvarPedido(pedido);
        return pedido;
    }
    public async Task<Pedido> Cancelar(Guid id)
    {
        var pedido = await DeveEncontrarPedido(id);
        pedido.Cancelar();
        await SalvarPedido(pedido);
        return pedido;
    }

    public Task<Pedido?> GetPedidoCompletoAsync(Guid id)
    {
        return _dbContext.Pedidos.Include(p => p.ItensDoPedido)
             .ThenInclude(i => i.Produto)
             .Include(i => i.Cliente)
             .SingleOrDefaultAsync(i => i.Id == id);
    }

    public Task<List<Pedido>> GetAllPedidosShowStatusAsync()
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
}
