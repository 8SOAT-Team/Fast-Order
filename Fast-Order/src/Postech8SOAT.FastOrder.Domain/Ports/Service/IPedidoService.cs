using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Domain.Ports.Service;
public interface IPedidoService
{
    Task<Pedido> GetPedidoByIdAsync(Guid id);
    Task<Pedido> CreatePedidoAsync(Pedido pedido);
    Task<Pedido> UpdatePedidoAsync(Pedido pedido);
    Task DeletePedidoAsync(Pedido pedido);
    Task<ICollection<Pedido>> GetAllPedidosAsync();
}
