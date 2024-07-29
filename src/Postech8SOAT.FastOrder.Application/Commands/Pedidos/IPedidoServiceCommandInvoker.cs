using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Commands.Pedidos;

public interface IPedidoServiceCommandInvoker
{
    Task ExecutarComandoAsync(StatusPedido status, Guid pedidoId, IPedidoService pedidoService);
}
