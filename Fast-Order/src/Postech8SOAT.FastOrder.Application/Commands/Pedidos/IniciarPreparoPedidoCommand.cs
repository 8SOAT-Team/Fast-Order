using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Commands.Pedidos;


internal class IniciarPreparoPedidoCommand : IPedidoServiceCommand
{
    public const StatusPedido Status = StatusPedido.EmPreparacao;
    public Task ExecutarAsync(IPedidoService pedido, Guid pedidoId) => pedido.IniciarPreparo(pedidoId);
}
