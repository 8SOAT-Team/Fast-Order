using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Gateways.Interfaces;
public interface IPedidoGateway
{
    Task<Pedido> GetPedidoByIdAsync(Guid id);
    Task<Pedido> CreatePedidoAsync(Pedido pedido);
    Task<Pedido> IniciarPreparo(Guid id);
    Task<Pedido> FinalizarPreparo(Guid id);
    Task<Pedido> Entregar(Guid id);
    Task<Pedido> Cancelar(Guid id);
    Task<List<Pedido>> GetAllPedidosAsync();  
    Task<List<Pedido>> GetAllPedidosShowStatusAsync();  
    Task<Pedido?> GetPedidoCompletoAsync(Guid id);
    Task<Pedido> AtualizarPedidoPagamentoIniciadoAsync(Pedido pedido);

}
