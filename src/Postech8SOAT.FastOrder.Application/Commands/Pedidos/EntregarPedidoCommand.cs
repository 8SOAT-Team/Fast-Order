using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Commands.Pedidos;

internal class EntregarPedidoCommand : IPedidoServiceCommand
{
    public const StatusPedido Status = StatusPedido.Finalizado;

    public Task ExecutarAsync(IPedidoService pedido, Guid pedidoId) => pedido.Entregar(pedidoId);
}
