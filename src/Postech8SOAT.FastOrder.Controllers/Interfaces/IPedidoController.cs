using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Types.Results;

namespace Postech8SOAT.FastOrder.Controllers.Interfaces;
public interface IPedidoController
{
    Task<Result<PedidoDTO>> GetPedidoByIdAsync(Guid id);
    Task<Result<PedidoDTO>> CreatePedidoAsync(NovoPedidoDTO pedido);
    Task<Result<List<PedidoDTO>>> GetAllPedidosAsync();
    Task<Result<List<PedidoDTO>>> GetAllPedidosPending();
    Task<Result<PedidoDTO>> AtualizarStatusDePreparacaoDoPedido(StatusPedido status, Guid pedidoId);
}