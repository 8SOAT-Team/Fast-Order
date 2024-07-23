using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Commands.Pedidos;

internal class CancelarPedidoCommand : IPedidoServiceCommand
{
    public const StatusPedido Status = StatusPedido.Cancelado;

    public Task ExecutarAsync(IPedidoService pedido, Guid pedidoId) => pedido.Cancelar(pedidoId);
}
