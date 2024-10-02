using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.UseCases.Commands.Pedidos;

public interface IPedidoServiceUseCaseInvoker
{
    Task ExecutarComandoAsync(StatusPedido status, Guid pedidoId, IPedidoUseCase pedidoService);
}
