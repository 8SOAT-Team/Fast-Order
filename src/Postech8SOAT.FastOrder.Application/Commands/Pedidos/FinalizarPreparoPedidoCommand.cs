using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Commands.Pedidos;

internal class FinalizarPreparoPedidoCommand : IPedidoServiceCommand
{
    public const StatusPedido Status = StatusPedido.Pronto;

    public Task ExecutarAsync(IPedidoService pedido, Guid pedidoId) => pedido.FinalizarPreparo(pedidoId);
}
