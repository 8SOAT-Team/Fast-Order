using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Commands.Pedidos;

public interface IPedidoServiceCommand
{
    Task ExecutarAsync(IPedidoService service, Guid pedidoId);
}
